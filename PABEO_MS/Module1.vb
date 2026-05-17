Imports MySql.Data.MySqlClient

Module Module1
    Public conn As New MySqlConnection
    Public cmd As New MySqlCommand
    Public cmdread As MySqlDataReader

    ' Connection parameters
    Public db_server As String = "localhost"
    Public db_uid As String = "root"
    Public db_pwd As String = ""
    Public db_name As String = "pabeo"

    ' IMPORTANT: Add this variable so frmPanelHolder can check the status
    Public isConnected As Boolean = False

    Public Sub readqueary(ByVal sql As String)
        Dim strconn As String = "server=" & db_server & ";uid=" & db_uid & ";password=" & db_pwd & ";database=" & db_name & ""

        Try
            With conn
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = strconn
                .Open()
            End With

            With cmd
                .Connection = conn
                .CommandText = sql
                cmdread = .ExecuteReader
            End With

            ' If we reach this line, it means the connection succeeded
            isConnected = True

        Catch ex As Exception
            ' If an error happens, connection failed
            isConnected = False
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Module

