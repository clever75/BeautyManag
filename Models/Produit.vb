Public Class Produit
    Public Property IdProduit As Integer
    Public Property Nom As String
    Public Property Categorie As String
    Public Property Prix As Decimal
    Public Property StockActuel As Integer
    Public Property SeuilAlerte As Integer
    Public Property Description As String
    Public Property Actif As Boolean

    ' Propriété calculée - pas en base
    Public ReadOnly Property EnAlerte As Boolean
        Get
            Return StockActuel <= SeuilAlerte AndAlso StockActuel > 0
        End Get
    End Property

    ' Propriété calculée - pas en base
    Public ReadOnly Property EnRupture As Boolean
        Get
            Return StockActuel = 0
        End Get
    End Property
End Class