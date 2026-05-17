Imports System.Security
Imports MySql.Data.MySqlClient
Imports OfficeOpenXml
Imports System.ComponentModel

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
    ' Private txtMachinerySearch As TextBox
    Private txtOperatorSearch As TextBox
    Private txtEmployeeSearch As TextBox
    Private txtStationSearch As TextBox
    Public pnlReports As New Panel
    Public pnlFlexibleSearch As New Panel
    Private cmbReportTable As ComboBox
    Private btnServiceEditTop As Button
    Private btnServiceDeleteTop As Button
    Private lblAmountApprovedRequests As Label
    Private lblAmountRejectedRequests As Label

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
            MsgBox("Connected successfully to PABEO database! Please login.", MsgBoxStyle.Information)

            ' ── ADD THIS LINE — hide test button when switching to login ──
            BtnTestConection.Visible = False

            ' Transform config panel into Login panel
            lblServer.Visible = False
            txtServer.Visible = False
            lblDatabase.Visible = False
            txtDatabase.Visible = False

            lblUsername.Text = "Username"
            lblPWD.Text = "Password"

            txtUID.Text = ""
            txtPWD.Text = ""

            ' Shift elements up and center them for login UI
            Dim startX As Integer = CInt((pnlConfigForm.Width - txtUID.Width) / 2)

            lblUsername.Location = New Point(startX, 100)
            txtUID.Location = New Point(startX, 135)

            lblPWD.Location = New Point(startX, 190)
            txtPWD.Location = New Point(startX, 225)

            btnConnect.Location = New Point(startX, 290)
            btnConnect.Size = New Size(txtUID.Width, 40)
            btnConnect.Text = "Login"

            ' Change event handler
            RemoveHandler btnConnect.Click, AddressOf btnConnect_Click
            AddHandler btnConnect.Click, AddressOf btnLogin_Click
        End If
    End Sub

    Private Function HashPassword(password As String) As String
        Using sha256 As System.Security.Cryptography.SHA256 = System.Security.Cryptography.SHA256.Create()
            Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(password)
            Dim hash As Byte() = sha256.ComputeHash(bytes)
            Dim sb As New System.Text.StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sb.Append(hash(i).ToString("X2"))
            Next
            Return sb.ToString()
        End Using
    End Function

    Private Sub btnLogin_Click(sender As Object, e As EventArgs)
        If txtUID.Text = "admin" AndAlso txtPWD.Text = "admin" Then
            ' 1. Set the Parent of the Farmers Panel to the MDI's pnlForms
            Me.pnlFarmers.Parent = mdiPABEO.pnlForms

            ' 2. Make it fill the entire space of pnlForms
            Me.pnlFarmers.Dock = DockStyle.Fill

            ' 3. Show the MDI and Hide this config form
            mdiPABEO.Show()
            mdiPABEO.ApplyRolePermissions("admin")
            Me.Hide()
        Else
            Dim hashedInput As String = HashPassword(txtPWD.Text)
            Dim sql As String = "SELECT * FROM employee WHERE email_address = '" & txtUID.Text.Replace("'", "''") & "' AND password = '" & hashedInput & "'"
            readqueary(sql)

            If cmdread IsNot Nothing AndAlso cmdread.HasRows Then
                cmdread.Close()
                Me.pnlFarmers.Parent = mdiPABEO.pnlForms
                Me.pnlFarmers.Dock = DockStyle.Fill
                mdiPABEO.Show()
                mdiPABEO.ApplyRolePermissions("employee")
                Me.Hide()
            Else
                If cmdread IsNot Nothing Then cmdread.Close()
                MsgBox("Invalid username or password.", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub frmPanelHolder_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Environment.Exit(0)
    End Sub


    Private Sub frmPanelHolder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlConfig.Visible = True
        pnlConfig.BringToFront()

        ' Ensure required columns exist and fix data on startup
        Try
            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
            Using fixConn As New MySql.Data.MySqlClient.MySqlConnection(strconn)
                fixConn.Open()

                ' Ensure operator table has availability_status column
                Try
                    Using c As New MySql.Data.MySqlClient.MySqlCommand("ALTER TABLE operator ADD COLUMN availability_status VARCHAR(50) DEFAULT 'AVAILABLE'", fixConn)
                        c.ExecuteNonQuery()
                    End Using
                Catch : End Try

                ' Fix operators with NULL availability
                Using fixCmd As New MySql.Data.MySqlClient.MySqlCommand("UPDATE operator SET availability_status='AVAILABLE' WHERE availability_status IS NULL OR availability_status=''", fixConn)
                    fixCmd.ExecuteNonQuery()
                End Using

                ' Ensure service_request table has all required columns
                Dim alterCommands() As String = {
                    "ALTER TABLE service_request ADD COLUMN request_id INT AUTO_INCREMENT PRIMARY KEY",
                    "ALTER TABLE service_request ADD COLUMN station_id INT",
                    "ALTER TABLE service_request ADD COLUMN operator_id INT",
                    "ALTER TABLE service_request ADD COLUMN assigned_machinery_id INT",
                    "ALTER TABLE service_request ADD COLUMN assignment_date DATE"
                }
                For Each alterSql In alterCommands
                    Try
                        Using c As New MySql.Data.MySqlClient.MySqlCommand(alterSql, fixConn)
                            c.ExecuteNonQuery()
                        End Using
                    Catch
                        ' Column already exists, ignore
                    End Try
                Next

                ' Ensure employee table has email and password
                Try
                    Using c As New MySql.Data.MySqlClient.MySqlCommand("ALTER TABLE employee ADD COLUMN email_address VARCHAR(255), ADD COLUMN password VARCHAR(255)", fixConn)
                        c.ExecuteNonQuery()
                    End Using
                Catch : End Try

                ' Hash any existing plaintext passwords (retroactive fix)
                Try
                    Dim updates As New Dictionary(Of Integer, String)
                    Using c As New MySql.Data.MySqlClient.MySqlCommand("SELECT employee_id, IFNULL(password, '') as password FROM employee", fixConn)
                        Using reader As MySql.Data.MySqlClient.MySqlDataReader = c.ExecuteReader()
                            While reader.Read()
                                Dim pw As String = reader("password").ToString()
                                ' SHA256 hex is exactly 64 chars. If it's shorter, it's a plaintext password!
                                If pw.Length > 0 AndAlso pw.Length < 64 Then
                                    updates.Add(Convert.ToInt32(reader("employee_id")), HashPassword(pw))
                                End If
                            End While
                        End Using
                    End Using

                    For Each kvp In updates
                        Using uCmd As New MySql.Data.MySqlClient.MySqlCommand("UPDATE employee SET password='" & kvp.Value & "' WHERE employee_id=" & kvp.Key, fixConn)
                            uCmd.ExecuteNonQuery()
                        End Using
                    Next
                Catch : End Try

                ' Backfill missing assigned_machinery_id from the service table for existing records
                Try
                    Using fixCmd2 As New MySql.Data.MySqlClient.MySqlCommand(
                        "UPDATE service_request sr " &
                        "JOIN service s ON sr.service_id = s.service_id " &
                        "SET sr.assigned_machinery_id = s.machinery_id " &
                        "WHERE sr.assigned_machinery_id IS NULL AND s.machinery_id IS NOT NULL", fixConn)
                        fixCmd2.ExecuteNonQuery()
                    End Using
                Catch : End Try
            End Using
        Catch
            ' Ignore connection errors during startup
        End Try

        LoadFarmersGrid()
        FillEmployeeComboBox()

        LoadServiceGrid()
        RefreshServiceStats()
        EnsureServiceTopActionButtons()
        ConfigureRequestsGridColumns()
        BuildRequestActionButtons()
        BuildRequestStatusPanels()
        LoadRequestsGrid()
        InitializeFarmerEditPanel()
        BuildManagementPanelUI(pnlMachinery, "Machinery Management", "P.A.B.E.O. machinery inventory records", "machinery")
        BuildManagementPanelUI(pnlOperator, "Operator Management", "P.A.B.E.O. operator records and assignments", "operator")
        BuildManagementPanelUI(pnlEmployee, "Employee Management", "P.A.B.E.O. employee records", "employee")
        BuildManagementPanelUI(pnlStation, "Station Management", "P.A.B.E.O. station records", "station")
        BuildReportsPanel()
        BuildFlexibleSearchPanel()
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
        pnlCreateService.Dock = DockStyle.None
        pnlCreateService.Visible = True

        ' Add the panel to the MDI controls so it can float over the overlay
        If Not mdiPABEO.Controls.Contains(pnlCreateService) Then
            mdiPABEO.Controls.Add(pnlCreateService)
        End If

        ' Force the Service Panel to stay ABOVE the dim overlay
        pnlCreateService.BringToFront()

        ' 3. Center the panel within the overlay
        Dim x = (pnlOverlay.Width - pnlCreateService.Width) \ 2
        Dim y = (pnlOverlay.Height - pnlCreateService.Height) \ 2
        pnlCreateService.Location = New Point(x, y)

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

    Public Sub LoadEmployeesToDropdown()
        Try
            ' 1. Select both the ID and the Name from your employee table
            Dim sql As String = "SELECT employee_id, full_name FROM employee ORDER BY full_name ASC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                ' 2. Bind the data to the ComboBox
                cmbEmployeeID.DataSource = dt

                ' 3. The "DisplayMember" is what the user actually sees in the dropdown
                cmbEmployeeID.DisplayMember = "full_name"

                ' 4. The "ValueMember" is the hidden ID attached to whatever name they pick
                cmbEmployeeID.ValueMember = "employee_id"

                ' 5. Leave it blank by default
                cmbEmployeeID.SelectedIndex = -1
            End If

        Catch ex As Exception
            Console.WriteLine("Error loading employees: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub



    Private Sub btnSaveService_Click(sender As Object, e As EventArgs) Handles btnSaveService.Click
        ' 1. Basic Validation - Ensure required dropdowns are selected
        If cmbEmployeeID.SelectedIndex = -1 Then
            MessageBox.Show("Please select an Employee.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim empID As String = cmbEmployeeID.SelectedValue.ToString()
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

        ' 3. SQL Query - Points to the singular 'service' table
        Dim sql As String = "INSERT INTO service (service_name, service_type, description, machinery_id, policy_limit, employee_id) " &
                           "VALUES ('" & sName & "', '" & sType & "', '" & sDesc & "', '" & machID & "', '" & pLimit & "', '" & empID & "')"

        Try
            ' Execute query via Module1
            readqueary(sql)

            MessageBox.Show("Service added successfully!", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadServiceGrid()
            ClearServiceFields()

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
            Case "CORN SHELLER"
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
            Dim sql As String = "SELECT s.service_id, " &
                                "CONCAT('SRV-', LPAD(s.service_id, 4, '0')) AS formatted_service_id, " &
                                "IFNULL(m.machinery_name, 'N/A') AS machinery_name, " &
                                "s.service_name, s.service_type, s.description, s.policy_limit, " &
                                "e.full_name AS employee_name " &
                                "FROM service s " &
                                "LEFT JOIN machinery m ON s.machinery_id = m.machinery_id " &
                                "LEFT JOIN employee e ON s.employee_id = e.employee_id " &
                                "ORDER BY s.service_id DESC"
            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                dgvServices.AutoGenerateColumns = False
                dgvServices.DataSource = dt

                dgvServices.DefaultCellStyle.WrapMode = DataGridViewTriState.True
                dgvServices.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
                dgvServices.DefaultCellStyle.Padding = New Padding(5, 10, 5, 10)

                lblServiceTotal.Text = dt.Rows.Count.ToString("00")
            End If

        Catch ex As Exception
            Console.WriteLine("Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub EnsureServiceActionColumns()
        If Not dgvServices.Columns.Contains("ServiceEdit") Then
            Dim colEdit As New DataGridViewImageColumn With {
                .Name = "ServiceEdit",
                .HeaderText = "Edit",
                .Image = FarmerEdit.Image,
                .Width = 75
            }
            dgvServices.Columns.Add(colEdit)
        End If

        If Not dgvServices.Columns.Contains("ServiceDelete") Then
            Dim colDelete As New DataGridViewImageColumn With {
                .Name = "ServiceDelete",
                .HeaderText = "Delete",
                .Image = FarmerDelete.Image,
                .Width = 75
            }
            dgvServices.Columns.Add(colDelete)
        End If
    End Sub

    Private Sub BuildRequestActionButtons()
        Dim btnEditRequest As New Button With {
            .Text = "Edit Request",
            .Location = New Point(1020, 410),
            .Size = New Size(142, 37),
            .BackColor = Color.White,
            .ForeColor = Color.DarkGreen,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        }
        btnEditRequest.FlatAppearance.BorderSize = 1
        btnEditRequest.FlatAppearance.BorderColor = Color.DarkGreen

        Dim btnDeleteRequest As New Button With {
            .Text = "Delete Request",
            .Location = New Point(1170, 410),
            .Size = New Size(142, 37),
            .BackColor = Color.White,
            .ForeColor = Color.DarkRed,
            .FlatStyle = FlatStyle.Flat,
            .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        }
        btnDeleteRequest.FlatAppearance.BorderSize = 1
        btnDeleteRequest.FlatAppearance.BorderColor = Color.DarkRed

        AddHandler btnEditRequest.Click,
            Sub(sender As Object, e As EventArgs)
                If dgvRequests.CurrentRow IsNot Nothing AndAlso dgvRequests.CurrentRow.Index >= 0 Then
                    EditRequestRow(dgvRequests.CurrentRow.Index)
                Else
                    MessageBox.Show("Please select a request to edit.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Sub

        AddHandler btnDeleteRequest.Click,
            Sub(sender As Object, e As EventArgs)
                If dgvRequests.CurrentRow IsNot Nothing AndAlso dgvRequests.CurrentRow.Index >= 0 Then
                    DeleteRequestRow(dgvRequests.CurrentRow.Index)
                Else
                    MessageBox.Show("Please select a request to delete.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Sub

        pnlRequests.Controls.Add(btnEditRequest)
        pnlRequests.Controls.Add(btnDeleteRequest)
    End Sub

    Private Sub EnsureServiceTopActionButtons()
        If btnServiceEditTop Is Nothing Then
            btnServiceEditTop = New Button With {
                .Name = "btnServiceEditTop",
                .Text = "Edit",
                .BackColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold),
                .ImageAlign = ContentAlignment.MiddleLeft,
                .Location = New Point(1167, 410),
                .Size = New Size(147, 37)
            }
            btnServiceEditTop.FlatAppearance.BorderColor = Color.Silver
            AddHandler btnServiceEditTop.Click, AddressOf btnServiceEditTop_Click
            pnlServices.Controls.Add(btnServiceEditTop)
            btnServiceEditTop.BringToFront()
        End If

        If btnServiceDeleteTop Is Nothing Then
            btnServiceDeleteTop = New Button With {
                .Name = "btnServiceDeleteTop",
                .Text = "Delete",
                .BackColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold),
                .ImageAlign = ContentAlignment.MiddleLeft,
                .Location = New Point(1014, 410),
                .Size = New Size(147, 37)
            }
            btnServiceDeleteTop.FlatAppearance.BorderColor = Color.Silver
            AddHandler btnServiceDeleteTop.Click, AddressOf btnServiceDeleteTop_Click
            pnlServices.Controls.Add(btnServiceDeleteTop)
            btnServiceDeleteTop.BringToFront()
        End If
    End Sub

    Private Sub btnServiceEditTop_Click(sender As Object, e As EventArgs)
        If dgvServices.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a service row first.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        EditServiceRow(dgvServices.CurrentRow.Index)
    End Sub

    Private Sub btnServiceDeleteTop_Click(sender As Object, e As EventArgs)
        If dgvServices.CurrentRow Is Nothing Then
            MessageBox.Show("Please select a service row first.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If
        DeleteServiceRow(dgvServices.CurrentRow.Index)
    End Sub

    Private Sub dgvServices_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvServices.CellClick
        If e.RowIndex < 0 Then Return
        Dim colName = dgvServices.Columns(e.ColumnIndex).Name
        If colName = "ServiceEdit" Then
            EditServiceRow(e.RowIndex)
        ElseIf colName = "ServiceDelete" Then
            DeleteServiceRow(e.RowIndex)
        End If
    End Sub

    Private Sub EditServiceRow(rowIndex As Integer)
        Try
            Dim row = dgvServices.Rows(rowIndex)
            Dim servicePk As Integer = ExtractNumericId(Convert.ToString(row.Cells("service_id").Value))
            If servicePk <= 0 Then
                MessageBox.Show("Invalid service ID.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim frm As New Form With {
                .Text = "Edit Service",
                .StartPosition = FormStartPosition.CenterParent,
                .FormBorderStyle = FormBorderStyle.FixedDialog,
                .ClientSize = New Size(520, 360),
                .MaximizeBox = False,
                .MinimizeBox = False
            }

            Dim lblName As New Label With {.Text = "Service Name", .Location = New Point(20, 20), .AutoSize = True}
            Dim txtName As New TextBox With {.Location = New Point(20, 40), .Width = 470, .Text = Convert.ToString(row.Cells("service_name").Value)}

            Dim lblType As New Label With {.Text = "Service Type", .Location = New Point(20, 80), .AutoSize = True}
            Dim txtType As New TextBox With {.Location = New Point(20, 100), .Width = 220, .Text = Convert.ToString(row.Cells("service_type").Value)}

            Dim lblMach As New Label With {.Text = "Machinery ID", .Location = New Point(270, 80), .AutoSize = True}
            Dim txtMach As New TextBox With {.Location = New Point(270, 100), .Width = 220, .Text = Convert.ToString(row.Cells("machinery_id").Value)}

            Dim lblDesc As New Label With {.Text = "Description", .Location = New Point(20, 140), .AutoSize = True}
            Dim txtDesc As New TextBox With {.Location = New Point(20, 160), .Width = 470, .Height = 70, .Multiline = True, .Text = Convert.ToString(row.Cells("service_description").Value)}

            Dim lblPolicy As New Label With {.Text = "Policy Limit", .Location = New Point(20, 240), .AutoSize = True}
            Dim txtPolicy As New TextBox With {.Location = New Point(20, 260), .Width = 360, .Text = Convert.ToString(row.Cells("service_policy_limit").Value)}

            Dim lblEmp As New Label With {.Text = "Employee ID", .Location = New Point(390, 240), .AutoSize = True}
            Dim txtEmp As New TextBox With {.Location = New Point(390, 260), .Width = 100, .Text = Convert.ToString(row.Cells("employee_id").Value)}

            Dim btnSave As New Button With {.Text = "Save", .Location = New Point(410, 315), .Width = 80}
            Dim btnCancel As New Button With {.Text = "Cancel", .Location = New Point(320, 315), .Width = 80}
            frm.Controls.AddRange(New Control() {lblName, txtName, lblType, txtType, lblMach, txtMach, lblDesc, txtDesc, lblPolicy, txtPolicy, lblEmp, txtEmp, btnSave, btnCancel})
            AddHandler btnCancel.Click, Sub() frm.Close()
            AddHandler btnSave.Click,
                Sub()
                    Dim sql As String = "UPDATE service SET " &
                                        "service_name=@name, service_type=@type, description=@desc, machinery_id=@mach, policy_limit=@policy, employee_id=@emp " &
                                        "WHERE service_id=@id"
                    Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                        localConn.Open()
                        Using c As New MySqlCommand(sql, localConn)
                            c.Parameters.AddWithValue("@name", txtName.Text.Trim().ToUpper())
                            c.Parameters.AddWithValue("@type", txtType.Text.Trim().ToUpper())
                            c.Parameters.AddWithValue("@desc", txtDesc.Text.Trim().ToUpper())
                            c.Parameters.AddWithValue("@mach", Val(txtMach.Text))
                            c.Parameters.AddWithValue("@policy", txtPolicy.Text.Trim().ToUpper())
                            c.Parameters.AddWithValue("@emp", Val(txtEmp.Text))
                            c.Parameters.AddWithValue("@id", servicePk)
                            c.ExecuteNonQuery()
                        End Using
                    End Using
                    frm.Close()
                    LoadServiceGrid()
                End Sub
            frm.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show("Service edit failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteServiceRow(rowIndex As Integer)
        Try
            If MessageBox.Show("Delete selected service?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return
            Dim row = dgvServices.Rows(rowIndex)
            Dim servicePk As Integer = ExtractNumericId(Convert.ToString(row.Cells("service_id").Value))
            If servicePk <= 0 Then Return

            Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                localConn.Open()
                Using c As New MySqlCommand("DELETE FROM service WHERE service_id=@id", localConn)
                    c.Parameters.AddWithValue("@id", servicePk)
                    c.ExecuteNonQuery()
                End Using
            End Using
            LoadServiceGrid()
        Catch ex As Exception
            If ex.Message.Contains("foreign key constraint") Then
                MessageBox.Show("Cannot delete this Service because it is currently linked to one or more active Farmer Requests. Please delete or reassign those requests first before deleting this service.", "Deletion Prevented", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Service delete failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End Try
    End Sub

    Public Sub ClearServiceFields()
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

        ' --- EDIT ACTION ---
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

        ' --- DELETE ACTION ---
        If dgvFarmers.Columns(e.ColumnIndex).Name = "FarmerDelete" Then
            Try
                Dim pidCell = dgvFarmers.Rows(e.RowIndex).Cells("farmer_id").Value
                Dim nameCell = dgvFarmers.Rows(e.RowIndex).Cells("full_name").Value

                If pidCell IsNot Nothing AndAlso Not IsDBNull(pidCell) Then
                    Dim fName As String = nameCell.ToString()
                    Dim farmerID As String = Val(pidCell.ToString())

                    Dim result = MessageBox.Show("Are you sure you want to delete " & fName & " and all their service history?",
                                                             "Confirm Deletion",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Warning)

                    If result = DialogResult.Yes Then
                        ' 1. Delete dependent records first (to avoid foreign key error)
                        ' This clears the 'service_request' table records for this farmer
                        readqueary("DELETE FROM service_request WHERE farmer_id = " & farmerID)

                        ' 2. Now delete the actual farmer record
                        readqueary("DELETE FROM farmer WHERE farmer_id = " & farmerID)

                        MessageBox.Show("Farmer and all related records deleted successfully.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        ' 3. Refresh the grid
                        LoadFarmersGrid()
                    End If
                Else
                    MessageBox.Show("System error: Primary key 'farmer_id' is missing for this row.", "PABEO")
                End If

            Catch ex As Exception
                ' If it still fails for some other reason, this will show you why
                MessageBox.Show("Logic Error: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

        ' Column 5 is now a ComboBoxColumn — DataPropertyName is set in the Designer
        dgvRequests.Columns(5).HeaderText = "Service Status"

        If dgvRequests.Columns.Contains("DataGridViewImageColumn1") Then
            dgvRequests.Columns("DataGridViewImageColumn1").Visible = False
        End If
        If dgvRequests.Columns.Contains("DataGridViewImageColumn2") Then
            dgvRequests.Columns("DataGridViewImageColumn2").Visible = False
        End If

        If Not dgvRequests.Columns.Contains("assigned_operator") Then
            dgvRequests.Columns.Add(New DataGridViewTextBoxColumn With {
                .Name = "assigned_operator",
                .DataPropertyName = "assigned_operator",
                .HeaderText = "Assigned Operator",
                .Width = 250
            })
        End If
    End Sub

    Public Sub LoadRequestsGrid()
        Try
            Dim hasOperatorColumn As Boolean = ColumnExists("service_request", "operator_id")
            Dim hasRequestId As Boolean = ColumnExists("service_request", "request_id")

            ' Build SELECT list
            Dim selectList As String = "sr.farmer_id, sr.service_id, " &
                                   "CONCAT('RSBSA-', LPAD(sr.farmer_id, 4, '0')) AS farmer_display_id, " &
                                   "CONCAT('SRV-', LPAD(sr.service_id, 4, '0')) AS service_display_id, " &
                                   "sr.request_date, sr.farm_location, sr.hectares_served, sr.service_status"

            If hasRequestId Then
                selectList &= ", sr.request_id"
            End If

            If hasOperatorColumn Then
                selectList &= ", IFNULL(o.full_name, '-') AS assigned_operator"
            End If

            Dim sql As String = "SELECT " & selectList & " FROM service_request sr"
            If hasOperatorColumn Then
                sql &= " LEFT JOIN operator o ON o.operator_id = sr.operator_id"
            End If
            sql &= " ORDER BY sr.request_date DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                ' If the query doesn't have operator column, add it manually to avoid binding errors
                If Not hasOperatorColumn AndAlso Not dt.Columns.Contains("assigned_operator") Then
                    dt.Columns.Add("assigned_operator", GetType(String))
                    For Each row As DataRow In dt.Rows
                        row("assigned_operator") = "-"
                    Next
                End If

                dgvRequests.AutoGenerateColumns = False
                dgvRequests.DataSource = dt
                dgvRequests.AllowUserToAddRows = False

                ' Ensure the assigned_operator column exists in the grid
                If Not dgvRequests.Columns.Contains("assigned_operator") Then
                    dgvRequests.Columns.Add(New DataGridViewTextBoxColumn With {
                        .Name = "assigned_operator",
                        .DataPropertyName = "assigned_operator",
                        .HeaderText = "Assigned Operator",
                        .Width = 250
                    })
                End If

                ' Hide internal ID columns
                If dgvRequests.Columns.Contains("farmer_id") Then
                    dgvRequests.Columns("farmer_id").Visible = False
                End If
                If dgvRequests.Columns.Contains("service_id") Then
                    dgvRequests.Columns("service_id").Visible = False
                End If
                If dgvRequests.Columns.Contains("request_id") Then
                    dgvRequests.Columns("request_id").Visible = False
                End If
                ' Hide the old image columns (edit/delete icons replaced by buttons)
                If dgvRequests.Columns.Contains("DataGridViewImageColumn1") Then
                    dgvRequests.Columns("DataGridViewImageColumn1").Visible = False
                End If
                If dgvRequests.Columns.Contains("DataGridViewImageColumn2") Then
                    dgvRequests.Columns("DataGridViewImageColumn2").Visible = False
                End If
            End If

            RefreshRequestStats()
        Catch ex As Exception
            Console.WriteLine("Request Load Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub txtRequestSearch_TextChanged(sender As Object, e As EventArgs) Handles txtRequestSearch.TextChanged
        Dim searchKey = txtRequestSearch.Text.Trim

        If searchKey = "" Then
            LoadRequestsGrid()
            Return
        End If

        Try
            Dim hasOperatorColumn As Boolean = ColumnExists("service_request", "operator_id")
            Dim hasRequestId As Boolean = ColumnExists("service_request", "request_id")

            Dim selectList As String = "sr.farmer_id, sr.service_id, " &
                                   "CONCAT('RSBSA-', LPAD(sr.farmer_id, 4, '0')) AS farmer_display_id, " &
                                   "CONCAT('SRV-', LPAD(sr.service_id, 4, '0')) AS service_display_id, " &
                                   "sr.request_date, sr.farm_location, sr.hectares_served, sr.service_status"

            If hasRequestId Then selectList &= ", sr.request_id"
            If hasOperatorColumn Then selectList &= ", IFNULL(o.full_name, '-') AS assigned_operator"

            Dim sql As String = "SELECT " & selectList & " FROM service_request sr"
            If hasOperatorColumn Then sql &= " LEFT JOIN operator o ON o.operator_id = sr.operator_id"

            sql &= " WHERE CONCAT('RSBSA-', LPAD(sr.farmer_id, 4, '0')) LIKE '%" & searchKey & "%'" &
                   " OR CONCAT('SRV-', LPAD(sr.service_id, 4, '0')) LIKE '%" & searchKey & "%'" &
                   " OR sr.farm_location LIKE '%" & searchKey & "%'" &
                   " OR sr.service_status LIKE '%" & searchKey & "%'"

            If hasOperatorColumn Then sql &= " OR o.full_name LIKE '%" & searchKey & "%'"

            sql &= " ORDER BY sr.request_date DESC"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                If Not hasOperatorColumn AndAlso Not dt.Columns.Contains("assigned_operator") Then
                    dt.Columns.Add("assigned_operator", GetType(String))
                    For Each row As DataRow In dt.Rows
                        row("assigned_operator") = "-"
                    Next
                End If

                dgvRequests.AutoGenerateColumns = False
                dgvRequests.DataSource = dt
            End If

        Catch ex As Exception
            Console.WriteLine("Request Search Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub RefreshRequestStats()
        Try
            ' Pending count
            readqueary("SELECT COUNT(*) FROM service_request WHERE UPPER(service_status)='PENDING'")
            If cmdread IsNot Nothing AndAlso cmdread.Read() Then
                lblAmountPendingRequests.Text = Val(cmdread(0)).ToString("00")
            End If
        Catch ex As Exception
            Console.WriteLine("Request Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try

        Try
            ' Approved count
            readqueary("SELECT COUNT(*) FROM service_request WHERE UPPER(service_status)='APPROVED'")
            If cmdread IsNot Nothing AndAlso cmdread.Read() Then
                If lblAmountApprovedRequests IsNot Nothing Then
                    lblAmountApprovedRequests.Text = Val(cmdread(0)).ToString("00")
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Approved Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try

        Try
            ' Rejected count
            readqueary("SELECT COUNT(*) FROM service_request WHERE UPPER(service_status)='REJECTED'")
            If cmdread IsNot Nothing AndAlso cmdread.Read() Then
                If lblAmountRejectedRequests IsNot Nothing Then
                    lblAmountRejectedRequests.Text = Val(cmdread(0)).ToString("00")
                End If
            End If
        Catch ex As Exception
            Console.WriteLine("Rejected Stats Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub BuildRequestStatusPanels()
        ' --- Total Approved Panel ---
        Dim pnlApproved As New Panel With {
            .BackColor = Color.White,
            .ForeColor = Color.Black,
            .Location = New Point(370, 200),
            .Size = New Size(337, 178),
            .Name = "pnlTotalApproved"
        }

        Dim lblApprovedTitle As New Label With {
            .AutoSize = True,
            .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold),
            .ForeColor = SystemColors.ControlDarkDark,
            .Location = New Point(15, 13),
            .Text = "Total Approved"
        }

        lblAmountApprovedRequests = New Label With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 48.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0)),
            .Location = New Point(15, 50),
            .Text = "00"
        }

        pnlApproved.Controls.Add(lblAmountApprovedRequests)
        pnlApproved.Controls.Add(lblApprovedTitle)
        pnlRequests.Controls.Add(pnlApproved)
        pnlApproved.BringToFront()

        ' --- Total Rejected Panel ---
        Dim pnlRejected As New Panel With {
            .BackColor = Color.White,
            .ForeColor = Color.Black,
            .Location = New Point(721, 200),
            .Size = New Size(337, 178),
            .Name = "pnlTotalRejected"
        }

        Dim lblRejectedTitle As New Label With {
            .AutoSize = True,
            .Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold),
            .ForeColor = SystemColors.ControlDarkDark,
            .Location = New Point(15, 13),
            .Text = "Total Rejected"
        }

        lblAmountRejectedRequests = New Label With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 48.0F, FontStyle.Bold, GraphicsUnit.Point, CByte(0)),
            .Location = New Point(15, 50),
            .Text = "00"
        }

        pnlRejected.Controls.Add(lblAmountRejectedRequests)
        pnlRejected.Controls.Add(lblRejectedTitle)
        pnlRequests.Controls.Add(pnlRejected)
        pnlRejected.BringToFront()

        ' --- Center the ComboBox dropdown column text ---
        DataGridViewTextBoxColumn6.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    ' When a ComboBox cell is edited, commit the change immediately so CellValueChanged fires
    Private Sub dgvRequests_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvRequests.CurrentCellDirtyStateChanged
        If dgvRequests.IsCurrentCellDirty AndAlso TypeOf dgvRequests.CurrentCell.OwningColumn Is DataGridViewComboBoxColumn Then
            dgvRequests.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    ' Track the old status value before the user changes it
    Private _oldStatusValue As String = ""

    Private Sub dgvRequests_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles dgvRequests.CellBeginEdit
        If e.RowIndex >= 0 AndAlso dgvRequests.Columns(e.ColumnIndex).Name = "DataGridViewTextBoxColumn6" Then
            Dim val = dgvRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            _oldStatusValue = If(val IsNot Nothing, val.ToString(), "")
        End If
    End Sub

    Private Sub dgvRequests_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellValueChanged
        If e.RowIndex < 0 Then Return
        If dgvRequests.Columns(e.ColumnIndex).Name <> "DataGridViewTextBoxColumn6" Then Return

        Dim newStatus As String = Convert.ToString(dgvRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)
        If newStatus = _oldStatusValue Then Return  ' No actual change

        ' Confirmation dialog
        Dim result = MessageBox.Show(
            "Are you sure you want to change the service status to """ & newStatus & """?",
            "Confirm Status Change",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Update the database
            Try
                Dim row = dgvRequests.Rows(e.RowIndex)
                Dim drv As DataRowView = CType(row.DataBoundItem, DataRowView)
                Dim farmerId As Integer = Convert.ToInt32(drv("farmer_id"))
                Dim serviceId As Integer = Convert.ToInt32(drv("service_id"))

                Dim requestId As Integer = 0
                If drv.Row.Table.Columns.Contains("request_id") AndAlso Not IsDBNull(drv("request_id")) Then
                    requestId = Convert.ToInt32(drv("request_id"))
                End If

                Dim connStr As String = "server=" & db_server & ";uid=" & db_uid & ";pwd=" & db_pwd & ";database=" & db_name
                Using conn As New MySql.Data.MySqlClient.MySqlConnection(connStr)
                    conn.Open()

                    ' 1. Update the service request status
                    Dim updateReqSql As String = If(requestId > 0,
                        "UPDATE service_request SET service_status=@status WHERE request_id=@rid",
                        "UPDATE service_request SET service_status=@status WHERE farmer_id=@fid AND service_id=@sid ORDER BY validation_date DESC LIMIT 1")

                    Using cmd As New MySql.Data.MySqlClient.MySqlCommand(updateReqSql, conn)
                        cmd.Parameters.AddWithValue("@status", newStatus)
                        If requestId > 0 Then
                            cmd.Parameters.AddWithValue("@rid", requestId)
                        Else
                            cmd.Parameters.AddWithValue("@fid", farmerId)
                            cmd.Parameters.AddWithValue("@sid", serviceId)
                        End If
                        cmd.ExecuteNonQuery()
                    End Using

                    ' 2. Update the machinery availability based on the new status
                    Dim newAvailability As String = If(newStatus.ToUpper() = "APPROVED", "NOT AVAILABLE", "AVAILABLE")
                    Using cmdMach As New MySql.Data.MySqlClient.MySqlCommand(
                        "UPDATE machinery SET availability_status=@avail " &
                        "WHERE machinery_id = (SELECT machinery_id FROM service WHERE service_id=@sid LIMIT 1)", conn)
                        cmdMach.Parameters.AddWithValue("@avail", newAvailability)
                        cmdMach.Parameters.AddWithValue("@sid", serviceId)
                        cmdMach.ExecuteNonQuery()
                    End Using

                    ' 3. Update the operator availability based on the new status
                    If ColumnExists("service_request", "operator_id") Then
                        Dim opAvail As String = If(newStatus.ToUpper() = "APPROVED", "CURRENTLY OPERATING MACHINE", "AVAILABLE")
                        Dim opSql As String = If(requestId > 0,
                            "UPDATE operator SET availability_status=@avail WHERE operator_id = (SELECT operator_id FROM service_request WHERE request_id=@rid LIMIT 1)",
                            "UPDATE operator SET availability_status=@avail WHERE operator_id = (SELECT operator_id FROM service_request WHERE farmer_id=@fid AND service_id=@sid ORDER BY validation_date DESC LIMIT 1)")

                        Using cmdOp As New MySqlCommand(opSql, conn)
                            cmdOp.Parameters.AddWithValue("@avail", opAvail)
                            If requestId > 0 Then
                                cmdOp.Parameters.AddWithValue("@rid", requestId)
                            Else
                                cmdOp.Parameters.AddWithValue("@fid", farmerId)
                                cmdOp.Parameters.AddWithValue("@sid", serviceId)
                            End If
                            cmdOp.ExecuteNonQuery()
                        End Using
                    End If
                End Using

                MessageBox.Show("Service status updated to """ & newStatus & """ successfully!" & vbCrLf &
                               "Machinery availability set to " & If(newStatus.ToUpper() = "APPROVED", """NOT AVAILABLE"".", """AVAILABLE""."),
                               "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                RefreshRequestStats()
                LoadMachineryCrudGrid()
                LoadOperatorGrid()

            Catch ex As Exception
                MessageBox.Show("Error updating status: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ' Revert the cell value on error
                dgvRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _oldStatusValue
            End Try
        Else
            ' User cancelled — revert to old value
            dgvRequests.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = _oldStatusValue
        End If
    End Sub

    Private Sub dgvRequests_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRequests.CellClick
        If e.RowIndex < 0 Then Return

        Dim colName As String = dgvRequests.Columns(e.ColumnIndex).Name
        If colName = "DataGridViewImageColumn1" Then
            EditRequestRow(e.RowIndex)
        ElseIf colName = "DataGridViewImageColumn2" Then
            DeleteRequestRow(e.RowIndex)
        End If
    End Sub

    Private Sub EditRequestRow(rowIndex As Integer)
        Try
            Dim row = dgvRequests.Rows(rowIndex)
            Dim currentDate As Date = Date.Today
            Date.TryParse(Convert.ToString(row.Cells(2).Value), currentDate)
            Dim currentFarmLocation As String = Convert.ToString(row.Cells(3).Value)
            Dim currentHectares As String = Convert.ToString(row.Cells(4).Value)
            Dim currentStatus As String = Convert.ToString(row.Cells(5).Value)

            Dim frm As New Form With {.Text = "Edit Request", .StartPosition = FormStartPosition.CenterParent, .FormBorderStyle = FormBorderStyle.FixedDialog, .ClientSize = New Size(420, 260), .MaximizeBox = False, .MinimizeBox = False}
            Dim lblDate As New Label With {.Text = "Request Date", .Location = New Point(20, 20), .AutoSize = True}
            Dim dtpDate As New DateTimePicker With {.Location = New Point(20, 40), .Width = 370, .Format = DateTimePickerFormat.Short, .Value = currentDate}
            Dim lblLoc As New Label With {.Text = "Farm Location", .Location = New Point(20, 80), .AutoSize = True}
            Dim txtLoc As New TextBox With {.Location = New Point(20, 100), .Width = 370, .Text = currentFarmLocation}
            Dim lblHec As New Label With {.Text = "Hectares Served", .Location = New Point(20, 135), .AutoSize = True}
            Dim txtHec As New TextBox With {.Location = New Point(20, 155), .Width = 170, .Text = currentHectares}
            Dim lblStat As New Label With {.Text = "Service Status", .Location = New Point(220, 135), .AutoSize = True}
            Dim cmbStat As New ComboBox With {.Location = New Point(220, 155), .Width = 170, .DropDownStyle = ComboBoxStyle.DropDownList}
            cmbStat.Items.AddRange(New Object() {"Pending", "Approved", "Rejected", "Done"})
            cmbStat.Text = currentStatus
            Dim btnSave As New Button With {.Text = "Save", .Location = New Point(310, 210), .Width = 80}
            Dim btnCancel As New Button With {.Text = "Cancel", .Location = New Point(220, 210), .Width = 80}
            frm.Controls.AddRange(New Control() {lblDate, dtpDate, lblLoc, txtLoc, lblHec, txtHec, lblStat, cmbStat, btnSave, btnCancel})
            AddHandler btnCancel.Click, Sub() frm.Close()

            AddHandler btnSave.Click,
                Sub()
                    Dim hectaresValue As Decimal
                    If Not Decimal.TryParse(txtHec.Text.Trim(), hectaresValue) Then
                        MessageBox.Show("Invalid hectares value.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If

                    Dim requestId As Integer = 0
                    If dgvRequests.Columns.Contains("request_id") AndAlso row.Cells("request_id").Value IsNot Nothing Then
                        requestId = Val(row.Cells("request_id").Value.ToString())
                    End If

                    Dim farmerId As Integer = ExtractNumericId(Convert.ToString(row.Cells(0).Value))
                    Dim serviceId As Integer = ExtractNumericId(Convert.ToString(row.Cells(1).Value))

                    If requestId > 0 Then
                        Dim sqlUpdate As String = "UPDATE service_request SET request_date=@request_date, farm_location=@farm_location, hectares_served=@hectares_served, service_status=@service_status WHERE request_id=@request_id"
                        Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                            localConn.Open()
                            Using c As New MySqlCommand(sqlUpdate, localConn)
                                c.Parameters.AddWithValue("@request_date", dtpDate.Value.Date)
                                c.Parameters.AddWithValue("@farm_location", txtLoc.Text.Trim().ToUpper())
                                c.Parameters.AddWithValue("@hectares_served", hectaresValue)
                                c.Parameters.AddWithValue("@service_status", cmbStat.Text)
                                c.Parameters.AddWithValue("@request_id", requestId)
                                c.ExecuteNonQuery()
                            End Using
                        End Using
                    Else
                        Dim oldDate As Date = currentDate
                        Dim sqlUpdate As String = "UPDATE service_request SET request_date=@request_date, farm_location=@farm_location, hectares_served=@hectares_served, service_status=@service_status WHERE farmer_id=@farmer_id AND service_id=@service_id AND request_date=@old_date ORDER BY validation_date DESC LIMIT 1"
                        Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                            localConn.Open()
                            Using c As New MySqlCommand(sqlUpdate, localConn)
                                c.Parameters.AddWithValue("@request_date", dtpDate.Value.Date)
                                c.Parameters.AddWithValue("@farm_location", txtLoc.Text.Trim().ToUpper())
                                c.Parameters.AddWithValue("@hectares_served", hectaresValue)
                                c.Parameters.AddWithValue("@service_status", cmbStat.Text)
                                c.Parameters.AddWithValue("@farmer_id", farmerId)
                                c.Parameters.AddWithValue("@service_id", serviceId)
                                c.Parameters.AddWithValue("@old_date", oldDate.Date)
                                c.ExecuteNonQuery()
                            End Using
                        End Using
                    End If

                    ' Update Machinery and Operator availability based on the new status
                    Dim newAvailability As String = If(cmbStat.Text.ToUpper() = "APPROVED", "NOT AVAILABLE", "AVAILABLE")
                    Dim opAvail As String = If(cmbStat.Text.ToUpper() = "APPROVED", "CURRENTLY OPERATING MACHINE", "AVAILABLE")

                    Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                        localConn.Open()
                        Using cmdMach As New MySqlCommand("UPDATE machinery SET availability_status=@avail WHERE machinery_id = (SELECT machinery_id FROM service WHERE service_id=@sid LIMIT 1)", localConn)
                            cmdMach.Parameters.AddWithValue("@avail", newAvailability)
                            cmdMach.Parameters.AddWithValue("@sid", serviceId)
                            cmdMach.ExecuteNonQuery()
                        End Using

                        If ColumnExists("service_request", "operator_id") Then
                            Dim opSql As String = If(requestId > 0,
                                "UPDATE operator SET availability_status=@avail WHERE operator_id = (SELECT operator_id FROM service_request WHERE request_id=@rid LIMIT 1)",
                                "UPDATE operator SET availability_status=@avail WHERE operator_id = (SELECT operator_id FROM service_request WHERE farmer_id=@fid AND service_id=@sid ORDER BY validation_date DESC LIMIT 1)")

                            Using cmdOp As New MySqlCommand(opSql, localConn)
                                cmdOp.Parameters.AddWithValue("@avail", opAvail)
                                If requestId > 0 Then
                                    cmdOp.Parameters.AddWithValue("@rid", requestId)
                                Else
                                    cmdOp.Parameters.AddWithValue("@fid", farmerId)
                                    cmdOp.Parameters.AddWithValue("@sid", serviceId)
                                End If
                                cmdOp.ExecuteNonQuery()
                            End Using
                        End If
                    End Using

                    frm.Close()
                    LoadRequestsGrid()
                    LoadMachineryCrudGrid()
                    LoadOperatorGrid()
                End Sub

            frm.ShowDialog(Me)
        Catch ex As Exception
            MessageBox.Show("Request edit failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DeleteRequestRow(rowIndex As Integer)
        Try
            If MessageBox.Show("Delete selected request?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Return

            Dim row = dgvRequests.Rows(rowIndex)
            Dim requestId As Integer = 0
            If dgvRequests.Columns.Contains("request_id") AndAlso row.Cells("request_id").Value IsNot Nothing Then
                requestId = Val(row.Cells("request_id").Value.ToString())
            End If

            Dim farmerId As Integer = ExtractNumericId(Convert.ToString(row.Cells(0).Value))
            Dim serviceId As Integer = ExtractNumericId(Convert.ToString(row.Cells(1).Value))

            Using localConn As New MySqlConnection("server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name)
                localConn.Open()

                ' Before deleting, reset the machinery linked to this service back to AVAILABLE
                Try
                    Using cmdMach As New MySqlCommand("UPDATE machinery SET availability_status='AVAILABLE' WHERE machinery_id = (SELECT machinery_id FROM service WHERE service_id=@sid LIMIT 1)", localConn)
                        cmdMach.Parameters.AddWithValue("@sid", serviceId)
                        cmdMach.ExecuteNonQuery()
                    End Using
                Catch
                End Try

                ' Before deleting, reset the assigned operator back to AVAILABLE
                If ColumnExists("service_request", "operator_id") Then
                    Try
                        Dim opSql As String = ""
                        If requestId > 0 Then
                            opSql = "UPDATE operator SET availability_status='AVAILABLE' WHERE operator_id = (SELECT operator_id FROM service_request WHERE request_id=@rid LIMIT 1)"
                        Else
                            opSql = "UPDATE operator SET availability_status='AVAILABLE' WHERE operator_id = (SELECT operator_id FROM service_request WHERE farmer_id=@fid AND service_id=@sid LIMIT 1)"
                        End If
                        Using cmdOp As New MySqlCommand(opSql, localConn)
                            If requestId > 0 Then
                                cmdOp.Parameters.AddWithValue("@rid", requestId)
                            Else
                                cmdOp.Parameters.AddWithValue("@fid", farmerId)
                                cmdOp.Parameters.AddWithValue("@sid", serviceId)
                            End If
                            cmdOp.ExecuteNonQuery()
                        End Using
                    Catch
                    End Try
                End If

                ' Now delete the request
                If requestId > 0 Then
                    Using c As New MySqlCommand("DELETE FROM service_request WHERE request_id=@request_id", localConn)
                        c.Parameters.AddWithValue("@request_id", requestId)
                        c.ExecuteNonQuery()
                    End Using
                Else
                    Dim reqDate As Date = Date.Today
                    Date.TryParse(Convert.ToString(row.Cells(2).Value), reqDate)
                    Using c As New MySqlCommand("DELETE FROM service_request WHERE farmer_id=@farmer_id AND service_id=@service_id AND request_date=@request_date ORDER BY validation_date DESC LIMIT 1", localConn)
                        c.Parameters.AddWithValue("@farmer_id", farmerId)
                        c.Parameters.AddWithValue("@service_id", serviceId)
                        c.Parameters.AddWithValue("@request_date", reqDate.Date)
                        c.ExecuteNonQuery()
                    End Using
                End If
            End Using

            LoadRequestsGrid()
            LoadMachineryCrudGrid()
            LoadOperatorGrid()
        Catch ex As Exception
            MessageBox.Show("Request delete failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ExtractNumericId(displayId As String) As Integer
        If String.IsNullOrWhiteSpace(displayId) Then Return 0
        Dim digits = New String(displayId.Where(Function(ch) Char.IsDigit(ch)).ToArray())
        Return Val(digits)
    End Function

    Private dgvReports As DataGridView

    Private Sub BuildReportsPanel()
        pnlReports.Controls.Clear()
        pnlReports.Name = "pnlReports"
        pnlReports.Size = New Size(1663, 1041)
        pnlReports.BackColor = Color.FromArgb(245, 245, 245)

        Dim lblDir As New Label With {.Text = "Reports", .Location = New Point(20, 25), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDark}
        Dim lblHeader As New Label With {.Text = "Reports Management", .Location = New Point(15, 70), .AutoSize = True, .Font = New Font("Segoe UI", 30, FontStyle.Bold), .ForeColor = Color.Black}
        Dim lblSubtitle As New Label With {.Text = "Export table data to Excel-compatible CSV", .Location = New Point(20, 125), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}

        Dim lblTable As New Label With {.Text = "Select Table", .Location = New Point(20, 210), .AutoSize = True, .Font = New Font("Segoe UI", 11, FontStyle.Bold)}
        cmbReportTable = New ComboBox With {.Location = New Point(20, 235), .Width = 360, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbReportTable.Items.AddRange(New Object() {"farmer", "service", "service_request", "machinery", "operator", "employee", "station"})

        Dim btnExport As New Button With {.Text = "Export to Excel (CSV)", .Location = New Point(390, 235), .Size = New Size(220, 37), .BackColor = Color.DarkGreen, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        btnExport.FlatAppearance.BorderSize = 0
        AddHandler btnExport.Click, AddressOf ExportSelectedReportTable

        Dim pnlGrid As New Panel With {
            .BackColor = Color.White,
            .Location = New Point(20, 290),
            .Size = New Size(1596, 731),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        }
        dgvReports = New DataGridView With {
            .Dock = DockStyle.Fill,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
            .GridColor = Color.LightGray
        }
        ApplyFarmersLikeGridStyle(dgvReports)
        pnlGrid.Controls.Add(dgvReports)

        AddHandler cmbReportTable.SelectedIndexChanged, AddressOf LoadReportTableData

        pnlReports.Controls.AddRange(New Control() {lblDir, lblHeader, lblSubtitle, lblTable, cmbReportTable, btnExport, pnlGrid})

        cmbReportTable.SelectedIndex = 0
    End Sub

    Private Sub LoadReportTableData(sender As Object, e As EventArgs)
        If cmbReportTable.SelectedItem Is Nothing Then Return
        Dim tableName As String = cmbReportTable.SelectedItem.ToString()

        Try
            Dim dt As New DataTable()
            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
            Using localConn As New MySql.Data.MySqlClient.MySqlConnection(strconn)
                localConn.Open()
                Using cmd As New MySql.Data.MySqlClient.MySqlCommand($"SELECT * FROM `{tableName}`", localConn)
                    dt.Load(cmd.ExecuteReader())
                End Using
            End Using

            dgvReports.DataSource = dt
        Catch ex As Exception
            MessageBox.Show("Failed to load table data: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- FLEXIBLE SEARCH ---
    Private txtFlexSearch As TextBox
    Private dgvFlexSearch As DataGridView

    Private Sub BuildFlexibleSearchPanel()
        pnlFlexibleSearch.Controls.Clear()
        pnlFlexibleSearch.Name = "pnlFlexibleSearch"
        pnlFlexibleSearch.Size = New Size(1663, 1041)
        pnlFlexibleSearch.BackColor = Color.FromArgb(245, 245, 245)

        Dim lblDir As New Label With {.Text = "Flexible Search", .Location = New Point(20, 25), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDark}
        Dim lblHeader As New Label With {.Text = "Flexible Search", .Location = New Point(15, 70), .AutoSize = True, .Font = New Font("Segoe UI", 30, FontStyle.Bold), .ForeColor = Color.Black}
        Dim lblSubtitle As New Label With {.Text = "Global real-time search across all system records", .Location = New Point(20, 125), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}

        Dim lblSearch As New Label With {.Text = "Search", .Location = New Point(20, 210), .AutoSize = True, .Font = New Font("Segoe UI", 11, FontStyle.Bold)}
        txtFlexSearch = New TextBox With {.Location = New Point(20, 235), .Width = 620, .Font = New Font("Segoe UI", 11, FontStyle.Regular)}

        AddHandler txtFlexSearch.TextChanged, AddressOf ExecuteFlexibleSearch

        Dim pnlGrid As New Panel With {
            .BackColor = Color.White,
            .Location = New Point(20, 290),
            .Size = New Size(1596, 731),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        }
        dgvFlexSearch = New DataGridView With {
            .Dock = DockStyle.Fill,
            .AutoGenerateColumns = True,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = True,
            .RowHeadersVisible = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .BackgroundColor = Color.White,
            .BorderStyle = BorderStyle.None,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        ApplyFarmersLikeGridStyle(dgvFlexSearch)
        pnlGrid.Controls.Add(dgvFlexSearch)

        pnlFlexibleSearch.Controls.AddRange(New Control() {lblDir, lblHeader, lblSubtitle, lblSearch, txtFlexSearch, pnlGrid})

        ' Trigger initial load
        ExecuteFlexibleSearch(Nothing, Nothing)
    End Sub

    Private Sub ExecuteFlexibleSearch(sender As Object, e As EventArgs)
        Dim term As String = txtFlexSearch.Text.Trim()

        If term = "" Then
            dgvFlexSearch.DataSource = Nothing
            Return
        End If

        Try
            ' Define the queries for each table to match their respective tab's formatting
            Dim queries As New List(Of String) From {
                $"SELECT CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS 'Farmer ID', full_name AS 'Full Name', residence_address AS 'Address', contact_number AS 'Contact Number', classification AS 'Classification', registration_status AS 'Status' FROM farmer WHERE CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) LIKE '%{term}%' OR full_name LIKE '%{term}%' OR residence_address LIKE '%{term}%' OR contact_number LIKE '%{term}%' OR classification LIKE '%{term}%' OR registration_status LIKE '%{term}%'",
                $"SELECT CONCAT('SRV-', LPAD(s.service_id, 4, '0')) AS 'Service ID', IFNULL(m.machinery_name, 'N/A') AS 'Machinery Name', s.service_name AS 'Service Name', s.service_type AS 'Service Type', s.description AS 'Description', s.policy_limit AS 'Policy Limit', IFNULL(e.full_name, 'N/A') AS 'Employee Name' FROM service s LEFT JOIN machinery m ON s.machinery_id = m.machinery_id LEFT JOIN employee e ON s.employee_id = e.employee_id WHERE CONCAT('SRV-', LPAD(s.service_id, 4, '0')) LIKE '%{term}%' OR s.service_name LIKE '%{term}%' OR s.service_type LIKE '%{term}%' OR IFNULL(m.machinery_name, '') LIKE '%{term}%' OR IFNULL(e.full_name, '') LIKE '%{term}%'",
                $"SELECT CONCAT('SRV-', LPAD(service_id, 4, '0')) AS 'Service ID', CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS 'Farmer ID', request_date AS 'Request Date', farm_location AS 'Farm Location', hectares_served AS 'Hectares', service_status AS 'Status' FROM service_request WHERE CONCAT('SRV-', LPAD(service_id, 4, '0')) LIKE '%{term}%' OR CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) LIKE '%{term}%' OR farm_location LIKE '%{term}%' OR service_status LIKE '%{term}%' OR request_date LIKE '%{term}%'",
                $"SELECT CONCAT('MCH-', LPAD(machinery_id, 4, '0')) AS 'Machinery ID', machinery_name AS 'Machinery Name', machinery_type AS 'Machinery Type', `condition` AS 'Condition', availability_status AS 'Availability' FROM machinery WHERE CONCAT('MCH-', LPAD(machinery_id, 4, '0')) LIKE '%{term}%' OR machinery_name LIKE '%{term}%' OR machinery_type LIKE '%{term}%' OR `condition` LIKE '%{term}%' OR availability_status LIKE '%{term}%'",
                $"SELECT CONCAT('OPR-', LPAD(operator_id, 4, '0')) AS 'Operator ID', full_name AS 'Operator Name', position AS 'Position', contact_number AS 'Contact Number' FROM operator WHERE CONCAT('OPR-', LPAD(operator_id, 4, '0')) LIKE '%{term}%' OR full_name LIKE '%{term}%' OR position LIKE '%{term}%' OR contact_number LIKE '%{term}%'",
                $"SELECT CONCAT('EMP-', LPAD(employee_id, 4, '0')) AS 'Employee ID', full_name AS 'Employee Name', email_address AS 'Email', position AS 'Position', offiice_assignment AS 'Office Assignment', contact_number AS 'Contact Number' FROM employee WHERE CONCAT('EMP-', LPAD(employee_id, 4, '0')) LIKE '%{term}%' OR full_name LIKE '%{term}%' OR email_address LIKE '%{term}%' OR position LIKE '%{term}%' OR offiice_assignment LIKE '%{term}%' OR contact_number LIKE '%{term}%'",
                $"SELECT CONCAT('STN-', LPAD(station_id, 4, '0')) AS 'Station ID', station_name AS 'Station Name', location AS 'Location', description AS 'Description' FROM station WHERE CONCAT('STN-', LPAD(station_id, 4, '0')) LIKE '%{term}%' OR station_name LIKE '%{term}%' OR location LIKE '%{term}%' OR description LIKE '%{term}%'"
            }

            ' Iterate through queries. The first one that returns results will morph the table!
            For Each sql In queries
                readqueary(sql)
                If cmdread IsNot Nothing Then
                    Dim dt As New DataTable
                    dt.Load(cmdread)
                    cmdread.Close()

                    If dt.Rows.Count > 0 Then
                        dgvFlexSearch.DataSource = dt
                        Return ' Stop searching, we found a match!
                    End If
                End If
            Next

            ' If we get here, no matches were found in any table
            dgvFlexSearch.DataSource = Nothing

        Catch ex As Exception
            MessageBox.Show("Error performing search: " & ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub ExportSelectedReportTable(sender As Object, e As EventArgs)
        If cmbReportTable Is Nothing OrElse cmbReportTable.SelectedItem Is Nothing Then Return
        ExportTableToExcelWithTemplate(cmbReportTable.SelectedItem.ToString())
    End Sub

    Private Function GetTemplatePath(tableName As String) As String
        Dim baseDir As String = IO.Path.Combine(Application.StartupPath, "Report Templates")
        Select Case tableName.ToLower()
            Case "farmer" : Return IO.Path.Combine(baseDir, "01_Farmer_Report.xlsx")
            Case "service" : Return IO.Path.Combine(baseDir, "02_Service_Report.xlsx")
            Case "service_request" : Return IO.Path.Combine(baseDir, "03_Requests_Report.xlsx")
            Case "machinery" : Return IO.Path.Combine(baseDir, "04_Machinery_Report.xlsx")
            Case "operator" : Return IO.Path.Combine(baseDir, "05_Operator_Report.xlsx")
            Case "employee" : Return IO.Path.Combine(baseDir, "06_Employee_Report.xlsx")
            Case "station" : Return IO.Path.Combine(baseDir, "07_Station_Report.xlsx")
            Case Else : Return ""
        End Select
    End Function

    Private Sub ExportTableToExcelWithTemplate(tableName As String)
        Try
            Dim templatePath As String = GetTemplatePath(tableName)
            If Not IO.File.Exists(templatePath) Then
                MessageBox.Show("Template file not found at: " & templatePath & vbCrLf &
                            "Please ensure the 'Report Templates' folder exists and contains the required .xlsx files.",
                            "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim sfd As New SaveFileDialog With {
            .Filter = "Excel files (*.xlsx)|*.xlsx",
            .FileName = tableName & "_Report_" & Date.Now.ToString("yyyyMMdd_HHmmss") & ".xlsx"
        }
            If sfd.ShowDialog() <> DialogResult.OK Then Return

            ' Set EPPlus License Context
            OfficeOpenXml.ExcelPackage.License.SetNonCommercialOrganization("PABEO")

            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd &
                                ";database=" & db_name &
                                ";Allow Zero Datetime=True;Convert Zero Datetime=False;"

            Using localConn As New MySqlConnection(strconn)
                localConn.Open()

                ' ── Build a clean, display-friendly SQL query per table ──────
                Dim exportSql As String = BuildExportQuery(tableName)

                Using cmdExport As New MySqlCommand(exportSql, localConn)
                    Using reader As MySqlDataReader = cmdExport.ExecuteReader()
                        Dim dt As New DataTable()
                        dt.Load(reader)

                        Using package As New OfficeOpenXml.ExcelPackage(New IO.FileInfo(templatePath))
                            Dim ws = package.Workbook.Worksheets(0)

                            ' ── Fill Date Generated (left half of row 6) ─────
                            ws.Cells("A6").Value = "  📅  Date Generated:  " &
                                               Date.Now.ToString("MMMM dd, yyyy")

                            ' ── Fill Total Records (right half of row 6) ─────
                            Dim midCol As String = GetColumnLetter((dt.Columns.Count \ 2) + 1)
                            ws.Cells(midCol & "6").Value = "📊  Total Records:  " &
                                                       dt.Rows.Count.ToString()

                            ' ── Write data rows starting at row 8 ────────────
                            ' Rows 1-7 are the template header — DO NOT touch them
                            Dim startRow As Integer = 8

                            For rowIdx As Integer = 0 To dt.Rows.Count - 1
                                Dim excelRow As Integer = startRow + rowIdx
                                Dim dr As DataRow = dt.Rows(rowIdx)

                                For colIdx As Integer = 0 To dt.Columns.Count - 1
                                    Dim cellValue As Object = dr(colIdx)

                                    ' ── Clean up ugly zero-dates from MySQL ──
                                    If cellValue IsNot Nothing AndAlso Not IsDBNull(cellValue) Then
                                        Dim strVal As String = cellValue.ToString()
                                        If strVal = "0001-01-01 00:00:00" OrElse
                                       strVal = "0001-01-01" OrElse
                                       strVal = "1/1/0001 12:00:00 AM" Then
                                            cellValue = ""
                                        End If
                                    Else
                                        cellValue = ""
                                    End If

                                    ws.Cells(excelRow, colIdx + 1).Value = cellValue
                                Next
                            Next

                            ' ── DO NOT call AutoFitColumns ────────────────────
                            ' It would destroy the template's carefully set column widths

                            package.SaveAs(New IO.FileInfo(sfd.FileName))
                        End Using
                    End Using
                End Using
            End Using

            MessageBox.Show("Export successful!", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Process.Start(New ProcessStartInfo(sfd.FileName) With {.UseShellExecute = True})

        Catch ex As Exception
            MessageBox.Show("Export failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ── Builds a clean display query per table (mirrors what the grids show) ─────
    Private Function BuildExportQuery(tableName As String) As String
        Select Case tableName.ToLower()
            Case "farmer"
                Return "SELECT " &
                   "CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')) AS 'Farmer ID', " &
                   "full_name AS 'Full Name', " &
                   "residence_address AS 'Address', " &
                   "contact_number AS 'Contact Number', " &
                   "classification AS 'Classification', " &
                   "registration_status AS 'Status' " &
                   "FROM farmer ORDER BY farmer_id ASC"

            Case "service"
                Return "SELECT " &
                   "CONCAT('SRV-', LPAD(s.service_id, 4, '0')) AS 'Service ID', " &
                   "IFNULL(m.machinery_name, 'N/A') AS 'Machinery Name', " &
                   "s.service_name AS 'Service Name', " &
                   "s.service_type AS 'Service Type', " &
                   "s.description AS 'Description', " &
                   "s.policy_limit AS 'Policy Limit', " &
                   "IFNULL(e.full_name, 'N/A') AS 'Employee Name' " &
                   "FROM service s " &
                   "LEFT JOIN machinery m ON s.machinery_id = m.machinery_id " &
                   "LEFT JOIN employee e ON s.employee_id = e.employee_id " &
                   "ORDER BY s.service_id ASC"

            Case "service_request"
                Return "SELECT " &
                   "CONCAT('RSBSA-', LPAD(sr.farmer_id, 4, '0')) AS 'Farmer ID', " &
                   "CONCAT('SRV-', LPAD(sr.service_id, 4, '0')) AS 'Service ID', " &
                   "sr.request_date AS 'Request Date', " &
                   "sr.farm_location AS 'Farm Location', " &
                   "sr.hectares_served AS 'Hectare Served', " &
                   "sr.service_status AS 'Service Status', " &
                   "IFNULL(o.full_name, '-') AS 'Assigned Operator' " &
                   "FROM service_request sr " &
                   "LEFT JOIN operator o ON sr.operator_id = o.operator_id " &
                   "ORDER BY sr.request_date DESC"

            Case "machinery"
                Return "SELECT " &
                   "CONCAT('MCH-', LPAD(machinery_id, 4, '0')) AS 'Machinery ID', " &
                   "machinery_name AS 'Machinery Name', " &
                   "machinery_type AS 'Machinery Type', " &
                   "CONCAT('STN-', LPAD(station_id, 4, '0')) AS 'Station ID', " &
                   "`condition` AS 'Condition', " &
                   "availability_status AS 'Availability' " &
                   "FROM machinery ORDER BY machinery_id ASC"

            Case "operator"
                Return "SELECT " &
                   "CONCAT('OPR-', LPAD(operator_id, 4, '0')) AS 'Operator ID', " &
                   "full_name AS 'Full Name', " &
                   "position AS 'Position', " &
                   "contact_number AS 'Contact Number', " &
                   "CONCAT('STN-', LPAD(station_id, 4, '0')) AS 'Station ID', " &
                   "availability_status AS 'Availability' " &
                   "FROM operator ORDER BY operator_id ASC"

            Case "employee"
                Return "SELECT " &
                   "CONCAT('EMP-', LPAD(employee_id, 4, '0')) AS 'Employee ID', " &
                   "full_name AS 'Full Name', " &
                   "IFNULL(email_address, '') AS 'Email Address', " &
                   "position AS 'Position', " &
                   "contact_number AS 'Contact Number', " &
                   "IFNULL(offiice_assignment, '') AS 'Office' " &
                   "FROM employee ORDER BY employee_id ASC"

            Case "station"
                Return "SELECT " &
                   "CONCAT('STN-', LPAD(station_id, 4, '0')) AS 'Station ID', " &
                   "station_name AS 'Station Name', " &
                   "location AS 'Location', " &
                   "description AS 'Description' " &
                   "FROM station ORDER BY station_id ASC"

            Case Else
                ' Fallback: raw table dump (no formatting)
                Return "SELECT * FROM `" & tableName & "`"
        End Select
    End Function

    ' ── Converts a column number to Excel letter (1=A, 2=B, 27=AA, etc.) ────────
    Private Function GetColumnLetter(colNumber As Integer) As String
        Dim result As String = ""
        While colNumber > 0
            Dim remainder As Integer = (colNumber - 1) Mod 26
            result = Chr(65 + remainder) & result
            colNumber = (colNumber - remainder - 1) \ 26
        End While
        Return result
    End Function

    Private Sub ExportTableToCsv(tableName As String)
        Try
            Dim allowedTables As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {
                "farmer", "service", "service_request", "machinery", "operator", "employee", "station"
            }
            If Not allowedTables.Contains(tableName) Then
                MessageBox.Show("Invalid table selected for export.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim sfd As New SaveFileDialog With {
                .Filter = "CSV files (*.csv)|*.csv",
                .FileName = tableName & "_" & Date.Now.ToString("yyyyMMdd_HHmmss") & ".csv"
            }

            If sfd.ShowDialog() <> DialogResult.OK Then Return

            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name & ";Allow Zero Datetime=True;Convert Zero Datetime=False;"
            Using localConn As New MySqlConnection(strconn)
                localConn.Open()
                Using cmdExport As New MySqlCommand("SELECT * FROM `" & tableName & "`", localConn)
                    Using reader As MySqlDataReader = cmdExport.ExecuteReader()
                        Using sw As New IO.StreamWriter(sfd.FileName, False, System.Text.Encoding.UTF8)
                            ' Header
                            Dim headers As New List(Of String)
                            For i As Integer = 0 To reader.FieldCount - 1
                                headers.Add("""" & reader.GetName(i).Replace("""", """""") & """")
                            Next
                            sw.WriteLine(String.Join(",", headers))

                            ' Rows
                            While reader.Read()
                                Dim rowValues As New List(Of String)
                                For i As Integer = 0 To reader.FieldCount - 1
                                    Dim textValue As String = ""
                                    If Not reader.IsDBNull(i) Then
                                        Dim raw = reader.GetValue(i)
                                        ' Keep zero-date values as literal text from provider.
                                        textValue = Convert.ToString(raw)
                                        If textValue = "0001-01-01 00:00:00" OrElse textValue = "0001-01-01" Then
                                            textValue = ""
                                        End If
                                    End If
                                    rowValues.Add("""" & textValue.Replace("""", """""") & """")
                                Next
                                sw.WriteLine(String.Join(",", rowValues))
                            End While
                        End Using
                    End Using
                End Using
            End Using

            MessageBox.Show("Export successful.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("Export failed: " & ex.Message, "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFarmerExport_Click(sender As Object, e As EventArgs)
        ExportTableToCsv("farmer")
    End Sub

    Private Sub btExportServiceReport_Click(sender As Object, e As EventArgs) Handles btExportServiceReport.Click
        ExportTableToExcelWithTemplate("service")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ExportTableToExcelWithTemplate("service_request")
    End Sub


    Private Sub BuildManagementPanelUI(targetPanel As Panel, headerText As String, subtitleText As String, entity As String)
        targetPanel.Controls.Clear()
        targetPanel.BackColor = Color.FromArgb(245, 245, 245)

        ' Directory and Headers
        Dim lblDir As New Label With {.Text = headerText.Replace(" Management", ""), .Location = New Point(20, 25), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDark}
        Dim lblHeader As New Label With {.Text = headerText, .Location = New Point(15, 70), .AutoSize = True, .Font = New Font("Segoe UI", 30, FontStyle.Bold), .ForeColor = Color.Black}
        Dim lblSubtitle As New Label With {.Text = subtitleText, .Location = New Point(20, 125), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}

        ' Stats Card
        Dim pnlStats As New Panel With {.BackColor = Color.White, .Location = New Point(20, 190), .Size = New Size(337, 140)}
        Dim lblStatsTitle As New Label With {.Text = "Total Records", .Location = New Point(15, 13), .AutoSize = True, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .ForeColor = SystemColors.ControlDarkDark}
        Dim lblStatsValue As New Label With {.Name = $"lbl{entity}TotalDynamic", .Text = "00", .Location = New Point(15, 40), .AutoSize = True, .Font = New Font("Segoe UI", 42, FontStyle.Bold), .ForeColor = Color.Black}
        pnlStats.Controls.Add(lblStatsTitle)
        pnlStats.Controls.Add(lblStatsValue)

        ' --- Search Bar Panel with icSearch Icon ---
        Dim pnlSearch As New Panel With {.BackColor = Color.White, .Location = New Point(20, 360), .Size = New Size(568, 37)}

        ' PictureBox for Search Icon
        Dim picSearchIcon As New PictureBox With {
            .Image = My.Resources.icSearch,
            .SizeMode = PictureBoxSizeMode.Zoom,
            .Size = New Size(25, 25),
            .Location = New Point(10, 6),
            .BackColor = Color.Transparent
        }

        ' TextBox shifted to the right to accommodate icon
        Dim txtSearch As New TextBox With {
            .BorderStyle = BorderStyle.None,
            .Font = New Font("Segoe UI", 15.75F, FontStyle.Regular),
            .Location = New Point(45, 5),
            .Size = New Size(510, 28)
        }

        pnlSearch.Controls.Add(picSearchIcon)
        pnlSearch.Controls.Add(txtSearch)

        ' Main Add Button
        Dim btnAdd As New Button With {.Text = $"+Add {entity.Substring(0, 1).ToUpper() & entity.Substring(1)}", .BackColor = Color.DarkGreen, .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold), .Location = New Point(1473, 360), .Size = New Size(142, 37)}
        btnAdd.FlatAppearance.BorderSize = 0

        ' DataGridView Container
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

        ' Logic for Top Action Buttons (Edit/Delete) - Active for Machinery, Operator, and Employee
        If entity = "machinery" OrElse entity = "operator" OrElse entity = "employee" Then
            Dim btnEditTop As New Button With {
                .Text = "Edit",
                .BackColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold),
                .Location = New Point(1320, 360),
                .Size = New Size(147, 37)
            }
            btnEditTop.FlatAppearance.BorderColor = Color.Silver

            Dim btnDeleteTop As New Button With {
                .Text = "Delete",
                .BackColor = Color.White,
                .FlatStyle = FlatStyle.Flat,
                .Font = New Font("Segoe UI Semibold", 12, FontStyle.Bold),
                .Location = New Point(1167, 360),
                .Size = New Size(147, 37)
            }
            btnDeleteTop.FlatAppearance.BorderColor = Color.Silver

            targetPanel.Controls.Add(btnEditTop)
            targetPanel.Controls.Add(btnDeleteTop)

            If entity = "machinery" Then
                AddHandler btnEditTop.Click, Sub()
                                                 If dgvMachineryUi.CurrentRow IsNot Nothing Then
                                                     MachineryGrid_Edit(Nothing, New DataGridViewCellEventArgs(0, dgvMachineryUi.CurrentRow.Index))
                                                 Else
                                                     MessageBox.Show("Please select a machinery record to edit.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 End If
                                             End Sub

                AddHandler btnDeleteTop.Click, Sub()
                                                   If dgvMachineryUi.CurrentRow IsNot Nothing Then
                                                       MachineryGrid_DeleteKey(Nothing, New KeyEventArgs(Keys.Delete))
                                                   Else
                                                       MessageBox.Show("Please select a machinery record to delete.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                   End If
                                               End Sub
            ElseIf entity = "operator" Then
                AddHandler btnEditTop.Click, Sub()
                                                 If dgvOperatorUi IsNot Nothing AndAlso dgvOperatorUi.CurrentRow IsNot Nothing Then
                                                     OperatorGrid_Edit(Nothing, New DataGridViewCellEventArgs(0, dgvOperatorUi.CurrentRow.Index))
                                                 Else
                                                     MessageBox.Show("Please select an operator to edit.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 End If
                                             End Sub

                AddHandler btnDeleteTop.Click, Sub()
                                                   If dgvOperatorUi IsNot Nothing AndAlso dgvOperatorUi.CurrentRow IsNot Nothing Then
                                                       OperatorGrid_DeleteKey(Nothing, New KeyEventArgs(Keys.Delete))
                                                   Else
                                                       MessageBox.Show("Please select an operator to delete.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                   End If
                                               End Sub
            ElseIf entity = "employee" Then
                AddHandler btnEditTop.Click, Sub()
                                                 If dgvEmployeeUi IsNot Nothing AndAlso dgvEmployeeUi.CurrentRow IsNot Nothing Then
                                                     EmployeeGrid_Edit(Nothing, New DataGridViewCellEventArgs(0, dgvEmployeeUi.CurrentRow.Index))
                                                 Else
                                                     MessageBox.Show("Please select an employee to edit.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                 End If
                                             End Sub

                AddHandler btnDeleteTop.Click, Sub()
                                                   If dgvEmployeeUi IsNot Nothing AndAlso dgvEmployeeUi.CurrentRow IsNot Nothing Then
                                                       EmployeeGrid_DeleteKey(Nothing, New KeyEventArgs(Keys.Delete))
                                                   Else
                                                       MessageBox.Show("Please select an employee to delete.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                                   End If
                                               End Sub
            End If
        End If

        ' Entity Mapping and Event Handlers
        Select Case entity
            Case "machinery"
                dgvMachineryUi = dgv
                txtMachinerySearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddMachineryRecord
                AddHandler txtSearch.TextChanged, Sub() LoadMachineryCrudGrid(txtSearch.Text.Trim())
                AddHandler dgv.KeyDown, AddressOf MachineryGrid_DeleteKey
            Case "operator"
                dgvOperatorUi = dgv
                txtOperatorSearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddOperatorRecord
                AddHandler txtSearch.TextChanged, Sub() LoadOperatorGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf OperatorGrid_Edit
                ' CellClick handler removed — Edit/Delete now via top buttons
                AddHandler dgv.KeyDown, AddressOf OperatorGrid_DeleteKey
            Case "employee"
                dgvEmployeeUi = dgv
                txtEmployeeSearch = txtSearch
                AddHandler btnAdd.Click, AddressOf AddEmployeeRecord
                AddHandler txtSearch.TextChanged, Sub() LoadEmployeeGrid(txtSearch.Text.Trim())
                AddHandler dgv.CellDoubleClick, AddressOf EmployeeGrid_Edit
                ' CellClick handler removed — Edit/Delete now via top buttons
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
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 235, 235)
        dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 220, 220) ' Slightly darker than header for contrast
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black
        dgv.AlternatingRowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.GridColor = Color.White
        dgv.RowTemplate.Height = 40
    End Sub

    Private Sub ConfigureCrudGridColumns(dgv As DataGridView, entity As String)
        dgv.Columns.Clear()
        ApplyFarmersLikeGridStyle(dgv)

        Select Case entity
' Inside ConfigureCrudGridColumns sub:
            Case "machinery"
                ' Give the ID slightly more space for the "MCH-" prefix
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "formatted_mach_id",
                    .Name = "formatted_mach_id",
                    .HeaderText = "Machinery ID",
                    .Width = 250
                })

                ' Give Machinery Name the most space
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "machinery_name",
                    .Name = "machinery_name",
                    .HeaderText = "Machinery Name",
                    .Width = 400
                })

                ' Moderate width for Type and Condition
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "machinery_type",
                    .Name = "machinery_type",
                    .HeaderText = "Machinery Type",
                    .Width = 200
                })

                ' Station ID
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "formatted_station_id",
                    .Name = "formatted_station_id",
                    .HeaderText = "Station ID",
                    .Width = 175
                })

                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "condition",
                    .Name = "condition",
                    .HeaderText = "Condition",
                    .Width = 150
                })

                ' Availability needs enough room for "NOT AVAILABLE"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "availability_status",
                    .Name = "availability_status",
                    .HeaderText = "Availability",
                    .Width = 200
                })

                ' Hidden original ID for backend logic
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "machinery_id",
                    .Name = "machinery_id",
                    .Visible = False
                })
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {
                    .DataPropertyName = "station_id",
                    .Name = "station_id",
                    .Visible = False
                })
            Case "operator"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "formatted_operator_id", .Name = "formatted_operator_id", .HeaderText = "Operator ID", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "full_name", .Name = "full_name", .HeaderText = "Full Name", .Width = 320})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "position", .Name = "position", .HeaderText = "Position", .Width = 250})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "contact_number", .Name = "contact_number", .HeaderText = "Contact Number", .Width = 180})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "formatted_station_id", .Name = "formatted_station_id", .HeaderText = "Station ID", .Width = 120})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "availability_status", .Name = "availability_status", .HeaderText = "Availability", .Width = 200})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "operator_id", .Name = "operator_id", .Visible = False})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_id", .Name = "station_id", .Visible = False})
            Case "employee"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "formatted_employee_id", .Name = "formatted_employee_id", .HeaderText = "Employee ID", .Width = 130})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "full_name", .Name = "full_name", .HeaderText = "Full Name", .Width = 260})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "email_address", .Name = "email_address", .HeaderText = "Email Address", .Width = 270})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "position", .Name = "position", .HeaderText = "Position", .Width = 150})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "contact_number", .Name = "contact_number", .HeaderText = "Contact", .Width = 120})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "offiice_assignment", .Name = "offiice_assignment", .HeaderText = "Office", .Width = 150})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "password", .Name = "password", .Visible = False})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "employee_id", .Name = "employee_id", .Visible = False})
            Case "station"
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "formatted_station_id", .Name = "formatted_station_id", .HeaderText = "Station ID", .Width = 140})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_name", .Name = "station_name", .HeaderText = "Station Name", .Width = 300})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "location", .Name = "location", .HeaderText = "Location", .Width = 400})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "description", .Name = "description", .HeaderText = "Description", .Width = 300})
                dgv.Columns.Add(New DataGridViewTextBoxColumn With {.DataPropertyName = "station_id", .Name = "station_id", .Visible = False})
        End Select

        If entity <> "machinery" AndAlso entity <> "operator" AndAlso entity <> "employee" AndAlso entity <> "station" Then
            dgv.Columns.Add(New DataGridViewImageColumn With {.Name = "ActionEdit", .HeaderText = "Edit", .Image = FarmerEdit.Image, .Width = 75})
            dgv.Columns.Add(New DataGridViewImageColumn With {.Name = "ActionDelete", .HeaderText = "Delete", .Image = FarmerDelete.Image, .Width = 75})
        End If
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
            ' Update the SQL to include the CONCAT/LPAD logic
            Dim sql As String = "SELECT machinery_id, " &
                            "CONCAT('MCH-', LPAD(machinery_id, 4, '0')) AS formatted_mach_id, " &
                            "machinery_name, machinery_type, station_id, CONCAT('STN-', LPAD(station_id, 4, '0')) AS formatted_station_id, `condition`, availability_status FROM machinery"

            If filter <> "" Then
                ' Update filter to also allow searching by the new MCH- format
                sql &= " WHERE CONCAT('MCH-', LPAD(machinery_id, 4, '0')) LIKE '%" & filter & "%' OR " &
                   "CONCAT('STN-', LPAD(station_id, 4, '0')) LIKE '%" & filter & "%' OR " &
                   "machinery_name LIKE '%" & filter & "%' OR machinery_type LIKE '%" & filter & "%' OR availability_status LIKE '%" & filter & "%'"
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
            Dim sql As String = "SELECT operator_id, CONCAT('OPR-', LPAD(operator_id, 4, '0')) AS formatted_operator_id, full_name, position, contact_number, station_id, CONCAT('STN-', LPAD(station_id, 4, '0')) AS formatted_station_id, availability_status FROM operator"
            If filter <> "" Then
                sql &= " WHERE full_name LIKE '%" & filter & "%' OR position LIKE '%" & filter & "%' OR contact_number LIKE '%" & filter & "%' OR CONCAT('OPR-', LPAD(operator_id, 4, '0')) LIKE '%" & filter & "%' OR CONCAT('STN-', LPAD(station_id, 4, '0')) LIKE '%" & filter & "%' OR availability_status LIKE '%" & filter & "%'"
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
            Dim sql As String = "SELECT employee_id, CONCAT('EMP-', LPAD(employee_id, 4, '0')) AS formatted_employee_id, full_name, IFNULL(email_address, '') AS email_address, IFNULL(password, '') AS password, position, contact_number, offiice_assignment FROM employee"
            If filter <> "" Then
                sql &= " WHERE full_name LIKE '%" & filter & "%' OR email_address LIKE '%" & filter & "%' OR position LIKE '%" & filter & "%' OR contact_number LIKE '%" & filter & "%' OR offiice_assignment LIKE '%" & filter & "%' OR CONCAT('EMP-', LPAD(employee_id, 4, '0')) LIKE '%" & filter & "%'"
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
            Dim sql As String = "SELECT station_id, CONCAT('STN-', LPAD(station_id, 4, '0')) AS formatted_station_id, station_name, location, description FROM station"
            If filter <> "" Then
                sql &= " WHERE station_name LIKE '%" & filter & "%' OR location LIKE '%" & filter & "%' OR description LIKE '%" & filter & "%' OR CONCAT('STN-', LPAD(station_id, 4, '0')) LIKE '%" & filter & "%'"
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
        readqueary("INSERT INTO operator (full_name,position,contact_number,station_id,availability_status) VALUES ('" & data("full_name").ToUpper() & "','" & data("position").ToUpper() & "','" & data("contact_number") & "'," & Val(data("station_id")) & ",'AVAILABLE')")
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
        Dim data = ShowCrudInputForm("Add Employee", New Dictionary(Of String, String) From {{"full_name", ""}, {"email_address", ""}, {"password", ""}, {"position", ""}, {"contact_number", ""}, {"offiice_assignment", "MAIN OFFICE"}})
        If data Is Nothing Then Return
        Dim hashedPassword As String = If(data("password") <> "", HashPassword(data("password")), "")
        readqueary("INSERT INTO employee (full_name,email_address,password,position,contact_number,offiice_assignment) VALUES ('" & data("full_name").ToUpper() & "','" & data("email_address") & "','" & hashedPassword & "','" & data("position").ToUpper() & "','" & data("contact_number") & "','" & data("offiice_assignment").ToUpper() & "')")
        LoadEmployeeGrid(txtEmployeeSearch.Text.Trim())
    End Sub

    Private Sub EmployeeGrid_Edit(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Dim r = dgvEmployeeUi.Rows(e.RowIndex)
        Dim currentPasswordHash As String = r.Cells("password").Value.ToString()
        ' We pass an empty string for the password so the UI doesn't display the hash. If the user leaves it blank, we keep the old hash.
        Dim data = ShowCrudInputForm("Edit Employee", New Dictionary(Of String, String) From {{"employee_id", r.Cells("employee_id").Value.ToString()}, {"full_name", r.Cells("full_name").Value.ToString()}, {"email_address", r.Cells("email_address").Value.ToString()}, {"password", ""}, {"position", r.Cells("position").Value.ToString()}, {"contact_number", r.Cells("contact_number").Value.ToString()}, {"offiice_assignment", r.Cells("offiice_assignment").Value.ToString()}}, "employee_id")
        If data Is Nothing Then Return

        Dim finalPasswordHash As String = If(data("password") <> "", HashPassword(data("password")), currentPasswordHash)

        readqueary("UPDATE employee SET full_name='" & data("full_name").ToUpper() & "', email_address='" & data("email_address") & "', password='" & finalPasswordHash & "', position='" & data("position").ToUpper() & "', contact_number='" & data("contact_number") & "', offiice_assignment='" & data("offiice_assignment").ToUpper() & "' WHERE employee_id=" & Val(data("employee_id")))
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
            ' Try exact match on station_name first, then description, then LIKE on both
            Dim sql As String = "SELECT station_id FROM station WHERE UPPER(station_name)=@desc OR UPPER(description)=@desc OR UPPER(station_name) LIKE @descLike OR UPPER(description) LIKE @descLike LIMIT 1"
            Using cmdLocal As New MySqlCommand(sql, localConn)
                cmdLocal.Parameters.AddWithValue("@desc", stationDescription)
                cmdLocal.Parameters.AddWithValue("@descLike", "%" & stationDescription & "%")
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
        Dim txtNearestStation As New TextBox With {.Location = New Point(20, 400), .Width = 190, .ReadOnly = True}

        Dim lblOperator As New Label With {.Text = "Assign Operator", .Location = New Point(230, 380), .AutoSize = True}
        Dim cmbOperator As New ComboBox With {.Location = New Point(230, 400), .Width = 190, .DropDownStyle = ComboBoxStyle.DropDownList}

        Dim lblHectares As New Label With {.Text = "Hectares Served", .Location = New Point(20, 440), .AutoSize = True}
        Dim txtHectares As New TextBox With {.Location = New Point(20, 460), .Width = 190}

        Dim lblStatus As New Label With {.Text = "Service Status", .Location = New Point(230, 440), .AutoSize = True}
        Dim cmbStatus As New ComboBox With {.Location = New Point(230, 460), .Width = 190, .DropDownStyle = ComboBoxStyle.DropDownList}
        cmbStatus.Items.AddRange(New Object() {"Pending", "Approved", "Rejected", "Done"})
        cmbStatus.SelectedIndex = 0

        Dim btnSave As New Button With {.Text = "Submit Request", .Location = New Point(290, 500), .Width = 130, .BackColor = Color.DarkGreen, .ForeColor = Color.White}
        Dim btnCancel As New Button With {.Text = "Cancel", .Location = New Point(200, 500), .Width = 80}

        frm.Controls.AddRange(New Control() {lblFarmer, cmbFarmer, lblService, cmbService, lblReqDate, dtpRequestDate, lblProv, cmbProv, lblCity, cmbCityReq, lblBrgy, cmbBrgy, lblNearestStation, txtNearestStation, lblOperator, cmbOperator, lblHectares, txtHectares, lblStatus, cmbStatus, btnSave, btnCancel})

        Try
            Dim dtFarmers As New DataTable
            Dim dtServices As New DataTable
            Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name

            Using localConn As New MySqlConnection(strconn)
                localConn.Open()

                Using cmdF As New MySqlCommand("SELECT farmer_id, CONCAT(CONCAT('RSBSA-', LPAD(farmer_id, 4, '0')), ' - ', full_name) AS farmer_label FROM farmer ORDER BY farmer_id DESC", localConn)
                    dtFarmers.Load(cmdF.ExecuteReader())
                End Using

                Using cmdS As New MySqlCommand("SELECT service_id, service_name FROM service ORDER BY service_id DESC", localConn)
                    dtServices.Load(cmdS.ExecuteReader())
                End Using
            End Using

            cmbFarmer.DataSource = dtFarmers
            cmbFarmer.DisplayMember = "farmer_label"
            cmbFarmer.ValueMember = "farmer_id"

            cmbService.DataSource = dtServices
            cmbService.DisplayMember = "service_name"
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

                Dim dtOps As New DataTable
                Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
                Dim currentStationId As Integer = GetStationIdByCity(selectedCity)

                Using localConn As New MySqlConnection(strconn)
                    localConn.Open()

                    If currentStationId > 0 Then
                        ' Try loading operators for this specific station first
                        Using cmdOps As New MySqlCommand("SELECT operator_id, full_name FROM operator WHERE station_id=@station_id AND (availability_status='AVAILABLE' OR availability_status IS NULL) ORDER BY full_name", localConn)
                            cmdOps.Parameters.AddWithValue("@station_id", currentStationId)
                            dtOps.Load(cmdOps.ExecuteReader())
                        End Using
                    End If

                    ' Fallback: if no operators found for that station, load ALL available operators
                    If dtOps.Rows.Count = 0 Then
                        dtOps = New DataTable
                        Using cmdOps As New MySqlCommand("SELECT operator_id, full_name FROM operator WHERE (availability_status='AVAILABLE' OR availability_status IS NULL) ORDER BY full_name", localConn)
                            dtOps.Load(cmdOps.ExecuteReader())
                        End Using
                    End If
                End Using

                If dtOps.Rows.Count > 0 Then
                    cmbOperator.DataSource = dtOps
                    cmbOperator.DisplayMember = "full_name"
                    cmbOperator.ValueMember = "operator_id"
                Else
                    cmbOperator.DataSource = Nothing
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

                Dim selectedStatus As String = cmbStatus.Text.Trim()
                If selectedStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase) Then
                    If cmbOperator.SelectedValue Is Nothing Then
                        MessageBox.Show("Please assign an available operator before approving the request.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return
                    End If
                End If

                Dim farmLocation As String = barangayText & ", " & cityText & ", CAMARINES NORTE"
                Dim validationDate As Date = Date.Now.Date
                Dim hasRequestId As Boolean = ColumnExists("service_request", "request_id")
                Dim hasOperatorId As Boolean = ColumnExists("service_request", "operator_id")
                Dim hasStationId As Boolean = ColumnExists("service_request", "station_id")
                Dim hasAssignedMachinery As Boolean = ColumnExists("service_request", "assigned_machinery_id")
                Dim hasAssignmentDate As Boolean = ColumnExists("service_request", "assignment_date")

                ' Capture operator ID BEFORE any database operations while the combobox is still valid
                Dim selectedOperatorId As Integer = 0
                If cmbOperator.SelectedValue IsNot Nothing Then
                    Try
                        selectedOperatorId = Convert.ToInt32(cmbOperator.SelectedValue)
                    Catch
                        selectedOperatorId = 0
                    End Try
                End If

                Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name
                Using localConn As New MySqlConnection(strconn)
                    localConn.Open()
                    Dim requestId As Integer = 0

                    Dim cols As New List(Of String) From {"farmer_id", "service_id", "request_date", "farm_location", "hectares_served", "validation_date", "service_status"}
                    Dim vals As New List(Of String) From {"@farmer_id", "@service_id", "@request_date", "@farm_location", "@hectares_served", "@validation_date", "@service_status"}

                    If hasStationId Then
                        cols.Add("station_id")
                        vals.Add("@station_id")
                    End If

                    If hasOperatorId Then
                        cols.Add("operator_id")
                        vals.Add("@operator_id")
                    End If

                    If hasAssignedMachinery Then
                        cols.Add("assigned_machinery_id")
                        vals.Add("@machinery_id_ins")
                    End If

                    If hasAssignmentDate Then
                        cols.Add("assignment_date")
                        vals.Add("@assignment_date_ins")
                    End If

                    Dim machineryId As Integer = 0
                    Using cmdMach As New MySqlCommand("SELECT machinery_id FROM service WHERE service_id=@sid LIMIT 1", localConn)
                        cmdMach.Parameters.AddWithValue("@sid", CInt(cmbService.SelectedValue))
                        Dim machObj = cmdMach.ExecuteScalar()
                        If machObj IsNot Nothing AndAlso Not IsDBNull(machObj) Then
                            machineryId = Convert.ToInt32(machObj)
                        End If
                    End Using

                    Dim sqlInsert As String = $"INSERT INTO service_request ({String.Join(", ", cols)}) VALUES ({String.Join(", ", vals)})"

                    Using cmdIns As New MySqlCommand(sqlInsert, localConn)
                        cmdIns.Parameters.AddWithValue("@farmer_id", CInt(cmbFarmer.SelectedValue))
                        cmdIns.Parameters.AddWithValue("@service_id", CInt(cmbService.SelectedValue))
                        cmdIns.Parameters.AddWithValue("@request_date", dtpRequestDate.Value.Date)
                        cmdIns.Parameters.AddWithValue("@farm_location", farmLocation)
                        cmdIns.Parameters.AddWithValue("@hectares_served", hectares)
                        cmdIns.Parameters.AddWithValue("@validation_date", validationDate)
                        cmdIns.Parameters.AddWithValue("@service_status", selectedStatus)
                        If hasStationId Then cmdIns.Parameters.AddWithValue("@station_id", stationId)
                        If hasOperatorId Then
                            If selectedOperatorId > 0 Then
                                cmdIns.Parameters.AddWithValue("@operator_id", selectedOperatorId)
                            Else
                                cmdIns.Parameters.AddWithValue("@operator_id", DBNull.Value)
                            End If
                        End If
                        If hasAssignedMachinery Then
                            If machineryId > 0 Then
                                cmdIns.Parameters.AddWithValue("@machinery_id_ins", machineryId)
                            Else
                                cmdIns.Parameters.AddWithValue("@machinery_id_ins", DBNull.Value)
                            End If
                        End If
                        If hasAssignmentDate Then cmdIns.Parameters.AddWithValue("@assignment_date_ins", If(selectedStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase), Date.Now.Date, DBNull.Value))

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
                        ' Update machinery availability

                        If machineryId > 0 Then
                            Using cmdUpMach As New MySqlCommand("UPDATE machinery SET availability_status='NOT AVAILABLE' WHERE machinery_id=@mid", localConn)
                                cmdUpMach.Parameters.AddWithValue("@mid", machineryId)
                                cmdUpMach.ExecuteNonQuery()
                            End Using
                        End If

                        ' Update operator availability
                        If selectedOperatorId > 0 Then
                            Using cmdUpOp As New MySqlCommand("UPDATE operator SET availability_status='CURRENTLY OPERATING MACHINE' WHERE operator_id=@oid", localConn)
                                cmdUpOp.Parameters.AddWithValue("@oid", selectedOperatorId)
                                cmdUpOp.ExecuteNonQuery()
                            End Using
                        End If

                        MessageBox.Show("Request approved! Operator and Machinery are now active.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show("Request submitted successfully. Operator is assigned but remains available until approval.", "PABEO", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using

                LoadRequestsGrid()
                LoadMachineryCrudGrid()
                LoadOperatorGrid()
                frm.Close()
            End Sub

        frm.ShowDialog(Me)
    End Sub

    Private Sub txtServiceSearch_TextChanged(sender As Object, e As EventArgs) Handles txtServiceSearch.TextChanged
        Dim searchKey = txtServiceSearch.Text.Trim

        If searchKey = "" Then
            LoadServiceGrid()
            Return
        End If

        Try
            ' CRITICAL: Column alias MUST match DataPropertyName in Designer (formatted_service_id)
            Dim sql = "SELECT s.service_id, " &
                                "CONCAT('SRV-', LPAD(s.service_id, 4, '0')) AS formatted_service_id, " &
                                "IFNULL(m.machinery_name, 'N/A') AS machinery_name, " &
                                "s.service_name, s.service_type, s.description, s.policy_limit, " &
                                "e.full_name AS employee_name " &
                                "FROM service s " &
                                "LEFT JOIN machinery m ON s.machinery_id = m.machinery_id " &
                                "LEFT JOIN employee e ON s.employee_id = e.employee_id " &
                           "WHERE CONCAT('SRV-', LPAD(s.service_id, 4, '0')) LIKE '%" & searchKey & "%' " &
                           "OR s.service_name LIKE '%" & searchKey & "%' " &
                           "OR s.service_type LIKE '%" & searchKey & "%' " &
                           "OR IFNULL(m.machinery_name, '') LIKE '%" & searchKey & "%'"

            readqueary(sql)

            If cmdread IsNot Nothing Then
                Dim dt As New DataTable
                dt.Load(cmdread)

                ' This is the most important part for PABEO grids
                dgvServices.AutoGenerateColumns = False
                dgvServices.DataSource = dt
            End If

        Catch ex As Exception
            Console.WriteLine("Search Error: " & ex.Message)
        Finally
            If cmdread IsNot Nothing Then cmdread.Close()
        End Try
    End Sub

    Private Sub pnlServices_Paint(sender As Object, e As PaintEventArgs) Handles pnlServices.Paint

    End Sub

    Private Sub pnlDGVService_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub BtnTestConection_Click(sender As Object, e As EventArgs) Handles BtnTestConection.Click
        BtnTestConection.Enabled = False
        BtnTestConection.Text = "Testing..."
        Application.DoEvents()

        Dim testConn As New MySql.Data.MySqlClient.MySqlConnection()

        Try
            Dim connStr As String =
            "server=" & txtServer.Text.Trim() & ";" &
            "uid=" & txtUID.Text.Trim() & ";" &
            "password=" & txtPWD.Text.Trim() & ";" &
            "database=" & txtDatabase.Text.Trim() & ";" &
            "Connect Timeout=5;"

            testConn.ConnectionString = connStr
            testConn.Open()

            ' ── SUCCESS ──────────────────────────────────────────
            MessageBox.Show(
            "✅  Connection Successful!" & vbCrLf & vbCrLf &
            "Server:   " & txtServer.Text.Trim() & vbCrLf &
            "Database: " & txtDatabase.Text.Trim() & vbCrLf &
            "MySQL v" & testConn.ServerVersion,
            "PABEO - Connection Test",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information)

        Catch ex As MySql.Data.MySqlClient.MySqlException
            ' ── MYSQL-SPECIFIC ERRORS ────────────────────────────
            Dim friendlyMsg As String = ""

            Select Case ex.Number
                Case 0
                    friendlyMsg = "Cannot reach the server." & vbCrLf &
                              "Make sure MySQL is running."
                Case 1045
                    friendlyMsg = "Wrong username or password." & vbCrLf &
                              "Please check your UID and Password."
                Case 1049
                    friendlyMsg = "Database '" & txtDatabase.Text.Trim() &
                              "' was not found." & vbCrLf &
                              "Please check the Database name."
                Case 1042
                    friendlyMsg = "Cannot connect to server '" &
                              txtServer.Text.Trim() & "'." & vbCrLf &
                              "Server is unreachable or offline."
                Case 1044
                    friendlyMsg = "Access denied to database '" &
                              txtDatabase.Text.Trim() & "'." & vbCrLf &
                              "User does not have permission."
                Case Else
                    friendlyMsg = "MySQL Error " & ex.Number &
                              ": " & ex.Message
            End Select

            MessageBox.Show(
            "❌  Connection Failed!" & vbCrLf & vbCrLf & friendlyMsg,
            "PABEO - Connection Test",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Catch ex As Exception
            ' ── GENERIC ERRORS (timeout, network, etc.) ──────────
            MessageBox.Show(
            "❌  Connection Failed!" & vbCrLf & vbCrLf & ex.Message,
            "PABEO - Connection Test",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error)

        Finally
            If testConn.State = ConnectionState.Open Then testConn.Close()
            testConn.Dispose()
            BtnTestConection.Enabled = True
            BtnTestConection.Text = "Test Connection"
        End Try
    End Sub
End Class
