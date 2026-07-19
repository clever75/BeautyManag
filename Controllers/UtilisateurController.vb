' =====================================================
' CONTROLLER UTILISATEUR
' Sert uniquement pour la connexion
' =====================================================
Imports MySql.Data.MySqlClient

Public Class UtilisateurController

    ' ─────────────────────────────────────────────
    ' VÉRIFIER LES IDENTIFIANTS DE CONNEXION
    ' Retourne l'utilisateur si OK, Nothing si NON
    ' ─────────────────────────────────────────────
    Public Function Connecter(nomUtilisateur As String, motDePasse As String) As Utilisateur
        Dim u As Utilisateur = Nothing

        Using conn = DBConnexion.GetConnexion()
            conn.Open()
            Dim sql = "SELECT * FROM utilisateur WHERE nomUtilisateur = @nom AND motDePasse = @mdp"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@nom", nomUtilisateur)
            cmd.Parameters.AddWithValue("@mdp", motDePasse)
            Dim reader = cmd.ExecuteReader()

            If reader.Read() Then
                u = New Utilisateur()
                u.IdUser = reader.GetInt32("idUser")
                u.Nom = reader.GetString("nom")
                u.Prenom = reader.GetString("prenom")
                u.NomUtilisateur = reader.GetString("nomUtilisateur")
            End If
        End Using

        Return u
    End Function

End Class