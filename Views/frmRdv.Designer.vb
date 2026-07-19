<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRdv
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges13 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges14 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges11 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges12 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges9 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges10 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges7 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges8 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges5 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges6 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges17 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges18 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges15 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges16 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        pnlHeaderForm = New Guna.UI2.WinForms.Guna2Panel()
        lblTitreForm = New Label()
        lblSousTitreForm = New Label()
        pnlCorps = New Panel()
        lblCliente = New Label()
        cboCliente = New Guna.UI2.WinForms.Guna2ComboBox()
        lblEmployeForm = New Label()
        cboEmployeForm = New Guna.UI2.WinForms.Guna2ComboBox()
        lblPrestationForm = New Label()
        cboPrestationForm = New Guna.UI2.WinForms.Guna2ComboBox()
        lblDateDebut = New Label()
        dtpDebut = New Guna.UI2.WinForms.Guna2DateTimePicker()
        Label1 = New Label()
        txtHeureFin = New Guna.UI2.WinForms.Guna2TextBox()
        lblDureeInfo = New Label()
        lblConflitAvertissement = New Label()
        lblStatut = New Label()
        cboStatutForm = New Guna.UI2.WinForms.Guna2ComboBox()
        pnlFooter = New Panel()
        btnAnnulerRdv = New Guna.UI2.WinForms.Guna2Button()
        btnEnregistrerRdv = New Guna.UI2.WinForms.Guna2Button()
        pnlHeaderForm.SuspendLayout()
        pnlCorps.SuspendLayout()
        pnlFooter.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlHeaderForm
        ' 
        pnlHeaderForm.Controls.Add(lblSousTitreForm)
        pnlHeaderForm.Controls.Add(lblTitreForm)
        pnlHeaderForm.CustomizableEdges = CustomizableEdges1
        pnlHeaderForm.Dock = DockStyle.Top
        pnlHeaderForm.FillColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        pnlHeaderForm.Location = New Point(0, 0)
        pnlHeaderForm.Name = "pnlHeaderForm"
        pnlHeaderForm.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        pnlHeaderForm.Size = New Size(442, 70)
        pnlHeaderForm.TabIndex = 0
        ' 
        ' lblTitreForm
        ' 
        lblTitreForm.AutoSize = True
        lblTitreForm.BackColor = Color.Transparent
        lblTitreForm.Font = New Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitreForm.ForeColor = Color.Transparent
        lblTitreForm.Location = New Point(20, 14)
        lblTitreForm.Name = "lblTitreForm"
        lblTitreForm.Size = New Size(247, 31)
        lblTitreForm.TabIndex = 0
        lblTitreForm.Text = "Nouveau rendez-vous"
        ' 
        ' lblSousTitreForm
        ' 
        lblSousTitreForm.AutoSize = True
        lblSousTitreForm.BackColor = Color.Transparent
        lblSousTitreForm.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblSousTitreForm.ForeColor = Color.FromArgb(CByte(200), CByte(160), CByte(176))
        lblSousTitreForm.Location = New Point(20, 44)
        lblSousTitreForm.Name = "lblSousTitreForm"
        lblSousTitreForm.Size = New Size(193, 20)
        lblSousTitreForm.TabIndex = 1
        lblSousTitreForm.Text = "Remplissez les informations"
        ' 
        ' pnlCorps
        ' 
        pnlCorps.Controls.Add(cboStatutForm)
        pnlCorps.Controls.Add(lblStatut)
        pnlCorps.Controls.Add(lblConflitAvertissement)
        pnlCorps.Controls.Add(lblDureeInfo)
        pnlCorps.Controls.Add(txtHeureFin)
        pnlCorps.Controls.Add(Label1)
        pnlCorps.Controls.Add(dtpDebut)
        pnlCorps.Controls.Add(lblDateDebut)
        pnlCorps.Controls.Add(cboPrestationForm)
        pnlCorps.Controls.Add(lblPrestationForm)
        pnlCorps.Controls.Add(cboEmployeForm)
        pnlCorps.Controls.Add(lblEmployeForm)
        pnlCorps.Controls.Add(cboCliente)
        pnlCorps.Controls.Add(lblCliente)
        pnlCorps.Dock = DockStyle.Fill
        pnlCorps.Location = New Point(0, 70)
        pnlCorps.Name = "pnlCorps"
        pnlCorps.Padding = New Padding(20, 14, 20, 0)
        pnlCorps.Size = New Size(442, 443)
        pnlCorps.TabIndex = 1
        ' 
        ' lblCliente
        ' 
        lblCliente.AutoSize = True
        lblCliente.BackColor = Color.Transparent
        lblCliente.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCliente.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        lblCliente.Location = New Point(0, 0)
        lblCliente.Name = "lblCliente"
        lblCliente.Size = New Size(57, 20)
        lblCliente.TabIndex = 2
        lblCliente.Text = "Client *"
        ' 
        ' cboCliente
        ' 
        cboCliente.BackColor = Color.Transparent
        cboCliente.CustomizableEdges = CustomizableEdges13
        cboCliente.DrawMode = DrawMode.OwnerDrawFixed
        cboCliente.DropDownStyle = ComboBoxStyle.DropDownList
        cboCliente.FocusedColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboCliente.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboCliente.Font = New Font("Segoe UI", 10F)
        cboCliente.ForeColor = Color.FromArgb(CByte(68), CByte(88), CByte(112))
        cboCliente.ItemHeight = 30
        cboCliente.Location = New Point(0, 20)
        cboCliente.Name = "cboCliente"
        cboCliente.ShadowDecoration.CustomizableEdges = CustomizableEdges14
        cboCliente.Size = New Size(415, 36)
        cboCliente.TabIndex = 3
        ' 
        ' lblEmployeForm
        ' 
        lblEmployeForm.AutoSize = True
        lblEmployeForm.BackColor = Color.Transparent
        lblEmployeForm.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblEmployeForm.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        lblEmployeForm.Location = New Point(0, 60)
        lblEmployeForm.Name = "lblEmployeForm"
        lblEmployeForm.Size = New Size(77, 20)
        lblEmployeForm.TabIndex = 4
        lblEmployeForm.Text = "Employé *"
        ' 
        ' cboEmployeForm
        ' 
        cboEmployeForm.BackColor = Color.Transparent
        cboEmployeForm.CustomizableEdges = CustomizableEdges11
        cboEmployeForm.DrawMode = DrawMode.OwnerDrawFixed
        cboEmployeForm.DropDownStyle = ComboBoxStyle.DropDownList
        cboEmployeForm.FocusedColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboEmployeForm.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboEmployeForm.Font = New Font("Segoe UI", 10F)
        cboEmployeForm.ForeColor = Color.FromArgb(CByte(68), CByte(88), CByte(112))
        cboEmployeForm.ItemHeight = 30
        cboEmployeForm.Location = New Point(0, 80)
        cboEmployeForm.Name = "cboEmployeForm"
        cboEmployeForm.ShadowDecoration.CustomizableEdges = CustomizableEdges12
        cboEmployeForm.Size = New Size(415, 36)
        cboEmployeForm.TabIndex = 5
        ' 
        ' lblPrestationForm
        ' 
        lblPrestationForm.AutoSize = True
        lblPrestationForm.BackColor = Color.Transparent
        lblPrestationForm.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblPrestationForm.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        lblPrestationForm.Location = New Point(0, 120)
        lblPrestationForm.Name = "lblPrestationForm"
        lblPrestationForm.Size = New Size(85, 20)
        lblPrestationForm.TabIndex = 6
        lblPrestationForm.Text = "Prestation *"
        ' 
        ' cboPrestationForm
        ' 
        cboPrestationForm.BackColor = Color.Transparent
        cboPrestationForm.CustomizableEdges = CustomizableEdges9
        cboPrestationForm.DrawMode = DrawMode.OwnerDrawFixed
        cboPrestationForm.DropDownStyle = ComboBoxStyle.DropDownList
        cboPrestationForm.FocusedColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboPrestationForm.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboPrestationForm.Font = New Font("Segoe UI", 10F)
        cboPrestationForm.ForeColor = Color.FromArgb(CByte(68), CByte(88), CByte(112))
        cboPrestationForm.ItemHeight = 30
        cboPrestationForm.Location = New Point(0, 140)
        cboPrestationForm.Name = "cboPrestationForm"
        cboPrestationForm.ShadowDecoration.CustomizableEdges = CustomizableEdges10
        cboPrestationForm.Size = New Size(415, 36)
        cboPrestationForm.TabIndex = 7
        ' 
        ' lblDateDebut
        ' 
        lblDateDebut.AutoSize = True
        lblDateDebut.BackColor = Color.Transparent
        lblDateDebut.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblDateDebut.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        lblDateDebut.Location = New Point(0, 180)
        lblDateDebut.Name = "lblDateDebut"
        lblDateDebut.Size = New Size(173, 20)
        lblDateDebut.TabIndex = 8
        lblDateDebut.Text = "Date et heure de début *"
        ' 
        ' dtpDebut
        ' 
        dtpDebut.BackColor = Color.White
        dtpDebut.BorderRadius = 3
        dtpDebut.Checked = True
        dtpDebut.CustomFormat = "dd/MM/yyyy HH:mm"
        dtpDebut.CustomizableEdges = CustomizableEdges7
        dtpDebut.FillColor = Color.White
        dtpDebut.Font = New Font("Segoe UI", 9F)
        dtpDebut.Format = DateTimePickerFormat.Long
        dtpDebut.Location = New Point(0, 200)
        dtpDebut.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        dtpDebut.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        dtpDebut.Name = "dtpDebut"
        dtpDebut.ShadowDecoration.CustomizableEdges = CustomizableEdges8
        dtpDebut.Size = New Size(200, 30)
        dtpDebut.TabIndex = 9
        dtpDebut.Value = New Date(2026, 5, 15, 13, 12, 3, 980)
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.BackColor = Color.Transparent
        Label1.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        Label1.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        Label1.Location = New Point(215, 180)
        Label1.Name = "Label1"
        Label1.Size = New Size(98, 20)
        Label1.TabIndex = 10
        Label1.Text = "Heure de fin :"
        ' 
        ' txtHeureFin
        ' 
        txtHeureFin.BackColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtHeureFin.CustomizableEdges = CustomizableEdges5
        txtHeureFin.DefaultText = ""
        txtHeureFin.DisabledState.BorderColor = Color.FromArgb(CByte(208), CByte(208), CByte(208))
        txtHeureFin.DisabledState.FillColor = Color.FromArgb(CByte(226), CByte(226), CByte(226))
        txtHeureFin.DisabledState.ForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtHeureFin.DisabledState.PlaceholderForeColor = Color.FromArgb(CByte(138), CByte(138), CByte(138))
        txtHeureFin.FillColor = Color.FromArgb(CByte(240), CByte(220), CByte(226))
        txtHeureFin.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtHeureFin.Font = New Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        txtHeureFin.HoverState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        txtHeureFin.Location = New Point(215, 200)
        txtHeureFin.Margin = New Padding(4, 5, 4, 5)
        txtHeureFin.Name = "txtHeureFin"
        txtHeureFin.PlaceholderText = ""
        txtHeureFin.SelectedText = ""
        txtHeureFin.ShadowDecoration.CustomizableEdges = CustomizableEdges6
        txtHeureFin.Size = New Size(200, 30)
        txtHeureFin.TabIndex = 11
        ' 
        ' lblDureeInfo
        ' 
        lblDureeInfo.AutoSize = True
        lblDureeInfo.BackColor = Color.Transparent
        lblDureeInfo.Font = New Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblDureeInfo.ForeColor = Color.FromArgb(CByte(196), CByte(90), CByte(126))
        lblDureeInfo.Location = New Point(0, 236)
        lblDureeInfo.Name = "lblDureeInfo"
        lblDureeInfo.Size = New Size(161, 17)
        lblDureeInfo.TabIndex = 12
        lblDureeInfo.Text = "Sélectionnz une prestation"
        ' 
        ' lblConflitAvertissement
        ' 
        lblConflitAvertissement.BackColor = Color.FromArgb(CByte(254), CByte(245), CByte(231))
        lblConflitAvertissement.Font = New Font("Segoe UI", 7.8F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblConflitAvertissement.ForeColor = Color.FromArgb(CByte(133), CByte(79), CByte(11))
        lblConflitAvertissement.Location = New Point(0, 258)
        lblConflitAvertissement.Name = "lblConflitAvertissement"
        lblConflitAvertissement.Size = New Size(415, 32)
        lblConflitAvertissement.TabIndex = 13
        lblConflitAvertissement.Visible = False
        ' 
        ' lblStatut
        ' 
        lblStatut.AutoSize = True
        lblStatut.Location = New Point(0, 298)
        lblStatut.Name = "lblStatut"
        lblStatut.Size = New Size(48, 20)
        lblStatut.TabIndex = 14
        lblStatut.Text = "Statut"
        ' 
        ' cboStatutForm
        ' 
        cboStatutForm.BackColor = Color.Transparent
        cboStatutForm.CustomizableEdges = CustomizableEdges3
        cboStatutForm.DrawMode = DrawMode.OwnerDrawFixed
        cboStatutForm.DropDownStyle = ComboBoxStyle.DropDownList
        cboStatutForm.FocusedColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboStatutForm.FocusedState.BorderColor = Color.FromArgb(CByte(94), CByte(148), CByte(255))
        cboStatutForm.Font = New Font("Segoe UI", 10F)
        cboStatutForm.ForeColor = Color.FromArgb(CByte(68), CByte(88), CByte(112))
        cboStatutForm.ItemHeight = 30
        cboStatutForm.Location = New Point(0, 318)
        cboStatutForm.Name = "cboStatutForm"
        cboStatutForm.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        cboStatutForm.Size = New Size(415, 36)
        cboStatutForm.TabIndex = 15
        ' 
        ' pnlFooter
        ' 
        pnlFooter.BackColor = Color.White
        pnlFooter.Controls.Add(btnEnregistrerRdv)
        pnlFooter.Controls.Add(btnAnnulerRdv)
        pnlFooter.Dock = DockStyle.Bottom
        pnlFooter.Location = New Point(0, 449)
        pnlFooter.Name = "pnlFooter"
        pnlFooter.Size = New Size(442, 64)
        pnlFooter.TabIndex = 16
        ' 
        ' btnAnnulerRdv
        ' 
        btnAnnulerRdv.BorderColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        btnAnnulerRdv.BorderRadius = 8
        btnAnnulerRdv.BorderThickness = 1
        btnAnnulerRdv.CustomizableEdges = CustomizableEdges17
        btnAnnulerRdv.DisabledState.BorderColor = Color.DarkGray
        btnAnnulerRdv.DisabledState.CustomBorderColor = Color.DarkGray
        btnAnnulerRdv.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnAnnulerRdv.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnAnnulerRdv.FillColor = Color.White
        btnAnnulerRdv.Font = New Font("Segoe UI", 9F)
        btnAnnulerRdv.ForeColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        btnAnnulerRdv.Location = New Point(20, 13)
        btnAnnulerRdv.Name = "btnAnnulerRdv"
        btnAnnulerRdv.ShadowDecoration.CustomizableEdges = CustomizableEdges18
        btnAnnulerRdv.Size = New Size(185, 38)
        btnAnnulerRdv.TabIndex = 0
        btnAnnulerRdv.Text = "Annuler"
        ' 
        ' btnEnregistrerRdv
        ' 
        btnEnregistrerRdv.BorderRadius = 8
        btnEnregistrerRdv.CustomizableEdges = CustomizableEdges15
        btnEnregistrerRdv.DisabledState.BorderColor = Color.DarkGray
        btnEnregistrerRdv.DisabledState.CustomBorderColor = Color.DarkGray
        btnEnregistrerRdv.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        btnEnregistrerRdv.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        btnEnregistrerRdv.FillColor = Color.FromArgb(CByte(61), CByte(26), CByte(36))
        btnEnregistrerRdv.Font = New Font("Segoe UI", 9F)
        btnEnregistrerRdv.ForeColor = Color.White
        btnEnregistrerRdv.Location = New Point(215, 13)
        btnEnregistrerRdv.Name = "btnEnregistrerRdv"
        btnEnregistrerRdv.ShadowDecoration.CustomizableEdges = CustomizableEdges16
        btnEnregistrerRdv.Size = New Size(185, 38)
        btnEnregistrerRdv.TabIndex = 1
        btnEnregistrerRdv.Text = "Enregistrer"
        ' 
        ' frmRdv
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(255), CByte(250), CByte(249))
        ClientSize = New Size(442, 513)
        Controls.Add(pnlFooter)
        Controls.Add(pnlCorps)
        Controls.Add(pnlHeaderForm)
        FormBorderStyle = FormBorderStyle.FixedDialog
        MaximizeBox = False
        MinimizeBox = False
        MinimumSize = New Size(460, 560)
        Name = "frmRdv"
        StartPosition = FormStartPosition.CenterParent
        Text = "Rendez-vous"
        pnlHeaderForm.ResumeLayout(False)
        pnlHeaderForm.PerformLayout()
        pnlCorps.ResumeLayout(False)
        pnlCorps.PerformLayout()
        pnlFooter.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlHeaderForm As Guna.UI2.WinForms.Guna2Panel
    Friend WithEvents lblTitreForm As Label
    Friend WithEvents lblSousTitreForm As Label
    Friend WithEvents pnlCorps As Panel
    Friend WithEvents cboCliente As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblCliente As Label
    Friend WithEvents lblEmployeForm As Label
    Friend WithEvents cboEmployeForm As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblPrestationForm As Label
    Friend WithEvents lblDateDebut As Label
    Friend WithEvents cboPrestationForm As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents dtpDebut As Guna.UI2.WinForms.Guna2DateTimePicker
    Friend WithEvents txtHeureFin As Guna.UI2.WinForms.Guna2TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblDureeInfo As Label
    Friend WithEvents cboStatutForm As Guna.UI2.WinForms.Guna2ComboBox
    Friend WithEvents lblStatut As Label
    Friend WithEvents lblConflitAvertissement As Label
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents btnEnregistrerRdv As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents btnAnnulerRdv As Guna.UI2.WinForms.Guna2Button
End Class
