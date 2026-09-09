Imports System.Configuration
Imports System.Configuration.ConfigurationManager
Imports System.Xml
Imports System.Data.SqlClient

Public Class FrmLogin

    Dim parameters() As SqlParameter

    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim dd As DateTime = DateTime.Now.ToShortDateString()
        'MsgBox("Date : " & DateTime.Now & " ; short " & dd.ToBinary() & " date : " & DateTime.Now.ToBinary())
        Open()
        'Changement du classement(Collation) de la bdd{
        Dim Currentdb As String = GetSetting("PROGICIELSYSTEM", "SIMPLETVA", "BD")
        Dim CurrentdbSel As String = "SELECT ISNULL(collation_name, '-') FROM sys.databases WHERE name = '" + Currentdb + "' "
        CurrentdbSel = Read(CurrentdbSel).Rows(0)(0).ToString()
        If CurrentdbSel <> "French_CI_AS" Then
            Dim ssqlAlter As String = "ALTER DATABASE [" + Currentdb + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;" &
                " ALTER DATABASE [" + Currentdb + "] SET SINGLE_USER;" &
                " ALTER DATABASE [" + Currentdb + "] COLLATE French_CI_AS;" &
                " ALTER DATABASE [" + Currentdb + "] SET MULTI_USER;"
            Execute(ssqlAlter, Nothing)
        End If

        Dim SsqlDateToDateTime As String = "" &
            "ALTER TABLE [dbo].[EnteteGeneration] ALTER COLUMN [Datecreation] DATETIME;" &
            "ALTER TABLE [dbo].[EnteteGeneration] ALTER COLUMN [Datemodification] DATETIME;" &
                    "" &
            "ALTER TABLE [dbo].[LigneGeneration] ALTER COLUMN [Datefacture] DATETIME;" &
            "ALTER TABLE [dbo].[LigneGeneration] ALTER COLUMN [Datepaiement] DATETIME;" &
                    "" &
            "ALTER TABLE [dbo].[syslicenceinfo] ALTER COLUMN [previousRD] DATETIME;" &
                    "" &
            "ALTER TABLE [dbo].[users] ALTER COLUMN [userCrearedDate] DATETIME;" &
            "ALTER TABLE [dbo].[users] ALTER COLUMN [userLastLoginDate] DATETIME;"

        Execute(SsqlDateToDateTime, Nothing)

        CurrentdbSel = "SELECT TOP 1 * FROM [Societe]"
        Dim updateDBnewSchema As String = ""
        If Not Read(CurrentdbSel).Columns.Contains("regime") Then
            updateDBnewSchema = "ALTER TABLE Societe ADD regime varchar(23);"
        End If
        If Read(CurrentdbSel).Columns.Contains("PRORATA") Then
            updateDBnewSchema &= "ALTER TABLE Societe DROP COLUMN PRORATA;"
        End If
        If updateDBnewSchema <> "" Then
            Execute(updateDBnewSchema, Nothing)
        End If
        '}Changement du classement(Collation) de la bdd
        LabelExpirationMsg.Text = ""
        TrialLicCheck.ShowDialog()
        LabelExpirationMsg.Text = msgExpiration
        Username.Text = GetSetting("PROGICIELSYSTEM", "Simple_Tva", "TmpLogin", "")
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        parameters = New SqlParameter() _
        {
            New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = MachineSysID}
        }
        Dim LicenceData As DataTable = Read("SELECT " &
          "[systemeid] ," &
          "[societe] ," &
          "[logiciel] ," &
          "[date_exp] ," &
          "[opts] ," &
          "[nbr_soc] ," &
          "[previousRD] " &
        "FROM [syslicenceinfo] WHERE [systemeid] = @P2 ", parameters)

        Select Case LicenceData.Rows.Count
            Case Is > 0

                Try
                    parameters = New SqlParameter() _
                    {
                        New SqlParameter("@P1", SqlDbType.DateTime, 50) With {.Value = Date.Now},
                        New SqlParameter("@P2", SqlDbType.VarChar, 255) With {.Value = Username.Text}
                    }

                    Dim sql As String = "SELECT [userState], [userPassword] FROM  [users] WHERE LTRIM(RTRIM([userLogOn])) LIKE @P2 "
                    Dim loginResult As DataTable = Read(sql, parameters)
                    If loginResult.Rows.Count > 0 Then
                        ERREUR.Visible = False
                        Dim pwdEncode As String = encodepwd(Trim(LCase(Password.Text)))
                        Dim pwdDb As String = loginResult.Rows(0)("userPassword").ToString()
                        Dim userState As String = loginResult.Rows(0)("userState").ToString()

                        If userState.Equals("0") Then
                            MsgBox("Utilisateur : " & Username.Text & " désactiver!")
                            Application.Exit()
                        End If
                        Dim MUSER As String = UCase(Trim(Username.Text))
                        If pwdDb = pwdEncode Then
                            curentUser = MUSER
                            Execute("UPDATE [users] SET [userLastLoginDate] = @P1 WHERE LTRIM(RTRIM([userLogOn])) LIKE @P2 ", parameters)
                            SaveSetting("PROGICIELSYSTEM", "Simple_Tva", "TmpLogin", Trim(Username.Text))
                            If curentUser.ToLower() <> "admin" And curentUser.ToLower() <> "administrateur" Then
                                LoadRoles(curentUser)
                            Else
                                Dim societeIDS As DataTable = Read("SELECT Idsociete FROM Societe ")
                                Dim socitiesRights As String = ""
                                For x = 0 To societeIDS.Rows.Count - 1
                                    socitiesRights &= "," & societeIDS.Rows(x)(0).ToString()
                                Next
                                socitiesRights = socitiesRights.TrimEnd(socitiesRights, ",")
                                socitiesRights = socitiesRights.TrimStart(socitiesRights, ",")

                                acceuil_lecturedonnees = True
                                acceuil_traitement = True
                                acceuil_generation = True
                                acceuil_societesmaj = True
                                acceuil_usersmaj = True
                                linkedsocities = socitiesRights
                            End If
                            Dim frmMode As New FrmModeConnexion()
                            If frmMode.ShowDialog() = DialogResult.OK Then
                                Dim frmHome As SimplTVA = New SimplTVA()
                                frmHome.Show()
                                Me.Hide()
                            End If
                        Else
                            ERREUR.Visible = True
                        End If
                    Else
                        ERREUR.Visible = True
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical)
                    Application.OpenForms.Item(0).Close()
                End Try

            Case Else
                TrialLicCheck.ShowDialog()
        End Select
    End Sub

    Public Sub LoadRoles(logon As String)
        parameters = New SqlParameter() _
        {
            New SqlParameter("@P1", SqlDbType.VarChar, 255) With {.Value = logon}
        }
        Dim filterResult As DataTable = New DataTable()
        filterResult = Read("SELECT users.[userLogOn], rights.* FROM users JOIN rights ON rights.[userID] = users.[userID] WHERE users.[userLogOn] = @P1 ", parameters)
        If filterResult.Rows.Count >= 1 Then
            ''Start Checking
            acceuil_lecturedonnees = If(filterResult.Rows(0)("acceuil_lecturedonnees").ToString().Equals("1"), True, False)
            acceuil_traitement = If(filterResult.Rows(0)("acceuil_traitement").ToString().Equals("1"), True, False)
            acceuil_generation = If(filterResult.Rows(0)("acceuil_generation").ToString().Equals("1"), True, False)
            acceuil_societesmaj = If(filterResult.Rows(0)("acceuil_societesmaj").ToString().Equals("1"), True, False)
            acceuil_usersmaj = If(filterResult.Rows(0)("acceuil_usersmaj").ToString().Equals("1"), True, False)
            linkedsocities = filterResult.Rows(0)("linkedsocities").ToString()
            curentUser = filterResult.Rows(0)("userLogOn").ToString()
            ''End Checking
        End If
    End Sub



    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Application.Exit()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ServerParameters.ShowDialog()
    End Sub
End Class