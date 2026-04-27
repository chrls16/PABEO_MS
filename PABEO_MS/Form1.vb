Imports System.Security

Public Class frmPanelHolder
    ' Global variables for the class
    Dim pnlOverlay As New Panel
    Private btnBack As Object

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
            Dim sql As String = "SELECT CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS formatted_id, " &
                               "full_name, residence_address, contact_number, classification, registration_status " &
                               "FROM farmer ORDER BY created_at DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                dgvFarmers.AutoGenerateColumns = False
                dgvFarmers.DataSource = dt

                dgvFarmers.AllowUserToAddRows = False
                ' This only forces the text color, not the background/selection colors
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
            Dim sql As String = "SELECT CONCAT('SRV-', LPAD(service_id, 4, '0')) AS formatted_id, " &
                           "service_name, service_type, description, policy_limit, employee_id " &
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
            Dim sql As String = "SELECT * FROM service"
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
End Class
