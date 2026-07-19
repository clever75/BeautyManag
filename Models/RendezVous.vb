Public Class RendezVous
    Public Property IdRdv As Integer
    Public Property IdClient As Integer
    Public Property IdEmploye As Integer
    Public Property IdPrestation As Integer
    Public Property DateHeureDebut As DateTime
    Public Property DateHeureFin As DateTime
    Public Property Statut As String

    ' Propriété calculée - pas en base
    Public ReadOnly Property DureeMinutes As Integer
        Get
            Return CInt((DateHeureFin - DateHeureDebut).TotalMinutes)
        End Get
    End Property
End Class