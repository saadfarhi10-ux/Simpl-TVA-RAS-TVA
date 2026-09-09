<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmModeConnexion
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.LabelTitre = New System.Windows.Forms.Label()
        Me.LabelSousTitre = New System.Windows.Forms.Label()
        Me.BtnClient = New System.Windows.Forms.Button()
        Me.BtnFournisseur = New System.Windows.Forms.Button()
        Me.BtnFournisseurNonResident = New System.Windows.Forms.Button()
        Me.BtnQuitterMode = New System.Windows.Forms.Button()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.Simpl_TVA.My.Resources.Resources.logo
        Me.PictureBox1.InitialImage = Global.Simpl_TVA.My.Resources.Resources.logo
        Me.PictureBox1.Location = New System.Drawing.Point(307, 16)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(101, 71)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'LabelTitre
        '
        Me.LabelTitre.AutoSize = True
        Me.LabelTitre.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTitre.ForeColor = System.Drawing.Color.Indigo
        Me.LabelTitre.Location = New System.Drawing.Point(20, 22)
        Me.LabelTitre.Name = "LabelTitre"
        Me.LabelTitre.Size = New System.Drawing.Size(238, 26)
        Me.LabelTitre.TabIndex = 1
        Me.LabelTitre.Text = "Choisir un mode de connexion"
        '
        'LabelSousTitre
        '
        Me.LabelSousTitre.AutoSize = True
        Me.LabelSousTitre.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelSousTitre.ForeColor = System.Drawing.Color.Gray
        Me.LabelSousTitre.Location = New System.Drawing.Point(21, 54)
        Me.LabelSousTitre.Name = "LabelSousTitre"
        Me.LabelSousTitre.Size = New System.Drawing.Size(238, 16)
        Me.LabelSousTitre.TabIndex = 2
        Me.LabelSousTitre.Text = "Comment souhaitez-vous vous connecter ?"
        '
        'BtnClient
        '
        Me.BtnClient.BackColor = System.Drawing.Color.White
        Me.BtnClient.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnClient.FlatAppearance.BorderColor = System.Drawing.Color.Indigo
        Me.BtnClient.FlatAppearance.BorderSize = 2
        Me.BtnClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnClient.Font = New System.Drawing.Font("Calibri", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClient.ForeColor = System.Drawing.Color.Indigo
        Me.BtnClient.Location = New System.Drawing.Point(20, 96)
        Me.BtnClient.Name = "BtnClient"
        Me.BtnClient.Size = New System.Drawing.Size(380, 50)
        Me.BtnClient.TabIndex = 3
        Me.BtnClient.Text = "Mode Client"
        Me.BtnClient.UseVisualStyleBackColor = False
        '
        'BtnFournisseur
        '
        Me.BtnFournisseur.BackColor = System.Drawing.Color.White
        Me.BtnFournisseur.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFournisseur.FlatAppearance.BorderColor = System.Drawing.Color.Indigo
        Me.BtnFournisseur.FlatAppearance.BorderSize = 2
        Me.BtnFournisseur.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFournisseur.Font = New System.Drawing.Font("Calibri", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFournisseur.ForeColor = System.Drawing.Color.Indigo
        Me.BtnFournisseur.Location = New System.Drawing.Point(20, 152)
        Me.BtnFournisseur.Name = "BtnFournisseur"
        Me.BtnFournisseur.Size = New System.Drawing.Size(380, 50)
        Me.BtnFournisseur.TabIndex = 4
        Me.BtnFournisseur.Text = "Fournisseur Résident"
        Me.BtnFournisseur.UseVisualStyleBackColor = False
        '
        'BtnFournisseurNonResident
        '
        Me.BtnFournisseurNonResident.BackColor = System.Drawing.Color.White
        Me.BtnFournisseurNonResident.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnFournisseurNonResident.FlatAppearance.BorderColor = System.Drawing.Color.Indigo
        Me.BtnFournisseurNonResident.FlatAppearance.BorderSize = 2
        Me.BtnFournisseurNonResident.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFournisseurNonResident.Font = New System.Drawing.Font("Calibri", 13.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFournisseurNonResident.ForeColor = System.Drawing.Color.Indigo
        Me.BtnFournisseurNonResident.Location = New System.Drawing.Point(20, 208)
        Me.BtnFournisseurNonResident.Name = "BtnFournisseurNonResident"
        Me.BtnFournisseurNonResident.Size = New System.Drawing.Size(380, 50)
        Me.BtnFournisseurNonResident.TabIndex = 6
        Me.BtnFournisseurNonResident.Text = "Fournisseur Non-Résident"
        Me.BtnFournisseurNonResident.UseVisualStyleBackColor = False
        '
        'BtnQuitterMode
        '
        Me.BtnQuitterMode.BackColor = System.Drawing.Color.White
        Me.BtnQuitterMode.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnQuitterMode.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick
        Me.BtnQuitterMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnQuitterMode.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnQuitterMode.ForeColor = System.Drawing.Color.Firebrick
        Me.BtnQuitterMode.Location = New System.Drawing.Point(20, 268)
        Me.BtnQuitterMode.Name = "BtnQuitterMode"
        Me.BtnQuitterMode.Size = New System.Drawing.Size(93, 27)
        Me.BtnQuitterMode.TabIndex = 5
        Me.BtnQuitterMode.Text = "Retour"
        Me.BtnQuitterMode.UseVisualStyleBackColor = False
        '
        'FrmModeConnexion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.BtnQuitterMode
        Me.ClientSize = New System.Drawing.Size(419, 310)
        Me.Controls.Add(Me.BtnQuitterMode)
        Me.Controls.Add(Me.BtnFournisseurNonResident)
        Me.Controls.Add(Me.BtnFournisseur)
        Me.Controls.Add(Me.BtnClient)
        Me.Controls.Add(Me.LabelSousTitre)
        Me.Controls.Add(Me.LabelTitre)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmModeConnexion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mode de connexion"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LabelTitre As System.Windows.Forms.Label
    Friend WithEvents LabelSousTitre As System.Windows.Forms.Label
    Friend WithEvents BtnClient As System.Windows.Forms.Button
    Friend WithEvents BtnFournisseur As System.Windows.Forms.Button
    Friend WithEvents BtnFournisseurNonResident As System.Windows.Forms.Button
    Friend WithEvents BtnQuitterMode As System.Windows.Forms.Button
End Class