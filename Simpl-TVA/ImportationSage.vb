Imports System.Data.Odbc
Imports System.Data.SqlClient

Public Class ImportationSage
    Sub grillestyle()
        Try
            Grille.Columns("EC_REFERENCE").HeaderText = "N° Facture"
            Grille.Columns("EC_Montant").HeaderText = "Montant HT"
            Grille.Columns("EC_Intitule1").HeaderText = "Désignation"
            Grille.Columns("EC_Intitule").HeaderText = "N° Réglement"
            Grille.Columns("INT_REGLEMENT").HeaderText = "Mode Réglement"
            Grille.Columns("EXPR_1").HeaderText = "Date Facture"
            Grille.Columns("CT_Intitule").HeaderText = "Nom Fournisseur"
            Grille.Columns("EXPR_2").HeaderText = "Taux Tva"
            Grille.Columns("EXPR_3").HeaderText = "Date Paiement"
            Grille.Columns("EC_Montant1").HeaderText = "Montant Tva"
            Grille.Columns("CT_SIRET").HeaderText = "Identifiant Fiscale"
            Grille.Columns("CT_IDENTIFIANT").HeaderText = "ICE"

            Grille.Columns("EC_REFERENCE").Width = 80
            Grille.Columns("EC_Montant").Width = 80
            Grille.Columns("EC_Intitule").Width = 180
            Grille.Columns("EC_Intitule1").Width = 180
            Grille.Columns("CT_Intitule").Width = 230
            Grille.Columns("EXPR_1").Width = 75
            Grille.Columns("EXPR_3").Width = 75
            Grille.Columns("INT_REGLEMENT").Width = 60
            Grille.Columns("EXPR_2").Width = 60
            Grille.Columns("EC_Montant1").Width = 80
            Grille.Columns("CT_SIRET").Width = 60
            Grille.Columns("CT_IDENTIFIANT").Width = 100
            Grille.Columns(0).Width = 70
            Grille.Columns("EC_Montant").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            Grille.Columns("EC_Montant1").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub anomalie()
        Try
            Dim d As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                d = Grille.Rows(i)


                If (d.Cells(1).Value.ToString = "") Then
                    d.Cells(1).Style.BackColor = Color.Red
                End If

                If (d.Cells(2).Value.ToString = "") Then
                    d.Cells(2).Style.BackColor = Color.Red
                End If
                If (d.Cells(3).Value.ToString = "") Then
                    d.Cells(3).Style.BackColor = Color.Red
                End If



                If (d.Cells(4).Value.ToString = "") Then
                    d.Cells(4).Style.BackColor = Color.Red
                End If

                If (IsDBNull(d.Cells(5).Value)) Then
                    d.Cells(5).Style.BackColor = Color.Red
                Else
                    If (d.Cells(5).Value = 0) Then
                        d.Cells(5).Style.BackColor = Color.Red
                    End If
                End If

                If (IsDBNull(d.Cells(6).Value)) Then
                    d.Cells(6).Style.BackColor = Color.Red
                Else
                    If (d.Cells(6).Value = 0) Then
                        d.Cells(6).Style.BackColor = Color.Red
                    End If
                End If

                If (IsDBNull(d.Cells(7).Value)) Then
                    d.Cells(7).Style.BackColor = Color.Red
                Else
                    If (d.Cells(7).Value = 0) Then
                        d.Cells(7).Style.BackColor = Color.Red
                    End If
                End If

                If (d.Cells(8).Value.ToString = "") Then
                    d.Cells(8).Style.BackColor = Color.Red
                End If
                If (d.Cells(9).Value.ToString = "") Then
                    d.Cells(9).Style.BackColor = Color.Red
                End If
                If (d.Cells(10).Value.ToString = "") Then
                    d.Cells(10).Style.BackColor = Color.Red
                End If
                If (d.Cells(11).Value.ToString = "") Then
                    d.Cells(11).Style.BackColor = Color.Red
                End If
                If (d.Cells(12).Value.ToString = "") Then
                    d.Cells(12).Style.BackColor = Color.Red
                End If
            Next
            Grille.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ImportationSage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            cnx.Close()
            cnx.Open()
            Dim com1 As New SqlCommand
            com1.Connection = cnx
            com1.CommandText = "(SELECT EnteteGeneration.Libellegeneration, EnteteGeneration.Statut FROM EnteteGeneration) union (select '','' from EnteteGeneration)"
            Dim dr1 As SqlDataReader
            dr1 = com1.ExecuteReader
            Dim t1 As New DataTable()
            t1.Load(dr1)
            ComboBox2.DataSource = t1
            ComboBox2.DisplayMember = "Libellegeneration"
            ComboBox2.ValueMember = "Statut"
            ComboBox2.Text = ""
            dr1.Close()
            cnx.Close()

            '            Dim strConnexion As String = ("Dsn=SageACB;uid=<Administrateur>;pwd=voic")
            '            Dim oConnection As Odbc.OdbcConnection = New OdbcConnection(strConnexion)

            '            Dim mystring As String = "Select FE.EC_REFERENCE  ,FE.EC_Jour+FE.JM_Date-1,FOUR.CT_SIRET,FOUR.CT_INTITULE,FE.EC_Montant,Round(Round((F.EC_Montant/FE.EC_Montant)*100,2),2)," & _
            '    "FEC.EC_Montant,F1.INT_REGLEMENT,F1.EC_Intitule,FE.EC_Intitule,F1.EC_Jour+F1.JM_Date-1,FOUR.CT_IDENTIFIANT" & _
            '"  from F_ECRITUREC FE ,F_ECRITUREC F,F_ECRITUREC FEC,F_COMPTET FOUR,F_JOURNAUX J,F_ECRITUREC F1" & _
            '" WHERE  J.JO_NUM=FE.JO_Num AND J.JO_INTITULE LIKE 'ACHATS'   AND FE.EC_Sens=0 and FE.CG_Num like '61%' AND F.CG_Num like '3455%' and F.EC_Sens =0 AND " & _
            '" FEC.EC_Sens=1 and FEC.CG_Num like '4411%' " & _
            '" AND F.EC_Piece=FE.EC_Piece and F.JM_Date=FE.JM_Date and F.EC_Jour=FE.EC_Jour AND FE.JO_Num=F.JO_Num AND FE.EC_Date=F.EC_Date" & _
            '" AND F.EC_Piece=FEC.EC_Piece and F.JM_Date=FEC.JM_Date and F.EC_Jour=FEC.EC_Jour AND FEC.JO_Num=F.JO_Num AND FEC.EC_Date=F.EC_Date" & _
            '" AND FEC.EC_Piece=FE.EC_Piece and FEC.JM_Date=FE.JM_Date and FEC.EC_Jour=FE.EC_Jour AND FE.JO_Num=FEC.JO_Num AND FE.EC_Date=FEC.EC_Date" & _
            '" AND FEC.CG_NUM=FOUR.CG_NUMPRINC AND FEC.CT_NUM=FOUR.CT_NUM AND FEC.CG_Num like '4411%' AND FEC.JO_Num like '6111' AND FEC.EC_Sens=1" & _
            '" AND F1.CT_NUM=FEC.CT_NUM AND F1.CG_NUM=FEC.CG_NUM AND F1.EC_LETTRAGE=FEC.EC_LETTRAGE AND F1.EC_MONTANT=FEC.EC_MONTANT AND F1.JO_NUM like '5142' AND F1.EC_LETTRAGE <>'' AND F1.CG_NUM LIKE '4411%' AND (FEC.JM_DATE LIKE '%05%' AND FEC.JM_DATE LIKE '%2015%')"

            'Fonction origine
            '            Dim mystring As String = "Select FE.EC_REFERENCE  ,FE.EC_Jour+FE.JM_Date-1,FOUR.CT_SIRET,FE.EC_Montant,FOUR.CT_INTITULE,Round(Round((F.EC_Montant/FE.EC_Montant)*100,2),2)," & _
            '"FEC.EC_Montant,FE.EC_Intitule,FOUR.CT_IDENTIFIANT" & _
            '"  from F_ECRITUREC FE ,F_ECRITUREC F,F_ECRITUREC FEC,F_COMPTET FOUR,F_JOURNAUX J" & _
            '" WHERE  J.JO_NUM=FE.JO_Num AND J.JO_INTITULE LIKE 'ACHATS'   AND FE.EC_Sens=0 and FE.CG_Num like '61%' AND F.CG_Num like '3455%' and F.EC_Sens =0 AND " & _
            '" FEC.EC_Sens=1 and FEC.CG_Num like '4411%' " & _
            '" AND F.EC_Piece=FE.EC_Piece and F.JM_Date=FE.JM_Date and F.EC_Jour=FE.EC_Jour AND FE.JO_Num=F.JO_Num AND FE.EC_Date=F.EC_Date" & _
            '" AND F.EC_Piece=FEC.EC_Piece and F.JM_Date=FEC.JM_Date and F.EC_Jour=FEC.EC_Jour AND FEC.JO_Num=F.JO_Num AND FEC.EC_Date=F.EC_Date" & _
            '" AND FEC.EC_Piece=FE.EC_Piece and FEC.JM_Date=FE.JM_Date and FEC.EC_Jour=FE.EC_Jour AND FE.JO_Num=FEC.JO_Num AND FE.EC_Date=FEC.EC_Date" & _
            '" AND FEC.CG_NUM=FOUR.CG_NUMPRINC AND FEC.CT_NUM=FOUR.CT_NUM AND FEC.CG_Num like '4411%' AND FEC.JO_Num like '6111' AND FEC.EC_Sens=1"





            'Dim cmd As OdbcCommand = New OdbcCommand(mystring)
            'cmd.Connection = oConnection
            'oConnection.Open()
            'Dim dr As OdbcDataReader
            'dr = cmd.ExecuteReader
            'Dim t As New DataTable()
            't.Clear()
            't.Load(dr)
            'Grille.DataSource = t
            '' MsgBox(Grille.Rows.Count)
            'grillestyle()
            'dr.Close()
            'oConnection.Close()
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


    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Dim dd As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                dd = Grille.Rows(i)
                dd.Cells(0).Value = True
            Next
            Grille.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Try
            Dim dd As New DataGridViewRow()
            For i As Integer = 0 To Grille.Rows.Count - 1
                dd = Grille.Rows(i)
                dd.Cells(0).Value = False
            Next
            Grille.Refresh()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox2_TextChanged(sender As Object, e As EventArgs) Handles ComboBox2.TextChanged
        Try
            If (ComboBox2.Text <> "" And ComboBox2.Text <> "System.Data.DataRowView") Then
                Dim mois, annee As String
                mois = ComboBox2.Text.Substring(0, 2)
                annee = ComboBox2.Text.Substring(3, 4)
                Dim strConnexion As String = ("Dsn=SageACB;uid=<Administrateur>;pwd=voic")
                Dim oConnection As Odbc.OdbcConnection = New OdbcConnection(strConnexion)

                Dim mystring As String = "Select FE.EC_REFERENCE  ,FE.EC_Jour+FE.JM_Date-1,FOUR.CT_SIRET,FOUR.CT_INTITULE,FE.EC_Montant,Round(Round((F.EC_Montant/FE.EC_Montant)*100,2),2)," & _
        "FEC.EC_Montant,F1.INT_REGLEMENT,F1.EC_Intitule,FE.EC_Intitule,F1.EC_Jour+F1.JM_Date-1,FOUR.CT_IDENTIFIANT" & _
    "  from F_ECRITUREC FE ,F_ECRITUREC F,F_ECRITUREC FEC,F_COMPTET FOUR,F_JOURNAUX J,F_ECRITUREC F1" & _
    " WHERE  J.JO_NUM=FE.JO_Num AND J.JO_INTITULE LIKE 'ACHATS'   AND FE.EC_Sens=0 and FE.CG_Num like '61%' AND F.CG_Num like '3455%' and F.EC_Sens =0 AND " & _
    " FEC.EC_Sens=1 and FEC.CG_Num like '4411%' " & _
    " AND F.EC_Piece=FE.EC_Piece and F.JM_Date=FE.JM_Date and F.EC_Jour=FE.EC_Jour AND FE.JO_Num=F.JO_Num AND FE.EC_Date=F.EC_Date" & _
    " AND F.EC_Piece=FEC.EC_Piece and F.JM_Date=FEC.JM_Date and F.EC_Jour=FEC.EC_Jour AND FEC.JO_Num=F.JO_Num AND FEC.EC_Date=F.EC_Date" & _
    " AND FEC.EC_Piece=FE.EC_Piece and FEC.JM_Date=FE.JM_Date and FEC.EC_Jour=FE.EC_Jour AND FE.JO_Num=FEC.JO_Num AND FE.EC_Date=FEC.EC_Date" & _
    " AND FEC.CG_NUM=FOUR.CG_NUMPRINC AND FEC.CT_NUM=FOUR.CT_NUM AND FEC.CG_Num like '4411%' AND FEC.JO_Num like '6111' AND FEC.EC_Sens=1" & _
    " AND F1.CT_NUM=FEC.CT_NUM AND F1.CG_NUM=FEC.CG_NUM AND F1.EC_LETTRAGE=FEC.EC_LETTRAGE AND F1.EC_MONTANT=FEC.EC_MONTANT AND F1.JO_NUM like '5142' AND F1.EC_LETTRAGE <>'' AND F1.CG_NUM LIKE '4411%' AND (FEC.JM_DATE LIKE '%" & mois & "%' AND FEC.JM_DATE LIKE '%" & annee & "%')"
                Dim cmd As OdbcCommand = New OdbcCommand(mystring)
                cmd.Connection = oConnection
                oConnection.Open()
                Dim dr As OdbcDataReader
                dr = cmd.ExecuteReader
                Dim t As New DataTable()
                t.Clear()
                t.Load(dr)
                Grille.DataSource = t
                ' MsgBox(Grille.Rows.Count)
                grillestyle()
                dr.Close()
                oConnection.Close()
            Else
                Grille.DataSource = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class