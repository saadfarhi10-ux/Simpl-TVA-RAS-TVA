Imports System.Data.SqlClient

Public Class FrmUsersRightsUp
    Public Property idAuth As Integer
    Dim check As Boolean = False
    Public Property idAuthFilter As Integer
    Sub checkParents()
        Dim checkCount As Integer = 0
        For i = 0 To TreeView1.Nodes.Count - 1
            checkCount = 0
            For Each item As TreeNode In TreeView1.Nodes(i).Nodes
                If item.Checked = False Then
                    checkCount = -1
                    Exit For
                End If
            Next

            If checkCount <> -1 Then
                TreeView1.Nodes(i).Checked = True
            End If
        Next
    End Sub

    Private Sub CheckTreeViewNode(node As TreeNode, isChecked As Boolean)
        For Each item As TreeNode In node.Nodes
            item.Checked = isChecked
            If item.Nodes.Count > 0 Then
                Me.CheckTreeViewNode(item, isChecked)
            End If
        Next
    End Sub

    Private Sub CheckAll(tree As TreeView, isChecked As Boolean)
        For Each item As TreeNode In tree.Nodes
            item.Checked = isChecked
        Next
    End Sub

    Private Sub LoadRoles(id_profil_auth As Integer)
        Dim filterResult As DataTable = New DataTable()
        filterResult = Read("SELECT * FROM [rights] WHERE [userID] = '" & id_profil_auth & "' ")
        If filterResult.Rows.Count >= 1 Then
            ''Start Checking
            TreeView1.Nodes("acceuil").Nodes("acceuil_lecturedonnees").Checked = If(filterResult.Rows(0)("acceuil_lecturedonnees").ToString().Equals("1"), True, False)
            TreeView1.Nodes("acceuil").Nodes("acceuil_traitement").Checked = If(filterResult.Rows(0)("acceuil_traitement").ToString().Equals("1"), True, False)
            TreeView1.Nodes("acceuil").Nodes("acceuil_generation").Checked = If(filterResult.Rows(0)("acceuil_generation").ToString().Equals("1"), True, False)
            TreeView1.Nodes("acceuil").Nodes("acceuil_societesmaj").Checked = If(filterResult.Rows(0)("acceuil_societesmaj").ToString().Equals("1"), True, False)
            TreeView1.Nodes("acceuil").Nodes("acceuil_usersmaj").Checked = If(filterResult.Rows(0)("acceuil_usersmaj").ToString().Equals("1"), True, False)
            ''End Checking
            checkParents()
        End If
    End Sub
    Private Sub UsersRightsUp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBoxUserR.DataSource = Read("SELECT [userID] AS [USERID], LTRIM(RTRIM([userLogOn])) AS [Logon] FROM [users] WHERE LOWER(LTRIM(RTRIM([userLogOn]))) <> 'administrateur' ORDER BY [userID] DESC")
        ComboBoxUserR.DisplayMember = "Logon"
        ComboBoxUserR.ValueMember = "USERID"
        ComboBoxUserR.SelectedValue = idAuth

        ComboBoxFilterUser.DataSource = Read("SELECT [userID] AS [USERID], LTRIM(RTRIM([userLogOn])) AS [Logon] FROM [users] ORDER BY [userID] DESC")
        ComboBoxFilterUser.DisplayMember = "Logon"
        ComboBoxFilterUser.ValueMember = "USERID"
        ComboBoxFilterUser.SelectedValue = idAuth

        Btn_MaxiMini.Text = "Développer"
        Btn_Toggle.Text = "Cocher"

        TreeView1.CheckBoxes = True
        TreeView1.Nodes.Add("acceuil", "Droits utilisateurs", 0, 0)
        TreeView1.Nodes("acceuil").Nodes.Add("acceuil_lecturedonnees", "Lecture de données", 1, 0)
        TreeView1.Nodes("acceuil").Nodes.Add("acceuil_traitement", "Traitement", 1, 0)
        TreeView1.Nodes("acceuil").Nodes.Add("acceuil_generation", "Génération", 1, 0)
        TreeView1.Nodes("acceuil").Nodes.Add("acceuil_societesmaj", "Gestion des sociètés", 1, 0)
        TreeView1.Nodes("acceuil").Nodes.Add("acceuil_usersmaj", "Gestion des utilisateurs", 1, 0)

        TreeView1.ExpandAll()

        LoadRoles(idAuth)
    End Sub

    Private Sub Btn_Toggle_Click(sender As Object, e As EventArgs) Handles Btn_Toggle.Click
        If Btn_Toggle.Text = "Cocher" Then
            CheckAll(TreeView1, True)
            Btn_Toggle.Text = "Decocher"
        ElseIf Btn_Toggle.Text = "Decocher" Then
            CheckAll(TreeView1, False)
            Btn_Toggle.Text = "Cocher"
        End If
    End Sub

    Private Sub Btn_MaxiMini_Click(sender As Object, e As EventArgs) Handles Btn_MaxiMini.Click
        If Btn_MaxiMini.Text = "Développer" Then
            TreeView1.ExpandAll()
            Btn_MaxiMini.Text = "Envelopper"
        ElseIf Btn_MaxiMini.Text = "Envelopper" Then
            TreeView1.CollapseAll()
            Btn_MaxiMini.Text = "Développer"
        End If
    End Sub

    Private Sub ComboBoxUserR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxUserR.SelectedIndexChanged
        Try
            idAuth = Integer.Parse(ComboBoxUserR.SelectedValue.ToString())
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ComboBoxFilterUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxFilterUser.SelectedIndexChanged
        Try
            idAuthFilter = Integer.Parse(ComboBoxFilterUser.SelectedValue.ToString())
            LoadRoles(idAuthFilter)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnAddRoles_Click(sender As Object, e As EventArgs) Handles BtnAddRoles.Click
        Dim Sql As String = "UPDATE [rights] SET " & _
                            "  [acceuil_lecturedonnees]  = " & If(TreeView1.Nodes("acceuil").Nodes("acceuil_lecturedonnees").Checked, 1, 0) & _
                            ", [acceuil_traitement]  = " & If(TreeView1.Nodes("acceuil").Nodes("acceuil_traitement").Checked, 1, 0) & _
                            ", [acceuil_generation]  = " & If(TreeView1.Nodes("acceuil").Nodes("acceuil_generation").Checked, 1, 0) & _
                            ", [acceuil_societesmaj]  = " & If(TreeView1.Nodes("acceuil").Nodes("acceuil_societesmaj").Checked, 1, 0) & _
                            ", [acceuil_usersmaj]  = " & If(TreeView1.Nodes("acceuil").Nodes("acceuil_usersmaj").Checked, 1, 0) & _
                            "WHERE userID = " & idAuth
        Dim result As Integer = Execute(Sql, Nothing)
        If result > 0 Then
            ToolStripStatusLabelVal.Text = "Opération effectuée avec succès"
        Else
            ToolStripStatusLabelVal.Text = "Opération Échoué, réessayer"
        End If

    End Sub

    Private Sub TreeView1_AfterCheck(sender As Object, e As TreeViewEventArgs) Handles TreeView1.AfterCheck
        CheckTreeViewNode(e.Node, e.Node.Checked)
    End Sub
End Class