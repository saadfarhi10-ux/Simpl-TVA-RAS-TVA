<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsersRightsUp
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUsersRightsUp))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxFilterUser = New System.Windows.Forms.ComboBox()
        Me.ComboBoxUserR = New System.Windows.Forms.ComboBox()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStripStatusLabelVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.BtnAddRoles = New System.Windows.Forms.Button()
        Me.Btn_Toggle = New System.Windows.Forms.Button()
        Me.Btn_MaxiMini = New System.Windows.Forms.Button()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(343, 70)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 107
        Me.Label1.Text = "à partir de : "
        '
        'ComboBoxFilterUser
        '
        Me.ComboBoxFilterUser.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxFilterUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxFilterUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxFilterUser.FormattingEnabled = True
        Me.ComboBoxFilterUser.Location = New System.Drawing.Point(412, 67)
        Me.ComboBoxFilterUser.Name = "ComboBoxFilterUser"
        Me.ComboBoxFilterUser.Size = New System.Drawing.Size(190, 21)
        Me.ComboBoxFilterUser.TabIndex = 106
        '
        'ComboBoxUserR
        '
        Me.ComboBoxUserR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxUserR.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxUserR.FormattingEnabled = True
        Me.ComboBoxUserR.Location = New System.Drawing.Point(90, 27)
        Me.ComboBoxUserR.Name = "ComboBoxUserR"
        Me.ComboBoxUserR.Size = New System.Drawing.Size(212, 21)
        Me.ComboBoxUserR.TabIndex = 105
        '
        'TreeView1
        '
        Me.TreeView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TreeView1.ImageIndex = 0
        Me.TreeView1.ImageList = Me.ImageList1
        Me.TreeView1.Indent = 19
        Me.TreeView1.LineColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.TreeView1.Location = New System.Drawing.Point(12, 58)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.SelectedImageIndex = 0
        Me.TreeView1.Size = New System.Drawing.Size(607, 292)
        Me.TreeView1.TabIndex = 101
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "1448385054_todo_list_add.png")
        Me.ImageList1.Images.SetKeyName(1, "1448384546_order-1.png")
        Me.ImageList1.Images.SetKeyName(2, "1448384553_category.png")
        '
        'ToolStripStatusLabelVal
        '
        Me.ToolStripStatusLabelVal.Name = "ToolStripStatusLabelVal"
        Me.ToolStripStatusLabelVal.Size = New System.Drawing.Size(0, 17)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelVal})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 353)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(631, 22)
        Me.StatusStrip1.TabIndex = 104
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'BtnAddRoles
        '
        Me.BtnAddRoles.BackColor = System.Drawing.Color.White
        Me.BtnAddRoles.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.BtnAddRoles.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnAddRoles.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.BtnAddRoles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnAddRoles.Location = New System.Drawing.Point(308, 25)
        Me.BtnAddRoles.Name = "BtnAddRoles"
        Me.BtnAddRoles.Size = New System.Drawing.Size(75, 27)
        Me.BtnAddRoles.TabIndex = 102
        Me.BtnAddRoles.Text = "Valider"
        Me.BtnAddRoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnAddRoles.UseVisualStyleBackColor = False
        '
        'Btn_Toggle
        '
        Me.Btn_Toggle.BackColor = System.Drawing.Color.White
        Me.Btn_Toggle.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Toggle.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Toggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Toggle.Font = New System.Drawing.Font("Microsoft Sans Serif", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_Toggle.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Btn_Toggle.Image = Global.Simpl_TVA.My.Resources.Resources.finished
        Me.Btn_Toggle.Location = New System.Drawing.Point(12, 25)
        Me.Btn_Toggle.Name = "Btn_Toggle"
        Me.Btn_Toggle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Toggle.Size = New System.Drawing.Size(33, 27)
        Me.Btn_Toggle.TabIndex = 108
        Me.Btn_Toggle.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_Toggle.UseVisualStyleBackColor = False
        '
        'Btn_MaxiMini
        '
        Me.Btn_MaxiMini.BackColor = System.Drawing.Color.White
        Me.Btn_MaxiMini.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_MaxiMini.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_MaxiMini.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_MaxiMini.Font = New System.Drawing.Font("Microsoft Sans Serif", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btn_MaxiMini.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Btn_MaxiMini.Image = Global.Simpl_TVA.My.Resources.Resources.expand
        Me.Btn_MaxiMini.Location = New System.Drawing.Point(51, 25)
        Me.Btn_MaxiMini.Name = "Btn_MaxiMini"
        Me.Btn_MaxiMini.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_MaxiMini.Size = New System.Drawing.Size(33, 27)
        Me.Btn_MaxiMini.TabIndex = 103
        Me.Btn_MaxiMini.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_MaxiMini.UseVisualStyleBackColor = False
        '
        'FrmUsersRightsUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(631, 375)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBoxFilterUser)
        Me.Controls.Add(Me.ComboBoxUserR)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.BtnAddRoles)
        Me.Controls.Add(Me.Btn_Toggle)
        Me.Controls.Add(Me.Btn_MaxiMini)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmUsersRightsUp"
        Me.Text = "Gestion droits utilisateurs"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxFilterUser As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxUserR As System.Windows.Forms.ComboBox
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripStatusLabelVal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents BtnAddRoles As System.Windows.Forms.Button
    Public WithEvents Btn_Toggle As System.Windows.Forms.Button
    Public WithEvents Btn_MaxiMini As System.Windows.Forms.Button
End Class
