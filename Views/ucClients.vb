' =====================================================
' USERCONTROL CLIENTS
' =====================================================
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class ucClients

    Private _clientSelectionne As Client = Nothing
    Private _modeAjout As Boolean = False
    Private _filtreCourant As String = "Tous"

    ' ─────────────────────────────────────────────
    ' CHARGEMENT
    ' ─────────────────────────────────────────────
    Private Sub ucClients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDate.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                       New System.Globalization.CultureInfo("fr-FR"))
        ViderFiche()
        ChargerClients()
        dgvClients.ClearSelection()
        pnlFiche.Visible = False
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LA LISTE
    ' ─────────────────────────────────────────────
    Private Sub ChargerClients()
        Try
            ' S'assurer que les colonnes existent avant d'ajouter des lignes.
            If dgvClients.Columns.Count = 0 Then
                dgvClients.Columns.Add("colNom", "Nom complet")
                dgvClients.Columns.Add("colTel", "Téléphone")
                dgvClients.Columns.Add("colGenre", "Genre")
            End If

            dgvClients.Rows.Clear()

            Dim liste As List(Of Client)

            If Not String.IsNullOrWhiteSpace(txtRecherche.Text) Then
                liste = Mainframe.ClientCtrl.Rechercher(txtRecherche.Text.Trim())
            Else
                liste = Mainframe.ClientCtrl.GetAllClients()
            End If



            If _filtreCourant <> "Tous" Then
                liste = liste.Where(Function(c) c.Genre = _filtreCourant).ToList()
            End If

            ' Message si liste vide
            If liste.Count = 0 Then
                lblSousTitre.Text = "Aucun résultat trouvé"
            Else
                lblSousTitre.Text = liste.Count & " client(s) enregistré(s)"
            End If

            For Each c In liste
                Dim index = dgvClients.Rows.Add(
                    c.Prenom & " " & c.Nom,
                    If(String.IsNullOrEmpty(c.Telephone), "—", c.Telephone),
                    If(String.IsNullOrEmpty(c.Genre), "—", c.Genre)
                )
                dgvClients.Rows(index).Tag = c
            Next

            dgvClients.ClearSelection()

        Catch ex As Exception
            MsgBox("Erreur chargement clients : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' SÉLECTION D'UN CLIENT DANS LA LISTE
    ' ─────────────────────────────────────────────
    Private Sub dgvClients_SelectionChanged(sender As Object, e As EventArgs) _
        Handles dgvClients.SelectionChanged

        If dgvClients.SelectedRows.Count = 0 Then Return
        If Not Me.IsHandleCreated Then Return

        Dim row = dgvClients.SelectedRows(0)
        If row.Tag Is Nothing Then Return

        Dim c = TryCast(row.Tag, Client)
        If c Is Nothing Then Return

        _clientSelectionne = c
        _modeAjout = False
        RemplirFiche(c)
    End Sub

    ' ─────────────────────────────────────────────
    ' REMPLIR LA FICHE AVEC UN CLIENT
    ' ─────────────────────────────────────────────
    Private Sub RemplirFiche(c As Client)
        lblNomClient.Text = FormatPrenom(c.Prenom) & " " & c.Nom.ToUpper()
        txtNom.Text = c.Nom.ToUpper()
        txtPrenom.Text = FormatPrenom(c.Prenom)
        If c Is Nothing Then Return
        pnlFiche.Visible = True


        lblGenreClient.Text = If(String.IsNullOrEmpty(c.Genre), "", c.Genre)

        txtNom.Text = c.Nom
        txtPrenom.Text = c.Prenom
        txtTelephone.Text = If(String.IsNullOrEmpty(c.Telephone), "", c.Telephone)

        If txtEmail IsNot Nothing Then
            txtEmail.Text = If(String.IsNullOrEmpty(c.Email), "", c.Email)
        End If

        If Not String.IsNullOrEmpty(c.Genre) Then
            cboGenre.SelectedItem = c.Genre
        Else
            cboGenre.SelectedIndex = -1
        End If

        btnEnregistrer.Enabled = True
        btnSupprimer.Enabled = True
    End Sub

    ' ─────────────────────────────────────────────
    ' VIDER LA FICHE
    ' ─────────────────────────────────────────────
    Private Sub ViderFiche()
        lblNomClient.Text = "Sélectionner un client"
        lblGenreClient.Text = ""
        txtNom.Text = ""
        txtPrenom.Text = ""
        txtTelephone.Text = ""
        If txtEmail IsNot Nothing Then txtEmail.Text = ""
        cboGenre.SelectedIndex = -1
        btnEnregistrer.Enabled = False
        btnSupprimer.Enabled = False
        _clientSelectionne = Nothing
    End Sub

    ' ─────────────────────────────────────────────
    ' RECHERCHE EN TEMPS RÉEL
    ' ─────────────────────────────────────────────
    Private Sub txtRecherche_TextChanged(sender As Object, e As EventArgs) _
        Handles txtRecherche.TextChanged
        ChargerClients()
    End Sub

    ' ─────────────────────────────────────────────
    ' FILTRES PAR GENRE
    ' ─────────────────────────────────────────────
    Private Sub ActiverFiltre(btnActif As Guna2Button, filtre As String)
        For Each b As Guna2Button In {btnFiltreAll, btnFiltreFemme, btnFiltreHomme, btnFiltreAutre}
            b.FillColor = ColorTranslator.FromHtml("#FDE8EF")
            b.ForeColor = ColorTranslator.FromHtml("#C45A7E")
        Next
        btnActif.FillColor = ColorTranslator.FromHtml("#C45A7E")
        btnActif.ForeColor = Color.White
        _filtreCourant = filtre
        ChargerClients()
    End Sub

    Private Sub btnFiltreAll_Click(s As Object, e As EventArgs) Handles btnFiltreAll.Click
        ActiverFiltre(btnFiltreAll, "Tous")
    End Sub

    Private Sub btnFiltreFemme_Click(s As Object, e As EventArgs) Handles btnFiltreFemme.Click
        ActiverFiltre(btnFiltreFemme, "Femme")
    End Sub

    Private Sub btnFiltreHomme_Click(s As Object, e As EventArgs) Handles btnFiltreHomme.Click
        ActiverFiltre(btnFiltreHomme, "Homme")
    End Sub

    Private Sub btnFiltreAutre_Click(s As Object, e As EventArgs) Handles btnFiltreAutre.Click
        ActiverFiltre(btnFiltreAutre, "Autre")
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON NOUVEAU CLIENT
    ' ─────────────────────────────────────────────
    Private Sub btnNouveauClient_Click(sender As Object, e As EventArgs) _
        Handles btnNouveauClient.Click
        _modeAjout = True
        _clientSelectionne = Nothing
        dgvClients.ClearSelection()
        ViderFiche()
        pnlFiche.Visible = True
        lblNomClient.Text = "Nouveau client"
        btnEnregistrer.Enabled = True
        btnSupprimer.Enabled = False
        txtNom.Focus()
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON ENREGISTRER
    ' ─────────────────────────────────────────────
    Private Sub btnEnregistrer_Click(sender As Object, e As EventArgs) _
        Handles btnEnregistrer.Click

        ' ── Nom obligatoire ──
        If String.IsNullOrWhiteSpace(txtNom.Text) Then
            MsgBox("Le nom est obligatoire.", MsgBoxStyle.Exclamation, "Champ manquant")
            txtNom.Focus()
            Return
        End If

        ' ── Prénom obligatoire ──
        If String.IsNullOrWhiteSpace(txtPrenom.Text) Then
            MsgBox("Le prénom est obligatoire.", MsgBoxStyle.Exclamation, "Champ manquant")
            txtPrenom.Focus()
            Return
        End If

        ' ── Longueur maximale ──
        If txtNom.Text.Trim().Length > 50 Then
            MsgBox("Le nom ne peut pas dépasser 50 caractères.", MsgBoxStyle.Exclamation, "Nom trop long")
            txtNom.Focus()
            Return
        End If

        If txtPrenom.Text.Trim().Length > 50 Then
            MsgBox("Le prénom ne peut pas dépasser 50 caractères.", MsgBoxStyle.Exclamation, "Prénom trop long")
            txtPrenom.Focus()
            Return
        End If

        ' ── Nom : lettres seulement ──
        If Not Regex.IsMatch(txtNom.Text.Trim(), "^[a-zA-ZÀ-ÿ\s\-']+$") Then
            MsgBox("Le nom ne doit contenir que des lettres." & vbCrLf & "Exemple : Agbeko",
                   MsgBoxStyle.Exclamation, "Nom invalide")
            txtNom.Focus()
            Return
        End If

        ' ── Prénom : lettres seulement ──
        If Not Regex.IsMatch(txtPrenom.Text.Trim(), "^[a-zA-ZÀ-ÿ\s\-']+$") Then
            MsgBox("Le prénom ne doit contenir que des lettres." & vbCrLf & "Exemple : Fatou",
                   MsgBoxStyle.Exclamation, "Prénom invalide")
            txtPrenom.Focus()
            Return
        End If

        ' ── Téléphone : 8 chiffres exactement ──
        If Not String.IsNullOrWhiteSpace(txtTelephone.Text) Then
            Dim tel = txtTelephone.Text.Trim()
            If Not Regex.IsMatch(tel, "^\d{8}$") Then
                MsgBox("Le numéro doit contenir exactement 8 chiffres.",
               MsgBoxStyle.Exclamation, "Téléphone invalide")
                txtTelephone.Focus()
                Return
            End If
            If Not "279".Contains(tel(0)) Then
                MsgBox("Le numéro doit commencer par 2, 7 ou 9." & vbCrLf &
               "Exemple : 90123456",
               MsgBoxStyle.Exclamation, "Numéro invalide")
                txtTelephone.Focus()
                Return
            End If
        End If

        ' ── Email : format valide si renseigné ──
        If txtEmail IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtEmail.Text) Then
            If Not Regex.IsMatch(txtEmail.Text.Trim(), "^[^@\s]+@[^@\s]+\.[^@\s]+$") Then
                MsgBox("L'adresse email n'est pas valide." & vbCrLf &
                       "Exemple : fatou@gmail.com",
                       MsgBoxStyle.Exclamation, "Email invalide")
                txtEmail.Focus()
                Return
            End If
        End If

        ' ── Vérifier doublon téléphone ──
        If Not String.IsNullOrWhiteSpace(txtTelephone.Text) Then
            Dim idExistant As Integer = If(_modeAjout, 0, _clientSelectionne.IdClient)
            If Mainframe.ClientCtrl.TelephoneExiste(txtTelephone.Text.Trim(), idExistant) Then
                MsgBox("Ce numéro de téléphone est déjà utilisé par un autre client.",
                       MsgBoxStyle.Exclamation, "Doublon détecté")
                txtTelephone.Focus()
                Return
            End If
        End If

        ' ── Construire l'objet client ──
        Dim c As New Client()
        c.Nom = txtNom.Text.Trim().ToUpper()
        c.Prenom = FormatPrenom(txtPrenom.Text)
        c.Telephone = If(String.IsNullOrWhiteSpace(txtTelephone.Text), "", txtTelephone.Text.Trim())
        c.Email = If(txtEmail IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtEmail.Text),
                     txtEmail.Text.Trim(), "")
        c.Genre = If(cboGenre.SelectedIndex >= 0, cboGenre.SelectedItem.ToString(), "")

        Try
            If _modeAjout Then
                Mainframe.ClientCtrl.AjouterClient(c)
                MsgBox("La cliente " & c.Prenom & " " & c.Nom & " a été ajoutée avec succès.",
                       MsgBoxStyle.Information, "Succès")
            Else
                If _clientSelectionne Is Nothing Then Return
                c.IdClient = _clientSelectionne.IdClient
                Mainframe.ClientCtrl.ModifierClient(c)
                MsgBox("Les informations de " & c.Prenom & " " & c.Nom & " ont été modifiées.",
                       MsgBoxStyle.Information, "Modification réussie")
            End If

            ChargerClients()
            ViderFiche()

        Catch ex As Exception
            MsgBox("Erreur lors de l'enregistrement : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON SUPPRIMER
    ' ─────────────────────────────────────────────
    Private Sub btnSupprimer_Click(sender As Object, e As EventArgs) _
        Handles btnSupprimer.Click

        If _clientSelectionne Is Nothing Then Return

        ' Vérifier d'abord si le client a des rendez-vous
        Dim nbRdv As Integer = Mainframe.ClientCtrl.GetNbRendezVous(_clientSelectionne.IdClient)
        If nbRdv > 0 Then
            MsgBox("Impossible de supprimer " & _clientSelectionne.Prenom & " " &
           _clientSelectionne.Nom & "." & vbCrLf &
           "Ce client a " & nbRdv & " rendez-vous enregistré(s).",
           MsgBoxStyle.Exclamation, "Suppression impossible")
            Return
        End If

        Dim rep = MsgBox("Supprimer " & _clientSelectionne.Prenom & " " &
                 _clientSelectionne.Nom & " définitivement ?" & vbCrLf &
                 "Cette action est irréversible.",
                 MsgBoxStyle.YesNo Or MsgBoxStyle.Critical, "Confirmer")

        If rep = MsgBoxResult.Yes Then
            Try
                Mainframe.ClientCtrl.SupprimerClient(_clientSelectionne.IdClient)
                MsgBox("Le client a été supprimé avec succès.",
               MsgBoxStyle.Information, "Suppression réussie")
                ChargerClients()
                ViderFiche()
                pnlFiche.Visible = False
            Catch ex As Exception
                MsgBox("Erreur lors de la suppression : " & ex.Message,
               MsgBoxStyle.Critical, "Erreur")
            End Try
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON ANNULER
    ' ─────────────────────────────────────────────
    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) _
        Handles btnAnnuler.Click
        _modeAjout = False
        If _clientSelectionne IsNot Nothing Then
            RemplirFiche(_clientSelectionne)
        Else
            ViderFiche()
            pnlFiche.Visible = False
        End If
    End Sub
    Private Function FormatPrenom(prenom As String) As String
        If String.IsNullOrWhiteSpace(prenom) Then Return prenom
        ' Gère les prénoms composés : "jean-marie" → "Jean-Marie"
        Return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(prenom.ToLower().Trim())
    End Function

    Private Sub cboGenre_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboGenre.SelectedIndexChanged
    End Sub

    Private Sub lblNomClient_Click(sender As Object, e As EventArgs) Handles lblNomClient.Click
    End Sub

    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click

    End Sub
End Class