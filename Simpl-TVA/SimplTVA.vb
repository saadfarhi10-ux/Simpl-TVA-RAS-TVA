Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.IO
Imports Ionic.Zip
Imports System.Security
Imports Microsoft.Office.Interop

Public Class SimplTVA
    Dim nl As String = Environment.NewLine, kv
    Dim rappotDerreur As String = ""

    Function EscapeXMLchar(st As String) As String
        Return SecurityElement.Escape(st)
    End Function

    ' Culture fixe utilisée pour tout le parsing CSV, afin que l'import fonctionne
    ' à l'identique quels que soient les paramètres régionaux Windows du PC (virgule/point, format de date...)
    Private ReadOnly cultureCSV As New System.Globalization.CultureInfo("fr-FR")

    ' Parse un nombre décimal du CSV, quel que soit le séparateur utilisé (virgule ou point) et
    ' quels que soient les paramètres régionaux du PC (contrairement à Double.Parse seul)
    Function ParseDecimalCsv(valeur As String) As Double
        If valeur Is Nothing Then Return 0
        valeur = valeur.Trim()
        If valeur = "" Then Return 0
        valeur = valeur.Replace(".", ",")
        Return Double.Parse(valeur, cultureCSV)
    End Function

    ' Parse une date du CSV au format jj/mm/aaaa, quels que soient les paramètres régionaux du PC
    ' (contrairement à DateTime.Parse seul, qui peut confondre jour/mois selon la culture Windows)
    Function ParseDateCsv(valeur As String) As DateTime
        Dim dt As DateTime
        valeur = If(valeur, "").Trim()
        If DateTime.TryParseExact(valeur, New String() {"dd/MM/yyyy", "d/M/yyyy", "dd/MM/yyyy HH:mm:ss"},
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None, dt) Then
            Return dt
        End If
        ' Repli tolérant si le format ne correspond pas exactement
        Return DateTime.Parse(valeur, cultureCSV)
    End Function

    ' Version TryParse (ne lève pas d'exception) pour la pré-validation
    Function TryParseDateCsv(valeur As String, ByRef dt As DateTime) As Boolean
        valeur = If(valeur, "").Trim()
        If valeur = "" Then Return False
        Return DateTime.TryParseExact(valeur, New String() {"dd/MM/yyyy", "d/M/yyyy", "dd/MM/yyyy HH:mm:ss"},
                                       System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, dt)
    End Function

    ' Taux TVA autorisés selon l'année de l'opération (cf CDC EDI)
    Function TauxTvaValides(annee As Integer) As String()
        Select Case annee
            Case 2024
                Return New String() {"20", "16", "13", "12", "11", "10", "8"}
            Case 2025
                Return New String() {"20", "18", "15", "12", "10", "9"}
            Case Is >= 2026
                Return New String() {"20", "10"}
            Case Else
                Return New String() {"20", "10"}
        End Select
    End Function


    Public Sub gridStyle(g As DataGridView)
        g.ForeColor = Color.Black
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.AllowUserToAddRows = False
        g.RowHeadersVisible = False
    End Sub

    Sub gridstyle()
        Try
            If modeConnexion.ToLower() = "fournisseurnonresident" Then
                If (Grille.Rows.Count > 0) Then
                    Grille.Columns(2).Width = 160   ' Nom/Raison Sociale
                    Grille.Columns(3).Width = 160   ' Adresse Etranger
                    Grille.Columns(4).Width = 110   ' N° Identification Fiscale
                    Grille.Columns(5).Width = 130   ' Nature Opération
                    Grille.Columns(6).Width = 90    ' Date Paiement
                    Grille.Columns(7).Width = 100   ' Base Imposable HT
                    Grille.Columns(8).Width = 60    ' Taux
                    Grille.Columns(9).Width = 90    ' Tva Exigible
                    Grille.Columns(10).Width = 90   ' Montant Tva
                    Grille.Columns(11).Width = 80   ' Redevance
                End If
            ElseIf modeConnexion.ToLower() = "client" Then
                If (Grille.Rows.Count > 0) Then
                    Grille.Columns(2).Width = 70    ' N° Facture
                    Grille.Columns(3).Visible = False  ' Désignation - non utilisé côté Client
                    Grille.Columns(4).Width = 90    ' Date Facture
                    Grille.Columns(5).Width = 90    ' Identifiant Fiscal
                    Grille.Columns(6).Width = 150   ' Nom Fournisseur (= Nom Client)
                    Grille.Columns(7).Width = 110   ' ICE
                    Grille.Columns(8).Width = 90    ' Montant HT
                    Grille.Columns(9).Width = 60    ' Taux Tva
                    Grille.Columns(10).Visible = False ' Montant Tva - non utilisé côté Client
                    Grille.Columns(11).Visible = False ' Mode règlement - non utilisé côté Client
                    Grille.Columns(12).Visible = False ' N° Règlement - jamais utilisé
                    Grille.Columns(13).Width = 90    ' Date Paiement
                    Grille.Columns(14).Visible = False ' Prorata - non utilisé côté Client
                    Grille.Columns(15).Width = 90    ' Régime
                    Grille.Columns(16).Width = 100   ' Réf Nature Opération
                    Grille.Columns(17).Width = 110   ' Taux Retenue Source
                    Grille.Columns(18).Width = 100   ' Réf Type Client
                End If
            Else
                If (Grille.Rows.Count > 0) Then
                    Grille.Columns(2).Width = 70    ' N° Facture
                    Grille.Columns(3).Width = 130   ' Désignation
                    Grille.Columns(4).Width = 90    ' Date Facture
                    Grille.Columns(5).Width = 90    ' Identifiant Fiscal
                    Grille.Columns(6).Width = 150   ' Nom Fournisseur
                    Grille.Columns(7).Width = 110   ' ICE
                    Grille.Columns(8).Width = 90    ' Montant HT
                    Grille.Columns(9).Width = 60    ' Taux Tva
                    Grille.Columns(10).Width = 90   ' Montant Tva
                    Grille.Columns(11).Width = 90   ' Mode règlement
                    Grille.Columns(12).Visible = False ' N° Règlement - jamais utilisé (ni TVA ni RAS)
                    Grille.Columns(13).Width = 90    ' Date Paiement
                    Grille.Columns(14).Width = 70    ' Prorata
                    Grille.Columns(15).Width = 90    ' Régime
                    Grille.Columns(16).Width = 100   ' Réf Nature Opération
                    Grille.Columns(17).Width = 110   ' Taux Retenue Source
                    Grille.Columns(18).Visible = False ' Réf Type Client - n'existe pas côté Fournisseur
                End If
            End If

        Catch ex As Exception
            ''MsgBox(ex.Message)
        End Try

    End Sub

    Sub colorierTypeLigne()
        Try
            Dim d As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                d = Grille.Rows(i)
                Dim refOpt = d.Cells("Réf Nature Opération").Value
                Dim tauxRet = d.Cells("Taux Retenue Source").Value

                Dim estRAS As Boolean =
                    (Not IsDBNull(refOpt) AndAlso refOpt IsNot Nothing AndAlso refOpt.ToString().Trim() <> "") OrElse
                    (Not IsDBNull(tauxRet) AndAlso tauxRet IsNot Nothing AndAlso tauxRet.ToString().Trim() <> "")

                If estRAS Then
                    d.DefaultCellStyle.BackColor = Color.FromArgb(245, 200, 150)
                Else
                    d.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            ''MsgBox(ex.Message)
        End Try
    End Sub

    Sub colorierTypeLigneNonResident()
        Try
            Dim d As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                d = Grille.Rows(i)
                Dim redevance = d.Cells("Redevance").Value

                Dim estRedevance As Boolean =
                    Not IsDBNull(redevance) AndAlso redevance IsNot Nothing AndAlso
                    (redevance.ToString().Trim() = "1")

                If estRedevance Then
                    d.DefaultCellStyle.BackColor = Color.White
                Else
                    d.DefaultCellStyle.BackColor = Color.FromArgb(245, 200, 150)
                End If
            Next
        Catch ex As Exception
            ''MsgBox(ex.Message)
        End Try
    End Sub

    ' Import du CSV spécifique au mode "Fournisseur Non-Résident"
    ' Détection automatique du format (voir plus bas dans la boucle) :
    ' - 14 colonnes ou plus : IdentifiantFiscal;Annee;Periode;Regime; en tête (ignorées) puis les données
    ' - moins de 14 colonnes : directement les données, sans les 4 colonnes d'en-tête
    ' Données : NomPrenomOuRaisonSociale ; AdresseEtranger ; NIdentificationFiscale ; NatureOperation ; DatePaiement ;
    ' BaseImposableHT ; Taux ; TvaExigible ; MontantTva ; EstRedevance (0 ou 1)
    Sub importerNonResident()
        Dim i As Integer = 1
        Dim indexCol As Integer = 0
        Try
            Dim parametersF() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = MaskedTextBox1.Text},
                New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                New SqlParameter("@P3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
            }
            Dim checkEnteteGeneration As DataTable = Read("SELECT * FROM [EnteteGeneration] WHERE Libellegeneration = @P1 AND IdSociete = @P2 AND ModeConnexion = @P3 ", parametersF)
            If checkEnteteGeneration.Rows.Count >= 1 Then
                If MessageBox.Show("C'est déja traité voulez vous l'enregistrer du nouveau ?", "Traitement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            ' Pré-validation du fichier entier AVANT toute modification de la base :
            ' Redevance doit être renseignée (0 ou 1), et Nature Opération / Montant Tva sont mutuellement
            ' exclusifs (un seul des deux rempli, celui-ci étant déterminé par la valeur de Redevance)
            Dim preValidationErrorsNR As String = ""
            Dim preLineNumNR As Integer = 1
            Dim preReaderNR As StreamReader = My.Computer.FileSystem.OpenTextFileReader(TextBox1.Text)
            preReaderNR.ReadLine() ' en-tête
            While Not preReaderNR.EndOfStream
                Dim preLineNR As String = preReaderNR.ReadLine()
                Dim preArrayNR() As String = preLineNR.Split(";")

                Dim preOffsetNR As Integer = 0
                If preArrayNR.Length >= 14 Then
                    preOffsetNR = 4
                End If

                Dim preNomNR As String = IIf(IsDBNull(preArrayNR(preOffsetNR + 0)), "", preArrayNR(preOffsetNR + 0))
                If preNomNR.ToLower().Equals("fin") Then
                    Exit While
                End If

                Dim preNatureRempliNR As Boolean = preArrayNR.Length > preOffsetNR + 3 AndAlso preArrayNR(preOffsetNR + 3).ToString().Trim() <> ""
                Dim preMontantTvaRempliNR As Boolean = preArrayNR.Length > preOffsetNR + 8 AndAlso preArrayNR(preOffsetNR + 8).ToString().Trim() <> ""
                Dim preRedevanceTexteNR As String = IIf(preArrayNR.Length > preOffsetNR + 9, preArrayNR(preOffsetNR + 9).ToString().Trim(), "")

                If preRedevanceTexteNR = "" Then
                    preValidationErrorsNR &= vbNewLine & "      Ligne " & (preLineNumNR + 1) & " (" & preNomNR & ") : Redevance doit être renseignée (0 ou 1)"
                Else
                    If preNatureRempliNR AndAlso preMontantTvaRempliNR Then
                        preValidationErrorsNR &= vbNewLine & "      Ligne " & (preLineNumNR + 1) & " (" & preNomNR & ") : Nature Opération et Montant Tva ne peuvent pas être remplis tous les deux, un seul des deux"
                    End If
                    If preRedevanceTexteNR = "1" AndAlso preNatureRempliNR Then
                        preValidationErrorsNR &= vbNewLine & "      Ligne " & (preLineNumNR + 1) & " (" & preNomNR & ") : Nature Opération ne doit pas être remplie quand Redevance = 1"
                    End If
                    If preRedevanceTexteNR = "0" AndAlso preMontantTvaRempliNR Then
                        preValidationErrorsNR &= vbNewLine & "      Ligne " & (preLineNumNR + 1) & " (" & preNomNR & ") : Montant Tva ne doit pas être rempli quand Redevance = 0"
                    End If
                End If

                preLineNumNR += 1
            End While
            preReaderNR.Close()

            If preValidationErrorsNR <> "" Then
                MessageBox.Show("Import annulé - le fichier contient des erreurs :" & preValidationErrorsNR & vbNewLine & vbNewLine &
                    "Corrigez le fichier CSV source puis réessayez." & vbNewLine &
                    "Aucune donnée n'a été importée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim SSSSql = "DELETE from LigneNonResident where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 ) ; " &
                " DELETE from EnteteGeneration where  EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3; "
            Execute(SSSSql, parametersF)

            parametersF = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = MaskedTextBox1.Text},
                New SqlParameter("@P3", SqlDbType.VarChar, 255) With {.Value = Label4.Text},
                New SqlParameter("@P4", SqlDbType.DateTime) With {.Value = DateTime.Now.Date},
                New SqlParameter("@P5", SqlDbType.DateTime) With {.Value = DateTime.Now.Date},
                New SqlParameter("@P6", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
            }
            SSSSql = "insert into EnteteGeneration (IdSociete,Libellegeneration,Userid,Datecreation,Datemodification,Statut,ModeConnexion) " &
                " values(@p1,@p2,@p3,@p4,@p5,1,@p6)"
            Execute(SSSSql, parametersF)

            SSSSql = "SELECT Max(EnteteGeneration.Idgeneration) FROM EnteteGeneration"
            Dim idgeneration As Integer = Integer.Parse(Read(SSSSql).Rows(0)(0).ToString())
            Dim fileReader As StreamReader = New StreamReader(TextBox1.Text)
            fileReader.Close()
            fileReader = My.Computer.FileSystem.OpenTextFileReader(TextBox1.Text)
            Dim stringReader As String
            stringReader = fileReader.ReadLine()

            While Not fileReader.EndOfStream
                stringReader = fileReader.ReadLine()
                Dim str As String = stringReader
                Dim TestArray() As String = str.Split(";")

                ' Détection automatique du format selon le nombre de colonnes
                Dim offset As Integer = 0
                If TestArray.Length >= 14 Then
                    offset = 4
                End If

                Dim nomRaisonSociale As String = IIf(IsDBNull(TestArray(offset + 0)), "", TestArray(offset + 0))
                If nomRaisonSociale.ToLower().Equals("fin") Then
                    Exit While
                End If
                indexCol = offset + 1
                Dim adresseEtranger As String = IIf(IsDBNull(TestArray(offset + 1)), "", TestArray(offset + 1))
                Dim nIdentificationFiscale As String = IIf(IsDBNull(TestArray(offset + 2)), "", TestArray(offset + 2))
                Dim natureOperation As String = IIf(IsDBNull(TestArray(offset + 3)), "", TestArray(offset + 3))
                indexCol = offset + 5
                Dim datePaiement As Object = DBNull.Value
                If TestArray(offset + 4).ToString().Trim() <> "" Then
                    datePaiement = ParseDateCsv(TestArray(offset + 4))
                End If
                indexCol = offset + 6
                Dim baseImposableHT As Double = 0
                If TestArray(offset + 5).ToString() <> "" Then
                    baseImposableHT = ParseDecimalCsv(TestArray(offset + 5))
                End If
                indexCol = offset + 7
                Dim taux As Double = 0
                If TestArray(offset + 6).ToString() <> "" Then
                    taux = ParseDecimalCsv(TestArray(offset + 6))
                End If
                indexCol = offset + 8
                Dim tvaExigibleValue As Object = DBNull.Value
                If TestArray.Length > offset + 7 AndAlso TestArray(offset + 7).ToString().Trim() <> "" Then
                    tvaExigibleValue = ParseDecimalCsv(TestArray(offset + 7))
                End If
                indexCol = offset + 9
                Dim montantTvaValue As Object = DBNull.Value
                If TestArray.Length > offset + 8 AndAlso TestArray(offset + 8).ToString().Trim() <> "" Then
                    montantTvaValue = ParseDecimalCsv(TestArray(offset + 8))
                End If
                indexCol = offset + 10
                Dim estRedevance As Integer = 0
                If TestArray.Length > offset + 9 AndAlso TestArray(offset + 9).ToString().Trim() <> "" Then
                    estRedevance = Integer.Parse(TestArray(offset + 9))
                End If

                SSSSql = "INSERT INTO [LigneNonResident] " &
                    "([Idgeneration], [Nligne], [NomPrenomOuRaisonSociale], [AdresseEtranger], [NIdentificationFiscale], [NatureOperation], [DatePaiement], [BaseImposableHT], [Taux], [TvaExigible], [MontantTva], [EstRedevance]) " &
                    "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12)"
                parametersF = New SqlParameter() _
                {
                    New SqlParameter("@P1", SqlDbType.Int) With {.Value = idgeneration},
                    New SqlParameter("@P2", SqlDbType.Int) With {.Value = i},
                    New SqlParameter("@P3", SqlDbType.NVarChar, 255) With {.Value = nomRaisonSociale},
                    New SqlParameter("@P4", SqlDbType.NVarChar, 255) With {.Value = adresseEtranger},
                    New SqlParameter("@P5", SqlDbType.VarChar, 255) With {.Value = nIdentificationFiscale},
                    New SqlParameter("@P6", SqlDbType.NVarChar, 255) With {.Value = natureOperation},
                    New SqlParameter("@P7", SqlDbType.Date) With {.Value = datePaiement},
                    New SqlParameter("@P8", SqlDbType.Float) With {.Value = baseImposableHT},
                    New SqlParameter("@P9", SqlDbType.Float) With {.Value = taux},
                    New SqlParameter("@P10", SqlDbType.Float) With {.Value = tvaExigibleValue},
                    New SqlParameter("@P11", SqlDbType.Float) With {.Value = montantTvaValue},
                    New SqlParameter("@P12", SqlDbType.Int) With {.Value = estRedevance}
                }

                If Execute(SSSSql, parametersF) > 0 Then
                    rappotDerreur &= vbNewLine & "      Ligne : " & (i + 1) & " " & nomRaisonSociale & " | Importer avec succées"
                Else
                    rappotDerreur &= vbNewLine & "      Ligne : " & (i + 1) & " " & nomRaisonSociale & " | Non importer"
                End If
                i = i + 1
            End While
            fileReader.Close()
            MessageBox.Show("Importation avec succès", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            rappotDerreur = vbNewLine & "importation de " & (i - 1) & " enregistrements avec succès" & rappotDerreur

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & "Erreur dans la ligne : " & (i + 1) & " dans la colonne : " & indexCol & vbNewLine & "Veuillez réessayer", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub anomalie()
        Try
            Dim d As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                d = Grille.Rows(i)

                For index = 2 To 13
                    If (
                        d.Cells(index).Value.ToString().Equals("") _
                        Or IsDBNull(d.Cells(index).Value) _
                        Or d.Cells(index).Value.Equals(0)
                        ) Then
                        d.Cells(index).Style.BackColor = Color.Red
                    End If
                Next

                If d.Cells("Identifiant Fiscal").Value.ToString.Length > 8 Then
                    d.Cells("Identifiant Fiscal").Style.BackColor = Color.Red
                End If
                If d.Cells("ICE").Value.ToString.Length > 15 Then
                    d.Cells("ICE").Style.BackColor = Color.Red
                End If
            Next
        Catch ex As Exception
            ''MsgBox(ex.Message)
        End Try
    End Sub

    Sub importer()
        Dim i As Integer = 1
        Dim indexCol As Integer = 0
        Try
            ''''''' if EnteteGeneration and statut >1
            ''{code par yacine
            Dim parametersF() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = MaskedTextBox1.Text},
                New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                New SqlParameter("@P3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
            }
            Dim checkEnteteGeneration As DataTable = Read("SELECT * FROM [EnteteGeneration] WHERE Libellegeneration = @P1 AND IdSociete = @P2 AND ModeConnexion = @P3 ", parametersF)
            If checkEnteteGeneration.Rows.Count >= 1 Then
                If MessageBox.Show("C'est déja traité voulez vous l'enregistrer du nouveau ?", "Traitement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            ''}code par yacine

            ' Pré-validation du fichier entier AVANT toute modification de la base :
            ' - ICE en notation scientifique
            ' - Cohérence RefNatOpt/TauxRetenuSource
            ' - Toutes les valeurs "Valeur possible (...)" du CDC : obligatoires + dans la liste autorisée
            ' Les positions de colonnes diffèrent entre le CSV Client (11 colonnes) et le CSV Fournisseur (14 colonnes)
            Dim isClientMode As Boolean = (modeConnexion.ToLower() = "client")
            Dim dateOpIdx As Integer = 1
            Dim tauxTvaIdx As Integer = 5
            Dim iceIdx As Integer = IIf(isClientMode, 7, 10)
            Dim refNatOptIdx As Integer = IIf(isClientMode, 8, 12)
            Dim tauxRetenuIdx As Integer = IIf(isClientMode, 9, 13)
            Dim refTypCltIdx As Integer = 10 ' Client uniquement

            Dim refNatOptValides As String() = IIf(isClientMode, New String() {"4", "5", "6"}, New String() {"1", "2", "3"})
            Dim tauxRetenuValides As String() = New String() {"100", "75"}
            Dim refTypCltValides As String() = New String() {"1", "2", "3", "4", "5"}

            Dim preValidationErrors As String = ""
            Dim preLineNum As Integer = 1
            Dim preReader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(TextBox1.Text)
            preReader.ReadLine() ' en-tête
            While Not preReader.EndOfStream
                Dim preLine As String = preReader.ReadLine()
                Dim preArray() As String = preLine.Split(";")
                Dim preNumFacture As String = IIf(IsDBNull(preArray(0)), "", preArray(0))
                If preNumFacture.ToLower().Equals("fin") Then
                    Exit While
                End If
                If preArray.Length > iceIdx Then
                    Dim preIce As String = IIf(IsDBNull(preArray(iceIdx)), "", preArray(iceIdx))
                    If System.Text.RegularExpressions.Regex.IsMatch(preIce, "^[0-9]+([.,][0-9]+)?[eE][+\-]?[0-9]+$") Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : ICE en notation scientifique (" & preIce & ")"
                    End If
                End If

                ' tauxTva : obligatoire + doit correspondre à l'année de dateOperation
                Dim preTauxTva As String = IIf(preArray.Length > tauxTvaIdx, preArray(tauxTvaIdx).ToString().Trim(), "")
                If preTauxTva = "" Then
                    preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : tauxTva doit être renseigné"
                Else
                    Dim preAnnee As Integer = 0
                    If preArray.Length > dateOpIdx AndAlso preArray(dateOpIdx).ToString().Trim() <> "" Then
                        Dim preDt As DateTime
                        If TryParseDateCsv(preArray(dateOpIdx), preDt) Then
                            preAnnee = preDt.Year
                        End If
                    End If
                    If preAnnee > 0 AndAlso Not TauxTvaValides(preAnnee).Contains(preTauxTva) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : tauxTva (" & preTauxTva & ") invalide pour l'année " & preAnnee & " - valeurs possibles : " & String.Join("/", TauxTvaValides(preAnnee))
                    End If
                End If

                If isClientMode Then
                    ' Côté Client, refNatOpt / tauxRetenu / refTypClt sont TOUJOURS obligatoires
                    Dim preRefNatOpt As String = IIf(preArray.Length > refNatOptIdx, preArray(refNatOptIdx).ToString().Trim(), "")
                    Dim preTauxRetenu As String = IIf(preArray.Length > tauxRetenuIdx, preArray(tauxRetenuIdx).ToString().Trim(), "")
                    Dim preRefTypClt As String = IIf(preArray.Length > refTypCltIdx, preArray(refTypCltIdx).ToString().Trim(), "")

                    If preRefNatOpt = "" Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : refNatOpt doit être renseigné"
                    ElseIf Not refNatOptValides.Contains(preRefNatOpt) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : refNatOpt (" & preRefNatOpt & ") invalide - valeurs possibles : " & String.Join("/", refNatOptValides)
                    End If

                    If preTauxRetenu = "" Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : tauxRetenu doit être renseigné"
                    ElseIf Not tauxRetenuValides.Contains(preTauxRetenu) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : tauxRetenu (" & preTauxRetenu & ") invalide - valeurs possibles : " & String.Join("/", tauxRetenuValides)
                    End If

                    If preRefTypClt = "" Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : refTypClt doit être renseigné"
                    ElseIf Not refTypCltValides.Contains(preRefTypClt) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : refTypClt (" & preRefTypClt & ") invalide - valeurs possibles : " & String.Join("/", refTypCltValides)
                    End If
                Else
                    ' Côté Fournisseur, refNatOpt / tauxRetenuSource sont optionnels (ligne RAS ou non),
                    ' mais doivent être soit tous les deux vides, soit tous les deux remplis et valides
                    Dim preRefRempli As Boolean = preArray.Length > refNatOptIdx AndAlso preArray(refNatOptIdx).ToString().Trim() <> ""
                    Dim preTauxRempli As Boolean = preArray.Length > tauxRetenuIdx AndAlso preArray(tauxRetenuIdx).ToString().Trim() <> ""
                    If preRefRempli <> preTauxRempli Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : Réf Nature Opération et Taux Retenue Source doivent être soit tous les deux vides, soit tous les deux remplis"
                    End If
                    If preRefRempli AndAlso Not refNatOptValides.Contains(preArray(refNatOptIdx).ToString().Trim()) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : refNatOpt (" & preArray(refNatOptIdx).ToString().Trim() & ") invalide - valeurs possibles : " & String.Join("/", refNatOptValides)
                    End If
                    If preTauxRempli AndAlso Not tauxRetenuValides.Contains(preArray(tauxRetenuIdx).ToString().Trim()) Then
                        preValidationErrors &= vbNewLine & "      Ligne " & (preLineNum + 1) & " (N° facture " & preNumFacture & ") : tauxRetenuSource (" & preArray(tauxRetenuIdx).ToString().Trim() & ") invalide - valeurs possibles : " & String.Join("/", tauxRetenuValides)
                    End If
                End If

                preLineNum += 1
            End While
            preReader.Close()

            If preValidationErrors <> "" Then
                MessageBox.Show("Import annulé - le fichier contient des erreurs :" & preValidationErrors & vbNewLine & vbNewLine &
                    "Corrigez le fichier CSV source puis réessayez." & vbNewLine &
                    "Aucune donnée n'a été importée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim SSSSql = "DELETE from LigneGeneration where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 ) ; " &
                " DELETE from EnteteGeneration where  EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3; "
            Execute(SSSSql, parametersF)

            parametersF = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = MaskedTextBox1.Text},
                New SqlParameter("@P3", SqlDbType.VarChar, 255) With {.Value = Label4.Text},
                New SqlParameter("@P4", SqlDbType.DateTime) With {.Value = DateTime.Now.Date},
                New SqlParameter("@P5", SqlDbType.DateTime) With {.Value = DateTime.Now.Date},
                New SqlParameter("@P6", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
            }
            SSSSql = "insert into EnteteGeneration (IdSociete,Libellegeneration,Userid,Datecreation,Datemodification,Statut,ModeConnexion) " &
                " values(@p1,@p2,@p3,@p4,@p5,1,@p6)"
            Execute(SSSSql, parametersF)

            SSSSql = "SELECT Max(EnteteGeneration.Idgeneration) FROM EnteteGeneration"
            'Dim idgeneration As Integer = Convert.ToInt16(comidgeneration.ExecuteScalar)
            Dim idgeneration As Integer = Integer.Parse(Read(SSSSql).Rows(0)(0).ToString())
            Dim fileReader As StreamReader = New StreamReader(TextBox1.Text)
            fileReader.Close()
            fileReader = My.Computer.FileSystem.OpenTextFileReader(TextBox1.Text)
            Dim stringReader As String
            stringReader = fileReader.ReadLine()

            While Not fileReader.EndOfStream
                Dim numfacture, datefacture, identifiant, fournisseur, modereglement, nreglement, designation, datepaiement, ice As String
                Dim montantht, tauxtva, montanttva As Double
                Dim prorata As Double = 0
                Dim refnatoptValue As Object = DBNull.Value
                Dim tauxretenuesourceValue As Object = DBNull.Value
                Dim reftypcltValue As Object = DBNull.Value

                stringReader = fileReader.ReadLine()
                Dim str As String
                Dim TestArray() As String

                str = stringReader
                TestArray = str.Split(";")
                numfacture = IIf(IsDBNull(TestArray(0)), "", TestArray(0))
                If numfacture.ToLower().Equals("fin") Then
                    Exit While
                End If

                If modeConnexion.ToLower() = "client" Then
                    ' Format Client (11 colonnes) :
                    ' numFacture;dateOperation;if;nom;montantHT;tauxTva;datePaiement;ICE;refNatOpt;tauxRetenu;RefTypClt
                    indexCol = 2
                    datefacture = IIf(IsDBNull(TestArray(1)), DateTime.Now.Date, ParseDateCsv(TestArray(1)))
                    identifiant = IIf(IsDBNull(TestArray(2)), "", TestArray(2))
                    fournisseur = IIf(IsDBNull(TestArray(3)), "", TestArray(3))
                    indexCol = 5
                    montantht = 0
                    If TestArray(4).ToString <> "" Then
                        montantht = ParseDecimalCsv(TestArray(4))
                    End If
                    indexCol = 6
                    tauxtva = 0
                    If TestArray(5).ToString <> "" Then
                        tauxtva = ParseDecimalCsv(TestArray(5))
                    End If
                    ' Pas de Montant Tva / Mode Reglement / N Reglement / Designation / Prorata côté Client
                    montanttva = 0
                    modereglement = ""
                    nreglement = ""
                    designation = ""
                    indexCol = 7
                    datepaiement = IIf(IsDBNull(TestArray(6)), "", ParseDateCsv(TestArray(6)))
                    ice = IIf(IsDBNull(TestArray(7)), "", TestArray(7))

                    indexCol = 9
                    If TestArray.Length > 8 AndAlso TestArray(8).ToString().Trim() <> "" Then
                        refnatoptValue = TestArray(8)
                    End If
                    If TestArray.Length > 9 AndAlso TestArray(9).ToString().Trim() <> "" Then
                        tauxretenuesourceValue = ParseDecimalCsv(TestArray(9))
                    End If
                    ' RefTypClt : uniquement côté Client
                    If TestArray.Length > 10 AndAlso TestArray(10).ToString().Trim() <> "" Then
                        reftypcltValue = TestArray(10).ToString().Trim()
                    End If

                Else
                    ' Format Fournisseur (14 colonnes) :
                    ' numFacture;dateOperation;ifuFournisseur;nom;montantHT;tauxTva;Montant Tva;Mode paiement;Designation;datePaiement;ICE;Prorata;refNatOpt;tauxRetenuSource
                    indexCol = 2
                    datefacture = IIf(IsDBNull(TestArray(1)), DateTime.Now.Date, ParseDateCsv(TestArray(1)))
                    identifiant = IIf(IsDBNull(TestArray(2)), "", TestArray(2))
                    fournisseur = IIf(IsDBNull(TestArray(3)), "", TestArray(3))
                    indexCol = 5
                    montantht = 0
                    If TestArray(4).ToString <> "" Then
                        montantht = ParseDecimalCsv(TestArray(4))
                    End If
                    indexCol = 6
                    tauxtva = 0
                    If TestArray(5).ToString <> "" Then
                        tauxtva = ParseDecimalCsv(TestArray(5))
                    End If
                    indexCol = 7
                    montanttva = 0
                    If TestArray(6).ToString <> "" Then
                        montanttva = ParseDecimalCsv(TestArray(6))
                    End If
                    modereglement = IIf(IsDBNull(TestArray(7)), "", TestArray(7))
                    ' N Reglement retiré du CSV Fournisseur (jamais utilisé dans les XML)
                    nreglement = ""
                    designation = IIf(IsDBNull(TestArray(8)), "", TestArray(8))
                    datepaiement = IIf(IsDBNull(TestArray(9)), "", ParseDateCsv(TestArray(9)))
                    ice = IIf(IsDBNull(TestArray(10)), "", TestArray(10))

                    If Not TestArray(11).ToString().Equals("") Then
                        prorata = ParseDecimalCsv(TestArray(11))
                    End If

                    If TestArray.Length > 12 AndAlso Not IsDBNull(TestArray(12)) AndAlso TestArray(12).ToString().Trim() <> "" Then
                        refnatoptValue = TestArray(12)
                    End If
                    If TestArray.Length > 13 AndAlso TestArray(13).ToString().Trim() <> "" Then
                        tauxretenuesourceValue = ParseDecimalCsv(TestArray(13))
                    End If
                    ' RefTypClt n'existe pas côté Fournisseur : reftypcltValue reste DBNull
                End If

                SSSSql = "INSERT INTO [LigneGeneration] " &
                    "([Idgeneration], [Nligne], [Numfacture], [Datefacture], [Identifiantfiscale], [Fournisseur], [MontantHT], [TauxTVA], [MontantTVA], [Modereglement], [Numreglement], [Designation], [Datepaiement], [ice], [prorata], [RefNatOpt], [TauxRetenuSource], [RefTypClt]) " &
                    "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15,@P16,@P17,@P18)"
                parametersF = New SqlParameter() _
                {
                    New SqlParameter("@P1", SqlDbType.Int) With {.Value = idgeneration},
                    New SqlParameter("@P2", SqlDbType.Int) With {.Value = i},
                    New SqlParameter("@P3", SqlDbType.VarChar, 255) With {.Value = numfacture},
                    New SqlParameter("@P4", SqlDbType.DateTime) With {.Value = datefacture},
                    New SqlParameter("@P5", SqlDbType.VarChar, 255) With {.Value = identifiant},
                    New SqlParameter("@P6", SqlDbType.VarChar, 255) With {.Value = fournisseur},
                    New SqlParameter("@P7", SqlDbType.Float) With {.Value = montantht},
                    New SqlParameter("@P8", SqlDbType.Float) With {.Value = tauxtva},
                    New SqlParameter("@P9", SqlDbType.Float) With {.Value = montanttva},
                    New SqlParameter("@P10", SqlDbType.VarChar, 255) With {.Value = modereglement},
                    New SqlParameter("@P11", SqlDbType.VarChar, 255) With {.Value = nreglement},
                    New SqlParameter("@P12", SqlDbType.VarChar, 255) With {.Value = designation},
                    New SqlParameter("@P13", SqlDbType.DateTime) With {.Value = datepaiement},
                    New SqlParameter("@P14", SqlDbType.VarChar, 255) With {.Value = ice},
                    New SqlParameter("@P15", SqlDbType.Float) With {.Value = prorata},
                    New SqlParameter("@P16", SqlDbType.VarChar, 50) With {.Value = refnatoptValue},
                    New SqlParameter("@P17", SqlDbType.Float) With {.Value = tauxretenuesourceValue},
                    New SqlParameter("@P18", SqlDbType.VarChar, 10) With {.Value = reftypcltValue}
                }

                If Execute(SSSSql, parametersF) > 0 Then
                    rappotDerreur &= vbNewLine & "      Ligne : " & (i + 1) & " N° de facture " & numfacture & "  IF : " & identifiant & " | Importer avec succées"
                Else
                    rappotDerreur &= vbNewLine & "      Ligne : " & (i + 1) & " N° de facture " & numfacture & "  IF : " & identifiant & " | Non importer"
                End If
                i = i + 1
            End While
            fileReader.Close()
            MessageBox.Show("Importation avec succès", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'MsgBox("Ajout avec succès", MsgBoxStyle.Information, "Simpl-TVA")
            rappotDerreur = vbNewLine & "importation de " & (i - 1) & " enregistrements avec succès" & rappotDerreur

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & "Erreur dans la ligne : " & (i + 1) & " dans la colonne : " & indexCol & vbNewLine & "Veuillez réessayer", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Error)
            'MsgBox()
        End Try

    End Sub

    Sub remplircombo()
        Try
            cnx.Close()
            cnx.Open()
            Dim com1 As New SqlCommand
            com1.Connection = cnx
            com1.CommandText = "(SELECT EnteteGeneration.Libellegeneration, EnteteGeneration.Statut FROM EnteteGeneration Where idsociete=@p1 and ModeConnexion=@p2) union (select '','' from EnteteGeneration)"
            With com1.Parameters
                .Add("@p1", SqlDbType.Int).Value = ComboBox1.SelectedValue
                .Add("@p2", SqlDbType.VarChar, 50).Value = modeConnexion
            End With
            Dim dr As SqlDataReader
            dr = com1.ExecuteReader
            Dim t As New DataTable()
            t.Load(dr)
            ComboBox2.DataSource = t
            ComboBox2.DisplayMember = "Libellegeneration"
            ComboBox2.ValueMember = "Statut"
            ComboBox2.Text = ""
            dr.Close()
            Dim com2 As New SqlCommand
            com2.Connection = cnx
            com2.CommandText = "(SELECT EnteteGeneration.Libellegeneration, EnteteGeneration.Statut FROM EnteteGeneration Where Statut in (1,2,3) and idsociete=@p1 and ModeConnexion=@p2) union (select '','' from EnteteGeneration)"
            With com2.Parameters
                .Add("@p1", SqlDbType.Int).Value = ComboBox1.SelectedValue
                .Add("@p2", SqlDbType.VarChar, 50).Value = modeConnexion
            End With
            Dim dr1 As SqlDataReader
            dr1 = com2.ExecuteReader
            Dim t1 As New DataTable()
            t1.Load(dr1)
            ComboBox3.DataSource = t1
            ComboBox3.DisplayMember = "Libellegeneration"
            ComboBox3.ValueMember = "Statut"
            ComboBox3.Text = ""
            dr1.Close()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label4.Text = curentUser
        LabelModeConnexion.Text = "Mode : " & modeConnexion

        If modeConnexion.ToLower() = "client" OrElse modeConnexion.ToLower() = "fournisseurnonresident" Then
            ComboBoxTypeExport.Visible = False
            LabelTypeExport.Visible = False
        End If
        If ComboBoxTypeExport.Items.Count > 0 Then
            ComboBoxTypeExport.SelectedIndex = 0
        End If
        Timer1.Start()
        Try
            cnx.Close()
            cnx.Open()
            'LabelClient.Text = ComboBox1.Text
            Dim dtAt As Date = DateTime.Now.AddMonths(-1)
            Dim m As Integer = dtAt.Month
            Dim y As Integer = dtAt.Year
            MaskedTextBox1.Text = "0" & m & "/" & y
            If m > 9 Then
                MaskedTextBox1.Text = m & "/" & y
            End If
            Dim com11 As New SqlCommand
            com11.Connection = cnx
            com11.CommandText = "(SELECT Societe.Idsociete, Societe.RaisonSociale FROM Societe "
            com11.CommandText &= "WHERE Idsociete IN (" & linkedsocities & ") ) "
            com11.CommandText &= "UNION (select '' AS Idsociete,'' AS RaisonSociale FROM  Societe)  ORDER BY Societe.Idsociete DESC"
            Dim dr11 As SqlDataReader
            dr11 = com11.ExecuteReader
            Dim t11 As New DataTable()
            t11.Load(dr11)
            ComboBox1.DataSource = t11
            ComboBox1.DisplayMember = "RaisonSociale"
            ComboBox1.ValueMember = "Idsociete"
            ComboBox1.Text = ""
            dr11.Close()
            remplircombo()
            If Not Read("SELECT TOP 1 * FROM LigneGeneration").Columns.Contains("prorata") Then
                Execute("ALTER TABLE [LigneGeneration] ADD [prorata] float", Nothing)
            End If
            If Not Read("SELECT TOP 1 * FROM LigneGeneration").Columns.Contains("RefNatOpt") Then
                Execute("ALTER TABLE [LigneGeneration] ADD [RefNatOpt] varchar(50)", Nothing)
            End If
            If Not Read("SELECT TOP 1 * FROM LigneGeneration").Columns.Contains("TauxRetenuSource") Then
                Execute("ALTER TABLE [LigneGeneration] ADD [TauxRetenuSource] float", Nothing)
            End If
            If Not Read("SELECT TOP 1 * FROM LigneGeneration").Columns.Contains("RefTypClt") Then
                Execute("ALTER TABLE [LigneGeneration] ADD [RefTypClt] varchar(10)", Nothing)
            End If
            Dim checkTableNonResident As DataTable = Read("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'LigneNonResident'")
            If Not Read("SELECT TOP 1 * FROM EnteteGeneration").Columns.Contains("ModeConnexion") Then
                Execute("ALTER TABLE [EnteteGeneration] ADD [ModeConnexion] varchar(50) NULL", Nothing)
            End If
            If checkTableNonResident.Rows.Count = 0 Then
                Execute("CREATE TABLE [LigneNonResident] (" &
                    "[Idgeneration] [int] NOT NULL, " &
                    "[Nligne] [int] NOT NULL, " &
                    "[NomPrenomOuRaisonSociale] [nvarchar](255) NULL, " &
                    "[AdresseEtranger] [nvarchar](255) NULL, " &
                    "[NIdentificationFiscale] [varchar](255) NULL, " &
                    "[NatureOperation] [nvarchar](255) NULL, " &
                    "[DatePaiement] [date] NULL, " &
                    "[BaseImposableHT] [float] NULL, " &
                    "[Taux] [float] NULL, " &
                    "[TvaExigible] [float] NULL, " &
                    "[MontantTva] [float] NULL, " &
                    "[EstRedevance] [int] NULL, " &
                    "CONSTRAINT [PK_LigneNonResident] PRIMARY KEY ([Nligne],[Idgeneration]))", Nothing)
            End If
        Catch ex As Exception
            ''MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub CmdCpt_Click(sender As Object, e As EventArgs) Handles CmdCpt.Click
        Try
            OpenFileDialog1.Filter = "Text Files (.csv)|*.csv|All Files (*.*)|*.*"
            OpenFileDialog1.ShowDialog()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Try
            TextBox1.Text = OpenFileDialog1.FileName
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        ErrorProvider1.Clear()
        Try
            rappotDerreur = ""
            cnx.Close()
            cnx.Open()
            If MaskedTextBox1.Text.Equals("  /") Then
                ErrorProvider1.SetError(MaskedTextBox1, "Obligatoire")
                Exit Sub
            End If
            If (ComboBox1.Text = "") Then
                MsgBox("Veuillez choisir la société", MsgBoxStyle.Information, "Simpl-TVA")
                ErrorProvider1.SetError(ComboBox1, "Veuillez choisir la société")
            Else
                Dim regime As String = Read("SELECT [regime] FROM [Societe] WHERE [Idsociete] = " & ComboBox1.SelectedValue.ToString()).Rows(0)(0).ToString()
                If regime.ToLower().Equals("trimestriel") Then
                    If Integer.Parse(MaskedTextBox1.Text.Split("/")(0).ToString()) > 4 Then
                        MsgBox("La société sélectionnée a un régime trimestriel (période autorisée de 1 à 4)", MsgBoxStyle.Information, "Simpl-TVA")
                        ErrorProvider1.SetError(MaskedTextBox1, "La société sélectionnée a un régime trimestriel (période autorisée de 1 à 4)")
                        Exit Sub
                    End If
                End If
                If TextBox1.Text.Equals("") Then
                    MsgBox("Veuillez ouvrir votre fichier CVS", MsgBoxStyle.Information, "Simpl-TVA")
                    ErrorProvider1.SetError(TextBox1, "Veuillez ouvrir votre fichier CVS")
                    Exit Sub
                End If
                If modeConnexion.ToLower() = "fournisseurnonresident" Then
                    importerNonResident()
                Else
                    importer()
                End If
                remplircombo()
            End If
            TextBoxRapprt.Text = rappotDerreur
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            anomalie()
        Catch ex As Exception

        End Try
    End Sub

    Function verifier(ByVal libelle As String) As Integer
        Try
            Dim statut As Integer
            Dim com As New SqlCommand
            com.Connection = cnx
            com.CommandText = "SELECT EnteteGeneration.Statut FROM EnteteGeneration WHERE EnteteGeneration.Libellegeneration=@p1  and [IdSociete]=@p2 and ModeConnexion=@p3"
            With com.Parameters
                .Add("@p1", SqlDbType.VarChar, 255).Value = libelle
                .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
            End With
            If (IsDBNull(com.ExecuteScalar)) Then
                statut = 0
            Else
                statut = Convert.ToInt16(com.ExecuteScalar)
            End If
            Return statut
        Catch ex As Exception
        End Try

    End Function

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try
            cnx.Close()
            cnx.Open()
            'If (verifier(ComboBox2.Text) = 1) Then
            If (True) Then

                If modeConnexion.ToLower() = "fournisseurnonresident" Then

                    ' Pré-validation de la grille AVANT toute modification de la base :
                    ' même règle que pour l'import CSV (Redevance obligatoire, Nature Opération / Montant Tva
                    ' mutuellement exclusifs selon la valeur de Redevance)
                    Dim preValidationErrorsGridNR As String = ""
                    For iv As Integer = 0 To Grille.Rows.Count - 1
                        Dim dv As DataGridViewRow = Grille.Rows(iv)
                        Dim natureRempliV As Boolean = Not IsDBNull(dv.Cells("Nature Opération").Value) AndAlso dv.Cells("Nature Opération").Value.ToString().Trim() <> ""
                        Dim montantTvaRempliV As Boolean = Not IsDBNull(dv.Cells("Montant Tva").Value) AndAlso dv.Cells("Montant Tva").Value.ToString().Trim() <> ""
                        Dim redevanceTexteV As String = IIf(IsDBNull(dv.Cells("Redevance").Value), "", dv.Cells("Redevance").Value.ToString().Trim())

                        If redevanceTexteV = "" Then
                            preValidationErrorsGridNR &= vbNewLine & "      Ligne " & (iv + 1) & " : Redevance doit être renseignée (0 ou 1)"
                        Else
                            If natureRempliV AndAlso montantTvaRempliV Then
                                preValidationErrorsGridNR &= vbNewLine & "      Ligne " & (iv + 1) & " : Nature Opération et Montant Tva ne peuvent pas être remplis tous les deux, un seul des deux"
                            End If
                            If redevanceTexteV = "1" AndAlso natureRempliV Then
                                preValidationErrorsGridNR &= vbNewLine & "      Ligne " & (iv + 1) & " : Nature Opération ne doit pas être remplie quand Redevance = 1"
                            End If
                            If redevanceTexteV = "0" AndAlso montantTvaRempliV Then
                                preValidationErrorsGridNR &= vbNewLine & "      Ligne " & (iv + 1) & " : Montant Tva ne doit pas être rempli quand Redevance = 0"
                            End If
                        End If
                    Next

                    If preValidationErrorsGridNR <> "" Then
                        MessageBox.Show("Modification annulée - la grille contient des erreurs :" & preValidationErrorsGridNR & vbNewLine & vbNewLine &
                            "Corrigez les lignes concernées puis réessayez." & vbNewLine &
                            "Aucune donnée n'a été modifiée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ' Recharge la grille depuis la base pour effacer la saisie invalide à l'écran
                        ' et ne garder que la dernière version correcte enregistrée
                        ComboBox2_SelectedIndexChanged(Nothing, EventArgs.Empty)
                        Exit Sub
                    End If

                    Dim parametersNRV() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                        New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                        New SqlParameter("@P3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                    }
                    Dim yyySqlNR = "DELETE from LigneNonResident where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                        " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 )  "
                    Execute(yyySqlNR, parametersNRV)

                    yyySqlNR = "Update EnteteGeneration set statut=2 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 "
                    Execute(yyySqlNR, parametersNRV)

                    Dim dNR As New DataGridViewRow
                    For i As Integer = 0 To Grille.Rows.Count - 1
                        dNR = Grille.Rows(i)
                        Dim idg As String = IIf(IsDBNull(dNR.Cells(0).Value), "", dNR.Cells(0).Value)
                        Dim nligneNR As String = IIf(IsDBNull(dNR.Cells(1).Value), "", dNR.Cells(1).Value)
                        Dim nomNR As String = IIf(IsDBNull(dNR.Cells("Nom/Raison Sociale").Value), "", dNR.Cells("Nom/Raison Sociale").Value)
                        Dim adresseNR As String = IIf(IsDBNull(dNR.Cells("Adresse Etranger").Value), "", dNR.Cells("Adresse Etranger").Value)
                        Dim nifNR As String = IIf(IsDBNull(dNR.Cells("N° Identification Fiscale").Value), "", dNR.Cells("N° Identification Fiscale").Value)
                        Dim natureNR As String = IIf(IsDBNull(dNR.Cells("Nature Opération").Value), "", dNR.Cells("Nature Opération").Value)

                        Dim datePaiementNRValue As Object = DBNull.Value
                        If Not IsDBNull(dNR.Cells("Date Paiement").Value) AndAlso dNR.Cells("Date Paiement").Value.ToString().Trim() <> "" Then
                            datePaiementNRValue = dNR.Cells("Date Paiement").Value
                        End If

                        Dim baseHTNR As Double = IIf(IsDBNull(dNR.Cells("Base Imposable HT").Value), 0, dNR.Cells("Base Imposable HT").Value)
                        Dim tauxNR As Double = IIf(IsDBNull(dNR.Cells("Taux").Value), 0, dNR.Cells("Taux").Value)

                        Dim tvaExigibleNRValue As Object = DBNull.Value
                        If Not IsDBNull(dNR.Cells("Tva Exigible").Value) AndAlso dNR.Cells("Tva Exigible").Value.ToString().Trim() <> "" Then
                            tvaExigibleNRValue = dNR.Cells("Tva Exigible").Value
                        End If
                        Dim montantTvaNRValue As Object = DBNull.Value
                        If Not IsDBNull(dNR.Cells("Montant Tva").Value) AndAlso dNR.Cells("Montant Tva").Value.ToString().Trim() <> "" Then
                            montantTvaNRValue = dNR.Cells("Montant Tva").Value
                        End If
                        Dim estRedevanceNR As Integer = IIf(IsDBNull(dNR.Cells("Redevance").Value), 0, dNR.Cells("Redevance").Value)

                        Dim comNR As New SqlCommand
                        comNR.Connection = cnx
                        comNR.CommandText = "INSERT INTO [LigneNonResident] " &
                            "([Idgeneration], [Nligne], [NomPrenomOuRaisonSociale], [AdresseEtranger], [NIdentificationFiscale], [NatureOperation], [DatePaiement], [BaseImposableHT], [Taux], [TvaExigible], [MontantTva], [EstRedevance]) " &
                            "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12)"
                        With comNR.Parameters
                            .Add("@p1", SqlDbType.Int).Value = idg
                            .Add("@p2", SqlDbType.Int).Value = nligneNR
                            .Add("@p3", SqlDbType.NVarChar, 255).Value = nomNR
                            .Add("@p4", SqlDbType.NVarChar, 255).Value = adresseNR
                            .Add("@p5", SqlDbType.VarChar, 255).Value = nifNR
                            .Add("@p6", SqlDbType.NVarChar, 255).Value = natureNR
                            .Add("@p7", SqlDbType.Date).Value = datePaiementNRValue
                            .Add("@p8", SqlDbType.Float).Value = baseHTNR
                            .Add("@p9", SqlDbType.Float).Value = tauxNR
                            .Add("@p10", SqlDbType.Float).Value = tvaExigibleNRValue
                            .Add("@p11", SqlDbType.Float).Value = montantTvaNRValue
                            .Add("@p12", SqlDbType.Int).Value = estRedevanceNR
                        End With
                        comNR.ExecuteNonQuery()
                    Next
                    MsgBox("Modification avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    Dim libelleNR As String = ComboBox2.Text
                    remplircombo()
                    ComboBox2.Text = libelleNR
                    Exit Sub
                End If

                Dim parametersF() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                    New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                    New SqlParameter("@P3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                }

                ' Sauvegarde des anciennes valeurs RefNatOpt/TauxRetenuSource/RefTypClt (par Nligne) avant suppression,
                ' pour pouvoir annuler une modification incohérente saisie manuellement dans la grille
                Dim oldValuesTable As DataTable = Read("SELECT LigneGeneration.Nligne, LigneGeneration.RefNatOpt, LigneGeneration.TauxRetenuSource, LigneGeneration.RefTypClt " &
                    "FROM LigneGeneration INNER JOIN EnteteGeneration ON LigneGeneration.Idgeneration = EnteteGeneration.Idgeneration " &
                    "WHERE EnteteGeneration.Libellegeneration=@P1 and EnteteGeneration.[IdSociete]=@P2 and EnteteGeneration.ModeConnexion=@P3", parametersF)
                Dim oldValuesDict As New Dictionary(Of Integer, Object())
                For Each r As DataRow In oldValuesTable.Rows
                    oldValuesDict(Convert.ToInt32(r("Nligne"))) = New Object() {r("RefNatOpt"), r("TauxRetenuSource"), r("RefTypClt")}
                Next

                Dim yyySql = "DELETE from LigneGeneration where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                    " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 )  "
                Execute(yyySql, parametersF)

                yyySql = "Update EnteteGeneration set statut=2 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3 "
                Execute(yyySql, parametersF)

                Dim d As New DataGridViewRow
                For i As Integer = 0 To Grille.Rows.Count - 1
                    d = Grille.Rows(i)
                    Dim t1, t2, t3, t4, t5, t6, t11, t12, t13, t10, t14, t15, t16 As String
                    Dim t7, t8, t9, t17 As Double

                    t1 = IIf(IsDBNull(d.Cells(0).Value), "", d.Cells(0).Value)
                    t2 = IIf(IsDBNull(d.Cells(1).Value), "", d.Cells(1).Value)

                    t3 = IIf(IsDBNull(d.Cells("N° Facture").Value), "", d.Cells("N° Facture").Value)
                    t4 = IIf(IsDBNull(d.Cells("Date Facture").Value), "", d.Cells("Date Facture").Value)
                    t5 = IIf(IsDBNull(d.Cells("Identifiant Fiscal").Value), "", d.Cells("Identifiant Fiscal").Value)
                    t6 = IIf(IsDBNull(d.Cells("Nom Fournisseur").Value), "", d.Cells("Nom Fournisseur").Value)
                    t7 = IIf(IsDBNull(d.Cells("Montant HT").Value), 0, d.Cells("Montant HT").Value)
                    t8 = IIf(IsDBNull(d.Cells("Taux Tva").Value), 0, d.Cells("Taux Tva").Value)
                    t9 = IIf(IsDBNull(d.Cells("Montant Tva").Value), 0, d.Cells("Montant Tva").Value)
                    t10 = IIf(IsDBNull(d.Cells("Mode règlement").Value), "", d.Cells("Mode règlement").Value)
                    t11 = IIf(IsDBNull(d.Cells("N° règlement").Value), "", d.Cells("N° règlement").Value)
                    t12 = IIf(IsDBNull(d.Cells("Désignation").Value), "", d.Cells("Désignation").Value)
                    t13 = IIf(IsDBNull(d.Cells("Date paiement").Value), "", d.Cells("Date paiement").Value)
                    t14 = IIf(IsDBNull(d.Cells("ICE").Value), "", d.Cells("ICE").Value)
                    t15 = IIf(IsDBNull(d.Cells("Prorata").Value), "0", d.Cells("Prorata").Value)
                    t16 = IIf(IsDBNull(d.Cells("Réf Nature Opération").Value), "", d.Cells("Réf Nature Opération").Value)
                    Dim t16Value As Object = DBNull.Value
                    If Not IsDBNull(d.Cells("Réf Nature Opération").Value) AndAlso d.Cells("Réf Nature Opération").Value.ToString().Trim() <> "" Then
                        t16Value = d.Cells("Réf Nature Opération").Value.ToString()
                    End If
                    Dim t17Value As Object = DBNull.Value
                    If Not IsDBNull(d.Cells("Taux Retenue Source").Value) AndAlso d.Cells("Taux Retenue Source").Value.ToString().Trim() <> "" Then
                        t17Value = d.Cells("Taux Retenue Source").Value
                    End If
                    Dim t18Value As Object = DBNull.Value
                    If Not IsDBNull(d.Cells("Réf Type Client").Value) AndAlso d.Cells("Réf Type Client").Value.ToString().Trim() <> "" Then
                        t18Value = d.Cells("Réf Type Client").Value.ToString().Trim()
                    End If

                    ' Règle : RefNatOpt et TauxRetenuSource doivent être soit tous les deux vides, soit tous les deux remplis
                    Dim refnatoptRempliV As Boolean = Not (t16Value Is DBNull.Value)
                    Dim tauxRempliV As Boolean = Not (t17Value Is DBNull.Value)
                    If refnatoptRempliV <> tauxRempliV Then
                        MessageBox.Show("Ligne " & (i + 1) & " (N° facture " & t3 & ") : Réf Nature Opération et Taux Retenue Source doivent être soit tous les deux vides, soit tous les deux remplis." & vbNewLine & "La modification a été annulée, l'ancienne valeur a été conservée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Dim nligneKey As Integer = 0
                        Integer.TryParse(t2, nligneKey)
                        If oldValuesDict.ContainsKey(nligneKey) Then
                            t16Value = oldValuesDict(nligneKey)(0)
                            t17Value = oldValuesDict(nligneKey)(1)
                        Else
                            t16Value = DBNull.Value
                            t17Value = DBNull.Value
                        End If
                    End If

                    ' Côté Client : Réf Nature Opération, Taux Retenue Source et Réf Type Client sont TOUJOURS
                    ' obligatoires et doivent respecter les valeurs autorisées du CDC
                    If modeConnexion.ToLower() = "client" Then
                        Dim clientLigneErreur As String = ""
                        If t16Value Is DBNull.Value Then
                            clientLigneErreur &= "Réf Nature Opération doit être renseigné (4, 5 ou 6). "
                        ElseIf Not New String() {"4", "5", "6"}.Contains(t16Value.ToString().Trim()) Then
                            clientLigneErreur &= "Réf Nature Opération (" & t16Value.ToString() & ") invalide - valeurs possibles : 4/5/6. "
                        End If
                        If t17Value Is DBNull.Value Then
                            clientLigneErreur &= "Taux Retenue Source doit être renseigné (100 ou 75). "
                        ElseIf Not New String() {"100", "75"}.Contains(t17Value.ToString().Trim()) Then
                            clientLigneErreur &= "Taux Retenue Source (" & t17Value.ToString() & ") invalide - valeurs possibles : 100/75. "
                        End If
                        If t18Value Is DBNull.Value Then
                            clientLigneErreur &= "Réf Type Client doit être renseigné (1 à 5). "
                        ElseIf Not New String() {"1", "2", "3", "4", "5"}.Contains(t18Value.ToString().Trim()) Then
                            clientLigneErreur &= "Réf Type Client (" & t18Value.ToString() & ") invalide - valeurs possibles : 1/2/3/4/5. "
                        End If

                        If clientLigneErreur <> "" Then
                            MessageBox.Show("Ligne " & (i + 1) & " (N° facture " & t3 & ") : " & clientLigneErreur & vbNewLine & "La modification a été annulée, l'ancienne valeur a été conservée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Dim nligneKeyC As Integer = 0
                            Integer.TryParse(t2, nligneKeyC)
                            If oldValuesDict.ContainsKey(nligneKeyC) Then
                                t16Value = oldValuesDict(nligneKeyC)(0)
                                t17Value = oldValuesDict(nligneKeyC)(1)
                                t18Value = oldValuesDict(nligneKeyC)(2)
                            Else
                                t16Value = DBNull.Value
                                t17Value = DBNull.Value
                                t18Value = DBNull.Value
                            End If
                        End If
                    ElseIf refnatoptRempliV AndAlso tauxRempliV Then
                        ' Côté Fournisseur : optionnels, mais s'ils sont remplis, doivent respecter les valeurs autorisées
                        Dim fournLigneErreur As String = ""
                        If Not New String() {"1", "2", "3"}.Contains(t16Value.ToString().Trim()) Then
                            fournLigneErreur &= "Réf Nature Opération (" & t16Value.ToString() & ") invalide - valeurs possibles : 1/2/3. "
                        End If
                        If Not New String() {"100", "75"}.Contains(t17Value.ToString().Trim()) Then
                            fournLigneErreur &= "Taux Retenue Source (" & t17Value.ToString() & ") invalide - valeurs possibles : 100/75. "
                        End If
                        If fournLigneErreur <> "" Then
                            MessageBox.Show("Ligne " & (i + 1) & " (N° facture " & t3 & ") : " & fournLigneErreur & vbNewLine & "La modification a été annulée, l'ancienne valeur a été conservée.", "Simpl-TVA", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Dim nligneKeyF As Integer = 0
                            Integer.TryParse(t2, nligneKeyF)
                            If oldValuesDict.ContainsKey(nligneKeyF) Then
                                t16Value = oldValuesDict(nligneKeyF)(0)
                                t17Value = oldValuesDict(nligneKeyF)(1)
                            Else
                                t16Value = DBNull.Value
                                t17Value = DBNull.Value
                            End If
                        End If
                    End If

                    Dim com1 As New SqlCommand
                    com1.Connection = cnx

                    If t4 = "" Then
                        com1.Parameters.Add(New SqlParameter _
                                           With {.ParameterName = "@p4",
                                                 .SqlDbType = SqlDbType.DateTime,
                                                 .Value = DBNull.Value})
                    Else
                        com1.Parameters.Add(New SqlParameter _
                                           With {.ParameterName = "@p4",
                                                 .SqlDbType = SqlDbType.DateTime,
                                                 .Value = t4})
                    End If

                    If t13 = "" Then
                        com1.Parameters.Add(New SqlParameter _
                                           With {.ParameterName = "@p13",
                                                 .SqlDbType = SqlDbType.DateTime,
                                                 .Value = DBNull.Value})
                    Else
                        com1.Parameters.Add(New SqlParameter _
                                           With {.ParameterName = "@p13",
                                                 .SqlDbType = SqlDbType.DateTime,
                                                 .Value = t13})
                    End If

                    com1.CommandText = "INSERT INTO [LigneGeneration] " &
                        "([Idgeneration], [Nligne], [Numfacture], [Datefacture], [Identifiantfiscale], [Fournisseur], [MontantHT], [TauxTVA], [MontantTVA], [Modereglement], [Numreglement], [Designation], [Datepaiement], [ice], [prorata], [RefNatOpt], [TauxRetenuSource], [RefTypClt]) " &
                        "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17,@p18)"

                    With com1.Parameters
                        .Add("@p1", SqlDbType.Int).Value = t1
                        .Add("@p2", SqlDbType.Int).Value = t2
                        .Add("@p3", SqlDbType.VarChar, 255).Value = t3
                        '.AddWithValue("@p4", t5)
                        .Add("@p5", SqlDbType.VarChar, 255).Value = t5
                        .Add("@p6", SqlDbType.VarChar, 255).Value = t6
                        .Add("@p7", SqlDbType.Float).Value = t7
                        .Add("@p8", SqlDbType.Float).Value = t8
                        .Add("@p9", SqlDbType.Float).Value = t9
                        .Add("@p10", SqlDbType.VarChar, 255).Value = t10
                        .Add("@p11", SqlDbType.VarChar, 255).Value = t11
                        .Add("@p12", SqlDbType.VarChar, 255).Value = t12
                        .Add("@p14", SqlDbType.VarChar, 255).Value = t14
                        .Add("@p15", SqlDbType.Float).Value = t15
                        .Add("@p16", SqlDbType.VarChar, 50).Value = t16Value
                        .Add("@p17", SqlDbType.Float).Value = t17Value
                        .Add("@p18", SqlDbType.VarChar, 10).Value = t18Value
                    End With
                    com1.ExecuteNonQuery()
                Next
                MsgBox("Modification avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                Dim libelle As String = ComboBox2.Text
                anomalie()
                remplircombo()
                ComboBox2.Text = libelle
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Function GetCodPai(str As String) As Integer
        Select Case str
            Case "ES"
                Return 1
            Case "CH"
                Return 2
            Case "PR"
                Return 3
            Case "VI"
                Return 4
            Case "EF"
                Return 5
            Case "CO"
                Return 6
            Case "AU"
                Return 7
            Case Else
                Return 7
        End Select
    End Function

    Private Sub CmdConfirmer_Click(sender As Object, e As EventArgs) Handles CmdConfirmer.Click
        If modeConnexion.ToLower() = "client" Then
            GenererClient()
        ElseIf modeConnexion.ToLower() = "fournisseurnonresident" Then
            GenererNonResident()
        ElseIf ComboBoxTypeExport.SelectedItem IsNot Nothing AndAlso ComboBoxTypeExport.SelectedItem.ToString() = "TVA RAS" Then
            GenererTVARAS()
        Else
            GenererTVA()
        End If
    End Sub

    Sub GenererTVA()
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox1.Text = "") Then
                MsgBox("Vous devez choisir la société !!! ", MsgBoxStyle.Information, "Simpl-TVA")
            Else
                If (ComboBox3.Text = "") Then
                    MsgBox("Vous devez choisir la période !!! ", MsgBoxStyle.Information, "Simpl-TVA")
                Else

                    Dim parametersFF() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox3.Text},
                        New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                        New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                    }
                    Dim vvvSql = "Update EnteteGeneration set statut=3 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3"
                    Execute(vvvSql, parametersFF)

                    Dim ssql As String = "SELECT [regime], [IF] FROM Societe INNER JOIN " &
                        "EnteteGeneration ON Societe.Idsociete = EnteteGeneration.IdSociete " &
                        "WHERE EnteteGeneration.Libellegeneration = @p1 and EnteteGeneration.[IdSociete]= @p2 and EnteteGeneration.ModeConnexion=@p3"

                    Dim regime, identifiantfiscale As String
                    identifiantfiscale = Read(ssql, parametersFF).Rows(0)("IF").ToString()
                    regime = Read(ssql, parametersFF).Rows(0)("regime").ToString()
                    If regime <> "" Then
                        regime = IIf(regime.ToLower().Equals("mensuel"), "1", "2")
                    Else
                        regime = "1"
                    End If

                    Dim baliseouvrir As String = "" &
                        "<DeclarationReleveDeduction>" &
                      nl & "    <identifiantFiscal>" & identifiantfiscale & "</identifiantFiscal>" &
                      nl & "    <annee>" & ComboBox3.Text.Substring(3, 4) & "</annee>" &
                      nl & "    <periode>" & Integer.Parse(ComboBox3.Text.Substring(0, 2)).ToString() & "</periode>" &
                      nl & "    <regime>" & regime & "</regime>"
                    Dim com2 As New SqlCommand
                    com2.Connection = cnx
                    com2.CommandText = "SELECT " &
                        "LigneGeneration.Numfacture" &
                        ", LigneGeneration.Designation" &
                        ", LigneGeneration.MontantHT" &
                        ", LigneGeneration.MontantTVA" &
                        ", (LigneGeneration.MontantHT+LigneGeneration.MontantTVA) AS MontantTTC" &
                        ", LigneGeneration.Identifiantfiscale" &
                        ", LigneGeneration.Fournisseur" &
                        ", LigneGeneration.TauxTVA" &
                        ", LigneGeneration.Modereglement" &
                        ", LigneGeneration.Datepaiement" &
                        ", LigneGeneration.Datefacture" &
                        ", LigneGeneration.ice " &
                        ", LigneGeneration.prorata " &
                        "   FROM EnteteGeneration INNER JOIN LigneGeneration " &
                        "       ON EnteteGeneration.Idgeneration = LigneGeneration.Idgeneration " &
                        "   WHERE EnteteGeneration.Libellegeneration = @p1 AND EnteteGeneration.[IdSociete]=@p2 AND EnteteGeneration.ModeConnexion=@p3 " &
                        "       AND (LigneGeneration.RefNatOpt IS NULL OR LigneGeneration.TauxRetenuSource IS NULL)"
                    With com2.Parameters
                        .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox3.Text
                        .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                        .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                    End With
                    Dim dr As SqlDataReader
                    dr = com2.ExecuteReader

                    If Not dr.HasRows Then
                        dr.Close()
                        MsgBox("Aucune ligne trouvée pour cette société et cette période. Vérifie l'importation, la société et la période sélectionnée.", MsgBoxStyle.Exclamation, "Simpl-TVA")
                        Exit Sub
                    End If

                    Dim i As Integer = 1
                    Dim baliserelevedeductionsouvrer As String = "<releveDeductions>"
                    Dim baliserelevedeductionsfermer As String = "</releveDeductions>"
                    Dim contenu As String = baliseouvrir & nl & baliserelevedeductionsouvrer & nl
                    Dim mdP As String = ""

                    While dr.Read()
                        Dim mode As Integer
                        mdP = dr(8).ToString().ToUpper()
                        If mdP.Length > 2 Then
                            mdP = mdP.Substring(0, 2)
                        End If
                        mode = GetCodPai(mdP)

                        Dim datefacture, datepaiement As String
                        If (dr(9).ToString = "NULL" Or IsDBNull(dr(9))) Then
                            datepaiement = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr(9)).Year
                            mois = IIf(Convert.ToDateTime(dr(9)).Month < 10, "0" & Convert.ToDateTime(dr(9)).Month, Convert.ToDateTime(dr(9)).Month)
                            jour = IIf(Convert.ToDateTime(dr(9)).Day < 10, "0" & Convert.ToDateTime(dr(9)).Day, Convert.ToDateTime(dr(9)).Day)
                            datepaiement = anne & "-" & mois & "-" & jour
                        End If
                        If (dr(10).ToString = "NULL" Or IsDBNull(dr(10))) Then
                            datefacture = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr(10)).Year
                            mois = IIf(Convert.ToDateTime(dr(10)).Month < 10, "0" & Convert.ToDateTime(dr(10)).Month, Convert.ToDateTime(dr(10)).Month)
                            jour = IIf(Convert.ToDateTime(dr(10)).Day < 10, "0" & Convert.ToDateTime(dr(10)).Day, Convert.ToDateTime(dr(10)).Day)
                            datefacture = anne & "-" & mois & "-" & jour
                        End If

                        Dim balisecontenu As String = "" &
                            nl & "   <rd>" &
                            nl & "      <ord>" & i & "</ord>" &
                            nl & "      <num>" & dr(0).ToString & "</num>" &
                            nl & "      <des>" & EscapeXMLchar(dr(1).ToString) & "</des>" &
                            nl & "      <mht>" & IIf(IsDBNull(dr(2)), 0, dr(2).ToString.Replace(",", ".")) & "</mht>" &
                            nl & "      <tva>" & IIf(IsDBNull(dr(3)), 0, dr(3).ToString.Replace(",", ".")) & "</tva>" &
                            nl & "      <ttc>" & IIf(IsDBNull(dr(4)), 0, dr(4).ToString.Replace(",", ".")) & "</ttc>" &
                            nl & "      <refF>" &
                            nl & "          <if>" & IIf(IsDBNull(dr(5)), "", dr(5).ToString) & "</if>" &
                            nl & "          <nom>" & EscapeXMLchar(dr(6).ToString) & "</nom>" &
                            nl & "          <ice>" & EscapeXMLchar(dr(11).ToString) & "</ice>" &
                            nl & "      </refF>" &
                            nl & "      <tx>" & IIf(IsDBNull(dr(7)), 0, dr(7).ToString) & "</tx>" &
                            nl & "      <prorata>" & dr("prorata").ToString().Replace(",", ".") & "</prorata>" &
                            nl & "      <mp>" &
                            nl & "          <id>" & mode & "</id>" &
                            nl & "      </mp>" &
                            nl & "      <dpai>" & datepaiement & "</dpai>" &
                            nl & "      <dfac>" & datefacture & "</dfac>" &
                        nl & "   </rd>"
                        i = i + 1
                        contenu = contenu & balisecontenu & nl

                    End While
                    dr.Close()
                    contenu = contenu & nl & baliserelevedeductionsfermer & nl & "</DeclarationReleveDeduction> "
                    Dim sw As New IO.StreamWriter(TextBox3.Text)
                    sw.WriteLine(contenu)
                    sw.Close()
                    If CheckBoxZip.Checked Then
                        Using zip As ZipFile = New ZipFile()
                            ' add this map file into the "images" directory in the zip archive
                            zip.AddFile(TextBox3.Text, "")
                            zip.Save(TextBox3.Text.Replace("xml", "zip"))
                        End Using
                    End If
                    MsgBox("Génération avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    Process.Start("explorer.exe", String.Format("/select,{0}", IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), TextBox3.Text)))
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub GenererTVARAS()
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox1.Text = "") Then
                MsgBox("Vous devez choisir la société !!! ", MsgBoxStyle.Information, "Simpl-TVA")
            Else
                If (ComboBox3.Text = "") Then
                    MsgBox("Vous devez choisir la période !!! ", MsgBoxStyle.Information, "Simpl-TVA")
                Else

                    Dim parametersFF() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox3.Text},
                        New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                        New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                    }
                    Dim vvvSql = "Update EnteteGeneration set statut=3 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3"
                    Execute(vvvSql, parametersFF)

                    Dim ssql As String = "SELECT [regime], [IF] FROM Societe INNER JOIN " &
                        "EnteteGeneration ON Societe.Idsociete = EnteteGeneration.IdSociete " &
                        "WHERE EnteteGeneration.Libellegeneration = @p1 and EnteteGeneration.[IdSociete]= @p2 and EnteteGeneration.ModeConnexion=@p3"

                    Dim regime, identifiantfiscale As String
                    identifiantfiscale = Read(ssql, parametersFF).Rows(0)("IF").ToString()
                    regime = Read(ssql, parametersFF).Rows(0)("regime").ToString()
                    If regime <> "" Then
                        regime = IIf(regime.ToLower().Equals("mensuel"), "1", "2")
                    Else
                        regime = "1"
                    End If

                    Dim baliseouvrir As String = "" &
                        "<VersementRetenueSources>" &
                      nl & "    <identifiantFiscal>" & identifiantfiscale & "</identifiantFiscal>" &
                      nl & "    <annee>" & ComboBox3.Text.Substring(3, 4) & "</annee>" &
                      nl & "    <periode>" & Integer.Parse(ComboBox3.Text.Substring(0, 2)).ToString() & "</periode>" &
                      nl & "    <regime>" & regime & "</regime>"

                    Dim com2 As New SqlCommand
                    com2.Connection = cnx
                    com2.CommandText = "SELECT " &
                        "LigneGeneration.Identifiantfiscale" &
                        ", LigneGeneration.Numfacture" &
                        ", LigneGeneration.Datepaiement" &
                        ", LigneGeneration.Datefacture" &
                        ", LigneGeneration.RefNatOpt" &
                        ", LigneGeneration.MontantHT" &
                        ", LigneGeneration.TauxTVA" &
                        ", LigneGeneration.TauxRetenuSource " &
                        "   FROM EnteteGeneration INNER JOIN LigneGeneration " &
                        "       ON EnteteGeneration.Idgeneration = LigneGeneration.Idgeneration " &
                        "   WHERE EnteteGeneration.Libellegeneration = @p1 AND EnteteGeneration.[IdSociete]=@p2 AND EnteteGeneration.ModeConnexion=@p3 " &
                        "       AND LigneGeneration.RefNatOpt IS NOT NULL AND LigneGeneration.TauxRetenuSource IS NOT NULL"
                    With com2.Parameters
                        .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox3.Text
                        .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                        .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                    End With
                    Dim dr As SqlDataReader
                    dr = com2.ExecuteReader

                    If Not dr.HasRows Then
                        dr.Close()
                        MsgBox("Aucune ligne trouvée pour cette société et cette période. Vérifie l'importation, la société et la période sélectionnée.", MsgBoxStyle.Exclamation, "Simpl-TVA")
                        Exit Sub
                    End If

                    Dim baliseFournisseursOuvrer As String = "<fournisseurs>"
                    Dim baliseFournisseursFermer As String = "</fournisseurs>"
                    Dim contenu As String = baliseouvrir & nl & baliseFournisseursOuvrer & nl

                    While dr.Read()
                        Dim datefacture, datepaiement As String
                        If (dr("Datepaiement").ToString = "NULL" Or IsDBNull(dr("Datepaiement"))) Then
                            datepaiement = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr("Datepaiement")).Year
                            mois = IIf(Convert.ToDateTime(dr("Datepaiement")).Month < 10, "0" & Convert.ToDateTime(dr("Datepaiement")).Month, Convert.ToDateTime(dr("Datepaiement")).Month)
                            jour = IIf(Convert.ToDateTime(dr("Datepaiement")).Day < 10, "0" & Convert.ToDateTime(dr("Datepaiement")).Day, Convert.ToDateTime(dr("Datepaiement")).Day)
                            datepaiement = anne & "-" & mois & "-" & jour
                        End If
                        If (dr("Datefacture").ToString = "NULL" Or IsDBNull(dr("Datefacture"))) Then
                            datefacture = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr("Datefacture")).Year
                            mois = IIf(Convert.ToDateTime(dr("Datefacture")).Month < 10, "0" & Convert.ToDateTime(dr("Datefacture")).Month, Convert.ToDateTime(dr("Datefacture")).Month)
                            jour = IIf(Convert.ToDateTime(dr("Datefacture")).Day < 10, "0" & Convert.ToDateTime(dr("Datefacture")).Day, Convert.ToDateTime(dr("Datefacture")).Day)
                            datefacture = anne & "-" & mois & "-" & jour
                        End If

                        Dim balisecontenu As String = "" &
                            nl & "   <fournisseur>" &
                            nl & "      <ifuFournisseur>" & IIf(IsDBNull(dr("Identifiantfiscale")), "", dr("Identifiantfiscale").ToString) & "</ifuFournisseur>" &
                            nl & "      <numFacture>" & EscapeXMLchar(dr("Numfacture").ToString) & "</numFacture>" &
                            nl & "      <datePaiement>" & datepaiement & "</datePaiement>" &
                            nl & "      <dateOperation>" & datefacture & "</dateOperation>" &
                            nl & "      <refNatOpt>" & IIf(IsDBNull(dr("RefNatOpt")), "", dr("RefNatOpt").ToString) & "</refNatOpt>" &
                            nl & "      <montantHT>" & IIf(IsDBNull(dr("MontantHT")), 0, dr("MontantHT").ToString.Replace(",", ".")) & "</montantHT>" &
                            nl & "      <tauxTva>" & IIf(IsDBNull(dr("TauxTVA")), 0, dr("TauxTVA").ToString) & "</tauxTva>" &
                            nl & "      <tauxRetenuSource>" & IIf(IsDBNull(dr("TauxRetenuSource")), 0, dr("TauxRetenuSource").ToString.Replace(",", ".")) & "</tauxRetenuSource>" &
                        nl & "   </fournisseur>"
                        contenu = contenu & balisecontenu & nl

                    End While
                    dr.Close()
                    contenu = contenu & nl & baliseFournisseursFermer & nl & "</VersementRetenueSources> "
                    Dim sw As New IO.StreamWriter(TextBox3.Text)
                    sw.WriteLine(contenu)
                    sw.Close()
                    If CheckBoxZip.Checked Then
                        Using zip As ZipFile = New ZipFile()
                            zip.AddFile(TextBox3.Text, "")
                            zip.Save(TextBox3.Text.Replace("xml", "zip"))
                        End Using
                    End If
                    MsgBox("Génération avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    Process.Start("explorer.exe", String.Format("/select,{0}", IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), TextBox3.Text)))
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub GenererClient()
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox1.Text = "") Then
                MsgBox("Vous devez choisir la société !!! ", MsgBoxStyle.Information, "Simpl-TVA")
            Else
                If (ComboBox3.Text = "") Then
                    MsgBox("Vous devez choisir la période !!! ", MsgBoxStyle.Information, "Simpl-TVA")
                Else

                    Dim parametersFF() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox3.Text},
                        New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                        New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                    }
                    Dim vvvSql = "Update EnteteGeneration set statut=3 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3"
                    Execute(vvvSql, parametersFF)

                    Dim ssql As String = "SELECT [regime], [IF] FROM Societe INNER JOIN " &
                        "EnteteGeneration ON Societe.Idsociete = EnteteGeneration.IdSociete " &
                        "WHERE EnteteGeneration.Libellegeneration = @p1 and EnteteGeneration.[IdSociete]= @p2 and EnteteGeneration.ModeConnexion=@p3"

                    Dim regime, identifiantfiscale As String
                    identifiantfiscale = Read(ssql, parametersFF).Rows(0)("IF").ToString()
                    regime = Read(ssql, parametersFF).Rows(0)("regime").ToString()
                    If regime <> "" Then
                        regime = IIf(regime.ToLower().Equals("mensuel"), "1", "2")
                    Else
                        regime = "1"
                    End If

                    Dim baliseouvrir As String = "" &
                        "<DeclarationReleveRetenueSource>" &
                      nl & "    <identifiantFiscal>" & identifiantfiscale & "</identifiantFiscal>" &
                      nl & "    <annee>" & ComboBox3.Text.Substring(3, 4) & "</annee>" &
                      nl & "    <periode>" & Integer.Parse(ComboBox3.Text.Substring(0, 2)).ToString() & "</periode>" &
                      nl & "    <regime>" & regime & "</regime>"

                    Dim com2 As New SqlCommand
                    com2.Connection = cnx
                    com2.CommandText = "SELECT " &
                        "LigneGeneration.Identifiantfiscale" &
                        ", LigneGeneration.Fournisseur" &
                        ", LigneGeneration.ice" &
                        ", LigneGeneration.Numfacture" &
                        ", LigneGeneration.Datepaiement" &
                        ", LigneGeneration.Datefacture" &
                        ", LigneGeneration.RefNatOpt" &
                        ", LigneGeneration.RefTypClt" &
                        ", LigneGeneration.MontantHT" &
                        ", LigneGeneration.TauxTVA" &
                        ", LigneGeneration.TauxRetenuSource " &
                        "   FROM EnteteGeneration INNER JOIN LigneGeneration " &
                        "       ON EnteteGeneration.Idgeneration = LigneGeneration.Idgeneration " &
                        "   WHERE EnteteGeneration.Libellegeneration = @p1 AND EnteteGeneration.[IdSociete]=@p2 AND EnteteGeneration.ModeConnexion=@p3 " &
                        "       AND LigneGeneration.RefNatOpt IS NOT NULL AND LigneGeneration.TauxRetenuSource IS NOT NULL"
                    With com2.Parameters
                        .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox3.Text
                        .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                        .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                    End With
                    Dim dr As SqlDataReader
                    dr = com2.ExecuteReader

                    If Not dr.HasRows Then
                        dr.Close()
                        MsgBox("Aucune ligne trouvée pour cette société et cette période. Vérifie l'importation, la société et la période sélectionnée.", MsgBoxStyle.Exclamation, "Simpl-TVA")
                        Exit Sub
                    End If

                    Dim baliseClientsOuvrer As String = "    <releveRetenuSources>"
                    Dim baliseClientsFermer As String = "    </releveRetenuSources>"
                    Dim contenu As String = baliseouvrir & nl & baliseClientsOuvrer & nl

                    While dr.Read()
                        Dim datefacture, datepaiement As String
                        If (dr("Datepaiement").ToString = "NULL" Or IsDBNull(dr("Datepaiement"))) Then
                            datepaiement = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr("Datepaiement")).Year
                            mois = IIf(Convert.ToDateTime(dr("Datepaiement")).Month < 10, "0" & Convert.ToDateTime(dr("Datepaiement")).Month, Convert.ToDateTime(dr("Datepaiement")).Month)
                            jour = IIf(Convert.ToDateTime(dr("Datepaiement")).Day < 10, "0" & Convert.ToDateTime(dr("Datepaiement")).Day, Convert.ToDateTime(dr("Datepaiement")).Day)
                            datepaiement = anne & "-" & mois & "-" & jour
                        End If
                        If (dr("Datefacture").ToString = "NULL" Or IsDBNull(dr("Datefacture"))) Then
                            datefacture = ""
                        Else
                            Dim anne, mois, jour As String
                            anne = Convert.ToDateTime(dr("Datefacture")).Year
                            mois = IIf(Convert.ToDateTime(dr("Datefacture")).Month < 10, "0" & Convert.ToDateTime(dr("Datefacture")).Month, Convert.ToDateTime(dr("Datefacture")).Month)
                            jour = IIf(Convert.ToDateTime(dr("Datefacture")).Day < 10, "0" & Convert.ToDateTime(dr("Datefacture")).Day, Convert.ToDateTime(dr("Datefacture")).Day)
                            datefacture = anne & "-" & mois & "-" & jour
                        End If

                        Dim balisecontenu As String = "" &
                            nl & "        <rrs>" &
                            nl & "            <refTypClt>" & IIf(IsDBNull(dr("RefTypClt")), "", dr("RefTypClt").ToString) & "</refTypClt>" &
                            nl & "            <refNatOpt>" & IIf(IsDBNull(dr("RefNatOpt")), "", dr("RefNatOpt").ToString) & "</refNatOpt>" &
                            nl & "            <numFacture>" & EscapeXMLchar(dr("Numfacture").ToString) & "</numFacture>" &
                            nl & "            <dateOperation>" & datefacture & "</dateOperation>" &
                            nl & "            <datePaiement>" & datepaiement & "</datePaiement>" &
                            nl & "            <montantHT>" & IIf(IsDBNull(dr("MontantHT")), 0, dr("MontantHT").ToString.Replace(",", ".")) & "</montantHT>" &
                            nl & "            <tauxTva>" & IIf(IsDBNull(dr("TauxTVA")), 0, dr("TauxTVA").ToString) & "</tauxTva>" &
                            nl & "            <tauxRetenu>" & IIf(IsDBNull(dr("TauxRetenuSource")), 0, dr("TauxRetenuSource").ToString.Replace(",", ".")) & "</tauxRetenu>" &
                            nl & "            <refF>" &
                            nl & "                <if>" & IIf(IsDBNull(dr("Identifiantfiscale")), "", dr("Identifiantfiscale").ToString) & "</if>" &
                            nl & "                <nom>" & IIf(IsDBNull(dr("Fournisseur")), "", EscapeXMLchar(dr("Fournisseur").ToString)) & "</nom>" &
                            nl & "                <ice>" & IIf(IsDBNull(dr("ice")), "", dr("ice").ToString) & "</ice>" &
                            nl & "            </refF>" &
                        nl & "        </rrs>"
                        contenu = contenu & balisecontenu & nl

                    End While
                    dr.Close()
                    contenu = contenu & baliseClientsFermer & nl & "</DeclarationReleveRetenueSource> "
                    Dim sw As New IO.StreamWriter(TextBox3.Text)
                    sw.WriteLine(contenu)
                    sw.Close()
                    If CheckBoxZip.Checked Then
                        Using zip As ZipFile = New ZipFile()
                            zip.AddFile(TextBox3.Text, "")
                            zip.Save(TextBox3.Text.Replace("xml", "zip"))
                        End Using
                    End If
                    MsgBox("Génération avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    Process.Start("explorer.exe", String.Format("/select,{0}", IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), TextBox3.Text)))
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ' Génération XML mode "Fournisseur Non-Résident" (Annexe 3 EDI - liste des contribuables non-résidents)
    Sub GenererNonResident()
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox1.Text = "") Then
                MsgBox("Vous devez choisir la société !!! ", MsgBoxStyle.Information, "Simpl-TVA")
            Else
                If (ComboBox3.Text = "") Then
                    MsgBox("Vous devez choisir la période !!! ", MsgBoxStyle.Information, "Simpl-TVA")
                Else
                    Dim parametersFF() As SqlParameter = New SqlParameter() _
                    {
                        New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox3.Text},
                        New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                        New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                    }
                    Dim vvvSql = "Update EnteteGeneration set statut=3 where Libellegeneration=@p1 and [IdSociete]=@p2 and ModeConnexion=@p3"
                    Execute(vvvSql, parametersFF)

                    Dim ssql As String = "SELECT [regime], [IF] FROM Societe INNER JOIN " &
                        "EnteteGeneration ON Societe.Idsociete = EnteteGeneration.IdSociete " &
                        "WHERE EnteteGeneration.Libellegeneration = @p1 and EnteteGeneration.[IdSociete]= @p2 and EnteteGeneration.ModeConnexion=@p3"

                    Dim regime, identifiantfiscale As String
                    identifiantfiscale = Read(ssql, parametersFF).Rows(0)("IF").ToString()
                    regime = Read(ssql, parametersFF).Rows(0)("regime").ToString()
                    If regime <> "" Then
                        regime = IIf(regime.ToLower().Equals("mensuel"), "1", "2")
                    Else
                        regime = "1"
                    End If

                    Dim baliseouvrir As String = "" &
                        "<declarationNonResidents>" &
                      nl & "    <identifiantFiscal>" & identifiantfiscale & "</identifiantFiscal>" &
                      nl & "    <annee>" & ComboBox3.Text.Substring(3, 4) & "</annee>" &
                      nl & "    <periode>" & Integer.Parse(ComboBox3.Text.Substring(0, 2)).ToString() & "</periode>" &
                      nl & "    <regime>" & regime & "</regime>"

                    Dim com2 As New SqlCommand
                    com2.Connection = cnx
                    com2.CommandText = "SELECT " &
                        "LigneNonResident.NomPrenomOuRaisonSociale" &
                        ", LigneNonResident.AdresseEtranger" &
                        ", LigneNonResident.NIdentificationFiscale" &
                        ", LigneNonResident.NatureOperation" &
                        ", LigneNonResident.DatePaiement" &
                        ", LigneNonResident.BaseImposableHT" &
                        ", LigneNonResident.Taux" &
                        ", LigneNonResident.TvaExigible" &
                        ", LigneNonResident.MontantTva" &
                        ", LigneNonResident.EstRedevance " &
                        "   FROM EnteteGeneration INNER JOIN LigneNonResident " &
                        "       ON EnteteGeneration.Idgeneration = LigneNonResident.Idgeneration " &
                        "   WHERE EnteteGeneration.Libellegeneration = @p1 AND EnteteGeneration.[IdSociete]=@p2 AND EnteteGeneration.ModeConnexion=@p3"
                    With com2.Parameters
                        .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox3.Text
                        .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                        .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                    End With
                    Dim dr As SqlDataReader
                    dr = com2.ExecuteReader

                    If Not dr.HasRows Then
                        dr.Close()
                        MsgBox("Aucune ligne trouvée pour cette société et cette période. Vérifie l'importation, la société et la période sélectionnée.", MsgBoxStyle.Exclamation, "Simpl-TVA")
                        Exit Sub
                    End If

                    Dim t As New DataTable()
                    t.Load(dr)
                    dr.Close()

                    ' Section 1 : opérations normales (EstRedevance = 0)
                    Dim contenuNonResidents As String = ""
                    ' Section 2 : redevances et droits de licence (EstRedevance = 1)
                    Dim contenuRedevances As String = ""

                    For Each row As DataRow In t.Rows
                        Dim datepaiement As String = ""
                        If Not IsDBNull(row("DatePaiement")) Then
                            Dim dt As DateTime = Convert.ToDateTime(row("DatePaiement"))
                            datepaiement = dt.ToString("yyyy-MM-dd")
                        End If

                        Dim nom As String = EscapeXMLchar(IIf(IsDBNull(row("NomPrenomOuRaisonSociale")), "", row("NomPrenomOuRaisonSociale").ToString))
                        Dim adresse As String = EscapeXMLchar(IIf(IsDBNull(row("AdresseEtranger")), "", row("AdresseEtranger").ToString))
                        Dim nif As String = EscapeXMLchar(IIf(IsDBNull(row("NIdentificationFiscale")), "", row("NIdentificationFiscale").ToString))
                        Dim baseHT As String = IIf(IsDBNull(row("BaseImposableHT")), 0, row("BaseImposableHT").ToString.Replace(",", "."))
                        Dim taux As String = IIf(IsDBNull(row("Taux")), 0, row("Taux").ToString.Replace(",", "."))

                        Dim estRedevance As Boolean = (Not IsDBNull(row("EstRedevance")) AndAlso Convert.ToInt32(row("EstRedevance")) = 1)

                        If estRedevance Then
                            Dim montantTva As String = IIf(IsDBNull(row("MontantTva")), 0, row("MontantTva").ToString.Replace(",", "."))
                            Dim tvaExigibleR As String = IIf(IsDBNull(row("TvaExigible")), 0, row("TvaExigible").ToString.Replace(",", "."))
                            contenuRedevances = contenuRedevances &
                                nl & "      <nonResidentsRedevance>" &
                                nl & "          <nomPrenomOuRaisonSociale>" & nom & "</nomPrenomOuRaisonSociale>" &
                                nl & "          <adresseEtranger>" & adresse & "</adresseEtranger>" &
                                nl & "          <nIdentificationFiscale>" & nif & "</nIdentificationFiscale>" &
                                nl & "          <datePaiement>" & datepaiement & "</datePaiement>" &
                                nl & "          <montantTva>" & montantTva & "</montantTva>" &
                                nl & "          <baseImposableHT>" & baseHT & "</baseImposableHT>" &
                                nl & "          <taux>" & taux & "</taux>" &
                                nl & "          <tvaExigible>" & tvaExigibleR & "</tvaExigible>" &
                                nl & "      </nonResidentsRedevance>"
                        Else
                            Dim natureOp As String = EscapeXMLchar(IIf(IsDBNull(row("NatureOperation")), "", row("NatureOperation").ToString))
                            Dim tvaExigible As String = IIf(IsDBNull(row("TvaExigible")), 0, row("TvaExigible").ToString.Replace(",", "."))
                            contenuNonResidents = contenuNonResidents &
                                nl & "      <nonResident>" &
                                nl & "          <nomPrenomOuRaisonSociale>" & nom & "</nomPrenomOuRaisonSociale>" &
                                nl & "          <adresseEtranger>" & adresse & "</adresseEtranger>" &
                                nl & "          <nIdentificationFiscale>" & nif & "</nIdentificationFiscale>" &
                                nl & "          <natureOperation>" & natureOp & "</natureOperation>" &
                                nl & "          <datePaiement>" & datepaiement & "</datePaiement>" &
                                nl & "          <baseImposableHT>" & baseHT & "</baseImposableHT>" &
                                nl & "          <taux>" & taux & "</taux>" &
                                nl & "          <tvaExigible>" & tvaExigible & "</tvaExigible>" &
                                nl & "      </nonResident>"
                        End If
                    Next

                    Dim contenu As String = baliseouvrir &
                        nl & "    <nonResidents>" & contenuNonResidents &
                        nl & "    </nonResidents>" &
                        nl & "    <nonResidentsRedevances>" & contenuRedevances &
                        nl & "    </nonResidentsRedevances>" &
                        nl & "</declarationNonResidents> "

                    Dim sw As New IO.StreamWriter(TextBox3.Text)
                    sw.WriteLine(contenu)
                    sw.Close()
                    If CheckBoxZip.Checked Then
                        Using zip As ZipFile = New ZipFile()
                            zip.AddFile(TextBox3.Text, "")
                            zip.Save(TextBox3.Text.Replace("xml", "zip"))
                        End Using
                    End If
                    MsgBox("Génération avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    Process.Start("explorer.exe", String.Format("/select,{0}", IO.Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), TextBox3.Text)))
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            cnx.Close()
        Catch ex As Exception
        End Try
        FrmLogin.Show()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox2.Text = "" Or ComboBox2.Text = "System.Data.DataRowView") Then
                Grille.DataSource = Nothing
                gridstyle()
                'ComboBox1.Text = ""
                Button4.Enabled = False
            ElseIf modeConnexion.ToLower() = "fournisseurnonresident" Then
                Dim com1NR As New SqlCommand
                com1NR.Connection = cnx
                com1NR.CommandText = "SELECT " &
                    "LigneNonResident.Idgeneration" &
                    ", LigneNonResident.Nligne" &
                    ", LigneNonResident.NomPrenomOuRaisonSociale AS [Nom/Raison Sociale]" &
                    ", LigneNonResident.AdresseEtranger AS [Adresse Etranger]" &
                    ", LigneNonResident.NIdentificationFiscale AS [N° Identification Fiscale]" &
                    ", LigneNonResident.NatureOperation AS [Nature Opération]" &
                    ", LigneNonResident.DatePaiement AS [Date Paiement]" &
                    ", LigneNonResident.BaseImposableHT AS [Base Imposable HT]" &
                    ", LigneNonResident.Taux AS [Taux]" &
                    ", LigneNonResident.TvaExigible AS [Tva Exigible]" &
                    ", LigneNonResident.MontantTva AS [Montant Tva]" &
                    ", LigneNonResident.EstRedevance AS [Redevance] " &
                    " FROM LigneNonResident INNER JOIN EnteteGeneration ON LigneNonResident.Idgeneration = EnteteGeneration.Idgeneration " &
                    " WHERE  EnteteGeneration.Libellegeneration =@p1  and  EnteteGeneration.IdSociete =@p2 and EnteteGeneration.ModeConnexion=@p3"

                With com1NR.Parameters
                    .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox2.Text
                    .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                    .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                End With
                Dim drNR As SqlDataReader
                drNR = com1NR.ExecuteReader
                Dim tNR As New DataTable()
                tNR.Clear()
                tNR.Load(drNR)
                Grille.DataSource = tNR

                Grille.Columns("Idgeneration").Visible = False
                Grille.Columns("Nligne").Visible = False
                gridstyle()
                gridStyle(Grille)
                Grille.RowHeadersVisible = True
                colorierTypeLigneNonResident()
                drNR.Close()
                For Each c As DataGridViewColumn In Grille.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                Button4.Enabled = True
                Dim parametersNR() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                    New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                    New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                }
                Dim WHERESUMSNR As String = "FROM [LigneNonResident] JOIN [EnteteGeneration] ON [LigneNonResident].[Idgeneration] = [EnteteGeneration].[Idgeneration] " &
                    "WHERE [EnteteGeneration].[Libellegeneration] = @p1 AND [EnteteGeneration].[Idsociete] = @p2 AND [EnteteGeneration].[ModeConnexion] = @p3 "
                Dim sqlSUMSNR As String = "SELECT COUNT(*) " & WHERESUMSNR &
                    " UNION ALL SELECT SUM(BaseImposableHT) " & WHERESUMSNR &
                    " UNION ALL SELECT SUM(ISNULL(TvaExigible,0) + ISNULL(MontantTva,0)) " & WHERESUMSNR
                Dim dtSUMSNR As DataTable = Read(sqlSUMSNR, parametersNR)
                Dim totalHTNR As Double = 0
                Dim totalTVANR As Double = 0
                If Not IsDBNull(dtSUMSNR.Rows(1)(0)) AndAlso dtSUMSNR.Rows(1)(0).ToString() <> "" Then
                    totalHTNR = Double.Parse(dtSUMSNR.Rows(1)(0).ToString())
                End If
                If Not IsDBNull(dtSUMSNR.Rows(2)(0)) AndAlso dtSUMSNR.Rows(2)(0).ToString() <> "" Then
                    totalTVANR = Double.Parse(dtSUMSNR.Rows(2)(0).ToString())
                End If
                LabelCountSums.Text = dtSUMSNR.Rows(0)(0).ToString() & " enregistrements | " &
                    "Total Base Imposable HT: " & Format(totalHTNR, "n").ToString() & " | " &
                    "Total Tva: " & Format(totalTVANR, "n").ToString()
            Else
                'cnx.Close()
                'cnx.Open()
                Dim com1 As New SqlCommand
                com1.Connection = cnx
                com1.CommandText = "SELECT " &
                    "LigneGeneration.Idgeneration" &
                    ", LigneGeneration.Nligne" &
                    ", LigneGeneration.Numfacture AS [N° Facture]" &
                    ",Designation as [Désignation]" &
                    ", LigneGeneration.Datefacture AS [Date Facture]" &
                    ", LigneGeneration.Identifiantfiscale AS [Identifiant Fiscal]" &
                    ", LigneGeneration.Fournisseur AS [Nom Fournisseur]" &
                    ", LigneGeneration.ice AS [ICE]" &
                    ", LigneGeneration.MontantHT AS [Montant HT]" &
                    ", LigneGeneration.TauxTVA AS [Taux Tva]" &
                    ", LigneGeneration.MontantTVA AS [Montant Tva]" &
                    ", LigneGeneration.Modereglement AS [Mode règlement]" &
                    ", LigneGeneration.Numreglement AS [N° Règlement]" &
                    ", datepaiement as [Date Paiement]" &
                    ", LigneGeneration.prorata AS [Prorata]" &
                    ", Societe.regime AS [Régime] " &
                    ", LigneGeneration.RefNatOpt AS [Réf Nature Opération]" &
                    ", LigneGeneration.TauxRetenuSource AS [Taux Retenue Source]" &
                    ", LigneGeneration.RefTypClt AS [Réf Type Client]" &
                    " FROM LigneGeneration INNER JOIN EnteteGeneration ON LigneGeneration.Idgeneration = EnteteGeneration.Idgeneration inner join Societe on Societe .Idsociete =EnteteGeneration .IdSociete " &
                    " WHERE  EnteteGeneration.Libellegeneration =@p1  and  Societe .Idsociete =@p2 and EnteteGeneration.ModeConnexion=@p3"

                With com1.Parameters
                    .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox2.Text
                    .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
                    .Add("@p3", SqlDbType.VarChar, 50).Value = modeConnexion
                End With
                Dim dr As SqlDataReader
                dr = com1.ExecuteReader
                Dim t As New DataTable()
                t.Clear()
                t.Load(dr)
                Grille.DataSource = t

                Grille.Columns("Idgeneration").Visible = False
                Grille.Columns("Nligne").Visible = False
                gridstyle()
                gridStyle(Grille)
                Grille.RowHeadersVisible = True
                colorierTypeLigne()
                anomalie()
                dr.Close()
                For Each c As DataGridViewColumn In Grille.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                Button4.Enabled = True
                Dim parametersFF() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                    New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                    New SqlParameter("@p3", SqlDbType.VarChar, 50) With {.Value = modeConnexion}
                }
                Dim WHERESUMS As String = "FROM [LigneGeneration] JOIN [EnteteGeneration] ON [LigneGeneration].[Idgeneration] = [EnteteGeneration].[Idgeneration] " &
                    "WHERE [EnteteGeneration].[Libellegeneration] = @p1 AND [EnteteGeneration].[Idsociete] = @p2 AND [EnteteGeneration].[ModeConnexion] = @p3 "
                Dim sqlSUMS As String = "SELECT COUNT(*) " & WHERESUMS &
                    " UNION ALL SELECT SUM(MontantHT) " & WHERESUMS &
                    " UNION ALL SELECT SUM(MontantTVA) " & WHERESUMS
                Dim dtSUMS As DataTable = Read(sqlSUMS, parametersFF)
                Dim totalHT As Double = 0
                Dim totalTVA As Double = 0
                If Not IsDBNull(dtSUMS.Rows(1)(0)) AndAlso dtSUMS.Rows(1)(0).ToString() <> "" Then
                    totalHT = Double.Parse(dtSUMS.Rows(1)(0).ToString())
                End If
                If Not IsDBNull(dtSUMS.Rows(2)(0)) AndAlso dtSUMS.Rows(2)(0).ToString() <> "" Then
                    totalTVA = Double.Parse(dtSUMS.Rows(2)(0).ToString())
                End If
                LabelCountSums.Text = dtSUMS.Rows(0)(0).ToString() & " enregistrements | " &
                    "Total Montant HT: " & Format(totalHT, "n").ToString() & " | " &
                    "Total Montant TVA: " & Format(totalTVA, "n").ToString()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
        End Try
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            cnx.Close()
            cnx.Open()
            If (ComboBox1.Text <> "" And ComboBox1.Text <> "") Then
                remplircombo()
            End If
        Catch ex As Exception
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            ''MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            SaveFileDialog1.ShowDialog()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub SaveFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            TextBox3.Text = SaveFileDialog1.FileName & ".xml"
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Try
        Catch ex As Exception
        End Try
    End Sub
    Private Sub passercommandee_Click(sender As Object, e As EventArgs) Handles passercommandee.Click
        'MiseaJourSociete.ShowDialog()
    End Sub
    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSoc.Click
        Try
            If (ToolStripMenuItemSoc.Text = "Sociétés") Then
                MiseaJourSociete.ShowDialog()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LabelClient.Text = societe
        ToolStripStatusLabelCur.Text = curentUser
        ToolStripStatusLabelExperitaion.Text = msgExpiration
        ToolStripStatusLabelCurTime.Text = DateTime.Now()
        If curentUser.ToLower() <> "admin" And curentUser.ToLower() <> "administrateur" Then
            FrmLogin.LoadRoles(curentUser)
        Else
            Dim societeIDS As DataTable = Read("SELECT Idsociete FROM Societe ")
            Dim socitiesRights As String = ""
            For x = 0 To societeIDS.Rows.Count - 1
                socitiesRights &= "," & societeIDS.Rows(x)(0).ToString()
            Next
            socitiesRights = socitiesRights.TrimEnd(socitiesRights, ",")
            socitiesRights = socitiesRights.TrimStart(socitiesRights, ",")

            acceuil_lecturedonnees = True
            acceuil_traitement = True
            acceuil_generation = True
            acceuil_societesmaj = True
            acceuil_usersmaj = True
            linkedsocities = socitiesRights
        End If
        lecture.Enabled = acceuil_lecturedonnees
        traitement.Enabled = acceuil_traitement
        generation.Enabled = acceuil_generation
        ''Commentaire passercommandee.Enabled = acceuil_societesmaj
        ToolStripMenuItemSoc.Enabled = acceuil_societesmaj
        ToolStripMenuItemUsers.Enabled = acceuil_usersmaj
    End Sub

    Private Sub ToolStripMenuItemPwd_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemPwd.Click
        Dim chngPwd As ChangePwd = New ChangePwd()
        chngPwd.ShowDialog()
    End Sub
    Private Sub ToolStripMenuItemUsers_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemUsers.Click
        Dim userUp As FrmUsersUp = New FrmUsersUp()
        userUp.ShowDialog()
    End Sub

    Private Sub GérerLaLicenceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GérerLaLicenceToolStripMenuItem.Click
        gererLicence = True
        TrialLicCheck.ShowDialog()
    End Sub

    Private Sub DéconnexionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DéconnexionToolStripMenuItem.Click
        Me.Close()
        FrmLogin.Show()
    End Sub

    Private Sub MiseÀJourToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MiseÀJourToolStripMenuItem.Click
        Process.Start(Application.StartupPath & "/updater.exe")
        End
    End Sub


    Private Sub LinkLabelXsl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelXsl.LinkClicked
        If Grille.Rows.Count = 0 Then
            MsgBox("Aucune donnée à exporter.", MsgBoxStyle.Information, "Simpl-TVA")
            Exit Sub
        End If

        Dim excelApp As Excel.Application = Nothing
        Dim excelWorkbook As Excel.Workbook = Nothing
        Dim excelSheet As Excel.Worksheet = Nothing

        Try
            excelApp = New Excel.Application()
            excelApp.Visible = False
            excelApp.DisplayAlerts = False
            excelWorkbook = excelApp.Workbooks.Add()
            excelSheet = CType(excelWorkbook.Sheets(1), Excel.Worksheet)

            ' En-têtes : uniquement les colonnes actuellement visibles, dans l'ordre affiché à l'écran
            Dim visibleColumns As New List(Of DataGridViewColumn)
            Dim colIndex As Integer = 1
            For Each col As DataGridViewColumn In Grille.Columns
                If col.Visible Then
                    visibleColumns.Add(col)
                    excelSheet.Cells(1, colIndex) = col.HeaderText
                    colIndex += 1
                End If
            Next
            excelSheet.Range(excelSheet.Cells(1, 1), excelSheet.Cells(1, visibleColumns.Count)).Font.Bold = True

            ' Données
            Dim rowIndex As Integer = 2
            For Each row As DataGridViewRow In Grille.Rows
                If Not row.IsNewRow Then
                    colIndex = 1
                    For Each col As DataGridViewColumn In visibleColumns
                        Dim valeur = row.Cells(col.Index).Value
                        excelSheet.Cells(rowIndex, colIndex) = IIf(valeur Is Nothing OrElse IsDBNull(valeur), "", valeur.ToString())
                        colIndex += 1
                    Next
                    rowIndex += 1
                End If
            Next

            excelSheet.Columns.AutoFit()

            Dim saveDialog As New SaveFileDialog()
            saveDialog.Filter = "Fichier Excel (*.xlsx)|*.xlsx"
            saveDialog.FileName = "Export_" & modeConnexion & "_" & ComboBox2.Text.Replace("/", "-") & ".xlsx"
            If saveDialog.ShowDialog() = DialogResult.OK Then
                excelWorkbook.SaveAs(saveDialog.FileName)
                MsgBox("Export terminé avec succès." & vbNewLine & saveDialog.FileName, MsgBoxStyle.Information, "Simpl-TVA")
            End If

        Catch ex As Exception
            MsgBox("L'export Excel a échoué : " & ex.Message & vbNewLine & vbNewLine &
                   "Vérifie que Microsoft Excel est bien installé sur ce poste.", MsgBoxStyle.Critical, "Simpl-TVA")
        Finally
            Try
                If excelWorkbook IsNot Nothing Then excelWorkbook.Close(False)
                If excelApp IsNot Nothing Then excelApp.Quit()
            Catch
            End Try
            If excelSheet IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(excelSheet)
            If excelWorkbook IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook)
            If excelApp IsNot Nothing Then System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
            excelSheet = Nothing
            excelWorkbook = Nothing
            excelApp = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelRecap.LinkClicked
        Recapitulatif.per = "FROM [LigneGeneration] JOIN [EnteteGeneration] ON [LigneGeneration].[Idgeneration] = [EnteteGeneration].[Idgeneration] " &
                    "WHERE [EnteteGeneration].[Libellegeneration] = '" & ComboBox2.Text & "' AND [EnteteGeneration].[Idsociete] = " & ComboBox1.SelectedValue & " AND [EnteteGeneration].[ModeConnexion] = '" & modeConnexion & "' "
        Recapitulatif.ShowDialog()
    End Sub

    Private Sub Grille_KeyDown(sender As Object, e As KeyEventArgs) Handles Grille.KeyDown
        'If e.KeyCode = Keys.Delete Then
        '    If MessageBox.Show("voulez vous supprimer l'enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
        '        e.Handled = True
        '    Else
        '        e.Handled = False
        '    End If
        'End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        modele.ShowDialog()
    End Sub
End Class