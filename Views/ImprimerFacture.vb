' =====================================================
' CLASSE IMPRESSION FACTURE — Rosa Beauty
' Utilise iTextSharp 5.5.13.5
' =====================================================
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Public Class ImprimerFacture

    ' ── Couleurs Rosa Beauty ──
    Private Shared couleurBordeaux As New BaseColor(61, 26, 36)
    Private Shared couleurRose As New BaseColor(196, 90, 126)
    Private Shared couleurRoseClair As New BaseColor(253, 232, 239)
    Private Shared couleurGrisClair As New BaseColor(245, 245, 245)
    Private Shared couleurTexte As New BaseColor(71, 69, 94)

    ' ── Polices ──
    Private Shared fonteTitre As Font
    Private Shared fonteNormal As Font
    Private Shared fonteBold As Font
    Private Shared fontePetit As Font
    Private Shared fonteBlanche As Font
    Private Shared fonteBoldBlanc As Font
    Private Shared fonteRose As Font
    Private Shared fonteBordeaux As Font

    ' ─────────────────────────────────────────────
    ' MÉTHODE PRINCIPALE
    ' ─────────────────────────────────────────────
    Public Shared Sub GenererEtImprimer(
        nomCliente As String,
        telCliente As String,
        nomEmploye As String,
        modePaiement As String,
        lignes As List(Of FactureDetail),
        numeroFacture As String)

        Try
            ' Chemin de sauvegarde temporaire
            Dim cheminPdf = Path.Combine(Path.GetTempPath(),
                "FactureRosaBeauty_" & numeroFacture & ".pdf")

            ' Créer le document A4
            Dim doc As New Document(PageSize.A4, 40, 40, 40, 40)
            Dim writer = PdfWriter.GetInstance(doc, New FileStream(cheminPdf, FileMode.Create))

            ' Ajouter le fond de page
            writer.PageEvent = New FondDePage()

            doc.Open()
            InitPolices()

            ' ── En-tête ──
            AjouterEntete(doc, writer)

            doc.Add(New Paragraph(" "))

            ' ── Infos facture + cliente ──
            AjouterInfos(doc, nomCliente, telCliente, nomEmploye, numeroFacture)

            doc.Add(New Paragraph(" "))

            ' ── Tableau des lignes ──
            Dim totalPresta As Decimal = 0
            Dim totalProduits As Decimal = 0
            AjouterTableau(doc, lignes, totalPresta, totalProduits)

            doc.Add(New Paragraph(" "))

            ' ── Résumé total ──
            AjouterResume(doc, totalPresta, totalProduits, modePaiement)

            doc.Add(New Paragraph(" "))
            doc.Add(New Paragraph(" "))

            ' ── Pied de page message ──
            AjouterPiedDePage(doc)

            doc.Close()

            ' Ouvrir le PDF pour impression
            Process.Start(New ProcessStartInfo(cheminPdf) With {.UseShellExecute = True})

        Catch ex As Exception
            MsgBox("Erreur lors de la génération du PDF : " & ex.Message,
                   MsgBoxStyle.Critical, "Erreur impression")
        End Try
    End Sub

    ' ─────────────────────────────────────────────
    ' INITIALISER LES POLICES
    ' ─────────────────────────────────────────────
    Private Shared Sub InitPolices()
        fonteTitre = New Font(Font.FontFamily.HELVETICA, 22, Font.BOLD, couleurBordeaux)
        fonteNormal = New Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL, couleurTexte)
        fonteBold = New Font(Font.FontFamily.HELVETICA, 9, Font.BOLD, couleurTexte)
        fontePetit = New Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL, couleurTexte)
        fonteBlanche = New Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL, BaseColor.WHITE)
        fonteBoldBlanc = New Font(Font.FontFamily.HELVETICA, 9, Font.BOLD, BaseColor.WHITE)
        fonteRose = New Font(Font.FontFamily.HELVETICA, 14, Font.BOLD, couleurRose)
        fonteBordeaux = New Font(Font.FontFamily.HELVETICA, 9, Font.BOLD, couleurBordeaux)
    End Sub

    ' ─────────────────────────────────────────────
    ' EN-TÊTE
    ' ─────────────────────────────────────────────
    Private Shared Sub AjouterEntete(doc As Document, writer As PdfWriter)
        Dim tbl As New PdfPTable(2)
        tbl.WidthPercentage = 100
        tbl.SetWidths(New Single() {1, 2})
        tbl.DefaultCell.Border = Rectangle.NO_BORDER

        ' ── Cellule logo ──
        Dim cellLogo As New PdfPCell()
        cellLogo.Border = Rectangle.NO_BORDER
        cellLogo.VerticalAlignment = Element.ALIGN_MIDDLE
        cellLogo.PaddingRight = 10

        Try
            Dim logoPath = "C:\Users\Clever\Desktop\BeautyManager\Ressources\icons8-lotus-40.png"
            If File.Exists(logoPath) Then
                Dim logo = iTextSharp.text.Image.GetInstance(logoPath)
                logo.ScaleAbsolute(45, 45)
                cellLogo.AddElement(logo)
            End If
        Catch
        End Try
        tbl.AddCell(cellLogo)

        ' ── Cellule infos salon ──
        Dim cellInfos As New PdfPCell()
        cellInfos.Border = Rectangle.NO_BORDER
        cellInfos.VerticalAlignment = Element.ALIGN_MIDDLE

        Dim nomSalon As New Paragraph("Rosa Beauty", fonteTitre)
        nomSalon.Alignment = Element.ALIGN_LEFT
        cellInfos.AddElement(nomSalon)

        Dim fonteGrise = New Font(Font.FontFamily.HELVETICA, 9, Font.NORMAL,
                                   New BaseColor(160, 112, 128))
        cellInfos.AddElement(New Paragraph("Salon de beauté", fonteGrise))
        cellInfos.AddElement(New Paragraph("Lomé, Togo  |  +228 91 23 14 04", fonteGrise))
        tbl.AddCell(cellInfos)

        doc.Add(tbl)

        ' ── Ligne séparatrice rose ──
        Dim sep As New PdfPTable(1)
        sep.WidthPercentage = 100
        Dim cellSep As New PdfPCell(New Phrase(" "))
        cellSep.BackgroundColor = couleurRose
        cellSep.Border = Rectangle.NO_BORDER
        cellSep.FixedHeight = 3
        sep.AddCell(cellSep)
        doc.Add(sep)
    End Sub

    ' ─────────────────────────────────────────────
    ' INFOS FACTURE ET CLIENTE
    ' ─────────────────────────────────────────────
    Private Shared Sub AjouterInfos(doc As Document,
                                     nomCliente As String,
                                     telCliente As String,
                                     nomEmploye As String,
                                     numeroFacture As String)
        Dim tbl As New PdfPTable(2)
        tbl.WidthPercentage = 100
        tbl.SpacingBefore = 10
        tbl.DefaultCell.Border = Rectangle.NO_BORDER

        ' ── Gauche : infos cliente ──
        Dim cellCliente As New PdfPCell()
        cellCliente.Border = Rectangle.NO_BORDER
        cellCliente.BackgroundColor = couleurRoseClair
        cellCliente.Padding = 12
        'cellCliente.BorderRadius = 6

        Dim titreCliente = New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD,
                                     New BaseColor(160, 112, 128))
        cellCliente.AddElement(New Paragraph("CLIENTE", titreCliente))
        cellCliente.AddElement(New Paragraph(" "))
        cellCliente.AddElement(New Paragraph(nomCliente,
            New Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, couleurBordeaux)))
        cellCliente.AddElement(New Paragraph(telCliente, fonteNormal))
        If Not String.IsNullOrEmpty(nomEmploye) AndAlso nomEmploye <> "—" Then
            cellCliente.AddElement(New Paragraph("Employée : " & nomEmploye, fontePetit))
        End If
        tbl.AddCell(cellCliente)

        ' ── Droite : numéro et date ──
        Dim cellFacture As New PdfPCell()
        cellFacture.Border = Rectangle.NO_BORDER
        cellFacture.HorizontalAlignment = Element.ALIGN_RIGHT
        cellFacture.PaddingLeft = 20

        Dim fonteNumero = New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD,
                                    New BaseColor(160, 112, 128))
        cellFacture.AddElement(New Paragraph("FACTURE N°", fonteNumero) With {
            .Alignment = Element.ALIGN_RIGHT})

        Dim fonteNumVal = New Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, couleurBordeaux)
        cellFacture.AddElement(New Paragraph("#" & numeroFacture, fonteNumVal) With {
            .Alignment = Element.ALIGN_RIGHT})

        cellFacture.AddElement(New Paragraph(" "))
        cellFacture.AddElement(New Paragraph("Date : " & Date.Today.ToString("dd/MM/yyyy"),
            fonteNormal) With {.Alignment = Element.ALIGN_RIGHT})
        tbl.AddCell(cellFacture)

        doc.Add(tbl)
    End Sub

    ' ─────────────────────────────────────────────
    ' TABLEAU DES LIGNES
    ' ─────────────────────────────────────────────
    Private Shared Sub AjouterTableau(doc As Document,
                                       lignes As List(Of FactureDetail),
                                       ByRef totalPresta As Decimal,
                                       ByRef totalProduits As Decimal)
        Dim tbl As New PdfPTable(5)
        tbl.WidthPercentage = 100
        tbl.SpacingBefore = 10
        tbl.SetWidths(New Single() {4, 1.5, 1, 1.5, 1.5})

        ' ── En-têtes colonnes ──
        For Each entete In {"Désignation", "Type", "Qté", "Prix unit.", "Total"}
            Dim cell As New PdfPCell(New Phrase(entete, fonteBoldBlanc))
            cell.BackgroundColor = couleurBordeaux
            cell.Border = Rectangle.NO_BORDER
            cell.Padding = 8
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            tbl.AddCell(cell)
        Next

        ' ── Lignes de la facture ──
        Dim alternance As Boolean = False
        For Each ligne In lignes
            Dim designation = "—"
            Dim typeLigne = "—"
            Dim total = ligne.Prix * ligne.Quantite
            Dim bg = If(alternance, couleurGrisClair, BaseColor.WHITE)

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

            ' Désignation
            Dim c1 As New PdfPCell(New Phrase(designation, fonteBold))
            c1.BackgroundColor = bg : c1.Border = Rectangle.NO_BORDER
            c1.Padding = 8 : tbl.AddCell(c1)

            ' Type
            Dim c2 As New PdfPCell(New Phrase(typeLigne, fontePetit))
            c2.BackgroundColor = bg : c2.Border = Rectangle.NO_BORDER
            c2.Padding = 8 : c2.HorizontalAlignment = Element.ALIGN_CENTER
            tbl.AddCell(c2)

            ' Quantité
            Dim c3 As New PdfPCell(New Phrase(ligne.Quantite.ToString(), fonteNormal))
            c3.BackgroundColor = bg : c3.Border = Rectangle.NO_BORDER
            c3.Padding = 8 : c3.HorizontalAlignment = Element.ALIGN_CENTER
            tbl.AddCell(c3)

            ' Prix unitaire
            Dim c4 As New PdfPCell(New Phrase(FormatNumber(ligne.Prix, 0) & " F", fonteNormal))
            c4.BackgroundColor = bg : c4.Border = Rectangle.NO_BORDER
            c4.Padding = 8 : c4.HorizontalAlignment = Element.ALIGN_RIGHT
            tbl.AddCell(c4)

            ' Total ligne
            Dim c5 As New PdfPCell(New Phrase(FormatNumber(total, 0) & " F", fonteBold))
            c5.BackgroundColor = bg : c5.Border = Rectangle.NO_BORDER
            c5.Padding = 8 : c5.HorizontalAlignment = Element.ALIGN_RIGHT
            tbl.AddCell(c5)

            alternance = Not alternance
        Next

        doc.Add(tbl)
    End Sub

    ' ─────────────────────────────────────────────
    ' RÉSUMÉ TOTAL
    ' ─────────────────────────────────────────────
    Private Shared Sub AjouterResume(doc As Document,
                                      totalPresta As Decimal,
                                      totalProduits As Decimal,
                                      modePaiement As String)
        Dim tbl As New PdfPTable(2)
        tbl.WidthPercentage = 50
        tbl.HorizontalAlignment = Element.ALIGN_RIGHT
        tbl.SpacingBefore = 10

        ' Sous-total prestation
        AjouterLigneResume(tbl, "Prestation", FormatNumber(totalPresta, 0) & " F", False)

        ' Sous-total produits
        AjouterLigneResume(tbl, "Produits", FormatNumber(totalProduits, 0) & " F", False)

        ' Séparateur
        Dim sep1 As New PdfPCell(New Phrase(" "))
        sep1.Colspan = 2
        sep1.BackgroundColor = couleurRose
        sep1.Border = Rectangle.NO_BORDER
        sep1.FixedHeight = 2
        tbl.AddCell(sep1)

        ' Total général
        Dim totalGeneral = totalPresta + totalProduits
        Dim fonteTotal = New Font(Font.FontFamily.HELVETICA, 13, Font.BOLD, couleurRose)
        Dim fonteTotalVal = New Font(Font.FontFamily.HELVETICA, 13, Font.BOLD, couleurBordeaux)

        Dim cTotalLbl As New PdfPCell(New Phrase("TOTAL", fonteTotal))
        cTotalLbl.Border = Rectangle.NO_BORDER
        cTotalLbl.BackgroundColor = couleurRoseClair
        cTotalLbl.Padding = 10
        tbl.AddCell(cTotalLbl)

        Dim cTotalVal As New PdfPCell(New Phrase(FormatNumber(totalGeneral, 0) & " F CFA", fonteTotalVal))
        cTotalVal.Border = Rectangle.NO_BORDER
        cTotalVal.BackgroundColor = couleurRoseClair
        cTotalVal.Padding = 10
        cTotalVal.HorizontalAlignment = Element.ALIGN_RIGHT
        tbl.AddCell(cTotalVal)

        ' Mode de paiement
        Dim fontePaiement = New Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL,
                                      New BaseColor(160, 112, 128))
        Dim cPaiementLbl As New PdfPCell(New Phrase("Mode de paiement", fontePaiement))
        cPaiementLbl.Border = Rectangle.NO_BORDER
        cPaiementLbl.Padding = 6
        tbl.AddCell(cPaiementLbl)

        Dim cPaiementVal As New PdfPCell(New Phrase(modePaiement, fonteBordeaux))
        cPaiementVal.Border = Rectangle.NO_BORDER
        cPaiementVal.Padding = 6
        cPaiementVal.HorizontalAlignment = Element.ALIGN_RIGHT
        tbl.AddCell(cPaiementVal)

        doc.Add(tbl)
    End Sub

    Private Shared Sub AjouterLigneResume(tbl As PdfPTable,
                                           label As String,
                                           valeur As String,
                                           estTotal As Boolean)
        Dim fonte = If(estTotal, fonteBold, fonteNormal)

        Dim c1 As New PdfPCell(New Phrase(label, fonte))
        c1.Border = Rectangle.NO_BORDER
        c1.Padding = 5
        tbl.AddCell(c1)

        Dim c2 As New PdfPCell(New Phrase(valeur, fonte))
        c2.Border = Rectangle.NO_BORDER
        c2.Padding = 5
        c2.HorizontalAlignment = Element.ALIGN_RIGHT
        tbl.AddCell(c2)
    End Sub

    ' ─────────────────────────────────────────────
    ' PIED DE PAGE
    ' ─────────────────────────────────────────────
    Private Shared Sub AjouterPiedDePage(doc As Document)
        Dim sep As New PdfPTable(1)
        sep.WidthPercentage = 100
        Dim cellSep As New PdfPCell(New Phrase(" "))
        cellSep.BackgroundColor = couleurRose
        cellSep.Border = Rectangle.NO_BORDER
        cellSep.FixedHeight = 2
        sep.AddCell(cellSep)
        doc.Add(sep)

        Dim fonteMessage = New Font(Font.FontFamily.HELVETICA, 9, Font.ITALIC,
                                     New BaseColor(160, 112, 128))
        Dim message As New Paragraph(
            "Merci pour votre confiance ! Rosa Beauty vous souhaite une excellente journée. 🌸",
            fonteMessage)
        message.Alignment = Element.ALIGN_CENTER
        message.SpacingBefore = 8
        doc.Add(message)
    End Sub

End Class

' ─────────────────────────────────────────────
' FOND DE PAGE (watermark discret)
' ─────────────────────────────────────────────
Public Class FondDePage
    Inherits PdfPageEventHelper

    Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
        ' Numéro de page en bas
        Dim cb = writer.DirectContent
        Dim fonte = New Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL,
                              New BaseColor(160, 112, 128))
        Dim phrase As New Phrase("Rosa Beauty — Page " &
                                  writer.PageNumber.ToString(), fonte)
        ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, phrase,
                                    document.PageSize.Width / 2, 25, 0)
    End Sub
End Class