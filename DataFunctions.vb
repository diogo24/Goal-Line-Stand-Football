Imports System.Data.Sql
Imports System.Data.SqlTypes

Public Class DataFunctions

    'Loads DT into memory
    Public Sub LoadTable(ByVal DT As DataTable, ByVal TableName As String)
        Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=MNasty-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
            'Conn.ConnectionString = "Data Source=.\SQLExpress; integrated security=true; attachdbfilename=D:\Documents and Settings\MNasty\My Documents\SQL Server Management Studio\Projects\football.mdf; User instance=true;"
            Conn.Open()
        End If

        Dim Cmd As SqlClient.SqlCommand = New SqlClient.SqlCommand("Select * From " & TableName & "")
        Dim DA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(Cmd)
        DA.SelectCommand.Connection = Conn
        DA.Fill(DT)
        Conn.Close()

    End Sub
    'Updates Data Into a Table that has not been modified except by inserting data into the columns
    Public Sub UpdateTable(ByVal DT As DataTable, ByVal TableName As String)
        Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection

        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=MNASTY-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
            Conn.Open()
        Else
            Conn.ConnectionString = "Data Source=MNASTY-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
        End If

        Dim SQL As SqlClient.SqlCommand = New SqlClient.SqlCommand("Select * From " & TableName & "")
        Dim DA As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(SQL)
        DA.SelectCommand.Connection = Conn
        Dim CMD As SqlClient.SqlCommandBuilder = New SqlClient.SqlCommandBuilder(DA)

        CMD.GetUpdateCommand()
        DA.ContinueUpdateOnError = True
        DA.Update(DT)
        Conn.Close()

    End Sub

    Public Sub DeleteTable(ByVal DT As DataTable, ByVal TableName As String)
        Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=MNASTY-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
            Conn.Open()
        End If
        Dim strSQL As String
        strSQL = "TRUNCATE TABLE " & TableName & ""
        Using connection As New SqlClient.SqlConnection(Conn.ConnectionString)
            Dim cmd As New SqlClient.SqlCommand(strSQL, connection)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub CreateTable(ByVal DT As DataTable, ByVal TableName As String, ByVal SQLFieldNames As String)
        Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=MNASTY-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
            Conn.Open()
        End If
        Dim restrictions(3) As String
        restrictions(2) = TableName
        Dim dbTbl As DataTable = Conn.GetSchema("Tables", restrictions)

        If dbTbl.Rows.Count = 0 Then 'Checks to see if Table Exists or not.  
            'Table does not exist---makes new table
            Dim strSQL As String = "CREATE TABLE " & TableName & " ( " & SQLFieldNames & " )"
            Using connection As New SqlClient.SqlConnection(Conn.ConnectionString)
                Dim cmd As New SqlClient.SqlCommand(strSQL, connection)
                cmd.Connection.Open()
                cmd.ExecuteNonQuery()
            End Using
        Else
            'Table exists---exits function.
            Exit Sub
        End If
        dbTbl.Dispose()
        Conn.Close()
        Conn.Dispose()
    End Sub


    Public Sub AlterTable(ByVal DT As DataTable, ByVal TableName As String, ByVal SQLAlterString As String)
        Dim Conn As SqlClient.SqlConnection = New SqlClient.SqlConnection
        If Conn.State <> ConnectionState.Open Then
            Conn.ConnectionString = "Data Source=MNASTY-PC;Initial Catalog=FootballData;User ID=MNasty-PC\MNasty;Integrated Security=True"
            Conn.Open()
        End If
        Dim StrSQL As String = "ALTER TABLE " & TableName & " " & SQLAlterString & ""
        Using connection As New SqlClient.SqlConnection(Conn.ConnectionString)
            Dim cmd As New SqlClient.SqlCommand(StrSQL, connection)
            cmd.Connection.Open()
            cmd.ExecuteNonQuery()
        End Using


    End Sub
End Class
