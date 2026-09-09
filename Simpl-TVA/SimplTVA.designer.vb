<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SimplTVA
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimplTVA))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LabelModeConnexion = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Final = New System.Windows.Forms.TabControl()
        Me.lecture = New System.Windows.Forms.TabPage()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.CmdCpt = New System.Windows.Forms.Button()
        Me.traitement = New System.Windows.Forms.TabPage()
        Me.LinkLabelRecap = New System.Windows.Forms.LinkLabel()
        Me.LinkLabelXsl = New System.Windows.Forms.LinkLabel()
        Me.LabelCountSums = New System.Windows.Forms.Label()
        Me.Grille = New System.Windows.Forms.DataGridView()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.generation = New System.Windows.Forms.TabPage()
        Me.CheckBoxZip = New System.Windows.Forms.CheckBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.CmdConfirmer = New System.Windows.Forms.Button()
        Me.ComboBoxTypeExport = New System.Windows.Forms.ComboBox()
        Me.LabelTypeExport = New System.Windows.Forms.Label()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TextBoxRapprt = New System.Windows.Forms.TextBox()
        Me.HistoriqueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LabelClient = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItemSoc = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemUsers = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemPwd = New System.Windows.Forms.ToolStripMenuItem()
        Me.GérerLaLicenceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DéconnexionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MiseÀJourToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabelCur = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelCurTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabelExperitaion = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.passercommandee = New MBGlassStyleButton.MBGlassButton()
        Me.Final.SuspendLayout()
        Me.lecture.SuspendLayout()
        Me.traitement.SuspendLayout()
        CType(Me.Grille, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.generation.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(136, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "License accordée à : "
        '
        'LabelModeConnexion
        '
        Me.LabelModeConnexion.AutoSize = True
        Me.LabelModeConnexion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelModeConnexion.ForeColor = System.Drawing.Color.Indigo
        Me.LabelModeConnexion.Location = New System.Drawing.Point(17, 42)
        Me.LabelModeConnexion.Name = "LabelModeConnexion"
        Me.LabelModeConnexion.Size = New System.Drawing.Size(100, 13)
        Me.LabelModeConnexion.TabIndex = 66
        Me.LabelModeConnexion.Text = "Mode : "
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(465, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 24)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Socièté : "
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(549, 14)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(252, 28)
        Me.ComboBox1.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(155, 191)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(117, 25)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Parcourir : "
        '
        'TextBox1
        '
        Me.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Location = New System.Drawing.Point(332, 195)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(295, 20)
        Me.TextBox1.TabIndex = 1
        '
        'Final
        '
        Me.Final.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Final.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.Final.Controls.Add(Me.lecture)
        Me.Final.Controls.Add(Me.traitement)
        Me.Final.Controls.Add(Me.generation)
        Me.Final.Controls.Add(Me.TabPage1)
        Me.Final.ImeMode = System.Windows.Forms.ImeMode.Hangul
        Me.Final.ItemSize = New System.Drawing.Size(120, 21)
        Me.Final.Location = New System.Drawing.Point(0, 64)
        Me.Final.Name = "Final"
        Me.Final.SelectedIndex = 0
        Me.Final.Size = New System.Drawing.Size(940, 536)
        Me.Final.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.Final.TabIndex = 5
        '
        'lecture
        '
        Me.lecture.BackColor = System.Drawing.Color.White
        Me.lecture.Controls.Add(Me.Button1)
        Me.lecture.Controls.Add(Me.MaskedTextBox1)
        Me.lecture.Controls.Add(Me.Button3)
        Me.lecture.Controls.Add(Me.CmdCpt)
        Me.lecture.Controls.Add(Me.TextBox1)
        Me.lecture.Controls.Add(Me.Label3)
        Me.lecture.Location = New System.Drawing.Point(4, 25)
        Me.lecture.Name = "lecture"
        Me.lecture.Size = New System.Drawing.Size(932, 507)
        Me.lecture.TabIndex = 4
        Me.lecture.Text = "Lecture de données"
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button1.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button1.Image = Global.Simpl_TVA.My.Resources.Resources.if_help_browser_118806
        Me.Button1.Location = New System.Drawing.Point(664, 195)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(25, 20)
        Me.Button1.TabIndex = 56
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = False
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.MaskedTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MaskedTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaskedTextBox1.Location = New System.Drawing.Point(332, 221)
        Me.MaskedTextBox1.Mask = "00/0000"
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(88, 31)
        Me.MaskedTextBox1.TabIndex = 55
        '
        'Button3
        '
        Me.Button3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button3.BackColor = System.Drawing.Color.White
        Me.Button3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button3.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button3.Location = New System.Drawing.Point(431, 221)
        Me.Button3.Name = "Button3"
        Me.Button3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button3.Size = New System.Drawing.Size(196, 31)
        Me.Button3.TabIndex = 54
        Me.Button3.Text = "Valider"
        Me.Button3.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button3.UseVisualStyleBackColor = False
        '
        'CmdCpt
        '
        Me.CmdCpt.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CmdCpt.BackColor = System.Drawing.Color.White
        Me.CmdCpt.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCpt.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CmdCpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdCpt.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCpt.Image = Global.Simpl_TVA.My.Resources.Resources.dossier
        Me.CmdCpt.Location = New System.Drawing.Point(633, 195)
        Me.CmdCpt.Name = "CmdCpt"
        Me.CmdCpt.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCpt.Size = New System.Drawing.Size(25, 20)
        Me.CmdCpt.TabIndex = 50
        Me.CmdCpt.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.CmdCpt.UseVisualStyleBackColor = False
        '
        'traitement
        '
        Me.traitement.BackColor = System.Drawing.Color.White
        Me.traitement.Controls.Add(Me.LinkLabelRecap)
        Me.traitement.Controls.Add(Me.LinkLabelXsl)
        Me.traitement.Controls.Add(Me.LabelCountSums)
        Me.traitement.Controls.Add(Me.Grille)
        Me.traitement.Controls.Add(Me.ComboBox2)
        Me.traitement.Controls.Add(Me.Button4)
        Me.traitement.Controls.Add(Me.Button7)
        Me.traitement.Location = New System.Drawing.Point(4, 25)
        Me.traitement.Name = "traitement"
        Me.traitement.Size = New System.Drawing.Size(932, 507)
        Me.traitement.TabIndex = 5
        Me.traitement.Text = "Traitement"
        '
        'LinkLabelRecap
        '
        Me.LinkLabelRecap.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LinkLabelRecap.AutoSize = True
        Me.LinkLabelRecap.Location = New System.Drawing.Point(33, 487)
        Me.LinkLabelRecap.Name = "LinkLabelRecap"
        Me.LinkLabelRecap.Size = New System.Drawing.Size(66, 13)
        Me.LinkLabelRecap.TabIndex = 62
        Me.LinkLabelRecap.TabStop = True
        Me.LinkLabelRecap.Text = "Récapitulatif"
        '
        'LinkLabelXsl
        '
        Me.LinkLabelXsl.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LinkLabelXsl.AutoSize = True
        Me.LinkLabelXsl.Location = New System.Drawing.Point(802, 30)
        Me.LinkLabelXsl.Name = "LinkLabelXsl"
        Me.LinkLabelXsl.Size = New System.Drawing.Size(97, 13)
        Me.LinkLabelXsl.TabIndex = 61
        Me.LinkLabelXsl.TabStop = True
        Me.LinkLabelXsl.Text = "Exporter vers excel"
        '
        'LabelCountSums
        '
        Me.LabelCountSums.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelCountSums.AutoSize = True
        Me.LabelCountSums.Location = New System.Drawing.Point(105, 487)
        Me.LabelCountSums.Name = "LabelCountSums"
        Me.LabelCountSums.Size = New System.Drawing.Size(31, 13)
        Me.LabelCountSums.TabIndex = 58
        Me.LabelCountSums.Text = "Total"
        '
        'Grille
        '
        Me.Grille.AllowUserToAddRows = False
        Me.Grille.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Grille.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None
        Me.Grille.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Grille.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Grille.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Grille.DefaultCellStyle = DataGridViewCellStyle2
        Me.Grille.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke
        Me.Grille.Location = New System.Drawing.Point(19, 51)
        Me.Grille.Name = "Grille"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(102, Byte), Integer), CType(CType(0, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Grille.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.Grille.RowHeadersVisible = False
        Me.Grille.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grille.Size = New System.Drawing.Size(880, 428)
        Me.Grille.TabIndex = 57
        '
        'ComboBox2
        '
        Me.ComboBox2.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {""})
        Me.ComboBox2.Location = New System.Drawing.Point(19, 15)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(104, 28)
        Me.ComboBox2.TabIndex = 56
        '
        'Button4
        '
        Me.Button4.BackColor = System.Drawing.Color.White
        Me.Button4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button4.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button4.Image = Global.Simpl_TVA.My.Resources.Resources.ok
        Me.Button4.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Button4.Location = New System.Drawing.Point(215, 16)
        Me.Button4.Name = "Button4"
        Me.Button4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button4.Size = New System.Drawing.Size(80, 27)
        Me.Button4.TabIndex = 55
        Me.Button4.Text = "Valider"
        Me.Button4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button4.UseVisualStyleBackColor = False
        '
        'Button7
        '
        Me.Button7.BackColor = System.Drawing.Color.White
        Me.Button7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button7.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button7.Image = Global.Simpl_TVA.My.Resources.Resources.cancel
        Me.Button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button7.Location = New System.Drawing.Point(129, 16)
        Me.Button7.Name = "Button7"
        Me.Button7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button7.Size = New System.Drawing.Size(80, 27)
        Me.Button7.TabIndex = 54
        Me.Button7.Text = "Anomalie"
        Me.Button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Button7.UseVisualStyleBackColor = False
        '
        'generation
        '
        Me.generation.BackColor = System.Drawing.Color.White
        Me.generation.Controls.Add(Me.CheckBoxZip)
        Me.generation.Controls.Add(Me.ComboBox3)
        Me.generation.Controls.Add(Me.TextBox3)
        Me.generation.Controls.Add(Me.Label6)
        Me.generation.Controls.Add(Me.Button6)
        Me.generation.Controls.Add(Me.CmdConfirmer)
        Me.generation.Controls.Add(Me.ComboBoxTypeExport)
        Me.generation.Controls.Add(Me.LabelTypeExport)
        Me.generation.Location = New System.Drawing.Point(4, 25)
        Me.generation.Name = "generation"
        Me.generation.Padding = New System.Windows.Forms.Padding(3)
        Me.generation.Size = New System.Drawing.Size(932, 507)
        Me.generation.TabIndex = 6
        Me.generation.Text = "Génération"
        '
        'CheckBoxZip
        '
        Me.CheckBoxZip.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CheckBoxZip.AutoSize = True
        Me.CheckBoxZip.Checked = True
        Me.CheckBoxZip.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxZip.Location = New System.Drawing.Point(656, 196)
        Me.CheckBoxZip.Name = "CheckBoxZip"
        Me.CheckBoxZip.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxZip.TabIndex = 62
        Me.CheckBoxZip.Text = "Compressé"
        Me.CheckBoxZip.UseVisualStyleBackColor = True
        '
        'ComboBox3
        '
        Me.ComboBox3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBox3.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox3.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {""})
        Me.ComboBox3.Location = New System.Drawing.Point(324, 226)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(104, 32)
        Me.ComboBox3.TabIndex = 59
        '
        'TextBox3
        '
        Me.TextBox3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.TextBox3.BackColor = System.Drawing.Color.White
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Location = New System.Drawing.Point(324, 195)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(295, 20)
        Me.TextBox3.TabIndex = 57
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(147, 191)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(181, 25)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Enregistrer sous :"
        '
        'Button6
        '
        Me.Button6.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Button6.BackColor = System.Drawing.Color.White
        Me.Button6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Button6.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Button6.Image = Global.Simpl_TVA.My.Resources.Resources.dossier
        Me.Button6.Location = New System.Drawing.Point(625, 195)
        Me.Button6.Name = "Button6"
        Me.Button6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button6.Size = New System.Drawing.Size(25, 20)
        Me.Button6.TabIndex = 58
        Me.Button6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button6.UseVisualStyleBackColor = False
        '
        'CmdConfirmer
        '
        Me.CmdConfirmer.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.CmdConfirmer.BackColor = System.Drawing.Color.White
        Me.CmdConfirmer.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdConfirmer.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.CmdConfirmer.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CmdConfirmer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdConfirmer.Location = New System.Drawing.Point(434, 226)
        Me.CmdConfirmer.Name = "CmdConfirmer"
        Me.CmdConfirmer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdConfirmer.Size = New System.Drawing.Size(110, 30)
        Me.CmdConfirmer.TabIndex = 52
        Me.CmdConfirmer.Text = "Générer"
        Me.CmdConfirmer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdConfirmer.UseVisualStyleBackColor = False
        '
        'LabelTypeExport
        '
        Me.LabelTypeExport.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.LabelTypeExport.AutoSize = True
        Me.LabelTypeExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelTypeExport.ForeColor = System.Drawing.Color.Black
        Me.LabelTypeExport.Location = New System.Drawing.Point(147, 274)
        Me.LabelTypeExport.Name = "LabelTypeExport"
        Me.LabelTypeExport.Size = New System.Drawing.Size(150, 25)
        Me.LabelTypeExport.TabIndex = 65
        Me.LabelTypeExport.Text = "Type d'export :"
        '
        'ComboBoxTypeExport
        '
        Me.ComboBoxTypeExport.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.ComboBoxTypeExport.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.ComboBoxTypeExport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxTypeExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.ComboBoxTypeExport.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBoxTypeExport.FormattingEnabled = True
        Me.ComboBoxTypeExport.Items.AddRange(New Object() {"TVA", "TVA RAS"})
        Me.ComboBoxTypeExport.Location = New System.Drawing.Point(324, 270)
        Me.ComboBoxTypeExport.Name = "ComboBoxTypeExport"
        Me.ComboBoxTypeExport.Size = New System.Drawing.Size(195, 32)
        Me.ComboBoxTypeExport.TabIndex = 63
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TextBoxRapprt)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(932, 507)
        Me.TabPage1.TabIndex = 7
        Me.TabPage1.Text = "Rapport"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TextBoxRapprt
        '
        Me.TextBoxRapprt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextBoxRapprt.Location = New System.Drawing.Point(3, 3)
        Me.TextBoxRapprt.Multiline = True
        Me.TextBoxRapprt.Name = "TextBoxRapprt"
        Me.TextBoxRapprt.ReadOnly = True
        Me.TextBoxRapprt.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TextBoxRapprt.Size = New System.Drawing.Size(926, 501)
        Me.TextBoxRapprt.TabIndex = 0
        '
        'HistoriqueToolStripMenuItem
        '
        Me.HistoriqueToolStripMenuItem.Name = "HistoriqueToolStripMenuItem"
        Me.HistoriqueToolStripMenuItem.Size = New System.Drawing.Size(97, 22)
        Me.HistoriqueToolStripMenuItem.Text = "&Historique"
        '
        'LabelClient
        '
        Me.LabelClient.AutoSize = True
        Me.LabelClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelClient.ForeColor = System.Drawing.Color.Red
        Me.LabelClient.Location = New System.Drawing.Point(150, 22)
        Me.LabelClient.Name = "LabelClient"
        Me.LabelClient.Size = New System.Drawing.Size(129, 16)
        Me.LabelClient.TabIndex = 6
        Me.LabelClient.Text = "Produit non sérialisé"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 2.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(340, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 4)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Hamza"
        Me.Label4.Visible = False
        '
        'SaveFileDialog1
        '
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemSoc, Me.ToolStripMenuItemUsers, Me.ToolStripMenuItemPwd, Me.GérerLaLicenceToolStripMenuItem, Me.DéconnexionToolStripMenuItem, Me.MiseÀJourToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 158)
        Me.ContextMenuStrip1.Text = "Société"
        '
        'ToolStripMenuItemSoc
        '
        Me.ToolStripMenuItemSoc.Name = "ToolStripMenuItemSoc"
        Me.ToolStripMenuItemSoc.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItemSoc.Text = "Sociétés"
        '
        'ToolStripMenuItemUsers
        '
        Me.ToolStripMenuItemUsers.Name = "ToolStripMenuItemUsers"
        Me.ToolStripMenuItemUsers.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItemUsers.Text = "Utilisateurs"
        '
        'ToolStripMenuItemPwd
        '
        Me.ToolStripMenuItemPwd.Name = "ToolStripMenuItemPwd"
        Me.ToolStripMenuItemPwd.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItemPwd.Text = "Mot de passe"
        '
        'GérerLaLicenceToolStripMenuItem
        '
        Me.GérerLaLicenceToolStripMenuItem.Name = "GérerLaLicenceToolStripMenuItem"
        Me.GérerLaLicenceToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.GérerLaLicenceToolStripMenuItem.Text = "Licence"
        '
        'DéconnexionToolStripMenuItem
        '
        Me.DéconnexionToolStripMenuItem.Name = "DéconnexionToolStripMenuItem"
        Me.DéconnexionToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DéconnexionToolStripMenuItem.Text = "Déconnexion"
        '
        'MiseÀJourToolStripMenuItem
        '
        Me.MiseÀJourToolStripMenuItem.Name = "MiseÀJourToolStripMenuItem"
        Me.MiseÀJourToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.MiseÀJourToolStripMenuItem.Text = "Mise à jour"
        '
        'Timer1
        '
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabelCur, Me.ToolStripStatusLabelCurTime, Me.ToolStripStatusLabelExperitaion})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 603)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(944, 22)
        Me.StatusStrip1.TabIndex = 28
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabelCur
        '
        Me.ToolStripStatusLabelCur.Name = "ToolStripStatusLabelCur"
        Me.ToolStripStatusLabelCur.Size = New System.Drawing.Size(69, 17)
        Me.ToolStripStatusLabelCur.Text = "Current User"
        '
        'ToolStripStatusLabelCurTime
        '
        Me.ToolStripStatusLabelCurTime.Name = "ToolStripStatusLabelCurTime"
        Me.ToolStripStatusLabelCurTime.Size = New System.Drawing.Size(69, 17)
        Me.ToolStripStatusLabelCurTime.Text = "Current Time"
        '
        'ToolStripStatusLabelExperitaion
        '
        Me.ToolStripStatusLabelExperitaion.Name = "ToolStripStatusLabelExperitaion"
        Me.ToolStripStatusLabelExperitaion.Size = New System.Drawing.Size(48, 17)
        Me.ToolStripStatusLabelExperitaion.Text = "DateExp"
        Me.ToolStripStatusLabelExperitaion.Visible = False
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.RightToLeft = True
        '
        'passercommandee
        '
        Me.passercommandee.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.passercommandee.Arrow = MBGlassStyleButton.MBGlassButton.MB_Arrow.ToRight
        Me.passercommandee.BackColor = System.Drawing.Color.Ivory
        Me.passercommandee.BaseColor = System.Drawing.Color.Ivory
        Me.passercommandee.BaseStrokeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(77, Byte), Integer))
        Me.passercommandee.ContextMenuStrip = Me.ContextMenuStrip1
        Me.passercommandee.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight
        Me.passercommandee.FlatAppearance.BorderSize = 0
        Me.passercommandee.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.passercommandee.Image = CType(resources.GetObject("passercommandee.Image"), System.Drawing.Image)
        Me.passercommandee.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.passercommandee.ImageSize = New System.Drawing.Size(24, 24)
        Me.passercommandee.Location = New System.Drawing.Point(807, 14)
        Me.passercommandee.MenuListPosition = New System.Drawing.Point(0, 0)
        Me.passercommandee.Name = "passercommandee"
        Me.passercommandee.OnColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.passercommandee.OnStrokeColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(177, Byte), Integer), CType(CType(118, Byte), Integer))
        Me.passercommandee.PressColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.passercommandee.PressStrokeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.passercommandee.Radius = 2
        Me.passercommandee.Size = New System.Drawing.Size(125, 30)
        Me.passercommandee.SplitButton = MBGlassStyleButton.MBGlassButton.MB_SplitButton.Yes
        Me.passercommandee.SplitDistance = 15
        Me.passercommandee.SplitLocation = MBGlassStyleButton.MBGlassButton.MB_SplitLocation.Right
        Me.passercommandee.TabIndex = 27
        Me.passercommandee.Text = "Paramétrage"
        Me.passercommandee.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.passercommandee.UseVisualStyleBackColor = False
        '
        'SimplTVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(944, 625)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.passercommandee)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LabelClient)
        Me.Controls.Add(Me.Final)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelModeConnexion)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SimplTVA"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Simpl-TVA"
        Me.Final.ResumeLayout(False)
        Me.lecture.ResumeLayout(False)
        Me.lecture.PerformLayout()
        Me.traitement.ResumeLayout(False)
        Me.traitement.PerformLayout()
        CType(Me.Grille, System.ComponentModel.ISupportInitialize).EndInit()
        Me.generation.ResumeLayout(False)
        Me.generation.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LabelModeConnexion As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Final As System.Windows.Forms.TabControl
    Friend WithEvents lecture As System.Windows.Forms.TabPage
    Friend WithEvents traitement As System.Windows.Forms.TabPage
    Friend WithEvents generation As System.Windows.Forms.TabPage
    Friend WithEvents LabelClient As System.Windows.Forms.Label
    Public WithEvents CmdCpt As System.Windows.Forms.Button
    Public WithEvents Button3 As System.Windows.Forms.Button
    Public WithEvents Button7 As System.Windows.Forms.Button
    Public WithEvents CmdConfirmer As System.Windows.Forms.Button
    Public WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents HistoriqueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Grille As System.Windows.Forms.DataGridView
    Friend WithEvents passercommandee As MBGlassStyleButton.MBGlassButton
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItemSoc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemPwd As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabelCur As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabelCurTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GérerLaLicenceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DéconnexionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripStatusLabelExperitaion As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LabelCountSums As System.Windows.Forms.Label
    Friend WithEvents CheckBoxZip As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxTypeExport As System.Windows.Forms.ComboBox
    Friend WithEvents LabelTypeExport As System.Windows.Forms.Label
    Friend WithEvents MiseÀJourToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkLabelXsl As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabelRecap As System.Windows.Forms.LinkLabel
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TextBoxRapprt As System.Windows.Forms.TextBox
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
    Public WithEvents Button1 As System.Windows.Forms.Button

End Class