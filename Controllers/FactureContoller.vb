' =====================================================
' CONTROLLER FACTURE
' =====================================================
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class FactureController

    Public Function GetAllFactures() As List(Of Facture)
        Dim liste As New List(Of Facture)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM facture ORDER BY dateFacture DESC", conn)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                liste.Add(LireFacture(reader))
            End While
        End Using
        Return liste
    End Function

    Public Function GetFactureById(id As Integer) As Facture
        Dim f As Facture = Nothing
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM facture WHERE idFacture = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then f = LireFacture(reader)
        End Using
        Return f
    End Function

    Public Function GetDetailsFacture(idFacture As Integer) As List(Of FactureDetail)
        Dim liste As New List(Of FactureDetail)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM factureDetail WHERE idFacture = @id", conn)
            cmd.Parameters.AddWithValue("@id", idFacture)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim d As New FactureDetail()
                d.IdDetail = reader.GetInt32("idDetail")
                d.IdFacture = reader.GetInt32("idFacture")
                d.IdPrestation = If(reader.IsDBNull(reader.GetOrdinal("idPrestation")), Nothing, reader.GetInt32("idPrestation"))
                d.IdProduit = If(reader.IsDBNull(reader.GetOrdinal("idProduit")), Nothing, reader.GetInt32("idProduit"))
                d.Quantite = reader.GetInt32("quantite")
                d.Prix = reader.GetDecimal("prix")
                liste.Add(d)
            End While
        End Using
        Return liste
    End Function

    ' ─────────────────────────────────────────────
    ' CALCULER LE TOTAL D'UNE FACTURE
    ' Donnée calculée depuis factureDetail
    ' ─────────────────────────────────────────────
    Public Function GetTotalFacture(idFacture As Integer) As Decimal
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT IFNULL(SUM(quantite * prix), 0) FROM factureDetail WHERE idFacture = @id"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", idFacture)
            Return CDec(cmd.ExecuteScalar())
        End Using
    End Function

    ' ─────────────────────────────────────────────
    ' CRÉER UNE FACTURE AVEC SES LIGNES
    ' ─────────────────────────────────────────────
    Public Function CreerFacture(idRdv As Integer, details As List(Of FactureDetail),
                             modePaiement As String) As Integer
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim transaction = conn.BeginTransaction()

            Try
                ' Créer la facture
                Dim cmdFacture As New MySqlCommand(
    "INSERT INTO facture (idRdv, modePaiement) VALUES (@idRdv, @mode)", conn, transaction)
                cmdFacture.Parameters.AddWithValue("@idRdv", If(idRdv = 0, DBNull.Value, CObj(idRdv)))
                cmdFacture.Parameters.AddWithValue("@mode", If(String.IsNullOrEmpty(modePaiement), "Espèces", modePaiement))
                cmdFacture.ExecuteNonQuery()

                ' Récupérer l'ID de la facture créée
                Dim idFacture = CInt(New MySqlCommand("SELECT LAST_INSERT_ID()", conn, transaction).ExecuteScalar())

                ' Ajouter chaque ligne de détail
                For Each d In details
                    Dim cmdDetail As New MySqlCommand(
                        "INSERT INTO factureDetail (idFacture, idPrestation, idProduit, quantite, prix)
                         VALUES (@idFacture, @idPrestation, @idProduit, @quantite, @prix)", conn, transaction)
                    cmdDetail.Parameters.AddWithValue("@idFacture", idFacture)
                    cmdDetail.Parameters.AddWithValue("@idPrestation", If(d.IdPrestation.HasValue, d.IdPrestation.Value, DBNull.Value))
                    cmdDetail.Parameters.AddWithValue("@idProduit", If(d.IdProduit.HasValue, d.IdProduit.Value, DBNull.Value))
                    cmdDetail.Parameters.AddWithValue("@quantite", d.Quantite)
                    cmdDetail.Parameters.AddWithValue("@prix", d.Prix)
                    cmdDetail.ExecuteNonQuery()
                Next

                transaction.Commit()
                Return idFacture

            Catch ex As Exception
                transaction.Rollback()
                Throw New Exception("Erreur lors de la création de la facture : " & ex.Message)
            End Try
        End Using
    End Function

    Public Sub SupprimerFacture(id As Integer)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            ' Les détails sont supprimés automatiquement grâce au ON DELETE CASCADE
            Dim cmd As New MySqlCommand("DELETE FROM facture WHERE idFacture = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function LireFacture(reader As MySqlDataReader) As Facture
        Dim f As New Facture()
        f.IdFacture = reader.GetInt32("idFacture")
        f.IdRdv = reader.GetInt32("idRdv")
        f.DateFacture = reader.GetDateTime("dateFacture")
        Return f
    End Function
    Public Function GetChiffreDuMois() As Decimal
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT COALESCE(SUM(fd.prix * fd.quantite), 0) 
           FROM factureDetail fd
           JOIN facture f ON fd.idFacture = f.idFacture
           WHERE MONTH(f.dateFacture) = MONTH(NOW())
           AND YEAR(f.dateFacture) = YEAR(NOW())"
            Using cmd As New MySqlClient.MySqlCommand(sql, conn)
                Return Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Public Function GetTopPrestations(nb As Integer) As List(Of KeyValuePair(Of String, Integer))
        Dim liste As New List(Of KeyValuePair(Of String, Integer))
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT p.nom, COUNT(*) as nb FROM factureDetail fd 
           JOIN prestation p ON fd.idPrestation = p.idPrestation
           JOIN facture f ON fd.idFacture = f.idFacture
           WHERE MONTH(f.dateFacture) = MONTH(NOW())
           AND YEAR(f.dateFacture) = YEAR(NOW())
           GROUP BY p.nom ORDER BY nb DESC LIMIT @nb"
            Using cmd As New MySqlClient.MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@nb", nb)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    liste.Add(New KeyValuePair(Of String, Integer)(
                        reader.GetString(0), reader.GetInt32(1)))
                End While
            End Using
        End Using
        Return liste
    End Function

    Public Function GetRevenusParMois(nbMois As Integer) As List(Of KeyValuePair(Of String, Decimal))
        Dim liste As New List(Of KeyValuePair(Of String, Decimal))
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmdLocale As New MySqlCommand("SET lc_time_names = 'fr_FR'", conn)
            cmdLocale.ExecuteNonQuery()
            Dim sql = "SELECT DATE_FORMAT(f.dateFacture, '%b') as mois,
           COALESCE(SUM(fd.prix * fd.quantite), 0) as total
           FROM facture f
           LEFT JOIN factureDetail fd ON fd.idFacture = f.idFacture
           WHERE f.dateFacture >= DATE_SUB(NOW(), INTERVAL @nb MONTH)
           GROUP BY YEAR(f.dateFacture), MONTH(f.dateFacture)
           ORDER BY f.dateFacture ASC"
            Using cmd As New MySqlClient.MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@nb", nbMois)
                Dim reader = cmd.ExecuteReader()
                While reader.Read()
                    liste.Add(New KeyValuePair(Of String, Decimal)(
                        reader.GetString(0), reader.GetDecimal(1)))
                End While
            End Using
        End Using
        Return liste
    End Function

End Class