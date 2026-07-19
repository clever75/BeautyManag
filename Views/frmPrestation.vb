' =====================================================
' FORM PRESTATION - Ajout et Modification
' =====================================================
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class frmPrestation

    Private _prestation As Prestation = Nothing
    Private _modeAjout As Boolean = False

    ' ─────────────────────────────────────────────
    ' CONSTRUCTEUR
    ' ─────────────────────────────────────────────
    Public Sub New(p As Prestation)
        InitializeComponent()
        _prestation = p
        _modeAjout = (p Is Nothing)
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGEMENT
    ' ─────────────────────────────────────────────
    Private Sub frmPrestation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Charger les catégories dans le ComboBox
        ' Style du ComboBox standard
        cboCategorieForm.BackColor = ColorTranslator.FromHtml("#FFF5F8")
        cboCategorieForm.ForeColor = ColorTranslator.FromHtml("#3D1A24")
        cboCategorieForm.Font = New Font("Segoe UI", 10)
        Dim cats = {"Coiffure", "Tressage", "Onglerie", "Maquillage", "Esthétique", "Soin visage"}
        cboCategorieForm.Items.Clear()
        For Each cat In cats
            cboCategorieForm.Items.Add(cat)
        Next

        If _modeAjout Then
            lblTitreForm.Text = "Nouvelle prestation"
            lblSousTitreForm.Text = "Remplissez les informations"
            tglActifForm.Checked = True
        Else
            lblTitreForm.Text = "Modifier la prestation"
            lblSousTitreForm.Text = _prestation.Nom
            RemplirFormulaire(_prestation)
        End If

        txtNomPrestation.Focus()
    End Sub

    ' ─────────────────────────────────────────────
    ' REMPLIR LE FORMULAIRE EN MODE MODIFICATION
    ' ─────────────────────────────────────────────
    Private Sub RemplirFormulaire(p As Prestation)
        txtNomPrestation.Text = p.Nom
        cboCategorieForm.Text = p.Categorie
        txtDescription.Text = If(String.IsNullOrEmpty(p.Description), "", p.Description)
        txtPrix.Text = p.Prix.ToString("0")
        txtDuree.Text = p.DureeMinutes.ToString()
        MettreAJourPlaceholderDuree(p.DureeMinutes)
    End Sub

    ' ─────────────────────────────────────────────
    ' CONVERSION DURÉE EN TEMPS RÉEL
    ' ─────────────────────────────────────────────
    Private Sub txtDuree_TextChanged(sender As Object, e As EventArgs) _
        Handles txtDuree.TextChanged
        Dim mins As Integer
        If Integer.TryParse(txtDuree.Text, mins) AndAlso mins > 0 Then
            MettreAJourPlaceholderDuree(mins)
        Else
            txtDuree.PlaceholderText = "Ex : 60 (= 1h 00min)"
        End If
    End Sub

    Private Sub MettreAJourPlaceholderDuree(mins As Integer)
        Dim h = mins \ 60
        Dim m = mins Mod 60
        txtDuree.PlaceholderText = If(h > 0, $"= {h}h {m:D2}min", $"= {m} min")
    End Sub

    ' ─────────────────────────────────────────────
    ' VALIDATION SAISIE NUMÉRIQUE
    ' ─────────────────────────────────────────────
    Private Sub txtPrix_KeyPress(sender As Object, e As KeyPressEventArgs) _
        Handles txtPrix.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtDuree_KeyPress(sender As Object, e As KeyPressEventArgs) _
        Handles txtDuree.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON ENREGISTRER
    ' ─────────────────────────────────────────────
    Private Sub btnEnregistrerForm_Click(sender As Object, e As EventArgs) _
        Handles btnEnregistrerForm.Click

        ' ── Nom obligatoire ──
        If String.IsNullOrWhiteSpace(txtNomPrestation.Text) Then
            MsgBox("Le nom de la prestation est obligatoire.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            txtNomPrestation.Focus()
            Return
        End If

        ' ── Longueur maximale nom ──
        If txtNomPrestation.Text.Trim().Length > 100 Then
            MsgBox("Le nom ne peut pas dépasser 100 caractères.",
                   MsgBoxStyle.Exclamation, "Nom trop long")
            txtNomPrestation.Focus()
            Return
        End If

        ' ── Catégorie obligatoire ──
        If cboCategorieForm.SelectedIndex < 0 Then
            MsgBox("Veuillez sélectionner ou saisir une catégorie.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            cboCategorieForm.Focus()
            Return
        End If

        ' ── Prix obligatoire ──
        If String.IsNullOrWhiteSpace(txtPrix.Text) Then
            MsgBox("Le prix est obligatoire.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            txtPrix.Focus()
            Return
        End If

        If txtNomPrestation.Text.Trim().Length < 2 Then
            MsgBox("Le nom de la prestation doit contenir au moins 2 caractères.",
           MsgBoxStyle.Exclamation, "Nom trop court")
            txtNomPrestation.Focus()
            Return
        End If
        ' ── Prix valide ──
        Dim prix As Decimal
        If Not Decimal.TryParse(txtPrix.Text, prix) OrElse prix <= 0 Then
            MsgBox("Veuillez entrer un prix valide en FCFA supérieur à 0." & vbCrLf &
                   "Exemple : 5000",
                   MsgBoxStyle.Exclamation, "Prix invalide")
            txtPrix.Focus()
            Return
        End If

        ' ── Prix pas trop élevé ──
        If prix > 999999 Then
            MsgBox("Le prix semble trop élevé. Vérifiez la valeur saisie.",
                   MsgBoxStyle.Exclamation, "Prix trop élevé")
            txtPrix.Focus()
            Return
        End If
        If prix < 100 Then
            MsgBox("Le prix doit être d'au moins 100 FCFA.",
           MsgBoxStyle.Exclamation, "Prix trop bas")
            txtPrix.Focus()
            Return
        End If

        ' ── Durée obligatoire ──
        If String.IsNullOrWhiteSpace(txtDuree.Text) Then
            MsgBox("La durée est obligatoire.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            txtDuree.Focus()
            Return
        End If

        ' ── Durée valide ──
        Dim duree As Integer
        If Not Integer.TryParse(txtDuree.Text, duree) OrElse duree <= 0 Then
            MsgBox("La durée doit être un nombre entier de minutes supérieur à 0." & vbCrLf &
                   "Exemple : 60 pour 1 heure",
                   MsgBoxStyle.Exclamation, "Durée invalide")
            txtDuree.Focus()
            Return
        End If

        ' ── Durée pas trop longue ──
        If duree > 480 Then
            MsgBox("La durée ne peut pas dépasser 480 minutes (8 heures).",
                   MsgBoxStyle.Exclamation, "Durée trop longue")
            txtDuree.Focus()
            Return
        End If

        ' ── Vérifier doublon nom prestation ──
        Dim idExclure As Integer = If(_modeAjout, 0, _prestation.IdPrestation)
        If Mainframe.PrestationCtrl.NomExiste(txtNomPrestation.Text.Trim(), idExclure) Then
            MsgBox("Une prestation avec ce nom existe déjà." & vbCrLf &
                   "Veuillez choisir un nom différent.",
                   MsgBoxStyle.Exclamation, "Doublon détecté")
            txtNomPrestation.Focus()
            Return
        End If

        ' ── Construire l'objet ──
        Dim p As New Prestation()
        p.Nom = txtNomPrestation.Text.Trim()
        p.Categorie = cboCategorieForm.Text.Trim()
        p.Description = txtDescription.Text.Trim()
        p.Prix = prix
        p.DureeMinutes = duree
        p.Actif = True
        Try
            If _modeAjout Then
                Mainframe.PrestationCtrl.AjouterPrestation(p)
                MsgBox($"La prestation ""{p.Nom}"" a été ajoutée avec succès." & vbCrLf &
                       $"Prix : {FormatNumber(p.Prix, 0)} FCFA — Durée : {p.DureeMinutes} min",
                       MsgBoxStyle.Information, "Succès")
            Else
                p.IdPrestation = _prestation.IdPrestation
                Mainframe.PrestationCtrl.ModifierPrestation(p)
                MsgBox($"La prestation ""{p.Nom}"" a été modifiée avec succès.",
                       MsgBoxStyle.Information, "Modification réussie")
            End If

            Me.DialogResult = DialogResult.OK
            Me.Close()

        Catch ex As Exception
            MsgBox("Erreur lors de l'enregistrement : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON ANNULER
    ' ─────────────────────────────────────────────
    Private Sub btnAnnulerForm_Click(sender As Object, e As EventArgs) _
        Handles btnAnnulerForm.Click
        ' Demander confirmation si des données ont été saisies
        If Not String.IsNullOrWhiteSpace(txtNomPrestation.Text) OrElse
           Not String.IsNullOrWhiteSpace(txtPrix.Text) Then
            Dim rep = MsgBox("Voulez-vous vraiment annuler ?" & vbCrLf &
                             "Les informations saisies seront perdues.",
                             MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmer")
            If rep = MsgBoxResult.No Then Return
        End If

        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    ' ─────────────────────────────────────────────
    ' FERMETURE PAR LA CROIX WINDOWS
    ' ─────────────────────────────────────────────
    Private Sub frmPrestation_FormClosing(sender As Object, e As FormClosingEventArgs) _
        Handles MyBase.FormClosing
        If Me.DialogResult = DialogResult.OK Then Return
        Me.DialogResult = DialogResult.Cancel
    End Sub

End Class