Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Public Class Rijndael

    Private Const Keysize As Integer = 256

    ' This constant determines the number of iterations for the password bytes generation function.
    Private Const DerivationIterations As Integer = 1000

    Public Shared Function Decrypt(cipherText As String, passPhrase As String) As String
        ' Get the complete stream of bytes that represent:
        ' [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
        Dim cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText)
        ' Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
        Dim saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray()
        ' Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
        Dim ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray()
        ' Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
        Dim cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray()

        Dim password = New Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations)
        Dim keyBytes = password.GetBytes(Keysize / 8)
        Using symmetricKey = New RijndaelManaged()
            symmetricKey.BlockSize = 256
            symmetricKey.Mode = CipherMode.CBC
            symmetricKey.Padding = PaddingMode.PKCS7
            Using decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes)
                Using memoryStream = New MemoryStream(cipherTextBytes)
                    Using cryptoStream = New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)
                        Dim plainTextBytes = New Byte(cipherTextBytes.Length - 1) {}
                        Dim decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)
                        memoryStream.Close()
                        cryptoStream.Close()
                        Return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)
                    End Using
                End Using
            End Using
        End Using
    End Function

    Private Shared Function Generate256BitsOfRandomEntropy() As Byte()
        Dim randomBytes = New Byte(31) {}
        ' 32 Bytes will give us 256 bits.
        Dim rngCsp = New RNGCryptoServiceProvider()
        ' Fill the array with cryptographically secure random bytes.
        rngCsp.GetBytes(randomBytes)
        Return randomBytes
    End Function


End Class
