<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerParameters
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerParameters))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtBase = New System.Windows.Forms.TextBox()
        Me.GroupConnexion = New System.Windows.Forms.GroupBox()
        Me.TxtUser = New System.Windows.Forms.TextBox()
        Me._Label1_1 = New System.Windows.Forms.Label()
        Me._Label1_0 = New System.Windows.Forms.Label()
        Me.TxtPwd = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonWIN = New System.Windows.Forms.RadioButton()
        Me.RadioButtonSQL = New System.Windows.Forms.RadioButton()
        Me.TxtServer = New System.Windows.Forms.TextBox()
        Me._Label1_3 = New System.Windows.Forms.Label()
        Me.Btn_Restart = New System.Windows.Forms.Button()
        Me.CmdQuit = New System.Windows.Forms.Button()
        Me.CmdConfirmer = New System.Windows.Forms.Button()
        Me.GroupConnexion.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Label3.Location = New System.Drawing.Point(17, 174)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 13)
        Me.Label3.TabIndex = 26
        Me.Label3.Text = "Base de données : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtBase
        '
        Me.TxtBase.AcceptsReturn = True
        Me.TxtBase.BackColor = System.Drawing.SystemColors.Window
        Me.TxtBase.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtBase.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtBase.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtBase.Location = New System.Drawing.Point(122, 167)
        Me.TxtBase.MaxLength = 40
        Me.TxtBase.Name = "TxtBase"
        Me.TxtBase.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBase.Size = New System.Drawing.Size(186, 20)
        Me.TxtBase.TabIndex = 0
        '
        'GroupConnexion
        '
        Me.GroupConnexion.Controls.Add(Me.TxtUser)
        Me.GroupConnexion.Controls.Add(Me._Label1_1)
        Me.GroupConnexion.Controls.Add(Me._Label1_0)
        Me.GroupConnexion.Controls.Add(Me.TxtPwd)
        Me.GroupConnexion.Location = New System.Drawing.Point(21, 85)
        Me.GroupConnexion.Name = "GroupConnexion"
        Me.GroupConnexion.Size = New System.Drawing.Size(307, 79)
        Me.GroupConnexion.TabIndex = 33
        Me.GroupConnexion.TabStop = False
        Me.GroupConnexion.Text = "Connexion"
        '
        'TxtUser
        '
        Me.TxtUser.AcceptsReturn = True
        Me.TxtUser.BackColor = System.Drawing.SystemColors.Window
        Me.TxtUser.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtUser.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtUser.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtUser.Location = New System.Drawing.Point(113, 19)
        Me.TxtUser.MaxLength = 20
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtUser.Size = New System.Drawing.Size(186, 20)
        Me.TxtUser.TabIndex = 0
        '
        '_Label1_1
        '
        Me._Label1_1.AutoSize = True
        Me._Label1_1.BackColor = System.Drawing.Color.Transparent
        Me._Label1_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_1.Location = New System.Drawing.Point(8, 48)
        Me._Label1_1.Name = "_Label1_1"
        Me._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_1.Size = New System.Drawing.Size(77, 13)
        Me._Label1_1.TabIndex = 18
        Me._Label1_1.Text = "Mot de passe :"
        Me._Label1_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_0
        '
        Me._Label1_0.AutoSize = True
        Me._Label1_0.BackColor = System.Drawing.Color.Transparent
        Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_0.Location = New System.Drawing.Point(8, 22)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(59, 13)
        Me._Label1_0.TabIndex = 19
        Me._Label1_0.Text = "Utilisateur :"
        Me._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TxtPwd
        '
        Me.TxtPwd.AcceptsReturn = True
        Me.TxtPwd.BackColor = System.Drawing.SystemColors.Window
        Me.TxtPwd.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtPwd.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtPwd.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtPwd.Location = New System.Drawing.Point(113, 45)
        Me.TxtPwd.MaxLength = 20
        Me.TxtPwd.Name = "TxtPwd"
        Me.TxtPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPwd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtPwd.Size = New System.Drawing.Size(186, 20)
        Me.TxtPwd.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.RadioButtonWIN)
        Me.GroupBox3.Controls.Add(Me.TxtBase)
        Me.GroupBox3.Controls.Add(Me.RadioButtonSQL)
        Me.GroupBox3.Controls.Add(Me.TxtServer)
        Me.GroupBox3.Controls.Add(Me._Label1_3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(322, 214)
        Me.GroupBox3.TabIndex = 34
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Authentification SQL"
        '
        'RadioButtonWIN
        '
        Me.RadioButtonWIN.AutoSize = True
        Me.RadioButtonWIN.ForeColor = System.Drawing.Color.Black
        Me.RadioButtonWIN.Location = New System.Drawing.Point(168, 46)
        Me.RadioButtonWIN.Name = "RadioButtonWIN"
        Me.RadioButtonWIN.Size = New System.Drawing.Size(69, 17)
        Me.RadioButtonWIN.TabIndex = 1
        Me.RadioButtonWIN.Text = "Windows"
        Me.RadioButtonWIN.UseVisualStyleBackColor = True
        '
        'RadioButtonSQL
        '
        Me.RadioButtonSQL.AutoSize = True
        Me.RadioButtonSQL.Checked = True
        Me.RadioButtonSQL.ForeColor = System.Drawing.Color.Black
        Me.RadioButtonSQL.Location = New System.Drawing.Point(74, 47)
        Me.RadioButtonSQL.Name = "RadioButtonSQL"
        Me.RadioButtonSQL.Size = New System.Drawing.Size(80, 17)
        Me.RadioButtonSQL.TabIndex = 0
        Me.RadioButtonSQL.TabStop = True
        Me.RadioButtonSQL.Text = "SQL Server"
        Me.RadioButtonSQL.UseVisualStyleBackColor = True
        '
        'TxtServer
        '
        Me.TxtServer.AcceptsReturn = True
        Me.TxtServer.BackColor = System.Drawing.SystemColors.Window
        Me.TxtServer.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtServer.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtServer.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TxtServer.Location = New System.Drawing.Point(122, 19)
        Me.TxtServer.MaxLength = 60
        Me.TxtServer.Name = "TxtServer"
        Me.TxtServer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtServer.Size = New System.Drawing.Size(193, 20)
        Me.TxtServer.TabIndex = 0
        '
        '_Label1_3
        '
        Me._Label1_3.AutoSize = True
        Me._Label1_3.BackColor = System.Drawing.Color.Transparent
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_3.Location = New System.Drawing.Point(17, 22)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(50, 13)
        Me._Label1_3.TabIndex = 20
        Me._Label1_3.Text = "Serveur :"
        Me._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Btn_Restart
        '
        Me.Btn_Restart.BackColor = System.Drawing.Color.White
        Me.Btn_Restart.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Restart.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Restart.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Restart.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Restart.Image = CType(resources.GetObject("Btn_Restart.Image"), System.Drawing.Image)
        Me.Btn_Restart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Restart.Location = New System.Drawing.Point(12, 277)
        Me.Btn_Restart.Name = "Btn_Restart"
        Me.Btn_Restart.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Restart.Size = New System.Drawing.Size(322, 42)
        Me.Btn_Restart.TabIndex = 38
        Me.Btn_Restart.Text = "&Continuer"
        Me.Btn_Restart.UseVisualStyleBackColor = False
        Me.Btn_Restart.Visible = False
        '
        'CmdQuit
        '
        Me.CmdQuit.BackColor = System.Drawing.Color.White
        Me.CmdQuit.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdQuit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdQuit.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CmdQuit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdQuit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdQuit.Image = Global.Simpl_TVA.My.Resources.Resources.fermer
        Me.CmdQuit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdQuit.Location = New System.Drawing.Point(213, 246)
        Me.CmdQuit.Name = "CmdQuit"
        Me.CmdQuit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdQuit.Size = New System.Drawing.Size(121, 25)
        Me.CmdQuit.TabIndex = 37
        Me.CmdQuit.Text = "&Annuler"
        Me.CmdQuit.UseVisualStyleBackColor = False
        '
        'CmdConfirmer
        '
        Me.CmdConfirmer.BackColor = System.Drawing.Color.White
        Me.CmdConfirmer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdConfirmer.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CmdConfirmer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdConfirmer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdConfirmer.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.CmdConfirmer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdConfirmer.Location = New System.Drawing.Point(12, 246)
        Me.CmdConfirmer.Name = "CmdConfirmer"
        Me.CmdConfirmer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdConfirmer.Size = New System.Drawing.Size(121, 25)
        Me.CmdConfirmer.TabIndex = 36
        Me.CmdConfirmer.Text = "&Confirmer"
        Me.CmdConfirmer.UseVisualStyleBackColor = False
        '
        'ServerParameters
        '
        Me.AcceptButton = Me.CmdConfirmer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.CmdQuit
        Me.ClientSize = New System.Drawing.Size(342, 329)
        Me.Controls.Add(Me.Btn_Restart)
        Me.Controls.Add(Me.CmdQuit)
        Me.Controls.Add(Me.CmdConfirmer)
        Me.Controls.Add(Me.GroupConnexion)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(649, 421)
        Me.Name = "ServerParameters"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuration Serveur"
        Me.GroupConnexion.ResumeLayout(False)
        Me.GroupConnexion.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtBase As System.Windows.Forms.TextBox
    Friend WithEvents GroupConnexion As System.Windows.Forms.GroupBox
    Public WithEvents TxtUser As System.Windows.Forms.TextBox
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    Public WithEvents TxtPwd As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButtonWIN As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonSQL As System.Windows.Forms.RadioButton
    Public WithEvents TxtServer As System.Windows.Forms.TextBox
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Public WithEvents CmdQuit As System.Windows.Forms.Button
    Public WithEvents CmdConfirmer As System.Windows.Forms.Button
    Public WithEvents Btn_Restart As System.Windows.Forms.Button
End Class
