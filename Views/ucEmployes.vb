' =====================================================
' USERCONTROL EMPLOYES
' =====================================================
Imports Guna.UI2.WinForms
Imports System.Text.RegularExpressions

Public Class ucEmployes

    Private _employeSelectionne As Employe = Nothing
    Private _modeAjout As Boolean = False
    Private _filtreCourant As String = "Tous"

    ' ─────────────────────────────────────────────
    ' CHARGEMENT
    ' ─────────────────────────────────────────────
    Private Sub ucEmployes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblDate.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                       New System.Globalization.CultureInfo("fr-FR"))
        ViderFiche()
        pnlFiche.Visible = False

        ' Différer le chargement après que Guna2 ait fini son initialisation
        BeginInvoke(Sub() ChargerEmployes())
    End Sub

    ' ─────────────────────────────────────────────
    ' INITIALISER LES COLONNES DGV (protection anti-bug Guna2)
    ' ─────────────────────────────────────────────
    Private Sub InitialiserColonnesDgv()
        dgvEmployes.Columns.Clear()

        Dim c1 As New DataGridViewTextBoxColumn()
        c1.Name = "colNom"
        c1.HeaderText = "Employé"
        c1.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        c1.ReadOnly = True

        Dim c2 As New DataGridViewTextBoxColumn()
        c2.Name = "colSpec"
        c2.HeaderText = "Spécialité"
        c2.Width = 125
        c2.ReadOnly = True

        Dim c3 As New DataGridViewTextBoxColumn()
        c3.Name = "colStatut"
        c3.HeaderText = "Statut"
        c3.Width = 75
        c3.ReadOnly = True

        dgvEmployes.Columns.AddRange(c1, c2, c3)
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LA LISTE
    ' ─────────────────────────────────────────────
    Private Sub ChargerEmployes()
        Try
            ' ── Recréer les colonnes si Guna2 les a effacées ou doublées ──
            If dgvEmployes.Columns.Count = 0 OrElse dgvEmployes.Columns.Count > 3 Then
                InitialiserColonnesDgv()
            End If

            dgvEmployes.Rows.Clear()

            Dim liste As List(Of Employe)

            Select Case _filtreCourant
                Case "Actifs"
                    liste = Mainframe.EmployeCtrl.GetEmployesActifs()
                Case "Inactifs"
                    liste = Mainframe.EmployeCtrl.GetAllEmployes().
                            Where(Function(emp) Not emp.Actif).ToList()
                Case Else
                    liste = Mainframe.EmployeCtrl.GetAllEmployes()
            End Select

            ' Filtrer par recherche
            If Not String.IsNullOrWhiteSpace(txtRecherche.Text) Then
                liste = liste.Where(Function(emp)
                                        Return emp.Nom.ToLower.Contains(txtRecherche.Text.ToLower) OrElse
                                               emp.Prenom.ToLower.Contains(txtRecherche.Text.ToLower)
                                    End Function).ToList()
            End If

            ' Message si liste vide
            If liste.Count = 0 Then
                lblSousTitre.Text = "Aucun résultat trouvé"
            Else
                lblSousTitre.Text = liste.Count & " employé(s)"
            End If

            For Each emp In liste
                Dim index = dgvEmployes.Rows.Add(
                    emp.Prenom & " " & emp.Nom,
                    If(String.IsNullOrEmpty(emp.Specialite), "—", emp.Specialite),
                    If(emp.Actif, "Actif", "Inactif")
                )

                dgvEmployes.Rows(index).Tag = emp

                ' Colorier la cellule statut
                If emp.Actif Then
                    dgvEmployes.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#0F6E56")
                    dgvEmployes.Rows(index).Cells("colStatut").Style.BackColor =
                        ColorTranslator.FromHtml("#E1F5EE")
                Else
                    dgvEmployes.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#854F0B")
                    dgvEmployes.Rows(index).Cells("colStatut").Style.BackColor =
                        ColorTranslator.FromHtml("#FEF5E7")
                End If
            Next

            dgvEmployes.ClearSelection()

        Catch ex As Exception
            MsgBox("Erreur chargement employés : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' SÉLECTION D'UN EMPLOYÉ
    ' ─────────────────────────────────────────────
    Private Sub dgvEmployes_SelectionChanged(sender As Object, e As EventArgs) _
        Handles dgvEmployes.SelectionChanged

        If dgvEmployes.SelectedRows.Count = 0 Then Return
        If Not Me.IsHandleCreated Then Return

        Dim row = dgvEmployes.SelectedRows(0)
        If row.Tag Is Nothing Then Return

        Dim emp = TryCast(row.Tag, Employe)
        If emp Is Nothing Then Return

        _employeSelectionne = emp
        _modeAjout = False
        RemplirFiche(emp)
    End Sub

    ' ─────────────────────────────────────────────
    ' REMPLIR LA FICHE
    ' ─────────────────────────────────────────────
    Private Sub RemplirFiche(emp As Employe)
        lblNomEmploye.Text = FormatPrenom(emp.Prenom) & " " & emp.Nom.ToUpper()
        txtNom.Text = emp.Nom.ToUpper()
        txtPrenom.Text = FormatPrenom(emp.Prenom)
        If emp Is Nothing Then Return
        pnlFiche.Visible = True
        lblSpecEmploye.Text = If(String.IsNullOrEmpty(emp.Specialite), "", emp.Specialite)

        txtNom.Text = emp.Nom
        txtPrenom.Text = emp.Prenom
        txtSpecialite.Text = If(String.IsNullOrEmpty(emp.Specialite), "", emp.Specialite)

        ' APRÈS - texte fixe "Supprimer"
        btnDesactiver.Text = "Supprimer"
        btnDesactiver.FillColor = ColorTranslator.FromHtml("#FCEBEB")
        btnDesactiver.ForeColor = ColorTranslator.FromHtml("#A32D2D")

        btnEnregistrer.Enabled = True
        btnDesactiver.Enabled = True
    End Sub

    ' ─────────────────────────────────────────────
    ' VIDER LA FICHE
    ' ─────────────────────────────────────────────
    Private Sub ViderFiche()
        lblNomEmploye.Text = "Sélectionner un employé"
        lblSpecEmploye.Text = ""
        txtNom.Text = ""
        txtPrenom.Text = ""
        txtSpecialite.Text = ""
        btnEnregistrer.Enabled = False
        btnDesactiver.Enabled = False
        _employeSelectionne = Nothing
    End Sub

    ' ─────────────────────────────────────────────
    ' RECHERCHE EN TEMPS RÉEL
    ' ─────────────────────────────────────────────
    Private Sub txtRecherche_TextChanged(sender As Object, e As EventArgs) _
        Handles txtRecherche.TextChanged
        ChargerEmployes()
    End Sub

    ' ─────────────────────────────────────────────
    ' FILTRES
    ' ─────────────────────────────────────────────
    Private Sub ActiverFiltre(btnActif As Guna2Button, filtre As String)
        btnFiltreAll.FillColor = ColorTranslator.FromHtml("#FDE8EF")
        btnFiltreAll.ForeColor = ColorTranslator.FromHtml("#C45A7E")
        btnFiltreActifs.FillColor = ColorTranslator.FromHtml("#E1F5EE")
        btnFiltreActifs.ForeColor = ColorTranslator.FromHtml("#0F6E56")
        btnFiltreInactifs.FillColor = ColorTranslator.FromHtml("#FEF5E7")
        btnFiltreInactifs.ForeColor = ColorTranslator.FromHtml("#854F0B")

        btnActif.FillColor = ColorTranslator.FromHtml("#C45A7E")
        btnActif.ForeColor = Color.White
        _filtreCourant = filtre
        ChargerEmployes()
    End Sub

    Private Sub btnFiltreAll_Click(s As Object, e As EventArgs) Handles btnFiltreAll.Click
        ActiverFiltre(btnFiltreAll, "Tous")
    End Sub

    Private Sub btnFiltreActifs_Click(s As Object, e As EventArgs) Handles btnFiltreActifs.Click
        ActiverFiltre(btnFiltreActifs, "Actifs")
    End Sub

    Private Sub btnFiltreInactifs_Click(s As Object, e As EventArgs) Handles btnFiltreInactifs.Click
        ActiverFiltre(btnFiltreInactifs, "Inactifs")
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON AJOUTER EMPLOYÉ
    ' ─────────────────────────────────────────────
    Private Sub btnAjouterEmploye_Click(sender As Object, e As EventArgs) _
        Handles btnAjouterEmploye.Click
        _modeAjout = True
        _employeSelectionne = Nothing
        dgvEmployes.ClearSelection()
        ViderFiche()
        pnlFiche.Visible = True
        lblNomEmploye.Text = "Nouvel employé"
        btnEnregistrer.Enabled = True
        btnDesactiver.Enabled = False
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
        If txtNom.Text.Trim().Length < 2 Then
            MsgBox("Le nom doit contenir au moins 2 caractères.",
           MsgBoxStyle.Exclamation, "Nom trop court")
            txtNom.Focus()
            Return
        End If

        If txtPrenom.Text.Trim().Length < 2 Then
            MsgBox("Le prénom doit contenir au moins 2 caractères.",
           MsgBoxStyle.Exclamation, "Prénom trop court")
            txtPrenom.Focus()
            Return
        End If

        ' ── Longueur maximale ──
        If txtNom.Text.Trim().Length > 50 Then
            MsgBox("Le nom ne peut pas dépasser 50 caractères.",
                   MsgBoxStyle.Exclamation, "Nom trop long")
            txtNom.Focus()
            Return
        End If

        If txtPrenom.Text.Trim().Length > 50 Then
            MsgBox("Le prénom ne peut pas dépasser 50 caractères.",
                   MsgBoxStyle.Exclamation, "Prénom trop long")
            txtPrenom.Focus()
            Return
        End If

        ' ── Nom : lettres seulement ──
        If Not Regex.IsMatch(txtNom.Text.Trim(), "^[a-zA-ZÀ-ÿ\s\-']+$") Then
            MsgBox("Le nom ne doit contenir que des lettres." & vbCrLf & "Exemple : Amewonu",
                   MsgBoxStyle.Exclamation, "Nom invalide")
            txtNom.Focus()
            Return
        End If

        ' ── Prénom : lettres seulement ──
        If Not Regex.IsMatch(txtPrenom.Text.Trim(), "^[a-zA-ZÀ-ÿ\s\-']+$") Then
            MsgBox("Le prénom ne doit contenir que des lettres." & vbCrLf & "Exemple : Sophie",
                   MsgBoxStyle.Exclamation, "Prénom invalide")
            txtPrenom.Focus()
            Return
        End If

        ' ── Vérifier doublon nom + prénom ──
        Dim idExclure As Integer = If(_modeAjout, 0, _employeSelectionne.IdEmploye)
        If Mainframe.EmployeCtrl.NomPrenomExiste(txtNom.Text.Trim(),
                                                  txtPrenom.Text.Trim(),
                                                  idExclure) Then
            MsgBox("Un employé avec ce nom et prénom existe déjà.",
                   MsgBoxStyle.Exclamation, "Doublon détecté")
            txtNom.Focus()
            Return
        End If
        If txtSpecialite.Text.Trim().Length > 100 Then
            MsgBox("La spécialité ne peut pas dépasser 100 caractères.",
           MsgBoxStyle.Exclamation, "Spécialité trop longue")
            txtSpecialite.Focus()
            Return
        End If
        ' ── Construire l'objet ──
        Dim emp As New Employe()
        emp.Nom = txtNom.Text.Trim().ToUpper()
        emp.Prenom = FormatPrenom(txtPrenom.Text)
        emp.Specialite = If(String.IsNullOrWhiteSpace(txtSpecialite.Text), "", txtSpecialite.Text.Trim())
        emp.Actif = True
        Dim action = If(_modeAjout, "ajouter", "modifier")
        Dim rep = MsgBox("Confirmer " & action & " " &
                 FormatPrenom(txtPrenom.Text) & " " & txtNom.Text.Trim().ToUpper() & " ?",
                 MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmation")
        If rep <> MsgBoxResult.Yes Then Return
        Try
            If _modeAjout Then
                Mainframe.EmployeCtrl.AjouterEmploye(emp)
                MsgBox("L'employée " & emp.Prenom & " " & emp.Nom & " a été ajoutée avec succès.",
                       MsgBoxStyle.Information, "Succès")
            Else
                If _employeSelectionne Is Nothing Then Return
                emp.IdEmploye = _employeSelectionne.IdEmploye
                Mainframe.EmployeCtrl.ModifierEmploye(emp)
                MsgBox("Les informations de " & emp.Prenom & " " & emp.Nom & " ont été modifiées.",
                       MsgBoxStyle.Information, "Modification réussie")
            End If

            ChargerEmployes()
            ViderFiche()

        Catch ex As Exception
            MsgBox("Erreur lors de l'enregistrement : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub



    ' ─────────────────────────────────────────────
    ' BOUTON SUPPRIMER
    ' Logique : suppression physique si pas de lien,
    '           sinon désactivation (soft delete)
    ' ─────────────────────────────────────────────
    Private Sub btnDesactiver_Click(sender As Object, e As EventArgs) _
        Handles btnDesactiver.Click

        If _employeSelectionne Is Nothing Then Return

        Try
            Dim aDesLiens As Boolean = Mainframe.EmployeCtrl.AEmployeDesLiens(_employeSelectionne.IdEmploye)


            If aDesLiens Then
                If Not _employeSelectionne.Actif Then
                    MsgBox(_employeSelectionne.Prenom & " " & _employeSelectionne.Nom &
                           " est déjà marqué(e) comme inactif(ve).",
                           MsgBoxStyle.Information, "Déjà inactif")
                    Return
                End If
                ' ── Lié à des factures → on propose seulement la désactivation ──
                Dim confirm = MsgBox(
                    _employeSelectionne.Prenom & " " & _employeSelectionne.Nom &
                    " est lié(e) à des factures existantes." & vbCrLf &
                    "Il/elle ne peut pas être supprimé(e) définitivement." & vbCrLf & vbCrLf &
                    "Voulez-vous le/la marquer comme inactif(ve) à la place ?",
                    MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Suppression impossible")

                If confirm = MsgBoxResult.Yes Then
                    Mainframe.EmployeCtrl.DesactiverEmploye(_employeSelectionne.IdEmploye)
                    MsgBox(_employeSelectionne.Prenom & " " & _employeSelectionne.Nom &
                           " a été marqué(e) comme inactif(ve)." & vbCrLf &
                           "Il/elle n'apparaîtra plus dans les nouvelles factures.",
                           MsgBoxStyle.Information, "Employé désactivé")
                    ChargerEmployes()
                    ViderFiche()
                End If

            Else
                ' ── Pas de lien → suppression physique possible ──
                Dim confirm = MsgBox(
                    "Supprimer définitivement " &
                    _employeSelectionne.Prenom & " " & _employeSelectionne.Nom & " ?" & vbCrLf &
                    "Cette action est irréversible.",
                    MsgBoxStyle.YesNo Or MsgBoxStyle.Critical, "Confirmation de suppression")

                If confirm = MsgBoxResult.Yes Then
                    Mainframe.EmployeCtrl.SupprimerEmploye(_employeSelectionne.IdEmploye)
                    MsgBox(_employeSelectionne.Prenom & " " & _employeSelectionne.Nom &
                           " a été supprimé(e) définitivement.",
                           MsgBoxStyle.Information, "Suppression réussie")
                    ChargerEmployes()
                    ViderFiche()
                End If
            End If

        Catch ex As Exception
            MsgBox("Erreur lors de la suppression : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON ANNULER
    ' ─────────────────────────────────────────────
    Private Sub btnAnnuler_Click(sender As Object, e As EventArgs) _
        Handles btnAnnuler.Click
        _modeAjout = False
        If _employeSelectionne IsNot Nothing Then
            RemplirFiche(_employeSelectionne)
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

    Private Sub pnlRecherche_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub dgvEmployes_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmployes.CellContentClick

    End Sub

End Class