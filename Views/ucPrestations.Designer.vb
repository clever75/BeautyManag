<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucPrestations
    Inherits System.Windows.Forms.UserControl

    'UserControl remplace la méthode Dispose pour nettoyer la liste des composants.
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
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ucPrestations))
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        pnlHeader = New Guna.UI2.WinForms.Guna2Panel()
        btnNouvellePrestation = New Guna.UI2.WinForms.Guna2Button()
        lblDate = New Label()
        lblSousTitre = New Label()
        lblTitre = New Label()
        pnlFiltres = New Guna.UI2.WinForms.Guna2Panel()
        txtRecherche = New Guna.UI2.WinForms.Guna2TextBox()
        cboCategorie = New Guna.UI2.WinForms.Guna2ComboBox()
        lblFiltreCategorie = New Label()
        pnlCartes = New Panel()
        pnlHeader.SuspendLayout()
        pnlFiltres.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlHeader
        ' 
        pnlHeader.BackColor = Color.White
        pnlHeader.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        pnlHeader.BorderThickness = 2
        pnlHeader.Controls.Add(btnNouvellePrestation)
        pnlHeader.Controls.Add(lblDate)
        pnlHeader.Controls.Add(lblSousTitre)
        pnlHeader.Controls.Add(lblTitre)
        pnlHeader.CustomizableEdges = CustomizableEdges3
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.FillColor = Color.White
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.ShadowDecoration.Color = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        pnlHeader.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        pnlHeader.ShadowDecoration.Depth = 4
        pnlHeader.ShadowDecoration.Enabled = True
        pnlHeader.ShadowDecoration.Shadow = New Padding(0, 0, 6, 0)
        pnlHeader.Size = New Size(1176, 90)
        pnlHeader.TabIndex = 2
        ' 
        ' btnNouvellePrestation
        ' 
        btnNouvellePrestation.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        btnNouvellePrestation.BorderRadius = 9
        btnNouvellePrestation.Cursor = Cursors.Hand
        btnNouvellePrestation.CustomizableEdges = CustomizableEdges1
        btnNouvellePrestation.DisabledState.BorderColor = Color.DarkGray
        btnNouvellePrestation.DisabledState.CustomBorderColor = Color.DarkGray
        btnNouvellePrestation.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnNouvellePrestation.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnNouvellePrestation.FillColor = Color.FromArgb(CByte(196), CByte(90), CByte(126))
        btnNouvellePrestation.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnNouvellePrestation.ForeColor = Color.White
        btnNouvellePrestation.HoverState.FillColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        btnNouvellePrestation.Image = CType(resources.GetObject("btnNouvellePrestation.Image"), Image)
        btnNouvellePrestation.ImageAlign = HorizontalAlignment.Left
        btnNouvellePrestation.ImageSize = New Size(18, 18)
        btnNouvellePrestation.Location = New Point(926, 23)
        btnNouvellePrestation.Name = "btnNouvellePrestation"
        btnNouvellePrestation.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        btnNouvellePrestation.Size = New Size(233, 38)
        btnNouvellePrestation.TabIndex = 3
        btnNouvellePrestation.Text = "Nouvelle prestation"
        btnNouvellePrestation.TextAlign = HorizontalAlignment.Left
        btnNouvellePrestation.TextOffset = New Point(8, 0)
        ' 
        ' lblDate
        ' 
        lblDate.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblDate.AutoSize = True
        lblDate.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblDate.Location = New Point(783, 41)
        lblDate.Name = "lblDate"
        lblDate.Size = New Size(41, 20)
        lblDate.TabIndex = 2
        lblDate.Text = "Date"
        ' 
        ' lblSousTitre
        ' 
        lblSousTitre.AutoSize = True
        lblSousTitre.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblSousTitre.Location = New Point(24, 52)
        lblSousTitre.Name = "lblSousTitre"
        lblSousTitre.Size = New Size(241, 20)
        lblSousTitre.TabIndex = 1
        lblSousTitre.Text = "Catalogue des prestations du salon"
        ' 
        ' lblTitre
        ' 
        lblTitre.AutoSize = True
        lblTitre.BackColor = Color.Transparent
        lblTitre.Font = New Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitre.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        lblTitre.Location = New Point(24, 14)
        lblTitre.Name = "lblTitre"
        lblTitre.Size = New Size(163, 38)
        lblTitre.TabIndex = 0
        lblTitre.Text = "Prestations"
        ' 
        ' pnlFiltres
        ' 
        pnlFiltres.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        pnlFiltres.BorderThickness = 2
        pnlFiltres.Controls.Add(txtRecherche)
        pnlFiltres.Controls.Add(cboCategorie)
        pnlFiltres.Controls.Add(lblFiltreCategorie)
        pnlFiltres.CustomizableEdges = CustomizableEdges9
        pnlFiltres.Dock = DockStyle.Top
        pnlFiltres.FillColor = Color.White
        pnlFiltres.Location = New Point(0, 90)
        pnlFiltres.Name = "pnlFiltres"
        pnlFiltres.ShadowDecoration.Color = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        pnlFiltres.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        pnlFiltres.ShadowDecoration.Depth = 3
        pnlFiltres.ShadowDecoration.Enabled = True
        pnlFiltres.ShadowDecoration.Shadow = New Padding(0, 0, 5, 5)
        pnlFiltres.Size = New Size(1176, 70)
        pnlFiltres.TabIndex = 3
        ' 
        ' txtRecherche
        ' 
        txtRecherche.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtRecherche.BorderRadius = 8
        txtRecherche.CustomizableEdges = CustomizableEdges5
        txtRecherche.DefaultText = ""
        txtRecherche.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtRecherche.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtRecherche.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtRecherche.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtRecherche.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        txtRecherche.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtRecherche.Font = New Font("Segoe UI", 9F)
        txtRecherche.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtRecherche.IconLeft = CType(resources.GetObject("txtRecherche.IconLeft"), Image)
        txtRecherche.Location = New Point(326, 10)
        txtRecherche.Margin = New Padding(3, 4, 3, 4)
        txtRecherche.Name = "txtRecherche"
        txtRecherche.PlaceholderText = "Rechercher une prestation . . "
        txtRecherche.SelectedText = ""
        txtRecherche.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        txtRecherche.Size = New Size(348, 38)
        txtRecherche.TabIndex = 2
        ' 
        ' cboCategorie
        ' 
        cboCategorie.BackColor = Color.Transparent
        cboCategorie.BorderColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        cboCategorie.BorderRadius = 8
        cboCategorie.CustomizableEdges = CustomizableEdges7
        cboCategorie.DrawMode = DrawMode.OwnerDrawFixed
        cboCategorie.DropDownStyle = ComboBoxStyle.DropDownList
        cboCategorie.FillColor = Color.FromArgb(CByte(255), CByte(245), CByte(248))
        cboCategorie.FocusedColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        cboCategorie.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        cboCategorie.Font = New Font("Segoe UI", 10F)
        cboCategorie.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        cboCategorie.ItemHeight = 30
        cboCategorie.Location = New Point(111, 10)
        cboCategorie.Name = "cboCategorie"
        cboCategorie.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        cboCategorie.Size = New Size(200, 36)
        cboCategorie.TabIndex = 1
        ' 
        ' lblFiltreCategorie
        ' 
        lblFiltreCategorie.AutoSize = True
        lblFiltreCategorie.BackColor = Color.Transparent
        lblFiltreCategorie.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblFiltreCategorie.Location = New Point(24, 16)
        lblFiltreCategorie.Name = "lblFiltreCategorie"
        lblFiltreCategorie.Size = New Size(81, 20)
        lblFiltreCategorie.TabIndex = 0
        lblFiltreCategorie.Text = "Catégorie :"
        ' 
        ' pnlCartes
        ' 
        pnlCartes.BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(249))
        pnlCartes.Dock = DockStyle.Fill
        pnlCartes.Location = New Point(0, 160)
        pnlCartes.Name = "pnlCartes"
        pnlCartes.Padding = New Padding(16)
        pnlCartes.Size = New Size(1176, 648)
        pnlCartes.TabIndex = 4
        ' 
        ' ucPrestations
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        Controls.Add(pnlCartes)
        Controls.Add(pnlFiltres)
        Controls.Add(pnlHeader)
        Name = "ucPrestations"
        Size = New Size(1176, 808)
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        pnlFiltres.ResumeLayout(False)
        pnlFiltres.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents btnNouvellePrestation As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents lblDate As Label
    Friend WithEvents lblSousTitre As Label
    Friend WithEvents lblTitre As Label
    Friend WithEvents pnlFiltres As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblFiltreCategorie As Label
    Friend WithEvents cboCategorie As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents txtRecherche As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents pnlCartes As Panel

End Class
