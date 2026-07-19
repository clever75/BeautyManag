' =====================================================
' USERCONTROL DASHBOARD
' =====================================================
Imports System.Drawing.Drawing2D
Imports Guna.UI2.WinForms
Imports MySql.Data

Public Class ucDashboard

    ' ─────────────────────────────────────────────
    ' CHARGEMENT
    ' ─────────────────────────────────────────────
    Private Sub ucDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblSousTitre.Text = Date.Today.ToString("dddd dd MMMM yyyy",
                            New System.Globalization.CultureInfo("fr-FR"))
        AddHandler pnlBars.Paint, AddressOf DessinerGraphique

        ' Configurer les FlowLayoutPanel
        ConfigurerFlowLayouts()

        ' Charger toutes les données
        ChargerKpi()
        ChargerRdvDuJour()
        ChargerAlertes()
        ChargerEmployees()
        ChargerTopPrestations()

        ' Dessiner le graphique
        pnlBars.Invalidate()
    End Sub

    ' ─────────────────────────────────────────────
    ' CONFIGURER LES FLOWLAYOUTPANEL
    ' ─────────────────────────────────────────────
    Private Sub ConfigurerFlowLayouts()
        ' RDV
        flpRdv.WrapContents = False
        flpRdv.FlowDirection = FlowDirection.TopDown
        flpRdv.AutoScroll = True
        flpRdv.Width = pnlRdvJour.Width - 10
        flpRdv.Height = pnlRdvJour.Height - 42

        ' Alertes
        flpAlertes.WrapContents = False
        flpAlertes.FlowDirection = FlowDirection.TopDown
        flpAlertes.AutoScroll = True
        flpAlertes.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                    AnchorStyles.Left Or AnchorStyles.Right
        flpAlertes.Location = New Point(10, 44)
        flpAlertes.Width = pnlAlertes.Width - 20
        flpAlertes.Height = pnlAlertes.Height - 54


        ' Employées
        flpEmployees.WrapContents = False
        flpEmployees.FlowDirection = FlowDirection.TopDown
        flpEmployees.AutoScroll = True
        flpEmployees.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                      AnchorStyles.Left Or AnchorStyles.Right
        flpEmployees.Location = New Point(10, 44)
        flpEmployees.Width = pnlEmployees.Width - 20
        flpEmployees.Height = pnlEmployees.Height - 54
        ' Prestations
        flpPrestations.WrapContents = False
        flpPrestations.FlowDirection = FlowDirection.TopDown
        flpPrestations.AutoScroll = True
        flpPrestations.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                        AnchorStyles.Left Or AnchorStyles.Right
        flpPrestations.Location = New Point(10, 44)
        flpPrestations.Width = pnlPrestations.Width - 20
        flpPrestations.Height = pnlPrestations.Height - 54
        ' Graphique
        pnlBars.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or
                     AnchorStyles.Left Or AnchorStyles.Right
        pnlBars.Width = pnlGraphique.Width - 28
        pnlBars.Height = pnlGraphique.Height - 46
        pnlBars.Location = New Point(14, 40)
        pnlBars.BackColor = Color.White
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LES KPI
    ' ─────────────────────────────────────────────
    Private Sub ChargerKpi()
        Try
            ' KPI 1 — Nombre de clients
            Dim nbClients = Mainframe.ClientCtrl.GetAllClients().Count
            lblKpi1.Text = nbClients.ToString()

            ' KPI 2 — RDV aujourd'hui
            Dim nbRdv = Mainframe.RendezVousCtrl.GetRdvDuJour().Count
            lblKpi2.Text = nbRdv.ToString()

            ' KPI 3 — Chiffre du mois
            Dim chiffre = Mainframe.FactureCtrl.GetChiffreDuMois()
            lblKpi3.Text = FormatNumber(chiffre, 0) & " F CFA"
            lblKpi3.Font = New Font("Segoe UI", 14, FontStyle.Bold)

            ' KPI 4 — Alertes stock
            Dim nbAlertes = Mainframe.ProduitCtrl.CompterProduitsEnAlerte() +
                            Mainframe.ProduitCtrl.CompterProduitsEnRupture()
            lblKpi4.Text = nbAlertes.ToString()

            ' Colorier KPI4 en rouge si alertes
            If nbAlertes > 0 Then
                lblKpi4.ForeColor = ColorTranslator.FromHtml("#A32D2D")
            End If

        Catch ex As Exception
            MsgBox("Erreur chargement KPI : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' CHARGER LES RDV DU JOUR
    ' ─────────────────────────────────────────────
    Private Sub ChargerRdvDuJour()
        Try
            flpRdv.Controls.Clear()
            Dim liste = Mainframe.RendezVousCtrl.GetRdvDuJour()

            If liste.Count = 0 Then
                Dim lblVide As New Label()
                lblVide.Text = "Aucun rendez-vous aujourd'hui"
                lblVide.ForeColor = ColorTranslator.FromHtml("#A07080")
                lblVide.Font = New Font("Segoe UI", 9)
                lblVide.AutoSize = True
                lblVide.Margin = New Padding(10, 8, 0, 0)
                flpRdv.Controls.Add(lblVide)
                Return
            End If

            For Each rdv In liste
                Dim item As New Panel()
                item.Size = New Size(flpRdv.Width - 10, 44)
                item.BackColor = Color.White

                ' Récupérer le client par ID
                Dim nomClient = "Client"
                Try
                    Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                    If c IsNot Nothing Then nomClient = c.Prenom & " " & c.Nom
                Catch
                End Try

                Dim lblNom As New Label()
                lblNom.Text = nomClient
                lblNom.Location = New Point(8, 4)
                lblNom.Size = New Size(200, 18)
                lblNom.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblNom.ForeColor = ColorTranslator.FromHtml("#3D1A24")
                lblNom.BackColor = Color.Transparent

                ' Heure depuis ta propriété réelle
                Dim lblHeure As New Label()
                ' lblHeure.Text = rdv.DateRdv.ToString("HH:mm") ' ← adapte selon ton vrai nom
                lblHeure.Location = New Point(item.Width - 80, 4)
                lblHeure.AutoSize = True
                lblHeure.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblHeure.ForeColor = ColorTranslator.FromHtml("#C45A7E")
                lblHeure.BackColor = Color.Transparent

                Dim sep As New Panel()
                sep.Size = New Size(item.Width, 1)
                sep.Location = New Point(0, 43)
                sep.BackColor = ColorTranslator.FromHtml("#FDE8EF")

                item.Controls.AddRange({lblNom, lblHeure, sep})
                flpRdv.Controls.Add(item)
            Next

        Catch ex As Exception
            MsgBox("Erreur RDV : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub


    Private Function CreerItemRdv(rdv As RendezVous) As Panel
        Dim item As New Panel()
        item.Size = New Size(flpRdv.Width - 10, 44)
        item.BackColor = Color.White

        ' Récupérer le nom du client
        Dim nomClient = "Client"
        Try
            Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
            If c IsNot Nothing Then nomClient = c.Prenom & " " & c.Nom
        Catch
        End Try

        ' Récupérer la prestation
        Dim nomPrestation = ""
        Try
            Dim p = Mainframe.PrestationCtrl.GetPrestationById(rdv.IdPrestation)
            If p IsNot Nothing Then nomPrestation = p.Nom
        Catch
        End Try

        ' Récupérer l'employée
        Dim nomEmploye = ""
        Try
            Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
            If emp IsNot Nothing Then nomEmploye = emp.Prenom
        Catch
        End Try

        ' Avatar initiales
        Dim avatar As New Panel()
        avatar.Size = New Size(32, 32)
        avatar.Location = New Point(6, 6)
        avatar.BackColor = ColorTranslator.FromHtml("#FDE8EF")

        Dim initiales = If(nomClient.Length >= 2, nomClient.Substring(0, 1) & nomClient.Split(" "c).Last()(0), "?")
        Dim lblInit As New Label()
        lblInit.Text = initiales.ToUpper()
        lblInit.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblInit.ForeColor = ColorTranslator.FromHtml("#C45A7E")
        lblInit.TextAlign = ContentAlignment.MiddleCenter
        lblInit.Dock = DockStyle.Fill
        lblInit.BackColor = Color.Transparent
        avatar.Controls.Add(lblInit)

        ' Nom
        Dim lblNom As New Label()
        lblNom.Text = nomClient
        lblNom.Location = New Point(46, 4)
        lblNom.Size = New Size(150, 18)
        lblNom.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblNom.ForeColor = ColorTranslator.FromHtml("#3D1A24")
        lblNom.BackColor = Color.Transparent

        ' Info prestation + employée
        Dim lblInfo As New Label()
        lblInfo.Text = nomPrestation & If(nomEmploye <> "", " · " & nomEmploye, "")
        lblInfo.Location = New Point(46, 22)
        lblInfo.Size = New Size(180, 16)
        lblInfo.Font = New Font("Segoe UI", 8)
        lblInfo.ForeColor = ColorTranslator.FromHtml("#A07080")
        lblInfo.BackColor = Color.Transparent

        ' Heure — utilise DateHeureDebut
        Dim lblHeure As New Label()
        lblHeure.Text = rdv.DateHeureDebut.ToString("HH:mm")
        lblHeure.Location = New Point(item.Width - 90, 4)
        lblHeure.AutoSize = True
        lblHeure.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        lblHeure.ForeColor = ColorTranslator.FromHtml("#C45A7E")
        lblHeure.BackColor = Color.Transparent

        ' Badge statut
        Dim lblBadge As New Label()
        lblBadge.Location = New Point(item.Width - 90, 22)
        lblBadge.AutoSize = True
        lblBadge.Font = New Font("Segoe UI", 8, FontStyle.Bold)
        lblBadge.BackColor = Color.Transparent
        Select Case rdv.Statut
            Case "Confirmé"
                lblBadge.Text = "Confirmé"
                lblBadge.ForeColor = ColorTranslator.FromHtml("#0F6E56")
            Case "Terminé"
                lblBadge.Text = "Terminé"
                lblBadge.ForeColor = ColorTranslator.FromHtml("#C45A7E")
            Case "Annulé"
                lblBadge.Text = "Annulé"
                lblBadge.ForeColor = ColorTranslator.FromHtml("#A32D2D")
            Case Else
                lblBadge.Text = "En attente"
                lblBadge.ForeColor = ColorTranslator.FromHtml("#854F0B")
        End Select

        Dim sep As New Panel()
        sep.Size = New Size(item.Width, 1)
        sep.Location = New Point(0, 43)
        sep.BackColor = ColorTranslator.FromHtml("#FDE8EF")

        item.Controls.AddRange({avatar, lblNom, lblInfo, lblHeure, lblBadge, sep})
        Return item
    End Function
    ' ─────────────────────────────────────────────
    ' CHARGER LES ALERTES STOCK
    ' ─────────────────────────────────────────────
    Private Sub ChargerAlertes()
        Try
            flpAlertes.Controls.Clear()

            Dim produits = Mainframe.ProduitCtrl.GetTousProduits("", "")
            Dim alertes = produits.Where(Function(p) p.EnRupture OrElse p.EnAlerte).ToList()

            If alertes.Count = 0 Then
                Dim lblOk As New Label()
                lblOk.Text = "✓ Aucune alerte stock"
                lblOk.ForeColor = ColorTranslator.FromHtml("#0F6E56")
                lblOk.Font = New Font("Segoe UI", 9)
                lblOk.AutoSize = True
                lblOk.Margin = New Padding(4, 8, 0, 0)
                flpAlertes.Controls.Add(lblOk)
                Return
            End If

            For Each p In alertes
                Dim item As New Panel()
                item.Size = New Size(flpAlertes.Width - 6, 48)
                item.Margin = New Padding(0, 0, 0, 4)
                item.BackColor = If(p.EnRupture,
                ColorTranslator.FromHtml("#FCEBEB"),
                ColorTranslator.FromHtml("#FEF5E7"))

                Dim lblNom As New Label()
                lblNom.Text = p.Nom
                lblNom.Location = New Point(8, 6)
                lblNom.Size = New Size(item.Width - 16, 18)
                lblNom.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblNom.ForeColor = ColorTranslator.FromHtml("#3D1A24")
                lblNom.BackColor = Color.Transparent

                Dim lblDetail As New Label()
                If p.EnRupture Then
                    lblDetail.Text = "Rupture — 0 unité"
                    lblDetail.ForeColor = ColorTranslator.FromHtml("#A32D2D")
                Else
                    lblDetail.Text = $"Stock faible — {p.StockActuel} unité(s) (seuil : {p.SeuilAlerte})"
                    lblDetail.ForeColor = ColorTranslator.FromHtml("#854F0B")
                End If
                lblDetail.Location = New Point(8, 26)
                lblDetail.Size = New Size(item.Width - 16, 16)
                lblDetail.Font = New Font("Segoe UI", 8)
                lblDetail.BackColor = Color.Transparent

                item.Controls.AddRange({lblNom, lblDetail})
                flpAlertes.Controls.Add(item)
            Next

            ' ── Résumé en bas ──
            Dim nbRupture = alertes.Where(Function(p) p.StockActuel = 0).Count()
            Dim nbFaible = alertes.Where(Function(p) p.StockActuel > 0 AndAlso p.StockActuel <= p.SeuilAlerte).Count()

            Dim pnlResume As New Panel()
            pnlResume.Size = New Size(flpAlertes.Width - 6, 50)
            pnlResume.Margin = New Padding(0, 8, 0, 0)
            pnlResume.BackColor = Color.Transparent

            Dim sep As New Panel()
            sep.Size = New Size(pnlResume.Width, 1)
            sep.Location = New Point(0, 0)
            sep.BackColor = ColorTranslator.FromHtml("#FDE8EF")

            Dim lblResume As New Label()
            lblResume.Text = $"{alertes.Count} produit(s) concerné(s) · {nbRupture} rupture(s) · {nbFaible} stock faible"
            lblResume.Location = New Point(0, 8)
            lblResume.Size = New Size(pnlResume.Width, 16)
            lblResume.Font = New Font("Segoe UI", 8)
            lblResume.ForeColor = ColorTranslator.FromHtml("#A07080")
            lblResume.BackColor = Color.Transparent

            Dim btnVoir As New Guna2Button()
            btnVoir.Text = "→ Voir le stock produits"
            btnVoir.Location = New Point(0, 26)
            btnVoir.Size = New Size(200, 22)
            btnVoir.Font = New Font("Segoe UI", 8, FontStyle.Bold)
            btnVoir.FillColor = Color.Transparent
            btnVoir.ForeColor = ColorTranslator.FromHtml("#C45A7E")
            btnVoir.BorderThickness = 0
            btnVoir.TextAlign = HorizontalAlignment.Left
            AddHandler btnVoir.Click, Sub()
                                          Dim mf = TryCast(Me.FindForm(), Mainframe)
                                          If mf IsNot Nothing Then
                                              mf.ActiverBouton(mf.btnProduits)
                                              mf.AfficherPage(New ucProduits())
                                          End If
                                      End Sub
            pnlResume.Controls.AddRange({sep, lblResume, btnVoir})
            flpAlertes.Controls.Add(pnlResume)

        Catch ex As Exception
            MsgBox("Erreur alertes : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub
    ' ─────────────────────────────────────────────
    ' CHARGER LES EMPLOYÉES
    ' ─────────────────────────────────────────────


    Private Sub ChargerEmployees()
        Try
            flpEmployees.Controls.Clear()

            Dim employes = Mainframe.EmployeCtrl.GetEmployesActifs()
            Dim tousRdv = Mainframe.RendezVousCtrl.GetRdvDuJour()

            ' ── Résumé total en haut ──
            Dim pnlTotal As New Panel()
            pnlTotal.Size = New Size(flpEmployees.Width - 6, 36)
            pnlTotal.BackColor = ColorTranslator.FromHtml("#FDE8EF")
            pnlTotal.Margin = New Padding(0, 0, 0, 6)

            Dim lblTotal As New Label()
            lblTotal.Text = $"Total aujourd'hui : {tousRdv.Count} RDV  ·  {employes.Count} employée(s) active(s)"
            lblTotal.Location = New Point(8, 10)
            lblTotal.Size = New Size(pnlTotal.Width - 16, 16)
            lblTotal.Font = New Font("Segoe UI", 8, FontStyle.Bold)
            lblTotal.ForeColor = ColorTranslator.FromHtml("#C45A7E")
            lblTotal.BackColor = Color.Transparent
            pnlTotal.Controls.Add(lblTotal)
            flpEmployees.Controls.Add(pnlTotal)

            ' ── Liste des employées ──
            For Each emp In employes
                Dim item As New Panel()
                item.Size = New Size(flpEmployees.Width - 6, 40)
                item.BackColor = Color.White
                item.Margin = New Padding(0, 0, 0, 2)

                Dim lblNom As New Label()
                lblNom.Text = emp.Prenom & " " & emp.Nom
                lblNom.Location = New Point(8, 4)
                lblNom.Size = New Size(150, 18)
                lblNom.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblNom.ForeColor = ColorTranslator.FromHtml("#3D1A24")
                lblNom.BackColor = Color.Transparent

                Dim lblSpec As New Label()
                lblSpec.Text = If(String.IsNullOrEmpty(emp.Specialite), "—", emp.Specialite)
                lblSpec.Location = New Point(8, 22)
                lblSpec.Size = New Size(150, 14)
                lblSpec.Font = New Font("Segoe UI", 8)
                lblSpec.ForeColor = ColorTranslator.FromHtml("#A07080")
                lblSpec.BackColor = Color.Transparent

                Dim nbRdvEmp = tousRdv.Where(Function(r) r.IdEmploye = emp.IdEmploye).Count()

                Dim lblRdv As New Label()
                lblRdv.Text = nbRdvEmp & " RDV"
                lblRdv.Location = New Point(item.Width - 50, 12)
                lblRdv.AutoSize = True
                lblRdv.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblRdv.ForeColor = If(nbRdvEmp > 0,
                ColorTranslator.FromHtml("#C45A7E"),
                ColorTranslator.FromHtml("#CCAABB"))
                lblRdv.BackColor = Color.Transparent

                Dim sep As New Panel()
                sep.Size = New Size(item.Width, 1)
                sep.Location = New Point(0, 39)
                sep.BackColor = ColorTranslator.FromHtml("#FDE8EF")

                item.Controls.AddRange({lblNom, lblSpec, lblRdv, sep})
                flpEmployees.Controls.Add(item)
            Next

        Catch ex As Exception
            MsgBox("Erreur employées : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub
    ' ─────────────────────────────────────────────
    ' CHARGER TOP PRESTATIONS
    ' ─────────────────────────────────────────────
    Private Sub ChargerTopPrestations()
        Try
            flpPrestations.Controls.Clear()

            Dim top = Mainframe.FactureCtrl.GetTopPrestations(5)

            If top.Count = 0 Then
                Dim lblVide As New Label()
                lblVide.Text = "Aucune donnée ce mois"
                lblVide.ForeColor = ColorTranslator.FromHtml("#A07080")
                lblVide.Font = New Font("Segoe UI", 9)
                lblVide.AutoSize = True
                lblVide.Margin = New Padding(4, 8, 0, 0)
                flpPrestations.Controls.Add(lblVide)
                Return
            End If

            For Each item In top
                Dim pnl As New Panel()
                pnl.Size = New Size(flpPrestations.Width - 6, 38)
                pnl.BackColor = Color.White
                pnl.Margin = New Padding(0, 0, 0, 2)

                Dim lblNom As New Label()
                lblNom.Text = item.Key
                lblNom.Location = New Point(6, 4)
                lblNom.Size = New Size(160, 18)
                lblNom.Font = New Font("Segoe UI", 9, FontStyle.Bold)
                lblNom.ForeColor = ColorTranslator.FromHtml("#3D1A24")
                lblNom.BackColor = Color.Transparent

                Dim lblCount As New Label()
                lblCount.Text = item.Value & " ce mois"
                lblCount.Location = New Point(6, 22)
                lblCount.AutoSize = True
                lblCount.Font = New Font("Segoe UI", 8)
                lblCount.ForeColor = ColorTranslator.FromHtml("#C45A7E")
                lblCount.BackColor = Color.Transparent

                Dim sep As New Panel()
                sep.Size = New Size(pnl.Width, 1)
                sep.Location = New Point(0, 37)
                sep.BackColor = ColorTranslator.FromHtml("#FDE8EF")

                pnl.Controls.AddRange({lblNom, lblCount, sep})
                flpPrestations.Controls.Add(pnl)
            Next

        Catch ex As Exception
            MsgBox("Erreur top prestations : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' DESSINER LE GRAPHIQUE DES REVENUS
    ' ─────────────────────────────────────────────
    Private Sub DessinerGraphique(sender As Object, e As PaintEventArgs)
        Try
            Dim g = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias

            ' Données des 6 derniers mois
            Dim donnees As New List(Of KeyValuePair(Of String, Decimal))
            Try
                donnees = Mainframe.FactureCtrl.GetRevenusParMois(6)
            Catch
                ' Données fictives si pas de factures
                donnees.Add(New KeyValuePair(Of String, Decimal)("Déc", 180000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Jan", 195000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Fév", 210000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Mar", 190000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Avr", 220000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Mai", 245000))
            End Try

            If donnees.Count = 0 Then
                donnees.Add(New KeyValuePair(Of String, Decimal)("Déc", 180000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Jan", 195000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Fév", 210000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Mar", 190000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Avr", 220000))
                donnees.Add(New KeyValuePair(Of String, Decimal)("Mai", 245000))
            End If
            Dim w = pnlBars.Width
            Dim h = pnlBars.Height
            Dim paddingLeft = 50
            Dim paddingBottom = 30
            Dim paddingTop = 22
            Dim paddingRight = 10

            Dim maxVal = donnees.Max(Function(d) d.Value)
            If maxVal = 0 Then maxVal = 1

            Dim nbBars = donnees.Count
            Dim zoneW = w - paddingLeft - paddingRight
            Dim zoneH = h - paddingBottom - paddingTop
            Dim barWidth = CInt(zoneW / nbBars * 0.6)
            Dim barSpacing = CInt(zoneW / nbBars)

            ' Couleurs
            Dim couleurNormale As New SolidBrush(ColorTranslator.FromHtml("#F0DCE2"))
            Dim couleurActuel As New SolidBrush(ColorTranslator.FromHtml("#C45A7E"))
            Dim styloGrille As New Pen(ColorTranslator.FromHtml("#F0DCE2"), 1)
            styloGrille.DashStyle = DashStyle.Dash

            ' Lignes de grille horizontales
            Dim fontPetit As New Font("Segoe UI", 7)
            Dim brushTexte As New SolidBrush(ColorTranslator.FromHtml("#A07080"))

            For i = 0 To 3
                Dim yGrille = paddingTop + CInt(zoneH * i / 3)
                g.DrawLine(styloGrille, paddingLeft, yGrille, w - paddingRight, yGrille)
                Dim valGrille = maxVal * (3 - i) / 3
                Dim labelGrille = If(valGrille >= 1000,
    Math.Round(valGrille / 1000).ToString() & "k F",
    Math.Round(valGrille).ToString() & " F")
                g.DrawString(labelGrille, fontPetit, brushTexte, 2, yGrille - 8)
            Next

            ' Dessiner les barres
            For i = 0 To donnees.Count - 1
                Dim d = donnees(i)
                Dim barH = CInt(zoneH * d.Value / maxVal)
                Dim x = paddingLeft + i * barSpacing + CInt((barSpacing - barWidth) / 2)
                Dim y = paddingTop + zoneH - barH

                ' Couleur spéciale pour le mois actuel (dernier)
                Dim brush = If(i = donnees.Count - 1, couleurActuel, couleurNormale)

                ' Dessiner la barre avec coins arrondis en haut
                Dim rect As New Rectangle(x, y, barWidth, barH)
                g.FillRectangle(brush, rect)

                ' Valeur au dessus de la barre
                Dim montant = CInt(Math.Round(d.Value))
                Dim valLabel = String.Format(New System.Globalization.CultureInfo("fr-FR"), "{0:N0}", montant) & " F CFA"

                Dim fontVal As New Font("Segoe UI", 6.5, FontStyle.Bold)
                Dim brushVal As New SolidBrush(ColorTranslator.FromHtml("#3D1A24"))
                Dim taille = g.MeasureString(valLabel, fontVal)
                g.DrawString(valLabel, fontVal, brushVal,
             x + (barWidth - taille.Width) / 2, y - 16)

                ' Label mois en bas
                Dim moisFr = New System.Globalization.CultureInfo("fr-FR")
                Dim nomMois = d.Key
                ' Si c'est un nom de mois anglais, convertir
                Try
                    Dim dateTest = DateTime.ParseExact(d.Key, "MMM",
                   System.Globalization.CultureInfo.InvariantCulture)
                    nomMois = dateTest.ToString("MMM", moisFr)
                    nomMois = Char.ToUpper(nomMois(0)) & nomMois.Substring(1)
                Catch
                    nomMois = d.Key ' garder tel quel si déjà bon
                End Try
                Dim tailleMoisFr = g.MeasureString(nomMois, fontPetit)
                g.DrawString(nomMois, fontPetit, brushTexte,
             x + (barWidth - tailleMoisFr.Width) / 2,
             h - paddingBottom + 4)
            Next

            ' Légende
            Dim fontLegende As New Font("Segoe UI", 7)
            g.FillRectangle(couleurActuel, w - 80, h - 14, 10, 8)
            g.DrawString("Mois actuel", fontLegende, brushTexte, w - 80, h - 16)

        Catch ex As Exception
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' REDIMENSIONNEMENT
    ' ─────────────────────────────────────────────
    Private Sub ucDashboard_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If pnlBars IsNot Nothing Then
            pnlBars.Width = pnlGraphique.Width - 28
            pnlBars.Height = pnlGraphique.Height - 46
            pnlBars.Invalidate()
        End If
        If flpRdv IsNot Nothing Then
            flpRdv.Width = pnlRdvJour.Width - 10
            flpRdv.Height = pnlRdvJour.Height - 42
        End If
        If flpAlertes IsNot Nothing Then
            flpAlertes.Width = pnlAlertes.Width - 20
            flpAlertes.Height = pnlAlertes.Height - 54
        End If
        If flpEmployees IsNot Nothing Then
            flpEmployees.Width = pnlEmployees.Width - 20
            flpEmployees.Height = pnlEmployees.Height - 54
        End If
        If flpPrestations IsNot Nothing Then
            flpPrestations.Width = pnlPrestations.Width - 20
            flpPrestations.Height = pnlPrestations.Height - 54
        End If
    End Sub

End Class