' =====================================================
' USERCONTROL RENDEZ-VOUS — Formulaire intégré
' =====================================================
Imports Guna.UI2.WinForms

Public Class ucRendezVous

    Private _tousRdv As New List(Of RendezVous)
    Private _rdvSelectionne As RendezVous = Nothing
    Private _modeAjout As Boolean = True
    Private _dureeMinutes As Integer = 0
    Private _listeClients As New List(Of Client)
    Private _listeEmployes As New List(Of Employe)
    Private _listePrestations As New List(Of Prestation)
    Public Sub New()
        InitializeComponent()

        ' ── Bouton État RDV du jour ──
        Dim btnEtat As New Guna.UI2.WinForms.Guna2Button()
        btnEtat.Text = "📄 État du jour"
        btnEtat.Size = New Size(140, 36)
        btnEtat.Location = New Point(840, 19)   ' juste à gauche de btnNouveauRdv (X=1000)
        btnEtat.BorderRadius = 10
        btnEtat.FillColor = ColorTranslator.FromHtml("#FDE8EF")
        btnEtat.ForeColor = ColorTranslator.FromHtml("#3D1A24")
        btnEtat.Font = New Font("Segoe UI", 9)
        btnEtat.Cursor = Cursors.Hand
        'btnEtat.FocusPainted = False
        AddHandler btnEtat.Click, Sub(s, e) EtatsHelper.EtatRdvDuJour()

        pnlHeader.Controls.Add(btnEtat)
    End Sub
    Private Sub ucRendezVous_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Format du DateTimePicker début
        dtpDebut.CustomFormat = "dd/MM/yyyy HH:mm"
        dtpDebut.Format = DateTimePickerFormat.Custom

        ' Date du jour en français dans le sous-titre header
        lblDate.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                       New System.Globalization.CultureInfo("fr-FR"))

        ' Couleurs header dgv
        dgvRdv.ThemeStyle.HeaderStyle.BackColor = ColorTranslator.FromHtml("#3D1A24")
        dgvRdv.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvRdv.ThemeStyle.RowsStyle.SelectionBackColor = ColorTranslator.FromHtml("#FDE8EF")
        dgvRdv.ThemeStyle.RowsStyle.SelectionForeColor = ColorTranslator.FromHtml("#3D1A24")

        ' Charger l'icône calendrier depuis le dossier Ressources
        Try
            Dim imgCal = ChargerIcone("icons8-calendar-24.png")
            If imgCal IsNot Nothing Then PictureBox1.Image = imgCal
        Catch
        End Try

        ChargerFiltres()
        ChargerFormulaire()
        ViderFormulaire()
        AjouterNoteFormulaire()

        ' Différer le chargement après que Guna2 ait fini son initialisation
        BeginInvoke(Sub() ChargerRdv())
    End Sub

    Private Sub AjouterNoteFormulaire()
        Dim lblNote As New Label()
        lblNote.Text = "ℹ L'heure de fin est calculée automatiquement" &
                   vbCrLf & "selon la durée de la prestation choisie."
        lblNote.Location = New Point(15, 460)
        lblNote.Size = New Size(280, 40)
        lblNote.Font = New Font("Segoe UI", 8)
        lblNote.ForeColor = ColorTranslator.FromHtml("#A07080")
        lblNote.BackColor = Color.Transparent
        pnlFormCorps.Controls.Add(lblNote)
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LES FILTRES
    ' ─────────────────────────────────────────────
    Private Sub ChargerFiltres()
        dtpDate.Value = Date.Today

        cboEmploye.Items.Clear()
        cboEmploye.Items.Add("Toutes")
        Try
            _listeEmployes = Mainframe.EmployeCtrl.GetEmployesActifs()
            For Each emp In _listeEmployes
                cboEmploye.Items.Add(emp.Prenom & " " & emp.Nom)
            Next
        Catch
        End Try
        cboEmploye.SelectedIndex = 0

        cboStatut.Items.Clear()
        cboStatut.Items.Add("Tous")
        cboStatut.Items.Add("En attente")
        cboStatut.Items.Add("Confirmé")
        cboStatut.Items.Add("Terminé")
        cboStatut.Items.Add("Annulé")
        cboStatut.SelectedIndex = 0
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LE FORMULAIRE (ComboBox)
    ' ─────────────────────────────────────────────
    Private Sub ChargerFormulaire()
        ' Clientes
        Try
            _listeClients = Mainframe.ClientCtrl.GetAllClients()
            cboCliente.Items.Clear()
            cboCliente.Items.Add("Sélectionner une cliente...")
            For Each c In _listeClients
                cboCliente.Items.Add(c.Prenom & " " & c.Nom & " | " & c.Telephone)
            Next
            cboCliente.SelectedIndex = 0
        Catch
        End Try

        ' Employées
        Try
            cboEmployeForm.Items.Clear()
            cboEmployeForm.Items.Add("Sélectionner une employée...")
            For Each emp In _listeEmployes
                cboEmployeForm.Items.Add(emp.Prenom & " " & emp.Nom)
            Next
            cboEmployeForm.SelectedIndex = 0
        Catch
        End Try

        ' Prestations
        Try
            _listePrestations = Mainframe.PrestationCtrl.GetPrestationsActives()
            cboPrestationForm.Items.Clear()
            cboPrestationForm.Items.Add("Sélectionner une prestation...")
            For Each p In _listePrestations
                cboPrestationForm.Items.Add(p.Nom & " (" & p.DureeMinutes & " min)")
            Next
            cboPrestationForm.SelectedIndex = 0
        Catch
        End Try

        ' Statuts
        cboStatutForm.Items.Clear()
        cboStatutForm.Items.Add("En attente")
        cboStatutForm.Items.Add("Confirmé")
        cboStatutForm.Items.Add("Terminé")
        cboStatutForm.Items.Add("Annulé")
        cboStatutForm.SelectedIndex = 0

        dtpDebut.Value = DateTime.Now
    End Sub

    ' ─────────────────────────────────────────────
    ' RECRÉER LES COLONNES (protection anti-bug Guna2)
    ' ─────────────────────────────────────────────
    ' ─────────────────────────────────────────────
    ' CHARGER L'ICÔNE DEPUIS LE DOSSIER RESSOURCES
    ' ─────────────────────────────────────────────
    ' ─────────────────────────────────────────────
    ' CHARGER L'ICÔNE DEPUIS LE DOSSIER RESSOURCES
    ' Cherche dans plusieurs emplacements possibles
    ' ─────────────────────────────────────────────
    Private Function ChargerIcone(nomFichier As String) As Image
        Dim candidats() As String = {
            Application.StartupPath & "\Ressources\" & nomFichier,
            Application.StartupPath & "\..\Ressources\" & nomFichier,
            Application.StartupPath & "\..\..\Ressources\" & nomFichier,
            Application.StartupPath & "\..\..\..\Ressources\" & nomFichier,
            "C:\Users\Clever\Desktop\BeautyManager\Ressources\" & nomFichier
        }
        For Each chemin In candidats
            Try
                Dim cheminNormalise = System.IO.Path.GetFullPath(chemin)
                If System.IO.File.Exists(cheminNormalise) Then
                    Return Image.FromFile(cheminNormalise)
                End If
            Catch
            End Try
        Next
        Return Nothing
    End Function

    Private Sub AssurerColonnes()
        ' Guna2 vide et/ou duplique les colonnes après le rendu.
        ' On efface toujours et on recrée proprement par code.
        dgvRdv.Columns.Clear()

        Dim c1 As New DataGridViewTextBoxColumn()
        c1.Name = "colCliente" : c1.HeaderText = "Cliente" : c1.Width = 165

        Dim c2 As New DataGridViewTextBoxColumn()
        c2.Name = "colHeure" : c2.HeaderText = "Heure" : c2.Width = 120

        Dim c3 As New DataGridViewTextBoxColumn()
        c3.Name = "colPrestation" : c3.HeaderText = "Prestation"
        c3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        c3.MinimumWidth = 150

        Dim c4 As New DataGridViewTextBoxColumn()
        c4.Name = "colEmploye" : c4.HeaderText = "Employé" : c4.Width = 140

        Dim c5 As New DataGridViewTextBoxColumn()
        c5.Name = "colStatut" : c5.HeaderText = "Statut" : c5.Width = 95

        ' Colonne Modifier — icons8-edit-24.png depuis Ressources
        Dim c6 As New DataGridViewImageColumn()
        c6.Name = "colModifier"
        c6.HeaderText = ""
        c6.Width = 38
        c6.Image = ChargerIcone("edit2.png")
        c6.ImageLayout = DataGridViewImageCellLayout.Zoom

        ' Colonne Supprimer — icons8-trash-24.png depuis Ressources
        Dim c7 As New DataGridViewImageColumn()
        c7.Name = "colSupprimer"
        c7.HeaderText = ""
        c7.Width = 38
        c7.Image = ChargerIcone("icons8-trash-24.png")
        c7.ImageLayout = DataGridViewImageCellLayout.Zoom

        dgvRdv.Columns.AddRange(c1, c2, c3, c4, c5, c6, c7)

        ' Réappliquer les styles après recréation
        dgvRdv.ThemeStyle.HeaderStyle.BackColor = ColorTranslator.FromHtml("#3D1A24")
        dgvRdv.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvRdv.ThemeStyle.RowsStyle.SelectionBackColor = ColorTranslator.FromHtml("#FDE8EF")
        dgvRdv.ThemeStyle.RowsStyle.SelectionForeColor = ColorTranslator.FromHtml("#3D1A24")
    End Sub

    ' ─────────────────────────────────────────────
    ' AJUSTER LES LARGEURS (appelé quand les colonnes existent)
    ' ─────────────────────────────────────────────
    Private Sub AjusterLargeursColonnes()
        Try
            dgvRdv.Columns("colCliente").Width = 160
            dgvRdv.Columns("colHeure").Width = 130
            dgvRdv.Columns("colEmploye").Width = 140
            dgvRdv.Columns("colStatut").Width = 95
            dgvRdv.Columns("colModifier").Width = 36
            dgvRdv.Columns("colSupprimer").Width = 36
        Catch
            ' Si une colonne est introuvable, on ignore silencieusement
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LES RDV
    ' ─────────────────────────────────────────────
    Private Sub ChargerRdv()
        Try
            ' S'assurer que les colonnes existent (Guna2 peut les avoir vidées)
            AssurerColonnes()

            ' Corriger le bug Guna — hauteur header
            dgvRdv.ColumnHeadersHeight = 38
            dgvRdv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            dgvRdv.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            dgvRdv.ThemeStyle.HeaderStyle.Height = 38

            ' Ajuster les largeurs pour éviter la troncature
            AjusterLargeursColonnes()

            dgvRdv.Rows.Clear()
            _tousRdv = Mainframe.RendezVousCtrl.GetRdvByDate(dtpDate.Value)
            Dim liste = FiltrerRdv()
            AfficherRdv(liste)
            MettreAJourStats(liste)
        Catch ex As Exception
            MsgBox("Erreur chargement RDV : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Function FiltrerRdv() As List(Of RendezVous)
        Dim liste = _tousRdv.ToList()

        If cboEmploye.SelectedIndex > 0 Then
            Dim idEmp = _listeEmployes(cboEmploye.SelectedIndex - 1).IdEmploye
            Dim temp As New List(Of RendezVous)
            For Each r In liste
                If r.IdEmploye = idEmp Then temp.Add(r)
            Next
            liste = temp
        End If

        If cboStatut.SelectedIndex > 0 Then
            Dim statut = cboStatut.SelectedItem.ToString()
            Dim temp As New List(Of RendezVous)
            For Each r In liste
                If r.Statut = statut Then temp.Add(r)
            Next
            liste = temp
        End If

        Return liste
    End Function

    Private Sub AfficherRdv(liste As List(Of RendezVous))
        dgvRdv.Rows.Clear()

        For Each rdv In liste
            Dim nomCliente = "—"
            Dim nomPrestation = "—"
            Dim nomEmploye = "—"

            Try
                Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                If c IsNot Nothing Then nomCliente = c.Prenom & " " & c.Nom
            Catch
            End Try
            Try
                Dim p = Mainframe.PrestationCtrl.GetPrestationById(rdv.IdPrestation)
                If p IsNot Nothing Then nomPrestation = p.Nom
            Catch
            End Try
            Try
                Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
                If emp IsNot Nothing Then nomEmploye = emp.Prenom & " " & emp.Nom
            Catch
            End Try

            Dim heure = rdv.DateHeureDebut.ToString("HH:mm") & " – " &
                        rdv.DateHeureFin.ToString("HH:mm")

            Dim index = dgvRdv.Rows.Add(nomCliente, heure, nomPrestation,
                                         nomEmploye, rdv.Statut,
                                         ChargerIcone("edit2.png"),
                                         ChargerIcone("icons8-trash-24.png"))
            dgvRdv.Rows(index).Tag = rdv

            ' Supprimer le fond bleu sur les cellules icônes
            dgvRdv.Rows(index).Cells("colModifier").Style.BackColor = Color.White
            dgvRdv.Rows(index).Cells("colModifier").Style.SelectionBackColor =
                ColorTranslator.FromHtml("#FDE8EF")
            dgvRdv.Rows(index).Cells("colSupprimer").Style.BackColor = Color.White
            dgvRdv.Rows(index).Cells("colSupprimer").Style.SelectionBackColor =
                ColorTranslator.FromHtml("#FDE8EF")

            Select Case rdv.Statut
                Case "Confirmé"
                    dgvRdv.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#0F6E56")
                    dgvRdv.Rows(index).Cells("colStatut").Style.BackColor =
                        ColorTranslator.FromHtml("#E1F5EE")
                Case "Annulé"
                    dgvRdv.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#A32D2D")
                    dgvRdv.Rows(index).Cells("colStatut").Style.BackColor =
                        ColorTranslator.FromHtml("#FCEBEB")
                    dgvRdv.Rows(index).DefaultCellStyle.ForeColor =
                        ColorTranslator.FromHtml("#A07080")
                Case "Terminé"
                    dgvRdv.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#A07080")
                    dgvRdv.Rows(index).DefaultCellStyle.ForeColor =
                        ColorTranslator.FromHtml("#CCBBCC")
                Case Else ' En attente
                    dgvRdv.Rows(index).Cells("colStatut").Style.ForeColor =
                        ColorTranslator.FromHtml("#854F0B")
                    dgvRdv.Rows(index).Cells("colStatut").Style.BackColor =
                        ColorTranslator.FromHtml("#FEF5E7")
            End Select
        Next

        dgvRdv.ClearSelection()
    End Sub

    Private Sub MettreAJourStats(liste As List(Of RendezVous))
        lblStatVal1.Text = liste.Count.ToString()
        Dim nbConfirmes = 0
        Dim nbAttente = 0
        For Each r In liste
            If r.Statut = "Confirmé" Then nbConfirmes += 1
            If r.Statut = "En attente" Then nbAttente += 1
        Next
        lblStatVal2.Text = nbConfirmes.ToString()
        lblStatVal3.Text = nbAttente.ToString()
    End Sub

    ' ─────────────────────────────────────────────
    ' FILTRES
    ' ─────────────────────────────────────────────
    Private Sub dtpDate_ValueChanged(sender As Object, e As EventArgs) _
        Handles dtpDate.ValueChanged
        ChargerRdv()
    End Sub

    Private Sub cboEmploye_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboEmploye.SelectedIndexChanged
        If _tousRdv.Count = 0 Then Return
        Dim liste = FiltrerRdv()
        AfficherRdv(liste)
        MettreAJourStats(liste)
    End Sub

    Private Sub cboStatut_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboStatut.SelectedIndexChanged
        If _tousRdv.Count = 0 Then Return
        Dim liste = FiltrerRdv()
        AfficherRdv(liste)
        MettreAJourStats(liste)
    End Sub

    ' ─────────────────────────────────────────────
    ' NOUVEAU RDV
    ' ─────────────────────────────────────────────
    Private Sub btnNouveauRdv_Click(sender As Object, e As EventArgs) _
        Handles btnNouveauRdv.Click
        ViderFormulaire()
    End Sub

    ' ─────────────────────────────────────────────
    ' CLIC SUR CELLULE — Modifier ou Supprimer
    ' ─────────────────────────────────────────────
    Private Sub dgvRdv_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles dgvRdv.CellClick

        If e.RowIndex < 0 Then Return
        Dim rdv = TryCast(dgvRdv.Rows(e.RowIndex).Tag, RendezVous)
        If rdv Is Nothing Then Return

        If e.ColumnIndex = dgvRdv.Columns("colModifier").Index Then
            RemplirFormulaire(rdv)
        End If

        If e.ColumnIndex = dgvRdv.Columns("colSupprimer").Index Then
            Dim nomCliente = dgvRdv.Rows(e.RowIndex).Cells("colCliente").Value?.ToString()
            Dim heure = dgvRdv.Rows(e.RowIndex).Cells("colHeure").Value?.ToString()
            Dim rep = MsgBox("Supprimer le rendez-vous de " & nomCliente &
                 " à " & heure & " ?" & vbCrLf & "Cette action est irréversible.",
                 MsgBoxStyle.YesNo Or MsgBoxStyle.Critical, "Confirmer la suppression")
            If rep = MsgBoxResult.No Then Return
            Try
                Mainframe.RendezVousCtrl.SupprimerRdv(rdv.IdRdv)
                MsgBox("Rendez-vous supprimé.", MsgBoxStyle.Information, "Succès")
                ChargerRdv()
                ViderFormulaire()
            Catch ex As Exception
                MsgBox("Erreur : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
            End Try
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' REMPLIR FORMULAIRE POUR MODIFICATION
    ' ─────────────────────────────────────────────
    Private Sub RemplirFormulaire(rdv As RendezVous)
        _rdvSelectionne = rdv
        _modeAjout = False

        lblTitreForm.Text = "Modifier le rendez-vous"
        lblSousTitreForm.Text = "Modifiez les informations"

        cboStatutForm.Items.Clear()
        cboStatutForm.Items.Add("En attente")
        cboStatutForm.Items.Add("Confirmé")
        cboStatutForm.Items.Add("Terminé")
        cboStatutForm.Items.Add("Annulé")

        For i = 0 To _listeClients.Count - 1
            If _listeClients(i).IdClient = rdv.IdClient Then
                cboCliente.SelectedIndex = i + 1 : Exit For
            End If
        Next
        For i = 0 To _listeEmployes.Count - 1
            If _listeEmployes(i).IdEmploye = rdv.IdEmploye Then
                cboEmployeForm.SelectedIndex = i + 1 : Exit For
            End If
        Next
        For i = 0 To _listePrestations.Count - 1
            If _listePrestations(i).IdPrestation = rdv.IdPrestation Then
                cboPrestationForm.SelectedIndex = i + 1 : Exit For
            End If
        Next
        ' Recalculer la durée après avoir sélectionné la prestation par code
        If cboPrestationForm.SelectedIndex > 0 Then
            Dim p = _listePrestations(cboPrestationForm.SelectedIndex - 1)
            _dureeMinutes = p.DureeMinutes
            CalculerHeureFin()
        End If
        dtpDebut.Value = rdv.DateHeureDebut
        cboStatutForm.SelectedItem = rdv.Statut
    End Sub

    ' ─────────────────────────────────────────────
    ' VIDER FORMULAIRE
    ' ─────────────────────────────────────────────
    Private Sub ViderFormulaire()
        _rdvSelectionne = Nothing
        _modeAjout = True
        _dureeMinutes = 0

        lblTitreForm.Text = "Nouveau rendez-vous"
        lblSousTitreForm.Text = "Remplissez les informations"

        cboStatutForm.Items.Clear()
        cboStatutForm.Items.Add("En attente")
        cboStatutForm.Items.Add("Confirmé")
        cboStatutForm.SelectedIndex = 0

        cboCliente.SelectedIndex = 0
        cboEmployeForm.SelectedIndex = 0
        cboPrestationForm.SelectedIndex = 0
        dtpDebut.Value = DateTime.Now
        txtHeureFin.Text = ""
        lblDureeInfo.Text = "Sélectionnez une prestation"
        lblConflitAvertissement.Visible = False

        dgvRdv.ClearSelection()
    End Sub

    Private Sub btnVider_Click(sender As Object, e As EventArgs) _
        Handles btnVider.Click
        ViderFormulaire()
    End Sub

    ' ─────────────────────────────────────────────
    ' CALCUL HEURE DE FIN
    ' ─────────────────────────────────────────────
    Private Sub cboPrestationForm_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboPrestationForm.SelectedIndexChanged

        If cboPrestationForm.SelectedIndex <= 0 Then
            lblDureeInfo.Text = "Sélectionnez une prestation"
            txtHeureFin.Text = ""
            _dureeMinutes = 0
            Return
        End If

        Dim p = _listePrestations(cboPrestationForm.SelectedIndex - 1)
        _dureeMinutes = p.DureeMinutes
        CalculerHeureFin()
        lblDureeInfo.Text = "Durée : " & _dureeMinutes & " min — fin calculée automatiquement"
        VerifierConflitVisuel()
    End Sub

    Private Sub dtpDebut_ValueChanged(sender As Object, e As EventArgs) _
        Handles dtpDebut.ValueChanged
        CalculerHeureFin()
        VerifierConflitVisuel()
    End Sub

    Private Sub CalculerHeureFin()
        If _dureeMinutes <= 0 Then Return
        txtHeureFin.Text = dtpDebut.Value.AddMinutes(_dureeMinutes).ToString("HH:mm")
    End Sub

    ' ─────────────────────────────────────────────
    ' VÉRIFICATION CONFLIT
    ' ─────────────────────────────────────────────
    Private Sub cboEmployeForm_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboEmployeForm.SelectedIndexChanged
        VerifierConflitVisuel()
    End Sub

    Private Sub VerifierConflitVisuel()
        lblConflitAvertissement.Visible = False
        If cboEmployeForm.SelectedIndex <= 0 OrElse _dureeMinutes <= 0 Then Return

        Try
            Dim emp = _listeEmployes(cboEmployeForm.SelectedIndex - 1)
            Dim debut = dtpDebut.Value
            Dim fin = debut.AddMinutes(_dureeMinutes)
            Dim idExclure = If(_modeAjout, 0, _rdvSelectionne.IdRdv)

            If Mainframe.RendezVousCtrl.VerifierConflit(emp.IdEmploye, debut, fin, idExclure) Then
                lblConflitAvertissement.Text = "  Attention : " & emp.Prenom &
                                               " a déjà un RDV sur ce créneau !"
                lblConflitAvertissement.Visible = True
            End If
        Catch
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' ENREGISTRER
    ' ─────────────────────────────────────────────
    Private Sub btnEnregistrerRdv_Click(sender As Object, e As EventArgs) _
        Handles btnEnregistrerRdv.Click

        If cboCliente.SelectedIndex <= 0 Then
            MsgBox("Veuillez sélectionner une cliente.", MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If
        If cboEmployeForm.SelectedIndex <= 0 Then
            MsgBox("Veuillez sélectionner une employée.", MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If
        If cboPrestationForm.SelectedIndex <= 0 Then
            MsgBox("Veuillez sélectionner une prestation.", MsgBoxStyle.Exclamation, "Champ manquant")
            Return

        End If
        If _dureeMinutes <= 0 Then
            MsgBox("Impossible de calculer l'heure de fin." & vbCrLf &
           "Veuillez sélectionner une prestation avec une durée valide.",
           MsgBoxStyle.Exclamation, "Durée manquante")
            Return
        End If
        If dtpDebut.Value < DateTime.Now.AddMinutes(-5) AndAlso _modeAjout Then
            Dim repi = MsgBox("La date choisie est dans le passé. Continuer quand même ?",
                             MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Date passée")
            If repi = MsgBoxResult.No Then Return
        End If
        ' ── Vérifier conflit employée ──
        If cboEmployeForm.SelectedIndex > 0 AndAlso _dureeMinutes > 0 Then
            Dim emp = _listeEmployes(cboEmployeForm.SelectedIndex - 1)
            Dim debut = dtpDebut.Value
            Dim fin = debut.AddMinutes(_dureeMinutes)
            Dim idExclure = If(_modeAjout, 0, _rdvSelectionne.IdRdv)

            If Mainframe.RendezVousCtrl.VerifierConflit(emp.IdEmploye, debut, fin, idExclure) Then
                Dim repi = MsgBox("Attention : " & emp.Prenom & " a déjà un rendez-vous sur ce créneau !" &
                         vbCrLf & "Voulez-vous quand même enregistrer ?",
                         MsgBoxStyle.YesNo Or MsgBoxStyle.Critical, "Conflit de planning")
                If repi = MsgBoxResult.No Then Return
            End If
        End If
        Dim rdv As New RendezVous()
        rdv.IdClient = _listeClients(cboCliente.SelectedIndex - 1).IdClient
        rdv.IdEmploye = _listeEmployes(cboEmployeForm.SelectedIndex - 1).IdEmploye
        rdv.IdPrestation = _listePrestations(cboPrestationForm.SelectedIndex - 1).IdPrestation
        rdv.DateHeureDebut = dtpDebut.Value
        rdv.DateHeureFin = dtpDebut.Value.AddMinutes(_dureeMinutes)
        rdv.Statut = If(cboStatutForm.SelectedIndex >= 0,
                        cboStatutForm.SelectedItem.ToString(), "En attente")

        Dim nomCliente = cboCliente.SelectedItem.ToString().Split("|")(0).Trim()
        Dim action = If(_modeAjout, "Confirmer l'ajout du rendez-vous de " & nomCliente & " ?",
               "Confirmer la modification de ce rendez-vous ?")
        Dim rep = MsgBox(action, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmation")
        If rep <> MsgBoxResult.Yes Then Return
        Try
            If _modeAjout Then
                Mainframe.RendezVousCtrl.AjouterRdv(rdv)
                MsgBox("Rendez-vous ajouté avec succès.", MsgBoxStyle.Information, "Succès")
            Else
                rdv.IdRdv = _rdvSelectionne.IdRdv
                Mainframe.RendezVousCtrl.ModifierRdv(rdv)
                MsgBox("Rendez-vous modifié avec succès.", MsgBoxStyle.Information, "Modification")
            End If

            ChargerRdv()
            ViderFormulaire()

        Catch ex As Exception
            MsgBox("Erreur : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub txtHeureFin_TextChanged(sender As Object, e As EventArgs) Handles txtHeureFin.TextChanged

    End Sub

    Private Sub lblFiltreStatut_Click(sender As Object, e As EventArgs) Handles lblFiltreStatut.Click

    End Sub

    Private Sub pnlHeader_Paint(sender As Object, e As PaintEventArgs) Handles pnlHeader.Paint

    End Sub

End Class