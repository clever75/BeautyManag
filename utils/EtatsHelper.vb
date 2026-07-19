' =====================================================
' CLASSE UTILITAIRE — ÉTATS IMPRIMABLES
' BeautyManag — HTML ouvert dans le navigateur
' =====================================================

Imports System.Linq
Imports System.Text
Imports System.Globalization

Public Class EtatsHelper

    ' ══════════════════════════════════════════════
    ' MÉTHODE COMMUNE — Ouvre l'état dans le navigateur
    ' ══════════════════════════════════════════════
    Private Shared Sub OuvrirEtat(html As String, nomFichier As String)
        Try
            Dim f = System.IO.Path.Combine(System.IO.Path.GetTempPath(),
                                       nomFichier & ".html")
            System.IO.File.WriteAllText(f, html, System.Text.Encoding.UTF8)

            ' ✅ Ouverture correcte sur .NET 6+ / .NET 10
            Dim psi As New System.Diagnostics.ProcessStartInfo()
            psi.FileName = f
            psi.UseShellExecute = True   ' ← clé du problème
            System.Diagnostics.Process.Start(psi)

        Catch ex As Exception
            MsgBox("Erreur génération état : " & ex.Message,
               MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' ══════════════════════════════════════════════
    ' CSS COMMUN
    ' ══════════════════════════════════════════════
    Private Shared Function CssCommun() As String
        Return "
        <style>
            * { margin: 0; padding: 0; box-sizing: border-box; }
            body { font-family: 'Segoe UI', Arial, sans-serif; background: #fff;
                   color: #3D1A24; padding: 30px; }
            .header { background: linear-gradient(135deg, #3D1A24 0%, #6B2D45 100%);
                      color: white; padding: 24px 30px; border-radius: 12px;
                      margin-bottom: 28px; display: flex;
                      justify-content: space-between; align-items: center; }
            .header h1 { font-size: 22px; font-weight: 600; letter-spacing: 0.5px; }
            .header .sous-titre { font-size: 13px; opacity: 0.75; margin-top: 4px; }
            .header .meta { text-align: right; font-size: 12px; opacity: 0.8; }
            .header .logo { font-size: 28px; margin-bottom: 4px; }
            .kpi-row { display: flex; gap: 16px; margin-bottom: 28px; }
            .kpi { flex: 1; background: #FDE8EF; border-radius: 10px;
                   padding: 18px 20px; text-align: center;
                   border-left: 4px solid #C45A7E; }
            .kpi .val { font-size: 26px; font-weight: 700; color: #C45A7E; }
            .kpi .lib { font-size: 12px; color: #7A3050; margin-top: 4px; }
            table { width: 100%; border-collapse: collapse; border-radius: 10px;
                    overflow: hidden; box-shadow: 0 2px 8px rgba(61,26,36,0.08); }
            thead th { background: #3D1A24; color: white; padding: 12px 14px;
                       font-size: 12px; font-weight: 600; text-align: left;
                       letter-spacing: 0.3px; }
            tbody tr:nth-child(even) { background: #FFF5F8; }
            tbody tr:hover { background: #FDE8EF; }
            td { padding: 11px 14px; font-size: 13px; border-bottom: 1px solid #F5E0E6; }
            .badge { display: inline-block; padding: 3px 10px; border-radius: 20px;
                     font-size: 11px; font-weight: 600; }
            .badge-vert   { background: #E1F5EE; color: #0F6E56; }
            .badge-rouge  { background: #FCEBEB; color: #A32D2D; }
            .badge-orange { background: #FEF5E7; color: #854F0B; }
            .badge-rose   { background: #FDE8EF; color: #C45A7E; }
            .badge-gris   { background: #F0F0F0; color: #777; }
            .total-row td { background: #3D1A24; color: white;
                            font-weight: 700; font-size: 14px; }
            .footer { margin-top: 30px; text-align: center; font-size: 11px;
                      color: #A07080; border-top: 1px solid #F5E0E6; padding-top: 14px; }
            @media print {
                body { padding: 10px; }
                .header { border-radius: 4px; }
                table { box-shadow: none; }
            }
        </style>"
    End Function

    ' ══════════════════════════════════════════════
    ' HELPER — Badge statut RDV
    ' ══════════════════════════════════════════════
    Private Shared Function BadgeStatutRdv(statut As String) As String
        Select Case statut
            Case "Confirmé" : Return "badge-vert"
            Case "Annulé" : Return "badge-rouge"
            Case "Terminé" : Return "badge-gris"
            Case Else : Return "badge-orange"
        End Select
    End Function

    ' ══════════════════════════════════════════════
    ' HELPER — Badge rang top prestations
    ' ══════════════════════════════════════════════
    Private Shared Function ClasseRang(rang As Integer) As String
        Select Case rang
            Case 1 : Return "rang-1"
            Case 2 : Return "rang-2"
            Case 3 : Return "rang-3"
            Case Else : Return "rang-n"
        End Select
    End Function

    ' ══════════════════════════════════════════════
    ' HELPER — Médaille podium
    ' ══════════════════════════════════════════════
    Private Shared Function Medaille(rang As Integer) As String
        Select Case rang
            Case 1 : Return "🥇"
            Case 2 : Return "🥈"
            Case 3 : Return "🥉"
            Case Else : Return rang.ToString() & "."
        End Select
    End Function

    ' ══════════════════════════════════════════════
    ' ÉTAT 1 — RENDEZ-VOUS DU JOUR
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatRdvDuJour()
        Dim rdvs = Mainframe.RendezVousCtrl.GetRdvDuJour()
        Dim date_ = Date.Today.ToString("dddd dd MMMM yyyy",
                    New CultureInfo("fr-FR"))

        Dim nbConfirmes = rdvs.Where(Function(r) r.Statut = "Confirmé").Count()
        Dim nbAttente = rdvs.Where(Function(r) r.Statut = "En attente").Count()
        Dim nbTermines = rdvs.Where(Function(r) r.Statut = "Terminé").Count()
        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>RDV du jour — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header'>")
        sb.AppendLine("  <div><div class='logo'>💅</div>")
        sb.AppendLine("    <h1>Rendez-vous du jour</h1>")
        sb.AppendLine("    <div class='sous-titre'>" & date_ & "</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<div class='kpi-row'>")
        sb.AppendLine("  <div class='kpi'><div class='val'>" & rdvs.Count &
                      "</div><div class='lib'>Total du jour</div></div>")
        sb.AppendLine("  <div class='kpi'><div class='val'>" & nbConfirmes &
                      "</div><div class='lib'>Confirmés</div></div>")
        sb.AppendLine("  <div class='kpi'><div class='val'>" & nbAttente &
                      "</div><div class='lib'>En attente</div></div>")
        sb.AppendLine("  <div class='kpi'><div class='val'>" & nbTermines &
                      "</div><div class='lib'>Terminés</div></div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>#</th><th>Cliente</th><th>Heure</th>")
        sb.AppendLine("  <th>Prestation</th><th>Employée</th><th>Statut</th>")
        sb.AppendLine("</tr></thead><tbody>")

        If rdvs.Count = 0 Then
            sb.AppendLine("<tr><td colspan='6' style='text-align:center;padding:30px;color:#A07080;'>")
            sb.AppendLine("Aucun rendez-vous prévu aujourd'hui.</td></tr>")
        Else
            Dim n = 1
            For Each rdv In rdvs
                Dim nomC = "—" : Dim nomP = "—" : Dim nomE = "—"
                Try
                    Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                    If c IsNot Nothing Then nomC = c.Prenom & " " & c.Nom
                Catch : End Try
                Try
                    Dim p = Mainframe.PrestationCtrl.GetPrestationById(rdv.IdPrestation)
                    If p IsNot Nothing Then nomP = p.Nom
                Catch : End Try
                Try
                    Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
                    If emp IsNot Nothing Then nomE = emp.Prenom & " " & emp.Nom
                Catch : End Try

                Dim heure = rdv.DateHeureDebut.ToString("HH:mm") & " – " &
                            rdv.DateHeureFin.ToString("HH:mm")

                sb.AppendLine("<tr>")
                sb.AppendLine("  <td>" & n & "</td>")
                sb.AppendLine("  <td><strong>" & Esc(nomC) & "</strong></td>")
                sb.AppendLine("  <td>" & heure & "</td>")
                sb.AppendLine("  <td>" & Esc(nomP) & "</td>")
                sb.AppendLine("  <td>" & Esc(nomE) & "</td>")
                sb.AppendLine("  <td><span class='badge " & BadgeStatutRdv(rdv.Statut) &
                              "'>" & rdv.Statut & "</span></td>")
                sb.AppendLine("</tr>")
                n += 1
            Next
        End If

        sb.AppendLine("</tbody></table>")
        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_RdvJour")
    End Sub

    ' ══════════════════════════════════════════════
    ' ÉTAT 2 — HISTORIQUE DES FACTURES
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatHistoriqueFactures()
        Dim factures = Mainframe.FactureCtrl.GetAllFactures()
        Dim totalGeneral As Decimal = 0

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>Historique Factures — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header'>")
        sb.AppendLine("  <div><div class='logo'>🧾</div>")
        sb.AppendLine("    <h1>Historique des factures</h1>")
        sb.AppendLine("    <div class='sous-titre'>" & factures.Count &
                      " facture(s) au total</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>N° Facture</th><th>Date</th><th>Cliente</th>")
        sb.AppendLine("  <th>Employée</th><th>Mode paiement</th>" &
                      "<th style='text-align:right'>Total</th>")
        sb.AppendLine("</tr></thead><tbody>")

        If factures.Count = 0 Then
            sb.AppendLine("<tr><td colspan='6' style='text-align:center;" &
                          "padding:30px;color:#A07080;'>")
            sb.AppendLine("Aucune facture enregistrée.</td></tr>")
        Else
            For Each f In factures
                Dim total = Mainframe.FactureCtrl.GetTotalFacture(f.IdFacture)
                totalGeneral += total

                Dim nomC = "—" : Dim nomE = "—"
                Try
                    If f.IdRdv > 0 Then
                        Dim rdv = Mainframe.RendezVousCtrl.GetRdvById(f.IdRdv)
                        If rdv IsNot Nothing Then
                            Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                            If c IsNot Nothing Then nomC = c.Prenom & " " & c.Nom
                            Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
                            If emp IsNot Nothing Then nomE = emp.Prenom & " " & emp.Nom
                        End If
                    End If
                Catch : End Try

                ' ── Mode paiement : adapte le nom de propriété à ton modèle ──
                Dim mode As String = "—"
                Try
                    Dim mp = TryCast(f.GetType().GetProperty("ModePaiement")?.
                                     GetValue(f), String)
                    If Not String.IsNullOrEmpty(mp) Then mode = mp
                Catch : End Try

                Dim numF = "F-" & f.IdFacture.ToString("D4")

                sb.AppendLine("<tr>")
                sb.AppendLine("  <td><strong>" & numF & "</strong></td>")
                sb.AppendLine("  <td>" & f.DateFacture.ToString("dd/MM/yyyy HH:mm") & "</td>")
                sb.AppendLine("  <td>" & Esc(nomC) & "</td>")
                sb.AppendLine("  <td>" & Esc(nomE) & "</td>")
                sb.AppendLine("  <td><span class='badge badge-rose'>" &
                              Esc(mode) & "</span></td>")
                sb.AppendLine("  <td style='text-align:right;font-weight:600'>" &
                              FormatNombre(total) & " F</td>")
                sb.AppendLine("</tr>")
            Next

            sb.AppendLine("<tr class='total-row'>")
            sb.AppendLine("  <td colspan='5'>TOTAL GÉNÉRAL</td>")
            sb.AppendLine("  <td style='text-align:right'>" &
                          FormatNombre(totalGeneral) & " F CFA</td>")
            sb.AppendLine("</tr>")
        End If

        sb.AppendLine("</tbody></table>")
        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_Factures")
    End Sub

    ' ══════════════════════════════════════════════
    ' ÉTAT 3 — REVENUS PAR MOIS (6 derniers mois)
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatRevenusParMois()
        Dim revenus = Mainframe.FactureCtrl.GetRevenusParMois(6)
        Dim totalGeneral = revenus.Sum(Function(r) r.Value)
        Dim maxVal = If(revenus.Count > 0, revenus.Max(Function(r) r.Value), 1D)

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>Revenus par mois — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("<style>
            .bar-row { display:flex; align-items:center; gap:12px; margin-bottom:10px; }
            .bar-label { width:60px; font-size:13px; font-weight:600; color:#3D1A24; }
            .bar-bg { flex:1; background:#FDE8EF; border-radius:6px;
                      height:28px; overflow:hidden; }
            .bar-fill { height:100%;
                        background:linear-gradient(90deg,#C45A7E,#E8829A);
                        border-radius:6px; display:flex; align-items:center;
                        padding-left:10px; color:white; font-size:12px;
                        font-weight:600; }
            .bar-val { width:110px; text-align:right; font-size:13px;
                       font-weight:700; color:#C45A7E; }
        </style>")
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header'>")
        sb.AppendLine("  <div><div class='logo'>📊</div>")
        sb.AppendLine("    <h1>Revenus des 6 derniers mois</h1>")
        sb.AppendLine("    <div class='sous-titre'>Total : " &
                      FormatNombre(totalGeneral) & " F CFA</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<div style='background:#FFF5F8;border-radius:12px;" &
                      "padding:24px;margin-bottom:28px;'>")
        sb.AppendLine("<h3 style='margin-bottom:20px;color:#3D1A24;font-size:15px;'>" &
                      "Évolution mensuelle</h3>")

        If revenus.Count = 0 Then
            sb.AppendLine("<p style='color:#A07080;text-align:center;padding:20px;'>" &
                          "Aucune donnée disponible.</p>")
        Else
            For Each r In revenus
                Dim pct = If(maxVal > 0, CInt(r.Value / maxVal * 100), 0)
                sb.AppendLine("<div class='bar-row'>")
                sb.AppendLine("  <div class='bar-label'>" & Esc(r.Key) & "</div>")
                sb.AppendLine("  <div class='bar-bg'>")
                sb.AppendLine("    <div class='bar-fill' style='width:" & pct & "%'>")
                If pct > 15 Then sb.AppendLine("      " & pct & "%")
                sb.AppendLine("    </div></div>")
                sb.AppendLine("  <div class='bar-val'>" &
                              FormatNombre(r.Value) & " F</div>")
                sb.AppendLine("</div>")
            Next
        End If
        sb.AppendLine("</div>")

        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>Mois</th>")
        sb.AppendLine("  <th style='text-align:right'>Chiffre d'affaires</th>")
        sb.AppendLine("  <th style='text-align:right'>Part du total</th>")
        sb.AppendLine("</tr></thead><tbody>")

        For Each r In revenus
            Dim part = If(totalGeneral > 0,
                          CInt(r.Value / totalGeneral * 100), 0)
            sb.AppendLine("<tr>")
            sb.AppendLine("  <td><strong>" & Esc(r.Key) & "</strong></td>")
            sb.AppendLine("  <td style='text-align:right;font-weight:600'>" &
                          FormatNombre(r.Value) & " F CFA</td>")
            sb.AppendLine("  <td style='text-align:right'>" & part & " %</td>")
            sb.AppendLine("</tr>")
        Next

        sb.AppendLine("<tr class='total-row'>")
        sb.AppendLine("  <td>TOTAL</td>")
        sb.AppendLine("  <td style='text-align:right'>" &
                      FormatNombre(totalGeneral) & " F CFA</td>")
        sb.AppendLine("  <td style='text-align:right'>100 %</td>")
        sb.AppendLine("</tr>")
        sb.AppendLine("</tbody></table>")

        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_Revenus")
    End Sub

    ' ══════════════════════════════════════════════
    ' ÉTAT 4 — TOP PRESTATIONS DU MOIS
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatTopPrestations()
        Dim tops = Mainframe.FactureCtrl.GetTopPrestations(10)
        Dim mois = Date.Today.ToString("MMMM yyyy", New CultureInfo("fr-FR"))
        Dim maxVal = If(tops.Count > 0, tops.Max(Function(t) t.Value), 1)

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>Top Prestations — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("<style>
            .rang { width:30px; height:30px; border-radius:50%;
                    display:inline-flex; align-items:center;
                    justify-content:center; font-weight:700; font-size:13px; }
            .rang-1 { background:#FFD700; color:#7A5500; }
            .rang-2 { background:#C0C0C0; color:#444; }
            .rang-3 { background:#CD7F32; color:#fff; }
            .rang-n { background:#FDE8EF; color:#C45A7E; }
        </style>")
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header'>")
        sb.AppendLine("  <div><div class='logo'>🏆</div>")
        sb.AppendLine("    <h1>Top prestations — " & mois & "</h1>")
        sb.AppendLine("    <div class='sous-titre'>" & tops.Count &
                      " prestation(s) ce mois</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th style='width:60px'>Rang</th>")
        sb.AppendLine("  <th>Prestation</th>")
        sb.AppendLine("  <th style='text-align:center'>Nombre de fois</th>")
        sb.AppendLine("  <th>Popularité</th>")
        sb.AppendLine("</tr></thead><tbody>")

        If tops.Count = 0 Then
            sb.AppendLine("<tr><td colspan='4' style='text-align:center;" &
                          "padding:30px;color:#A07080;'>")
            sb.AppendLine("Aucune prestation facturée ce mois.</td></tr>")
        Else
            Dim rang = 1
            For Each t In tops
                Dim pct = If(maxVal > 0, CInt(t.Value / maxVal * 100), 0)
                sb.AppendLine("<tr>")
                sb.AppendLine("  <td style='text-align:center'>")
                sb.AppendLine("    <span class='rang " & ClasseRang(rang) &
                              "'>" & rang & "</span></td>")
                sb.AppendLine("  <td><strong>" & Esc(t.Key) & "</strong></td>")
                sb.AppendLine("  <td style='text-align:center;font-size:16px;" &
                              "font-weight:700;color:#C45A7E'>" & t.Value & "</td>")
                sb.AppendLine("  <td>")
                sb.AppendLine("    <div style='background:#FDE8EF;border-radius:4px;height:14px;'>")
                sb.AppendLine("      <div style='background:#C45A7E;height:14px;" &
                              "border-radius:4px;width:" & pct & "%'></div>")
                sb.AppendLine("    </div></td>")
                sb.AppendLine("</tr>")
                rang += 1
            Next
        End If

        sb.AppendLine("</tbody></table>")
        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_TopPrestations")
    End Sub

    ' ══════════════════════════════════════════════
    ' ÉTAT 5 — RDV NON FACTURÉS
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatRdvNonFactures()
        Dim rdvs = Mainframe.RendezVousCtrl.GetRdvNonFactures()

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>RDV non facturés — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header' style='background:linear-gradient(" &
                      "135deg,#854F0B,#C47B1A)'>")
        sb.AppendLine("  <div><div class='logo'>⚠️</div>")
        sb.AppendLine("    <h1>Rendez-vous non facturés</h1>")
        sb.AppendLine("    <div class='sous-titre'>" & rdvs.Count &
                      " RDV terminé(s) sans facture</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        If rdvs.Count > 0 Then
            sb.AppendLine("<div style='background:#FEF5E7;border:1px solid #F0C060;" &
                          "border-radius:8px;padding:14px;margin-bottom:20px;" &
                          "color:#854F0B;font-size:13px;'>")
            sb.AppendLine("  ⚠ Ces rendez-vous sont terminés ou confirmés mais " &
                          "n'ont pas encore été facturés.")
            sb.AppendLine("</div>")
        End If

        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>#</th><th>Cliente</th><th>Date &amp; Heure</th>")
        sb.AppendLine("  <th>Prestation</th><th>Employée</th><th>Statut</th>")
        sb.AppendLine("</tr></thead><tbody>")

        If rdvs.Count = 0 Then
            sb.AppendLine("<tr><td colspan='6' style='text-align:center;" &
                          "padding:30px;color:#0F6E56;font-weight:600;'>")
            sb.AppendLine("✅ Tous les rendez-vous ont été facturés !</td></tr>")
        Else
            Dim n = 1
            For Each rdv In rdvs
                Dim nomC = "—" : Dim nomP = "—" : Dim nomE = "—"
                Try
                    Dim c = Mainframe.ClientCtrl.GetClientById(rdv.IdClient)
                    If c IsNot Nothing Then nomC = c.Prenom & " " & c.Nom
                Catch : End Try
                Try
                    Dim p = Mainframe.PrestationCtrl.GetPrestationById(rdv.IdPrestation)
                    If p IsNot Nothing Then nomP = p.Nom
                Catch : End Try
                Try
                    Dim emp = Mainframe.EmployeCtrl.GetEmployeById(rdv.IdEmploye)
                    If emp IsNot Nothing Then nomE = emp.Prenom & " " & emp.Nom
                Catch : End Try

                Dim badgeClass = If(rdv.Statut = "Confirmé",
                                    "badge-vert", "badge-gris")

                sb.AppendLine("<tr style='background:#FFFBF0;'>")
                sb.AppendLine("  <td>" & n & "</td>")
                sb.AppendLine("  <td><strong>" & Esc(nomC) & "</strong></td>")
                sb.AppendLine("  <td>" &
                              rdv.DateHeureDebut.ToString("dd/MM/yyyy HH:mm") & "</td>")
                sb.AppendLine("  <td>" & Esc(nomP) & "</td>")
                sb.AppendLine("  <td>" & Esc(nomE) & "</td>")
                sb.AppendLine("  <td><span class='badge " & badgeClass & "'>" &
                              rdv.Statut & "</span></td>")
                sb.AppendLine("</tr>")
                n += 1
            Next
        End If

        sb.AppendLine("</tbody></table>")
        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_RdvNonFactures")
    End Sub

    ' ══════════════════════════════════════════════
    ' ÉTAT 6 — CHIFFRE D'AFFAIRES DU MOIS
    ' ══════════════════════════════════════════════
    Public Shared Sub EtatChiffreDuMois()
        Dim totalMois = Mainframe.FactureCtrl.GetChiffreDuMois()
        Dim tops = Mainframe.FactureCtrl.GetTopPrestations(5)
        Dim revenus = Mainframe.FactureCtrl.GetRevenusParMois(6)
        Dim mois = Date.Today.ToString("MMMM yyyy", New CultureInfo("fr-FR"))

        Dim variation = ""
        If revenus.Count >= 2 Then
            Dim moisPrec = revenus(revenus.Count - 2).Value
            Dim moisCour = revenus(revenus.Count - 1).Value
            If moisPrec > 0 Then
                Dim pct = CInt((moisCour - moisPrec) / moisPrec * 100)
                variation = If(pct >= 0,
                               "▲ +" & pct & "% vs mois précédent",
                               "▼ " & pct & "% vs mois précédent")
            End If
        End If

        Dim sb As New StringBuilder()
        sb.AppendLine("<!DOCTYPE html><html lang='fr'><head>")
        sb.AppendLine("<meta charset='UTF-8'>")
        sb.AppendLine("<title>CA du mois — BeautyManag</title>")
        sb.AppendLine(CssCommun())
        sb.AppendLine("<style>
            .ca-hero { text-align:center;
                       background:linear-gradient(135deg,#3D1A24,#6B2D45);
                       color:white; border-radius:12px;
                       padding:36px 20px; margin-bottom:28px; }
            .ca-hero .montant { font-size:42px; font-weight:700; letter-spacing:1px; }
            .ca-hero .libelle { font-size:14px; opacity:0.75; margin-top:6px; }
            .ca-hero .var { font-size:13px; margin-top:12px;
                            background:rgba(255,255,255,0.15);
                            display:inline-block; padding:4px 14px;
                            border-radius:20px; }
            .section-title { font-size:15px; font-weight:700; color:#3D1A24;
                             margin:24px 0 14px; padding-left:10px;
                             border-left:4px solid #C45A7E; }
        </style>")
        sb.AppendLine("</head><body>")

        sb.AppendLine("<div class='header'>")
        sb.AppendLine("  <div><div class='logo'>💰</div>")
        sb.AppendLine("    <h1>Chiffre d'affaires — " & mois & "</h1>")
        sb.AppendLine("    <div class='sous-titre'>Tableau de bord financier mensuel" &
                      "</div></div>")
        sb.AppendLine("  <div class='meta'>BeautyManag<br>Imprimé le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</div>")

        sb.AppendLine("<div class='ca-hero'>")
        sb.AppendLine("  <div class='montant'>" &
                      FormatNombre(totalMois) & " F CFA</div>")
        sb.AppendLine("  <div class='libelle'>Chiffre d'affaires de " & mois & "</div>")
        If Not String.IsNullOrEmpty(variation) Then
            sb.AppendLine("  <div class='var'>" & variation & "</div>")
        End If
        sb.AppendLine("</div>")

        sb.AppendLine("<div class='section-title'>🏆 Top 5 prestations du mois</div>")
        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>Rang</th><th>Prestation</th>" &
                      "<th style='text-align:center'>Fois</th>")
        sb.AppendLine("</tr></thead><tbody>")

        If tops.Count = 0 Then
            sb.AppendLine("<tr><td colspan='3' style='text-align:center;" &
                          "padding:20px;color:#A07080;'>")
            sb.AppendLine("Aucune prestation ce mois.</td></tr>")
        Else
            Dim rang = 1
            For Each t In tops
                sb.AppendLine("<tr>")
                sb.AppendLine("  <td style='text-align:center;font-size:18px'>" &
                              Medaille(rang) & "</td>")
                sb.AppendLine("  <td><strong>" & Esc(t.Key) & "</strong></td>")
                sb.AppendLine("  <td style='text-align:center;font-weight:700;" &
                              "color:#C45A7E'>" & t.Value & " fois</td>")
                sb.AppendLine("</tr>")
                rang += 1
            Next
        End If
        sb.AppendLine("</tbody></table>")

        sb.AppendLine("<div class='section-title'>📈 Évolution sur 6 mois</div>")
        sb.AppendLine("<table><thead><tr>")
        sb.AppendLine("  <th>Mois</th>")
        sb.AppendLine("  <th style='text-align:right'>Chiffre d'affaires</th>")
        sb.AppendLine("</tr></thead><tbody>")

        Dim moisCourantLabel = Date.Today.ToString("MMM",
                               New CultureInfo("fr-FR")).ToLower()
        For Each r In revenus
            Dim estCourant = (r.Key.ToLower() = moisCourantLabel)
            Dim rowStyle = If(estCourant,
                              " style='background:#FDE8EF;font-weight:700'", "")
            sb.AppendLine("<tr" & rowStyle & ">")
            sb.AppendLine("  <td>" & Esc(r.Key) &
                          If(estCourant, " ← mois en cours", "") & "</td>")
            sb.AppendLine("  <td style='text-align:right'>" &
                          FormatNombre(r.Value) & " F CFA</td>")
            sb.AppendLine("</tr>")
        Next

        sb.AppendLine("</tbody></table>")
        sb.AppendLine("<div class='footer'>BeautyManag · État généré le " &
                      Date.Now.ToString("dd/MM/yyyy à HH:mm") & "</div>")
        sb.AppendLine("</body></html>")

        OuvrirEtat(sb.ToString(), "BeautyManag_ChiffreMois")
    End Sub

    ' ══════════════════════════════════════════════
    ' HELPERS PRIVÉS
    ' ══════════════════════════════════════════════
    Private Shared Function Esc(texte As String) As String
        If String.IsNullOrEmpty(texte) Then Return "—"
        Return texte.Replace("&", "&amp;").Replace("<", "&lt;").
                     Replace(">", "&gt;").Replace("""", "&quot;")
    End Function

    Private Shared Function FormatNombre(val As Decimal) As String
        Return val.ToString("N0", New CultureInfo("fr-FR"))
    End Function

End Class