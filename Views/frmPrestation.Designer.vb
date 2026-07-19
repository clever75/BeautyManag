<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrestation
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
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrestation))
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        pnlHeaderForm = New Guna.UI2.WinForms.Guna2Panel()
        lblSousTitreForm = New Label()
        lblTitreForm = New Label()
        pnlFooterForm = New Guna.UI2.WinForms.Guna2Panel()
        btnEnregistrerForm = New Guna.UI2.WinForms.Guna2Button()
        btnAnnulerForm = New Guna.UI2.WinForms.Guna2Button()
        tlpChamps = New TableLayoutPanel()
        txtDuree = New Guna.UI2.WinForms.Guna2TextBox()
        lblDuree = New Label()
        txtPrix = New Guna.UI2.WinForms.Guna2TextBox()
        lblPrix = New Label()
        txtDescription = New Guna.UI2.WinForms.Guna2TextBox()
        lblDescription = New Label()
        lblCategorie = New Label()
        lblNom = New Label()
        txtNomPrestation = New Guna.UI2.WinForms.Guna2TextBox()
        cboCategorieForm = New ComboBox()
        lblStatut = New Label()
        tglActifForm = New Guna.UI2.WinForms.Guna2ToggleSwitch()
        pnlHeaderForm.SuspendLayout()
        pnlFooterForm.SuspendLayout()
        tlpChamps.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlHeaderForm
        ' 
        pnlHeaderForm.Controls.Add(lblSousTitreForm)
        pnlHeaderForm.Controls.Add(lblTitreForm)
        pnlHeaderForm.CustomizableEdges = CustomizableEdges1
        pnlHeaderForm.Dock = DockStyle.Top
        pnlHeaderForm.FillColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        pnlHeaderForm.Location = New Point(0, 0)
        pnlHeaderForm.Name = "pnlHeaderForm"
        pnlHeaderForm.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        pnlHeaderForm.Size = New Size(462, 65)
        pnlHeaderForm.TabIndex = 0
        ' 
        ' lblSousTitreForm
        ' 
        lblSousTitreForm.AutoSize = True
        lblSousTitreForm.BackColor = Color.Transparent
        lblSousTitreForm.ForeColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        lblSousTitreForm.Location = New Point(20, 45)
        lblSousTitreForm.Name = "lblSousTitreForm"
        lblSousTitreForm.Size = New Size(193, 20)
        lblSousTitreForm.TabIndex = 1
        lblSousTitreForm.Text = "Remplissez les informations"
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.BackColor = Color.Transparent
        lblTitreForm.Font = New Font("Segoe UI", 13.2000008F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitreForm.ForeColor = Color.White
        lblTitreForm.Location = New Point(20, 14)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Size = New Size(226, 31)
        lblTitreForm.TabIndex = 0
        lblTitreForm.Text = "Nouvelle prestation"
        ' 
        ' pnlFooterForm
        ' 
        pnlFooterForm.Controls.Add(btnEnregistrerForm)
        pnlFooterForm.Controls.Add(btnAnnulerForm)
        pnlFooterForm.CustomizableEdges = CustomizableEdges7
        pnlFooterForm.Dock = DockStyle.Bottom
        pnlFooterForm.FillColor = Color.White
        pnlFooterForm.Location = New Point(0, 425)
        pnlFooterForm.Name = "pnlFooterForm"
        pnlFooterForm.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        pnlFooterForm.Size = New Size(462, 58)
        pnlFooterForm.TabIndex = 1
        ' 
        ' btnEnregistrerForm
        ' 
        btnEnregistrerForm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnEnregistrerForm.BorderRadius = 9
        btnEnregistrerForm.Cursor = Cursors.Hand
        btnEnregistrerForm.CustomizableEdges = CustomizableEdges3
        btnEnregistrerForm.DisabledState.BorderColor = Color.DarkGray
        btnEnregistrerForm.DisabledState.CustomBorderColor = Color.DarkGray
        btnEnregistrerForm.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnEnregistrerForm.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnEnregistrerForm.FillColor = Color.FromArgb(CByte(196), CByte(90), CByte(126))
        btnEnregistrerForm.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnEnregistrerForm.ForeColor = Color.White
        btnEnregistrerForm.HoverState.FillColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        btnEnregistrerForm.Image = CType(resources.GetObject("btnEnregistrerForm.Image"), Image)
        btnEnregistrerForm.ImageSize = New Size(16, 16)
        btnEnregistrerForm.Location = New Point(294, 8)
        btnEnregistrerForm.Name = "btnEnregistrerForm"
        btnEnregistrerForm.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        btnEnregistrerForm.Size = New Size(140, 38)
        btnEnregistrerForm.TabIndex = 1
        btnEnregistrerForm.Text = "Enregistrer"
        ' 
        ' btnAnnulerForm
        ' 
        btnAnnulerForm.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnAnnulerForm.BorderRadius = 9
        btnAnnulerForm.Cursor = Cursors.Hand
        btnAnnulerForm.CustomizableEdges = CustomizableEdges5
        btnAnnulerForm.DisabledState.BorderColor = Color.DarkGray
        btnAnnulerForm.DisabledState.CustomBorderColor = Color.DarkGray
        btnAnnulerForm.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnAnnulerForm.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnAnnulerForm.FillColor = Color.WhiteSmoke
        btnAnnulerForm.Font = New Font("Segoe UI", 9.0F)
        btnAnnulerForm.ForeColor = Color.FromArgb(CByte(136), CByte(135), CByte(128))
        btnAnnulerForm.Location = New Point(196, 8)
        btnAnnulerForm.Name = "btnAnnulerForm"
        btnAnnulerForm.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        btnAnnulerForm.Size = New Size(90, 38)
        btnAnnulerForm.TabIndex = 0
        btnAnnulerForm.Text = "Annuler"
        ' 
        ' tlpChamps
        ' 
        tlpChamps.BackColor = Color.Transparent
        tlpChamps.ColumnCount = 2
        tlpChamps.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 110.0F))
        tlpChamps.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100.0F))
        tlpChamps.Controls.Add(txtDuree, 1, 4)
        tlpChamps.Controls.Add(lblDuree, 0, 4)
        tlpChamps.Controls.Add(txtPrix, 1, 3)
        tlpChamps.Controls.Add(lblPrix, 0, 3)
        tlpChamps.Controls.Add(txtDescription, 1, 2)
        tlpChamps.Controls.Add(lblDescription, 0, 2)
        tlpChamps.Controls.Add(lblCategorie, 0, 1)
        tlpChamps.Controls.Add(lblNom, 0, 0)
        tlpChamps.Controls.Add(txtNomPrestation, 1, 0)
        tlpChamps.Controls.Add(cboCategorieForm, 1, 1)
        tlpChamps.Dock = DockStyle.Fill
        tlpChamps.Location = New Point(0, 65)
        tlpChamps.Name = "tlpChamps"
        tlpChamps.Padding = New Padding(20, 14, 20, 10)
        tlpChamps.RowCount = 5
        tlpChamps.RowStyles.Add(New RowStyle(SizeType.Absolute, 52.0F))
        tlpChamps.RowStyles.Add(New RowStyle(SizeType.Absolute, 52.0F))
        tlpChamps.RowStyles.Add(New RowStyle(SizeType.Absolute, 80.0F))
        tlpChamps.RowStyles.Add(New RowStyle(SizeType.Absolute, 52.0F))
        tlpChamps.RowStyles.Add(New RowStyle(SizeType.Absolute, 52.0F))
        tlpChamps.Size = New Size(462, 360)
        tlpChamps.TabIndex = 2
        ' 
        ' txtDuree
        ' 
        txtDuree.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtDuree.BorderRadius = 8
        txtDuree.CustomizableEdges = CustomizableEdges9
        txtDuree.DefaultText = ""
        txtDuree.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtDuree.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtDuree.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtDuree.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtDuree.Dock = DockStyle.Fill
        txtDuree.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        txtDuree.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtDuree.Font = New Font("Segoe UI", 9.0F)
        txtDuree.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtDuree.Location = New Point(130, 260)
        txtDuree.Margin = New Padding(0, 10, 0, 10)
        txtDuree.Name = "txtDuree"
        txtDuree.PlaceholderText = "Ex : 60 (= 1h 00min)"
        txtDuree.SelectedText = ""
        txtDuree.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        txtDuree.Size = New Size(312, 80)
        txtDuree.TabIndex = 9
        ' 
        ' lblDuree
        ' 
        lblDuree.AutoSize = True
        lblDuree.Dock = DockStyle.Fill
        lblDuree.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblDuree.Location = New Point(23, 250)
        lblDuree.Name = "lblDuree"
        lblDuree.Padding = New Padding(0, 0, 12, 0)
        lblDuree.Size = New Size(104, 100)
        lblDuree.TabIndex = 8
        lblDuree.Text = "Durée (min) *"
        lblDuree.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtPrix
        ' 
        txtPrix.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtPrix.BorderRadius = 8
        txtPrix.CustomizableEdges = CustomizableEdges11
        txtPrix.DefaultText = ""
        txtPrix.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtPrix.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtPrix.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPrix.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtPrix.Dock = DockStyle.Fill
        txtPrix.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        txtPrix.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtPrix.Font = New Font("Segoe UI", 9.0F)
        txtPrix.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtPrix.Location = New Point(130, 208)
        txtPrix.Margin = New Padding(0, 10, 0, 10)
        txtPrix.Name = "txtPrix"
        txtPrix.PlaceholderText = ""
        txtPrix.SelectedText = ""
        txtPrix.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        txtPrix.Size = New Size(312, 32)
        txtPrix.TabIndex = 7
        ' 
        ' lblPrix
        ' 
        lblPrix.AutoSize = True
        lblPrix.Dock = DockStyle.Fill
        lblPrix.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblPrix.Location = New Point(23, 198)
        lblPrix.Name = "lblPrix"
        lblPrix.Padding = New Padding(0, 0, 12, 0)
        lblPrix.Size = New Size(104, 52)
        lblPrix.TabIndex = 6
        lblPrix.Text = "Prix (FCA) *"
        lblPrix.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtDescription
        ' 
        txtDescription.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtDescription.BorderRadius = 8
        txtDescription.CustomizableEdges = CustomizableEdges13
        txtDescription.DefaultText = ""
        txtDescription.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtDescription.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtDescription.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtDescription.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtDescription.Dock = DockStyle.Fill
        txtDescription.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        txtDescription.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtDescription.Font = New Font("Segoe UI", 9.0F)
        txtDescription.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtDescription.Location = New Point(130, 124)
        txtDescription.Margin = New Padding(0, 6, 0, 6)
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.PlaceholderText = "Ex : Avec mèches incluses . . ."
        txtDescription.SelectedText = ""
        txtDescription.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        txtDescription.Size = New Size(312, 68)
        txtDescription.TabIndex = 5
        ' 
        ' lblDescription
        ' 
        lblDescription.AutoSize = True
        lblDescription.Dock = DockStyle.Fill
        lblDescription.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblDescription.Location = New Point(23, 118)
        lblDescription.Name = "lblDescription"
        lblDescription.Padding = New Padding(0, 12, 12, 0)
        lblDescription.Size = New Size(104, 80)
        lblDescription.TabIndex = 4
        lblDescription.Text = "Description"
        lblDescription.TextAlign = ContentAlignment.TopRight
        ' 
        ' lblCategorie
        ' 
        lblCategorie.AutoSize = True
        lblCategorie.Dock = DockStyle.Fill
        lblCategorie.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblCategorie.Location = New Point(23, 66)
        lblCategorie.Name = "lblCategorie"
        lblCategorie.Padding = New Padding(0, 0, 12, 0)
        lblCategorie.Size = New Size(104, 52)
        lblCategorie.TabIndex = 2
        lblCategorie.Text = "Catégorie *"
        lblCategorie.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblNom
        ' 
        lblNom.AutoSize = True
        lblNom.Dock = DockStyle.Fill
        lblNom.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblNom.Location = New Point(23, 14)
        lblNom.Name = "lblNom"
        lblNom.Padding = New Padding(0, 0, 12, 0)
        lblNom.Size = New Size(104, 52)
        lblNom.TabIndex = 0
        lblNom.Text = "Nom *"
        lblNom.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtNomPrestation
        ' 
        txtNomPrestation.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtNomPrestation.BorderRadius = 8
        txtNomPrestation.CustomizableEdges = CustomizableEdges15
        txtNomPrestation.DefaultText = ""
        txtNomPrestation.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtNomPrestation.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtNomPrestation.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtNomPrestation.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtNomPrestation.Dock = DockStyle.Fill
        txtNomPrestation.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        txtNomPrestation.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtNomPrestation.Font = New Font("Segoe UI", 9.0F)
        txtNomPrestation.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtNomPrestation.Location = New Point(130, 24)
        txtNomPrestation.Margin = New Padding(0, 10, 0, 10)
        txtNomPrestation.Name = "txtNomPrestation"
        txtNomPrestation.PlaceholderText = "Ex : Tressage, vanilles, Gel UV "
        txtNomPrestation.SelectedText = ""
        txtNomPrestation.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        txtNomPrestation.Size = New Size(312, 32)
        txtNomPrestation.TabIndex = 1
        ' 
        ' cboCategorieForm
        ' 
        cboCategorieForm.Dock = DockStyle.Fill
        cboCategorieForm.FormattingEnabled = True
        cboCategorieForm.Location = New Point(130, 76)
        cboCategorieForm.Margin = New Padding(0, 10, 0, 10)
        cboCategorieForm.Name = "cboCategorieForm"
        cboCategorieForm.Size = New Size(312, 28)
        cboCategorieForm.TabIndex = 13
        ' 
        ' lblStatut
        ' 
        lblStatut.AutoSize = True
        lblStatut.Dock = DockStyle.Fill
        lblStatut.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblStatut.Location = New Point(23, 302)
        lblStatut.Name = "lblStatut"
        lblStatut.Padding = New Padding(0, 0, 12, 0)
        lblStatut.Size = New Size(104, 52)
        lblStatut.TabIndex = 10
        lblStatut.Text = "Statut"
        lblStatut.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' tglActifForm
        ' 
        tglActifForm.Checked = True
        tglActifForm.CheckedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        tglActifForm.CheckedState.FillColor = Color.FromArgb(CByte(196), CByte(90), CByte(126))
        tglActifForm.CheckedState.InnerBorderColor = Color.White
        tglActifForm.CheckedState.InnerColor = Color.White
        tglActifForm.CustomizableEdges = CustomizableEdges17
        tglActifForm.Location = New Point(142, 302)
        tglActifForm.Margin = New Padding(12, 0, 0, 0)
        tglActifForm.Name = "tglActifForm"
        tglActifForm.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        tglActifForm.Size = New Size(44, 25)
        tglActifForm.TabIndex = 12
        tglActifForm.UncheckedState.BorderColor = Color.FromArgb(CByte(125), CByte(137), CByte(149))
        tglActifForm.UncheckedState.FillColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        tglActifForm.UncheckedState.InnerBorderColor = Color.White
        tglActifForm.UncheckedState.InnerColor = Color.White
        ' 
        ' frmPrestation
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(249))
        ClientSize = New Size(462, 483)
        Controls.Add(tlpChamps)
        Controls.Add(pnlFooterForm)
        Controls.Add(pnlHeaderForm)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmPrestation"
        StartPosition = FormStartPosition.CenterParent
        Text = "Prestation"
        pnlHeaderForm.ResumeLayout(False)
        pnlHeaderForm.PerformLayout()
        pnlFooterForm.ResumeLayout(False)
        tlpChamps.ResumeLayout(False)
        tlpChamps.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeaderForm As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents lblSousTitreForm As Label
    Friend WithEvents pnlFooterForm As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnAnnulerForm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnEnregistrerForm As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents tlpChamps As TableLayoutPanel
    Friend WithEvents lblNom As Label
    Friend WithEvents txtNomPrestation As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblStatut As Label
    Friend WithEvents txtDuree As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblDuree As Label
    Friend WithEvents txtPrix As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblPrix As Label
    Friend WithEvents txtDescription As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblDescription As Label
    Friend WithEvents lblCategorie As Label
    Friend WithEvents tglActifForm As Guna.UI2.WinForms.Guna2ToggleSwitch
    Friend WithEvents cboCategorieForm As ComboBox
End Class
