Imports System.Data.SqlClient
Imports System.Data

Public Class FrmUsersUp
    Dim errorsSum As String = ""
    Dim id As Integer = 0
    Dim sqlQuery As String

    Sub loadGrid()
        DataGridView1.DataSource = Read("SELECT " & _
          "LTRIM(RTRIM([userLogOn])) AS [Login] " & _
          ",LTRIM(RTRIM([userLName])) AS [Nom] " & _
          ",LTRIM(RTRIM([userFName])) AS [Prénom] " & _
          ",[userLastLoginDate] AS [Dernière connexion] " & _
          ",[userCrearedDate] AS [Date création] " & _
          ",CASE  [userState] WHEN 1 THEN 'Actif' WHEN 0 THEN 'Verrouillé' end AS [Etat] " & _
        "FROM [users] WHERE LTRIM(RTRIM([userLogOn])) NOT LIKE 'admin%' ORDER BY [userID] DESC")
    End Sub

    Function ValidateFRM() As Boolean
        Dim thereIsError As Boolean = False
        If TextBoxUN.Text.Equals(String.Empty) Then
            thereIsError = True
            errorsSum = "Veuillez remplir toutes les informations"
        End If
        If TextBoxLname.Text.Equals(String.Empty) Then
            thereIsError = True
            errorsSum = "Veuillez remplir toutes les informations"
        End If
        If TextBoxFname.Text.Equals(String.Empty) Then
            thereIsError = True
            errorsSum = "Veuillez remplir toutes les informations"
        End If
        If TextBoxPass.Text.Equals(String.Empty) Then
            thereIsError = True
            errorsSum = "Veuillez remplir toutes les informations"
        ElseIf TextBoxPass.Text <> TextBoxPassConfirm.Text Then
            thereIsError = True
            errorsSum = "Confirmation mot de passe invalid"
        End If
        If RadioButtonSA.Checked = False And RadioButtonSV.Checked = False Then
            thereIsError = True
            errorsSum = "Veuillez remplir toutes les informations"
        End If
        Return thereIsError
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        gridStyle(DataGridView1)
        loadGrid()
        Dim socitiesDATA As DataTable = Read("SELECT [Idsociete], [RaisonSociale] FROM [Societe]")
        ListBox1.DataSource = socitiesDATA
        ListBox1.DisplayMember = "RaisonSociale"
        ListBox1.ValueMember = "Idsociete"
        For x = 0 To ListBox1.Items.Count - 1
            ListBox1.SetSelected(x, False)
        Next
        ToolStripStatusLabelVal.Text = Read("SELECT COUNT(*) AS COUNT_USERS FROM [users] WHERE users.userLogOn <> 'administrateur'").Rows(0)(0).ToString() & " éléments "
    End Sub

    Private Sub Btn_Submit_Click(sender As Object, e As EventArgs) Handles Btn_Submit.Click
        Dim thereIsError As Boolean = ValidateFRM()
        Dim msgState As String

        If thereIsError Then
            ToolStripStatusLabelVal.Text = errorsSum
            MessageBox.Show(errorsSum, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim socitiesRights As String = ""
            For x = 0 To ListBox1.Items.Count - 1
                If ListBox1.GetSelected(x) Then
                    socitiesRights &= "," & ListBox1.Items(x).Row.ItemArray(0)
                End If
            Next
            socitiesRights = socitiesRights.TrimEnd(socitiesRights, ",")
            socitiesRights = socitiesRights.TrimStart(socitiesRights, ",")

            Dim passRef As String = TextBoxPass.Text
            Dim state As Integer = 0
            If RadioButtonSA.Checked Then
                state = 1
            End If
            If RadioButtonSV.Checked Then
                state = 0
            End If
            If Read("SELECT * FROM [users] WHERE [userLogOn] = '" & TextBoxUN.Text & "' ").Rows.Count > 0 Then
                'utilisateur existant, modification
                'modification
                Dim attachParams() As SqlParameter = New SqlParameter() _
                { _
                    New SqlParameter("userFName", SqlDbType.VarChar) With {.Value = TextBoxFname.Text}, _
                    New SqlParameter("userLName", SqlDbType.VarChar) With {.Value = TextBoxLname.Text}, _
                    New SqlParameter("userPassword", SqlDbType.VarChar) With {.Value = encodepwd(passRef)},
                    New SqlParameter("userState", SqlDbType.SmallInt) With {.Value = state},
                    New SqlParameter("userID", SqlDbType.Int) With {.Value = id}
                }


                sqlQuery = "UPDATE [users] SET [userFName] = @userFName , " & _
                                             " [userLName] = @userLName , " & _
                                             " [userState] =  @userState " & _
                                             " WHERE [userID] = @userID "
                If TextBoxPass.Text <> "******" And TextBoxPass.Text <> String.Empty Then
                    sqlQuery = "UPDATE [users] SET [userFName] = @userFName , " & _
                                                "  [userLName] = @userLName , " & _
                                                "  [userPassword] = @userPassword , " & _
                                                "  [userState] =  @userState " & _
                                                " WHERE [userID] = @userID "
                End If

                Dim result As Integer = Execute(sqlQuery, attachParams)
                If result > 0 Then
                    Execute("UPDATE [rights] SET [linkedsocities] = '" & socitiesRights & "'  WHERE [userID] = " & id, Nothing)
                    msgState = "Opération effectuée avec succès | Modification"
                Else
                    msgState = "Opération Échoué, réessayer"
                End If
                'modification
            Else
                'utilisateur no existant, ajout
                'ajout

                Dim attachParams() As SqlParameter = New SqlParameter() _
                { _
                    New SqlParameter("userLogOn", SqlDbType.VarChar) With {.Value = TextBoxUN.Text}, _
                    New SqlParameter("userFName", SqlDbType.VarChar) With {.Value = TextBoxFname.Text}, _
                    New SqlParameter("userLName", SqlDbType.VarChar) With {.Value = TextBoxLname.Text}, _
                    New SqlParameter("userPassword", SqlDbType.VarChar) With {.Value = encodepwd(passRef)}, _
                    New SqlParameter("userCrearedDate", SqlDbType.Date) With {.Value = Date.Now()}, _
                    New SqlParameter("userState", SqlDbType.SmallInt) With {.Value = state} _
                }
                sqlQuery = "INSERT INTO [users] ([userLogOn], " & _
                                                " [userFName]," & _
                                                " [userLName]," & _
                                                " [userPassword]," & _
                                                " [userCrearedDate]," & _
                                                " [userState]) " & _
                                                " VALUES ( " & _
                                                " @userLogOn, " & _
                                                " @userFName, " & _
                                                " @userLName, " & _
                                                " @userPassword, " & _
                                                " @userCrearedDate, " & _
                                                " @userState)"

                Dim result As Integer = Execute(sqlQuery, attachParams)
                If result > 0 Then
                    Dim nextRole As Integer = Integer.Parse(Read("SELECT MAX([userID]) as [NextID] FROM [users]").Rows(0)(0).ToString())
                    id = nextRole
                    Execute("INSERT INTO [rights]([userID], [linkedsocities]) VALUES('" & nextRole & "', '" & socitiesRights & "')", Nothing)
                    msgState = "Opération effectuée avec succès | Ajout"
                Else
                    msgState = "Opération Échoué, réessayer"
                End If
                'ajout
            End If
            ToolStripStatusLabelVal.Text = msgState
        End If
        loadGrid()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        For x = 0 To ListBox1.Items.Count - 1
            ListBox1.SetSelected(x, False)
        Next
        If e.RowIndex <> -1 Then
            id = Integer.Parse(Read("SELECT [userID] FROM [users] WHERE [userLogOn] = '" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString() & "' ").Rows(0)(0).ToString())
            TextBoxUN.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString()
            TextBoxLname.Text = DataGridView1.Rows(e.RowIndex).Cells(1).Value.ToString()
            TextBoxFname.Text = DataGridView1.Rows(e.RowIndex).Cells(2).Value.ToString()
            TextBoxPass.Text = "******"
            TextBoxPassConfirm.Text = "******"
            Select Case DataGridView1.Rows(e.RowIndex).Cells(5).Value.ToString()
                Case "Actif" : RadioButtonSA.Checked = True
                Case "Verrouillé" : RadioButtonSV.Checked = True
            End Select

            Dim socitiesRights As String = Read("SELECT [linkedsocities] FROM [rights] WHERE [userID] = " & id).Rows(0)(0).ToString()

            Dim split As String() = socitiesRights.Split(",")
            For Each item As String In split
                For x = 0 To ListBox1.Items.Count - 1
                    If ListBox1.Items(x).Row.ItemArray(0).ToString().Equals(item) Then
                        ListBox1.SetSelected(x, True)
                    End If
                Next
            Next

        End If
    End Sub

    Private Sub Btn_Del_Click(sender As Object, e As EventArgs) Handles Btn_Del.Click
        Dim confirm As DialogResult = MessageBox.Show("Êtes-vous sûrs de vouloir supprimer ?", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
        If confirm = Windows.Forms.DialogResult.Yes Then
            Dim id As Integer = 0
            Try
                id = Integer.Parse(Read("SELECT [userID] FROM [users] WHERE [userLogOn] = '" & DataGridView1.CurrentRow.Cells(0).Value.ToString() & "' ").Rows(0)(0).ToString())
            Catch ex As Exception
                ToolStripStatusLabelVal.Text = "Veuillez sélectionnez une ligne"
            End Try
            If id <> 0 Then

                Dim result As Integer = Execute("DELETE FROM [users] WHERE [userID] = " & id, Nothing)
                If result > 0 Then
                    Execute("DELETE FROM [rights] WHERE [userID] = " & id, Nothing)
                    ToolStripStatusLabelVal.Text = "Opération effectuée avec succès | Suppression"
                    loadGrid()
                Else
                    ToolStripStatusLabelVal.Text = "Opération Échoué, réessayer"
                End If
            End If
        End If
    End Sub

    Private Sub Btn_Profil_Click(sender As Object, e As EventArgs) Handles Btn_Profil.Click
        Dim id As Integer = 0
        Try
            id = Integer.Parse(Read("SELECT [userID] FROM [users] WHERE [userLogOn] = '" & DataGridView1.CurrentRow.Cells(0).Value.ToString() & "' ").Rows(0)(0).ToString())
        Catch ex As Exception
            ToolStripStatusLabelVal.Text = "Veuillez sélectionnez une ligne"
        End Try

        Dim frmAu As FrmUsersRightsUp = New FrmUsersRightsUp()
        frmAu.idAuth = id
        frmAu.ShowDialog()
    End Sub

    Private Sub FrmUsersUp_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
    End Sub

    Private Sub Btn_AddNew_Click(sender As Object, e As EventArgs) Handles Btn_AddNew.Click
        For Each Ctrl As Control In Me.Controls
            If TypeOf (Ctrl) Is TextBox Then
                Ctrl.Text = String.Empty
            ElseIf TypeOf (Ctrl) Is ListBox Then
                For x = 0 To ListBox1.Items.Count - 1
                    ListBox1.SetSelected(x, False)
                Next
            End If
        Next
        RadioButtonSA.Checked = True
        TextBoxUN.Focus()
    End Sub
End Class
