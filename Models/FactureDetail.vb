Public Class FactureDetail
    Public Property IdDetail As Integer
    Public Property IdFacture As Integer
    Public Property IdProduit As Integer?      ' Nullable - peut être NULL
    Public Property IdPrestation As Integer?   ' Nullable - peut être NULL
    Public Property Quantite As Integer
    Public Property Prix As Decimal

    ' Propriété calculée - pas en base
    Public ReadOnly Property SousTotal As Decimal
        Get
            Return Quantite * Prix
        End Get
    End Property
End Class