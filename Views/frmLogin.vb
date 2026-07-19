' =====================================================
' FORMULAIRE DE CONNEXION
' =====================================================
Imports Guna.UI2.WinForms

Public Class frmLogin

    Private _ctrl As New UtilisateurController()

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Rosa Beauty — Connexion"
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Size = New Size(420, 500)
        Me.BackColor = ColorTranslator.FromHtml("#FFFAF9")
    End Sub

    ' ─────────────────────────────────────────────
    ' BOUTON CONNEXION
    ' ─────────────────────────────────────────────
    Private Sub btnConnecter_Click(sender As Object, e As EventArgs) Handles btnConnecter.Click
        ' Validation basique
        If String.IsNullOrWhiteSpace(txtNomUtilisateur.Text) OrElse
           String.IsNullOrWhiteSpace(txtMotDePasse.Text) Then
            MsgBox("Veuillez remplir tous les champs.", MsgBoxStyle.Exclamation, "Champ manquant")
            Return
        End If

        Try
            Dim u = _ctrl.Connecter(txtNomUtilisateur.Text.Trim(), txtMotDePasse.Text.Trim())

            If u IsNot Nothing Then
                ' Connexion réussie → ouvrir le form principal
                Dim frm As New Mainframe()
                frm.UtilisateurConnecte = u
                frm.Show()
                Me.Hide()
            Else
                MsgBox("Nom d'utilisateur ou mot de passe incorrect.", MsgBoxStyle.Critical, "Erreur")
                txtMotDePasse.Clear()
                txtNomUtilisateur.Focus()
            End If

        Catch ex As Exception
            MsgBox("Erreur de connexion : " & ex.Message, MsgBoxStyle.Critical, "Erreur")
        End Try
    End Sub

    ' Permettre de valider avec la touche Entrée
    Private Sub txtMotDePasse_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMotDePasse.KeyDown
        If e.KeyCode = Keys.Enter Then btnConnecter_Click(sender, e)
    End Sub

    Private Sub txtNomUtilisateur_TextChanged(sender As Object, e As EventArgs) Handles txtNomUtilisateur.TextChanged

    End Sub
End Class