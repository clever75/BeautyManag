' =====================================================
' FORM RENDEZ-VOUS — Ajout et Modification
' =====================================================
Imports Guna.UI2.WinForms

Public Class frmRdv

    Private _rdv As RendezVous = Nothing
    Private _modeAjout As Boolean = True
    Private _dureeMinutes As Integer = 0
    Private _listeClients As New List(Of Client)
    Private _listeEmployes As New List(Of Employe)
    Private _listePrestations As New List(Of Prestation)

    Public Sub New(rdv As RendezVous)
        InitializeComponent()
        If rdv Is Nothing Then
            _modeAjout = True
        Else
            _modeAjout = False
            _rdv = rdv
        End If
    End Sub

    Private Sub frmRdv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChargerClientes()
        ChargerEmployees()
        ChargerPrestations()
        ChargerStatuts()

        If _modeAjout Then
            lblTitreForm.Text = "Nouveau rendez-vous"
            lblSousTitreForm.Text = "Remplissez les informations"
            dtpDebut.Value = DateTime.Now
        Else
            lblTitreForm.Text = "Modifier le rendez-vous"
            lblSousTitreForm.Text = "Modifiez les informations"
            RemplirFormulaire(_rdv)
        End If
    End Sub

    Private Sub ChargerClientes()
        Try
            _listeClients = Mainframe.ClientCtrl.GetAllClients()
            cboCliente.Items.Clear()
            cboCliente.Items.Add("Sélectionner une cliente...")
            For Each c In _listeClients
                cboCliente.Items.Add(c.Prenom & " " & c.Nom & " | " & c.Telephone)
            Next
            cboCliente.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement clientes : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Sub ChargerEmployees()
        Try
            _listeEmployes = Mainframe.EmployeCtrl.GetEmployesActifs()
            cboEmployeForm.Items.Clear()
            cboEmployeForm.Items.Add("Sélectionner une employée...")
            For Each emp In _listeEmployes
                cboEmployeForm.Items.Add(emp.Prenom & " " & emp.Nom)
            Next
            cboEmployeForm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement employées : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Sub ChargerPrestations()
        Try
            _listePrestations = Mainframe.PrestationCtrl.GetPrestationsActives()
            cboPrestationForm.Items.Clear()
            cboPrestationForm.Items.Add("Sélectionner une prestation...")
            For Each p In _listePrestations
                cboPrestationForm.Items.Add(p.Nom & " (" & p.DureeMinutes & " min)")
            Next
            cboPrestationForm.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement prestations : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Sub ChargerStatuts()
        cboStatutForm.Items.Clear()
        cboStatutForm.Items.Add("En attente")
        cboStatutForm.Items.Add("Confirmé")
        cboStatutForm.Items.Add("Annulé")
        cboStatutForm.SelectedIndex = 0
    End Sub

    Private Sub RemplirFormulaire(rdv As RendezVous)
        Try
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
            dtpDebut.Value = rdv.DateHeureDebut
            cboStatutForm.SelectedItem = rdv.Statut
        Catch ex As Exception
            MsgBox("Erreur remplissage : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
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
            Dim idExclure = If(_modeAjout, 0, _rdv.IdRdv)

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
        If dtpDebut.Value < DateTime.Now.AddMinutes(-10) AndAlso _modeAjout Then
            Dim rep = MsgBox("La date choisie est dans le passé. Continuer quand même ?",
                             MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Date passée")
            If rep = MsgBoxResult.No Then Return
        End If

        Dim rdv As New RendezVous()
        rdv.IdClient = _listeClients(cboCliente.SelectedIndex - 1).IdClient
        rdv.IdEmploye = _listeEmployes(cboEmployeForm.SelectedIndex - 1).IdEmploye
        rdv.IdPrestation = _listePrestations(cboPrestationForm.SelectedIndex - 1).IdPrestation
        rdv.DateHeureDebut = dtpDebut.Value
        rdv.DateHeureFin = dtpDebut.Value.AddMinutes(_dureeMinutes)
        rdv.Statut = If(cboStatutForm.SelectedIndex >= 0,
                        cboStatutForm.SelectedItem.ToString(), "En attente")

        Try
            If _modeAjout Then
                Mainframe.RendezVousCtrl.AjouterRdv(rdv)
                MsgBox("Rendez-vous ajouté avec succès.", MsgBoxStyle.Information, "Succès")
            Else
                rdv.IdRdv = _rdv.IdRdv
                Mainframe.RendezVousCtrl.ModifierRdv(rdv)
                MsgBox("Rendez-vous modifié avec succès.", MsgBoxStyle.Information, "Modification réussie")
            End If
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MsgBox("Erreur : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' ANNULER
    ' ─────────────────────────────────────────────
    Private Sub btnAnnulerRdv_Click(sender As Object, e As EventArgs) _
        Handles btnAnnulerRdv.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class