' =====================================================
' CONTROLLER CLIENT
' Opérations : Lire, Ajouter, Modifier, Supprimer
' =====================================================
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClientController

    ' ─────────────────────────────────────────────
    ' LIRE TOUS LES CLIENTS
    ' ─────────────────────────────────────────────
    Public Function GetAllClients() As List(Of Client)
        Dim liste As New List(Of Client)

        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT * FROM client ORDER BY idClient DESC", conn)
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim c As New Client()
                c.IdClient = reader.GetInt32("idClient")
                c.Nom = reader.GetString("nom")
                c.Prenom = reader.GetString("prenom")
                c.Telephone = If(reader.IsDBNull(reader.GetOrdinal("telephone")), "", reader.GetString("telephone"))
                c.Genre = If(reader.IsDBNull(reader.GetOrdinal("genre")), "", reader.GetString("genre"))
                c.Email = If(reader.IsDBNull(reader.GetOrdinal("email")), "", reader.GetString("email"))
                liste.Add(c)
            End While
        End Using

        Return liste
    End Function
    Public Function GetNbRendezVous(idClient As Integer) As Integer
        Dim sql = "SELECT COUNT(*) FROM rendez_vous WHERE idClient = ?"
        ' ... exécuter et retourner le résultat
    End Function
    ' ─────────────────────────────────────────────
    ' LIRE UN CLIENT PAR ID
    ' ─────────────────────────────────────────────


    Public Function GetClientById(id As Integer) As Client
        Try
            Using conn = DBConnexion.GetConnexion()
                conn.Open()
                Dim sql = "SELECT * FROM client WHERE idClient = @id"
                Using cmd As New MySqlClient.MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", id)
                    Dim reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim c As New Client()
                        c.IdClient = reader.GetInt32("idClient")
                        c.Nom = reader.GetString("nom")
                        c.Prenom = reader.GetString("prenom")
                        c.Telephone = If(reader.IsDBNull(reader.GetOrdinal("telephone")), "", reader.GetString("telephone"))
                        c.Email = If(reader.IsDBNull(reader.GetOrdinal("email")), "", reader.GetString("email"))
                        Return c
                    End If
                End Using
            End Using
        Catch
        End Try
        Return Nothing
    End Function

    ' ─────────────────────────────────────────────
    ' RECHERCHER PAR NOM OU PRENOM
    ' ─────────────────────────────────────────────
    Public Function Rechercher(texte As String) As List(Of Client)
        Dim liste As New List(Of Client)

        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT * FROM client WHERE nom LIKE @texte 
           OR prenom LIKE @texte 
           OR CONCAT(prenom, ' ', nom) LIKE @texte
           OR CONCAT(nom, ' ', prenom) LIKE @texte
           ORDER BY nom"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@texte", "%" & texte & "%")
            Dim reader = cmd.ExecuteReader()

            While reader.Read()
                Dim c As New Client()
                c.IdClient = reader.GetInt32("idClient")
                c.Nom = reader.GetString("nom")
                c.Prenom = reader.GetString("prenom")
                c.Telephone = If(reader.IsDBNull(reader.GetOrdinal("telephone")), "", reader.GetString("telephone"))
                c.Genre = If(reader.IsDBNull(reader.GetOrdinal("genre")), "", reader.GetString("genre"))
                c.Email = If(reader.IsDBNull(reader.GetOrdinal("email")), "", reader.GetString("email"))
                liste.Add(c)
            End While
        End Using

        Return liste
    End Function

    ' ─────────────────────────────────────────────
    ' AJOUTER UN CLIENT
    ' ─────────────────────────────────────────────
    Public Sub AjouterClient(c As Client)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "INSERT INTO client (nom, prenom, telephone, genre, email)
                       VALUES (@nom, @prenom, @telephone, @genre, @email)"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@nom", c.Nom)
            cmd.Parameters.AddWithValue("@prenom", c.Prenom)
            cmd.Parameters.AddWithValue("@telephone", If(c.Telephone = "", DBNull.Value, c.Telephone))
            cmd.Parameters.AddWithValue("@genre", If(c.Genre = "", DBNull.Value, c.Genre))
            cmd.Parameters.AddWithValue("@email", If(c.Email = "", DBNull.Value, c.Email))
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' ─────────────────────────────────────────────
    ' MODIFIER UN CLIENT
    ' ─────────────────────────────────────────────
    Public Sub ModifierClient(c As Client)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "UPDATE client SET nom=@nom, prenom=@prenom, telephone=@telephone,
                       genre=@genre, email=@email WHERE idClient=@id"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@nom", c.Nom)
            cmd.Parameters.AddWithValue("@prenom", c.Prenom)
            cmd.Parameters.AddWithValue("@telephone", If(c.Telephone = "", DBNull.Value, c.Telephone))
            cmd.Parameters.AddWithValue("@genre", If(c.Genre = "", DBNull.Value, c.Genre))
            cmd.Parameters.AddWithValue("@email", If(c.Email = "", DBNull.Value, c.Email))
            cmd.Parameters.AddWithValue("@id", c.IdClient)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' ─────────────────────────────────────────────
    ' SUPPRIMER UN CLIENT
    ' ─────────────────────────────────────────────
    Public Sub SupprimerClient(id As Integer)
        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim cmd As New MySqlCommand("DELETE FROM client WHERE idClient = @id", conn)
            cmd.Parameters.AddWithValue("@id", id)
            cmd.ExecuteNonQuery()
        End Using
    End Sub
    Public Function TelephoneExiste(telephone As String, idExclure As Integer) As Boolean
        Try
            Using conn = DBConnexion.GetConnexion()
                conn.Open()


                Dim sql = "SELECT COUNT(*) FROM client WHERE telephone = @tel AND idClient <> @id"
                Using cmd As New MySqlClient.MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@tel", telephone)
                    cmd.Parameters.AddWithValue("@id", idExclure)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

End Class