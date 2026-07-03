Imports System.Data.SqlClient

Public Class ChangePwd
    Dim AncienPwd As String

    Private Sub ChangePwd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cmbutilisateur.Text = curentUser
        Dim curUserData As DataTable = Read("SELECT * FROM [users] WHERE [userLogOn]='" & curentUser & "' ")
        If curUserData.Rows.Count > 0 Then
            AncienPwd = curUserData.Rows(0)("userPassword").ToString()
        End If
    End Sub

    Private Sub CmdConfirmer_Click(sender As Object, e As EventArgs) Handles CmdConfirmer.Click
        Dim NouveauPwd As String
        Dim AncienPwdSaisie As String = TxtAncPwd.Text

        If AncienPwd <> encodepwd(AncienPwdSaisie) Then
            MsgBox("L'ancien mot de passe est incorrect ", MsgBoxStyle.Critical, Me.Text)
        Else
            If TxtNouvPwd.Text <> TxtConfirmPwd.Text Then
                Call MsgBox("La confirmation du nouveau mot de passe est incorrecte", MsgBoxStyle.Critical, Me.Text)
                TxtConfirmPwd.Focus()
                Exit Sub
            End If
            NouveauPwd = encodepwd(TxtNouvPwd.Text)
            Dim parametersFF() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@p1", SqlDbType.VarChar, 255) With {.Value = NouveauPwd},
                New SqlParameter("@p2", SqlDbType.VarChar, 255) With {.Value = curentUser}
            }
            Dim result As Integer = Execute("UPDATE [users] set [userPassword] = @p1 WHERE [userLogOn] = @p2 ", parametersFF)
            If result > 0 Then
                MessageBox.Show("Opération effectuer avec succéss", "Changement de mot passe", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Opération non effectuer veuillez réessayer", "Changement de mot passe", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
End Class