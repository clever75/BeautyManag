' =====================================================
' CONNEXION À LA BASE DE DONNÉES
' =====================================================
Imports MySql.Data.MySqlClient

Public Class DBConnexion

    Private Shared serveur As String = "localhost"
    Private Shared baseDeDonnees As String = "salonbeaute"
    Private Shared utilisateur As String = "root"
    Private Shared motDePasse As String = ""

    Public Shared Function GetConnexion() As MySqlConnection
        Dim chaine As String = $"Server={serveur};Database={baseDeDonnees};Uid={utilisateur};Pwd={motDePasse};CharSet=utf8mb4;"
        Return New MySqlConnection(chaine)
    End Function

End Class