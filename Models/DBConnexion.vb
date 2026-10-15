' =====================================================
' CONNEXION À LA BASE DE DONNÉES
' Les paramètres sont lus dans config.properties, à côté de l'exécutable.
' Sans ce fichier, on retombe sur la configuration de développement (XAMPP).
' =====================================================
Imports System.IO
Imports MySql.Data.MySqlClient

Public Class DBConnexion

    Private Const FichierConfig As String = "config.properties"

    Private Shared config As Dictionary(Of String, String) = Nothing
    Private Shared ReadOnly verrou As New Object()

    ' Lit la configuration une seule fois, puis la garde en mémoire
    Private Shared Function ChargerConfig() As Dictionary(Of String, String)
        SyncLock verrou
            If config IsNot Nothing Then Return config

            ' Valeurs par défaut = configuration de développement actuelle
            Dim p As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase) From {
                {"db.host", "localhost"},
                {"db.port", "3306"},
                {"db.name", "salonbeaute"},
                {"db.user", "root"},
                {"db.password", ""}
            }

            ' Dossiers où on cherche le fichier : à côté de l'exécutable, puis dossier de travail
            Dim trouve As Boolean = False
            For Each dossier As String In New String() {AppContext.BaseDirectory, Environment.CurrentDirectory}
                Dim chemin As String = Path.Combine(dossier, FichierConfig)
                If File.Exists(chemin) Then
                    Try
                        For Each ligne As String In File.ReadAllLines(chemin, System.Text.Encoding.UTF8)
                            Dim l As String = ligne.Trim()
                            If l.Length = 0 OrElse l.StartsWith("#") OrElse l.StartsWith("!") Then Continue For
                            Dim i As Integer = l.IndexOf("="c)
                            If i > 0 Then
                                Dim cle As String = l.Substring(0, i).Trim()
                                Dim valeur As String = l.Substring(i + 1)
                                ' pas de Trim sur le mot de passe
                                If Not String.Equals(cle, "db.password", StringComparison.OrdinalIgnoreCase) Then
                                    valeur = valeur.Trim()
                                End If
                                p(cle) = valeur
                            End If
                        Next
                        Debug.WriteLine("Configuration lue : " & chemin)
                        trouve = True
                        Exit For
                    Catch ex As Exception
                        Debug.WriteLine("Impossible de lire " & chemin & " : " & ex.Message)
                    End Try
                End If
            Next

            If Not trouve Then
                Debug.WriteLine("Aucun " & FichierConfig & " trouvé : valeurs par défaut (développement)")
            End If
            Debug.WriteLine("Base : " & p("db.host") & ":" & p("db.port") & "/" & p("db.name") &
                            " (utilisateur " & p("db.user") & ")")

            config = p
            Return config
        End SyncLock
    End Function

    Public Shared Function GetConnexion() As MySqlConnection
        Dim cfg As Dictionary(Of String, String) = ChargerConfig()

        Dim port As UInteger = 3306
        UInteger.TryParse(cfg("db.port"), port)

        ' Le constructeur de chaîne de connexion protège contre les caractères spéciaux
        Dim b As New MySqlConnectionStringBuilder()
        b.Server = cfg("db.host")
        b.Port = port
        b.Database = cfg("db.name")
        b.UserID = cfg("db.user")
        b.Password = cfg("db.password")
        b.CharacterSet = "utf8mb4"

        Return New MySqlConnection(b.ConnectionString)
    End Function

End Class