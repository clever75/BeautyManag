' ══════════════════════════════════════════════════════════════
'  MODULE : DatabaseConfig
'
'  RÔLE : instancier tous les controllers une seule fois
'         au démarrage. La connexion est gérée par DBConnexion.
'
'  UTILISATION :
'    → Dans Form1_Load        : DatabaseConfig.Initialiser()
'    → N'importe où ensuite   : DatabaseConfig.ClientCtrl.GetAllClients()
'                               DatabaseConfig.RdvCtrl.AjouterRendezVous(rdv)
'                               etc.
' ══════════════════════════════════════════════════════════════
Public Module DatabaseConfig

    Public ClientCtrl As ClientController
    Public EmployeCtrl As EmployeController
    Public RdvCtrl As RendezVousController
    Public PrestationCtrl As PrestationController
    Public ProduitCtrl As ProduitController
    Public FactureCtrl As FactureController

    ''' <summary>
    ''' Appelle cette méthode UNE SEULE FOIS dans Form1_Load.
    ''' </summary>
    Public Sub Initialiser()
        ClientCtrl = New ClientController()
        EmployeCtrl = New EmployeController()
        RdvCtrl = New RendezVousController()
        PrestationCtrl = New PrestationController()
        ProduitCtrl = New ProduitController()
        FactureCtrl = New FactureController()
    End Sub

End Module