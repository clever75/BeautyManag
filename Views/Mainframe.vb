' =====================================================
' FORM PRINCIPAL — Sidebar + Navigation
' =====================================================
Imports Guna.UI2.WinForms
Imports System.ComponentModel

Public Class Mainframe

    ' L'utilisateur connecté passé depuis frmLogin
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property UtilisateurConnecte As Utilisateur

    ' Controllers — déclarés une fois ici, utilisés dans tous les UserControls
    Public Shared ClientCtrl As New ClientController()
    Public Shared EmployeCtrl As New EmployeController()
    Public Shared PrestationCtrl As New PrestationController()
    Public Shared ProduitCtrl As New ProduitController()
    Public Shared FactureCtrl As New FactureController()
    Public Shared RendezVousCtrl As New RendezVousController()

    ' ─────────────────────────────────────────────
    ' CHARGEMENT DU FORM
    ' ─────────────────────────────────────────────
    Private btnEtats As New Guna.UI2.WinForms.Guna2Button()

    Private Sub Mainframe_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Afficher le nom de l'utilisateur connecté
        If UtilisateurConnecte IsNot Nothing Then
            lblNomConnecte.Text = UtilisateurConnecte.Prenom & " " & UtilisateurConnecte.Nom
            lblRoleConnecte.Text = "Gérante"
        End If

        ' Afficher le Dashboard par défaut
        ActiverBouton(btnDashboard)
        AfficherPage(New ucDashboard())
        ' ── Bouton États & Rapports dans la sidebar ──
        Dim btnEtats As New Guna.UI2.WinForms.Guna2Button()
        btnEtats.Text = "📊 États & Rapports"
        btnEtats.Size = New Size(210, 44)
        btnEtats.Location = New Point(10, 499)  ' juste sous btnProduits (Y=449+44+6)
        btnEtats.BorderRadius = 8
        btnEtats.FillColor = ColorTranslator.FromHtml("#3D1A24")
        btnEtats.ForeColor = ColorTranslator.FromHtml("#C8A0B0")
        btnEtats.Font = New Font("Segoe UI", 10.2, FontStyle.Regular)
        btnEtats.TextAlign = HorizontalAlignment.Left
        btnEtats.ImageAlign = HorizontalAlignment.Left
        btnEtats.Cursor = Cursors.Hand
        btnEtats.HoverState.FillColor = ColorTranslator.FromHtml("#5C3040")
        btnEtats.HoverState.ForeColor = Color.White
        AddHandler btnEtats.Click, AddressOf btnEtats_Click
        pnlSidebar.Controls.Add(btnEtats)
    End Sub
    Private Sub btnEtats_Click(sender As Object, e As EventArgs)
        OuvrirMenuEtats(btnEtats)
    End Sub

    ' ─────────────────────────────────────────────
    ' AFFICHER UN USERCONTROL DANS pnlContenu
    ' ─────────────────────────────────────────────
    Public Sub AfficherPage(uc As UserControl)
        pnlContenu.Controls.Clear()
        uc.Dock = DockStyle.Fill
        pnlContenu.Controls.Add(uc)
        uc.BringToFront()
    End Sub
    Private Sub OuvrirMenuEtats(btnSource As Guna.UI2.WinForms.Guna2Button)
        Dim menu As New ContextMenuStrip()
        menu.Font = New Font("Segoe UI", 10)

        Dim itemCA = menu.Items.Add("💰 CA du mois")
        Dim itemRevenus = menu.Items.Add("📈 Revenus par mois")
        Dim itemTop = menu.Items.Add("🏆 Top prestations")

        AddHandler itemCA.Click, Sub(s, e) EtatsHelper.EtatChiffreDuMois()
        AddHandler itemRevenus.Click, Sub(s, e) EtatsHelper.EtatRevenusParMois()
        AddHandler itemTop.Click, Sub(s, e) EtatsHelper.EtatTopPrestations()

        ' Ouvrir le menu juste à droite du bouton sidebar
        Dim pt = btnSource.PointToScreen(New Point(btnSource.Width, 0))
        menu.Show(pt)
    End Sub
    ' ─────────────────────────────────────────────
    ' ACTIVER LE BOUTON CLIQUÉ DANS LA SIDEBAR
    ' ─────────────────────────────────────────────
    Public Sub ActiverBouton(btnActif As Guna2Button)
        Dim boutons() As Guna2Button = {btnDashboard, btnRendezVous, btnClients,
                                        btnEmployes, btnPrestations, btnFactures, btnProduits}
        ' Remettre tous les boutons en neutre
        For Each b In boutons
            b.FillColor = ColorTranslator.FromHtml("#3D1A24")
            b.ForeColor = ColorTranslator.FromHtml("#C8A0B0")
        Next

        ' Activer le bouton cliqué
        btnActif.FillColor = ColorTranslator.FromHtml("#5C3040")
        btnActif.ForeColor = Color.White
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTONS DE NAVIGATION
    ' ─────────────────────────────────────────────
    Private Sub btnDashboard_Click(sender As Object, e As EventArgs) Handles btnDashboard.Click
        ActiverBouton(btnDashboard)
        AfficherPage(New ucDashboard())
    End Sub

    Private Sub btnRendezVous_Click(sender As Object, e As EventArgs) Handles btnRendezVous.Click
        ActiverBouton(btnRendezVous)
        AfficherPage(New ucRendezVous())
    End Sub

    Private Sub btnClients_Click(sender As Object, e As EventArgs) Handles btnClients.Click
        ActiverBouton(btnClients)
        AfficherPage(New ucClients())
    End Sub

    Private Sub btnEmployes_Click(sender As Object, e As EventArgs) Handles btnEmployes.Click
        ActiverBouton(btnEmployes)
        AfficherPage(New ucEmployes())
    End Sub

    Private Sub btnPrestations_Click(sender As Object, e As EventArgs) Handles btnPrestations.Click
        ActiverBouton(btnPrestations)
        AfficherPage(New ucPrestations())
    End Sub

    Private Sub btnFactures_Click(sender As Object, e As EventArgs) Handles btnFactures.Click
        ActiverBouton(btnFactures)
        AfficherPage(New ucFacture())
    End Sub

    Private Sub btnProduits_Click(sender As Object, e As EventArgs) Handles btnProduits.Click
        ActiverBouton(btnProduits)
        AfficherPage(New ucProduits())
    End Sub

    ' ─────────────────────────────────────────────
    ' DÉCONNEXION
    ' ─────────────────────────────────────────────
    Private Sub btnDeconnecter_Click(sender As Object, e As EventArgs) Handles btnDeconnecter.Click
        Dim rep = MsgBox("Voulez-vous vous déconnecter ?",
                         MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Déconnexion")
        If rep = MsgBoxResult.Yes Then
            Dim login As New frmLogin()
            login.Show()
            Me.Close()
        End If
    End Sub

End Class