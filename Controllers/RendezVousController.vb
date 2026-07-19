' =====================================================
' CONTROLLER RENDEZ-VOUS
' =====================================================
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class RendezVousController

    Public Function GetRdvByDate(date_ As Date) As List(Of RendezVous)
        Dim liste As New List(Of RendezVous)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT * FROM rendezVous WHERE DATE(dateHeureDebut) = @date ORDER BY dateHeureDebut"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@date", date_.ToString("yyyy-MM-dd"))
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                liste.Add(LireRdv(reader))
            End While
        End Using
        Return liste
    End Function
    Public Function GetRdvNonFactures() As List(Of RendezVous)
        Dim liste As New List(Of RendezVous)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT r.* FROM rendezVous r
                   LEFT JOIN facture f ON f.idRdv = r.idRdv
                   WHERE (r.statut = 'Terminé' OR r.statut = 'Confirmé')
                   AND f.idFacture IS NULL
                   ORDER BY r.dateHeureDebut DESC"
            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                Dim rdv As New RendezVous()
                rdv.IdRdv = reader.GetInt32("idRdv")
                rdv.IdClient = reader.GetInt32("idClient")
                rdv.IdEmploye = reader.GetInt32("idEmploye")
                rdv.IdPrestation = reader.GetInt32("idPrestation")
                rdv.DateHeureDebut = reader.GetDateTime("dateHeureDebut")
                rdv.DateHeureFin = reader.GetDateTime("dateHeureFin")
                rdv.Statut = reader.GetString("statut")
                liste.Add(rdv)
            End While
        End Using
        Return liste
    End Function
    Public Function GetRdvByEmployeAndDate(idEmploye As Integer, date_ As Date) As List(Of RendezVous)
        Dim liste As New List(Of RendezVous)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT * FROM rendezVous WHERE idEmploye = @idEmploye
                       AND DATE(dateHeureDebut) = @date ORDER BY dateHeureDebut"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@idEmploye", idEmploye)
            cmd.Parameters.AddWithValue("@date", date_.ToString("yyyy-MM-dd"))
            Dim reader = cmd.ExecuteReader()
            While reader.Read()
                liste.Add(LireRdv(reader))
            End While
        End Using
        Return liste
    End Function

    Public Function GetRdvById(id As Integer) As RendezVous
        Dim rdv As RendezVous = Nothing
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM rendezVous WHERE idRdv = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            Dim reader = cmd.ExecuteReader()
            If reader.Read() Then rdv = LireRdv(reader)
        End Using
        Return rdv
    End Function

    ' ─────────────────────────────────────────────
    ' VÉRIFIER CONFLIT D'HORAIRE
    ' Un employé ne peut pas avoir 2 RDV en même temps
    ' ─────────────────────────────────────────────
    Public Function VerifierConflit(idEmploye As Integer, debut As DateTime, fin As DateTime,
                                    Optional idRdvExclure As Integer = 0) As Boolean
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT COUNT(*) FROM rendezVous
                       WHERE idEmploye = @idEmploye
                       AND idRdv <> @idRdvExclure
                       AND statut <> 'Annulé'
                       AND dateHeureDebut < @fin
                       AND dateHeureFin > @debut"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@idEmploye", idEmploye)
            cmd.Parameters.AddWithValue("@idRdvExclure", idRdvExclure)
            cmd.Parameters.AddWithValue("@debut", debut)
            cmd.Parameters.AddWithValue("@fin", fin)
            Dim count = CInt(cmd.ExecuteScalar())
            Return count > 0  ' True = il y a un conflit
        End Using
    End Function

    Public Sub AjouterRdv(rdv As RendezVous)
        ' Vérifier conflit avant d'ajouter
        If VerifierConflit(rdv.IdEmploye, rdv.DateHeureDebut, rdv.DateHeureFin) Then
            Throw New Exception("Cet employé a déjà un rendez-vous sur ce créneau.")
        End If

        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "INSERT INTO rendezVous (idClient, idEmploye, idPrestation, dateHeureDebut, dateHeureFin, statut)
                       VALUES (@idClient, @idEmploye, @idPrestation, @debut, @fin, @statut)"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@idClient", rdv.IdClient)
            cmd.Parameters.AddWithValue("@idEmploye", rdv.IdEmploye)
            cmd.Parameters.AddWithValue("@idPrestation", rdv.IdPrestation)
            cmd.Parameters.AddWithValue("@debut", rdv.DateHeureDebut)
            cmd.Parameters.AddWithValue("@fin", rdv.DateHeureFin)
            cmd.Parameters.AddWithValue("@statut", rdv.Statut)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub ModifierRdv(rdv As RendezVous)
        ' Vérifier conflit en excluant le RDV actuel
        If VerifierConflit(rdv.IdEmploye, rdv.DateHeureDebut, rdv.DateHeureFin, rdv.IdRdv) Then
            Throw New Exception("Cet employé a déjà un rendez-vous sur ce créneau.")
        End If

        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "UPDATE rendezVous SET idClient=@idClient, idEmploye=@idEmploye,
                       idPrestation=@idPrestation, dateHeureDebut=@debut,
                       dateHeureFin=@fin, statut=@statut WHERE idRdv=@id"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@idClient", rdv.IdClient)
            cmd.Parameters.AddWithValue("@idEmploye", rdv.IdEmploye)
            cmd.Parameters.AddWithValue("@idPrestation", rdv.IdPrestation)
            cmd.Parameters.AddWithValue("@debut", rdv.DateHeureDebut)
            cmd.Parameters.AddWithValue("@fin", rdv.DateHeureFin)
            cmd.Parameters.AddWithValue("@statut", rdv.Statut)
            cmd.Parameters.AddWithValue("@id", rdv.IdRdv)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub SupprimerRdv(id As Integer)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()

            ' Vérifier si le RDV est lié à une facture
            Dim cmdCheck As New MySqlCommand(
            "SELECT COUNT(*) FROM facture WHERE idRdv = @id", conn)
            cmdCheck.Parameters.AddWithValue("@id", id)
            Dim nbFactures = CInt(cmdCheck.ExecuteScalar())

            If nbFactures > 0 Then
                Throw New Exception("Ce rendez-vous est lié à une facture et ne peut pas être supprimé.")
            End If

            Dim cmd As New MySqlCommand("DELETE FROM rendezVous WHERE idRdv = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function LireRdv(reader As MySqlDataReader) As RendezVous
        Dim rdv As New RendezVous()
        rdv.IdRdv = reader.GetInt32("idRdv")
        rdv.IdClient = reader.GetInt32("idClient")
        rdv.IdEmploye = reader.GetInt32("idEmploye")
        rdv.IdPrestation = reader.GetInt32("idPrestation")
        rdv.DateHeureDebut = reader.GetDateTime("dateHeureDebut")
        rdv.DateHeureFin = reader.GetDateTime("dateHeureFin")
        rdv.Statut = If(reader.IsDBNull(reader.GetOrdinal("statut")), "En attente",
                    reader.GetString("statut"))
        Return rdv
    End Function
    Public Function GetRdvDuJour() As List(Of RendezVous)
        Dim liste As New List(Of RendezVous)
        Try
            Using conn = DBConnexion.GetConnexion()
                conn.Open()
                Dim sql = "SELECT * FROM rendezVous WHERE DATE(dateHeureDebut) = CURDATE() ORDER BY dateHeureDebut ASC"
                Using cmd As New MySqlCommand(sql, conn)
                    Dim reader = cmd.ExecuteReader()
                    While reader.Read()
                        liste.Add(LireRdv(reader))
                    End While
                End Using
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Erreur GetRdvDuJour : " & ex.Message)
            ' Liste vide retournée — le tableau de bord affichera 0 RDV
        End Try
        Return liste
    End Function
End Class