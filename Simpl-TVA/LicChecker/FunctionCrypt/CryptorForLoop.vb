Public Class CryptorForLoop

    Public Shared Function Encrypt(text As String, pw As String, type_of As Boolean) As String
        'Declaring variables
        Dim x As Integer
        Dim i As Integer
        Dim a As Integer
        Dim text_chr As String
        Dim text_asc As Integer
        Dim pw_chr As String
        Dim pw_asc As Integer
        Dim fin As String
        Dim fin_chr As String
        Dim fin_asc As Integer

        'if type_of = true then Encrypt
        'if type_of = False then Decrypt
        'Call percent(Picture1, 0) 'clear percent
        'making sure there is text to encrypt and a password
        'to go with it
        If Len(text$) = 0 Then Exit Function
        If Len(pw$) = 0 Then Exit Function

        x% = 1
        'the X variable is the loop that goes through the password
        'characters individually through the encrpytion processes
        'X = 1 to set the loop at the first character
        For i% = 1 To Len(text$) 'start of encrpyt loop
            'taking out characters from text to encrypt
            'the single character
            text_chr$ = Mid(text$, i, 1)
            'changing the character to its ASCII value to
            'easily change the character for encrypting
            text_asc% = Asc(text_chr$)
            'doing the same process with the password
            'using the X variable
            pw_chr$ = Mid(pw$, x, 1)
            pw_asc% = Asc(pw_chr$)
            'adding up variable to continue loop through different
            'characters within the password
            x% = x% + 1
            If x% > Len(pw$) Then x% = 1 'restarting password loop
            'Case to check if the user is Encrypting or Decrypting the text
            Select Case type_of
                Case True 'Encrypting
                    'adding the characters of both string and password
                    fin_asc% = text_asc% + pw_asc%
                    'making sure the final_asc will equal a valid ASCII character
                    If fin_asc% > 255 Then
                        'Character was an invalid character so we modify it
                        'to equal a valid character
                        a% = fin_asc% - 255
                        fin_chr$ = Chr(a%)
                    Else
                        'character was valid;D
                        fin_chr$ = Chr(fin_asc%)
                    End If
                Case False 'Decrypting
                    'here we subtract the characters...does the opposite of
                    'what encrypting does to put it back in its
                    'original state, which is why it's called Decrypting
                    fin_asc% = text_asc% - pw_asc% 'subtracting character values
                    If fin_asc% < 1 Then   'checking for invalid character
                        'invalid character..fixing problem =)
                        a% = fin_asc% + 255
                        fin_chr$ = Chr(a%)
                    Else
                        'it was all good.
                        fin_chr$ = Chr(fin_asc%)
                    End If
            End Select 'End of case
            'adding the final encrypted character to a string
            'to be later shown in its final state at the end
            fin$ = fin$ & fin_chr$
            'thought i'd be mr. fancy pants by adding a little
            'percentage bar =)
            'Call percent(Picture1, CInt((i / Len(text) * 100)))
        Next 'continuing loop =)
        'finalizing function to equal the final encrypted string
        Encrypt$ = fin$
        'DoEvents()
        'Call percent(Picture1, 0)
    End Function
End Class
