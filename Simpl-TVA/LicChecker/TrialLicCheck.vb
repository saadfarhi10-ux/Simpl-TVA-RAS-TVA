Imports System.IO
Imports System.Data.SqlClient
Imports System.Reflection

Public Class TrialLicCheck
    Dim token As String

    'Sub InsertIntoRegedit()
    '    SaveSetting("PROGICIELSYSTEM_SimpleTVA", "Simple_Tva", "frstRD", DateTime.Now)
    '    SaveSetting("PROGICIELSYSTEM_SimpleTVA", "Simple_Tva", "prevRD", DateTime.Now)
    '    SaveSetting("PROGICIELSYSTEM_SimpleTVA", "Simple_Tva", "isTrial", 1)
    '    SaveSetting("PROGICIELSYSTEM_SimpleTVA", "Simple_Tva", "isTrialD", 0)
    '    SaveSetting("PROGICIELSYSTEM_SimpleTVA", "Simple_Tva", "prevRD", DateTime.Now)

    '    My.Settings.curDate = Date.Now()
    '    My.Settings.prevDate = My.Settings.curDate
    '    My.Settings.isTrial = 1
    '    My.Settings.isTrialD = 0
    '    My.Settings.Save()
    'End Sub

    Sub ListLicInfo(systemeid As String, societe As String, logiciel As String, date_exp As DateTime, opts As String, nbr_soc_allowed As String)
        ListBox1.Items.Clear()
        ListBox1.Items.Add("Systéme ID :" & systemeid)
        ListBox1.Items.Add("Client :" & societe)
        ListBox1.Items.Add("Logiciel :" & logiciel)
        ListBox1.Items.Add("Date Expiration :" & date_exp)


        Dim Splitoptions() As String = opts.Split(",")
        Dim cpllectOptions As String = ""
        For Each index As String In Splitoptions
            cpllectOptions &= index & ","
        Next
        cpllectOptions = cpllectOptions.TrimEnd(",")
        ListBox1.Items.Add("Options :" & cpllectOptions)
        Dim nbrSocId As Integer = Integer.Parse(nbr_soc_allowed)
        Dim nbr_soc As String = ""
        Select Case nbrSocId
            Case 1
                nbr_soc = "1-5"
            Case 2
                nbr_soc = "5-10"
            Case 3
                nbr_soc = "+10"
            Case Else
        End Select
        ListBox1.Items.Add("Nombre de sociétés :" & nbr_soc)
    End Sub

    Private Sub TrialLicCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Open()
        ButtonOpenOrContnue.BackgroundImage = My.Resources.open_file
        ButtonOpenOrContnue.Text = "Parcourir ..."

        Dim versionNumber As Version
        versionNumber = Assembly.GetExecutingAssembly().GetName().Version
        ToolStripStatusLabel1.Text = "Version logiciel : " & versionNumber.ToString() & " "

        MachineSysID = encodeSerialNumber(GetSNumber())
        token = MachineSysID

        Dim LicenceData As DataTable = Read("SELECT " & _
          "[systemeid] ," & _
          "[societe] ," & _
          "[logiciel] ," & _
          "[date_exp] ," & _
          "[opts] ," & _
          "[nbr_soc] ," & _
          "[previousRD] " & _
        "FROM [syslicenceinfo] WHERE [systemeid] = '" & MachineSysID & "' ")
        If LicenceData.Rows.Count <= 0 Then
            ButtonOpenOrContnue.BackgroundImage = My.Resources.open_file
            ButtonOpenOrContnue.Text = "Parcourir ..."
        Else
            systemeid = LicenceData.Rows(0)("systemeid").ToString()
            societe = LicenceData.Rows(0)("societe").ToString()
            logiciel = LicenceData.Rows(0)("logiciel").ToString()
            date_exp = DateTime.FromBinary(LicenceData.Rows(0)("date_exp").ToString()).ToShortDateString()
            opts = LicenceData.Rows(0)("opts").ToString()
            nbr_soc_allowed = LicenceData.Rows(0)("nbr_soc").ToString()

            '{Ajout des élément à la listBox
            ListLicInfo(systemeid, societe, logiciel, date_exp, opts, nbr_soc_allowed)
            '}Ajout des élément à la listBox
            Dim previousRunDate As DateTime = DateTime.Parse(LicenceData.Rows(0)("previousRD").ToString())

            If DateTime.Now().ToShortDateString() < previousRunDate Then
                'MsgBox("date minimiser")
            End If

            If date_exp <= DateTime.Now().ToShortDateString() Then
                If MessageBox.Show("votre licence a expiré, renouvelé", "Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation).Equals(DialogResult.Yes) Then
                    ButtonOpenOrContnue.BackgroundImage = My.Resources.open_file
                    ButtonOpenOrContnue.Text = "Parcourir ..."
                Else
                    Application.Exit()
                End If
            Else
                If date_exp.Subtract(DateTime.Now).TotalDays() > 2 Then
                    msgExpiration = "Expériration dans : " & Math.Round(date_exp.Subtract(DateTime.Now).TotalDays()) & " jours"
                Else
                    msgExpiration = "Expériration dans : " & Math.Round(date_exp.Subtract(DateTime.Now).TotalHours()) & " heures"
                End If

                ButtonOpenOrContnue.BackgroundImage = My.Resources.go_next
                ButtonOpenOrContnue.Text = "Continuer ..."
                If gererLicence = False Then
                    Me.Close()
                End If
            End If

            Dim parameters() As SqlParameter = New SqlParameter() _
            {
                New SqlParameter("@P1", SqlDbType.DateTime, 50) With {.Value = DateTime.Now().ToShortDateString()},
                New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = MachineSysID}
            }

            Dim sqlQuery As String = "UPDATE [syslicenceinfo] SET [previousRD] = @P1 WHERE [systemeid] = @P2 "
            Execute(sqlQuery, parameters)

        End If
        If gererLicence = True Then
            ButtonOpenOrContnue.BackgroundImage = My.Resources.open_file
            ButtonOpenOrContnue.Text = "Parcourir ..."
        End If
    End Sub

    Private Sub ButtonOpenOrContnue_Click(sender As Object, e As EventArgs) Handles ButtonOpenOrContnue.Click
        If ButtonOpenOrContnue.Text.Equals("Parcourir ...") Then
            Dim OpenFileDialogLic As OpenFileDialog = New OpenFileDialog()
            OpenFileDialogLic.Filter = "Fichier licence(*.lic)|*.lic|Tous les fichiers(*.*)|*.*"
            OpenFileDialogLic.InitialDirectory = Environment.SpecialFolder.Desktop
            OpenFileDialogLic.RestoreDirectory = True

            If OpenFileDialogLic.ShowDialog().Equals(DialogResult.OK) Then
                loadLicenceFromFile(OpenFileDialogLic.FileName)
            End If
        Else
            Me.Close()
        End If
    End Sub

    Sub loadLicenceFromFile(FileName As String)
        Dim fileExt = Path.GetExtension(FileName)
        Dim statutMSG As String = ""
        If fileExt.Equals(".lic") Then

            Dim crypted As String = File.ReadAllText(FileName)
            Dim SpitCryptMethodFromCrypted() As String = crypted.Split(";")
            Dim cryptMethod As Integer = Integer.Parse(SpitCryptMethodFromCrypted(1))
            crypted = SpitCryptMethodFromCrypted(0)
            Dim decrypted As String = ""
            Try
                Select Case cryptMethod
                    Case 1
                        'Cryptage MD5CryptoServiceProvider
                        decrypted = CryptorTripleDES.decrypt(crypted, token)
                    Case 2
                        'Cryptage Custom using For Loop
                        decrypted = CryptorForLoop.Encrypt(crypted, token, False)
                    Case 3
                        'Cryptage Rijndael
                        decrypted = Rijndael.Decrypt(crypted, token)
                    Case Else
                        decrypted = CryptorForLoop.Encrypt(crypted, token, False)
                End Select
            Catch ex As Exception
                MsgBox("Cette licence n'appartient pas à cette machine")
            End Try
            'A70B90C808D81E801-Cli-Sage-07/01/2017-1,5-2
            Dim split() As String = decrypted.Split(";")
            Dim systemeid As String = split(0)
            If Not decodeSerialNumber(token).Equals(decodeSerialNumber(systemeid)) Then
                MsgBox("Cette licence n'appartient pas à cette machine")
                Exit Sub
            End If
            Dim societe As String = split(1)
            Dim logiciel As String = split(2)
            Dim date_expToBinary As String = DateTime.Parse(split(3)).ToBinary.ToString()
            Dim date_exp As DateTime = DateTime.FromBinary(date_expToBinary).ToShortDateString()
            Dim opts As String = ""
            Dim nbr_soc As String = ""
            If date_exp <= DateTime.Now Then
                If MessageBox.Show("votre licence a expiré, renouvelé", "Activation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation).Equals(DialogResult.Yes) Then
                    ButtonOpenOrContnue.BackgroundImage = My.Resources.open_file
                    ButtonOpenOrContnue.Text = "Parcourir ..."
                Else
                    Application.Exit()
                End If
            End If
            Dim nbrSocId As Integer = Integer.Parse(split(5))

            '{Ajout des élément à la listBox
            ListLicInfo(systemeid, societe, logiciel, date_exp, split(4), split(5))
            '}Ajout des élément à la listBox

            If date_exp.Subtract(DateTime.Now).TotalDays() > 2 Then
                msgExpiration = "Expériration dans : " & Math.Round(date_exp.Subtract(DateTime.Now).TotalDays()) & " jours"
            Else
                msgExpiration = "Expériration dans : " & Math.Round(date_exp.Subtract(DateTime.Now).TotalHours()) & " heures"
            End If

            statutMSG = "Fichier : " & FileName

            Dim sqlQuery As String = ""
            Dim result As Integer = 0
            Dim attachParams() As SqlParameter = New SqlParameter() _
                { _
                    New SqlParameter("systemeid", SqlDbType.VarChar) With {.Value = systemeid}, _
                    New SqlParameter("societe", SqlDbType.VarChar) With {.Value = societe}, _
                    New SqlParameter("logiciel", SqlDbType.VarChar) With {.Value = logiciel},
                    New SqlParameter("date_exp", SqlDbType.VarChar) With {.Value = date_expToBinary},
                    New SqlParameter("opts", SqlDbType.VarChar) With {.Value = opts}, _
                    New SqlParameter("nbr_soc", SqlDbType.Int) With {.Value = nbrSocId}, _
                    New SqlParameter("previousRD", SqlDbType.DateTime) With {.Value = DateTime.Now.ToShortDateString()}
                }

            Dim dataCheckExistant As DataTable = Read("SELECT * FROM [syslicenceinfo] WHERE [systemeid] = '" & systemeid & "' ")
            If dataCheckExistant.Rows.Count > 0 Then
                '{modification
                If dataCheckExistant.Rows(0)("societe").ToString().Equals(societe) And _
                    dataCheckExistant.Rows(0)("logiciel").ToString().Equals(logiciel) And _
                    DateTime.FromBinary(dataCheckExistant.Rows(0)("date_exp").ToString()) = (date_exp) And _
                    dataCheckExistant.Rows(0)("opts").ToString().Equals(opts) And _
                    dataCheckExistant.Rows(0)("nbr_soc").ToString().Equals(nbrSocId.ToString()) _
                 Then
                    'DateTime.FromBinary(date_expToBinary).ToShortDateString()
                    MsgBox("Cette licence est en cours de traitement")
                    Exit Sub
                End If
                sqlQuery = "UPDATE [syslicenceinfo] SET [societe] = @societe , [logiciel] = @logiciel , [date_exp] =  @date_exp, [opts] =  @opts, [nbr_soc] =  @nbr_soc , [previousRD] =  @previousRD " & _
                                             " WHERE [systemeid] = @systemeid "
                result = Execute(sqlQuery, attachParams)
                If result > 0 Then
                    statutMSG &= " | mise à jour des données avec succès"
                Else
                    statutMSG &= " | mise à jour des données non effectuer"
                End If
                '}modification
            Else
                'ajout
                sqlQuery = "INSERT INTO [syslicenceinfo] ([systemeid], [societe], [logiciel], [date_exp], [opts], [nbr_soc], [previousRD]) " & _
                                                " VALUES ( @systemeid, @societe, @logiciel, @date_exp, @opts, @nbr_soc, @previousRD)"

                result = Execute(sqlQuery, attachParams)
                
                'ajout
            End If
            If result > 0 Then
                statutMSG &= " | mise à jour des données avec succès"
                ButtonOpenOrContnue.BackgroundImage = My.Resources.go_next
                ButtonOpenOrContnue.Text = "Continuer ..."
            Else
                statutMSG &= " | mise à jour des données non effectuer"
            End If


        Else
            statutMSG = "Fichier : " & FileName & " n'est pas valid!"
        End If
        ToolStripStatusLabel1.Text &= statutMSG
    End Sub

    Private Sub Panel1_DragDrop(sender As Object, e As DragEventArgs) Handles Panel1.DragDrop
        If ButtonOpenOrContnue.Text.Equals("Parcourir ...") Then
            Panel1.BackgroundImage = Nothing
            ListBox1.Visible = True
            ButtonOpenOrContnue.Visible = True

            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
            Dim src As String = ""
            For Each path In files
                src = path
            Next
            loadLicenceFromFile(src)
        End If

    End Sub

    Private Sub Panel1_DragEnter(sender As Object, e As DragEventArgs) Handles Panel1.DragEnter
        If ButtonOpenOrContnue.Text.Equals("Parcourir ...") Then
            ListBox1.Visible = False
            ButtonOpenOrContnue.Visible = False
            Panel1.BackgroundImage = My.Resources.Drag_Drop
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effect = DragDropEffects.Copy
            End If
        End If
    End Sub
End Class