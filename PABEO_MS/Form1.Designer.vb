<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPanelHolder
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPanelHolder))
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As DataGridViewCellStyle = New DataGridViewCellStyle()
        pnlConfig = New Panel()
        pnlConfigForm = New Panel()
        txtUID = New TextBox()
        btnConnect = New Button()
        lblUsername = New Label()
        txtPWD = New TextBox()
        lblDatabase = New Label()
        lblPWD = New Label()
        lblServer = New Label()
        txtServer = New TextBox()
        txtDatabase = New TextBox()
        lblPABEO = New Label()
        pbLogo = New PictureBox()
        MySqlConnection1 = New MySql.Data.MySqlClient.MySqlConnection()
        pnlFarmers = New Panel()
        lblMainDir = New Label()
        btnAddFarmer = New Button()
        btnFarmerExport = New Button()
        pnlFarmersDataGrid = New Panel()
        dgvFarmers = New DataGridView()
        formatted_id = New DataGridViewTextBoxColumn()
        full_name = New DataGridViewTextBoxColumn()
        residence_address = New DataGridViewTextBoxColumn()
        contact_number = New DataGridViewTextBoxColumn()
        classification = New DataGridViewTextBoxColumn()
        registration_status = New DataGridViewTextBoxColumn()
        FarmerEdit = New DataGridViewImageColumn()
        FarmerDelete = New DataGridViewImageColumn()
        pnlFarmersSearch = New Panel()
        txtFarmersSearch = New TextBox()
        btnSearch = New Button()
        pnlPendingValidation = New Panel()
        lblTotalPending = New Label()
        lblPendingViolation = New Label()
        lblFarmerSubtitle = New Label()
        lblFarmerHeader = New Label()
        pnlTotalFarmers = New Panel()
        lblFarmerTotal = New Label()
        lblTotalFarmers = New Label()
        pnlServices = New Panel()
        lblServiceDir = New Label()
        btnAddService = New Button()
        btExportServiceReport = New Button()
        pnlDGVService = New Panel()
        dgvServices = New DataGridView()
        service_id = New DataGridViewTextBoxColumn()
        service_name = New DataGridViewTextBoxColumn()
        service_type = New DataGridViewTextBoxColumn()
        service_description = New DataGridViewTextBoxColumn()
        service_policy_limit = New DataGridViewTextBoxColumn()
        employee_id = New DataGridViewTextBoxColumn()
        pnlSearchService = New Panel()
        txtServiceSearch = New TextBox()
        btnServiceSearch = New Button()
        lblServiceSubtitle = New Label()
        lblServicesHeader = New Label()
        pnlTotalService = New Panel()
        lblServiceTotal = New Label()
        lblTotalServices = New Label()
        pnlOperator = New Panel()
        Label6 = New Label()
        pnlMachinery = New Panel()
        Label3 = New Label()
        pnlRequests = New Panel()
        lblRequestsDir = New Label()
        Button1 = New Button()
        Button2 = New Button()
        pnlDGVRequests = New Panel()
        dgvRequests = New DataGridView()
        DataGridViewTextBoxColumn1 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn2 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn3 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn4 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn5 = New DataGridViewTextBoxColumn()
        DataGridViewTextBoxColumn6 = New DataGridViewTextBoxColumn()
        DataGridViewImageColumn1 = New DataGridViewImageColumn()
        DataGridViewImageColumn2 = New DataGridViewImageColumn()
        Panel2 = New Panel()
        TextBox1 = New TextBox()
        Button3 = New Button()
        Label8 = New Label()
        lblRequestsHeader = New Label()
        Panel4 = New Panel()
        lblAmountPendingRequests = New Label()
        lblTotalPendingRequsts = New Label()
        pnlEmployee = New Panel()
        Label1 = New Label()
        pnlStation = New Panel()
        lblStation = New Label()
        MySqlCommand2 = New MySql.Data.MySqlClient.MySqlCommand()
        pnlCreateFarmer = New Panel()
        btnRegisterFarmer = New Button()
        btnRegisterCancel = New Button()
        pnlClassification = New Panel()
        pnlRegStatus = New Panel()
        cmbRegStatus = New ComboBox()
        lblRegStatus = New Label()
        lblClassification = New Label()
        pnlClass = New Panel()
        cmbClass = New ComboBox()
        lblMainDir2 = New Label()
        lblSubDir = New Label()
        pnlFPersonalInfo = New Panel()
        pnlFResidence = New Panel()
        lblProvince = New Label()
        lblCity = New Label()
        lblBarangay = New Label()
        cmbProvince = New ComboBox()
        cmbCity = New ComboBox()
        cmbBarangay = New ComboBox()
        lblFResidenceAddress = New Label()
        lblRSBSA = New Label()
        pnlFarmerID = New Panel()
        txtFarmerID = New TextBox()
        pnlFAge = New Panel()
        txtFAge = New TextBox()
        lblFAge = New Label()
        lblFBirth = New Label()
        pnlFBirth = New Panel()
        lblYear = New Label()
        lblDay = New Label()
        lblMonth = New Label()
        cmbYear = New ComboBox()
        cmbDay = New ComboBox()
        cmbMonth = New ComboBox()
        lblFAdress = New Label()
        pnlFAddress = New Panel()
        txtFAddress = New TextBox()
        lblFEmail = New Label()
        pnlFEmail = New Panel()
        txtFEmail = New TextBox()
        lblFContactNumber = New Label()
        pnlFContact = New Panel()
        txtFContact = New TextBox()
        lblFFullname = New Label()
        pnlFFullname = New Panel()
        txtFFullname = New TextBox()
        lblPersonalInfo = New Label()
        lblFCreateSubtitle = New Label()
        lblCreateFarmer = New Label()
        pnlCreateService = New Panel()
        btnSaveService = New Button()
        btnServiceCancel = New Button()
        imgRental = New PictureBox()
        lblServiceMainDir = New Label()
        lblServiceSubDir = New Label()
        pnlServiceInfo = New Panel()
        lblMachineID = New Label()
        pnlMachineryId = New Panel()
        cmbMachineryID = New ComboBox()
        lblServiceID = New Label()
        pnlServiceID = New Panel()
        txtServiceID = New TextBox()
        lblSEmployeeID = New Label()
        pnlSEmployee = New Panel()
        cmbSEmployeeID = New ComboBox()
        lblServiceDescription = New Label()
        pnlServiceDescription = New Panel()
        txtServiceDescription = New TextBox()
        lblServiceType = New Label()
        pnlServiceType = New Panel()
        cmbServiceType = New ComboBox()
        lblPolicyLimit = New Label()
        pnlPolicyLimit = New Panel()
        txtPolicyLimit = New TextBox()
        lblServiceName = New Label()
        pnlServiceName = New Panel()
        txtServiceName = New TextBox()
        lblServiceInformation = New Label()
        lblAddServiceSubtitle = New Label()
        lblAddService = New Label()
        ImagePolicy = New PictureBox()
        pnlConfig.SuspendLayout()
        pnlConfigForm.SuspendLayout()
        CType(pbLogo, ComponentModel.ISupportInitialize).BeginInit()
        pnlFarmers.SuspendLayout()
        pnlFarmersDataGrid.SuspendLayout()
        CType(dgvFarmers, ComponentModel.ISupportInitialize).BeginInit()
        pnlFarmersSearch.SuspendLayout()
        pnlPendingValidation.SuspendLayout()
        pnlTotalFarmers.SuspendLayout()
        pnlServices.SuspendLayout()
        pnlDGVService.SuspendLayout()
        CType(dgvServices, ComponentModel.ISupportInitialize).BeginInit()
        pnlSearchService.SuspendLayout()
        pnlTotalService.SuspendLayout()
        pnlOperator.SuspendLayout()
        pnlMachinery.SuspendLayout()
        pnlRequests.SuspendLayout()
        pnlDGVRequests.SuspendLayout()
        CType(dgvRequests, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        Panel4.SuspendLayout()
        pnlEmployee.SuspendLayout()
        pnlStation.SuspendLayout()
        pnlCreateFarmer.SuspendLayout()
        pnlClassification.SuspendLayout()
        pnlRegStatus.SuspendLayout()
        pnlClass.SuspendLayout()
        pnlFPersonalInfo.SuspendLayout()
        pnlFResidence.SuspendLayout()
        pnlFarmerID.SuspendLayout()
        pnlFAge.SuspendLayout()
        pnlFBirth.SuspendLayout()
        pnlFAddress.SuspendLayout()
        pnlFEmail.SuspendLayout()
        pnlFContact.SuspendLayout()
        pnlFFullname.SuspendLayout()
        pnlCreateService.SuspendLayout()
        CType(imgRental, ComponentModel.ISupportInitialize).BeginInit()
        pnlServiceInfo.SuspendLayout()
        pnlMachineryId.SuspendLayout()
        pnlServiceID.SuspendLayout()
        pnlSEmployee.SuspendLayout()
        pnlServiceDescription.SuspendLayout()
        pnlServiceType.SuspendLayout()
        pnlPolicyLimit.SuspendLayout()
        pnlServiceName.SuspendLayout()
        CType(ImagePolicy, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' pnlConfig
        ' 
        pnlConfig.BackgroundImage = CType(resources.GetObject("pnlConfig.BackgroundImage"), Image)
        pnlConfig.BackgroundImageLayout = ImageLayout.Stretch
        pnlConfig.Controls.Add(pnlConfigForm)
        pnlConfig.Controls.Add(lblPABEO)
        pnlConfig.Controls.Add(pbLogo)
        pnlConfig.Dock = DockStyle.Fill
        pnlConfig.Location = New Point(0, 0)
        pnlConfig.Name = "pnlConfig"
        pnlConfig.Size = New Size(1924, 1041)
        pnlConfig.TabIndex = 0
        ' 
        ' pnlConfigForm
        ' 
        pnlConfigForm.BackColor = Color.FromArgb(CByte(200), CByte(2), CByte(48), CByte(32))
        pnlConfigForm.Controls.Add(txtUID)
        pnlConfigForm.Controls.Add(btnConnect)
        pnlConfigForm.Controls.Add(lblUsername)
        pnlConfigForm.Controls.Add(txtPWD)
        pnlConfigForm.Controls.Add(lblDatabase)
        pnlConfigForm.Controls.Add(lblPWD)
        pnlConfigForm.Controls.Add(lblServer)
        pnlConfigForm.Controls.Add(txtServer)
        pnlConfigForm.Controls.Add(txtDatabase)
        pnlConfigForm.Location = New Point(584, 529)
        pnlConfigForm.Name = "pnlConfigForm"
        pnlConfigForm.Size = New Size(752, 422)
        pnlConfigForm.TabIndex = 11
        ' 
        ' txtUID
        ' 
        txtUID.BorderStyle = BorderStyle.FixedSingle
        txtUID.Font = New Font("Segoe UI", 12F)
        txtUID.Location = New Point(213, 184)
        txtUID.Name = "txtUID"
        txtUID.Size = New Size(366, 29)
        txtUID.TabIndex = 8
        txtUID.Text = "root"
        ' 
        ' btnConnect
        ' 
        btnConnect.BackColor = Color.DarkSeaGreen
        btnConnect.FlatAppearance.BorderSize = 0
        btnConnect.FlatStyle = FlatStyle.Flat
        btnConnect.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnConnect.ForeColor = Color.White
        btnConnect.Location = New Point(585, 364)
        btnConnect.Name = "btnConnect"
        btnConnect.Size = New Size(152, 41)
        btnConnect.TabIndex = 10
        btnConnect.Text = "Connect"
        btnConnect.UseVisualStyleBackColor = False
        ' 
        ' lblUsername
        ' 
        lblUsername.AutoSize = True
        lblUsername.BackColor = Color.Transparent
        lblUsername.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold)
        lblUsername.ForeColor = Color.White
        lblUsername.Location = New Point(55, 181)
        lblUsername.Name = "lblUsername"
        lblUsername.Size = New Size(109, 30)
        lblUsername.TabIndex = 4
        lblUsername.Text = "Username"
        ' 
        ' txtPWD
        ' 
        txtPWD.BorderStyle = BorderStyle.FixedSingle
        txtPWD.Font = New Font("Segoe UI", 12F)
        txtPWD.Location = New Point(213, 247)
        txtPWD.Name = "txtPWD"
        txtPWD.Size = New Size(366, 29)
        txtPWD.TabIndex = 9
        txtPWD.UseSystemPasswordChar = True
        ' 
        ' lblDatabase
        ' 
        lblDatabase.AutoSize = True
        lblDatabase.BackColor = Color.Transparent
        lblDatabase.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold)
        lblDatabase.ForeColor = Color.White
        lblDatabase.Location = New Point(55, 117)
        lblDatabase.Name = "lblDatabase"
        lblDatabase.Size = New Size(102, 30)
        lblDatabase.TabIndex = 3
        lblDatabase.Text = "Database"
        ' 
        ' lblPWD
        ' 
        lblPWD.AutoSize = True
        lblPWD.BackColor = Color.Transparent
        lblPWD.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold)
        lblPWD.ForeColor = Color.White
        lblPWD.Location = New Point(55, 244)
        lblPWD.Name = "lblPWD"
        lblPWD.Size = New Size(103, 30)
        lblPWD.TabIndex = 5
        lblPWD.Text = "Password"
        ' 
        ' lblServer
        ' 
        lblServer.AutoSize = True
        lblServer.BackColor = Color.Transparent
        lblServer.Font = New Font("Segoe UI Semibold", 15.75F, FontStyle.Bold)
        lblServer.ForeColor = Color.White
        lblServer.Location = New Point(55, 53)
        lblServer.Name = "lblServer"
        lblServer.Size = New Size(74, 30)
        lblServer.TabIndex = 2
        lblServer.Text = "Server"
        ' 
        ' txtServer
        ' 
        txtServer.BorderStyle = BorderStyle.FixedSingle
        txtServer.Font = New Font("Segoe UI", 12F)
        txtServer.Location = New Point(213, 56)
        txtServer.Name = "txtServer"
        txtServer.Size = New Size(366, 29)
        txtServer.TabIndex = 6
        txtServer.Text = "localhost"
        ' 
        ' txtDatabase
        ' 
        txtDatabase.BorderStyle = BorderStyle.FixedSingle
        txtDatabase.Font = New Font("Segoe UI", 12F)
        txtDatabase.Location = New Point(213, 120)
        txtDatabase.Name = "txtDatabase"
        txtDatabase.Size = New Size(366, 29)
        txtDatabase.TabIndex = 7
        txtDatabase.Text = "pabeo"
        ' 
        ' lblPABEO
        ' 
        lblPABEO.AutoSize = True
        lblPABEO.BackColor = Color.Transparent
        lblPABEO.Font = New Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPABEO.ForeColor = Color.White
        lblPABEO.Location = New Point(555, 417)
        lblPABEO.Name = "lblPABEO"
        lblPABEO.Size = New Size(809, 80)
        lblPABEO.TabIndex = 1
        lblPABEO.Text = "Provincial Agricultural and Biosystems Engineering Office" & vbCrLf & "Camarines Norte - System Config." & vbCrLf
        lblPABEO.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pbLogo
        ' 
        pbLogo.BackColor = Color.Transparent
        pbLogo.Image = CType(resources.GetObject("pbLogo.Image"), Image)
        pbLogo.Location = New Point(774, 84)
        pbLogo.Name = "pbLogo"
        pbLogo.Size = New Size(382, 319)
        pbLogo.SizeMode = PictureBoxSizeMode.Zoom
        pbLogo.TabIndex = 0
        pbLogo.TabStop = False
        ' 
        ' pnlFarmers
        ' 
        pnlFarmers.Controls.Add(lblMainDir)
        pnlFarmers.Controls.Add(btnAddFarmer)
        pnlFarmers.Controls.Add(btnFarmerExport)
        pnlFarmers.Controls.Add(pnlFarmersDataGrid)
        pnlFarmers.Controls.Add(pnlFarmersSearch)
        pnlFarmers.Controls.Add(pnlPendingValidation)
        pnlFarmers.Controls.Add(lblFarmerSubtitle)
        pnlFarmers.Controls.Add(lblFarmerHeader)
        pnlFarmers.Controls.Add(pnlTotalFarmers)
        pnlFarmers.Location = New Point(0, 0)
        pnlFarmers.Name = "pnlFarmers"
        pnlFarmers.Size = New Size(1663, 1041)
        pnlFarmers.TabIndex = 12
        ' 
        ' lblMainDir
        ' 
        lblMainDir.AutoSize = True
        lblMainDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblMainDir.ForeColor = SystemColors.ControlDark
        lblMainDir.Location = New Point(19, 25)
        lblMainDir.Name = "lblMainDir"
        lblMainDir.Size = New Size(67, 21)
        lblMainDir.TabIndex = 1
        lblMainDir.Text = "Farmers"
        ' 
        ' btnAddFarmer
        ' 
        btnAddFarmer.BackColor = Color.DarkGreen
        btnAddFarmer.BackgroundImageLayout = ImageLayout.None
        btnAddFarmer.FlatAppearance.BorderSize = 0
        btnAddFarmer.FlatStyle = FlatStyle.Flat
        btnAddFarmer.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        btnAddFarmer.ForeColor = Color.White
        btnAddFarmer.Image = CType(resources.GetObject("btnAddFarmer.Image"), Image)
        btnAddFarmer.ImageAlign = ContentAlignment.MiddleLeft
        btnAddFarmer.Location = New Point(1473, 410)
        btnAddFarmer.Name = "btnAddFarmer"
        btnAddFarmer.Size = New Size(142, 37)
        btnAddFarmer.TabIndex = 11
        btnAddFarmer.TabStop = False
        btnAddFarmer.Text = "Add Farmer"
        btnAddFarmer.UseVisualStyleBackColor = False
        ' 
        ' btnFarmerExport
        ' 
        btnFarmerExport.BackColor = Color.White
        btnFarmerExport.FlatAppearance.BorderColor = Color.Silver
        btnFarmerExport.FlatStyle = FlatStyle.Flat
        btnFarmerExport.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnFarmerExport.Image = CType(resources.GetObject("btnFarmerExport.Image"), Image)
        btnFarmerExport.ImageAlign = ContentAlignment.MiddleLeft
        btnFarmerExport.Location = New Point(1320, 410)
        btnFarmerExport.Name = "btnFarmerExport"
        btnFarmerExport.Size = New Size(147, 37)
        btnFarmerExport.TabIndex = 10
        btnFarmerExport.Text = "Export"
        btnFarmerExport.UseVisualStyleBackColor = False
        ' 
        ' pnlFarmersDataGrid
        ' 
        pnlFarmersDataGrid.BackColor = Color.White
        pnlFarmersDataGrid.Controls.Add(dgvFarmers)
        pnlFarmersDataGrid.Location = New Point(19, 462)
        pnlFarmersDataGrid.Name = "pnlFarmersDataGrid"
        pnlFarmersDataGrid.Size = New Size(1596, 489)
        pnlFarmersDataGrid.TabIndex = 9
        ' 
        ' dgvFarmers
        ' 
        dgvFarmers.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvFarmers.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        dgvFarmers.BackgroundColor = Color.White
        dgvFarmers.BorderStyle = BorderStyle.None
        dgvFarmers.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvFarmers.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        dgvFarmers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = Color.White
        DataGridViewCellStyle2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.ControlLight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        dgvFarmers.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        dgvFarmers.ColumnHeadersHeight = 50
        dgvFarmers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvFarmers.Columns.AddRange(New DataGridViewColumn() {formatted_id, full_name, residence_address, contact_number, classification, registration_status, FarmerEdit, FarmerDelete})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.BackColor = SystemColors.Window
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.ButtonFace
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        dgvFarmers.DefaultCellStyle = DataGridViewCellStyle4
        dgvFarmers.EnableHeadersVisualStyles = False
        dgvFarmers.GridColor = Color.White
        dgvFarmers.Location = New Point(15, 12)
        dgvFarmers.Name = "dgvFarmers"
        dgvFarmers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = SystemColors.Control
        DataGridViewCellStyle5.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle5.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = DataGridViewTriState.True
        dgvFarmers.RowHeadersDefaultCellStyle = DataGridViewCellStyle5
        dgvFarmers.RowHeadersVisible = False
        dgvFarmers.RowHeadersWidth = 60
        dgvFarmers.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvFarmers.RowTemplate.Height = 40
        dgvFarmers.RowTemplate.Resizable = DataGridViewTriState.True
        dgvFarmers.ScrollBars = ScrollBars.Vertical
        dgvFarmers.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFarmers.Size = New Size(1559, 428)
        dgvFarmers.TabIndex = 5
        ' 
        ' formatted_id
        ' 
        formatted_id.DataPropertyName = "formatted_id"
        formatted_id.HeaderText = "Farmer ID"
        formatted_id.Name = "formatted_id"
        formatted_id.Width = 225
        ' 
        ' full_name
        ' 
        full_name.DataPropertyName = "full_name"
        full_name.HeaderText = "Full Name"
        full_name.Name = "full_name"
        full_name.Width = 300
        ' 
        ' residence_address
        ' 
        residence_address.DataPropertyName = "residence_address"
        residence_address.HeaderText = "Address"
        residence_address.Name = "residence_address"
        residence_address.Width = 325
        ' 
        ' contact_number
        ' 
        contact_number.DataPropertyName = "contact_number"
        contact_number.HeaderText = "ContactNumber"
        contact_number.Name = "contact_number"
        contact_number.Width = 200
        ' 
        ' classification
        ' 
        classification.DataPropertyName = "classification"
        classification.HeaderText = "Classification"
        classification.Name = "classification"
        classification.Width = 200
        ' 
        ' registration_status
        ' 
        registration_status.DataPropertyName = "registration_status"
        registration_status.HeaderText = "Status"
        registration_status.Name = "registration_status"
        registration_status.Width = 150
        ' 
        ' FarmerEdit
        ' 
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.NullValue = "None"
        FarmerEdit.DefaultCellStyle = DataGridViewCellStyle3
        FarmerEdit.HeaderText = "Edit"
        FarmerEdit.Image = CType(resources.GetObject("FarmerEdit.Image"), Image)
        FarmerEdit.Name = "FarmerEdit"
        FarmerEdit.Resizable = DataGridViewTriState.True
        FarmerEdit.SortMode = DataGridViewColumnSortMode.Automatic
        FarmerEdit.Width = 75
        ' 
        ' FarmerDelete
        ' 
        FarmerDelete.HeaderText = "Delete"
        FarmerDelete.Image = CType(resources.GetObject("FarmerDelete.Image"), Image)
        FarmerDelete.Name = "FarmerDelete"
        FarmerDelete.Width = 75
        ' 
        ' pnlFarmersSearch
        ' 
        pnlFarmersSearch.BackColor = Color.White
        pnlFarmersSearch.Controls.Add(txtFarmersSearch)
        pnlFarmersSearch.Controls.Add(btnSearch)
        pnlFarmersSearch.ForeColor = Color.White
        pnlFarmersSearch.Location = New Point(19, 410)
        pnlFarmersSearch.Name = "pnlFarmersSearch"
        pnlFarmersSearch.Size = New Size(568, 37)
        pnlFarmersSearch.TabIndex = 8
        ' 
        ' txtFarmersSearch
        ' 
        txtFarmersSearch.BorderStyle = BorderStyle.None
        txtFarmersSearch.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtFarmersSearch.Location = New Point(46, 5)
        txtFarmersSearch.Name = "txtFarmersSearch"
        txtFarmersSearch.Size = New Size(515, 28)
        txtFarmersSearch.TabIndex = 6
        ' 
        ' btnSearch
        ' 
        btnSearch.BackColor = Color.White
        btnSearch.BackgroundImage = CType(resources.GetObject("btnSearch.BackgroundImage"), Image)
        btnSearch.BackgroundImageLayout = ImageLayout.Center
        btnSearch.FlatAppearance.BorderSize = 0
        btnSearch.FlatStyle = FlatStyle.Flat
        btnSearch.Location = New Point(5, 4)
        btnSearch.Name = "btnSearch"
        btnSearch.Size = New Size(31, 30)
        btnSearch.TabIndex = 7
        btnSearch.UseVisualStyleBackColor = False
        ' 
        ' pnlPendingValidation
        ' 
        pnlPendingValidation.BackColor = Color.White
        pnlPendingValidation.Controls.Add(lblTotalPending)
        pnlPendingValidation.Controls.Add(lblPendingViolation)
        pnlPendingValidation.ForeColor = Color.Black
        pnlPendingValidation.Location = New Point(387, 200)
        pnlPendingValidation.Name = "pnlPendingValidation"
        pnlPendingValidation.Size = New Size(337, 178)
        pnlPendingValidation.TabIndex = 3
        ' 
        ' lblTotalPending
        ' 
        lblTotalPending.AutoSize = True
        lblTotalPending.Font = New Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblTotalPending.Location = New Point(15, 50)
        lblTotalPending.Name = "lblTotalPending"
        lblTotalPending.Size = New Size(111, 86)
        lblTotalPending.TabIndex = 2
        lblTotalPending.Text = "00"
        ' 
        ' lblPendingViolation
        ' 
        lblPendingViolation.AutoSize = True
        lblPendingViolation.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        lblPendingViolation.ForeColor = SystemColors.ControlDarkDark
        lblPendingViolation.Location = New Point(15, 13)
        lblPendingViolation.Name = "lblPendingViolation"
        lblPendingViolation.Size = New Size(145, 21)
        lblPendingViolation.TabIndex = 1
        lblPendingViolation.Text = "Pending Validation"
        ' 
        ' lblFarmerSubtitle
        ' 
        lblFarmerSubtitle.AutoSize = True
        lblFarmerSubtitle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFarmerSubtitle.ForeColor = SystemColors.ControlDarkDark
        lblFarmerSubtitle.Location = New Point(19, 125)
        lblFarmerSubtitle.Name = "lblFarmerSubtitle"
        lblFarmerSubtitle.Size = New Size(568, 21)
        lblFarmerSubtitle.TabIndex = 4
        lblFarmerSubtitle.Text = "Manage registration and records of operational status of registered farmers."
        ' 
        ' lblFarmerHeader
        ' 
        lblFarmerHeader.AutoSize = True
        lblFarmerHeader.Font = New Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFarmerHeader.ForeColor = Color.Black
        lblFarmerHeader.Location = New Point(12, 71)
        lblFarmerHeader.Name = "lblFarmerHeader"
        lblFarmerHeader.Size = New Size(437, 54)
        lblFarmerHeader.TabIndex = 3
        lblFarmerHeader.Text = "Farmers Management"
        ' 
        ' pnlTotalFarmers
        ' 
        pnlTotalFarmers.BackColor = Color.White
        pnlTotalFarmers.Controls.Add(lblFarmerTotal)
        pnlTotalFarmers.Controls.Add(lblTotalFarmers)
        pnlTotalFarmers.ForeColor = Color.Black
        pnlTotalFarmers.Location = New Point(19, 200)
        pnlTotalFarmers.Name = "pnlTotalFarmers"
        pnlTotalFarmers.Size = New Size(337, 178)
        pnlTotalFarmers.TabIndex = 2
        ' 
        ' lblFarmerTotal
        ' 
        lblFarmerTotal.AutoSize = True
        lblFarmerTotal.Font = New Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFarmerTotal.Location = New Point(15, 50)
        lblFarmerTotal.Name = "lblFarmerTotal"
        lblFarmerTotal.Size = New Size(111, 86)
        lblFarmerTotal.TabIndex = 1
        lblFarmerTotal.Text = "00"
        ' 
        ' lblTotalFarmers
        ' 
        lblTotalFarmers.AutoSize = True
        lblTotalFarmers.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        lblTotalFarmers.ForeColor = SystemColors.ControlDarkDark
        lblTotalFarmers.Location = New Point(15, 13)
        lblTotalFarmers.Name = "lblTotalFarmers"
        lblTotalFarmers.Size = New Size(106, 21)
        lblTotalFarmers.TabIndex = 0
        lblTotalFarmers.Text = "Total Farmers"
        ' 
        ' pnlServices
        ' 
        pnlServices.Controls.Add(lblServiceDir)
        pnlServices.Controls.Add(btnAddService)
        pnlServices.Controls.Add(btExportServiceReport)
        pnlServices.Controls.Add(pnlDGVService)
        pnlServices.Controls.Add(pnlSearchService)
        pnlServices.Controls.Add(lblServiceSubtitle)
        pnlServices.Controls.Add(lblServicesHeader)
        pnlServices.Controls.Add(pnlTotalService)
        pnlServices.Location = New Point(0, 0)
        pnlServices.Name = "pnlServices"
        pnlServices.Size = New Size(1663, 1041)
        pnlServices.TabIndex = 13
        ' 
        ' lblServiceDir
        ' 
        lblServiceDir.AutoSize = True
        lblServiceDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceDir.ForeColor = SystemColors.ControlDark
        lblServiceDir.Location = New Point(19, 25)
        lblServiceDir.Name = "lblServiceDir"
        lblServiceDir.Size = New Size(124, 21)
        lblServiceDir.TabIndex = 21
        lblServiceDir.Text = "Service Catalog"
        ' 
        ' btnAddService
        ' 
        btnAddService.BackColor = Color.DarkGreen
        btnAddService.BackgroundImageLayout = ImageLayout.None
        btnAddService.FlatAppearance.BorderSize = 0
        btnAddService.FlatStyle = FlatStyle.Flat
        btnAddService.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        btnAddService.ForeColor = Color.White
        btnAddService.Image = CType(resources.GetObject("btnAddService.Image"), Image)
        btnAddService.ImageAlign = ContentAlignment.MiddleLeft
        btnAddService.Location = New Point(1473, 410)
        btnAddService.Name = "btnAddService"
        btnAddService.Size = New Size(142, 37)
        btnAddService.TabIndex = 28
        btnAddService.TabStop = False
        btnAddService.Text = "Add Service"
        btnAddService.UseVisualStyleBackColor = False
        ' 
        ' btExportServiceReport
        ' 
        btExportServiceReport.BackColor = Color.White
        btExportServiceReport.FlatAppearance.BorderColor = Color.Silver
        btExportServiceReport.FlatStyle = FlatStyle.Flat
        btExportServiceReport.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btExportServiceReport.Image = CType(resources.GetObject("btExportServiceReport.Image"), Image)
        btExportServiceReport.ImageAlign = ContentAlignment.MiddleLeft
        btExportServiceReport.Location = New Point(1320, 410)
        btExportServiceReport.Name = "btExportServiceReport"
        btExportServiceReport.Size = New Size(147, 37)
        btExportServiceReport.TabIndex = 27
        btExportServiceReport.Text = "Export"
        btExportServiceReport.UseVisualStyleBackColor = False
        ' 
        ' pnlDGVService
        ' 
        pnlDGVService.BackColor = Color.White
        pnlDGVService.Controls.Add(dgvServices)
        pnlDGVService.Location = New Point(19, 462)
        pnlDGVService.Name = "pnlDGVService"
        pnlDGVService.Size = New Size(1596, 489)
        pnlDGVService.TabIndex = 26
        ' 
        ' dgvServices
        ' 
        dgvServices.AllowUserToOrderColumns = True
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvServices.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle6
        dgvServices.BackgroundColor = Color.White
        dgvServices.BorderStyle = BorderStyle.None
        dgvServices.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvServices.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        dgvServices.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle7.BackColor = Color.White
        DataGridViewCellStyle7.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        DataGridViewCellStyle7.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle7.SelectionBackColor = SystemColors.ControlLight
        DataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = DataGridViewTriState.True
        dgvServices.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle7
        dgvServices.ColumnHeadersHeight = 50
        dgvServices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvServices.Columns.AddRange(New DataGridViewColumn() {service_id, service_name, service_type, service_description, service_policy_limit, employee_id})
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = SystemColors.Window
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle8.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.ButtonFace
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.False
        dgvServices.DefaultCellStyle = DataGridViewCellStyle8
        dgvServices.EnableHeadersVisualStyles = False
        dgvServices.GridColor = Color.White
        dgvServices.Location = New Point(15, 12)
        dgvServices.Name = "dgvServices"
        dgvServices.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = SystemColors.Control
        DataGridViewCellStyle9.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle9.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.True
        dgvServices.RowHeadersDefaultCellStyle = DataGridViewCellStyle9
        dgvServices.RowHeadersVisible = False
        dgvServices.RowHeadersWidth = 60
        dgvServices.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvServices.RowTemplate.Height = 40
        dgvServices.RowTemplate.Resizable = DataGridViewTriState.True
        dgvServices.ScrollBars = ScrollBars.Vertical
        dgvServices.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvServices.Size = New Size(1559, 428)
        dgvServices.TabIndex = 5
        ' 
        ' service_id
        ' 
        service_id.DataPropertyName = "formatted_id"
        service_id.HeaderText = "Service ID"
        service_id.Name = "service_id"
        service_id.Width = 175
        ' 
        ' service_name
        ' 
        service_name.DataPropertyName = "service_name"
        service_name.HeaderText = "Service Name"
        service_name.Name = "service_name"
        service_name.Width = 175
        ' 
        ' service_type
        ' 
        service_type.DataPropertyName = "service_type"
        service_type.HeaderText = "Service Type"
        service_type.Name = "service_type"
        service_type.Width = 175
        ' 
        ' service_description
        ' 
        service_description.DataPropertyName = "description"
        service_description.HeaderText = "Description"
        service_description.Name = "service_description"
        service_description.Width = 575
        ' 
        ' service_policy_limit
        ' 
        service_policy_limit.DataPropertyName = "policy_limit"
        service_policy_limit.HeaderText = "Policy Limit"
        service_policy_limit.Name = "service_policy_limit"
        service_policy_limit.Width = 300
        ' 
        ' employee_id
        ' 
        employee_id.DataPropertyName = "employee_id"
        employee_id.HeaderText = "Employee ID"
        employee_id.Name = "employee_id"
        employee_id.Width = 175
        ' 
        ' pnlSearchService
        ' 
        pnlSearchService.BackColor = Color.White
        pnlSearchService.Controls.Add(txtServiceSearch)
        pnlSearchService.Controls.Add(btnServiceSearch)
        pnlSearchService.ForeColor = Color.White
        pnlSearchService.Location = New Point(19, 410)
        pnlSearchService.Name = "pnlSearchService"
        pnlSearchService.Size = New Size(568, 37)
        pnlSearchService.TabIndex = 25
        ' 
        ' txtServiceSearch
        ' 
        txtServiceSearch.BorderStyle = BorderStyle.None
        txtServiceSearch.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        txtServiceSearch.Location = New Point(46, 5)
        txtServiceSearch.Name = "txtServiceSearch"
        txtServiceSearch.Size = New Size(515, 28)
        txtServiceSearch.TabIndex = 6
        ' 
        ' btnServiceSearch
        ' 
        btnServiceSearch.BackColor = Color.White
        btnServiceSearch.BackgroundImage = CType(resources.GetObject("btnServiceSearch.BackgroundImage"), Image)
        btnServiceSearch.BackgroundImageLayout = ImageLayout.Center
        btnServiceSearch.FlatAppearance.BorderSize = 0
        btnServiceSearch.FlatStyle = FlatStyle.Flat
        btnServiceSearch.Location = New Point(5, 4)
        btnServiceSearch.Name = "btnServiceSearch"
        btnServiceSearch.Size = New Size(31, 30)
        btnServiceSearch.TabIndex = 7
        btnServiceSearch.UseVisualStyleBackColor = False
        ' 
        ' lblServiceSubtitle
        ' 
        lblServiceSubtitle.AutoSize = True
        lblServiceSubtitle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceSubtitle.ForeColor = SystemColors.ControlDarkDark
        lblServiceSubtitle.Location = New Point(19, 125)
        lblServiceSubtitle.Name = "lblServiceSubtitle"
        lblServiceSubtitle.Size = New Size(483, 21)
        lblServiceSubtitle.TabIndex = 24
        lblServiceSubtitle.Text = "Manage and configure agricultural service types and policy limits"
        ' 
        ' lblServicesHeader
        ' 
        lblServicesHeader.AutoSize = True
        lblServicesHeader.Font = New Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServicesHeader.ForeColor = Color.Black
        lblServicesHeader.Location = New Point(12, 71)
        lblServicesHeader.Name = "lblServicesHeader"
        lblServicesHeader.Size = New Size(333, 54)
        lblServicesHeader.TabIndex = 23
        lblServicesHeader.Text = "Services Catalog"
        ' 
        ' pnlTotalService
        ' 
        pnlTotalService.BackColor = Color.White
        pnlTotalService.Controls.Add(lblServiceTotal)
        pnlTotalService.Controls.Add(lblTotalServices)
        pnlTotalService.ForeColor = Color.Black
        pnlTotalService.Location = New Point(19, 200)
        pnlTotalService.Name = "pnlTotalService"
        pnlTotalService.Size = New Size(337, 178)
        pnlTotalService.TabIndex = 22
        ' 
        ' lblServiceTotal
        ' 
        lblServiceTotal.AutoSize = True
        lblServiceTotal.Font = New Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceTotal.Location = New Point(15, 50)
        lblServiceTotal.Name = "lblServiceTotal"
        lblServiceTotal.Size = New Size(111, 86)
        lblServiceTotal.TabIndex = 1
        lblServiceTotal.Text = "00"
        ' 
        ' lblTotalServices
        ' 
        lblTotalServices.AutoSize = True
        lblTotalServices.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        lblTotalServices.ForeColor = SystemColors.ControlDarkDark
        lblTotalServices.Location = New Point(15, 13)
        lblTotalServices.Name = "lblTotalServices"
        lblTotalServices.Size = New Size(110, 21)
        lblTotalServices.TabIndex = 0
        lblTotalServices.Text = "Total Services"
        ' 
        ' pnlOperator
        ' 
        pnlOperator.Controls.Add(Label6)
        pnlOperator.Location = New Point(0, 0)
        pnlOperator.Name = "pnlOperator"
        pnlOperator.Size = New Size(1663, 1041)
        pnlOperator.TabIndex = 13
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(942, 513)
        Label6.Name = "Label6"
        Label6.Size = New Size(67, 15)
        Label6.TabIndex = 1
        Label6.Text = "lblOperator"
        ' 
        ' pnlMachinery
        ' 
        pnlMachinery.Controls.Add(Label3)
        pnlMachinery.Location = New Point(0, 0)
        pnlMachinery.Name = "pnlMachinery"
        pnlMachinery.Size = New Size(1663, 1041)
        pnlMachinery.TabIndex = 13
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(942, 513)
        Label3.Name = "Label3"
        Label3.Size = New Size(63, 15)
        Label3.TabIndex = 1
        Label3.Text = "Machinery"
        ' 
        ' pnlRequests
        ' 
        pnlRequests.Controls.Add(lblRequestsDir)
        pnlRequests.Controls.Add(Button1)
        pnlRequests.Controls.Add(Button2)
        pnlRequests.Controls.Add(pnlDGVRequests)
        pnlRequests.Controls.Add(Panel2)
        pnlRequests.Controls.Add(Label8)
        pnlRequests.Controls.Add(lblRequestsHeader)
        pnlRequests.Controls.Add(Panel4)
        pnlRequests.Location = New Point(0, 0)
        pnlRequests.Name = "pnlRequests"
        pnlRequests.Size = New Size(1663, 1041)
        pnlRequests.TabIndex = 14
        ' 
        ' lblRequestsDir
        ' 
        lblRequestsDir.AutoSize = True
        lblRequestsDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblRequestsDir.ForeColor = SystemColors.ControlDark
        lblRequestsDir.Location = New Point(19, 25)
        lblRequestsDir.Name = "lblRequestsDir"
        lblRequestsDir.Size = New Size(77, 21)
        lblRequestsDir.TabIndex = 12
        lblRequestsDir.Text = "Requests"
        ' 
        ' Button1
        ' 
        Button1.BackColor = Color.DarkGreen
        Button1.BackgroundImageLayout = ImageLayout.None
        Button1.FlatAppearance.BorderSize = 0
        Button1.FlatStyle = FlatStyle.Flat
        Button1.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        Button1.ForeColor = Color.White
        Button1.Image = CType(resources.GetObject("Button1.Image"), Image)
        Button1.ImageAlign = ContentAlignment.MiddleLeft
        Button1.Location = New Point(1473, 410)
        Button1.Name = "Button1"
        Button1.Size = New Size(142, 37)
        Button1.TabIndex = 20
        Button1.TabStop = False
        Button1.Text = "Add Requests"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.White
        Button2.FlatAppearance.BorderColor = Color.Silver
        Button2.FlatStyle = FlatStyle.Flat
        Button2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Button2.Image = CType(resources.GetObject("Button2.Image"), Image)
        Button2.ImageAlign = ContentAlignment.MiddleLeft
        Button2.Location = New Point(1320, 410)
        Button2.Name = "Button2"
        Button2.Size = New Size(147, 37)
        Button2.TabIndex = 19
        Button2.Text = "Export"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' pnlDGVRequests
        ' 
        pnlDGVRequests.BackColor = Color.White
        pnlDGVRequests.Controls.Add(dgvRequests)
        pnlDGVRequests.Location = New Point(19, 462)
        pnlDGVRequests.Name = "pnlDGVRequests"
        pnlDGVRequests.Size = New Size(1596, 489)
        pnlDGVRequests.TabIndex = 18
        ' 
        ' dgvRequests
        ' 
        dgvRequests.AllowUserToOrderColumns = True
        DataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvRequests.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle10
        dgvRequests.BackgroundColor = Color.White
        dgvRequests.BorderStyle = BorderStyle.None
        dgvRequests.CellBorderStyle = DataGridViewCellBorderStyle.None
        dgvRequests.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
        dgvRequests.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle11.BackColor = Color.White
        DataGridViewCellStyle11.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        DataGridViewCellStyle11.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle11.SelectionBackColor = SystemColors.ControlLight
        DataGridViewCellStyle11.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle11.WrapMode = DataGridViewTriState.True
        dgvRequests.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle11
        dgvRequests.ColumnHeadersHeight = 50
        dgvRequests.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgvRequests.Columns.AddRange(New DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumn5, DataGridViewTextBoxColumn6, DataGridViewImageColumn1, DataGridViewImageColumn2})
        DataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle13.BackColor = SystemColors.Window
        DataGridViewCellStyle13.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle13.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle13.SelectionBackColor = SystemColors.ButtonFace
        DataGridViewCellStyle13.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle13.WrapMode = DataGridViewTriState.False
        dgvRequests.DefaultCellStyle = DataGridViewCellStyle13
        dgvRequests.EnableHeadersVisualStyles = False
        dgvRequests.GridColor = Color.White
        dgvRequests.Location = New Point(15, 12)
        dgvRequests.Name = "dgvRequests"
        dgvRequests.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle14.BackColor = SystemColors.Control
        DataGridViewCellStyle14.Font = New Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        DataGridViewCellStyle14.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle14.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle14.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle14.WrapMode = DataGridViewTriState.True
        dgvRequests.RowHeadersDefaultCellStyle = DataGridViewCellStyle14
        dgvRequests.RowHeadersVisible = False
        dgvRequests.RowHeadersWidth = 60
        dgvRequests.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        dgvRequests.RowTemplate.Height = 40
        dgvRequests.RowTemplate.Resizable = DataGridViewTriState.True
        dgvRequests.ScrollBars = ScrollBars.Vertical
        dgvRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvRequests.Size = New Size(1559, 428)
        dgvRequests.TabIndex = 5
        ' 
        ' DataGridViewTextBoxColumn1
        ' 
        DataGridViewTextBoxColumn1.DataPropertyName = "formatted_id"
        DataGridViewTextBoxColumn1.HeaderText = "Farmer ID"
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        DataGridViewTextBoxColumn1.Width = 225
        ' 
        ' DataGridViewTextBoxColumn2
        ' 
        DataGridViewTextBoxColumn2.DataPropertyName = "full_name"
        DataGridViewTextBoxColumn2.HeaderText = "Full Name"
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        DataGridViewTextBoxColumn2.Width = 300
        ' 
        ' DataGridViewTextBoxColumn3
        ' 
        DataGridViewTextBoxColumn3.DataPropertyName = "residence_address"
        DataGridViewTextBoxColumn3.HeaderText = "Address"
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        DataGridViewTextBoxColumn3.Width = 325
        ' 
        ' DataGridViewTextBoxColumn4
        ' 
        DataGridViewTextBoxColumn4.DataPropertyName = "contact_number"
        DataGridViewTextBoxColumn4.HeaderText = "ContactNumber"
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        DataGridViewTextBoxColumn4.Width = 200
        ' 
        ' DataGridViewTextBoxColumn5
        ' 
        DataGridViewTextBoxColumn5.DataPropertyName = "classification"
        DataGridViewTextBoxColumn5.HeaderText = "Classification"
        DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        DataGridViewTextBoxColumn5.Width = 200
        ' 
        ' DataGridViewTextBoxColumn6
        ' 
        DataGridViewTextBoxColumn6.DataPropertyName = "registration_status"
        DataGridViewTextBoxColumn6.HeaderText = "Status"
        DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        DataGridViewTextBoxColumn6.Width = 150
        ' 
        ' DataGridViewImageColumn1
        ' 
        DataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle12.NullValue = "None"
        DataGridViewImageColumn1.DefaultCellStyle = DataGridViewCellStyle12
        DataGridViewImageColumn1.HeaderText = "Edit"
        DataGridViewImageColumn1.Image = CType(resources.GetObject("DataGridViewImageColumn1.Image"), Image)
        DataGridViewImageColumn1.Name = "DataGridViewImageColumn1"
        DataGridViewImageColumn1.Resizable = DataGridViewTriState.True
        DataGridViewImageColumn1.SortMode = DataGridViewColumnSortMode.Automatic
        DataGridViewImageColumn1.Width = 75
        ' 
        ' DataGridViewImageColumn2
        ' 
        DataGridViewImageColumn2.HeaderText = "Delete"
        DataGridViewImageColumn2.Image = CType(resources.GetObject("DataGridViewImageColumn2.Image"), Image)
        DataGridViewImageColumn2.Name = "DataGridViewImageColumn2"
        DataGridViewImageColumn2.Width = 75
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.White
        Panel2.Controls.Add(TextBox1)
        Panel2.Controls.Add(Button3)
        Panel2.ForeColor = Color.White
        Panel2.Location = New Point(19, 410)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(568, 37)
        Panel2.TabIndex = 17
        ' 
        ' TextBox1
        ' 
        TextBox1.BorderStyle = BorderStyle.None
        TextBox1.Font = New Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        TextBox1.Location = New Point(46, 5)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(515, 28)
        TextBox1.TabIndex = 6
        ' 
        ' Button3
        ' 
        Button3.BackColor = Color.White
        Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), Image)
        Button3.BackgroundImageLayout = ImageLayout.Center
        Button3.FlatAppearance.BorderSize = 0
        Button3.FlatStyle = FlatStyle.Flat
        Button3.Location = New Point(5, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(31, 30)
        Button3.TabIndex = 7
        Button3.UseVisualStyleBackColor = False
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        Label8.ForeColor = SystemColors.ControlDarkDark
        Label8.Location = New Point(19, 125)
        Label8.Name = "Label8"
        Label8.Size = New Size(423, 21)
        Label8.TabIndex = 16
        Label8.Text = "P.A.B.E.O. Real-time agricultural service requests tracking"
        ' 
        ' lblRequestsHeader
        ' 
        lblRequestsHeader.AutoSize = True
        lblRequestsHeader.Font = New Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblRequestsHeader.ForeColor = Color.Black
        lblRequestsHeader.Location = New Point(12, 71)
        lblRequestsHeader.Name = "lblRequestsHeader"
        lblRequestsHeader.Size = New Size(456, 54)
        lblRequestsHeader.TabIndex = 15
        lblRequestsHeader.Text = "Requests Management"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.White
        Panel4.Controls.Add(lblAmountPendingRequests)
        Panel4.Controls.Add(lblTotalPendingRequsts)
        Panel4.ForeColor = Color.Black
        Panel4.Location = New Point(19, 200)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(337, 178)
        Panel4.TabIndex = 13
        ' 
        ' lblAmountPendingRequests
        ' 
        lblAmountPendingRequests.AutoSize = True
        lblAmountPendingRequests.Font = New Font("Segoe UI", 48F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAmountPendingRequests.Location = New Point(15, 50)
        lblAmountPendingRequests.Name = "lblAmountPendingRequests"
        lblAmountPendingRequests.Size = New Size(111, 86)
        lblAmountPendingRequests.TabIndex = 1
        lblAmountPendingRequests.Text = "00"
        ' 
        ' lblTotalPendingRequsts
        ' 
        lblTotalPendingRequsts.AutoSize = True
        lblTotalPendingRequsts.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        lblTotalPendingRequsts.ForeColor = SystemColors.ControlDarkDark
        lblTotalPendingRequsts.Location = New Point(15, 13)
        lblTotalPendingRequsts.Name = "lblTotalPendingRequsts"
        lblTotalPendingRequsts.Size = New Size(108, 21)
        lblTotalPendingRequsts.TabIndex = 0
        lblTotalPendingRequsts.Text = "Total Pending"
        ' 
        ' pnlEmployee
        ' 
        pnlEmployee.Controls.Add(Label1)
        pnlEmployee.Location = New Point(0, 0)
        pnlEmployee.Name = "pnlEmployee"
        pnlEmployee.Size = New Size(1663, 1041)
        pnlEmployee.TabIndex = 15
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(942, 513)
        Label1.Name = "Label1"
        Label1.Size = New Size(59, 15)
        Label1.TabIndex = 1
        Label1.Text = "Employee"
        ' 
        ' pnlStation
        ' 
        pnlStation.Controls.Add(lblStation)
        pnlStation.Location = New Point(0, 0)
        pnlStation.Name = "pnlStation"
        pnlStation.Size = New Size(1663, 1041)
        pnlStation.TabIndex = 16
        ' 
        ' lblStation
        ' 
        lblStation.AutoSize = True
        lblStation.Location = New Point(21, 30)
        lblStation.Name = "lblStation"
        lblStation.Size = New Size(44, 15)
        lblStation.TabIndex = 0
        lblStation.Text = "Station"
        ' 
        ' MySqlCommand2
        ' 
        MySqlCommand2.CacheAge = 0
        MySqlCommand2.Connection = Nothing
        MySqlCommand2.EnableCaching = False
        MySqlCommand2.Transaction = Nothing
        ' 
        ' pnlCreateFarmer
        ' 
        pnlCreateFarmer.Controls.Add(btnRegisterFarmer)
        pnlCreateFarmer.Controls.Add(btnRegisterCancel)
        pnlCreateFarmer.Controls.Add(pnlClassification)
        pnlCreateFarmer.Controls.Add(lblMainDir2)
        pnlCreateFarmer.Controls.Add(lblSubDir)
        pnlCreateFarmer.Controls.Add(pnlFPersonalInfo)
        pnlCreateFarmer.Controls.Add(lblFCreateSubtitle)
        pnlCreateFarmer.Controls.Add(lblCreateFarmer)
        pnlCreateFarmer.Location = New Point(0, 0)
        pnlCreateFarmer.Name = "pnlCreateFarmer"
        pnlCreateFarmer.Size = New Size(1079, 990)
        pnlCreateFarmer.TabIndex = 12
        ' 
        ' btnRegisterFarmer
        ' 
        btnRegisterFarmer.BackColor = Color.FromArgb(CByte(172), CByte(248), CByte(71))
        btnRegisterFarmer.FlatStyle = FlatStyle.Flat
        btnRegisterFarmer.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnRegisterFarmer.ForeColor = Color.DarkGreen
        btnRegisterFarmer.Location = New Point(837, 927)
        btnRegisterFarmer.Name = "btnRegisterFarmer"
        btnRegisterFarmer.Size = New Size(195, 40)
        btnRegisterFarmer.TabIndex = 19
        btnRegisterFarmer.Text = "REGISTER FARMER"
        btnRegisterFarmer.UseVisualStyleBackColor = False
        ' 
        ' btnRegisterCancel
        ' 
        btnRegisterCancel.BackColor = Color.FromArgb(CByte(49), CByte(68), CByte(56))
        btnRegisterCancel.FlatStyle = FlatStyle.Flat
        btnRegisterCancel.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        btnRegisterCancel.ForeColor = Color.White
        btnRegisterCancel.Location = New Point(677, 927)
        btnRegisterCancel.Name = "btnRegisterCancel"
        btnRegisterCancel.Size = New Size(154, 40)
        btnRegisterCancel.TabIndex = 20
        btnRegisterCancel.Text = "CANCEL"
        btnRegisterCancel.UseVisualStyleBackColor = False
        ' 
        ' pnlClassification
        ' 
        pnlClassification.BackColor = Color.White
        pnlClassification.Controls.Add(pnlRegStatus)
        pnlClassification.Controls.Add(lblRegStatus)
        pnlClassification.Controls.Add(lblClassification)
        pnlClassification.Controls.Add(pnlClass)
        pnlClassification.ForeColor = Color.Black
        pnlClassification.Location = New Point(687, 200)
        pnlClassification.Name = "pnlClassification"
        pnlClassification.Size = New Size(345, 243)
        pnlClassification.TabIndex = 18
        ' 
        ' pnlRegStatus
        ' 
        pnlRegStatus.BorderStyle = BorderStyle.FixedSingle
        pnlRegStatus.Controls.Add(cmbRegStatus)
        pnlRegStatus.ForeColor = Color.Black
        pnlRegStatus.Location = New Point(22, 141)
        pnlRegStatus.Name = "pnlRegStatus"
        pnlRegStatus.Size = New Size(283, 46)
        pnlRegStatus.TabIndex = 18
        ' 
        ' cmbRegStatus
        ' 
        cmbRegStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRegStatus.FlatStyle = FlatStyle.Flat
        cmbRegStatus.Font = New Font("Segoe UI", 12F)
        cmbRegStatus.FormattingEnabled = True
        cmbRegStatus.Items.AddRange(New Object() {"NEW FARMER"})
        cmbRegStatus.Location = New Point(3, 8)
        cmbRegStatus.Name = "cmbRegStatus"
        cmbRegStatus.Size = New Size(275, 29)
        cmbRegStatus.TabIndex = 15
        ' 
        ' lblRegStatus
        ' 
        lblRegStatus.AutoSize = True
        lblRegStatus.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblRegStatus.ForeColor = Color.DimGray
        lblRegStatus.ImageAlign = ContentAlignment.MiddleLeft
        lblRegStatus.Location = New Point(22, 119)
        lblRegStatus.Name = "lblRegStatus"
        lblRegStatus.Size = New Size(159, 19)
        lblRegStatus.TabIndex = 19
        lblRegStatus.Text = "REGISTRATION STATUS"
        ' 
        ' lblClassification
        ' 
        lblClassification.AutoSize = True
        lblClassification.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblClassification.ForeColor = Color.DimGray
        lblClassification.ImageAlign = ContentAlignment.MiddleLeft
        lblClassification.Location = New Point(22, 25)
        lblClassification.Name = "lblClassification"
        lblClassification.Size = New Size(116, 19)
        lblClassification.TabIndex = 18
        lblClassification.Text = "CLASSIFICATION"
        ' 
        ' pnlClass
        ' 
        pnlClass.BorderStyle = BorderStyle.FixedSingle
        pnlClass.Controls.Add(cmbClass)
        pnlClass.ForeColor = Color.Black
        pnlClass.Location = New Point(22, 49)
        pnlClass.Name = "pnlClass"
        pnlClass.Size = New Size(283, 46)
        pnlClass.TabIndex = 17
        ' 
        ' cmbClass
        ' 
        cmbClass.DropDownStyle = ComboBoxStyle.DropDownList
        cmbClass.FlatStyle = FlatStyle.Flat
        cmbClass.Font = New Font("Segoe UI", 12F)
        cmbClass.FormattingEnabled = True
        cmbClass.Items.AddRange(New Object() {"INDIVIDUAL", "ASSOCIATION", "COOPERATIVE"})
        cmbClass.Location = New Point(3, 8)
        cmbClass.Name = "cmbClass"
        cmbClass.Size = New Size(275, 29)
        cmbClass.TabIndex = 14
        ' 
        ' lblMainDir2
        ' 
        lblMainDir2.AutoSize = True
        lblMainDir2.Cursor = Cursors.Hand
        lblMainDir2.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblMainDir2.ForeColor = SystemColors.ControlDark
        lblMainDir2.Location = New Point(19, 25)
        lblMainDir2.Name = "lblMainDir2"
        lblMainDir2.Size = New Size(67, 21)
        lblMainDir2.TabIndex = 13
        lblMainDir2.Text = "Farmers"
        ' 
        ' lblSubDir
        ' 
        lblSubDir.AutoSize = True
        lblSubDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblSubDir.ForeColor = Color.ForestGreen
        lblSubDir.Location = New Point(82, 25)
        lblSubDir.Name = "lblSubDir"
        lblSubDir.Size = New Size(127, 21)
        lblSubDir.TabIndex = 14
        lblSubDir.Text = "> Create Farmer"
        ' 
        ' pnlFPersonalInfo
        ' 
        pnlFPersonalInfo.BackColor = Color.White
        pnlFPersonalInfo.Controls.Add(pnlFResidence)
        pnlFPersonalInfo.Controls.Add(lblFResidenceAddress)
        pnlFPersonalInfo.Controls.Add(lblRSBSA)
        pnlFPersonalInfo.Controls.Add(pnlFarmerID)
        pnlFPersonalInfo.Controls.Add(pnlFAge)
        pnlFPersonalInfo.Controls.Add(lblFAge)
        pnlFPersonalInfo.Controls.Add(lblFBirth)
        pnlFPersonalInfo.Controls.Add(pnlFBirth)
        pnlFPersonalInfo.Controls.Add(lblFAdress)
        pnlFPersonalInfo.Controls.Add(pnlFAddress)
        pnlFPersonalInfo.Controls.Add(lblFEmail)
        pnlFPersonalInfo.Controls.Add(pnlFEmail)
        pnlFPersonalInfo.Controls.Add(lblFContactNumber)
        pnlFPersonalInfo.Controls.Add(pnlFContact)
        pnlFPersonalInfo.Controls.Add(lblFFullname)
        pnlFPersonalInfo.Controls.Add(pnlFFullname)
        pnlFPersonalInfo.Controls.Add(lblPersonalInfo)
        pnlFPersonalInfo.ForeColor = Color.Black
        pnlFPersonalInfo.Location = New Point(19, 200)
        pnlFPersonalInfo.Name = "pnlFPersonalInfo"
        pnlFPersonalInfo.Size = New Size(634, 767)
        pnlFPersonalInfo.TabIndex = 7
        ' 
        ' pnlFResidence
        ' 
        pnlFResidence.BorderStyle = BorderStyle.FixedSingle
        pnlFResidence.Controls.Add(lblProvince)
        pnlFResidence.Controls.Add(lblCity)
        pnlFResidence.Controls.Add(lblBarangay)
        pnlFResidence.Controls.Add(cmbProvince)
        pnlFResidence.Controls.Add(cmbCity)
        pnlFResidence.Controls.Add(cmbBarangay)
        pnlFResidence.ForeColor = Color.Black
        pnlFResidence.Location = New Point(21, 458)
        pnlFResidence.Name = "pnlFResidence"
        pnlFResidence.Size = New Size(587, 46)
        pnlFResidence.TabIndex = 14
        ' 
        ' lblProvince
        ' 
        lblProvince.AutoSize = True
        lblProvince.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblProvince.ForeColor = Color.DimGray
        lblProvince.ImageAlign = ContentAlignment.MiddleLeft
        lblProvince.Location = New Point(383, 3)
        lblProvince.Name = "lblProvince"
        lblProvince.Size = New Size(61, 13)
        lblProvince.TabIndex = 13
        lblProvince.Text = "PROVINCE"
        ' 
        ' lblCity
        ' 
        lblCity.AutoSize = True
        lblCity.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblCity.ForeColor = Color.DimGray
        lblCity.ImageAlign = ContentAlignment.MiddleLeft
        lblCity.Location = New Point(201, 2)
        lblCity.Name = "lblCity"
        lblCity.Size = New Size(30, 13)
        lblCity.TabIndex = 12
        lblCity.Text = "CITY"
        ' 
        ' lblBarangay
        ' 
        lblBarangay.AutoSize = True
        lblBarangay.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblBarangay.ForeColor = Color.DimGray
        lblBarangay.ImageAlign = ContentAlignment.MiddleLeft
        lblBarangay.Location = New Point(4, 2)
        lblBarangay.Name = "lblBarangay"
        lblBarangay.Size = New Size(68, 13)
        lblBarangay.TabIndex = 11
        lblBarangay.Text = "BARANGAY"
        ' 
        ' cmbProvince
        ' 
        cmbProvince.DropDownStyle = ComboBoxStyle.DropDownList
        cmbProvince.FlatStyle = FlatStyle.Flat
        cmbProvince.FormattingEnabled = True
        cmbProvince.Items.AddRange(New Object() {"CAMARINES NORTE"})
        cmbProvince.Location = New Point(383, 18)
        cmbProvince.Name = "cmbProvince"
        cmbProvince.Size = New Size(196, 23)
        cmbProvince.TabIndex = 2
        ' 
        ' cmbCity
        ' 
        cmbCity.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCity.FlatStyle = FlatStyle.Flat
        cmbCity.FormattingEnabled = True
        cmbCity.Location = New Point(201, 18)
        cmbCity.Name = "cmbCity"
        cmbCity.Size = New Size(176, 23)
        cmbCity.TabIndex = 1
        ' 
        ' cmbBarangay
        ' 
        cmbBarangay.DropDownStyle = ComboBoxStyle.DropDownList
        cmbBarangay.FlatStyle = FlatStyle.Flat
        cmbBarangay.FormattingEnabled = True
        cmbBarangay.Location = New Point(4, 18)
        cmbBarangay.Name = "cmbBarangay"
        cmbBarangay.Size = New Size(192, 23)
        cmbBarangay.TabIndex = 0
        ' 
        ' lblFResidenceAddress
        ' 
        lblFResidenceAddress.AutoSize = True
        lblFResidenceAddress.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFResidenceAddress.ForeColor = Color.DimGray
        lblFResidenceAddress.ImageAlign = ContentAlignment.MiddleLeft
        lblFResidenceAddress.Location = New Point(21, 434)
        lblFResidenceAddress.Name = "lblFResidenceAddress"
        lblFResidenceAddress.Size = New Size(146, 19)
        lblFResidenceAddress.TabIndex = 17
        lblFResidenceAddress.Text = "RESIDENCE ADDRESS"
        ' 
        ' lblRSBSA
        ' 
        lblRSBSA.AutoSize = True
        lblRSBSA.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblRSBSA.ForeColor = Color.DimGray
        lblRSBSA.ImageAlign = ContentAlignment.MiddleLeft
        lblRSBSA.Location = New Point(21, 87)
        lblRSBSA.Name = "lblRSBSA"
        lblRSBSA.Size = New Size(164, 19)
        lblRSBSA.TabIndex = 16
        lblRSBSA.Text = "RSBSA NO. (FARMER ID)"
        ' 
        ' pnlFarmerID
        ' 
        pnlFarmerID.BorderStyle = BorderStyle.FixedSingle
        pnlFarmerID.Controls.Add(txtFarmerID)
        pnlFarmerID.ForeColor = Color.Black
        pnlFarmerID.Location = New Point(21, 111)
        pnlFarmerID.Name = "pnlFarmerID"
        pnlFarmerID.Size = New Size(283, 46)
        pnlFarmerID.TabIndex = 15
        ' 
        ' txtFarmerID
        ' 
        txtFarmerID.BackColor = Color.White
        txtFarmerID.BorderStyle = BorderStyle.None
        txtFarmerID.Font = New Font("Segoe UI", 12F)
        txtFarmerID.Location = New Point(3, 11)
        txtFarmerID.Name = "txtFarmerID"
        txtFarmerID.ReadOnly = True
        txtFarmerID.Size = New Size(275, 22)
        txtFarmerID.TabIndex = 0
        ' 
        ' pnlFAge
        ' 
        pnlFAge.BorderStyle = BorderStyle.FixedSingle
        pnlFAge.Controls.Add(txtFAge)
        pnlFAge.ForeColor = Color.Black
        pnlFAge.Location = New Point(381, 287)
        pnlFAge.Name = "pnlFAge"
        pnlFAge.Size = New Size(227, 46)
        pnlFAge.TabIndex = 14
        ' 
        ' txtFAge
        ' 
        txtFAge.BackColor = Color.White
        txtFAge.BorderStyle = BorderStyle.None
        txtFAge.CharacterCasing = CharacterCasing.Upper
        txtFAge.Font = New Font("Segoe UI", 12F)
        txtFAge.Location = New Point(3, 10)
        txtFAge.Name = "txtFAge"
        txtFAge.ReadOnly = True
        txtFAge.Size = New Size(219, 22)
        txtFAge.TabIndex = 1
        ' 
        ' lblFAge
        ' 
        lblFAge.AutoSize = True
        lblFAge.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFAge.ForeColor = Color.DimGray
        lblFAge.ImageAlign = ContentAlignment.MiddleLeft
        lblFAge.Location = New Point(381, 263)
        lblFAge.Name = "lblFAge"
        lblFAge.Size = New Size(35, 19)
        lblFAge.TabIndex = 11
        lblFAge.Text = "AGE"
        ' 
        ' lblFBirth
        ' 
        lblFBirth.AutoSize = True
        lblFBirth.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFBirth.ForeColor = Color.DimGray
        lblFBirth.ImageAlign = ContentAlignment.MiddleLeft
        lblFBirth.Location = New Point(21, 263)
        lblFBirth.Name = "lblFBirth"
        lblFBirth.Size = New Size(107, 19)
        lblFBirth.TabIndex = 10
        lblFBirth.Text = "DATE OF BIRTH"
        ' 
        ' pnlFBirth
        ' 
        pnlFBirth.BorderStyle = BorderStyle.FixedSingle
        pnlFBirth.Controls.Add(lblYear)
        pnlFBirth.Controls.Add(lblDay)
        pnlFBirth.Controls.Add(lblMonth)
        pnlFBirth.Controls.Add(cmbYear)
        pnlFBirth.Controls.Add(cmbDay)
        pnlFBirth.Controls.Add(cmbMonth)
        pnlFBirth.ForeColor = Color.Black
        pnlFBirth.Location = New Point(21, 287)
        pnlFBirth.Name = "pnlFBirth"
        pnlFBirth.Size = New Size(339, 46)
        pnlFBirth.TabIndex = 9
        ' 
        ' lblYear
        ' 
        lblYear.AutoSize = True
        lblYear.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblYear.ForeColor = Color.DimGray
        lblYear.ImageAlign = ContentAlignment.MiddleLeft
        lblYear.Location = New Point(236, 3)
        lblYear.Name = "lblYear"
        lblYear.Size = New Size(35, 13)
        lblYear.TabIndex = 13
        lblYear.Text = "YEAR"
        ' 
        ' lblDay
        ' 
        lblDay.AutoSize = True
        lblDay.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblDay.ForeColor = Color.DimGray
        lblDay.ImageAlign = ContentAlignment.MiddleLeft
        lblDay.Location = New Point(133, 2)
        lblDay.Name = "lblDay"
        lblDay.Size = New Size(29, 13)
        lblDay.TabIndex = 12
        lblDay.Text = "DAY"
        ' 
        ' lblMonth
        ' 
        lblMonth.AutoSize = True
        lblMonth.Font = New Font("Segoe UI", 8F, FontStyle.Bold)
        lblMonth.ForeColor = Color.DimGray
        lblMonth.ImageAlign = ContentAlignment.MiddleLeft
        lblMonth.Location = New Point(4, 2)
        lblMonth.Name = "lblMonth"
        lblMonth.Size = New Size(49, 13)
        lblMonth.TabIndex = 11
        lblMonth.Text = "MONTH"
        ' 
        ' cmbYear
        ' 
        cmbYear.DropDownStyle = ComboBoxStyle.DropDownList
        cmbYear.FlatStyle = FlatStyle.Flat
        cmbYear.FormattingEnabled = True
        cmbYear.Items.AddRange(New Object() {"1900", "1901", "1902", "1903", "1904", "1905", "1906", "1907", "1908", "1909", "1910", "1911", "1912", "1913", "1914", "1915", "1916", "1917", "1918", "1919", "1920", "1921", "1922", "1923", "1924", "1925", "1926", "1927", "1928", "1929", "1930", "1931", "1932", "1933", "1934", "1935", "1936", "1937", "1938", "1939", "1940", "1941", "1942", "1943", "1944", "1945", "1946", "1947", "1948", "1949", "1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026"})
        cmbYear.Location = New Point(236, 18)
        cmbYear.Name = "cmbYear"
        cmbYear.Size = New Size(98, 23)
        cmbYear.TabIndex = 2
        ' 
        ' cmbDay
        ' 
        cmbDay.DropDownStyle = ComboBoxStyle.DropDownList
        cmbDay.FlatStyle = FlatStyle.Flat
        cmbDay.FormattingEnabled = True
        cmbDay.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31"})
        cmbDay.Location = New Point(133, 18)
        cmbDay.Name = "cmbDay"
        cmbDay.Size = New Size(98, 23)
        cmbDay.TabIndex = 1
        ' 
        ' cmbMonth
        ' 
        cmbMonth.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMonth.FlatStyle = FlatStyle.Flat
        cmbMonth.FormattingEnabled = True
        cmbMonth.Items.AddRange(New Object() {"JANUARY", "FEBRUARY", "MARCH", "APRIL", "MAY", "JUNE", "JULY", "AUGUST", "SEPTEMBER", "OCTOBER", "NOVEMBER", "DECEMBER"})
        cmbMonth.Location = New Point(4, 18)
        cmbMonth.Name = "cmbMonth"
        cmbMonth.Size = New Size(123, 23)
        cmbMonth.TabIndex = 0
        ' 
        ' lblFAdress
        ' 
        lblFAdress.AutoSize = True
        lblFAdress.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFAdress.ForeColor = Color.DimGray
        lblFAdress.ImageAlign = ContentAlignment.MiddleLeft
        lblFAdress.Location = New Point(21, 519)
        lblFAdress.Name = "lblFAdress"
        lblFAdress.Size = New Size(282, 19)
        lblFAdress.TabIndex = 8
        lblFAdress.Text = "FARM ADDRESS (INCLUDING LANDMARK)"
        ' 
        ' pnlFAddress
        ' 
        pnlFAddress.BorderStyle = BorderStyle.FixedSingle
        pnlFAddress.Controls.Add(txtFAddress)
        pnlFAddress.ForeColor = Color.Black
        pnlFAddress.Location = New Point(21, 541)
        pnlFAddress.Name = "pnlFAddress"
        pnlFAddress.Size = New Size(585, 104)
        pnlFAddress.TabIndex = 7
        ' 
        ' txtFAddress
        ' 
        txtFAddress.BorderStyle = BorderStyle.None
        txtFAddress.CharacterCasing = CharacterCasing.Upper
        txtFAddress.Font = New Font("Segoe UI", 12F)
        txtFAddress.Location = New Point(3, 5)
        txtFAddress.Multiline = True
        txtFAddress.Name = "txtFAddress"
        txtFAddress.Size = New Size(577, 92)
        txtFAddress.TabIndex = 0
        ' 
        ' lblFEmail
        ' 
        lblFEmail.AutoSize = True
        lblFEmail.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFEmail.ForeColor = Color.DimGray
        lblFEmail.ImageAlign = ContentAlignment.MiddleLeft
        lblFEmail.Location = New Point(21, 350)
        lblFEmail.Name = "lblFEmail"
        lblFEmail.Size = New Size(197, 19)
        lblFEmail.TabIndex = 6
        lblFEmail.Text = "EMAIL ADDRESS (OPTIONAL)"
        ' 
        ' pnlFEmail
        ' 
        pnlFEmail.BorderStyle = BorderStyle.FixedSingle
        pnlFEmail.Controls.Add(txtFEmail)
        pnlFEmail.ForeColor = Color.Black
        pnlFEmail.Location = New Point(21, 374)
        pnlFEmail.Name = "pnlFEmail"
        pnlFEmail.Size = New Size(283, 46)
        pnlFEmail.TabIndex = 5
        ' 
        ' txtFEmail
        ' 
        txtFEmail.BorderStyle = BorderStyle.None
        txtFEmail.CharacterCasing = CharacterCasing.Lower
        txtFEmail.Font = New Font("Segoe UI", 12F)
        txtFEmail.Location = New Point(3, 11)
        txtFEmail.Name = "txtFEmail"
        txtFEmail.Size = New Size(275, 22)
        txtFEmail.TabIndex = 0
        ' 
        ' lblFContactNumber
        ' 
        lblFContactNumber.AutoSize = True
        lblFContactNumber.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFContactNumber.ForeColor = Color.DimGray
        lblFContactNumber.ImageAlign = ContentAlignment.MiddleLeft
        lblFContactNumber.Location = New Point(320, 350)
        lblFContactNumber.Name = "lblFContactNumber"
        lblFContactNumber.Size = New Size(135, 19)
        lblFContactNumber.TabIndex = 4
        lblFContactNumber.Text = "CONTACT NUMBER"
        ' 
        ' pnlFContact
        ' 
        pnlFContact.BorderStyle = BorderStyle.FixedSingle
        pnlFContact.Controls.Add(txtFContact)
        pnlFContact.ForeColor = Color.Black
        pnlFContact.Location = New Point(324, 374)
        pnlFContact.Name = "pnlFContact"
        pnlFContact.Size = New Size(283, 46)
        pnlFContact.TabIndex = 3
        ' 
        ' txtFContact
        ' 
        txtFContact.BorderStyle = BorderStyle.None
        txtFContact.CharacterCasing = CharacterCasing.Upper
        txtFContact.Font = New Font("Segoe UI", 12F)
        txtFContact.Location = New Point(3, 11)
        txtFContact.MaxLength = 11
        txtFContact.Name = "txtFContact"
        txtFContact.Size = New Size(275, 22)
        txtFContact.TabIndex = 0
        ' 
        ' lblFFullname
        ' 
        lblFFullname.AutoSize = True
        lblFFullname.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFFullname.ForeColor = Color.DimGray
        lblFFullname.ImageAlign = ContentAlignment.MiddleLeft
        lblFFullname.Location = New Point(21, 181)
        lblFFullname.Name = "lblFFullname"
        lblFFullname.Size = New Size(444, 19)
        lblFFullname.TabIndex = 2
        lblFFullname.Text = "FULL NAME (LAST NAME, FIRST NAME, MIDDILE INITIAL, EX. NAME)"
        ' 
        ' pnlFFullname
        ' 
        pnlFFullname.BorderStyle = BorderStyle.FixedSingle
        pnlFFullname.Controls.Add(txtFFullname)
        pnlFFullname.ForeColor = Color.Black
        pnlFFullname.Location = New Point(21, 205)
        pnlFFullname.Name = "pnlFFullname"
        pnlFFullname.Size = New Size(587, 46)
        pnlFFullname.TabIndex = 1
        ' 
        ' txtFFullname
        ' 
        txtFFullname.BorderStyle = BorderStyle.None
        txtFFullname.CharacterCasing = CharacterCasing.Upper
        txtFFullname.Font = New Font("Segoe UI", 12F)
        txtFFullname.Location = New Point(3, 11)
        txtFFullname.Name = "txtFFullname"
        txtFFullname.Size = New Size(579, 22)
        txtFFullname.TabIndex = 0
        ' 
        ' lblPersonalInfo
        ' 
        lblPersonalInfo.AutoSize = True
        lblPersonalInfo.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblPersonalInfo.ForeColor = Color.DimGray
        lblPersonalInfo.Image = CType(resources.GetObject("lblPersonalInfo.Image"), Image)
        lblPersonalInfo.ImageAlign = ContentAlignment.MiddleLeft
        lblPersonalInfo.Location = New Point(26, 25)
        lblPersonalInfo.Name = "lblPersonalInfo"
        lblPersonalInfo.Size = New Size(268, 25)
        lblPersonalInfo.TabIndex = 0
        lblPersonalInfo.Text = "     PERSONAL INFORMATION"
        ' 
        ' lblFCreateSubtitle
        ' 
        lblFCreateSubtitle.AutoSize = True
        lblFCreateSubtitle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblFCreateSubtitle.ForeColor = SystemColors.ControlDarkDark
        lblFCreateSubtitle.Location = New Point(19, 125)
        lblFCreateSubtitle.Name = "lblFCreateSubtitle"
        lblFCreateSubtitle.Size = New Size(275, 42)
        lblFCreateSubtitle.TabIndex = 6
        lblFCreateSubtitle.Text = "Register a new agricultural producer" & vbCrLf & vbCrLf
        ' 
        ' lblCreateFarmer
        ' 
        lblCreateFarmer.AutoSize = True
        lblCreateFarmer.Font = New Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblCreateFarmer.ForeColor = Color.Black
        lblCreateFarmer.Location = New Point(12, 71)
        lblCreateFarmer.Name = "lblCreateFarmer"
        lblCreateFarmer.Size = New Size(432, 54)
        lblCreateFarmer.TabIndex = 5
        lblCreateFarmer.Text = "Create Farmer Record"
        ' 
        ' pnlCreateService
        ' 
        pnlCreateService.Controls.Add(ImagePolicy)
        pnlCreateService.Controls.Add(btnSaveService)
        pnlCreateService.Controls.Add(btnServiceCancel)
        pnlCreateService.Controls.Add(imgRental)
        pnlCreateService.Controls.Add(lblServiceMainDir)
        pnlCreateService.Controls.Add(lblServiceSubDir)
        pnlCreateService.Controls.Add(pnlServiceInfo)
        pnlCreateService.Controls.Add(lblAddServiceSubtitle)
        pnlCreateService.Controls.Add(lblAddService)
        pnlCreateService.Location = New Point(0, 0)
        pnlCreateService.Name = "pnlCreateService"
        pnlCreateService.Size = New Size(1364, 990)
        pnlCreateService.TabIndex = 29
        ' 
        ' btnSaveService
        ' 
        btnSaveService.BackColor = Color.FromArgb(CByte(172), CByte(248), CByte(71))
        btnSaveService.FlatStyle = FlatStyle.Flat
        btnSaveService.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        btnSaveService.ForeColor = Color.DarkGreen
        btnSaveService.Location = New Point(1126, 908)
        btnSaveService.Name = "btnSaveService"
        btnSaveService.Size = New Size(195, 40)
        btnSaveService.TabIndex = 19
        btnSaveService.Text = "Save Service"
        btnSaveService.UseVisualStyleBackColor = False
        ' 
        ' btnServiceCancel
        ' 
        btnServiceCancel.BackColor = Color.FromArgb(CByte(49), CByte(68), CByte(56))
        btnServiceCancel.FlatStyle = FlatStyle.Flat
        btnServiceCancel.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        btnServiceCancel.ForeColor = Color.White
        btnServiceCancel.Location = New Point(966, 908)
        btnServiceCancel.Name = "btnServiceCancel"
        btnServiceCancel.Size = New Size(154, 40)
        btnServiceCancel.TabIndex = 20
        btnServiceCancel.Text = "CANCEL"
        btnServiceCancel.UseVisualStyleBackColor = False
        ' 
        ' imgRental
        ' 
        imgRental.BackColor = Color.White
        imgRental.Location = New Point(668, 200)
        imgRental.Name = "imgRental"
        imgRental.Size = New Size(668, 504)
        imgRental.TabIndex = 15
        imgRental.TabStop = False
        ' 
        ' lblServiceMainDir
        ' 
        lblServiceMainDir.AutoEllipsis = True
        lblServiceMainDir.AutoSize = True
        lblServiceMainDir.Cursor = Cursors.Hand
        lblServiceMainDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceMainDir.ForeColor = SystemColors.ControlDark
        lblServiceMainDir.Location = New Point(19, 25)
        lblServiceMainDir.Name = "lblServiceMainDir"
        lblServiceMainDir.Size = New Size(124, 21)
        lblServiceMainDir.TabIndex = 13
        lblServiceMainDir.Text = "Service Catalog"
        ' 
        ' lblServiceSubDir
        ' 
        lblServiceSubDir.AutoSize = True
        lblServiceSubDir.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceSubDir.ForeColor = Color.ForestGreen
        lblServiceSubDir.Location = New Point(139, 25)
        lblServiceSubDir.Name = "lblServiceSubDir"
        lblServiceSubDir.Size = New Size(114, 21)
        lblServiceSubDir.TabIndex = 14
        lblServiceSubDir.Text = "> Add Service"
        ' 
        ' pnlServiceInfo
        ' 
        pnlServiceInfo.BackColor = Color.White
        pnlServiceInfo.Controls.Add(lblMachineID)
        pnlServiceInfo.Controls.Add(pnlMachineryId)
        pnlServiceInfo.Controls.Add(lblServiceID)
        pnlServiceInfo.Controls.Add(pnlServiceID)
        pnlServiceInfo.Controls.Add(lblSEmployeeID)
        pnlServiceInfo.Controls.Add(pnlSEmployee)
        pnlServiceInfo.Controls.Add(lblServiceDescription)
        pnlServiceInfo.Controls.Add(pnlServiceDescription)
        pnlServiceInfo.Controls.Add(lblServiceType)
        pnlServiceInfo.Controls.Add(pnlServiceType)
        pnlServiceInfo.Controls.Add(lblPolicyLimit)
        pnlServiceInfo.Controls.Add(pnlPolicyLimit)
        pnlServiceInfo.Controls.Add(lblServiceName)
        pnlServiceInfo.Controls.Add(pnlServiceName)
        pnlServiceInfo.Controls.Add(lblServiceInformation)
        pnlServiceInfo.ForeColor = Color.Black
        pnlServiceInfo.Location = New Point(19, 200)
        pnlServiceInfo.Name = "pnlServiceInfo"
        pnlServiceInfo.Size = New Size(634, 767)
        pnlServiceInfo.TabIndex = 7
        ' 
        ' lblMachineID
        ' 
        lblMachineID.AutoSize = True
        lblMachineID.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblMachineID.ForeColor = Color.DimGray
        lblMachineID.ImageAlign = ContentAlignment.MiddleLeft
        lblMachineID.Location = New Point(21, 350)
        lblMachineID.Name = "lblMachineID"
        lblMachineID.Size = New Size(81, 19)
        lblMachineID.TabIndex = 20
        lblMachineID.Text = "Machine ID"
        ' 
        ' pnlMachineryId
        ' 
        pnlMachineryId.BorderStyle = BorderStyle.FixedSingle
        pnlMachineryId.Controls.Add(cmbMachineryID)
        pnlMachineryId.ForeColor = Color.Black
        pnlMachineryId.Location = New Point(21, 374)
        pnlMachineryId.Name = "pnlMachineryId"
        pnlMachineryId.Size = New Size(283, 46)
        pnlMachineryId.TabIndex = 19
        ' 
        ' cmbMachineryID
        ' 
        cmbMachineryID.DropDownStyle = ComboBoxStyle.DropDownList
        cmbMachineryID.FlatStyle = FlatStyle.Flat
        cmbMachineryID.Font = New Font("Segoe UI", 12F)
        cmbMachineryID.FormattingEnabled = True
        cmbMachineryID.Items.AddRange(New Object() {"WALK-BEHIND TRANSPLANTER", "RIDE-IN TYPE TRANSPLANTER", "DC35 COMBINE HARVESTER", "DC60 COMBINE HARVESTER", "DC70 COMBINE HARVESTER", "M9540 TRACTOR", "L3608 TRACTOR", "TYM TRACTOR (BACKHOE, ROTAVATOR, PLOUGH)", "CORN SHELLER", "FLATBED DRYER"})
        cmbMachineryID.Location = New Point(3, 9)
        cmbMachineryID.Name = "cmbMachineryID"
        cmbMachineryID.Size = New Size(275, 29)
        cmbMachineryID.TabIndex = 15
        ' 
        ' lblServiceID
        ' 
        lblServiceID.AutoSize = True
        lblServiceID.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblServiceID.ForeColor = Color.DimGray
        lblServiceID.ImageAlign = ContentAlignment.MiddleLeft
        lblServiceID.Location = New Point(21, 87)
        lblServiceID.Name = "lblServiceID"
        lblServiceID.Size = New Size(80, 19)
        lblServiceID.TabIndex = 16
        lblServiceID.Text = "SERVICE ID"
        ' 
        ' pnlServiceID
        ' 
        pnlServiceID.BorderStyle = BorderStyle.FixedSingle
        pnlServiceID.Controls.Add(txtServiceID)
        pnlServiceID.ForeColor = Color.Black
        pnlServiceID.Location = New Point(21, 111)
        pnlServiceID.Name = "pnlServiceID"
        pnlServiceID.Size = New Size(586, 46)
        pnlServiceID.TabIndex = 15
        ' 
        ' txtServiceID
        ' 
        txtServiceID.BackColor = Color.White
        txtServiceID.BorderStyle = BorderStyle.None
        txtServiceID.Font = New Font("Segoe UI", 12F)
        txtServiceID.Location = New Point(3, 11)
        txtServiceID.Name = "txtServiceID"
        txtServiceID.ReadOnly = True
        txtServiceID.Size = New Size(579, 22)
        txtServiceID.TabIndex = 0
        ' 
        ' lblSEmployeeID
        ' 
        lblSEmployeeID.AutoSize = True
        lblSEmployeeID.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblSEmployeeID.ForeColor = Color.DimGray
        lblSEmployeeID.ImageAlign = ContentAlignment.MiddleLeft
        lblSEmployeeID.Location = New Point(15, 611)
        lblSEmployeeID.Name = "lblSEmployeeID"
        lblSEmployeeID.Size = New Size(95, 19)
        lblSEmployeeID.TabIndex = 18
        lblSEmployeeID.Text = "EMPLOYEE ID"
        ' 
        ' pnlSEmployee
        ' 
        pnlSEmployee.BorderStyle = BorderStyle.FixedSingle
        pnlSEmployee.Controls.Add(cmbSEmployeeID)
        pnlSEmployee.ForeColor = Color.Black
        pnlSEmployee.Location = New Point(15, 635)
        pnlSEmployee.Name = "pnlSEmployee"
        pnlSEmployee.Size = New Size(586, 46)
        pnlSEmployee.TabIndex = 17
        ' 
        ' cmbSEmployeeID
        ' 
        cmbSEmployeeID.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSEmployeeID.FlatStyle = FlatStyle.Flat
        cmbSEmployeeID.Font = New Font("Segoe UI", 12F)
        cmbSEmployeeID.FormattingEnabled = True
        cmbSEmployeeID.Location = New Point(3, 8)
        cmbSEmployeeID.Name = "cmbSEmployeeID"
        cmbSEmployeeID.Size = New Size(576, 29)
        cmbSEmployeeID.TabIndex = 14
        ' 
        ' lblServiceDescription
        ' 
        lblServiceDescription.AutoSize = True
        lblServiceDescription.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblServiceDescription.ForeColor = Color.DimGray
        lblServiceDescription.ImageAlign = ContentAlignment.MiddleLeft
        lblServiceDescription.Location = New Point(15, 477)
        lblServiceDescription.Name = "lblServiceDescription"
        lblServiceDescription.Size = New Size(155, 19)
        lblServiceDescription.TabIndex = 8
        lblServiceDescription.Text = "SERVICE DESCRIPTION"
        ' 
        ' pnlServiceDescription
        ' 
        pnlServiceDescription.BorderStyle = BorderStyle.FixedSingle
        pnlServiceDescription.Controls.Add(txtServiceDescription)
        pnlServiceDescription.ForeColor = Color.Black
        pnlServiceDescription.Location = New Point(15, 499)
        pnlServiceDescription.Name = "pnlServiceDescription"
        pnlServiceDescription.Size = New Size(585, 104)
        pnlServiceDescription.TabIndex = 7
        ' 
        ' txtServiceDescription
        ' 
        txtServiceDescription.BorderStyle = BorderStyle.None
        txtServiceDescription.CharacterCasing = CharacterCasing.Upper
        txtServiceDescription.Font = New Font("Segoe UI", 12F)
        txtServiceDescription.Location = New Point(3, 5)
        txtServiceDescription.Multiline = True
        txtServiceDescription.Name = "txtServiceDescription"
        txtServiceDescription.Size = New Size(577, 92)
        txtServiceDescription.TabIndex = 0
        ' 
        ' lblServiceType
        ' 
        lblServiceType.AutoSize = True
        lblServiceType.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblServiceType.ForeColor = Color.DimGray
        lblServiceType.ImageAlign = ContentAlignment.MiddleLeft
        lblServiceType.Location = New Point(21, 265)
        lblServiceType.Name = "lblServiceType"
        lblServiceType.Size = New Size(97, 19)
        lblServiceType.TabIndex = 6
        lblServiceType.Text = "SERVICE TYPE"
        ' 
        ' pnlServiceType
        ' 
        pnlServiceType.BorderStyle = BorderStyle.FixedSingle
        pnlServiceType.Controls.Add(cmbServiceType)
        pnlServiceType.ForeColor = Color.Black
        pnlServiceType.Location = New Point(21, 289)
        pnlServiceType.Name = "pnlServiceType"
        pnlServiceType.Size = New Size(283, 46)
        pnlServiceType.TabIndex = 5
        ' 
        ' cmbServiceType
        ' 
        cmbServiceType.DropDownStyle = ComboBoxStyle.DropDownList
        cmbServiceType.FlatStyle = FlatStyle.Flat
        cmbServiceType.Font = New Font("Segoe UI", 12F)
        cmbServiceType.FormattingEnabled = True
        cmbServiceType.Items.AddRange(New Object() {"LAND PREPERATION", "PLANTING", "HARVESTING", "POST-HARVEST"})
        cmbServiceType.Location = New Point(3, 9)
        cmbServiceType.Name = "cmbServiceType"
        cmbServiceType.Size = New Size(275, 29)
        cmbServiceType.TabIndex = 15
        ' 
        ' lblPolicyLimit
        ' 
        lblPolicyLimit.AutoSize = True
        lblPolicyLimit.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblPolicyLimit.ForeColor = Color.DimGray
        lblPolicyLimit.ImageAlign = ContentAlignment.MiddleLeft
        lblPolicyLimit.Location = New Point(327, 265)
        lblPolicyLimit.Name = "lblPolicyLimit"
        lblPolicyLimit.Size = New Size(96, 19)
        lblPolicyLimit.TabIndex = 4
        lblPolicyLimit.Text = "POLICY LIMIT"
        ' 
        ' pnlPolicyLimit
        ' 
        pnlPolicyLimit.BorderStyle = BorderStyle.FixedSingle
        pnlPolicyLimit.Controls.Add(txtPolicyLimit)
        pnlPolicyLimit.ForeColor = Color.Black
        pnlPolicyLimit.Location = New Point(327, 289)
        pnlPolicyLimit.Name = "pnlPolicyLimit"
        pnlPolicyLimit.Size = New Size(283, 46)
        pnlPolicyLimit.TabIndex = 3
        ' 
        ' txtPolicyLimit
        ' 
        txtPolicyLimit.BorderStyle = BorderStyle.None
        txtPolicyLimit.CharacterCasing = CharacterCasing.Upper
        txtPolicyLimit.Font = New Font("Segoe UI", 12F)
        txtPolicyLimit.Location = New Point(3, 11)
        txtPolicyLimit.Name = "txtPolicyLimit"
        txtPolicyLimit.Size = New Size(275, 22)
        txtPolicyLimit.TabIndex = 1
        ' 
        ' lblServiceName
        ' 
        lblServiceName.AutoSize = True
        lblServiceName.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblServiceName.ForeColor = Color.DimGray
        lblServiceName.ImageAlign = ContentAlignment.MiddleLeft
        lblServiceName.Location = New Point(21, 181)
        lblServiceName.Name = "lblServiceName"
        lblServiceName.Size = New Size(106, 19)
        lblServiceName.TabIndex = 2
        lblServiceName.Text = "SERVICE NAME"
        ' 
        ' pnlServiceName
        ' 
        pnlServiceName.BorderStyle = BorderStyle.FixedSingle
        pnlServiceName.Controls.Add(txtServiceName)
        pnlServiceName.ForeColor = Color.Black
        pnlServiceName.Location = New Point(21, 205)
        pnlServiceName.Name = "pnlServiceName"
        pnlServiceName.Size = New Size(587, 46)
        pnlServiceName.TabIndex = 1
        ' 
        ' txtServiceName
        ' 
        txtServiceName.BorderStyle = BorderStyle.None
        txtServiceName.CharacterCasing = CharacterCasing.Upper
        txtServiceName.Font = New Font("Segoe UI", 12F)
        txtServiceName.Location = New Point(3, 11)
        txtServiceName.Name = "txtServiceName"
        txtServiceName.Size = New Size(579, 22)
        txtServiceName.TabIndex = 0
        ' 
        ' lblServiceInformation
        ' 
        lblServiceInformation.AutoSize = True
        lblServiceInformation.Font = New Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblServiceInformation.ForeColor = Color.DimGray
        lblServiceInformation.Image = CType(resources.GetObject("lblServiceInformation.Image"), Image)
        lblServiceInformation.ImageAlign = ContentAlignment.MiddleLeft
        lblServiceInformation.Location = New Point(26, 25)
        lblServiceInformation.Name = "lblServiceInformation"
        lblServiceInformation.Size = New Size(217, 25)
        lblServiceInformation.TabIndex = 0
        lblServiceInformation.Text = "       Service Information"
        ' 
        ' lblAddServiceSubtitle
        ' 
        lblAddServiceSubtitle.AutoSize = True
        lblAddServiceSubtitle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAddServiceSubtitle.ForeColor = SystemColors.ControlDarkDark
        lblAddServiceSubtitle.Location = New Point(19, 125)
        lblAddServiceSubtitle.Name = "lblAddServiceSubtitle"
        lblAddServiceSubtitle.Size = New Size(415, 21)
        lblAddServiceSubtitle.TabIndex = 6
        lblAddServiceSubtitle.Text = "Create new service entries to enhance service coverage"
        ' 
        ' lblAddService
        ' 
        lblAddService.AutoSize = True
        lblAddService.Font = New Font("Segoe UI", 30F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        lblAddService.ForeColor = Color.Black
        lblAddService.Location = New Point(12, 71)
        lblAddService.Name = "lblAddService"
        lblAddService.Size = New Size(248, 54)
        lblAddService.TabIndex = 5
        lblAddService.Text = "Add Service"
        ' 
        ' ImagePolicy
        ' 
        ImagePolicy.Image = CType(resources.GetObject("ImagePolicy.Image"), Image)
        ImagePolicy.Location = New Point(677, 213)
        ImagePolicy.Name = "ImagePolicy"
        ImagePolicy.Size = New Size(644, 491)
        ImagePolicy.SizeMode = PictureBoxSizeMode.StretchImage
        ImagePolicy.TabIndex = 21
        ImagePolicy.TabStop = False
        ' 
        ' frmPanelHolder
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1924, 1041)
        Controls.Add(pnlCreateService)
        Controls.Add(pnlServices)
        Controls.Add(pnlRequests)
        Controls.Add(pnlCreateFarmer)
        Controls.Add(pnlFarmers)
        Controls.Add(pnlOperator)
        Controls.Add(pnlMachinery)
        Controls.Add(pnlEmployee)
        Controls.Add(pnlStation)
        Controls.Add(pnlConfig)
        Name = "frmPanelHolder"
        Text = "Form1"
        WindowState = FormWindowState.Maximized
        pnlConfig.ResumeLayout(False)
        pnlConfig.PerformLayout()
        pnlConfigForm.ResumeLayout(False)
        pnlConfigForm.PerformLayout()
        CType(pbLogo, ComponentModel.ISupportInitialize).EndInit()
        pnlFarmers.ResumeLayout(False)
        pnlFarmers.PerformLayout()
        pnlFarmersDataGrid.ResumeLayout(False)
        CType(dgvFarmers, ComponentModel.ISupportInitialize).EndInit()
        pnlFarmersSearch.ResumeLayout(False)
        pnlFarmersSearch.PerformLayout()
        pnlPendingValidation.ResumeLayout(False)
        pnlPendingValidation.PerformLayout()
        pnlTotalFarmers.ResumeLayout(False)
        pnlTotalFarmers.PerformLayout()
        pnlServices.ResumeLayout(False)
        pnlServices.PerformLayout()
        pnlDGVService.ResumeLayout(False)
        CType(dgvServices, ComponentModel.ISupportInitialize).EndInit()
        pnlSearchService.ResumeLayout(False)
        pnlSearchService.PerformLayout()
        pnlTotalService.ResumeLayout(False)
        pnlTotalService.PerformLayout()
        pnlOperator.ResumeLayout(False)
        pnlOperator.PerformLayout()
        pnlMachinery.ResumeLayout(False)
        pnlMachinery.PerformLayout()
        pnlRequests.ResumeLayout(False)
        pnlRequests.PerformLayout()
        pnlDGVRequests.ResumeLayout(False)
        CType(dgvRequests, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel4.ResumeLayout(False)
        Panel4.PerformLayout()
        pnlEmployee.ResumeLayout(False)
        pnlEmployee.PerformLayout()
        pnlStation.ResumeLayout(False)
        pnlStation.PerformLayout()
        pnlCreateFarmer.ResumeLayout(False)
        pnlCreateFarmer.PerformLayout()
        pnlClassification.ResumeLayout(False)
        pnlClassification.PerformLayout()
        pnlRegStatus.ResumeLayout(False)
        pnlClass.ResumeLayout(False)
        pnlFPersonalInfo.ResumeLayout(False)
        pnlFPersonalInfo.PerformLayout()
        pnlFResidence.ResumeLayout(False)
        pnlFResidence.PerformLayout()
        pnlFarmerID.ResumeLayout(False)
        pnlFarmerID.PerformLayout()
        pnlFAge.ResumeLayout(False)
        pnlFAge.PerformLayout()
        pnlFBirth.ResumeLayout(False)
        pnlFBirth.PerformLayout()
        pnlFAddress.ResumeLayout(False)
        pnlFAddress.PerformLayout()
        pnlFEmail.ResumeLayout(False)
        pnlFEmail.PerformLayout()
        pnlFContact.ResumeLayout(False)
        pnlFContact.PerformLayout()
        pnlFFullname.ResumeLayout(False)
        pnlFFullname.PerformLayout()
        pnlCreateService.ResumeLayout(False)
        pnlCreateService.PerformLayout()
        CType(imgRental, ComponentModel.ISupportInitialize).EndInit()
        pnlServiceInfo.ResumeLayout(False)
        pnlServiceInfo.PerformLayout()
        pnlMachineryId.ResumeLayout(False)
        pnlServiceID.ResumeLayout(False)
        pnlServiceID.PerformLayout()
        pnlSEmployee.ResumeLayout(False)
        pnlServiceDescription.ResumeLayout(False)
        pnlServiceDescription.PerformLayout()
        pnlServiceType.ResumeLayout(False)
        pnlPolicyLimit.ResumeLayout(False)
        pnlPolicyLimit.PerformLayout()
        pnlServiceName.ResumeLayout(False)
        pnlServiceName.PerformLayout()
        CType(ImagePolicy, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlConfig As Panel
    Friend WithEvents pbLogo As PictureBox
    Friend WithEvents lblPABEO As Label
    Friend WithEvents lblServer As Label
    Friend WithEvents txtPWD As TextBox
    Friend WithEvents txtUID As TextBox
    Friend WithEvents txtDatabase As TextBox
    Friend WithEvents txtServer As TextBox
    Friend WithEvents lblPWD As Label
    Friend WithEvents lblUsername As Label
    Friend WithEvents lblDatabase As Label
    Friend WithEvents btnConnect As Button
    Friend WithEvents MySqlConnection1 As MySql.Data.MySqlClient.MySqlConnection
    Friend WithEvents pnlConfigForm As Panel
    Friend WithEvents pnlFarmers As Panel
    Friend WithEvents pnlServices As Panel
    Friend WithEvents pnlOperator As Panel
    Friend WithEvents pnlMachinery As Panel
    Friend WithEvents pnlRequests As Panel
    Friend WithEvents pnlEmployee As Panel
    Friend WithEvents pnlStation As Panel
    Friend WithEvents Label3 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lblStation As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblFarmerSubtitle As Label
    Friend WithEvents lblFarmerHeader As Label
    Friend WithEvents pnlTotalFarmers As Panel
    Friend WithEvents pnlPendingValidation As Panel
    Friend WithEvents MySqlCommand2 As MySql.Data.MySqlClient.MySqlCommand
    Friend WithEvents dgvFarmers As DataGridView
    Friend WithEvents lblPendingViolation As Label
    Friend WithEvents lblTotalFarmers As Label
    Friend WithEvents lblTotalPending As Label
    Friend WithEvents lblFarmerTotal As Label
    Friend WithEvents txtFarmersSearch As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents pnlFarmersSearch As Panel
    Friend WithEvents pnlFarmersDataGrid As Panel
    Friend WithEvents btnAddFarmer As Button
    Friend WithEvents btnFarmerExport As Button
    Friend WithEvents pnlCreateFarmer As Panel
    Friend WithEvents lblFCreateSubtitle As Label
    Friend WithEvents lblCreateFarmer As Label
    Friend WithEvents pnlFPersonalInfo As Panel
    Friend WithEvents pnlFFullname As Panel
    Friend WithEvents lblPersonalInfo As Label
    Friend WithEvents lblFContactNumber As Label
    Friend WithEvents pnlFContact As Panel
    Friend WithEvents txtFContact As TextBox
    Friend WithEvents lblFFullname As Label
    Friend WithEvents txtFFullname As TextBox
    Friend WithEvents lblFAdress As Label
    Friend WithEvents pnlFAddress As Panel
    Friend WithEvents txtFAddress As TextBox
    Friend WithEvents lblFEmail As Label
    Friend WithEvents pnlFEmail As Panel
    Friend WithEvents txtFEmail As TextBox
    Friend WithEvents lblFBirth As Label
    Friend WithEvents pnlFBirth As Panel
    Friend WithEvents lblYear As Label
    Friend WithEvents lblDay As Label
    Friend WithEvents lblMonth As Label
    Friend WithEvents cmbYear As ComboBox
    Friend WithEvents cmbDay As ComboBox
    Friend WithEvents cmbMonth As ComboBox
    Friend WithEvents lblRSBSA As Label
    Friend WithEvents pnlFarmerID As Panel
    Friend WithEvents txtFarmerID As TextBox
    Friend WithEvents pnlFAge As Panel
    Friend WithEvents txtFAge As TextBox
    Friend WithEvents lblFAge As Label
    Friend WithEvents pnlFResidence As Panel
    Friend WithEvents lblProvince As Label
    Friend WithEvents lblCity As Label
    Friend WithEvents lblBarangay As Label
    Friend WithEvents cmbProvince As ComboBox
    Friend WithEvents cmbCity As ComboBox
    Friend WithEvents cmbBarangay As ComboBox
    Friend WithEvents lblFResidenceAddress As Label
    Friend WithEvents lblMainDir As Label
    Friend WithEvents lblMainDir2 As Label
    Friend WithEvents lblSubDir As Label
    Friend WithEvents pnlClassification As Panel
    Friend WithEvents lblClassification As Label
    Friend WithEvents pnlClass As Panel
    Friend WithEvents cmbClass As ComboBox
    Friend WithEvents btnRegisterCancel As Button
    Friend WithEvents btnRegisterFarmer As Button
    Friend WithEvents pnlRegStatus As Panel
    Friend WithEvents cmbRegStatus As ComboBox
    Friend WithEvents lblRegStatus As Label
    Friend WithEvents formatted_id As DataGridViewTextBoxColumn
    Friend WithEvents full_name As DataGridViewTextBoxColumn
    Friend WithEvents residence_address As DataGridViewTextBoxColumn
    Friend WithEvents contact_number As DataGridViewTextBoxColumn
    Friend WithEvents classification As DataGridViewTextBoxColumn
    Friend WithEvents registration_status As DataGridViewTextBoxColumn
    Friend WithEvents FarmerEdit As DataGridViewImageColumn
    Friend WithEvents FarmerDelete As DataGridViewImageColumn
    Friend WithEvents lblRequestsDir As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents pnlDGVRequests As Panel
    Friend WithEvents dgvRequests As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn1 As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn2 As DataGridViewImageColumn
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents lblRequestsHeader As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents lblAmountPendingRequests As Label
    Friend WithEvents lblTotalPendingRequsts As Label
    Friend WithEvents lblServiceDir As Label
    Friend WithEvents btnAddService As Button
    Friend WithEvents pnlDGVService As Panel
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewImageColumn3 As DataGridViewImageColumn
    Friend WithEvents DataGridViewImageColumn4 As DataGridViewImageColumn
    Friend WithEvents pnlSearchService As Panel
    Friend WithEvents txtServiceSearch As TextBox
    Friend WithEvents btnServiceSearch As Button
    Friend WithEvents lblServiceSubtitle As Label
    Friend WithEvents lblServicesHeader As Label
    Friend WithEvents pnlTotalService As Panel
    Friend WithEvents lblServiceTotal As Label
    Friend WithEvents lblTotalServices As Label
    Friend WithEvents dgvServices As DataGridView
    Friend WithEvents btExportServiceReport As Button
    Friend WithEvents pnlCreateService As Panel
    Friend WithEvents btnSaveService As Button
    Friend WithEvents btnServiceCancel As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents cmbServiceType As ComboBox
    Friend WithEvents lblSEmployeeID As Label
    Friend WithEvents pnlSEmployee As Panel
    Friend WithEvents cmbSEmployeeID As ComboBox
    Friend WithEvents lblServiceMainDir As Label
    Friend WithEvents lblServiceSubDir As Label
    Friend WithEvents pnlServiceInfo As Panel
    Friend WithEvents Panel8 As Panel
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents ComboBox4 As ComboBox
    Friend WithEvents ComboBox5 As ComboBox
    Friend WithEvents Label12 As Label
    Friend WithEvents lblServiceID As Label
    Friend WithEvents pnlServiceID As Panel
    Friend WithEvents txtServiceID As TextBox
    Friend WithEvents Panel10 As Panel
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents Panel11 As Panel
    Friend WithEvents Label16 As Label
    Friend WithEvents Label17 As Label
    Friend WithEvents Label18 As Label
    Friend WithEvents ComboBox6 As ComboBox
    Friend WithEvents ComboBox7 As ComboBox
    Friend WithEvents ComboBox8 As ComboBox
    Friend WithEvents lblServiceDescription As Label
    Friend WithEvents pnlServiceDescription As Panel
    Friend WithEvents txtServiceDescription As TextBox
    Friend WithEvents lblServiceType As Label
    Friend WithEvents pnlServiceType As Panel
    Friend WithEvents lblPolicyLimit As Label
    Friend WithEvents pnlPolicyLimit As Panel
    Friend WithEvents lblServiceName As Label
    Friend WithEvents pnlServiceName As Panel
    Friend WithEvents txtServiceName As TextBox
    Friend WithEvents lblServiceInformation As Label
    Friend WithEvents lblAddServiceSubtitle As Label
    Friend WithEvents lblAddService As Label
    Friend WithEvents service_id As DataGridViewTextBoxColumn
    Friend WithEvents service_name As DataGridViewTextBoxColumn
    Friend WithEvents service_type As DataGridViewTextBoxColumn
    Friend WithEvents service_description As DataGridViewTextBoxColumn
    Friend WithEvents service_policy_limit As DataGridViewTextBoxColumn
    Friend WithEvents employee_id As DataGridViewTextBoxColumn
    Friend WithEvents txtPolicyLimit As TextBox
    Friend WithEvents imgRental As PictureBox
    Friend WithEvents lblMachineID As Label
    Friend WithEvents pnlMachineryId As Panel
    Friend WithEvents cmbMachineryID As ComboBox
    Friend WithEvents ImagePolicy As PictureBox

End Class
