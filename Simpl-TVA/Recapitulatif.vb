Public Class Recapitulatif
    Public per As String
    Private Sub Recapitulatif_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sqlSUMS As String = "SELECT  Designation AS [Désignation], TauxTVA AS [Taux TVA], " & _
            "CONVERT(DECIMAL(24, 2), ROUND(SUM(MontantTVA), 2)) AS [Montant TVA] " & per & " GROUP BY Designation, TauxTVA"
        DataGridViewT.DataSource = Read(sqlSUMS)
        DataGridViewT.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewT.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewT.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        gridStyle(DataGridViewT)
    End Sub
End Class