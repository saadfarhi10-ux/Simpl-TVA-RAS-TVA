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


    Public Sub gridStyle(g As DataGridView)
        g.ForeColor = Color.Black
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.AllowUserToAddRows = False
        g.RowHeadersVisible = False
    End Sub

    Sub gridstyle()
        Try
            If (Grille.Rows.Count > 0) Then
                Grille.Columns(2).Width = 60
                Grille.Columns(4).Width = 70
                Grille.Columns(5).Width = 60
                Grille.Columns(9).Width = 50
                Grille.Columns(11).Width = 60
                Grille.Columns(13).Width = 70
                Grille.Columns(14).Width = 70
                Grille.Columns(8).Width = 80
                Grille.Columns(10).Width = 80
                Grille.Columns(6).Width = 107
                Grille.Columns(7).Width = 50
            End If


        Catch ex As Exception
            ''MsgBox(ex.Message)
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
                New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue}
            }
            Dim checkEnteteGeneration As DataTable = Read("SELECT * FROM [EnteteGeneration] WHERE Libellegeneration = @P1 AND IdSociete = @P2 ", parametersF)
            If checkEnteteGeneration.Rows.Count >= 1 Then
                If MessageBox.Show("C'est déja traité voulez vous l'enregistrer du nouveau ?", "Traitement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            ''}code par yacine
            Dim SSSSql = "DELETE from LigneGeneration where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 ) ; " &
                " DELETE from EnteteGeneration where  EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2; "
            Execute(SSSSql, parametersF)

            parametersF = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue},
                New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = MaskedTextBox1.Text},
                New SqlParameter("@P3", SqlDbType.VarChar, 255) With {.Value = Label4.Text},
                New SqlParameter("@P4", SqlDbType.DateTime) With {.Value = DateTime.Now.Date},
                New SqlParameter("@P5", SqlDbType.DateTime) With {.Value = DateTime.Now.Date}
            }
            SSSSql = "insert into EnteteGeneration (IdSociete,Libellegeneration,Userid,Datecreation,Datemodification,Statut) " &
                " values(@p1,@p2,@p3,@p4,@p5,1)"
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
                stringReader = fileReader.ReadLine()
                Dim str As String
                Dim TestArray() As String

                str = stringReader
                TestArray = str.Split(";")
                numfacture = IIf(IsDBNull(TestArray(0)), "", TestArray(0))
                If numfacture.ToLower().Equals("fin") Then
                    Exit While
                End If
                indexCol = 2
                datefacture = IIf(IsDBNull(TestArray(1)), DateTime.Now.Date, DateTime.Parse(TestArray(1)))
                identifiant = IIf(IsDBNull(TestArray(2)), "", TestArray(2))
                fournisseur = IIf(IsDBNull(TestArray(3)), "", TestArray(3))
                indexCol = 5
                montantht = 0
                If TestArray(4).ToString <> "" Then
                    montantht = Double.Parse(TestArray(4).Replace(".", ","))
                End If
                indexCol = 6
                If (IsDBNull(TestArray(5))) Then
                    tauxtva = 0
                Else
                    tauxtva = 0
                    If (TestArray(5).ToString <> "") Then
                        tauxtva = Double.Parse(TestArray(5).Replace(".", ","))
                    End If
                End If
                indexCol = 7
                montanttva = 0
                If TestArray(6).ToString <> "" Then
                    montanttva = Double.Parse(TestArray(6).Replace(".", ","))
                End If
                'MsgBox(TestArray(6).Replace(".", ","))
                modereglement = IIf(IsDBNull(TestArray(7)), "", TestArray(7))
                nreglement = IIf(IsDBNull(TestArray(8)), "", TestArray(8))
                designation = IIf(IsDBNull(TestArray(9)), "", TestArray(9))
                datepaiement = IIf(IsDBNull(TestArray(10)), "", DateTime.Parse(TestArray(10)))
                ice = IIf(IsDBNull(TestArray(11)), "", TestArray(11))
                Dim prorata As Double = 0
                If Not TestArray(12).ToString().Equals("") Then
                    prorata = Double.Parse(TestArray(12).Replace(".", ","))
                End If

                SSSSql = "INSERT INTO [LigneGeneration] " &
                    "([Idgeneration], [Nligne], [Numfacture], [Datefacture], [Identifiantfiscale], [Fournisseur], [MontantHT], [TauxTVA], [MontantTVA], [Modereglement], [Numreglement], [Designation], [Datepaiement], [ice], [prorata]) " &
                    "VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11,@P12,@P13,@P14,@P15)"
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
                    New SqlParameter("@P15", SqlDbType.Float) With {.Value = prorata}
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
            com1.CommandText = "(SELECT EnteteGeneration.Libellegeneration, EnteteGeneration.Statut FROM EnteteGeneration Where idsociete=@p1) union (select '','' from EnteteGeneration)"
            With com1.Parameters
                .Add("@p1", SqlDbType.Int).Value = ComboBox1.SelectedValue
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
            com2.CommandText = "(SELECT EnteteGeneration.Libellegeneration, EnteteGeneration.Statut FROM EnteteGeneration Where Statut in (1,2,3) and idsociete=@p1) union (select '','' from EnteteGeneration)"
            With com2.Parameters
                .Add("@p1", SqlDbType.Int).Value = ComboBox1.SelectedValue
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
                importer()
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
            com.CommandText = "SELECT EnteteGeneration.Statut FROM EnteteGeneration WHERE EnteteGeneration.Libellegeneration=@p1  and [IdSociete]=@p2"
            With com.Parameters
                .Add("@p1", SqlDbType.VarChar, 255).Value = libelle
                .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
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

                Dim parametersF() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                    New SqlParameter("@P2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue}
                }
                Dim yyySql = "DELETE from LigneGeneration where idgeneration =(select top 1 idgeneration from EnteteGeneration " &
                    " where EnteteGeneration.Libellegeneration=@p1 and [IdSociete]=@p2 )  "
                Execute(yyySql, parametersF)

                yyySql = "Update EnteteGeneration set statut=2 where Libellegeneration=@p1 and [IdSociete]=@p2 "
                Execute(yyySql, parametersF)

                Dim d As New DataGridViewRow
                For i As Integer = 0 To Grille.Rows.Count - 1
                    d = Grille.Rows(i)
                    Dim t1, t2, t3, t4, t5, t6, t11, t12, t13, t10, t14, t15 As String
                    Dim t7, t8, t9 As Double

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
                        "([Idgeneration], [Nligne], [Numfacture], [Datefacture], [Identifiantfiscale], [Fournisseur], [MontantHT], [TauxTVA], [MontantTVA], [Modereglement], [Numreglement], [Designation], [Datepaiement], [ice], [prorata]) " &
                        "VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)"

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
                        New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue}
                    }
                    Dim vvvSql = "Update EnteteGeneration set statut=3 where Libellegeneration=@p1 and [IdSociete]=@p2"
                    Execute(vvvSql, parametersFF)

                    Dim ssql As String = "SELECT [regime], [IF] FROM Societe INNER JOIN " &
                        "EnteteGeneration ON Societe.Idsociete = EnteteGeneration.IdSociete " &
                        "WHERE EnteteGeneration.Libellegeneration = @p1 and EnteteGeneration.[IdSociete]= @p2"

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
                        "   WHERE EnteteGeneration.Libellegeneration = @p1 AND EnteteGeneration.[IdSociete]=@p2"
                    With com2.Parameters
                        .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox3.Text
                        .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
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
                    " FROM LigneGeneration INNER JOIN EnteteGeneration ON LigneGeneration.Idgeneration = EnteteGeneration.Idgeneration inner join Societe on Societe .Idsociete =EnteteGeneration .IdSociete " &
                    " WHERE  EnteteGeneration.Libellegeneration =@p1  and  Societe .Idsociete =@p2"

                With com1.Parameters
                    .Add("@p1", SqlDbType.VarChar, 255).Value = ComboBox2.Text
                    .Add("@p2", SqlDbType.Int).Value = ComboBox1.SelectedValue
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
                anomalie()
                dr.Close()
                For Each c As DataGridViewColumn In Grille.Columns
                    c.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
                Button4.Enabled = True
                Dim parametersFF() As SqlParameter = New SqlParameter() _
                {
                    New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = ComboBox2.Text},
                    New SqlParameter("@p2", SqlDbType.Int) With {.Value = ComboBox1.SelectedValue}
                }
                Dim WHERESUMS As String = "FROM [LigneGeneration] JOIN [EnteteGeneration] ON [LigneGeneration].[Idgeneration] = [EnteteGeneration].[Idgeneration] " &
                    "WHERE [EnteteGeneration].[Libellegeneration] = @p1 AND [EnteteGeneration].[Idsociete] = @p2 "
                Dim sqlSUMS As String = "SELECT COUNT(*) " & WHERESUMS &
                    " UNION ALL SELECT SUM(MontantHT) " & WHERESUMS &
                    " UNION ALL SELECT SUM(MontantTVA) " & WHERESUMS
                Dim dtSUMS As DataTable = Read(sqlSUMS, parametersFF)
                LabelCountSums.Text = dtSUMS.Rows(0)(0).ToString() & " enregistrements | " &
                    "Total Montant HT: " & Format(Double.Parse(dtSUMS.Rows(1)(0).ToString()), "n").ToString() & " | " &
                    "Total Montant TVA: " & Format(Double.Parse(dtSUMS.Rows(2)(0).ToString()), "n").ToString()
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
        MsgBox("Export Excel désactivé temporairement.", MsgBoxStyle.Information, "Simpl-TVA")
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabelRecap.LinkClicked
        Recapitulatif.per = "FROM [LigneGeneration] JOIN [EnteteGeneration] ON [LigneGeneration].[Idgeneration] = [EnteteGeneration].[Idgeneration] " &
                    "WHERE [EnteteGeneration].[Libellegeneration] = '" & ComboBox2.Text & "' AND [EnteteGeneration].[Idsociete] = " & ComboBox1.SelectedValue & " "
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