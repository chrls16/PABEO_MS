Imports System.Windows.Forms

Public Class mdiPABEO
    Private Sub mdiPABEO_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If e.CloseReason = CloseReason.UserClosing Then
            Application.Exit()
        End If
    End Sub

    Private Sub LoadPanelToMDI(ByVal targetPanel As Panel)
        If targetPanel Is Nothing Then
            MsgBox("Error: Panel not found in frmPanelHolder.", MsgBoxStyle.Critical)
            Return
        End If

        Me.pnlForms.Controls.Clear()

        targetPanel.Dock = DockStyle.Fill
        targetPanel.Visible = True

        Me.pnlForms.Controls.Add(targetPanel)
    End Sub

    ' --- Button Actions for PABEO ---

    Private Sub btnFarmers_Click(sender As Object, e As EventArgs) Handles btnFarmers.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlFarmers, "Farmers")
    End Sub

    Private Sub btnServices_Click(sender As Object, e As EventArgs) Handles btnServices.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlServices, "Services")
    End Sub

    Private Sub btnRequests_Click(sender As Object, e As EventArgs) Handles btnRequests.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlRequests, "Service Requests")
    End Sub

    Private Sub btnMachinery_Click(sender As Object, e As EventArgs) Handles btnMachinery.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlMachinery, "Machinery Inventory")
    End Sub

    Private Sub btnOperator_Click(sender As Object, e As EventArgs) Handles btnOperator.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlOperator, "Operators")
    End Sub

    Private Sub btnEmployee_Click(sender As Object, e As EventArgs) Handles btnEmployee.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlEmployee, "Employees")
    End Sub

    Private Sub btnStation_Click(sender As Object, e As EventArgs) Handles btnStation.Click
        SetActiveButton(sender)
        LoadPanelToMDI(frmPanelHolder.pnlStation, "Stations")
    End Sub

    Private Sub SetActiveButton(ByVal activeBtn As Button)
        Dim activeColor As Color = Color.MediumSeaGreen
        Dim defaultColor As Color = Color.Transparent
        Dim hoverColor As Color = Color.LightSeaGreen

        For Each ctrl As Control In pnlSideNav.Controls
            If TypeOf ctrl Is Button Then
                Dim btn As Button = DirectCast(ctrl, Button)

                btn.BackColor = defaultColor
                btn.ForeColor = Color.White

                btn.FlatStyle = FlatStyle.Flat
                btn.FlatAppearance.BorderSize = 0
                btn.FlatAppearance.MouseOverBackColor = hoverColor
            End If
        Next

        activeBtn.BackColor = Color.LightSeaGreen
        activeBtn.ForeColor = Color.White

        activeBtn.FlatAppearance.MouseOverBackColor = Color.LightSeaGreen
    End Sub

    Private Sub btnSystemConfig_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles btnSystemConfig.LinkClicked
        frmPanelHolder.Show()

        frmPanelHolder.pnlConfig.Visible = True
        frmPanelHolder.pnlConfig.BringToFront()

        RemoveHandler Me.FormClosing, AddressOf mdiPABEO_FormClosing
        Me.Hide()
    End Sub

    Private Sub LoadPanelToMDI(ByVal targetPanel As Panel, ByVal headerTitle As String)
        Me.pnlForms.Controls.Clear()

        targetPanel.Dock = DockStyle.Fill
        targetPanel.Visible = True

        Me.pnlForms.Controls.Add(targetPanel)

        lblHeader.Text = headerTitle
    End Sub
End Class