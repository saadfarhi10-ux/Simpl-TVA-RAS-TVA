Imports System.Data.SqlClient
Imports System.IO

Public Class ServerParameters
    Dim connectionSuccess As Boolean

    Private Sub ServerParameters_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connectionSuccess = True
        Btn_Restart.Visible = False
        Me.Size = New Size(359, 310)
        TxtServer.Text = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "SERVER")
        TxtUser.Text = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "USER")
        TxtPwd.Text = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "PWD")
        TxtBase.Text = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "BD")
        Dim mode = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "SECURITY")
        If mode.Equals("True") Then
            RadioButtonWIN.Checked = True
        Else
            RadioButtonSQL.Checked = True
        End If
    End Sub

    Private Sub CmdConfirmer_Click(sender As Object, e As EventArgs) Handles CmdConfirmer.Click

        If RadioButtonSQL.Checked Then
            If TxtServer.Text.Equals("") Or TxtUser.Text.Equals("") Or TxtPwd.Text.Equals("") Or TxtBase.Text.Equals("") Then
                MsgBox("Veuillez remplir les informations de connexion")
                Exit Sub
            End If
        ElseIf RadioButtonWIN.Checked Then
            If TxtServer.Text.Equals("") Or TxtBase.Text.Equals("") Then
                MsgBox("Veuillez remplir les informations de connexion")
                Exit Sub
            End If
        End If

        SaveSetting("PROGICIELSYSTEM", "SIMPLETVA", "SERVER", Trim(TxtServer.Text))
        SaveSetting("PROGICIELSYSTEM", "SIMPLETVA", "USER", Trim(TxtUser.Text))
        SaveSetting("PROGICIELSYSTEM", "SIMPLETVA", "PWD", Trim(TxtPwd.Text))
        SaveSetting("PROGICIELSYSTEM", "SIMPLETVA", "BD", Trim(TxtBase.Text))
        SaveSetting("PROGICIELSYSTEM", "SIMPLETVA", "SECURITY", RadioButtonWIN.Checked)
        'GetConnectionStrings(True) seuelement retourne la chaine sans la base de donnés
        Dim cnxTMP As SqlConnection = New SqlConnection(GetConnectionStrings(True))
        'MsgBox(GetConnectionStrings(True))
        Try
            cnxTMP.Open()
            Dim cmdTmp As SqlCommand = New SqlCommand("SELECT * FROM sys.databases WHERE Name = '" & TxtBase.Text & "' ", cnxTMP)
            Dim rearderTmp As SqlDataReader = cmdTmp.ExecuteReader()
            If rearderTmp.HasRows Then
                'Base de données existante
                'rearderTmp.Read()
                'MsgBox(rearderTmp(0).ToString())
            Else
                '{Création de la BDD
                If MessageBox.Show("Base de données introuvable lancer l'installation ?", "Installation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                    MessageBox.Show("Installation de la base de données ...", "Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Dim FileName As String
                    Dim sqlInstallDB As String
                    rearderTmp.Close()
                    cmdTmp.Dispose()
                    Try
                        cmdTmp = New SqlCommand("CREATE DATABASE [" & TxtBase.Text & "] COLLATE French_CI_AS", cnxTMP)
                        cmdTmp.ExecuteNonQuery()
                        cmdTmp.Dispose()
                        'Try
                        '    cmdTmp = New SqlCommand("ALTER DATABASE [" & TxtBase.Text & "] SET COMPATIBILITY_LEVEL = 100 GO ", cnxTMP)
                        '    cmdTmp.ExecuteNonQuery()
                        'Catch
                        '    cmdTmp = New SqlCommand("ALTER DATABASE [" & TxtBase.Text & "] SET COMPATIBILITY_LEVEL = 90 ", cnxTMP)
                        '    cmdTmp.ExecuteNonQuery()
                        'End Try
                        
                        FileName = Application.StartupPath & "/QueryInstallDBContent.txt"
                        sqlInstallDB = File.ReadAllText(FileName)
                        sqlInstallDB = sqlInstallDB.Replace("DATABASE_NAME", TxtBase.Text)
                        sqlInstallDB = sqlInstallDB.Replace("GO", "--GO")
                        cmdTmp.Dispose()
                        cmdTmp = New SqlCommand(sqlInstallDB, cnxTMP)
                        cmdTmp.ExecuteNonQuery()
                        MessageBox.Show("Installation de la base de données terminée", "Installation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Catch ex As Exception
                        MessageBox.Show("Erreur de création de la base de données!" & vbNewLine & ex.Message, "Base de données", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                End If
                '}Création de la BDD
            End If
            cnxTMP.Close()

            cnx = New SqlConnection(GetConnectionStrings())
            cnxRoles = New SqlConnection(GetConnectionStrings())
            MessageBox.Show("Connexion avec succès", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Btn_Restart.Visible = True
            Me.Size = New Size(Me.Width, 364)
        Catch ex As Exception
            connectionSuccess = False
            MessageBox.Show("Connexion echoué merci de vérifier les informations de connexion! " & vbNewLine & ex.Message, "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        cnxTMP.Dispose()
    End Sub

    Private Sub RadioButtonWIN_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonWIN.CheckedChanged
        CheckedChanged()
    End Sub

    Private Sub RadioButtonSQL_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonSQL.CheckedChanged
        CheckedChanged()
    End Sub

    Sub CheckedChanged()
        If RadioButtonWIN.Checked Then
            TxtUser.Enabled = False
            TxtPwd.Enabled = False
        Else
            TxtUser.Enabled = True
            TxtPwd.Enabled = True
        End If
    End Sub

    Private Sub Btn_Restart_Click(sender As Object, e As EventArgs) Handles Btn_Restart.Click
        Me.Close()
    End Sub

    Private Sub ServerParameters_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim cnxTMP As SqlConnection = New SqlConnection(GetConnectionStrings(True))
        Try
            cnxTMP.Open()
            cnxTMP.Close()
        Catch ex As Exception
            If MessageBox.Show("Connexion echoué, fermer quand même ?" & vbNewLine & ex.Message, "Connexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        End Try
    End Sub
End Class