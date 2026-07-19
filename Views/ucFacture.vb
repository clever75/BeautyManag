' =====================================================
' USERCONTROL FACTURE
' =====================================================
Imports Guna.UI2.WinForms

Public Class ucFacture

    Private _lignes As New List(Of FactureDetail)
    Private _listeProduits As New List(Of Produit)
    Private _listeRdv As New List(Of RendezVous)
    Private _listeClients As New List(Of Client)
    Private _idClientCourant As Integer = 0
    Private _idEmployeCourant As Integer = 0
    Private _prixPrestation As Decimal = 0
    Private _filtreEnCours As Boolean = False
    Private _handlersInit As Boolean = False
    Public Sub New()
        InitializeComponent()

        ' ── Bouton Historique factures ──
        Dim btnEtatFactures As New Guna.UI2.WinForms.Guna2Button()
        btnEtatFactures.Text = "📄 Historique"
        btnEtatFactures.Size = New Size(140, 38)
        btnEtatFactures.Location = New Point(110, 22)  ' à gauche de btnImprimerFacture (X=430)
        btnEtatFactures.BorderRadius = 8
        btnEtatFactures.FillColor = ColorTranslator.FromHtml("#FDE8EF")
        btnEtatFactures.ForeColor = ColorTranslator.FromHtml("#3D1A24")
        btnEtatFactures.Font = New Font("Segoe UI", 9)
        btnEtatFactures.Cursor = Cursors.Hand
        AddHandler btnEtatFactures.Click, Sub(s, e) EtatsHelper.EtatHistoriqueFactures()
        pnlHeader.Controls.Add(btnEtatFactures)

        ' ── Bouton RDV non facturés ──
        Dim btnEtatNonFact As New Guna.UI2.WinForms.Guna2Button()
        btnEtatNonFact.Text = "⚠ Non facturés"
        btnEtatNonFact.Size = New Size(150, 38)
        btnEtatNonFact.Location = New Point(270, 22)
        btnEtatNonFact.BorderRadius = 8
        btnEtatNonFact.FillColor = ColorTranslator.FromHtml("#FEF5E7")
        btnEtatNonFact.ForeColor = ColorTranslator.FromHtml("#854F0B")
        btnEtatNonFact.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        btnEtatNonFact.Cursor = Cursors.Hand
        AddHandler btnEtatNonFact.Click, Sub(s, e) EtatsHelper.EtatRdvNonFactures()
        pnlHeader.Controls.Add(btnEtatNonFact)
    End Sub
    Private Sub ucFacture_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDate.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                       New System.Globalization.CultureInfo("fr-FR"))

        dgvLignes.ThemeStyle.HeaderStyle.BackColor = ColorTranslator.FromHtml("#3D1A24")
        dgvLignes.ThemeStyle.HeaderStyle.ForeColor = Color.White
        dgvLignes.ThemeStyle.RowsStyle.BackColor = Color.White
        dgvLignes.ThemeStyle.RowsStyle.SelectionBackColor = ColorTranslator.FromHtml("#FDE8EF")
        dgvLignes.ThemeStyle.RowsStyle.SelectionForeColor = ColorTranslator.FromHtml("#3D1A24")

        Try
            Dim pathTrash = Application.StartupPath & "\Resources\ic_trash.png"
            If System.IO.File.Exists(pathTrash) Then
                CType(dgvLignes.Columns("colSupprimerLigne"), DataGridViewImageColumn).Image =
                    Image.FromFile(pathTrash)
            End If
        Catch
        End Try

        ChargerModes()
        ChargerModePaiement()
        ChargerProduits()
        ViderFacture()

        ' Configurer l'AutoComplete pour le ComboBox afin d'aider la saisie sans vider la liste
        cboProduitAjouter.DropDownStyle = ComboBoxStyle.DropDown

        If Not _handlersInit Then
            AddHandler cboProduitAjouter.TextChanged, AddressOf FiltrerProduits
            AddHandler cboProduitAjouter.DropDownClosed, AddressOf cboProduitAjouter_DropDownClosed

            _handlersInit = True
        End If


    End Sub

    Private Sub cboProduitAjouter_DropDown(sender As Object, e As EventArgs)
        ' Remplir la liste à l'ouverture du dropdown pour éviter un affichage vide
        Try
            Dim txt = cboProduitAjouter.Text.Trim()
            RemplirItemsProduits(txt)
        Catch
        End Try
    End Sub

    Private Sub RemplirItemsProduits(filter As String)
        If _listeProduits Is Nothing OrElse _listeProduits.Count = 0 Then
            ChargerProduits()
        End If
        Dim texte = filter.Trim().ToLower()
        cboProduitAjouter.BeginUpdate()
        Try
            cboProduitAjouter.Items.Clear()
            cboProduitAjouter.Items.Add("Sélectionner un produit...")
            For Each p In _listeProduits
                Dim itemText = p.Nom & " — " & FormatNumber(p.Prix, 0) & " F (stock: " & p.StockActuel & ")"
                If String.IsNullOrEmpty(texte) OrElse p.Nom.ToLower().Contains(texte) Then
                    cboProduitAjouter.Items.Add(itemText)
                End If
            Next
            ' si possible, restaurer la sélection par texte
            If Not String.IsNullOrEmpty(filter) Then
                For i = 0 To cboProduitAjouter.Items.Count - 1
                    Dim itm = cboProduitAjouter.Items(i).ToString()
                    If itm.StartsWith(filter, StringComparison.CurrentCultureIgnoreCase) Then
                        cboProduitAjouter.SelectedIndex = i
                        Exit For
                    End If
                Next
            End If
        Finally
            cboProduitAjouter.EndUpdate()
        End Try
    End Sub

    Private Sub cboProduitAjouter_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboProduitAjouter.SelectedIndexChanged
    End Sub

    Private Sub btnAjouterLigne_Click(sender As Object, e As EventArgs) _
        Handles btnAjouterLigne.Click

        If String.IsNullOrWhiteSpace(cboProduitAjouter.Text) OrElse
           cboProduitAjouter.Text = "Sélectionner un produit..." Then
            MsgBox("Veuillez sélectionner un produit.", MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If

        Dim nomRecherche = cboProduitAjouter.Text.Split("—"c)(0).Trim()
        Dim produit = _listeProduits.FirstOrDefault(Function(p) p.Nom.ToLower() = nomRecherche.ToLower())

        If produit Is Nothing Then
            MsgBox("Produit introuvable. Veuillez sélectionner dans la liste.",
                   MsgBoxStyle.Exclamation, "Produit invalide")
            Return
        End If

        Dim qte = CInt(nudQuantite.Value)

        If produit.StockActuel < qte Then
            MsgBox("Stock insuffisant. Stock disponible : " & produit.StockActuel & " unité(s).",
                   MsgBoxStyle.Exclamation, "Stock insuffisant")
            Return
        End If

        Dim existant As FactureDetail = Nothing
        For Each l In _lignes
            If l.IdProduit.HasValue AndAlso l.IdProduit.Value = produit.IdProduit Then
                existant = l
                Exit For
            End If
        Next

        If existant IsNot Nothing Then
            existant.Quantite += qte
        Else
            Dim ligne As New FactureDetail()
            ligne.IdProduit = produit.IdProduit
            ligne.Quantite = qte
            ligne.Prix = produit.Prix
            _lignes.Add(ligne)
        End If

        AfficherLignes()

        ' Remettre la quantité par défaut et conserver la liste de produits initiale
        nudQuantite.Value = 1
    End Sub






    Private Sub FiltrerProduits(sender As Object, e As EventArgs)
        If _filtreEnCours Then Return

        ' Si un item est sélectionné, ne pas filtrer
        If cboProduitAjouter.SelectedIndex >= 0 Then Return

        _filtreEnCours = True
        Try
            Dim texte = cboProduitAjouter.Text.Trim().ToLower()
            Dim pos = cboProduitAjouter.SelectionStart

            cboProduitAjouter.BeginUpdate()
            cboProduitAjouter.Items.Clear()

            For Each p In _listeProduits
                Dim itemText = p.Nom & " — " & FormatNumber(p.Prix, 0) & " F (stock: " & p.StockActuel & ")"
                If String.IsNullOrEmpty(texte) OrElse p.Nom.ToLower().Contains(texte) Then
                    cboProduitAjouter.Items.Add(itemText)
                End If
            Next

            cboProduitAjouter.EndUpdate()
            cboProduitAjouter.Text = cboProduitAjouter.Text
            cboProduitAjouter.SelectionStart = pos
            cboProduitAjouter.SelectionLength = 0

            If texte.Length > 0 AndAlso cboProduitAjouter.Items.Count > 0 Then
                cboProduitAjouter.DroppedDown = True
                Cursor.Current = Cursors.Default
            End If

        Finally
            _filtreEnCours = False
        End Try
    End Sub
    Private Sub cboProduitAjouter_DropDownClosed(sender As Object, e As EventArgs)
        ' Si rien de sélectionné, remettre la liste complète
        If cboProduitAjouter.SelectedIndex < 0 Then
            _filtreEnCours = True
            Try
                cboProduitAjouter.BeginUpdate()
                cboProduitAjouter.Items.Clear()
                cboProduitAjouter.Items.Add("Sélectionner un produit...")
                For Each p In _listeProduits
                    cboProduitAjouter.Items.Add(p.Nom & " — " & FormatNumber(p.Prix, 0) & " F (stock: " & p.StockActuel & ")")
                Next
                cboProduitAjouter.EndUpdate()
            Finally
                _filtreEnCours = False
            End Try
        End If
    End Sub
    ' ─────────────────────────────────────────────
    ' CHARGER LES MODES ET PAIEMENTS
    ' ─────────────────────────────────────────────
    Private Sub ChargerModes()
        cboModeFacture.Items.Clear()
        cboModeFacture.Items.Add("Depuis un rendez-vous")
        cboModeFacture.Items.Add("Vente directe (sans RDV)")
        cboModeFacture.SelectedIndex = 0
    End Sub

    Private Sub ChargerModePaiement()
        cboModePaiement.Items.Clear()
        cboModePaiement.Items.Add("Espèces")
        cboModePaiement.Items.Add("Mobile Money")
        cboModePaiement.Items.Add("Virement")
        cboModePaiement.SelectedIndex = 0
    End Sub

    Private Sub ChargerProduits()
        Try
            _listeProduits = Mainframe.ProduitCtrl.GetProduitsActifs()
            cboProduitAjouter.Items.Clear()
            cboProduitAjouter.Items.Add("Sélectionner un produit...")
            For Each p In _listeProduits
                cboProduitAjouter.Items.Add(p.Nom & " — " &
                    FormatNumber(p.Prix, 0) & " F (stock: " & p.StockActuel & ")")
            Next
            cboProduitAjouter.SelectedIndex = 0
            cboProduitAjouter.DropDownStyle = ComboBoxStyle.DropDown

        Catch ex As Exception
            MsgBox("Erreur chargement produits : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' CHANGEMENT MODE FACTURATION
    ' ─────────────────────────────────────────────
    Private Sub cboModeFacture_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboModeFacture.SelectedIndexChanged

        ViderFacture()

        If cboModeFacture.SelectedIndex = 0 Then
            ' Depuis un RDV
            lblRdvLabel.Text = "Rendez-vous terminé"
            ChargerRdvTermines()
        Else
            ' Vente directe
            lblRdvLabel.Text = "Cliente"
            ChargerClientes()
        End If
    End Sub

    Private Sub ChargerRdvTermines()
        Try
            _listeRdv = Mainframe.RendezVousCtrl.GetRdvNonFactures()
            Dim rdvTermines As New List(Of RendezVous)
            For Each r In _listeRdv
                If r.Statut = "Terminé" OrElse r.Statut = "Confirmé" Then
                    rdvTermines.Add(r)
                End If
            Next

            cboRdvOuCliente.Items.Clear()
            cboRdvOuCliente.Items.Add("Sélectionner un RDV...")
            For Each r In rdvTermines
                Dim nomC = "Cliente"
                Try
                    Dim c = Mainframe.ClientCtrl.GetClientById(r.IdClient)
                    If c IsNot Nothing Then nomC = c.Prenom & " " & c.Nom
                Catch
                End Try
                Dim nomP = ""
                Try
                    Dim p = Mainframe.PrestationCtrl.GetPrestationById(r.IdPrestation)
                    If p IsNot Nothing Then nomP = p.Nom
                Catch
                End Try
                cboRdvOuCliente.Items.Add(nomC & " — " & nomP & " — " &
                    r.DateHeureDebut.ToString("HH:mm"))
            Next
            cboRdvOuCliente.Tag = rdvTermines
            cboRdvOuCliente.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement RDV : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    Private Sub ChargerClientes()
        Try
            _listeClients = Mainframe.ClientCtrl.GetAllClients()
            cboRdvOuCliente.Items.Clear()
            cboRdvOuCliente.Items.Add("Sélectionner une cliente...")
            For Each c In _listeClients
                cboRdvOuCliente.Items.Add(c.Prenom & " " & c.Nom & " | " & c.Telephone)
            Next
            cboRdvOuCliente.Tag = _listeClients
            cboRdvOuCliente.SelectedIndex = 0
        Catch ex As Exception
            MsgBox("Erreur chargement clientes : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' SÉLECTION RDV OU CLIENTE
    ' ─────────────────────────────────────────────
    Private Sub cboRdvOuCliente_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboRdvOuCliente.SelectedIndexChanged

        If cboRdvOuCliente.SelectedIndex <= 0 Then
            ViderInfoCliente()
            Return
        End If

        If cboModeFacture.SelectedIndex = 0 Then
            ' Depuis un RDV
            Dim rdvList = TryCast(cboRdvOuCliente.Tag, List(Of RendezVous))
            If rdvList Is Nothing Then Return
            Dim rdv = rdvList(cboRdvOuCliente.SelectedIndex - 1)

            ' Charger infos cliente
            Try
                Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                If c IsNot Nothing Then
                    lblNomCliente.Text = c.Prenom & " " & c.Nom
                    lblTelCliente.Text = If(String.IsNullOrEmpty(c.Telephone), "—", c.Telephone)
                    _idClientCourant = c.IdClient
                End If
            Catch
            End Try

            ' Charger infos employée
            Try
                Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
                If emp IsNot Nothing Then
                    lblEmployeFacture.Text = "Employée : " & emp.Prenom & " " & emp.Nom
                    _idEmployeCourant = emp.IdEmploye
                End If
            Catch
            End Try

            ' Ajouter la prestation automatiquement
            Try
                Dim p = Mainframe.PrestationCtrl.GetPrestationById(rdv.IdPrestation)
                If p IsNot Nothing Then
                    _prixPrestation = p.Prix
                    ' Vider les lignes et ajouter la prestation
                    _lignes.Clear()
                    Dim ligne As New FactureDetail()
                    ligne.IdPrestation = p.IdPrestation
                    ligne.Quantite = 1
                    ligne.Prix = p.Prix
                    _lignes.Add(ligne)
                    AfficherLignes()
                End If
            Catch
            End Try

        Else
            ' Vente directe — juste charger la cliente
            Dim clients = TryCast(cboRdvOuCliente.Tag, List(Of Client))
            If clients Is Nothing Then Return
            Dim c = clients(cboRdvOuCliente.SelectedIndex - 1)
            lblNomCliente.Text = c.Prenom & " " & c.Nom
            lblTelCliente.Text = If(String.IsNullOrEmpty(c.Telephone), "—", c.Telephone)
            lblEmployeFacture.Text = "—"
            _idClientCourant = c.IdClient
            _prixPrestation = 0
            If _lignes.Count > 0 Then
                Dim rep = MsgBox("Changer de rendez-vous va effacer les lignes déjà ajoutées." &
                     vbCrLf & "Voulez-vous continuer ?",
                     MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmation")
                If rep = MsgBoxResult.No Then
                    cboRdvOuCliente.SelectedIndex = 0
                    Return
                End If
            End If
            _lignes.Clear()
            AfficherLignes()
        End If
    End Sub

    Private Sub ViderInfoCliente()
        lblNomCliente.Text = "—"
        lblTelCliente.Text = "—"
        lblEmployeFacture.Text = "—"
        _idClientCourant = 0
        _idEmployeCourant = 0
    End Sub

    ' ─────────────────────────────────────────────
    ' AFFICHER LES LIGNES
    ' ─────────────────────────────────────────────
    Private Sub AfficherLignes()
        dgvLignes.Rows.Clear()
        Dim totalPresta As Decimal = 0
        Dim totalProduits As Decimal = 0

        For Each ligne In _lignes
            Dim designation = "—"
            Dim typeLigne = "—"
            Dim total = ligne.Prix * ligne.Quantite

            If ligne.IdPrestation.HasValue Then
                Try
                    Dim p = Mainframe.PrestationCtrl.GetPrestationById(ligne.IdPrestation.Value)
                    If p IsNot Nothing Then designation = p.Nom
                Catch
                End Try
                typeLigne = "Prestation"
                totalPresta += total
            ElseIf ligne.IdProduit.HasValue Then
                Try
                    Dim p = Mainframe.ProduitCtrl.GetProduitParId(ligne.IdProduit.Value)
                    If p IsNot Nothing Then designation = p.Nom
                Catch
                End Try
                typeLigne = "Produit"
                totalProduits += total
            End If

            Dim index = dgvLignes.Rows.Add(
                designation,
                typeLigne,
                ligne.Quantite,
                FormatNumber(ligne.Prix, 0) & " F",
                FormatNumber(total, 0) & " F",
                Nothing)
            dgvLignes.Rows(index).Tag = ligne
        Next

        ' Mettre à jour le résumé
        lblSousTotalPresta.Text = "Prestation : " & FormatNumber(totalPresta, 0) & " F"
        lblSousTotalProduits.Text = "Produits : " & FormatNumber(totalProduits, 0) & " F"
        Dim totalGeneral = totalPresta + totalProduits
        lblToatlFacture.Text = "Total : " & FormatNumber(totalGeneral, 0) & " F"
        btnValiderResume.Text = "Valider — " & FormatNumber(totalGeneral, 0) & " F"
    End Sub



    ' ─────────────────────────────────────────────
    ' SUPPRIMER UNE LIGNE
    ' ─────────────────────────────────────────────
    Private Sub dgvLignes_CellClick(sender As Object, e As DataGridViewCellEventArgs) _
        Handles dgvLignes.CellClick
        If e.ColumnIndex = dgvLignes.Columns("colSupprimerLigne").Index Then
            Dim ligne = TryCast(dgvLignes.Rows(e.RowIndex).Tag, FactureDetail)
            If ligne IsNot Nothing Then
                Dim nomLigne = dgvLignes.Rows(e.RowIndex).Cells(0).Value?.ToString()
                Dim rep = MsgBox("Retirer « " & nomLigne & " » de la facture ?",
                         MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmer")
                If rep = MsgBoxResult.No Then Return
                _lignes.Remove(ligne)
                AfficherLignes()
            End If
        End If
        If e.RowIndex < 0 Then Return
        If e.ColumnIndex = dgvLignes.Columns("colSupprimerLigne").Index Then
            Dim ligne = TryCast(dgvLignes.Rows(e.RowIndex).Tag, FactureDetail)
            If ligne IsNot Nothing Then
                _lignes.Remove(ligne)
                AfficherLignes()
            End If
        End If
    End Sub

    ' ─────────────────────────────────────────────
    ' VIDER LA FACTURE
    ' ─────────────────────────────────────────────
    Private Sub ViderFacture()
        _lignes.Clear()
        _idClientCourant = 0
        _idEmployeCourant = 0
        _prixPrestation = 0
        dgvLignes.Rows.Clear()
        ViderInfoCliente()
        lblSousTotalPresta.Text = "Prestation : 0 F"
        lblSousTotalProduits.Text = "Produits : 0 F"
        lblToatlFacture.Text = "Total : 0 F"
        btnValiderResume.Text = "Valider — 0 F"
        cboRdvOuCliente.Items.Clear()
    End Sub

    Private Sub btnNouvelleFacture_Click(sender As Object, e As EventArgs) _
        Handles btnNouvelleFacture.Click
        ViderFacture()
        ChargerModes()
        cboModeFacture.SelectedIndex = 0
    End Sub

    ' ─────────────────────────────────────────────
    ' VALIDER LA FACTURE
    ' ─────────────────────────────────────────────
    Private Sub btnValiderResume_Click(sender As Object, e As EventArgs) _
        Handles btnValiderResume.Click
        ValiderFacture()
    End Sub

    Private Sub ValiderFacture()
        If _idClientCourant = 0 Then
            MsgBox("Veuillez sélectionner une cliente ou un rendez-vous.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If

        If _lignes.Count = 0 Then
            MsgBox("La facture est vide. Ajoutez au moins une prestation ou un produit.",
                   MsgBoxStyle.Exclamation, "Facture vide")
            Return
        End If
        ' Vérifier qu'une facture depuis RDV a bien sa prestation
        If cboModeFacture.SelectedIndex = 0 Then
            Dim aPrestation = _lignes.Any(Function(l) l.IdPrestation.HasValue)
            If Not aPrestation Then
                Dim repi = MsgBox("Aucune prestation dans cette facture." & vbCrLf &
                         "Une facture depuis RDV devrait inclure la prestation." & vbCrLf &
                         "Continuer quand même ?",
                         MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Prestation manquante")
                If repi = MsgBoxResult.No Then Return
            End If
        End If

        If cboModePaiement.SelectedIndex < 0 Then
            MsgBox("Veuillez sélectionner un mode de paiement.",
                   MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If

        Dim total As Decimal = 0
        For Each l In _lignes
            total += l.Prix * l.Quantite
        Next

        Dim rep = MsgBox("Valider la facture de " & FormatNumber(total, 0) & " F CFA ?" &
                         vbCrLf & "Mode : " & cboModePaiement.SelectedItem.ToString(),
                         MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Confirmation")
        If rep = MsgBoxResult.No Then Return

        Try
            ' Trouver l'idRdv si applicable
            Dim idRdv As Integer = 0
            If cboModeFacture.SelectedIndex = 0 AndAlso cboRdvOuCliente.SelectedIndex > 0 Then
                Dim rdvList = TryCast(cboRdvOuCliente.Tag, List(Of RendezVous))
                If rdvList IsNot Nothing Then
                    idRdv = rdvList(cboRdvOuCliente.SelectedIndex - 1).IdRdv
                End If
            End If

            ' Créer la facture
            Dim modePaiement = cboModePaiement.SelectedItem?.ToString()
            Mainframe.FactureCtrl.CreerFacture(idRdv, _lignes, modePaiement)
            ' Diminuer le stock des produits
            For Each ligne In _lignes
                If ligne.IdProduit.HasValue Then
                    Try
                        Mainframe.ProduitCtrl.DiminuerStock(ligne.IdProduit.Value, ligne.Quantite)
                    Catch
                    End Try
                End If
            Next

            MsgBox("Facture de " & FormatNumber(total, 0) & " F CFA enregistrée avec succès !",
                   MsgBoxStyle.Information, "Facture validée")

            ViderFacture()
            ChargerModes()
            cboModeFacture.SelectedIndex = 0

        Catch ex As Exception
            MsgBox("Erreur lors de la validation : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub
    Private Sub btnImprimerFacture_Click(sender As Object, e As EventArgs) _
    Handles btnImprimerFacture.Click
        If _lignes.Count = 0 Then
            MsgBox("La facture est vide. Veuillez d'abord valider la facture.",
           MsgBoxStyle.Exclamation, "Impression impossible")
            Return
        End If
        Dim repImp = MsgBox("Avez-vous bien validé la facture avant d'imprimer ?",
                    MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Vérification")
        If repImp = MsgBoxResult.No Then Return
        If _idClientCourant = 0 Then
            MsgBox("Aucune cliente sélectionnée.", MsgBoxStyle.Exclamation, "Impression")
            Return
        End If
        If _lignes.Count = 0 Then
            MsgBox("La facture est vide.", MsgBoxStyle.Exclamation, "Impression")
            Return
        End If

        Dim numeroFacture = "F-" & Date.Now.ToString("yyyyMMdd") & "-" &
                    Date.Now.ToString("HHmmss")
        ImprimerFacture.GenererEtImprimer(
            lblNomCliente.Text,
            lblTelCliente.Text,
            lblEmployeFacture.Text,
            cboModePaiement.SelectedItem?.ToString(),
            _lignes,
            numeroFacture)
    End Sub

    Private Sub cboModePaiement_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboModePaiement.SelectedIndexChanged

    End Sub
End Class