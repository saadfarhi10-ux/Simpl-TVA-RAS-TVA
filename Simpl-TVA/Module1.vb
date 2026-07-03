Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Module Module1

    'Private cnxString = ConfigurationManager.ConnectionStrings("BaseSimplTVAConnectionString").ToString()
    'Public cnx As SqlClient.SqlConnection = New SqlClient.SqlConnection(cnxString)

    Public Sub gridStyle(g As DataGridView)
        g.ForeColor = Color.Black
        g.AlternatingRowsDefaultCellStyle.BackColor = Color.GhostWhite
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.AllowUserToAddRows = False
        g.RowHeadersVisible = False
    End Sub
End Module
