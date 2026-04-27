Imports System.Security
Imports MySql.Data.MySqlClient

Public Class frmPanelHolder
    ' Global variables for the class
    Dim pnlOverlay As New Panel
    Private btnBack As Object
    Private pnlEditFarmer As Panel
    Private txtEditFarmerName As TextBox
    Private txtEditFarmerAddress As TextBox
    Private txtEditFarmerContact As TextBox
    Private txtEditFarmerClass As TextBox
    Private txtEditFarmerStatus As TextBox
    Private editingFarmerId As Integer = 0
    Private dgvMachineryUi As DataGridView
    Private dgvOperatorUi As DataGridView
    Private dgvEmployeeUi As DataGridView
    Private dgvStationUi As DataGridView
    Private txtMachinerySearch As TextBox
    Private txtOperatorSearch As TextBox
    Private txtEmployeeSearch As TextBox
    Private txtStationSearch As TextBox

    Private Sub pnlConfig_Paint(sender As Object, e As PaintEventArgs) Handles pnlConfig.Paint
    End Sub

    Private Sub pnlConfigForm_Paint(sender As Object, e As PaintEventArgs) Handles pnlConfigForm.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim rect As New Rectangle(0, 0, pnlConfigForm.Width - 1, pnlConfigForm.Height - 1)
        Dim radius As Integer = 30

        Dim path As New Drawing2D.GraphicsPath()
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90)
        path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90)
        path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90)
        path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90)
        path.CloseAllFigures()

        pnlConfigForm.Region = New Region(path)

        Using pen As New Pen(Color.Gray, 1)
            e.Graphics.DrawPath(pen, path)
        End Using
    End Sub

    Private Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        db_server = txtServer.Text
        db_uid = txtUID.Text
        db_pwd = txtPWD.Text
        db_name = txtDatabase.Text

        readqueary("SELECT 1")

        If isConnected Then
            MsgBox("Connected successfully to PABEO database!", MsgBoxStyle.Information)

            ' 1. Set the Parent of the Farmers Panel to the MDI's pnlForms
            ' This physically moves the panel from the Holder form to the MDI form
            Me.pnlFarmers.Parent = mdiPABEO.pnlForms

            ' 2. Make it fill the entire space of pnlForms
            Me.pnlFarmers.Dock = DockStyle.Fill

            ' 3. Show the MDI and Hide this config form
            mdiPABEO.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub frmPanelHolder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlConfig.Visible = True
        pnlConfig.BringToFront()

        LoadFarmersGrid()
        FillEmployeeComboBox()

        LoadServicesGrid()
        RefreshServiceStats()
        ConfigureRequestsGridColumns()
        LoadRequestsGrid()
        LoadMachineryGrid()
        InitializeFarmerEditPanel()
        BuildManagementPanelUI(pnlMachinery, "Machinery Management", "P.A.B.E.O. machinery inventory records", "machinery")
        BuildManagementPanelUI(pnlOperator, "Operator Management", "P.A.B.E.O. operator records and assignments", "operator")
        BuildManagementPanelUI(pnlEmployee, "Employee Management", "P.A.B.E.O. employee records", "employee")
        BuildManagementPanelUI(pnlStation, "Station Management", "P.A.B.E.O. station records", "station")
        LoadMachineryCrudGrid()
        LoadOperatorGrid()
        LoadEmployeeGrid()
        LoadStationGrid()
    End Sub

    Private Sub btnAddFarmer_Click(sender As Object, e As EventArgs) Handles btnAddFarmer.Click
        pnlOverlay.Size = New Size(mdiPABEO.Width, mdiPABEO.Height)
        pnlOverlay.Location = New Point(0, 0)
        pnlOverlay.BackColor = Color.FromArgb(180, 26, 36, 33)
        pnlOverlay.BorderStyle = BorderStyle.None

        If Not mdiPABEO.Controls.Contains(pnlOverlay) Then
            mdiPABEO.Controls.Add(pnlOverlay)
        End If

        pnlOverlay.Visible = True
        pnlOverlay.BringToFront()

        pnlCreateFarmer.Dock = DockStyle.None
        pnlCreateFarmer.Visible = True

        If Not mdiPABEO.Controls.Contains(pnlCreateFarmer) Then
            mdiPABEO.Controls.Add(pnlCreateFarmer)
        End If

        pnlCreateFarmer.BringToFront()

        Dim x = (pnlOverlay.Width - pnlCreateFarmer.Width) \ 2
        Dim y = (pnlOverlay.Height - pnlCreateFarmer.Height) \ 2
        pnlCreateFarmer.Location = New Point(x, y)
    End Sub

    Private Sub pnlCreateFarmer_Paint(sender As Object, e As PaintEventArgs) Handles pnlCreateFarmer.Paint
    End Sub

    Private Sub lblMainDir2_Click(sender As Object, e As EventArgs) Handles lblMainDir2.Click
        Me.pnlCreateFarmer.Visible = False
        mdiPABEO.Controls.Remove(Me.pnlCreateFarmer)
        mdiPABEO.Controls.Remove(pnlOverlay)
        pnlOverlay.Visible = False

        Me.lblSubDir.Visible = True
        mdiPABEO.lblHeader.Text = "Farmers"
    End Sub

    Private Sub txtFarmerID_TextChanged(sender As Object, e As EventArgs) Handles txtFarmerID.TextChanged
    End Sub
    Private Sub txtFFullname_TextChanged(sender As Object, e As EventArgs) Handles txtFFullname.TextChanged
    End Sub
    Private Sub cmbMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMonth.SelectedIndexChanged
        ComputeAge()
    End Sub
    Private Sub cmbDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDay.SelectedIndexChanged
        ComputeAge()
    End Sub
    Private Sub cmbYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbYear.SelectedIndexChanged
        ComputeAge()
    End Sub
    Private Sub txtFAge_TextChanged(sender As Object, e As EventArgs) Handles txtFAge.TextChanged
    End Sub
    Private Sub txtFEmail_TextChanged(sender As Object, e As EventArgs) Handles txtFEmail.TextChanged
    End Sub
    Private Sub cmbBarangay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBarangay.SelectedIndexChanged
    End Sub
    Private Sub cmbCity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCity.SelectedIndexChanged
    End Sub
    Private Sub cmbProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProvince.SelectedIndexChanged
    End Sub
    Private Sub txtFAddress_TextChanged(sender As Object, e As EventArgs) Handles txtFAddress.TextChanged
    End Sub
    Private Sub cmbClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbClass.SelectedIndexChanged
    End Sub
    Private Sub cmbRegStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRegStatus.SelectedIndexChanged
    End Sub
    Private Sub txtFContact_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFContact.KeyPress
        ' Allow only numbers (0-9) and the Backspace key (Control)
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True ' This "swallows" the key press so nothing is typed
        End If
    End Sub
    Private Sub pnlFarmers_Paint(sender As Object, e As PaintEventArgs) Handles pnlFarmers.Paint
    End Sub
    Private Sub lblSubDir_Click(sender As Object, e As EventArgs) Handles lblSubDir.Click
    End Sub

    Public Sub LoadFarmersGrid()
        Try
            ' ADD farmer_id to the SELECT list so the grid can find it
            Dim sql As String = "SELECT farmer_id, " &
                            "CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS formatted_id, " &
                            "full_name, residence_address, contact_number, classification, registration_status " &
                            "FROM farmer ORDER BY created_at DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                dgvFarmers.AutoGenerateColumns = False
                dgvFarmers.DataSource = dt

                dgvFarmers.AllowUserToAddRows = False
                dgvFarmers.DefaultCellStyle.ForeColor = Color.Black
            End If

            UpdateFarmerStats()

        Catch ex As Exception
            Console.WriteLine("Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub btnRegisterFarmer_Click(sender As Object, e As EventArgs) Handles btnRegisterFarmer.Click
        ' 1. Birth Date Validation
        If String.IsNullOrWhiteSpace(cmbMonth.Text) OrElse
       String.IsNullOrWhiteSpace(cmbDay.Text) OrElse
       String.IsNullOrWhiteSpace(cmbYear.Text) Then

            MessageBox.Show("Please select a complete Birth Date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Contact Number Validation (Must be 11 digits and start with 09)
        Dim contact = txtFContact.Text.Trim
        If Not System.Text.RegularExpressions.Regex.IsMatch(contact, "^09\d{9}$") Then
            MessageBox.Show("Please enter a valid 11-digit contact number starting with 09.", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. Prepare Data (Applying .ToUpper to ensure database consistency)
        Dim fullName = txtFFullname.Text.Trim.ToUpper
        Dim farmLoc = txtFAddress.Text.Trim.ToUpper
        Dim dob = $"{cmbYear.Text}-{cmbMonth.SelectedIndex + 1:D2}-{cmbDay.Text}"
        Dim resAddress = $"{cmbBarangay.Text}, {cmbCity.Text}, {cmbProvince.Text}".ToUpper

        ' 4. INSERT Query
        Dim sql = "INSERT INTO farmer (full_name, birth_date, email, contact_number, residence_address, farm_location, classification, registration_status) " &
                       "VALUES ('" & fullName & "', '" & dob & "', '" & txtFEmail.Text.Trim & "', " &
                       "'" & contact & "', '" & resAddress & "', '" & farmLoc & "', '" & cmbClass.Text & "', '" & cmbRegStatus.Text & "')"

        Try
            ' 5. Execute
            readqueary(sql)
            MessageBox.Show("Farmer record saved successfully!", "PABEO System", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' 6. UI Cleanup and Refresh
            LoadFarmersGrid()
            lblMainDir2_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message, "PABEO System", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ShowPanel(targetPanel As Panel)
        pnlFarmers.Visible = False
        pnlConfig.Visible = False

        targetPanel.Visible = True
        targetPanel.BringToFront()
        targetPanel.Dock = DockStyle.Fill
    End Sub

    Private Sub btnRegisterCancel_Click(sender As Object, e As EventArgs) Handles btnRegisterCancel.Click
        ' 1. Clear the inputs so they are fresh for the next time you open the form
        txtFFullname.Clear()
        txtFEmail.Clear()
        txtFContact.Clear()
        txtFAddress.Clear()

        ' Reset dropdowns to their default state
        cmbMonth.SelectedIndex = -1
        cmbDay.SelectedIndex = -1
        cmbYear.SelectedIndex = -1
        cmbBarangay.SelectedIndex = -1
        cmbClass.SelectedIndex = -1
        cmbRegStatus.SelectedIndex = -1

        ' 2. Use your existing sub to hide the panel and the dimming overlay
        lblMainDir2_Click(Nothing, Nothing)
    End Sub

    Private Sub lblFarmerTotal_Click(sender As Object, e As EventArgs) Handles lblFarmerTotal.Click

    End Sub

    Public Sub UpdateFarmerStats()
        Try
            ' SQL to count the total number of farmers
            Dim sql As String = "SELECT COUNT(*) FROM farmer"
            readqueary(sql)

            If cmdread IsNot Nothing Then
                ' Read the result
                If cmdread.Read() Then
                    ' Format the number to always show two digits (e.g., 05 instead of 5)
                    lblFarmerTotal.Text = cmdread(0).ToString("00")
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub ComputeAge()
        ' Only calculate if all three parts of the date are selected
        If cmbMonth.SelectedIndex <> -1 AndAlso cmbDay.SelectedIndex <> -1 AndAlso cmbYear.SelectedIndex <> -1 Then
            Try
                Dim birthDate As New DateTime(CInt(cmbYear.Text), cmbMonth.SelectedIndex + 1, CInt(cmbDay.Text))
                Dim today As DateTime = DateTime.Today
                Dim age As Integer = today.Year - birthDate.Year

                ' Adjust if birthday hasn't happened yet this year
                If birthDate > today.AddYears(-age) Then age -= 1

                txtFAge.Text = age.ToString()
            Catch ex As Exception
                txtFAge.Text = "" ' Handle invalid dates (like Feb 30)
            End Try
        End If
    End Sub

    Private Sub txtFarmersSearch_TextChanged(sender As Object, e As EventArgs) Handles txtFarmersSearch.TextChanged
        Try
            Dim search = txtFarmersSearch.Text.Trim

            ' If the search box is empty, load the full list
            If search = "" Then
                LoadFarmersGrid()
                Return
            End If

            ' We use the same CONCAT/LPAD logic in the WHERE clause so you can search "RSBSA" or "0001"
            Dim sql = "SELECT CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS formatted_id, " &
                           "full_name, residence_address, contact_number, classification, registration_status " &
                           "FROM farmer WHERE " &
                           "CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) LIKE '%" & search & "%' OR " &
                           "full_name LIKE '%" & search & "%' OR " &
                           "residence_address LIKE '%" & search & "%' OR " &
                           "contact_number LIKE '%" & search & "%' OR " &
                           "classification LIKE '%" & search & "%' OR " &
                           "registration_status LIKE '%" & search & "%' " &
                           "ORDER BY created_at DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)
                dgvFarmers.AutoGenerateColumns = False
                dgvFarmers.DataSource = dt
            End If

        Catch ex As Exception
            Console.WriteLine("Search Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub Panel9_Paint(sender As Object, e As PaintEventArgs) Handles pnlServiceID.Paint

    End Sub

    Private Sub btnAddServices_Click(sender As Object, e As EventArgs) Handles btnAddService.Click
        pnlOverlay.Size = New Size(mdiPABEO.Width, mdiPABEO.Height)
        pnlOverlay.Location = New Point(0, 0)
        pnlOverlay.BackColor = Color.FromArgb(180, 26, 36, 33)
        pnlOverlay.BorderStyle = BorderStyle.None

        ' Add to MDI if it's not already there
        If Not mdiPABEO.Controls.Contains(pnlOverlay) Then
            mdiPABEO.Controls.Add(pnlOverlay)
        End If

        pnlOverlay.Visible = True
        pnlOverlay.BringToFront()

        ' 2. Prepare the Create Service Panel
        Me.pnlCreateService.Dock = DockStyle.None
        Me.pnlCreateService.Visible = True

        ' Add the panel to the MDI controls so it can float over the overlay
        If Not mdiPABEO.Controls.Contains(Me.pnlCreateService) Then
            mdiPABEO.Controls.Add(Me.pnlCreateService)
        End If

        ' Force the Service Panel to stay ABOVE the dim overlay
        Me.pnlCreateService.BringToFront()

        ' 3. Center the panel within the overlay
        Dim x As Integer = (pnlOverlay.Width - Me.pnlCreateService.Width) \ 2
        Dim y As Integer = (pnlOverlay.Height - Me.pnlCreateService.Height) \ 2
        Me.pnlCreateService.Location = New Point(x, y)

        ' Optional: Update header text if you have a label for it
        mdiPABEO.lblHeader.Text = "Add New Service"
    End Sub

    Private Sub lblServiceMainDir_Click(sender As Object, e As EventArgs) Handles lblServiceMainDir.Click

        btnServiceCancel_Click(Nothing, Nothing)
    End Sub

    Private Sub btnServiceCancel_Click(sender As Object, e As EventArgs) Handles btnServiceCancel.Click
        Me.pnlCreateService.Visible = False
        mdiPABEO.Controls.Remove(Me.pnlCreateService)

        mdiPABEO.Controls.Remove(pnlOverlay)
        pnlOverlay.Visible = False

        mdiPABEO.lblHeader.Text = "Services"

        txtServiceDescription.Clear()
    End Sub

    Private Sub txtServiceID_TextChanged(sender As Object, e As EventArgs) Handles txtServiceID.TextChanged

    End Sub

    'Private Sub txtPolicyLimit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtPolicyLimit.KeyPress
    'If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) AndAlso e.KeyChar <> "." Then
    '       e.Handled = True
    'End If

    'If e.KeyChar = "." AndAlso txtPolicyLimit.Text.Contains(".") Then
    '       e.Handled = True
    'End If
    'End Sub

    Private Sub txtServiceDescription_TextChanged(sender As Object, e As EventArgs) Handles txtServiceDescription.TextChanged

    End Sub



    Private Sub btnSaveService_Click(sender As Object, e As EventArgs) Handles btnSaveService.Click
        ' 1. Basic Validation - Ensure required dropdowns are selected
        If cmbServiceType.SelectedIndex = -1 OrElse cmbServiceName.SelectedIndex = -1 Then
            MessageBox.Show("Please complete the Service Information fields.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Prepare Data 
        Dim sName As String = cmbServiceName.Text
        Dim sType As String = cmbServiceType.Text
        Dim sDesc As String = txtServiceDescription.Text.Trim()
        Dim pLimit As String = cmbPolicyLimit.Text
        ' Updated Machinery ID logic based on your specific table IDs
        Dim machID As String = "0"
        Dim selectedMachine As String = cmbMachineryID.Text.ToUpper()

        If selectedMachine.Contains("WALK-BEHIND") Then
            machID = "14"
        ElseIf selectedMachine.Contains("RIDE-IN") Then
            machID = "21"
        ElseIf selectedMachine.Contains("DC35") Then
            machID = "15"
        ElseIf selectedMachine.Contains("DC60") Then
            machID = "17" ' Based on your machinery table screenshot
        ElseIf selectedMachine.Contains("DC70") Then
            machID = "16"
        ElseIf selectedMachine.Contains("M9540") Then
            machID = "18"
        ElseIf selectedMachine.Contains("L3608") OrElse selectedMachine.Contains("L3600") Then
            machID = "19"
        ElseIf selectedMachine.Contains("TYM") Then
            machID = "24"
        ElseIf selectedMachine.Contains("CORN SHELLER") Then
            machID = "25"
        ElseIf selectedMachine.Contains("FLATBED") Then
            machID = "22"
        End If

        ' Defaulting to 1 for Employee ID as seen in your table structure
        Dim empID As String = "1"

        ' 3. SQL Query - Points to the singular 'service' table
        Dim sql As String = "INSERT INTO service (service_name, service_type, description, machinery_id, policy_limit, employee_id) " &
                           "VALUES ('" & sName & "', '" & sType & "', '" & sDesc & "', '" & machID & "', '" & pLimit & "', '" & empID & "')"

        Try
            ' Execute query via Module1
            readqueary(sql)

            MessageBox.Show("Service added successfully!", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' --- REAL-TIME REFRESH ---
            ' This updates the background list immediately
            LoadServiceGrid()

            ' --- UI CLEANUP ---
            ' Resets the dropdowns for the next time the panel opens
            ClearServiceFields()

            ' --- HIDE OVERLAY ---
            ' Closes the panel and dark background
            pnlOverlay.Visible = False
            Me.pnlCreateService.Visible = False

            ' Safely remove controls from the MDI parent
            If mdiPABEO.Controls.Contains(pnlOverlay) Then mdiPABEO.Controls.Remove(pnlOverlay)
            If mdiPABEO.Controls.Contains(Me.pnlCreateService) Then mdiPABEO.Controls.Remove(Me.pnlCreateService)

            ' Reset Header text
            mdiPABEO.lblHeader.Text = "Services"

        Catch ex As Exception
            MessageBox.Show("Error saving service: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub LoadServicesGrid()
        Try
            ' We use SRV- and pad the ID to 4 digits (e.g., SRV-0001)
            Dim sql As String = "SELECT CONCAT('SRV-', LPAD(service_id, 4, '0')) AS service_id, " &
                           "machinery_id, service_name, service_type, description AS service_description, policy_limit AS service_policy_limit, employee_id " &
                           "FROM service ORDER BY service_id DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                dgvServices.AutoGenerateColumns = False
                dgvServices.DataSource = dt

                dgvServices.AllowUserToAddRows = False
                dgvServices.DefaultCellStyle.ForeColor = Color.Black
            End If
        Catch ex As Exception
            Console.WriteLine("Service Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub
    Private Sub cmbSEmployeeID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbEmployeeID.SelectedIndexChanged

    End Sub

    Public Sub FillEmployeeComboBox()
        Try
            ' Select both the ID and Name
            Dim sql As String = "SELECT employee_id, full_name FROM employee ORDER BY full_name ASC"
            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                ' Bind the data to the ComboBox
                cmbEmployeeID.DataSource = dt
                cmbEmployeeID.DisplayMember = "full_name"   ' What the user sees
                cmbEmployeeID.ValueMember = "employee_id"   ' The actual ID saved to DB

                ' Set to -1 so it starts empty
                cmbEmployeeID.SelectedIndex = -1
            End If
        Catch ex As Exception
            Console.WriteLine("Error loading employees: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Public Sub RefreshServiceStats()
        Try
            readqueary("SELECT COUNT(*) FROM service")

            If cmdread IsNot Nothing AndAlso cmdread.Read() Then
                ' This updates the text of your label
                lblServiceTotal.Text = Val(cmdread(0)).ToString("00")
            End If
        Catch ex As Exception
            Console.WriteLine("Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub



    ' Level 1: Filter Service Name by Service Type
    Private Sub cmbServiceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServiceType.SelectedIndexChanged
        cmbServiceName.Items.Clear()
        cmbMachineryID.Items.Clear()
        cmbPolicyLimit.Items.Clear()

        Select Case cmbServiceType.Text
            Case "LAND PREPARATION"
                cmbServiceName.Items.AddRange({"LAND PREPARATION SERVICE", "SOIL TILLING SERVICE", "FIELD EXCAVATION SERVICE"})
            Case "PLANTING"
                cmbServiceName.Items.Add("RICE PLANTING SERVICE")
            Case "HARVESTING"
                cmbServiceName.Items.AddRange({"RICE HARVESTING SERVICE", "CORN HARVESTING SERVICE"})
            Case "POST-HARVEST"
                cmbServiceName.Items.AddRange({"CORN SHELLING SERVICE", "GRAIN DRYING SERVICE"})
        End Select
    End Sub

    ' Level 2: Filter Machinery by Service Name
    Private Sub cmbServiceName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServiceName.SelectedIndexChanged
        cmbMachineryID.Items.Clear()
        cmbPolicyLimit.Items.Clear()

        Select Case cmbServiceName.Text
            Case "RICE PLANTING SERVICE"
                cmbMachineryID.Items.AddRange({"WALK-BEHIND TRANSPLANTER", "RIDE-IN TYPE TRANSPLANTER"})
            Case "RICE HARVESTING SERVICE"
                cmbMachineryID.Items.AddRange({"DC35 COMBINE HARVESTER", "DC60 COMBINE HARVESTER", "DC70 COMBINE HARVESTER"})
            Case "CORN HARVESTING SERVICE"
                cmbMachineryID.Items.Add("DC70 COMBINE HARVESTER")
            Case "LAND PREPARATION SERVICE", "SOIL TILLING SERVICE", "FIELD EXCAVATION SERVICE"
                cmbMachineryID.Items.AddRange({"M9540 TRACTOR", "L3608 TRACTOR", "TYM TRACTOR"})
            Case "CORN SHELLING SERVICE"
                cmbMachineryID.Items.Add("CORN SHELLER")
            Case "GRAIN DRYING SERVICE"
                cmbMachineryID.Items.Add("FLATBED DRYER")
        End Select
    End Sub

    ' Level 3: Filter Policy Limit by Machinery
    Private Sub cmbMachineryID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMachineryID.SelectedIndexChanged
        cmbPolicyLimit.Items.Clear()

        ' We use 'selectedPolicyText' to avoid conflict with 'System.Security.Policy'
        Dim selectedPolicyText As String = ""

        Select Case cmbMachineryID.Text
            Case "WALK-BEHIND TRANSPLANTER", "RIDE-IN TYPE TRANSPLANTER"
                selectedPolicyText = "FUEL FULL TANK SYSTEM REQUIRED; OIL 1 LITER PHP 250 PER HECTARE; MEALS OF OPERATOR REQUIRED"
            Case "DC35 COMBINE HARVESTER", "DC60 COMBINE HARVESTER"
                selectedPolicyText = "PHP 3000 PER HECTARE; FUEL FULL TANK SYSTEM REQUIRED; OIL 1 LITER PHP 250 PER HECTARE; MEALS OF OPERATOR REQUIRED"
            Case "DC70 COMBINE HARVESTER"
                If cmbServiceName.Text = "RICE HARVESTING SERVICE" Then
                    selectedPolicyText = "PHP 3000 PER HECTARE FOR RICE; FUEL FULL TANK SYSTEM REQUIRED; OIL 1 LITER PHP 250 PER HECTARE; MEALS OF OPERATOR REQUIRED"
                Else
                    selectedPolicyText = "PHP 5000 PER HECTARE FOR CORN; FUEL FULL TANK SYSTEM REQUIRED; OIL 1 LITER PHP 250 PER HECTARE; MEALS OF OPERATOR REQUIRED"
                End If
            Case "M9540 TRACTOR", "L3608 TRACTOR", "TYM TRACTOR"
                selectedPolicyText = "PHP 2500 PER HECTARE; FUEL FULL TANK SYSTEM REQUIRED; OIL 1 LITER PHP 250 PER HECTARE; MEALS OF OPERATOR REQUIRED"
            Case "CORN SHELLER"
                selectedPolicyText = "PHP 0.50 PER KG; FUEL FULL TANK SYSTEM REQUIRED; MEALS OF OPERATOR REQUIRED"
            Case "FLATBED DRYER"
                selectedPolicyText = "FUEL FULL TANK SYSTEM REQUIRED; MEALS OF OPERATOR REQUIRED"
        End Select

        If selectedPolicyText <> "" Then
            cmbPolicyLimit.Items.Add(selectedPolicyText)
            cmbPolicyLimit.SelectedIndex = 0
        End If
    End Sub

    Public Sub LoadServiceGrid()
        Try

            ' Pull fresh data from the singular 'service' table
            Dim sql As String = "SELECT CONCAT('SRV-', LPAD(service_id, 4, '0')) AS service_id, " &
                                "machinery_id, service_name, service_type, description AS service_description, policy_limit AS service_policy_limit, employee_id " &
                                "FROM service ORDER BY service_id DESC"
            readqueary(sql)

            Dim dt As New DataTable
            dt.Load(cmdread)

            ' Ensure the grid doesn't create extra columns
            dgvServices.AutoGenerateColumns = False
            dgvServices.DataSource = dt
            lblTotalServices.Text = dt.Rows.Count.ToString("00")

        Catch ex As Exception
            ' Optional: Silently handle errors or log them
        End Try
    End Sub

    Public Sub ClearServiceFields()
        ' Clear TextBoxes
        txtServiceDescription.Clear()
        ' If Service ID is manual, clear it too:
        ' txtServiceID.Clear() 

        ' Reset ComboBoxes
        cmbServiceType.SelectedIndex = -1
        cmbServiceName.Items.Clear()
        cmbMachineryID.Items.Clear()
        cmbPolicyLimit.Items.Clear()
        cmbEmployeeID.SelectedIndex = -1

        ' Optional: Reset the text property just in case
        cmbServiceName.Text = ""
        cmbMachineryID.Text = ""
        cmbPolicyLimit.Text = ""
    End Sub

    Private Sub cmbPolicyLimit_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPolicyLimit.SelectedIndexChanged

    End Sub

    Private Sub dgvFarmers_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFarmers.CellClick
        If e.RowIndex < 0 Then Return

        If dgvFarmers.Columns(e.ColumnIndex).Name = "FarmerEdit" Then
            Try
                Dim pidCell = dgvFarmers.Rows(e.RowIndex).Cells("farmer_id").Value
                If pidCell Is Nothing OrElse IsDBNull(pidCell) Then Return

                ShowFarmerEditPanel(
                    Val(pidCell.ToString()),
                    Convert.ToString(dgvFarmers.Rows(e.RowIndex).Cells("full_name").Value),
                    Convert.ToString(dgvFarmers.Rows(e.RowIndex).Cells("residence_address").Value),
                    Convert.ToString(dgvFarmers.Rows(e.RowIndex).Cells("contact_number").Value),
                    Convert.ToString(dgvFarmers.Rows(e.RowIndex).Cells("classification").Value),
                    Convert.ToString(dgvFarmers.Rows(e.RowIndex).Cells("registration_status").Value)
                )
            Catch ex As Exception
                MessageBox.Show("Update Error: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            Return
        End If

        ' 1. Check for the Delete column click
        If dgvFarmers.Columns(e.ColumnIndex).Name = "FarmerDelete" Then

            Try
                ' 2. Using your exact confirmed small-caps identifiers
                Dim pidCell = dgvFarmers.Rows(e.RowIndex).Cells("farmer_id").Value
                Dim nameCell = dgvFarmers.Rows(e.RowIndex).Cells("full_name").Value

                ' 3. Check if the cell has data
                If pidCell IsNot Nothing AndAlso Not IsDBNull(pidCell) Then

                    Dim fName As String = nameCell.ToString()
                    Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete " & fName & "?",
                                                             "Confirm Deletion",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        ' 4. SQL Execution using the numeric ID
                        Dim sql As String = "DELETE FROM farmer WHERE farmer_id = " & Val(pidCell.ToString())
                        readqueary(sql)

                        MessageBox.Show("Farmer deleted successfully.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' 5. Refresh the grid immediately
                        LoadFarmersGrid()
                    End If
                Else
                    ' If this shows, your LoadFarmersGrid() query might not be selecting farmer_id
                    MessageBox.Show("The system found the column but it's empty. Try restarting the app.", "PABEO")
                End If

            Catch ex As Exception
                MessageBox.Show("Logic Error: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub InitializeFarmerEditPanel()
        If pnlEditFarmer IsNot Nothing Then Return

        pnlEditFarmer = New Panel With {
            .Name = "pnlEditFarmer",
            .Size = New Size(700, 500),
            .BackColor = Color.White,
            .Visible = False
        }

        Dim lblTitle As New Label With {.Text = "Edit Farmer", .Font = New Font("Segoe UI", 24, FontStyle.Bold), .Location = New Point(25, 20), .AutoSize = True}
        Dim lblSub As New Label With {.Text = "Update farmer information", .Font = New Font("Segoe UI", 10, FontStyle.Regular), .Location = New Point(28, 70), .AutoSize = True}

        Dim lblName As New Label With {.Text = "Full Name", .Location = New Point(30, 120), .AutoSize = True}
        txtEditFarmerName = New TextBox With {.Location = New Point(30, 140), .Width = 300}

        Dim lblAddress As New Label With {.Text = "Residence Address", .Location = New Point(360, 120), .AutoSize = True}
        txtEditFarmerAddress = New TextBox With {.Location = New Point(360, 140), .Width = 300}

        Dim lblContact As New Label With {.Text = "Contact Number", .Location = New Point(30, 200), .AutoSize = True}
        txtEditFarmerContact = New TextBox With {.Location = New Point(30, 220), .Width = 300}

        Dim lblClass As New Label With {.Text = "Classification", .Location = New Point(360, 200), .AutoSize = True}
        txtEditFarmerClass = New TextBox With {.Location = New Point(360, 220), .Width = 300}

        Dim lblStatus As New Label With {.Text = "Registration Status", .Location = New Point(30, 280), .AutoSize = True}
        txtEditFarmerStatus = New TextBox With {.Location = New Point(30, 300), .Width = 300}

        Dim btnCancel As New Button With {
            .Text = "Cancel",
            .Location = New Point(430, 430),
            .Size = New Size(110, 40),
            .BackColor = Color.FromArgb(49, 68, 56),
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }
        Dim btnSave As New Button With {
            .Text = "Update Farmer",
            .Location = New Point(550, 430),
            .Size = New Size(120, 40),
            .BackColor = Color.DarkGreen,
            .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat
        }

        AddHandler btnCancel.Click, Sub() HideFarmerEditPanel()
        AddHandler btnSave.Click, AddressOf SaveFarmerEdit

        pnlEditFarmer.Controls.AddRange(New Control() {lblTitle, lblSub, lblName, txtEditFarmerName, lblAddress, txtEditFarmerAddress, lblContact, txtEditFarmerContact, lblClass, txtEditFarmerClass, lblStatus, txtEditFarmerStatus, btnCancel, btnSave})
    End Sub

    Private Sub ShowFarmerEditPanel(farmerId As Integer, fullName As String, address As String, contact As String, classification As String, regStatus As String)
        If pnlEditFarmer Is Nothing Then InitializeFarmerEditPanel()

        editingFarmerId = farmerId
        txtEditFarmerName.Text = fullName
        txtEditFarmerAddress.Text = address
        txtEditFarmerContact.Text = contact
        txtEditFarmerClass.Text = classification
        txtEditFarmerStatus.Text = regStatus

        pnlOverlay.Size = New Size(mdiPABEO.Width, mdiPABEO.Height)
        pnlOverlay.Location = New Point(0, 0)
        pnlOverlay.BackColor = Color.FromArgb(180, 26, 36, 33)
        pnlOverlay.BorderStyle = BorderStyle.None

        If Not mdiPABEO.Controls.Contains(pnlOverlay) Then
            mdiPABEO.Controls.Add(pnlOverlay)
        End If
        pnlOverlay.Visible = True
        pnlOverlay.BringToFront()

        If Not mdiPABEO.Controls.Contains(pnlEditFarmer) Then
            mdiPABEO.Controls.Add(pnlEditFarmer)
        End If

        pnlEditFarmer.Visible = True
        pnlEditFarmer.BringToFront()
        pnlEditFarmer.Location = New Point((pnlOverlay.Width - pnlEditFarmer.Width) \ 2, (pnlOverlay.Height - pnlEditFarmer.Height) \ 2)
    End Sub

    Private Sub HideFarmerEditPanel()
        If pnlEditFarmer IsNot Nothing Then pnlEditFarmer.Visible = False
        If pnlEditFarmer IsNot Nothing AndAlso mdiPABEO.Controls.Contains(pnlEditFarmer) Then mdiPABEO.Controls.Remove(pnlEditFarmer)
        If mdiPABEO.Controls.Contains(pnlOverlay) Then mdiPABEO.Controls.Remove(pnlOverlay)
        pnlOverlay.Visible = False
    End Sub

    Private Sub SaveFarmerEdit(sender As Object, e As EventArgs)
        Try
            Dim newName As String = txtEditFarmerName.Text.Trim()
            Dim newAddress As String = txtEditFarmerAddress.Text.Trim()
            Dim newContact As String = txtEditFarmerContact.Text.Trim()
            Dim newClass As String = txtEditFarmerClass.Text.Trim()
            Dim newRegStatus As String = txtEditFarmerStatus.Text.Trim()

            If String.IsNullOrWhiteSpace(newName) OrElse String.IsNullOrWhiteSpace(newAddress) OrElse
               String.IsNullOrWhiteSpace(newContact) OrElse String.IsNullOrWhiteSpace(newClass) OrElse String.IsNullOrWhiteSpace(newRegStatus) Then
                MessageBox.Show("Please complete all fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            If Not System.Text.RegularExpressions.Regex.IsMatch(newContact, "^09\d{9}$") Then
                MessageBox.Show("Please enter a valid 11-digit contact number starting with 09.", "Invalid Contact", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sqlUpdate As String =
                "UPDATE farmer SET " &
                "full_name='" & newName.ToUpper() & "', " &
                "residence_address='" & newAddress.ToUpper() & "', " &
                "contact_number='" & newContact & "', " &
                "classification='" & newClass.ToUpper() & "', " &
                "registration_status='" & newRegStatus.ToUpper() & "' " &
                "WHERE farmer_id=" & editingFarmerId

            readqueary(sqlUpdate)
            MessageBox.Show("Farmer updated successfully.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            HideFarmerEditPanel()
            LoadFarmersGrid()
        Catch ex As Exception
            MessageBox.Show("Update Error: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ColumnExists(tableName As String, columnName As String) As Boolean
        Dim exists As Boolean = False
        Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
        Using localConn As New MySqlConnection(strconn)
            localConn.Open()
            Dim sql As String = "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=@schema AND TABLE_NAME=@table AND COLUMN_NAME=@column"
            Using cmdLocal As New MySqlCommand(sql, localConn)
                cmdLocal.Parameters.AddWithValue("@schema", db_name)
                cmdLocal.Parameters.AddWithValue("@table", tableName)
                cmdLocal.Parameters.AddWithValue("@column", columnName)
                exists = Convert.ToInt32(cmdLocal.ExecuteScalar()) > 0
            End Using
        End Using
        Return exists
    End Function

    Private Sub ConfigureRequestsGridColumns()
        If dgvRequests.Columns.Count < 6 Then Return

        dgvRequests.AutoGenerateColumns = False

        dgvRequests.Columns(0).DataPropertyName = "farmer_display_id"
        dgvRequests.Columns(0).HeaderText = "Farmer ID"

        dgvRequests.Columns(1).DataPropertyName = "service_display_id"
        dgvRequests.Columns(1).HeaderText = "Service ID"

        dgvRequests.Columns(2).DataPropertyName = "request_date"
        dgvRequests.Columns(2).HeaderText = "Request Date"

        dgvRequests.Columns(3).DataPropertyName = "farm_location"
        dgvRequests.Columns(3).HeaderText = "Farm Location"

        dgvRequests.Columns(4).DataPropertyName = "hectares_served"
        dgvRequests.Columns(4).HeaderText = "Hectares Served"

        dgvRequests.Columns(5).DataPropertyName = "service_status"
        dgvRequests.Columns(5).HeaderText = "Service Status"
    End Sub

    Public Sub LoadRequestsGrid()
        Try
            Dim hasOperatorColumn As Boolean = ColumnExists("service_request", "operator_id")
            Dim sql As String =
                "SELECT " &
                "CONCAT('RSBSA-', LPAD(sr.farmer_id, 4, '0')) AS farmer_display_id, " &
                "CONCAT('SRV-', LPAD(sr.service_id, 4, '0')) AS service_display_id, " &
                "sr.request_date, sr.farm_location, sr.hectares_served, sr.service_status " &
                If(hasOperatorColumn, ", IFNULL(o.full_name,'-') AS assigned_operator FROM service_request sr LEFT JOIN operator o ON o.operator_id = sr.operator_id ", "FROM service_request sr ") &
                "ORDER BY sr.request_date DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)
                dgvRequests.DataSource = dt
            End If

            RefreshRequestStats()
        Catch ex As Exception
            Console.WriteLine("Request Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub RefreshRequestStats()
        Try
            readqueary("SELECT COUNT(*) FROM service_request WHERE UPPER(service_status)='PENDING'")
            If cmdread IsNot Nothing AndAlso cmdread.Read() Then
                lblAmountPendingRequests.Text = Val(cmdread(0)).ToString("00")
            End If
        Catch ex As Exception
            Console.WriteLine("Request Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub LoadMachineryGrid()
        Try
            Dim grid As DataGridView = Nothing
            If pnlMachinery.Controls.ContainsKey("dgvMachineryDynamic") Then
                grid = DirectCast(pnlMachinery.Controls("dgvMachineryDynamic"), DataGridView)
            Else
                grid = New DataGridView With {
                    .Name = "dgvMachineryDynamic",
                    .Dock = DockStyle.Fill,
                    .AutoGenerateColumns = True,
                    .ReadOnly = True,
                    .AllowUserToAddRows = False,
                    .AllowUserToDeleteRows = False,
                    .BackgroundColor = Color.White,
                    .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                }
                pnlMachinery.Controls.Clear()
                pnlMachinery.Controls.Add(grid)
            End If

            Dim sql As String =
                "SELECT machinery_id, machinery_name, machinery_type, station_id, `condition`, availability_status " &
                "FROM machinery ORDER BY machinery_id DESC"

            readqueary(sql)
            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)
                grid.DataSource = dt
            End If
        Catch ex As Exception
            Console.WriteLine("Machinery Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub BuildManagementPanelUI(targetPanel As Panel, headerText As String, subtitleText As String, entity As String)
        targetPanel.Controls.Clear()
        targetPanel.BackColor = Color.FromArgb(245, 245, 245)

        Dim lblDir As New Label With {.Text = headerText.Replace(" Management", ""), .Location = New Point(20, 25), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDark}
        Dim lblHeader As New Label With {.Text = headerText, .Location = New Point(15, 70), .AutoSize = True, .Font = New Font("Segoe UI", 30, FontStyle.Bold), .ForeColor = Color.Black}
        Dim lblSubtitle As New Label With {.Text = subtitleText, .Location = New Point(20, 125), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}

        Dim pnlStats As New Panel With {.BackColor = Color.White, .Location = New Point(20, 190), .Size = New Size(337, 140)}
        Dim lblStatsTitle As New Label With {.Text = "Total Records", .Location = New Point(15, 13), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}
        Dim lblStatsValue As New Label With {.Name = $"lbl{entity}TotalDynamic", .Text = "00", .Location = New Point(15, 40), .AutoSize = True, .Font = New Font("Segoe UI", 42, FontStyle.Bold), .ForeColor = Color.Black}
        pnlStats.Controls.Add(lblStatsTitle)
        pnlStats.Controls.Add(lblStatsValue)

        Dim pnlSearch As New Panel With {.BackColor = Color.White, .Location = New Point(20, 360), .Size = New Size(568, 37)}
        Dim txtSearch As New TextBox With {.BorderStyle = BorderStyle.None, .Font = New Font("Segoe UI", 15.75F, FontStyle.Regular), .Location = New Point(10, 5), .Size = New Size(550, 28)}
        pnlSearch.Controls.Add(txtSearch)

        Dim btnAdd As New Button With {.Text = $"+Add {entity.Substring(0, 1).ToUpper() & entity.Substring(1)}", .BackColor = Color.DarkGreen, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .Location = New Point(1473, 360), .Size = New Size(142, 37)}
        btnAdd.FlatAppearance.BorderSize = 0

        Dim pnlGrid As New Panel With {.BackColor = Color.White, .Location = New Point(20, 412), .Size = New Size(1596, 520)}
        Dim dgv As New DataGridView With {
            .Dock = DockStyle.Fill,
            .AutoGenerateColumns = False,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .CellBorderStyle = DataGridViewCellBorderStyle.None,
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        }
        pnlGrid.Controls.Add(dgv)

        targetPanel.Controls.AddRange(New Control() {lblDir, lblHeader, lblSubtitle, pnlStats, pnlSearch, btnAdd, pnlGrid})

        Select Case entity
            Case "machinery"
                dgvMachineryUi = dgv
                txtMachinerySearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddMachineryRecord
                AddHandler txtSearch.TextChanged, Sub() LoadMachineryCrudGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf MachineryGrid_Edit
                AddHandler dgv.CellClick, AddressOf MachineryGrid_CellClick
                AddHandler dgv.KeyDown, AddressOf MachineryGrid_DeleteKey
            Case "operator"
                dgvOperatorUi = dgv
                txtOperatorSearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddOperatorRecord
                AddHandler txtSearch.TextChanged, Sub() LoadOperatorGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf OperatorGrid_Edit
                AddHandler dgv.CellClick, AddressOf OperatorGrid_CellClick
                AddHandler dgv.KeyDown, AddressOf OperatorGrid_DeleteKey
            Case "employee"
                dgvEmployeeUi = dgv
                txtEmployeeSearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddEmployeeRecord
                AddHandler txtSearch.TextChanged, Sub() LoadEmployeeGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf EmployeeGrid_Edit
                AddHandler dgv.CellClick, AddressOf EmployeeGrid_CellClick
                AddHandler dgv.KeyDown, AddressOf EmployeeGrid_DeleteKey
            Case "station"
                dgvStationUi = dgv
                txtStationSearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddStationRecord
                AddHandler txtSearch.TextChanged, Sub() LoadStationGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf StationGrid_Edit
                AddHandler dgv.CellClick, AddressOf StationGrid_CellClick
                AddHandler dgv.KeyDown, AddressOf StationGrid_DeleteKey
        End Select
    End Sub

    Private Sub ApplyFarmersLikeGridStyle(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.ColumnHeadersHeight = 50
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 12F, FontStyle.Bold)
        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.DefaultCellStyle.SelectionBackColor = SystemColors.ButtonFace
        dgv.DefaultCellStyle.SelectionForeColor = SystemColors.ControlText
        dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.GridColor = Color.White
        dgv.RowTemplate.Height = 40
    End Sub

    Private Sub ConfigureCrudGridColumns(dgv As DataGridView, entity As String)
        dgv.Columns.Clear()
        ApplyFarmersLikeGridStyle(dgv)

        Select Case entity
            Case "machinery"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "machinery_id", .Name = "machinery_id", .HeaderText = "Machinery ID", .Width = 150})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "machinery_name", .Name = "machinery_name", .HeaderText = "Machinery Name", .Width = 280})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "machinery_type", .Name = "machinery_type", .HeaderText = "Machinery Type", .Width = 180})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_id", .Name = "station_id", .HeaderText = "Station ID", .Width = 120})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "condition", .Name = "condition", .HeaderText = "Condition", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "availability_status", .Name = "availability_status", .HeaderText = "Availability", .Width = 160})
            Case "operator"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "operator_id", .Name = "operator_id", .HeaderText = "Operator ID", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "full_name", .Name = "full_name", .HeaderText = "Full Name", .Width = 320})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "position", .Name = "position", .HeaderText = "Position", .Width = 250})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "contact_number", .Name = "contact_number", .HeaderText = "Contact Number", .Width = 180})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_id", .Name = "station_id", .HeaderText = "Station ID", .Width = 120})
            Case "employee"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "employee_id", .Name = "employee_id", .HeaderText = "Employee ID", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "full_name", .Name = "full_name", .HeaderText = "Full Name", .Width = 300})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "position", .Name = "position", .HeaderText = "Position", .Width = 250})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "contact_number", .Name = "contact_number", .HeaderText = "Contact Number", .Width = 180})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "offiice_assignment", .Name = "offiice_assignment", .HeaderText = "Office Assignment", .Width = 220})
            Case "station"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_id", .Name = "station_id", .HeaderText = "Station ID", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_name", .Name = "station_name", .HeaderText = "Station Name", .Width = 300})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "location", .Name = "location", .HeaderText = "Location", .Width = 400})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "description", .Name = "description", .HeaderText = "Description", .Width = 300})
        End Select

        dgv.Columns.Add(New DataGridViewImageColumn With {.Name = "ActionEdit", .HeaderText = "Edit", .Image = FarmerEdit.Image, .Width = 75})
        dgv.Columns.Add(New DataGridViewImageColumn With {.Name = "ActionDelete", .HeaderText = "Delete", .Image = FarmerDelete.Image, .Width = 75})
    End Sub

    Private Sub SetDynamicTotal(entity As String, count As Integer)
        Dim ctrl = Controls.Find($"lbl{entity}TotalDynamic", True)
        If ctrl IsNot Nothing AndAlso ctrl.Length > 0 Then
            DirectCast(ctrl(0), Label).Text = count.ToString("00")
        End If
    End Sub

    Private Sub LoadMachineryCrudGrid(Optional filter As String = "")
        If dgvMachineryUi Is Nothing Then Return
        Try
            Dim sql As String = "SELECT machinery_id, machinery_name, machinery_type, station_id, `condition`, availability_status FROM machinery"
            If filter <> "" Then
                sql &= " WHERE machinery_name LIKE '%" & filter & "%' OR machinery_type LIKE '%" & filter & "%' OR availability_status LIKE '%" & filter & "%'"
            End If
            sql &= " ORDER BY machinery_id DESC"
            readqueary(sql)
            Dim dt As New DataTable
            dt.Load(cmdread)
            If dgvMachineryUi.Columns.Count = 0 Then ConfigureCrudGridColumns(dgvMachineryUi, "machinery")
            dgvMachineryUi.DataSource = dt
            SetDynamicTotal("machinery", dt.Rows.Count)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub LoadOperatorGrid(Optional filter As String = "")
        If dgvOperatorUi Is Nothing Then Return
        Try
            Dim sql As String = "SELECT operator_id, full_name, position, contact_number, station_id FROM operator"
            If filter <> "" Then
                sql &= " WHERE full_name LIKE '%" & filter & "%' OR position LIKE '%" & filter & "%' OR contact_number LIKE '%" & filter & "%'"
            End If
            sql &= " ORDER BY operator_id DESC"
            readqueary(sql)
            Dim dt As New DataTable
            dt.Load(cmdread)
            If dgvOperatorUi.Columns.Count = 0 Then ConfigureCrudGridColumns(dgvOperatorUi, "operator")
            dgvOperatorUi.DataSource = dt
            SetDynamicTotal("operator", dt.Rows.Count)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub LoadEmployeeGrid(Optional filter As String = "")
        If dgvEmployeeUi Is Nothing Then Return
        Try
            Dim sql As String = "SELECT employee_id, full_name, position, contact_number, offiice_assignment FROM employee"
            If filter <> "" Then
                sql &= " WHERE full_name LIKE '%" & filter & "%' OR position LIKE '%" & filter & "%' OR contact_number LIKE '%" & filter & "%' OR offiice_assignment LIKE '%" & filter & "%'"
            End If
            sql &= " ORDER BY employee_id DESC"
            readqueary(sql)
            Dim dt As New DataTable
            dt.Load(cmdread)
            If dgvEmployeeUi.Columns.Count = 0 Then ConfigureCrudGridColumns(dgvEmployeeUi, "employee")
            dgvEmployeeUi.DataSource = dt
            SetDynamicTotal("employee", dt.Rows.Count)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub LoadStationGrid(Optional filter As String = "")
        If dgvStationUi Is Nothing Then Return
        Try
            Dim sql As String = "SELECT station_id, station_name, location, description FROM station"
            If filter <> "" Then
                sql &= " WHERE station_name LIKE '%" & filter & "%' OR location LIKE '%" & filter & "%' OR description LIKE '%" & filter & "%'"
            End If
            sql &= " ORDER BY station_id DESC"
            readqueary(sql)
            Dim dt As New DataTable
            dt.Load(cmdread)
            If dgvStationUi.Columns.Count = 0 Then ConfigureCrudGridColumns(dgvStationUi, "station")
            dgvStationUi.DataSource = dt
            SetDynamicTotal("station", dt.Rows.Count)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Function ShowCrudInputForm(title As String, fields As Dictionary(Of String, String), Optional keyField As String = "") As Dictionary(Of String, String)
        Dim frm As New Form With {.Text = title, .StartPosition = FormStartPosition.CenterParent, .FormBorderStyle = FormBorderStyle.FixedDialog, .MaximizeBox = False, .MinimizeBox = False, .ClientSize = New Size(500, 80 + (fields.Count * 55))}
        Dim y As Integer = 20
        Dim inputs As New Dictionary(Of String, TextBox)
        For Each kv In fields
            Dim lbl As New Label With {.Text = kv.Key, .Location = New Point(20, y + 5), .AutoSize = True}
            Dim txt As New TextBox With {.Location = New Point(180, y), .Width = 290, .Text = kv.Value}
            If keyField <> "" AndAlso kv.Key = keyField Then txt.ReadOnly = True
            frm.Controls.Add(lbl)
            frm.Controls.Add(txt)
            inputs(kv.Key) = txt
            y += 50
        Next
        Dim btnOk As New Button With {.Text = "Save", .Location = New Point(390, y + 5), .Width = 80}
        Dim btnCancel As New Button With {.Text = "Cancel", .Location = New Point(300, y + 5), .Width = 80}
        frm.Controls.Add(btnOk)
        frm.Controls.Add(btnCancel)
        Dim result As New Dictionary(Of String, String)
        AddHandler btnCancel.Click, Sub()
                                        frm.DialogResult = DialogResult.Cancel
                                        frm.Close()
                                    End Sub
        AddHandler btnOk.Click, Sub()
                                    For Each k In inputs.Keys
                                        result(k) = inputs(k).Text.Trim()
                                    Next
                                    frm.DialogResult = DialogResult.OK
                                    frm.Close()
                                End Sub
        If frm.ShowDialog(Me) = DialogResult.OK Then Return result
        Return Nothing
    End Function

    Private Sub AddMachineryRecord(sender As Object, e As EventArgs)
        Dim data = ShowCrudInputForm("Add Machinery", New Dictionary(Of String, String) From {{"machinery_name", ""}, {"machinery_type", ""}, {"station_id", ""}, {"condition", ""}, {"availability_status", "AVAILABLE"}})
        If data Is Nothing Then Return
        readqueary("INSERT INTO machinery (machinery_name,machinery_type,station_id,`condition`,availability_status) VALUES ('" & data("machinery_name").ToUpper() & "','" & data("machinery_type").ToUpper() & "'," & Val(data("station_id")) & ",'" & data("condition").ToUpper() & "','" & data("availability_status").ToUpper() & "')")
        LoadMachineryCrudGrid(txtMachinerySearch.Text.Trim())
    End Sub

    Private Sub MachineryGrid_Edit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim r = dgvMachineryUi.Rows(e.RowIndex)
        Dim data = ShowCrudInputForm("Edit Machinery", New Dictionary(Of String, String) From {{"machinery_id", r.Cells("machinery_id").Value.ToString()}, {"machinery_name", r.Cells("machinery_name").Value.ToString()}, {"machinery_type", r.Cells("machinery_type").Value.ToString()}, {"station_id", r.Cells("station_id").Value.ToString()}, {"condition", r.Cells("condition").Value.ToString()}, {"availability_status", r.Cells("availability_status").Value.ToString()}}, "machinery_id")
        If data Is Nothing Then Return
        readqueary("UPDATE machinery SET machinery_name='" & data("machinery_name").ToUpper() & "', machinery_type='" & data("machinery_type").ToUpper() & "', station_id=" & Val(data("station_id")) & ", `condition`='" & data("condition").ToUpper() & "', availability_status='" & data("availability_status").ToUpper() & "' WHERE machinery_id=" & Val(data("machinery_id")))
        LoadMachineryCrudGrid(txtMachinerySearch.Text.Trim())
    End Sub

    Private Sub MachineryGrid_DeleteKey(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete OrElse dgvMachineryUi.CurrentRow Is Nothing Then Return
        Dim id = Val(dgvMachineryUi.CurrentRow.Cells("machinery_id").Value.ToString())
        If MessageBox.Show("Delete selected machinery?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            readqueary("DELETE FROM machinery WHERE machinery_id=" & id)
            LoadMachineryCrudGrid(txtMachinerySearch.Text.Trim())
        End If
    End Sub

    Private Sub MachineryGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim col = dgvMachineryUi.Columns(e.ColumnIndex).Name
        If col = "ActionEdit" Then
            MachineryGrid_Edit(sender, e)
        ElseIf col = "ActionDelete" Then
            Dim ke As New KeyEventArgs(Keys.Delete)
            MachineryGrid_DeleteKey(sender, ke)
        End If
    End Sub

    Private Sub AddOperatorRecord(sender As Object, e As EventArgs)
        Dim data = ShowCrudInputForm("Add Operator", New Dictionary(Of String, String) From {{"full_name", ""}, {"position", "MACHINERY OPERATOR"}, {"contact_number", ""}, {"station_id", ""}})
        If data Is Nothing Then Return
        readqueary("INSERT INTO operator (full_name,position,contact_number,station_id) VALUES ('" & data("full_name").ToUpper() & "','" & data("position").ToUpper() & "','" & data("contact_number") & "'," & Val(data("station_id")) & ")")
        LoadOperatorGrid(txtOperatorSearch.Text.Trim())
    End Sub

    Private Sub OperatorGrid_Edit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim r = dgvOperatorUi.Rows(e.RowIndex)
        Dim data = ShowCrudInputForm("Edit Operator", New Dictionary(Of String, String) From {{"operator_id", r.Cells("operator_id").Value.ToString()}, {"full_name", r.Cells("full_name").Value.ToString()}, {"position", r.Cells("position").Value.ToString()}, {"contact_number", r.Cells("contact_number").Value.ToString()}, {"station_id", r.Cells("station_id").Value.ToString()}}, "operator_id")
        If data Is Nothing Then Return
        readqueary("UPDATE operator SET full_name='" & data("full_name").ToUpper() & "', position='" & data("position").ToUpper() & "', contact_number='" & data("contact_number") & "', station_id=" & Val(data("station_id")) & " WHERE operator_id=" & Val(data("operator_id")))
        LoadOperatorGrid(txtOperatorSearch.Text.Trim())
    End Sub

    Private Sub OperatorGrid_DeleteKey(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete OrElse dgvOperatorUi.CurrentRow Is Nothing Then Return
        Dim id = Val(dgvOperatorUi.CurrentRow.Cells("operator_id").Value.ToString())
        If MessageBox.Show("Delete selected operator?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            readqueary("DELETE FROM operator WHERE operator_id=" & id)
            LoadOperatorGrid(txtOperatorSearch.Text.Trim())
        End If
    End Sub

    Private Sub OperatorGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim col = dgvOperatorUi.Columns(e.ColumnIndex).Name
        If col = "ActionEdit" Then
            OperatorGrid_Edit(sender, e)
        ElseIf col = "ActionDelete" Then
            Dim ke As New KeyEventArgs(Keys.Delete)
            OperatorGrid_DeleteKey(sender, ke)
        End If
    End Sub

    Private Sub AddEmployeeRecord(sender As Object, e As EventArgs)
        Dim data = ShowCrudInputForm("Add Employee", New Dictionary(Of String, String) From {{"full_name", ""}, {"position", ""}, {"contact_number", ""}, {"offiice_assignment", "MAIN OFFICE"}})
        If data Is Nothing Then Return
        readqueary("INSERT INTO employee (full_name,position,contact_number,offiice_assignment) VALUES ('" & data("full_name").ToUpper() & "','" & data("position").ToUpper() & "','" & data("contact_number") & "','" & data("offiice_assignment").ToUpper() & "')")
        LoadEmployeeGrid(txtEmployeeSearch.Text.Trim())
    End Sub

    Private Sub EmployeeGrid_Edit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim r = dgvEmployeeUi.Rows(e.RowIndex)
        Dim data = ShowCrudInputForm("Edit Employee", New Dictionary(Of String, String) From {{"employee_id", r.Cells("employee_id").Value.ToString()}, {"full_name", r.Cells("full_name").Value.ToString()}, {"position", r.Cells("position").Value.ToString()}, {"contact_number", r.Cells("contact_number").Value.ToString()}, {"offiice_assignment", r.Cells("offiice_assignment").Value.ToString()}}, "employee_id")
        If data Is Nothing Then Return
        readqueary("UPDATE employee SET full_name='" & data("full_name").ToUpper() & "', position='" & data("position").ToUpper() & "', contact_number='" & data("contact_number") & "', offiice_assignment='" & data("offiice_assignment").ToUpper() & "' WHERE employee_id=" & Val(data("employee_id")))
        LoadEmployeeGrid(txtEmployeeSearch.Text.Trim())
    End Sub

    Private Sub EmployeeGrid_DeleteKey(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete OrElse dgvEmployeeUi.CurrentRow Is Nothing Then Return
        Dim id = Val(dgvEmployeeUi.CurrentRow.Cells("employee_id").Value.ToString())
        If MessageBox.Show("Delete selected employee?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            readqueary("DELETE FROM employee WHERE employee_id=" & id)
            LoadEmployeeGrid(txtEmployeeSearch.Text.Trim())
        End If
    End Sub

    Private Sub EmployeeGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim col = dgvEmployeeUi.Columns(e.ColumnIndex).Name
        If col = "ActionEdit" Then
            EmployeeGrid_Edit(sender, e)
        ElseIf col = "ActionDelete" Then
            Dim ke As New KeyEventArgs(Keys.Delete)
            EmployeeGrid_DeleteKey(sender, ke)
        End If
    End Sub

    Private Sub AddStationRecord(sender As Object, e As EventArgs)
        Dim data = ShowCrudInputForm("Add Station", New Dictionary(Of String, String) From {{"station_name", ""}, {"location", ""}, {"description", ""}})
        If data Is Nothing Then Return
        readqueary("INSERT INTO station (station_name,location,description) VALUES ('" & data("station_name").ToUpper() & "','" & data("location").ToUpper() & "','" & data("description").ToUpper() & "')")
        LoadStationGrid(txtStationSearch.Text.Trim())
    End Sub

    Private Sub StationGrid_Edit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim r = dgvStationUi.Rows(e.RowIndex)
        Dim data = ShowCrudInputForm("Edit Station", New Dictionary(Of String, String) From {{"station_id", r.Cells("station_id").Value.ToString()}, {"station_name", r.Cells("station_name").Value.ToString()}, {"location", r.Cells("location").Value.ToString()}, {"description", r.Cells("description").Value.ToString()}}, "station_id")
        If data Is Nothing Then Return
        readqueary("UPDATE station SET station_name='" & data("station_name").ToUpper() & "', location='" & data("location").ToUpper() & "', description='" & data("description").ToUpper() & "' WHERE station_id=" & Val(data("station_id")))
        LoadStationGrid(txtStationSearch.Text.Trim())
    End Sub

    Private Sub StationGrid_DeleteKey(sender As Object, e As KeyEventArgs)
        If e.KeyCode <> Keys.Delete OrElse dgvStationUi.CurrentRow Is Nothing Then Return
        Dim id = Val(dgvStationUi.CurrentRow.Cells("station_id").Value.ToString())
        If MessageBox.Show("Delete selected station?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then
            readqueary("DELETE FROM station WHERE station_id=" & id)
            LoadStationGrid(txtStationSearch.Text.Trim())
        End If
    End Sub

    Private Sub StationGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim col = dgvStationUi.Columns(e.ColumnIndex).Name
        If col = "ActionEdit" Then
            StationGrid_Edit(sender, e)
        ElseIf col = "ActionDelete" Then
            Dim ke As New KeyEventArgs(Keys.Delete)
            StationGrid_DeleteKey(sender, ke)
        End If
    End Sub

    Private Function GetStationIdByCity(city As String) As Integer
        Dim stationDescription As String = ""
        Dim c As String = city.Trim().ToUpper()

        Select Case c
            Case "DAET", "BASUD", "MERCEDES", "SAN LORENZO RUIZ"
                stationDescription = "STATION 1"
            Case "TALISAY", "LABO", "VINZONS", "SAN VICENTE", "JOSE PANGANIBAN", "PARACALE", "SANTA ELENA"
                stationDescription = "STATION 2"
            Case Else
                Return 0
        End Select

        Dim stationId As Integer = 0
        Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
        Dim localConn As New MySqlConnection(strconn)
        Try
            localConn.Open()
            Dim sql As String = "SELECT station_id FROM station WHERE UPPER(description)=@desc LIMIT 1"
            Using cmdLocal As New MySqlCommand(sql, localConn)
                cmdLocal.Parameters.AddWithValue("@desc", stationDescription)
                Dim result = cmdLocal.ExecuteScalar()
                If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                    stationId = Convert.ToInt32(result)
                End If
            End Using
        Catch
            stationId = 0
        Finally
            If localConn.State = ConnectionState.Open Then localConn.Close()
        End Try

        Return stationId
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New Form With {
            .Text = "Add Request",
            .StartPosition = FormStartPosition.CenterParent,
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .MaximizeBox = False,
            .MinimizeBox = False,
            .ClientSize = New Size(460, 560)
        }

        Dim lblFarmer As New Label With {.Text = "Farmer ID", .Location = New Point(20, 20), .AutoSize = True}
        Dim cmbFarmer As New ComboBox With {.Location = New Point(20, 40), .Width = 400, .DropDownStyle = ComboBoxStyle.DropDownList}

        Dim lblService As New Label With {.Text = "Service ID", .Location = New Point(20, 80), .AutoSize = True}
        Dim cmbService As New ComboBox With {.Location = New Point(20, 100), .Width = 400, .DropDownStyle = ComboBoxStyle.DropDownList}

        Dim lblReqDate As New Label With {.Text = "Request Date", .Location = New Point(20, 140), .AutoSize = True}
        Dim dtpRequestDate As New DateTimePicker With {.Location = New Point(20, 160), .Width = 400, .Format = DateTimePickerFormat.Short}

        Dim lblProv As New Label With {.Text = "Province", .Location = New Point(20, 200), .AutoSize = True}
        Dim cmbProv As New ComboBox With {.Location = New Point(20, 220), .Width = 400, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbProv.Items.Add("CAMARINES NORTE")
        cmbProv.SelectedIndex = 0

        Dim lblCity As New Label With {.Text = "City", .Location = New Point(20, 260), .AutoSize = True}
        Dim cmbCityReq As New ComboBox With {.Location = New Point(20, 280), .Width = 400, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbCityReq.Items.AddRange(New Object() {"DAET", "BASUD", "MERCEDES", "SAN LORENZO RUIZ", "TALISAY", "LABO", "VINZONS", "SAN VICENTE", "JOSE PANGANIBAN", "PARACALE", "SANTA ELENA"})

        Dim lblBrgy As New Label With {.Text = "Barangay", .Location = New Point(20, 320), .AutoSize = True}
        Dim cmbBrgy As New ComboBox With {.Location = New Point(20, 340), .Width = 400, .DropDownStyle = ComboBoxStyle.DropDown}
        cmbBrgy.Items.AddRange(New Object() {"COBANGBANG", "MANCRUZ", "MAGANG", "CALASGASAN", "STO. DOMINGO"})

        Dim lblNearestStation As New Label With {.Text = "Nearest Station (Auto)", .Location = New Point(20, 380), .AutoSize = True}
        Dim txtNearestStation As New TextBox With {.Location = New Point(20, 400), .Width = 400, .ReadOnly = True}

        Dim lblHectares As New Label With {.Text = "Hectares Served", .Location = New Point(20, 440), .AutoSize = True}
        Dim txtHectares As New TextBox With {.Location = New Point(20, 460), .Width = 190}

        Dim lblStatus As New Label With {.Text = "Service Status", .Location = New Point(230, 440), .AutoSize = True}
        Dim cmbStatus As New ComboBox With {.Location = New Point(230, 460), .Width = 190, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbStatus.Items.AddRange(New Object() {"Pending", "Approved", "Rejected"})
        cmbStatus.SelectedIndex = 0

        Dim btnSave As New Button With {.Text = "Submit Request", .Location = New Point(290, 500), .Width = 130, .BackColor = Color.DarkGreen, .ForeColor = Color.White}
        Dim btnCancel As New Button With {.Text = "Cancel", .Location = New Point(200, 500), .Width = 80}

        frm.Controls.AddRange(New Control() {lblFarmer, cmbFarmer, lblService, cmbService, lblReqDate, dtpRequestDate, lblProv, cmbProv, lblCity, cmbCityReq, lblBrgy, cmbBrgy, lblNearestStation, txtNearestStation, lblHectares, txtHectares, lblStatus, cmbStatus, btnSave, btnCancel})

        Try
            Dim dtFarmers As New DataTable
            Dim dtServices As New DataTable
            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name

            Using localConn As New MySqlConnection(strconn)
                localConn.Open()

                Using cmdF As New MySqlCommand("SELECT farmer_id, CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS farmer_label FROM farmer ORDER BY farmer_id DESC", localConn)
                    dtFarmers.Load(cmdF.ExecuteReader())
                End Using

                Using cmdS As New MySqlCommand("SELECT service_id, CONCAT('SRV-', LPAD(service_id, 4, '0')) AS service_label FROM service ORDER BY service_id DESC", localConn)
                    dtServices.Load(cmdS.ExecuteReader())
                End Using
            End Using

            cmbFarmer.DataSource = dtFarmers
            cmbFarmer.DisplayMember = "farmer_label"
            cmbFarmer.ValueMember = "farmer_id"

            cmbService.DataSource = dtServices
            cmbService.DisplayMember = "service_label"
            cmbService.ValueMember = "service_id"
        Catch ex As Exception
            MessageBox.Show("Unable to load request references: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        AddHandler btnCancel.Click, Sub() frm.Close()

        AddHandler cmbCityReq.SelectedIndexChanged,
            Sub()
                Dim selectedCity As String = cmbCityReq.Text.Trim().ToUpper()
                If {"DAET", "BASUD", "MERCEDES", "SAN LORENZO RUIZ"}.Contains(selectedCity) Then
                    txtNearestStation.Text = "STATION 1 - CALASGASAN, DAET, CAMARINES NORTE"
                ElseIf {"TALISAY", "LABO", "VINZONS", "SAN VICENTE", "JOSE PANGANIBAN", "PARACALE", "SANTA ELENA"}.Contains(selectedCity) Then
                    txtNearestStation.Text = "STATION 2 - STO. DOMINGO, VINZONS, CAMARINES NORTE"
                Else
                    txtNearestStation.Text = ""
                End If
            End Sub

        AddHandler btnSave.Click,
            Sub()
                Dim cityText As String = cmbCityReq.Text.Trim().ToUpper()
                Dim barangayText As String = cmbBrgy.Text.Trim().ToUpper()

                If cmbFarmer.SelectedValue Is Nothing OrElse cmbService.SelectedValue Is Nothing Then
                    MessageBox.Show("Please select Farmer ID and Service ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                If String.IsNullOrWhiteSpace(cityText) OrElse String.IsNullOrWhiteSpace(barangayText) Then
                    MessageBox.Show("Please complete farm location (city and barangay).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim hectares As Decimal
                If Not Decimal.TryParse(txtHectares.Text.Trim(), hectares) OrElse hectares <= 0 Then
                    MessageBox.Show("Please provide a valid hectares value.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim stationId As Integer = GetStationIdByCity(cityText)
                If stationId = 0 Then
                    MessageBox.Show("Cannot determine nearest station for selected city.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return
                End If

                Dim farmLocation As String = barangayText & ", " & cityText & ", CAMARINES NORTE"
                Dim selectedStatus As String = cmbStatus.Text.Trim()
                Dim validationDate As Date = Date.Now.Date
                Dim hasRequestId As Boolean = ColumnExists("service_request", "request_id")
                Dim hasOperatorId As Boolean = ColumnExists("service_request", "operator_id")
                Dim hasStationId As Boolean = ColumnExists("service_request", "station_id")
                Dim hasAssignedMachinery As Boolean = ColumnExists("service_request", "assigned_machinery_id")
                Dim hasAssignmentDate As Boolean = ColumnExists("service_request", "assignment_date")

                Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
                Using localConn As New MySqlConnection(strconn)
                    localConn.Open()
                    Dim requestId As Integer = 0

                    Dim sqlInsert As String

                    If hasOperatorId AndAlso hasStationId AndAlso hasAssignedMachinery AndAlso hasAssignmentDate Then
                        sqlInsert = "INSERT INTO service_request " &
                                    "(farmer_id, service_id, request_date, farm_location, hectares_served, validation_date, service_status, station_id, operator_id, assigned_machinery_id, assignment_date) " &
                                    "VALUES (@farmer_id, @service_id, @request_date, @farm_location, @hectares_served, @validation_date, @service_status, @station_id, NULL, NULL, NULL)"
                    ElseIf hasStationId Then
                        sqlInsert = "INSERT INTO service_request " &
                                    "(farmer_id, service_id, request_date, farm_location, hectares_served, validation_date, service_status, station_id) " &
                                    "VALUES (@farmer_id, @service_id, @request_date, @farm_location, @hectares_served, @validation_date, @service_status, @station_id)"
                    Else
                        sqlInsert = "INSERT INTO service_request " &
                                    "(farmer_id, service_id, request_date, farm_location, hectares_served, validation_date, service_status) " &
                                    "VALUES (@farmer_id, @service_id, @request_date, @farm_location, @hectares_served, @validation_date, @service_status)"
                    End If

                    Using cmdIns As New MySqlCommand(sqlInsert, localConn)
                        cmdIns.Parameters.AddWithValue("@farmer_id", CInt(cmbFarmer.SelectedValue))
                        cmdIns.Parameters.AddWithValue("@service_id", CInt(cmbService.SelectedValue))
                        cmdIns.Parameters.AddWithValue("@request_date", dtpRequestDate.Value.Date)
                        cmdIns.Parameters.AddWithValue("@farm_location", farmLocation)
                        cmdIns.Parameters.AddWithValue("@hectares_served", hectares)
                        cmdIns.Parameters.AddWithValue("@validation_date", validationDate)
                        cmdIns.Parameters.AddWithValue("@service_status", selectedStatus)
                        If sqlInsert.Contains("@station_id") Then cmdIns.Parameters.AddWithValue("@station_id", stationId)
                        cmdIns.ExecuteNonQuery()
                    End Using

                    If hasRequestId Then
                        Using cmdLast As New MySqlCommand("SELECT LAST_INSERT_ID()", localConn)
                            Dim rid = cmdLast.ExecuteScalar()
                            If rid IsNot Nothing AndAlso Not IsDBNull(rid) Then requestId = Convert.ToInt32(rid)
                        End Using
                    End If

                    If selectedStatus.Equals("Rejected", StringComparison.OrdinalIgnoreCase) Then
                        If hasRequestId AndAlso requestId > 0 Then
                            Using cmdDel As New MySqlCommand("DELETE FROM service_request WHERE request_id=@request_id", localConn)
                                cmdDel.Parameters.AddWithValue("@request_id", requestId)
                                cmdDel.ExecuteNonQuery()
                            End Using
                        Else
                            Using cmdDel As New MySqlCommand(
                                "DELETE FROM service_request " &
                                "WHERE farmer_id=@farmer_id AND service_id=@service_id AND request_date=@request_date " &
                                "AND farm_location=@farm_location AND hectares_served=@hectares_served AND validation_date=@validation_date " &
                                "ORDER BY validation_date DESC LIMIT 1", localConn)
                                cmdDel.Parameters.AddWithValue("@farmer_id", CInt(cmbFarmer.SelectedValue))
                                cmdDel.Parameters.AddWithValue("@service_id", CInt(cmbService.SelectedValue))
                                cmdDel.Parameters.AddWithValue("@request_date", dtpRequestDate.Value.Date)
                                cmdDel.Parameters.AddWithValue("@farm_location", farmLocation)
                                cmdDel.Parameters.AddWithValue("@hectares_served", hectares)
                                cmdDel.Parameters.AddWithValue("@validation_date", validationDate)
                                cmdDel.ExecuteNonQuery()
                            End Using
                        End If
                        MessageBox.Show("Request was rejected and automatically removed.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ElseIf selectedStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase) Then
                        MessageBox.Show("Request approved. Next step: assign an operator.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Dim opForm As New Form With {
                            .Text = "Choose Operator",
                            .StartPosition = FormStartPosition.CenterParent,
                            .FormBorderStyle = FormBorderStyle.FixedDialog,
                            .ClientSize = New Size(360, 150),
                            .MaximizeBox = False,
                            .MinimizeBox = False
                        }

                        Dim lblOp As New Label With {.Text = "Choose Operator", .Location = New Point(20, 20), .AutoSize = True}
                        Dim cmbOp As New ComboBox With {.Location = New Point(20, 45), .Width = 320, .DropDownStyle = ComboBoxStyle.DropDownList}
                        Dim btnOpOk As New Button With {.Text = "OK", .Location = New Point(260, 95), .Width = 80}
                        opForm.Controls.AddRange(New Control() {lblOp, cmbOp, btnOpOk})

                        Dim dtOps As New DataTable
                        Using cmdOps As New MySqlCommand("SELECT operator_id, full_name FROM operator WHERE station_id=@station_id ORDER BY full_name", localConn)
                            cmdOps.Parameters.AddWithValue("@station_id", stationId)
                            dtOps.Load(cmdOps.ExecuteReader())
                        End Using

                        cmbOp.DataSource = dtOps
                        cmbOp.DisplayMember = "full_name"
                        cmbOp.ValueMember = "operator_id"

                        AddHandler btnOpOk.Click,
                            Sub()
                                If cmbOp.SelectedValue Is Nothing Then
                                    MessageBox.Show("Please choose an operator.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                    Return
                                End If

                                Dim machineryId As Integer = 0
                                Using cmdMach As New MySqlCommand("SELECT machinery_id FROM service WHERE service_id=@sid LIMIT 1", localConn)
                                    cmdMach.Parameters.AddWithValue("@sid", CInt(cmbService.SelectedValue))
                                    Dim machObj = cmdMach.ExecuteScalar()
                                    If machObj IsNot Nothing AndAlso Not IsDBNull(machObj) Then
                                        machineryId = Convert.ToInt32(machObj)
                                    End If
                                End Using

                                If machineryId > 0 Then
                                    Using cmdUpMach As New MySqlCommand("UPDATE machinery SET availability_status='NOT AVAILABLE' WHERE machinery_id=@mid", localConn)
                                        cmdUpMach.Parameters.AddWithValue("@mid", machineryId)
                                        cmdUpMach.ExecuteNonQuery()
                                    End Using
                                End If

                                If hasRequestId AndAlso hasOperatorId AndAlso requestId > 0 Then
                                    Dim updateRequestSql As String = "UPDATE service_request SET operator_id=@operator_id"
                                    If hasAssignedMachinery Then updateRequestSql &= ", assigned_machinery_id=@machinery_id"
                                    If hasAssignmentDate Then updateRequestSql &= ", assignment_date=@assignment_date"
                                    updateRequestSql &= " WHERE request_id=@request_id"

                                    Using cmdUpdateReq As New MySqlCommand(updateRequestSql, localConn)
                                        cmdUpdateReq.Parameters.AddWithValue("@operator_id", CInt(cmbOp.SelectedValue))
                                        If hasAssignedMachinery Then cmdUpdateReq.Parameters.AddWithValue("@machinery_id", machineryId)
                                        If hasAssignmentDate Then cmdUpdateReq.Parameters.AddWithValue("@assignment_date", Date.Now.Date)
                                        cmdUpdateReq.Parameters.AddWithValue("@request_id", requestId)
                                        cmdUpdateReq.ExecuteNonQuery()
                                    End Using
                                End If

                                opForm.DialogResult = DialogResult.OK
                                opForm.Close()
                                MessageBox.Show("Operator assigned and machinery set to Not Available.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            End Sub

                        opForm.ShowDialog(frm)
                    Else
                        MessageBox.Show("Request submitted successfully.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using

                LoadRequestsGrid()
                LoadMachineryGrid()
                frm.Close()
            End Sub

        frm.ShowDialog(Me)
    End Sub
End Class
