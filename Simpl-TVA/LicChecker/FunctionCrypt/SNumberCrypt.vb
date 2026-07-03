Imports System.Management
Imports System.Net.NetworkInformation

Module SNumberCrypt

    Function GetSNumber() As String
        Dim q As New SelectQuery("Win32_bios")
        Dim search As New ManagementObjectSearcher(q)
        Dim info As New ManagementObject
        Dim serialnumber As String = ""
        For Each info In search.Get
            serialnumber &= info("serialnumber").ToString()
        Next
        Return serialnumber
    End Function

    Function encodeSerialNumber(serial As String) As String
        Dim encoded As String = ""
        Dim counter As Integer = 0
        Dim tryParse As Integer
        Dim letters() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"}
        serial = serial.ToUpper()
        For Each item As Char In serial
            If Integer.TryParse(item, tryParse) = False Then
                encoded &= letters(counter) & Asc(item)
                counter += 1
            Else
                encoded &= item
            End If
        Next
        Return encoded
    End Function

    Function decodeSerialNumber(serialEncoded As String) As String
        Dim decoded As String = ""
        Dim tryParse As Integer
        serialEncoded = serialEncoded.ToUpper()
        For index As Integer = 1 To serialEncoded.Length
            If Integer.TryParse(Mid(serialEncoded, index, 1), tryParse) = False Then
                decoded &= Chr(CInt(Mid(serialEncoded, index + 1, 2)))
                index += 2
            Else
                decoded &= Mid(serialEncoded, index, 1)
            End If
        Next

        Return decoded
    End Function

End Module
