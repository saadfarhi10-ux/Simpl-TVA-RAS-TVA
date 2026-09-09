Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration
Imports System.Configuration.ConfigurationManager

Module GlobalModule
    Public acceuil_lecturedonnees, acceuil_traitement, acceuil_generation, acceuil_societesmaj, acceuil_usersmaj As Boolean
    Public curentUser As String
    Public gererLicence As Boolean = False
    Public modeConnexion As String = ""
    'Private cnxString = ConfigurationManager.ConnectionStrings("BaseSimplTVAConnectionString").ToString()

    Public cnx As SqlConnection = New SqlConnection(GetConnectionStrings())

    Function GetConnectionStrings(Optional onlyTest As Boolean = False) As String
        Dim result As String = ""
        Dim TestResult As String = ""
        Try
            Dim Server As String = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "SERVER")
            Dim User As String = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "USER")
            Dim Pwd As String = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "PWD")
            Dim BD As String = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "BD")
            Dim mode As Boolean = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "SECURITY")
            If mode Then
                result = "Data Source=" & Server & ";Initial Catalog=" & BD & ";Integrated Security=True" & ";"
                TestResult = "Data Source=" & Server & ";Integrated Security=True" & ";"
            Else
                result = "Data Source=" & Server & ";Initial Catalog=" & BD & ";integrated security=False;User Id=" & User & ";Password=" & Pwd & ";"
                TestResult = "Data Source=" & Server & ";integrated security=False;User Id=" & User & ";Password=" & Pwd & ";"
            End If

            If onlyTest Then
                result = TestResult
            End If
        Catch
            result = ""
        End Try
        Return result
    End Function

    'config.AppSettings.Settings.Item("ConnectionString").Value = "Data Source=blah;Initial Catalog=blah;UID=blah;password=blah"
    'config.Save(ConfigurationSaveMode.Modified)
    'ConfigurationManager.RefreshSection("AppSettings")


    Public cnxRoles As SqlConnection = cnx
    Public query As SqlCommand
    Public reader As SqlDataReader
    Public dtRoles As DataTable

    Sub Open()
        cnxRoles = New SqlConnection(GetConnectionStrings())
        Try
            If cnxRoles.State = ConnectionState.Closed Then
                cnxRoles.Open()
            End If
        Catch ex As Exception
            ServerParameters.ShowDialog()
        End Try
    End Sub

    Sub Close()
        If cnxRoles.State = ConnectionState.Open Then
            cnxRoles.Close()
        End If
    End Sub

    Public Function Read(script As String, Optional params() As SqlParameter = Nothing) As DataTable
        Open()
        dtRoles = New DataTable("tbl_result")
        query = New SqlCommand(script, cnxRoles)
        If params IsNot Nothing Then
            query.Parameters.AddRange(params)
        End If
        Try
            reader = query.ExecuteReader()
            dtRoles.Load(reader)
        Catch ex As Exception
            MessageBox.Show("Erreur, veuillez réessayer" & vbNewLine & ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        query.Parameters.Clear()
        Close()
        Return dtRoles
    End Function

    Function Execute(script As String, params() As SqlParameter) As Integer
        Dim rowsAffected As Integer = 0
        Open()
        query = New SqlCommand(script, cnxRoles)
        If params IsNot Nothing Then
            query.Parameters.AddRange(params)
        End If
        Try
            rowsAffected = query.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("erreur de mise à jour des données, veuillez réessayer" & vbNewLine & ex.Message)
        End Try
        query.Parameters.Clear()
        Close()
        Return rowsAffected
    End Function

    Function encodepwd(ByRef Psw As String) As String
        Dim c As Short
        Dim basec As Short
        Dim Ch As String
        Dim pw As String
        Dim j As Short
        Dim i As Short
        Dim m(8, 8) As Single
        Dim E(8) As Short
        Dim S(8) As Short
        Psw = LCase(Psw)
        For i = 1 To 8
            E(i) = 0
            For j = 1 To i
                m(i, j) = 1 / i
            Next j
            For j = i + 1 To 8
                m(i, j) = 0
            Next j
        Next i
        m(8, 1) = 1

        For i = 1 To Len(Psw)
            E(i) = Asc(Mid(Psw, i, 1))
        Next i
        For i = 1 To 8
            S(i) = Int(E(1) * m(i, 1) + E(2) * m(i, 2) + E(3) * m(i, 3) + E(4) * m(i, 4) + E(5) * m(i, 5) + E(6) * m(i, 6) + E(7) * m(i, 7) + E(8) * m(i, 8))
        Next i

        pw = ""
        For i = 1 To 8
            pw = pw & Chr(S(i))
        Next i
        Psw = pw

        Ch = "NOVATECH"
        pw = ""
        For i = 1 To Len(Psw)
            If (i Mod 2 <> 0) Then
                basec = 200
            Else
                basec = 0
            End If
            'pw$ = pw$ + Chr$(127 + Asc(Mid$(Psw$, i%, 1)) - basec% + (((-1) ^ (i% + 1)) * Asc(Mid$(ch$, i%, 1))))
            c = Asc(Mid(Psw, i, 1)) - basec + (((-1) ^ (i + 1)) * Asc(Mid(Ch, i, 1)))
            c = 40 + (c + 256) Mod 87
            pw = pw & Chr(c)
        Next i
        encodepwd = pw
    End Function
End Module