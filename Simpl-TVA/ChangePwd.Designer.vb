<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangePwd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangePwd))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Cmbutilisateur = New System.Windows.Forms.Label()
        Me._Label1_2 = New System.Windows.Forms.Label()
        Me._Label1_1 = New System.Windows.Forms.Label()
        Me._Label1_0 = New System.Windows.Forms.Label()
        Me._Label1_3 = New System.Windows.Forms.Label()
        Me.CmdConfirmer = New System.Windows.Forms.Button()
        Me.CmdFermer = New System.Windows.Forms.Button()
        Me.TxtAncPwd = New System.Windows.Forms.TextBox()
        Me.TxtNouvPwd = New System.Windows.Forms.TextBox()
        Me.TxtConfirmPwd = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Cmbutilisateur
        '
        Me.Cmbutilisateur.AutoSize = True
        Me.Cmbutilisateur.BackColor = System.Drawing.Color.White
        Me.Cmbutilisateur.Cursor = System.Windows.Forms.Cursors.Default
        Me.Cmbutilisateur.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cmbutilisateur.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Cmbutilisateur.Location = New System.Drawing.Point(133, 13)
        Me.Cmbutilisateur.Name = "Cmbutilisateur"
        Me.Cmbutilisateur.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Cmbutilisateur.Size = New System.Drawing.Size(118, 14)
        Me.Cmbutilisateur.TabIndex = 17
        Me.Cmbutilisateur.Text = "Nouveau mot de passe"
        '
        '_Label1_2
        '
        Me._Label1_2.AutoSize = True
        Me._Label1_2.BackColor = System.Drawing.Color.White
        Me._Label1_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_2.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_2.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_2.Location = New System.Drawing.Point(63, 109)
        Me._Label1_2.Name = "_Label1_2"
        Me._Label1_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_2.Size = New System.Drawing.Size(67, 14)
        Me._Label1_2.TabIndex = 16
        Me._Label1_2.Text = "Confirmation"
        Me._Label1_2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_1
        '
        Me._Label1_1.AutoSize = True
        Me._Label1_1.BackColor = System.Drawing.Color.White
        Me._Label1_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_1.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_1.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_1.Location = New System.Drawing.Point(12, 77)
        Me._Label1_1.Name = "_Label1_1"
        Me._Label1_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_1.Size = New System.Drawing.Size(118, 14)
        Me._Label1_1.TabIndex = 15
        Me._Label1_1.Text = "Nouveau mot de passe"
        Me._Label1_1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_0
        '
        Me._Label1_0.AutoSize = True
        Me._Label1_0.BackColor = System.Drawing.Color.White
        Me._Label1_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_0.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_0.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_0.Location = New System.Drawing.Point(21, 45)
        Me._Label1_0.Name = "_Label1_0"
        Me._Label1_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_0.Size = New System.Drawing.Size(109, 14)
        Me._Label1_0.TabIndex = 14
        Me._Label1_0.Text = "Ancien mot de passe"
        Me._Label1_0.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        '_Label1_3
        '
        Me._Label1_3.AutoSize = True
        Me._Label1_3.BackColor = System.Drawing.Color.White
        Me._Label1_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._Label1_3.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._Label1_3.ForeColor = System.Drawing.SystemColors.WindowText
        Me._Label1_3.Location = New System.Drawing.Point(76, 13)
        Me._Label1_3.Name = "_Label1_3"
        Me._Label1_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._Label1_3.Size = New System.Drawing.Size(54, 14)
        Me._Label1_3.TabIndex = 13
        Me._Label1_3.Text = "Utilisateur"
        Me._Label1_3.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.CmdConfirmer.Location = New System.Drawing.Point(130, 131)
        Me.CmdConfirmer.Name = "CmdConfirmer"
        Me.CmdConfirmer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdConfirmer.Size = New System.Drawing.Size(83, 27)
        Me.CmdConfirmer.TabIndex = 19
        Me.CmdConfirmer.Text = "&Confirmer"
        Me.CmdConfirmer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CmdConfirmer.UseVisualStyleBackColor = False
        '
        'CmdFermer
        '
        Me.CmdFermer.BackColor = System.Drawing.Color.White
        Me.CmdFermer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdFermer.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdFermer.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CmdFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdFermer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdFermer.Image = Global.Simpl_TVA.My.Resources.Resources.fermer
        Me.CmdFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CmdFermer.Location = New System.Drawing.Point(237, 131)
        Me.CmdFermer.Name = "CmdFermer"
        Me.CmdFermer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdFermer.Size = New System.Drawing.Size(83, 27)
        Me.CmdFermer.TabIndex = 18
        Me.CmdFermer.Text = "&Fermer"
        Me.CmdFermer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CmdFermer.UseVisualStyleBackColor = False
        '
        'TxtAncPwd
        '
        Me.TxtAncPwd.Location = New System.Drawing.Point(136, 42)
        Me.TxtAncPwd.MaxLength = 8
        Me.TxtAncPwd.Name = "TxtAncPwd"
        Me.TxtAncPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtAncPwd.Size = New System.Drawing.Size(184, 20)
        Me.TxtAncPwd.TabIndex = 20
        '
        'TxtNouvPwd
        '
        Me.TxtNouvPwd.Location = New System.Drawing.Point(136, 74)
        Me.TxtNouvPwd.MaxLength = 8
        Me.TxtNouvPwd.Name = "TxtNouvPwd"
        Me.TxtNouvPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtNouvPwd.Size = New System.Drawing.Size(184, 20)
        Me.TxtNouvPwd.TabIndex = 21
        '
        'TxtConfirmPwd
        '
        Me.TxtConfirmPwd.Location = New System.Drawing.Point(136, 103)
        Me.TxtConfirmPwd.MaxLength = 8
        Me.TxtConfirmPwd.Name = "TxtConfirmPwd"
        Me.TxtConfirmPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtConfirmPwd.Size = New System.Drawing.Size(184, 20)
        Me.TxtConfirmPwd.TabIndex = 22
        '
        'ChangePwd
        '
        Me.AcceptButton = Me.CmdConfirmer
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.CmdFermer
        Me.ClientSize = New System.Drawing.Size(347, 177)
        Me.Controls.Add(Me.TxtConfirmPwd)
        Me.Controls.Add(Me.TxtNouvPwd)
        Me.Controls.Add(Me.TxtAncPwd)
        Me.Controls.Add(Me.CmdConfirmer)
        Me.Controls.Add(Me.CmdFermer)
        Me.Controls.Add(Me.Cmbutilisateur)
        Me.Controls.Add(Me._Label1_2)
        Me.Controls.Add(Me._Label1_1)
        Me.Controls.Add(Me._Label1_0)
        Me.Controls.Add(Me._Label1_3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximumSize = New System.Drawing.Size(450, 274)
        Me.Name = "ChangePwd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Changer le mot de passe"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents CmdConfirmer As System.Windows.Forms.Button
    Public WithEvents CmdFermer As System.Windows.Forms.Button
    Public WithEvents Cmbutilisateur As System.Windows.Forms.Label
    Public WithEvents _Label1_2 As System.Windows.Forms.Label
    Public WithEvents _Label1_1 As System.Windows.Forms.Label
    Public WithEvents _Label1_0 As System.Windows.Forms.Label
    Public WithEvents _Label1_3 As System.Windows.Forms.Label
    Friend WithEvents TxtAncPwd As System.Windows.Forms.TextBox
    Friend WithEvents TxtNouvPwd As System.Windows.Forms.TextBox
    Friend WithEvents TxtConfirmPwd As System.Windows.Forms.TextBox
End Class
