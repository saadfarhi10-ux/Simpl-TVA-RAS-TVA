<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUsersUp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUsersUp))
        Me.LabelConfirm = New System.Windows.Forms.Label()
        Me.TextBoxPassConfirm = New System.Windows.Forms.TextBox()
        Me.LabelUP = New System.Windows.Forms.Label()
        Me.TextBoxPass = New System.Windows.Forms.TextBox()
        Me.LabelFN = New System.Windows.Forms.Label()
        Me.TextBoxFname = New System.Windows.Forms.TextBox()
        Me.LabelLN = New System.Windows.Forms.Label()
        Me.TextBoxLname = New System.Windows.Forms.TextBox()
        Me.RadioButtonSA = New System.Windows.Forms.RadioButton()
        Me.LabelUN = New System.Windows.Forms.Label()
        Me.RadioButtonSV = New System.Windows.Forms.RadioButton()
        Me.TextBoxUN = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.LabelS = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Btn_Submit = New System.Windows.Forms.Button()
        Me.Btn_Profil = New System.Windows.Forms.Button()
        Me.Btn_Del = New System.Windows.Forms.Button()
        Me.Btn_AddNew = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelConfirm
        '
        Me.LabelConfirm.AutoSize = True
        Me.LabelConfirm.Location = New System.Drawing.Point(22, 89)
        Me.LabelConfirm.Name = "LabelConfirm"
        Me.LabelConfirm.Size = New System.Drawing.Size(123, 13)
        Me.LabelConfirm.TabIndex = 186
        Me.LabelConfirm.Text = "Confirmer mot de passe :"
        '
        'TextBoxPassConfirm
        '
        Me.TextBoxPassConfirm.Location = New System.Drawing.Point(151, 86)
        Me.TextBoxPassConfirm.MaxLength = 8
        Me.TextBoxPassConfirm.Name = "TextBoxPassConfirm"
        Me.TextBoxPassConfirm.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxPassConfirm.TabIndex = 170
        Me.TextBoxPassConfirm.UseSystemPasswordChar = True
        '
        'LabelUP
        '
        Me.LabelUP.AutoSize = True
        Me.LabelUP.Location = New System.Drawing.Point(68, 64)
        Me.LabelUP.Name = "LabelUP"
        Me.LabelUP.Size = New System.Drawing.Size(77, 13)
        Me.LabelUP.TabIndex = 185
        Me.LabelUP.Text = "Mot de passe :"
        '
        'TextBoxPass
        '
        Me.TextBoxPass.Location = New System.Drawing.Point(151, 61)
        Me.TextBoxPass.MaxLength = 8
        Me.TextBoxPass.Name = "TextBoxPass"
        Me.TextBoxPass.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxPass.TabIndex = 169
        Me.TextBoxPass.UseSystemPasswordChar = True
        '
        'LabelFN
        '
        Me.LabelFN.AutoSize = True
        Me.LabelFN.Location = New System.Drawing.Point(294, 62)
        Me.LabelFN.Name = "LabelFN"
        Me.LabelFN.Size = New System.Drawing.Size(43, 13)
        Me.LabelFN.TabIndex = 184
        Me.LabelFN.Text = "Préom :"
        '
        'TextBoxFname
        '
        Me.TextBoxFname.Location = New System.Drawing.Point(343, 59)
        Me.TextBoxFname.Name = "TextBoxFname"
        Me.TextBoxFname.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxFname.TabIndex = 172
        '
        'LabelLN
        '
        Me.LabelLN.AutoSize = True
        Me.LabelLN.Location = New System.Drawing.Point(302, 33)
        Me.LabelLN.Name = "LabelLN"
        Me.LabelLN.Size = New System.Drawing.Size(35, 13)
        Me.LabelLN.TabIndex = 183
        Me.LabelLN.Text = "Nom :"
        '
        'TextBoxLname
        '
        Me.TextBoxLname.Location = New System.Drawing.Point(343, 30)
        Me.TextBoxLname.Name = "TextBoxLname"
        Me.TextBoxLname.Size = New System.Drawing.Size(126, 20)
        Me.TextBoxLname.TabIndex = 171
        '
        'RadioButtonSA
        '
        Me.RadioButtonSA.AutoSize = True
        Me.RadioButtonSA.Checked = True
        Me.RadioButtonSA.Location = New System.Drawing.Point(343, 89)
        Me.RadioButtonSA.Name = "RadioButtonSA"
        Me.RadioButtonSA.Size = New System.Drawing.Size(46, 17)
        Me.RadioButtonSA.TabIndex = 173
        Me.RadioButtonSA.TabStop = True
        Me.RadioButtonSA.Text = "Actif"
        Me.RadioButtonSA.UseVisualStyleBackColor = True
        '
        'LabelUN
        '
        Me.LabelUN.AutoSize = True
        Me.LabelUN.Location = New System.Drawing.Point(106, 33)
        Me.LabelUN.Name = "LabelUN"
        Me.LabelUN.Size = New System.Drawing.Size(39, 13)
        Me.LabelUN.TabIndex = 180
        Me.LabelUN.Text = "Login :"
        '
        'RadioButtonSV
        '
        Me.RadioButtonSV.AutoSize = True
        Me.RadioButtonSV.Location = New System.Drawing.Point(398, 89)
        Me.RadioButtonSV.Name = "RadioButtonSV"
        Me.RadioButtonSV.Size = New System.Drawing.Size(71, 17)
        Me.RadioButtonSV.TabIndex = 174
        Me.RadioButtonSV.Text = "Verrouiller"
        Me.RadioButtonSV.UseVisualStyleBackColor = True
        '
        'TextBoxUN
        '
        Me.TextBoxUN.Location = New System.Drawing.Point(151, 30)
        Me.TextBoxUN.Name = "TextBoxUN"
        Me.TextBoxUN.Size = New System.Drawing.Size(114, 20)
        Me.TextBoxUN.TabIndex = 168
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 139)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(800, 252)
        Me.DataGridView1.TabIndex = 179
        '
        'LabelS
        '
        Me.LabelS.AutoSize = True
        Me.LabelS.Location = New System.Drawing.Point(305, 89)
        Me.LabelS.Name = "LabelS"
        Me.LabelS.Size = New System.Drawing.Size(32, 13)
        Me.LabelS.TabIndex = 181
        Me.LabelS.Text = "Etat :"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelVal})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 394)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(834, 22)
        Me.StatusStrip1.TabIndex = 189
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelVal
        '
        Me.ToolStripStatusLabelVal.Name = "ToolStripStatusLabelVal"
        Me.ToolStripStatusLabelVal.Size = New System.Drawing.Size(0, 17)
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(475, 12)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(188, 121)
        Me.ListBox1.TabIndex = 191
        '
        'Btn_Submit
        '
        Me.Btn_Submit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Submit.BackColor = System.Drawing.Color.White
        Me.Btn_Submit.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Submit.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Submit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Submit.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.Btn_Submit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Submit.Location = New System.Drawing.Point(715, 47)
        Me.Btn_Submit.Name = "Btn_Submit"
        Me.Btn_Submit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Submit.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Submit.TabIndex = 175
        Me.Btn_Submit.Text = "&Valider"
        Me.Btn_Submit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_Submit.UseVisualStyleBackColor = False
        '
        'Btn_Profil
        '
        Me.Btn_Profil.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Profil.BackColor = System.Drawing.Color.White
        Me.Btn_Profil.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Profil.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Profil.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Profil.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Profil.Image = Global.Simpl_TVA.My.Resources.Resources.User
        Me.Btn_Profil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Profil.Location = New System.Drawing.Point(715, 106)
        Me.Btn_Profil.Name = "Btn_Profil"
        Me.Btn_Profil.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Profil.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Profil.TabIndex = 178
        Me.Btn_Profil.Text = "&Profil"
        Me.Btn_Profil.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_Profil.UseVisualStyleBackColor = False
        '
        'Btn_Del
        '
        Me.Btn_Del.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Del.BackColor = System.Drawing.Color.White
        Me.Btn_Del.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Del.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Del.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Del.Image = Global.Simpl_TVA.My.Resources.Resources.delete
        Me.Btn_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Del.Location = New System.Drawing.Point(715, 76)
        Me.Btn_Del.Name = "Btn_Del"
        Me.Btn_Del.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Del.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Del.TabIndex = 176
        Me.Btn_Del.Text = "&Supprimer"
        Me.Btn_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_Del.UseVisualStyleBackColor = False
        '
        'Btn_AddNew
        '
        Me.Btn_AddNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_AddNew.BackColor = System.Drawing.Color.White
        Me.Btn_AddNew.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_AddNew.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_AddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_AddNew.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_AddNew.Image = Global.Simpl_TVA.My.Resources.Resources.add
        Me.Btn_AddNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_AddNew.Location = New System.Drawing.Point(715, 17)
        Me.Btn_AddNew.Name = "Btn_AddNew"
        Me.Btn_AddNew.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_AddNew.Size = New System.Drawing.Size(87, 27)
        Me.Btn_AddNew.TabIndex = 192
        Me.Btn_AddNew.Text = "&Nouveau"
        Me.Btn_AddNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_AddNew.UseVisualStyleBackColor = False
        '
        'FrmUsersUp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 416)
        Me.Controls.Add(Me.Btn_AddNew)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Btn_Submit)
        Me.Controls.Add(Me.LabelConfirm)
        Me.Controls.Add(Me.TextBoxPassConfirm)
        Me.Controls.Add(Me.Btn_Profil)
        Me.Controls.Add(Me.Btn_Del)
        Me.Controls.Add(Me.LabelUP)
        Me.Controls.Add(Me.TextBoxPass)
        Me.Controls.Add(Me.LabelFN)
        Me.Controls.Add(Me.TextBoxFname)
        Me.Controls.Add(Me.LabelLN)
        Me.Controls.Add(Me.TextBoxLname)
        Me.Controls.Add(Me.RadioButtonSA)
        Me.Controls.Add(Me.LabelUN)
        Me.Controls.Add(Me.RadioButtonSV)
        Me.Controls.Add(Me.TextBoxUN)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.LabelS)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmUsersUp"
        Me.Text = "Gestion des utilisateurs"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Btn_Submit As System.Windows.Forms.Button
    Friend WithEvents LabelConfirm As System.Windows.Forms.Label
    Friend WithEvents TextBoxPassConfirm As System.Windows.Forms.TextBox
    Public WithEvents Btn_Profil As System.Windows.Forms.Button
    Public WithEvents Btn_Del As System.Windows.Forms.Button
    Friend WithEvents LabelUP As System.Windows.Forms.Label
    Friend WithEvents TextBoxPass As System.Windows.Forms.TextBox
    Friend WithEvents LabelFN As System.Windows.Forms.Label
    Friend WithEvents TextBoxFname As System.Windows.Forms.TextBox
    Friend WithEvents LabelLN As System.Windows.Forms.Label
    Friend WithEvents TextBoxLname As System.Windows.Forms.TextBox
    Friend WithEvents RadioButtonSA As System.Windows.Forms.RadioButton
    Friend WithEvents LabelUN As System.Windows.Forms.Label
    Friend WithEvents RadioButtonSV As System.Windows.Forms.RadioButton
    Friend WithEvents TextBoxUN As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents LabelS As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelVal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Public WithEvents Btn_AddNew As System.Windows.Forms.Button

End Class
