<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mdiPABEO
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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


    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mdiPABEO))
        ToolTip = New ToolTip(components)
        pnlSideNav = New Panel()
        btnStation = New Button()
        btnEmployee = New Button()
        btnSystemConfig = New LinkLabel()
        btnOperator = New Button()
        btnMachinery = New Button()
        btnRequests = New Button()
        btnServices = New Button()
        btnFarmers = New Button()
        lblTitle = New Label()
        pbLogo = New PictureBox()
        pnlForms = New Panel()
        pnlHeader = New Panel()
        lblHeader = New Label()
        pnlSideNav.SuspendLayout()
        CType(pbLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlHeader.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlSideNav
        ' 
        pnlSideNav.BackColor = Color.DarkGreen
        pnlSideNav.Controls.Add(btnStation)
        pnlSideNav.Controls.Add(btnEmployee)
        pnlSideNav.Controls.Add(btnSystemConfig)
        pnlSideNav.Controls.Add(btnOperator)
        pnlSideNav.Controls.Add(btnMachinery)
        pnlSideNav.Controls.Add(btnRequests)
        pnlSideNav.Controls.Add(btnServices)
        pnlSideNav.Controls.Add(btnFarmers)
        pnlSideNav.Controls.Add(lblTitle)
        pnlSideNav.Controls.Add(pbLogo)
        pnlSideNav.Location = New Point(1, 0)
        pnlSideNav.Name = "pnlSideNav"
        pnlSideNav.Size = New Size(267, 1059)
        pnlSideNav.TabIndex = 9
        ' 
        ' btnStation
        ' 
        btnStation.BackColor = Color.Transparent
        btnStation.FlatAppearance.BorderColor = Color.White
        btnStation.FlatAppearance.BorderSize = 0
        btnStation.FlatStyle = FlatStyle.Flat
        btnStation.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnStation.ForeColor = Color.White
        btnStation.Image = My.Resources.Resources.icStation
        btnStation.ImageAlign = ContentAlignment.MiddleLeft
        btnStation.Location = New Point(9, 591)
        btnStation.Name = "btnStation"
        btnStation.Size = New Size(250, 60)
        btnStation.TabIndex = 20
        btnStation.Text = "Station"
        btnStation.UseVisualStyleBackColor = False
        ' 
        ' btnEmployee
        ' 
        btnEmployee.BackColor = Color.Transparent
        btnEmployee.FlatAppearance.BorderColor = Color.White
        btnEmployee.FlatAppearance.BorderSize = 0
        btnEmployee.FlatStyle = FlatStyle.Flat
        btnEmployee.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnEmployee.ForeColor = Color.White
        btnEmployee.Image = My.Resources.Resources.icEmployee
        btnEmployee.ImageAlign = ContentAlignment.MiddleLeft
        btnEmployee.Location = New Point(9, 525)
        btnEmployee.Name = "btnEmployee"
        btnEmployee.Size = New Size(250, 60)
        btnEmployee.TabIndex = 19
        btnEmployee.Text = "Employee"
        btnEmployee.UseVisualStyleBackColor = False
        ' 
        ' btnSystemConfig
        ' 
        btnSystemConfig.AutoSize = True
        btnSystemConfig.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSystemConfig.LinkBehavior = LinkBehavior.NeverUnderline
        btnSystemConfig.LinkColor = Color.White
        btnSystemConfig.Location = New Point(70, 1011)
        btnSystemConfig.Name = "btnSystemConfig"
        btnSystemConfig.Size = New Size(189, 21)
        btnSystemConfig.TabIndex = 18
        btnSystemConfig.TabStop = True
        btnSystemConfig.Text = "System Configuration ->"
        ' 
        ' btnOperator
        ' 
        btnOperator.BackColor = Color.Transparent
        btnOperator.FlatAppearance.BorderColor = Color.White
        btnOperator.FlatAppearance.BorderSize = 0
        btnOperator.FlatStyle = FlatStyle.Flat
        btnOperator.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnOperator.ForeColor = Color.White
        btnOperator.Image = My.Resources.Resources.icOperators
        btnOperator.ImageAlign = ContentAlignment.MiddleLeft
        btnOperator.Location = New Point(9, 459)
        btnOperator.Name = "btnOperator"
        btnOperator.Size = New Size(250, 60)
        btnOperator.TabIndex = 16
        btnOperator.Text = "Operator"
        btnOperator.UseVisualStyleBackColor = False
        ' 
        ' btnMachinery
        ' 
        btnMachinery.BackColor = Color.Transparent
        btnMachinery.FlatAppearance.BorderColor = Color.White
        btnMachinery.FlatAppearance.BorderSize = 0
        btnMachinery.FlatStyle = FlatStyle.Flat
        btnMachinery.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnMachinery.ForeColor = Color.White
        btnMachinery.Image = My.Resources.Resources.icMachinery
        btnMachinery.ImageAlign = ContentAlignment.MiddleLeft
        btnMachinery.Location = New Point(9, 393)
        btnMachinery.Name = "btnMachinery"
        btnMachinery.Size = New Size(250, 60)
        btnMachinery.TabIndex = 15
        btnMachinery.Text = "Machinery"
        btnMachinery.UseVisualStyleBackColor = False
        ' 
        ' btnRequests
        ' 
        btnRequests.BackColor = Color.Transparent
        btnRequests.FlatAppearance.BorderColor = Color.White
        btnRequests.FlatAppearance.BorderSize = 0
        btnRequests.FlatStyle = FlatStyle.Flat
        btnRequests.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnRequests.ForeColor = Color.White
        btnRequests.Image = My.Resources.Resources.icRequests
        btnRequests.ImageAlign = ContentAlignment.MiddleLeft
        btnRequests.Location = New Point(9, 327)
        btnRequests.Name = "btnRequests"
        btnRequests.Size = New Size(250, 60)
        btnRequests.TabIndex = 14
        btnRequests.Text = "Requests"
        btnRequests.UseVisualStyleBackColor = False
        ' 
        ' btnServices
        ' 
        btnServices.BackColor = Color.Transparent
        btnServices.FlatAppearance.BorderColor = Color.White
        btnServices.FlatAppearance.BorderSize = 0
        btnServices.FlatStyle = FlatStyle.Flat
        btnServices.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnServices.ForeColor = Color.White
        btnServices.Image = My.Resources.Resources.icServices
        btnServices.ImageAlign = ContentAlignment.MiddleLeft
        btnServices.Location = New Point(9, 261)
        btnServices.Name = "btnServices"
        btnServices.Size = New Size(250, 60)
        btnServices.TabIndex = 13
        btnServices.Text = "Services"
        btnServices.UseVisualStyleBackColor = False
        ' 
        ' btnFarmers
        ' 
        btnFarmers.BackColor = Color.Transparent
        btnFarmers.BackgroundImageLayout = ImageLayout.Center
        btnFarmers.FlatAppearance.BorderColor = Color.White
        btnFarmers.FlatAppearance.BorderSize = 0
        btnFarmers.FlatStyle = FlatStyle.Flat
        btnFarmers.Font = New Font("Bahnschrift SemiBold", 12.75F, FontStyle.Bold)
        btnFarmers.ForeColor = Color.White
        btnFarmers.Image = My.Resources.Resources.icFarmers
        btnFarmers.ImageAlign = ContentAlignment.MiddleLeft
        btnFarmers.Location = New Point(9, 195)
        btnFarmers.Name = "btnFarmers"
        btnFarmers.Size = New Size(250, 60)
        btnFarmers.TabIndex = 0
        btnFarmers.Text = "Farmers"
        btnFarmers.UseVisualStyleBackColor = False
        ' 
        ' lblTitle
        ' 
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(3, 117)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(261, 50)
        lblTitle.TabIndex = 12
        lblTitle.Text = " Provincial Agricultural and " & vbCrLf & "Biosystems and Engineering"
        ' 
        ' pbLogo
        ' 
        pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), Image)
        pbLogo.Location = New Point(79, 18)
        pbLogo.Name = "pbLogo"
        pbLogo.Size = New Size(102, 96)
        pbLogo.SizeMode = PictureBoxSizeMode.StretchImage
        pbLogo.TabIndex = 11
        pbLogo.TabStop = False
        ' 
        ' pnlForms
        ' 
        pnlForms.Location = New Point(268, 67)
        pnlForms.Name = "pnlForms"
        pnlForms.Size = New Size(1644, 993)
        pnlForms.TabIndex = 12
        ' 
        ' pnlHeader
        ' 
        pnlHeader.Controls.Add(lblHeader)
        pnlHeader.Location = New Point(268, 0)
        pnlHeader.Name = "pnlHeader"
        pnlHeader.Size = New Size(1645, 68)
        pnlHeader.TabIndex = 0
        ' 
        ' lblHeader
        ' 
        lblHeader.AutoSize = True
        lblHeader.Font = New Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblHeader.Location = New Point(6, 16)
        lblHeader.Name = "lblHeader"
        lblHeader.Size = New Size(119, 37)
        lblHeader.TabIndex = 0
        lblHeader.Text = "Farmers"
        ' 
        ' mdiPABEO
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1904, 1041)
        Controls.Add(pnlHeader)
        Controls.Add(pnlForms)
        Controls.Add(pnlSideNav)
        IsMdiContainer = True
        Margin = New Padding(4, 3, 4, 3)
        Name = "mdiPABEO"
        Text = "mdiPABEO"
        WindowState = FormWindowState.Maximized
        pnlSideNav.ResumeLayout(False)
        pnlSideNav.PerformLayout()
        CType(pbLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlHeader.ResumeLayout(False)
        pnlHeader.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents pnlSideNav As Panel
    Friend WithEvents pbLogo As PictureBox
    Friend WithEvents lblTitle As Label
    Friend WithEvents pnlForms As Panel
    Friend WithEvents btnFarmers As Button
    Friend WithEvents btnOperator As Button
    Friend WithEvents btnMachinery As Button
    Friend WithEvents btnRequests As Button
    Friend WithEvents btnServices As Button
    Friend WithEvents btnSystemConfig As LinkLabel
    Friend WithEvents pnlHeader As Panel
    Friend WithEvents btnStation As Button
    Friend WithEvents btnEmployee As Button
    Friend WithEvents lblHeader As Label

End Class
