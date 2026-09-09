Public Class FrmModeConnexion

    Private Sub BtnClient_Click(sender As Object, e As EventArgs) Handles BtnClient.Click
        modeConnexion = "Client"
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnFournisseur_Click(sender As Object, e As EventArgs) Handles BtnFournisseur.Click
        modeConnexion = "Fournisseur"
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnFournisseurNonResident_Click(sender As Object, e As EventArgs) Handles BtnFournisseurNonResident.Click
        modeConnexion = "FournisseurNonResident"
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnFournisseurNonResident_MouseEnter(sender As Object, e As EventArgs) Handles BtnFournisseurNonResident.MouseEnter
        BtnFournisseurNonResident.BackColor = Color.FromArgb(255, 102, 0)
        BtnFournisseurNonResident.ForeColor = Color.White
    End Sub

    Private Sub BtnFournisseurNonResident_MouseLeave(sender As Object, e As EventArgs) Handles BtnFournisseurNonResident.MouseLeave
        BtnFournisseurNonResident.BackColor = Color.White
        BtnFournisseurNonResident.ForeColor = Color.Indigo
    End Sub

    Private Sub BtnQuitterMode_Click(sender As Object, e As EventArgs) Handles BtnQuitterMode.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub BtnClient_MouseEnter(sender As Object, e As EventArgs) Handles BtnClient.MouseEnter
        BtnClient.BackColor = Color.FromArgb(255, 102, 0)
        BtnClient.ForeColor = Color.White
    End Sub

    Private Sub BtnClient_MouseLeave(sender As Object, e As EventArgs) Handles BtnClient.MouseLeave
        BtnClient.BackColor = Color.White
        BtnClient.ForeColor = Color.Indigo
    End Sub

    Private Sub BtnFournisseur_MouseEnter(sender As Object, e As EventArgs) Handles BtnFournisseur.MouseEnter
        BtnFournisseur.BackColor = Color.FromArgb(255, 102, 0)
        BtnFournisseur.ForeColor = Color.White
    End Sub

    Private Sub BtnFournisseur_MouseLeave(sender As Object, e As EventArgs) Handles BtnFournisseur.MouseLeave
        BtnFournisseur.BackColor = Color.White
        BtnFournisseur.ForeColor = Color.Indigo
    End Sub
End Class