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

        txtServiceName.Clear()
        txtServiceDescription.Clear()
    End Sub

    Private Sub txtServiceID_TextChanged(sender As Object, e As EventArgs) Handles txtServiceID.TextChanged

    End Sub

    Private Sub txtServiceName_TextChanged(sender As Object, e As EventArgs) Handles txtServiceName.TextChanged

    End Sub

    Private Sub cmbServiceType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbServiceType.SelectedIndexChanged

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
        ' 1. Basic Validation
        If String.IsNullOrWhiteSpace(txtServiceName.Text) OrElse cmbServiceType.SelectedIndex = -1 Then
            MessageBox.Show("Please provide a Service Name and Type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Prepare Data (Treating pLimit as a standard String)
        Dim sName As String = txtServiceName.Text.Trim().ToUpper()
        Dim sType As String = cmbServiceType.Text.ToUpper()
        Dim sDesc As String = txtServiceDescription.Text.Trim()
        Dim pLimit As String = txtPolicyLimit.Text.Trim() ' No numeric checks here!

        Dim empID As String = If(cmbSEmployeeID.SelectedValue IsNot Nothing, cmbSEmployeeID.SelectedValue.ToString(), "0")

        ' 3. INSERT Query
        Dim sql As String = "INSERT INTO service (service_name, service_type, description, policy_limit, employee_id) " &
                       "VALUES ('" & sName & "', '" & sType & "', '" & sDesc & "', '" & pLimit & "', '" & empID & "')"

        Try
            readqueary(sql)
            MessageBox.Show("Service successfully saved!", "PABEO System", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadServicesGrid()
            RefreshServiceStats()
            btnServiceCancel_Click(Nothing, Nothing)

        Catch ex As Exception
            MessageBox.Show("Database Error: " & ex.Message & vbCrLf & "Check if policy_limit is still an INT in MySQL!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    Private Sub cmbSEmployeeID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSEmployeeID.SelectedIndexChanged

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
                cmbSEmployeeID.DataSource = dt
                cmbSEmployeeID.DisplayMember = "full_name"   ' What the user sees
                cmbSEmployeeID.ValueMember = "employee_id"   ' The actual ID saved to DB

                ' Set to -1 so it starts empty
                cmbSEmployeeID.SelectedIndex = -1
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
End Class
