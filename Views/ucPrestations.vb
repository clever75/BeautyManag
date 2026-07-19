' =====================================================
' USERCONTROL PRESTATIONS
' Les cartes sont créées dynamiquement par le code
' =====================================================
Imports Guna.UI2.WinForms

Public Class ucPrestations

    Private _categorieFiltree As String = ""
    Private _recherche As String = ""

    ' ─────────────────────────────────────────────
    ' CHARGEMENT
    ' ─────────────────────────────────────────────
    Private Sub ucPrestations_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDate.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                       New System.Globalization.CultureInfo("fr-FR"))
        ChargerCategories()
        ChargerCartes()
    End Sub



    ' ─────────────────────────────────────────────
    ' CHARGER LES CATÉGORIES DANS LE COMBOBOX
    ' ─────────────────────────────────────────────
    Private Sub ChargerCategories()
        Try
            cboCategorie.Items.Clear()
            cboCategorie.Items.Add("Toutes les catégories")

            Dim categories = Mainframe.PrestationCtrl.GetCategories()
            For Each cat In categories
                cboCategorie.Items.Add(cat)
            Next

            cboCategorie.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement catégories : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LES CARTES
    ' ─────────────────────────────────────────────
    Private Sub ChargerCartes()
        Try
            pnlCartes.Controls.Clear()

            ' Récupérer toutes les prestations
            Dim liste = Mainframe.PrestationCtrl.GetAllPrestations()

            ' Filtrer par catégorie
            If Not String.IsNullOrEmpty(_categorieFiltree) Then
                liste = liste.Where(Function(p) p.Categorie = _categorieFiltree).ToList()
            End If

            ' Filtrer par recherche
            If Not String.IsNullOrWhiteSpace(_recherche) Then
                liste = liste.Where(Function(p)
                                        Return p.Nom.ToLower.Contains(_recherche.ToLower) OrElse
                                               (Not String.IsNullOrEmpty(p.Description) AndAlso
                                                p.Description.ToLower.Contains(_recherche.ToLower))
                                    End Function).ToList()
            End If

            ' Mettre à jour le sous-titre
            lblSousTitre.Text = liste.Count & " prestation(s)"

            ' Créer une carte pour chaque prestation
            Dim colonnes As Integer = 3
            Dim largeurCarte As Integer = (pnlCartes.Width - 32 - (colonnes - 1) * 12) \ colonnes
            Dim hauteurCarte As Integer = 170
            Dim x As Integer = 16
            Dim y As Integer = 16
            Dim compteur As Integer = 0

            For Each p In liste
                Dim carte = CreerCarte(p, largeurCarte, hauteurCarte)
                carte.Location = New Point(x, y)
                carte.BorderColor = ColorTranslator.FromHtml("#F0DCE2")
                carte.BorderThickness = 1
                pnlCartes.Controls.Add(carte)

                compteur += 1
                If compteur Mod colonnes = 0 Then
                    x = 16
                    y += hauteurCarte + 12
                Else
                    x += largeurCarte + 12
                End If
            Next

        Catch ex As Exception
            MsgBox("Erreur chargement prestations : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' CRÉER UNE CARTE DYNAMIQUEMENT
    ' ─────────────────────────────────────────────
    Private Function CreerCarte(p As Prestation,
                                largeur As Integer,
                                hauteur As Integer) As Guna2Panel
        ' Panel principal de la carte
        Dim carte As New Guna2Panel()
        carte.Size = New Size(largeur, hauteur)
        carte.FillColor = If(p.Actif, Color.White, ColorTranslator.FromHtml("#FAFAFA"))
        carte.BorderRadius = 12
        carte.ShadowDecoration.Enabled = True
        carte.ShadowDecoration.Color = ColorTranslator.FromHtml("#F0DCE2")
        carte.ShadowDecoration.Depth = 4
        carte.Tag = p

        ' ── Tag catégorie ──────────────────────
        Dim lblCategorie As New Label()
        lblCategorie.Text = If(String.IsNullOrEmpty(p.Categorie), "Autre", p.Categorie)
        lblCategorie.AutoSize = True
        lblCategorie.Location = New Point(12, 12)
        lblCategorie.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblCategorie.BackColor = CouleurFondCategorie(p.Categorie)
        lblCategorie.ForeColor = CouleurTexteCategorie(p.Categorie)
        lblCategorie.Padding = New Padding(8, 3, 8, 3)

        ' ── Badge Actif / Inactif ───────────────
        Dim lblStatut As New Label()
        lblStatut.Text = If(p.Actif, "Actif", "Inactif")
        lblStatut.AutoSize = True
        lblStatut.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        If p.Actif Then
            lblStatut.BackColor = ColorTranslator.FromHtml("#E1F5EE")
            lblStatut.ForeColor = ColorTranslator.FromHtml("#0F6E56")
        Else
            lblStatut.BackColor = ColorTranslator.FromHtml("#FEF5E7")
            lblStatut.ForeColor = ColorTranslator.FromHtml("#854F0B")
        End If
        lblStatut.Padding = New Padding(8, 3, 8, 3)

        ' ── Nom de la prestation ────────────────
        Dim lblNom As New Label()
        lblNom.Text = p.Nom
        lblNom.Location = New Point(12, 38)
        lblNom.Size = New Size(largeur - 24, 24)
        lblNom.Font = New Font("Segoe UI", 11, FontStyle.Bold)
        lblNom.ForeColor = If(p.Actif,
            ColorTranslator.FromHtml("#3D1A24"),
            ColorTranslator.FromHtml("#A07080"))
        lblNom.BackColor = Color.Transparent

        ' ── Description ─────────────────────────
        Dim lblDesc As New Label()
        lblDesc.Text = If(String.IsNullOrEmpty(p.Description), "Aucune description", p.Description)
        lblDesc.Location = New Point(12, 64)
        lblDesc.Size = New Size(largeur - 24, 36)
        lblDesc.Font = New Font("Segoe UI", 9)
        lblDesc.ForeColor = ColorTranslator.FromHtml("#A07080")
        lblDesc.BackColor = Color.Transparent

        ' ── Séparateur ──────────────────────────
        Dim sep As New Panel()
        sep.Location = New Point(0, 104)
        sep.Size = New Size(largeur, 1)
        sep.BackColor = ColorTranslator.FromHtml("#F0DCE2")

        ' ── Prix ────────────────────────────────
        Dim lblPrix As New Label()
        lblPrix.Text = p.Prix.ToString("N0") & " F CFA"
        lblPrix.Location = New Point(12, 112)
        lblPrix.AutoSize = True
        lblPrix.Font = New Font("Segoe UI", 12, FontStyle.Bold)
        lblPrix.ForeColor = If(p.Actif,
            ColorTranslator.FromHtml("#C45A7E"),
            ColorTranslator.FromHtml("#C8A0B0"))
        lblPrix.BackColor = Color.Transparent

        ' ── Durée ───────────────────────────────
        Dim lblDuree As New Label()
        lblDuree.Text = p.DureeMinutes & " min"
        lblDuree.Location = New Point(12, 136)
        lblDuree.AutoSize = True
        lblDuree.Font = New Font("Segoe UI", 9)
        lblDuree.ForeColor = ColorTranslator.FromHtml("#A07080")
        lblDuree.BackColor = Color.Transparent

        ' ── Bouton Modifier ─────────────────────
        Dim btnModifier As New Guna2Button()
        btnModifier.Text = "Modifier"
        btnModifier.Size = New Size(80, 28)
        btnModifier.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnModifier.FillColor = ColorTranslator.FromHtml("#FDE8EF")
        btnModifier.ForeColor = ColorTranslator.FromHtml("#C45A7E")
        btnModifier.BorderRadius = 7
        btnModifier.Cursor = Cursors.Hand
        btnModifier.Tag = p
        AddHandler btnModifier.Click, AddressOf BtnModifier_Click

        ' ── Bouton Supprimer ────────────────────
        Dim btnSupprimer As New Guna2Button()
        btnSupprimer.Text = "Supprimer"
        btnSupprimer.Size = New Size(88, 28)
        btnSupprimer.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnSupprimer.FillColor = ColorTranslator.FromHtml("#FEE2E2")
        btnSupprimer.ForeColor = ColorTranslator.FromHtml("#B91C1C")
        btnSupprimer.BorderRadius = 7
        btnSupprimer.Cursor = Cursors.Hand
        btnSupprimer.Tag = p
        AddHandler btnSupprimer.Click, Sub(s, ev)
                                           Dim prestation = TryCast(CType(s, Guna2Button).Tag, Prestation)
                                           SupprimerOuDesactiverPrestation(prestation)
                                       End Sub

        ' ── Positionnement ──────────────────────
        lblStatut.Location = New Point(largeur - lblStatut.PreferredWidth - 20, 12)

        ' Les deux boutons côte à côte en bas à droite
        btnModifier.Location = New Point(largeur - 92, 130)
        btnSupprimer.Location = New Point(largeur - 92 - 96, 130)

        ' ── Agrandir la carte pour les boutons ──
        carte.Size = New Size(largeur, 170)

        ' Ajouter tous les contrôles à la carte
        carte.Controls.AddRange({
            lblCategorie, lblStatut, lblNom, lblDesc,
            sep, lblPrix, lblDuree, btnModifier, btnSupprimer
        })

        Return carte
    End Function

    ' ─────────────────────────────────────────────
    ' COULEURS PAR CATÉGORIE
    ' ─────────────────────────────────────────────
    Private Function CouleurFondCategorie(categorie As String) As Color
        Select Case If(String.IsNullOrEmpty(categorie), "Autre", categorie).ToLower
            Case "coiffure"
                Return ColorTranslator.FromHtml("#FDE8EF")
            Case "esthétique", "esthetique"
                Return ColorTranslator.FromHtml("#EDE9FE")
            Case "onglerie"
                Return ColorTranslator.FromHtml("#E1F5EE")
            Case Else
                Return ColorTranslator.FromHtml("#F1EFE8")
        End Select
    End Function

    Private Function CouleurTexteCategorie(categorie As String) As Color
        Select Case If(String.IsNullOrEmpty(categorie), "Autre", categorie).ToLower
            Case "coiffure"
                Return ColorTranslator.FromHtml("#993556")
            Case "esthétique", "esthetique"
                Return ColorTranslator.FromHtml("#534AB7")
            Case "onglerie"
                Return ColorTranslator.FromHtml("#0F6E56")
            Case Else
                Return ColorTranslator.FromHtml("#5F5E5A")
        End Select
    End Function

    ' ─────────────────────────────────────────────
    ' FILTRE CATÉGORIE
    ' ─────────────────────────────────────────────
    Private Sub cboCategorie_SelectedIndexChanged(s As Object, e As EventArgs) _
        Handles cboCategorie.SelectedIndexChanged

        If cboCategorie.SelectedIndex <= 0 Then
            _categorieFiltree = ""
        Else
            _categorieFiltree = cboCategorie.SelectedItem.ToString()
        End If
        ChargerCartes()
    End Sub

    ' ─────────────────────────────────────────────
    ' RECHERCHE EN TEMPS RÉEL
    ' ─────────────────────────────────────────────
    Private Sub txtRecherche_TextChanged(s As Object, e As EventArgs) _
        Handles txtRecherche.TextChanged
        _recherche = txtRecherche.Text.Trim()
        ChargerCartes()
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON NOUVELLE PRESTATION
    ' ─────────────────────────────────────────────
    Private Sub btnNouvellePrestation_Click(sender As Object, e As EventArgs) _
    Handles btnNouvellePrestation.Click

        Dim frm As New frmPrestation(Nothing)  ' Nothing = mode ajout
        If frm.ShowDialog() = DialogResult.OK Then
            ChargerCategories()
            ChargerCartes()
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON MODIFIER SUR UNE CARTE
    ' ─────────────────────────────────────────────
    Private Sub BtnModifier_Click(sender As Object, e As EventArgs)
        Dim p = TryCast(CType(sender, Guna2Button).Tag, Prestation)
        If p Is Nothing Then Return

        Dim frm As New frmPrestation(p)  ' p = mode modification
        If frm.ShowDialog() = DialogResult.OK Then
            ChargerCategories()
            ChargerCartes()
        End If
    End Sub

    ' Ajouter une méthode pour supprimer ou désactiver une prestation depuis la carte
    Private Sub SupprimerOuDesactiverPrestation(p As Prestation)
        If p Is Nothing Then Return
        Try
            If Not Mainframe.PrestationCtrl.ADesLiens(p.IdPrestation) Then
                Dim rep = MsgBox($"La prestation ""{p.Nom}"" n'est liée à rien. Supprimer ?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Supprimer")
                If rep = MsgBoxResult.Yes Then
                    Mainframe.PrestationCtrl.SupprimerPrestation(p.IdPrestation)
                    ChargerCartes()
                    Return
                End If
            End If
        Catch
        End Try

        ' Sinon désactiver
        Dim rep2 = MsgBox($"Désactiver la prestation ""{p.Nom}"" ?", MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Désactiver")
        If rep2 = MsgBoxResult.Yes Then
            Mainframe.PrestationCtrl.DesactiverPrestation(p.IdPrestation)
            ChargerCartes()
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' REDIMENSIONNEMENT → RECONSTRUIRE LES CARTES
    ' ─────────────────────────────────────────────
    Private Sub ucPrestations_Resize(s As Object, e As EventArgs) _
        Handles Me.Resize
        ChargerCartes()
    End Sub

    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click

    End Sub
End Class