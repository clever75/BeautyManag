<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        pnlHeader = New Guna.UI2.WinForms.Guna2Panel()
        lblSousTitre = New Label()
        lblNomSalon = New Label()
        picLogo = New Guna.UI2.WinForms.Guna2CirclePictureBox()
        lblNomUtil = New Label()
        picIconUser = New PictureBox()
        txtNomUtilisateur = New Guna.UI2.WinForms.Guna2TextBox()
        lblMotDePasse = New Label()
        picIconLock = New PictureBox()
        txtMotDePasse = New Guna.UI2.WinForms.Guna2TextBox()
        btnConnecter = New Guna.UI2.WinForms.Guna2Button()
        pnlHeader.SuspendLayout()
        CType(picLogo, ComponentModel.ISupportInitialize).BeginInit()
        CType(picIconUser, ComponentModel.ISupportInitialize).BeginInit()
        CType(picIconLock, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlHeader
        ' 
        pnlHeader.Controls.Add(lblSousTitre)
        pnlHeader.Controls.Add(lblNomSalon)
        pnlHeader.Controls.Add(picLogo)
        pnlHeader.CustomizableEdges = CustomizableEdges2
        pnlHeader.Dock = DockStyle.Top
        pnlHeader.FillColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        pnlHeader.Location = New Point(0, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.ShadowDecoration.CustomizableEdges = CustomizableEdges3
        pnlHeader.Size = New Size(402, 170)
        pnlHeader.TabIndex = 0
        ' 
        ' lblSousTitre
        ' 
        lblSousTitre.AutoSize = True
        lblSousTitre.BackColor = Color.Transparent
        lblSousTitre.ForeColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        lblSousTitre.Location = New Point(118, 134)
        lblSousTitre.Name = "lblSousTitre"
        lblSousTitre.Size = New Size(214, 20)
        lblSousTitre.TabIndex = 2
        lblSousTitre.Text = "Connectez-vous à votre espace"
        ' 
        ' lblNomSalon
        ' 
        lblNomSalon.AutoSize = True
        lblNomSalon.BackColor = Color.Transparent
        lblNomSalon.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblNomSalon.ForeColor = Color.White
        lblNomSalon.Location = New Point(148, 103)
        lblNomSalon.Name = "lblNomSalon"
        lblNomSalon.Size = New Size(144, 31)
        lblNomSalon.TabIndex = 1
        lblNomSalon.Text = "Rosa Beauty"
        ' 
        ' picLogo
        ' 
        picLogo.BackColor = Color.FromArgb(CByte(244), CByte(160), CByte(188))
        picLogo.FillColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        picLogo.Image = CType(resources.GetObject("picLogo.Image"), Image)
        picLogo.ImageRotate = 0F
        picLogo.Location = New Point(178, 20)
        picLogo.Name = "picLogo"
        picLogo.ShadowDecoration.CustomizableEdges = CustomizableEdges1
        picLogo.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle
        picLogo.Size = New Size(80, 80)
        picLogo.SizeMode = PictureBoxSizeMode.Zoom
        picLogo.TabIndex = 0
        picLogo.TabStop = False
        ' 
        ' lblNomUtil
        ' 
        lblNomUtil.AutoSize = True
        lblNomUtil.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblNomUtil.Location = New Point(40, 195)
        lblNomUtil.Name = "lblNomUtil"
        lblNomUtil.Size = New Size(123, 20)
        lblNomUtil.TabIndex = 1
        lblNomUtil.Text = "Nom d'utilisateur"
        ' 
        ' picIconUser
        ' 
        picIconUser.Image = CType(resources.GetObject("picIconUser.Image"), Image)
        picIconUser.Location = New Point(40, 218)
        picIconUser.Name = "picIconUser"
        picIconUser.Size = New Size(20, 26)
        picIconUser.SizeMode = PictureBoxSizeMode.Zoom
        picIconUser.TabIndex = 2
        picIconUser.TabStop = False
        ' 
        ' txtNomUtilisateur
        ' 
        txtNomUtilisateur.BorderColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        txtNomUtilisateur.BorderRadius = 8
        txtNomUtilisateur.BorderThickness = 2
        txtNomUtilisateur.CustomizableEdges = CustomizableEdges4
        txtNomUtilisateur.DefaultText = ""
        txtNomUtilisateur.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtNomUtilisateur.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtNomUtilisateur.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtNomUtilisateur.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtNomUtilisateur.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtNomUtilisateur.Font = New Font("Segoe UI", 9F)
        txtNomUtilisateur.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtNomUtilisateur.Location = New Point(68, 214)
        txtNomUtilisateur.Margin = New Padding(3, 4, 3, 4)
        txtNomUtilisateur.Name = "txtNomUtilisateur"
        txtNomUtilisateur.PlaceholderText = "Entrez votre nom d'utilsateur"
        txtNomUtilisateur.SelectedText = ""
        txtNomUtilisateur.ShadowDecoration.CustomizableEdges = CustomizableEdges5
        txtNomUtilisateur.Size = New Size(312, 36)
        txtNomUtilisateur.TabIndex = 3
        ' 
        ' lblMotDePasse
        ' 
        lblMotDePasse.AutoSize = True
        lblMotDePasse.ForeColor = Color.FromArgb(CByte(160), CByte(112), CByte(128))
        lblMotDePasse.Location = New Point(40, 270)
        lblMotDePasse.Name = "lblMotDePasse"
        lblMotDePasse.Size = New Size(98, 20)
        lblMotDePasse.TabIndex = 4
        lblMotDePasse.Text = "Mot de passe"
        ' 
        ' picIconLock
        ' 
        picIconLock.Image = CType(resources.GetObject("picIconLock.Image"), Image)
        picIconLock.Location = New Point(40, 293)
        picIconLock.Name = "picIconLock"
        picIconLock.Size = New Size(20, 26)
        picIconLock.SizeMode = PictureBoxSizeMode.Zoom
        picIconLock.TabIndex = 5
        picIconLock.TabStop = False
        ' 
        ' txtMotDePasse
        ' 
        txtMotDePasse.BorderColor = Color.FromArgb(CByte(255), CByte(192), CByte(192))
        txtMotDePasse.BorderRadius = 8
        txtMotDePasse.BorderThickness = 2
        txtMotDePasse.CustomizableEdges = CustomizableEdges6
        txtMotDePasse.DefaultText = ""
        txtMotDePasse.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtMotDePasse.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtMotDePasse.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtMotDePasse.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtMotDePasse.FocusedState.BorderColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        txtMotDePasse.Font = New Font("Segoe UI", 9F)
        txtMotDePasse.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtMotDePasse.Location = New Point(68, 289)
        txtMotDePasse.Margin = New Padding(3, 4, 3, 4)
        txtMotDePasse.Name = "txtMotDePasse"
        txtMotDePasse.PasswordChar = "."c
        txtMotDePasse.PlaceholderText = "Entrez votre mot de passe"
        txtMotDePasse.SelectedText = ""
        txtMotDePasse.ShadowDecoration.BorderRadius = 0
        txtMotDePasse.ShadowDecoration.CustomizableEdges = CustomizableEdges7
        txtMotDePasse.Size = New Size(312, 36)
        txtMotDePasse.TabIndex = 6
        ' 
        ' btnConnecter
        ' 
        btnConnecter.BorderRadius = 10
        btnConnecter.Cursor = Cursors.Hand
        btnConnecter.CustomizableEdges = CustomizableEdges8
        btnConnecter.DisabledState.BorderColor = Color.DarkGray
        btnConnecter.DisabledState.CustomBorderColor = Color.DarkGray
        btnConnecter.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnConnecter.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnConnecter.FillColor = Color.FromArgb(CByte(196), CByte(90), CByte(126))
        btnConnecter.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnConnecter.ForeColor = SystemColors.Window
        btnConnecter.HoverState.FillColor = Color.FromArgb(CByte(232), CByte(120), CByte(154))
        btnConnecter.Image = CType(resources.GetObject("btnConnecter.Image"), Image)
        btnConnecter.Location = New Point(68, 350)
        btnConnecter.Name = "btnConnecter"
        btnConnecter.PressedColor = Color.FromArgb(CByte(160), CByte(48), CByte(96))
        btnConnecter.ShadowDecoration.CustomizableEdges = CustomizableEdges9
        btnConnecter.Size = New Size(312, 45)
        btnConnecter.TabIndex = 7
        btnConnecter.Text = "Se connecter"
        ' 
        ' frmLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(249))
        ClientSize = New Size(402, 473)
        Controls.Add(btnConnecter)
        Controls.Add(txtMotDePasse)
        Controls.Add(picIconLock)
        Controls.Add(lblMotDePasse)
        Controls.Add(txtNomUtilisateur)
        Controls.Add(picIconUser)
        Controls.Add(lblNomUtil)
        Controls.Add(pnlHeader)
        MaximizeBox = False
        MinimumSize = New Size(420, 520)
        Name = "frmLogin"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Rosa Beauty - Connexion"
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        CType(picLogo, ComponentModel.ISupportInitialize).EndInit()
        CType(picIconUser, ComponentModel.ISupportInitialize).EndInit()
        CType(picIconLock, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents pnlHeader As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents picLogo As Guna.UI2.WinForms.Guna2CirclePictureBox
    Friend WithEvents lblNomSalon As Label
    Friend WithEvents lblSousTitre As Label
    Friend WithEvents lblNomUtil As Label
    Friend WithEvents picIconUser As PictureBox
    Friend WithEvents txtNomUtilisateur As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents lblMotDePasse As Label
    Friend WithEvents picIconLock As PictureBox
    Friend WithEvents txtMotDePasse As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents btnConnecter As Guna.UI2.WinForms.Guna2Button
End Class
