Imports System.Security.Cryptography
Imports System.Text

Public Class CryptorTripleDES

    Public Shared Function Triple(key As String) As TripleDES
        Dim md5cryptor As MD5 = New MD5CryptoServiceProvider
        Dim TripleDEScryptor As TripleDES = New TripleDESCryptoServiceProvider
        TripleDEScryptor.Key = md5cryptor.ComputeHash(Encoding.Unicode.GetBytes(key))
        TripleDEScryptor.IV = New Byte(((TripleDEScryptor.BlockSize / 8)) - 1) {}
        Return TripleDEScryptor
    End Function

    Public Shared Function decrypt(value As String, key As String) As String
        Dim str2bytes() As Byte = Convert.FromBase64String(value)
        Dim cryptedKeyAsTripleDES As TripleDES = Triple(key)
        Dim decryptor As ICryptoTransform = cryptedKeyAsTripleDES.CreateDecryptor
        Dim output() As Byte = decryptor.TransformFinalBlock(str2bytes, 0, str2bytes.Length)
        Return Encoding.Unicode.GetString(output)
    End Function

End Class
