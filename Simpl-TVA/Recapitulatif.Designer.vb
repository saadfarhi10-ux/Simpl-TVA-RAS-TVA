<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Recapitulatif
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Recapitulatif))
        Me.DataGridViewT = New System.Windows.Forms.DataGridView()
        CType(Me.DataGridViewT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridViewT
        '
        Me.DataGridViewT.AllowUserToAddRows = False
        Me.DataGridViewT.AllowUserToDeleteRows = False
        Me.DataGridViewT.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.DataGridViewT.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewT.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewT.Location = New System.Drawing.Point(0, 0)
        Me.DataGridViewT.Name = "DataGridViewT"
        Me.DataGridViewT.ReadOnly = True
        Me.DataGridViewT.Size = New System.Drawing.Size(817, 459)
        Me.DataGridViewT.TabIndex = 0
        '
        'Recapitulatif
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 459)
        Me.Controls.Add(Me.DataGridViewT)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Recapitulatif"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Récapitulatif"
        CType(Me.DataGridViewT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewT As System.Windows.Forms.DataGridView
End Class
