<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MiseaJourSociete
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MiseaJourSociete))
        Me.ToolStripStatusLabelVal = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LabelConfirm = New System.Windows.Forms.Label()
        Me.patente = New System.Windows.Forms.TextBox()
        Me.LabelUP = New System.Windows.Forms.Label()
        Me.identifiantfiscal = New System.Windows.Forms.TextBox()
        Me.LabelFN = New System.Windows.Forms.Label()
        Me.ville = New System.Windows.Forms.TextBox()
        Me.LabelLN = New System.Windows.Forms.Label()
        Me.pays = New System.Windows.Forms.TextBox()
        Me.LabelUN = New System.Windows.Forms.Label()
        Me.raisonsociale = New System.Windows.Forms.TextBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.telephone = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.adresse = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.email = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.fax = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.tva = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.site = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cnss = New System.Windows.Forms.TextBox()
        Me.id = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.sce = New System.Windows.Forms.TextBox()
        Me.Btn_Submit = New System.Windows.Forms.Button()
        Me.Btn_Profil = New System.Windows.Forms.Button()
        Me.Btn_Del = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtProrata = New System.Windows.Forms.ComboBox()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStripStatusLabelVal
        '
        Me.ToolStripStatusLabelVal.Name = "ToolStripStatusLabelVal"
        Me.ToolStripStatusLabelVal.Size = New System.Drawing.Size(0, 17)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelVal, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 398)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(934, 22)
        Me.StatusStrip1.TabIndex = 209
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(10, 17)
        Me.ToolStripStatusLabel1.Text = " "
        '
        'LabelConfirm
        '
        Me.LabelConfirm.AutoSize = True
        Me.LabelConfirm.Location = New System.Drawing.Point(64, 60)
        Me.LabelConfirm.Name = "LabelConfirm"
        Me.LabelConfirm.Size = New System.Drawing.Size(53, 13)
        Me.LabelConfirm.TabIndex = 208
        Me.LabelConfirm.Text = "Patente : "
        '
        'patente
        '
        Me.patente.Location = New System.Drawing.Point(119, 57)
        Me.patente.Name = "patente"
        Me.patente.Size = New System.Drawing.Size(146, 20)
        Me.patente.TabIndex = 194
        '
        'LabelUP
        '
        Me.LabelUP.AutoSize = True
        Me.LabelUP.Location = New System.Drawing.Point(25, 33)
        Me.LabelUP.Name = "LabelUP"
        Me.LabelUP.Size = New System.Drawing.Size(92, 13)
        Me.LabelUP.TabIndex = 207
        Me.LabelUP.Text = "Identifiant Fiscal : "
        '
        'identifiantfiscal
        '
        Me.identifiantfiscal.Location = New System.Drawing.Point(119, 32)
        Me.identifiantfiscal.Name = "identifiantfiscal"
        Me.identifiantfiscal.Size = New System.Drawing.Size(146, 20)
        Me.identifiantfiscal.TabIndex = 193
        '
        'LabelFN
        '
        Me.LabelFN.AutoSize = True
        Me.LabelFN.Location = New System.Drawing.Point(316, 10)
        Me.LabelFN.Name = "LabelFN"
        Me.LabelFN.Size = New System.Drawing.Size(32, 13)
        Me.LabelFN.TabIndex = 206
        Me.LabelFN.Text = "Ville :"
        '
        'ville
        '
        Me.ville.Location = New System.Drawing.Point(358, 6)
        Me.ville.Name = "ville"
        Me.ville.Size = New System.Drawing.Size(146, 20)
        Me.ville.TabIndex = 196
        '
        'LabelLN
        '
        Me.LabelLN.AutoSize = True
        Me.LabelLN.Location = New System.Drawing.Point(77, 85)
        Me.LabelLN.Name = "LabelLN"
        Me.LabelLN.Size = New System.Drawing.Size(36, 13)
        Me.LabelLN.TabIndex = 205
        Me.LabelLN.Text = "Pays :"
        '
        'pays
        '
        Me.pays.Location = New System.Drawing.Point(119, 82)
        Me.pays.Name = "pays"
        Me.pays.Size = New System.Drawing.Size(146, 20)
        Me.pays.TabIndex = 195
        '
        'LabelUN
        '
        Me.LabelUN.AutoSize = True
        Me.LabelUN.Location = New System.Drawing.Point(30, 9)
        Me.LabelUN.Name = "LabelUN"
        Me.LabelUN.Size = New System.Drawing.Size(87, 13)
        Me.LabelUN.TabIndex = 203
        Me.LabelUN.Text = "Raison Sociale : "
        '
        'raisonsociale
        '
        Me.raisonsociale.Location = New System.Drawing.Point(119, 6)
        Me.raisonsociale.Name = "raisonsociale"
        Me.raisonsociale.Size = New System.Drawing.Size(146, 20)
        Me.raisonsociale.TabIndex = 192
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
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 145)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(922, 250)
        Me.DataGridView1.TabIndex = 202
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(287, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 213
        Me.Label1.Text = "Téléphone : "
        '
        'telephone
        '
        Me.telephone.Location = New System.Drawing.Point(358, 59)
        Me.telephone.Name = "telephone"
        Me.telephone.Size = New System.Drawing.Size(146, 20)
        Me.telephone.TabIndex = 211
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(299, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 13)
        Me.Label2.TabIndex = 212
        Me.Label2.Text = "Adresse : "
        '
        'adresse
        '
        Me.adresse.Location = New System.Drawing.Point(358, 33)
        Me.adresse.Name = "adresse"
        Me.adresse.Size = New System.Drawing.Size(146, 20)
        Me.adresse.TabIndex = 210
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(551, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(41, 13)
        Me.Label3.TabIndex = 217
        Me.Label3.Text = "Email : "
        '
        'email
        '
        Me.email.Location = New System.Drawing.Point(599, 6)
        Me.email.Name = "email"
        Me.email.Size = New System.Drawing.Size(146, 20)
        Me.email.TabIndex = 215
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(320, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(33, 13)
        Me.Label4.TabIndex = 216
        Me.Label4.Text = "Fax : "
        '
        'fax
        '
        Me.fax.Location = New System.Drawing.Point(358, 85)
        Me.fax.Name = "fax"
        Me.fax.Size = New System.Drawing.Size(146, 20)
        Me.fax.TabIndex = 214
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(556, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(37, 13)
        Me.Label5.TabIndex = 221
        Me.Label5.Text = "TVA : "
        '
        'tva
        '
        Me.tva.Location = New System.Drawing.Point(599, 59)
        Me.tva.Name = "tva"
        Me.tva.Size = New System.Drawing.Size(146, 20)
        Me.tva.TabIndex = 219
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(557, 34)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 220
        Me.Label6.Text = "Site : "
        '
        'site
        '
        Me.site.Location = New System.Drawing.Point(599, 30)
        Me.site.Name = "site"
        Me.site.Size = New System.Drawing.Size(146, 20)
        Me.site.TabIndex = 218
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(548, 85)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 223
        Me.Label7.Text = "CNSS : "
        '
        'cnss
        '
        Me.cnss.Location = New System.Drawing.Point(599, 82)
        Me.cnss.Name = "cnss"
        Me.cnss.Size = New System.Drawing.Size(146, 20)
        Me.cnss.TabIndex = 222
        '
        'id
        '
        Me.id.AutoSize = True
        Me.id.Font = New System.Drawing.Font("Microsoft Sans Serif", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.id.Location = New System.Drawing.Point(13, 68)
        Me.id.Name = "id"
        Me.id.Size = New System.Drawing.Size(14, 4)
        Me.id.TabIndex = 224
        Me.id.Text = "Label8"
        Me.id.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(83, 116)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(30, 13)
        Me.Label8.TabIndex = 226
        Me.Label8.Text = "ICE :"
        '
        'sce
        '
        Me.sce.Location = New System.Drawing.Point(119, 113)
        Me.sce.Name = "sce"
        Me.sce.Size = New System.Drawing.Size(385, 20)
        Me.sce.TabIndex = 225
        '
        'Btn_Submit
        '
        Me.Btn_Submit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Submit.BackColor = System.Drawing.Color.White
        Me.Btn_Submit.Cursor = System.Windows.Forms.Cursors.Default
        Me.Btn_Submit.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Btn_Submit.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btn_Submit.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Btn_Submit.Image = Global.Simpl_TVA.My.Resources.Resources.add
        Me.Btn_Submit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Submit.Location = New System.Drawing.Point(835, 9)
        Me.Btn_Submit.Name = "Btn_Submit"
        Me.Btn_Submit.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Submit.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Submit.TabIndex = 199
        Me.Btn_Submit.Text = "&Nouveau"
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
        Me.Btn_Profil.Image = Global.Simpl_TVA.My.Resources.Resources.delete
        Me.Btn_Profil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Profil.Location = New System.Drawing.Point(835, 68)
        Me.Btn_Profil.Name = "Btn_Profil"
        Me.Btn_Profil.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Profil.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Profil.TabIndex = 201
        Me.Btn_Profil.Text = "&Supprimer"
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
        Me.Btn_Del.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.Btn_Del.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Btn_Del.Location = New System.Drawing.Point(835, 38)
        Me.Btn_Del.Name = "Btn_Del"
        Me.Btn_Del.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Btn_Del.Size = New System.Drawing.Size(87, 27)
        Me.Btn_Del.TabIndex = 200
        Me.Btn_Del.Text = "&Valider"
        Me.Btn_Del.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Btn_Del.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(540, 113)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 228
        Me.Label9.Text = "Régime :"
        '
        'TxtProrata
        '
        Me.TxtProrata.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TxtProrata.FormattingEnabled = True
        Me.TxtProrata.Items.AddRange(New Object() {"Mensuel", "Trimestriel"})
        Me.TxtProrata.Location = New System.Drawing.Point(599, 108)
        Me.TxtProrata.Name = "TxtProrata"
        Me.TxtProrata.Size = New System.Drawing.Size(146, 21)
        Me.TxtProrata.TabIndex = 231
        '
        'MiseaJourSociete
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(934, 420)
        Me.Controls.Add(Me.TxtProrata)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.sce)
        Me.Controls.Add(Me.id)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cnss)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.tva)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.site)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.email)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.fax)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.telephone)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.adresse)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Btn_Submit)
        Me.Controls.Add(Me.LabelConfirm)
        Me.Controls.Add(Me.patente)
        Me.Controls.Add(Me.Btn_Profil)
        Me.Controls.Add(Me.Btn_Del)
        Me.Controls.Add(Me.LabelUP)
        Me.Controls.Add(Me.identifiantfiscal)
        Me.Controls.Add(Me.LabelFN)
        Me.Controls.Add(Me.ville)
        Me.Controls.Add(Me.LabelLN)
        Me.Controls.Add(Me.pays)
        Me.Controls.Add(Me.LabelUN)
        Me.Controls.Add(Me.raisonsociale)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MiseaJourSociete"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gestion sociétés"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripStatusLabelVal As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Public WithEvents Btn_Submit As System.Windows.Forms.Button
    Friend WithEvents LabelConfirm As System.Windows.Forms.Label
    Friend WithEvents patente As System.Windows.Forms.TextBox
    Public WithEvents Btn_Profil As System.Windows.Forms.Button
    Public WithEvents Btn_Del As System.Windows.Forms.Button
    Friend WithEvents LabelUP As System.Windows.Forms.Label
    Friend WithEvents identifiantfiscal As System.Windows.Forms.TextBox
    Friend WithEvents LabelFN As System.Windows.Forms.Label
    Friend WithEvents ville As System.Windows.Forms.TextBox
    Friend WithEvents LabelLN As System.Windows.Forms.Label
    Friend WithEvents pays As System.Windows.Forms.TextBox
    Friend WithEvents LabelUN As System.Windows.Forms.Label
    Friend WithEvents raisonsociale As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents telephone As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents adresse As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents email As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents fax As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents tva As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents site As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cnss As System.Windows.Forms.TextBox
    Friend WithEvents id As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents sce As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtProrata As System.Windows.Forms.ComboBox
End Class
