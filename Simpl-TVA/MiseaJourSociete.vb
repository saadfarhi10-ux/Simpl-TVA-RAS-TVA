Imports System.Data.SqlClient
Public Class MiseaJourSociete

    Sub actualiser()
        Dim com As New SqlCommand
        com.Connection = cnx
        com.CommandText = "select * from societe"
        Open()
        Dim dr As SqlDataReader
        dr = com.ExecuteReader
        Dim t As New DataTable()
        t.Clear()
        t.Load(dr)
        DataGridView1.DataSource = t
        DataGridView1.Columns(0).Visible = False
        gridStyle(DataGridView1)
        dr.Close()
    End Sub

    Function verifier(ByVal id As Integer) As Boolean
        Dim existe As Boolean
        Dim com As New SqlCommand
        com.Connection = cnx
        Open()
        com.CommandText = "select count(0) from societe where idsociete=@p1"
        With com.Parameters
            .AddWithValue("@p1", id)
        End With
        If (Convert.ToInt16(com.ExecuteScalar) = 0) Then
            existe = False
        Else
            existe = True
        End If
        Return existe
    End Function


    Sub TetsSocCountToSocCountAllowed()
        Dim LicenceData As DataTable = Read("SELECT [nbr_soc] FROM [syslicenceinfo] WHERE [systemeid] = '" & MachineSysID & "' ")
        nbr_soc_allowed = LicenceData.Rows(0)("nbr_soc").ToString()

        Dim SocietesData As DataTable = Read("SELECT COUNT(*) AS [COUNT_SOC] FROM [societe] ")
        Dim Count_Soc As Integer = Integer.Parse(SocietesData.Rows(0)(0).ToString())
        Dim ToolStripMsg As String = ""

        ToolStripMsg = Count_Soc & " éléments "

        If nbr_soc_allowed <= 2 Then
            Select Case Integer.Parse(nbr_soc_allowed)
                Case 1
                    If Count_Soc >= 5 Then
                        For Each Control As Control In Me.Controls
                            If Not Control.Text.Equals("&Supprimer") And Not TypeOf (Control) Is DataGridView Then
                                Control.Enabled = False
                            End If
                        Next
                        ToolStripMsg = "Vous avez ajouté le nombre de sociétés autorisé"
                    ElseIf Count_Soc < 5 Then
                        EnableAllControls()
                    End If
                Case 2
                    If Count_Soc >= 10 Then
                        For Each Control As Control In Me.Controls
                            If Not Control.Text.Equals("&Supprimer") And Not TypeOf (Control) Is DataGridView Then
                                Control.Enabled = False
                            End If
                        Next
                        ToolStripMsg = "Vous avez ajouté le nombre de sociétés autorisé"
                    ElseIf Count_Soc < 10 Then
                        EnableAllControls()
                    End If
                Case Else
            End Select
        End If
        
        ToolStripStatusLabel1.Text = ToolStripMsg
    End Sub

    Sub EnableAllControls()
        For Each Control As Control In Me.Controls
            Control.Enabled = True
        Next
    End Sub


    Private Sub MiseaJourSociete_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MachineSysID = encodeSerialNumber(GetSNumber())
        TetsSocCountToSocCountAllowed()
        'Try
        'cnx.Close()
        'cnx.Open()
        actualiser()
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If (e.RowIndex <> -1) Then
                id.Text = DataGridView1.Rows(e.RowIndex).Cells("Idsociete").Value.ToString
                raisonsociale.Text = DataGridView1.Rows(e.RowIndex).Cells("RaisonSociale").Value.ToString
                identifiantfiscal.Text = DataGridView1.Rows(e.RowIndex).Cells("IF").Value.ToString
                tva.Text = DataGridView1.Rows(e.RowIndex).Cells("TVA").Value.ToString
                cnss.Text = DataGridView1.Rows(e.RowIndex).Cells("CNSS").Value.ToString
                ville.Text = DataGridView1.Rows(e.RowIndex).Cells("Ville").Value.ToString
                pays.Text = DataGridView1.Rows(e.RowIndex).Cells("Pays").Value.ToString
                adresse.Text = DataGridView1.Rows(e.RowIndex).Cells("Adresse").Value.ToString
                email.Text = DataGridView1.Rows(e.RowIndex).Cells("Email").Value.ToString
                fax.Text = DataGridView1.Rows(e.RowIndex).Cells("Fax").Value.ToString
                site.Text = DataGridView1.Rows(e.RowIndex).Cells("Site").Value.ToString
                patente.Text = DataGridView1.Rows(e.RowIndex).Cells("Patente").Value.ToString
                telephone.Text = DataGridView1.Rows(e.RowIndex).Cells("Téléphone").Value.ToString
                sce.Text = DataGridView1.Rows(e.RowIndex).Cells("ICE").Value.ToString
                TxtProrata.Text = DataGridView1.Rows(e.RowIndex).Cells("regime").Value.ToString()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Btn_Submit_Click(sender As Object, e As EventArgs) Handles Btn_Submit.Click
        Open()

        Try
            Dim com As New SqlCommand
            com.Connection = cnx
            com.CommandText = "select max(idsociete) from societe"
            If (IsDBNull(com.ExecuteScalar)) Then
                id.Text = "1"
            Else
                id.Text = Convert.ToInt16(com.ExecuteScalar) + 1
            End If
            raisonsociale.Text = ""
            identifiantfiscal.Text = ""
            tva.Text = ""
            cnss.Text = ""
            ville.Text = ""
            pays.Text = ""
            adresse.Text = ""
            email.Text = ""
            fax.Text = ""
            site.Text = ""
            patente.Text = ""
            telephone.Text = ""
            sce.Text = ""
            TxtProrata.Text = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Btn_Profil_Click(sender As Object, e As EventArgs) Handles Btn_Profil.Click
        Open()

        Try
            Dim idsociete As Integer
            If (id.Text = "" Or Not IsNumeric(id.Text)) Then
                idsociete = 0
            Else
                idsociete = Convert.ToInt16(id.Text)
            End If
            If (idsociete = 0) Then
                MsgBox("Merci de choisir la société que voulez vous supprimez !!!", MsgBoxStyle.Information, "Simpl-TVA")
            Else
                Dim com As New SqlCommand
                com.Connection = cnx

                com.CommandText = "delete from societe where idsociete =@p1"
                With com.Parameters
                    .AddWithValue("@p1", idsociete)
                End With
                com.ExecuteNonQuery()
                MsgBox("Suppression avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                id.Text = ""
                raisonsociale.Text = ""
                identifiantfiscal.Text = ""
                tva.Text = ""
                cnss.Text = ""
                ville.Text = ""
                pays.Text = ""
                adresse.Text = ""
                email.Text = ""
                fax.Text = ""
                site.Text = ""
                patente.Text = ""
                telephone.Text = ""
                sce.Text = ""
                TxtProrata.Text = ""
                actualiser()
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        TetsSocCountToSocCountAllowed()

    End Sub
    Private Sub Btn_Del_Click(sender As Object, e As EventArgs) Handles Btn_Del.Click
        Open()
        Try
            Dim reg As String = TxtProrata.Text
            Dim idsociete As Integer
            If (id.Text = "" Or Not IsNumeric(id.Text)) Then
                idsociete = 0
            Else
                idsociete = Convert.ToInt16(id.Text)
            End If
            If (verifier(idsociete) = False) Then
                'ajout
                If (raisonsociale.Text = "") Then
                    MsgBox("Champs Raison Sociale est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    raisonsociale.BackColor = Color.Red
                ElseIf (TxtProrata.Text = "") Then
                    MsgBox("Champs Régime est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                ElseIf (identifiantfiscal.Text = "") Then
                    MsgBox("Champs IF est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    identifiantfiscal.BackColor = Color.Red
                ElseIf (patente.Text = "") Then
                    MsgBox("Champs Patente est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    patente.BackColor = Color.Red
                Else
                    Dim com As New SqlCommand
                    com.Connection = cnx

                    com.CommandText = "insert into Societe (raisonsociale,[if],tva,cnss,patente,pays,ville,adresse,téléphone,fax,email,site, [ICE], [regime]) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14)"
                    With com.Parameters
                        .AddWithValue("@p1", raisonsociale.Text)
                        .AddWithValue("@p2", identifiantfiscal.Text)
                        .AddWithValue("@p3", tva.Text)
                        .AddWithValue("@p4", cnss.Text)
                        .AddWithValue("@p5", patente.Text)
                        .AddWithValue("@p6", pays.Text)
                        .AddWithValue("@p7", ville.Text)
                        .AddWithValue("@p8", adresse.Text)
                        .AddWithValue("@p9", telephone.Text)
                        .AddWithValue("@p10", fax.Text)
                        .AddWithValue("@p11", email.Text)
                        .AddWithValue("@p12", site.Text)
                        .AddWithValue("@p13", sce.Text)
                        .AddWithValue("@p14", reg)
                    End With
                    com.ExecuteNonQuery()


                    MsgBox("Ajout avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    actualiser()
                End If

            Else
                'modification
                If (raisonsociale.Text = "") Then
                    MsgBox("Champs Raison Sociale est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    raisonsociale.BackColor = Color.Red
                ElseIf (identifiantfiscal.Text = "") Then
                    MsgBox("Champs IF est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    identifiantfiscal.BackColor = Color.Red
                ElseIf (patente.Text = "") Then
                    MsgBox("Champs Patente est obligatoire !!!", MsgBoxStyle.Information, "Simpl-TVA")
                    patente.BackColor = Color.Red
                Else
                    Dim com As New SqlCommand
                    com.Connection = cnx
                    com.CommandText = "Update Societe set raisonsociale=@p1,[if]=@p2,tva=@p3,cnss=@p4,patente=@p5,pays=@p6,ville=@p7,adresse=@p8,téléphone=@p9,fax=@p10,email=@p11,site =@p12, [ICE] = @p14, [regime] = @p15 where idsociete=@p13"
                    With com.Parameters
                        .AddWithValue("@p1", raisonsociale.Text)
                        .AddWithValue("@p2", identifiantfiscal.Text)
                        .AddWithValue("@p3", tva.Text)
                        .AddWithValue("@p4", cnss.Text)
                        .AddWithValue("@p5", patente.Text)
                        .AddWithValue("@p6", pays.Text)
                        .AddWithValue("@p7", ville.Text)
                        .AddWithValue("@p8", adresse.Text)
                        .AddWithValue("@p9", telephone.Text)
                        .AddWithValue("@p10", fax.Text)
                        .AddWithValue("@p11", email.Text)
                        .AddWithValue("@p12", site.Text)
                        .AddWithValue("@p13", id.Text)
                        .AddWithValue("@p14", sce.Text)
                        .AddWithValue("@p15", reg)
                    End With
                    com.ExecuteNonQuery()
                    MsgBox("Modification avec succès", MsgBoxStyle.Information, "Simpl-TVA")
                    actualiser()
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        TetsSocCountToSocCountAllowed()
    End Sub
    Private Sub raisonsociale_TextChanged(sender As Object, e As EventArgs) Handles raisonsociale.TextChanged
        Try
            If (raisonsociale.Text <> "") Then
                raisonsociale.BackColor = Color.White
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub identifiantfiscal_TextChanged(sender As Object, e As EventArgs) Handles identifiantfiscal.TextChanged
        Try
            If (identifiantfiscal.Text <> "") Then
                identifiantfiscal.BackColor = Color.White
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub patente_TextChanged(sender As Object, e As EventArgs) Handles patente.TextChanged
        Try
            If (patente.Text <> "") Then
                patente.BackColor = Color.White
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class