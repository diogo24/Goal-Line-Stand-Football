Imports System.IO
''' <summary>
''' Holds any Public variables for use within the Generation project
''' </summary>
Public Module FilesAndDataTables
    Public filePath As String = "files/"
    Public MT As New Mersenne.MersenneTwister
    Public CoachDT As New DataTable
    Public DraftDT As New DataTable
    Public GMDT As New DataTable
    Public OwnerDT As New DataTable
    Public ScoutDT As New DataTable
    Public ScoutGradeDT As New DataTable
    Public PlayerDT As New DataTable
    Public FirstNames As New DataTable
    Public LastNames As New DataTable
    Public Colleges As New DataTable
    Public Eval As New Evaluation
    Public MyDB As String = "Football"
    Public SQLiteTables As New SQLFunctions.SQLiteDataFunctions
    Public ReadFName As StreamReader = New StreamReader(filePath + "FName.txt")
    Public ReadLName As StreamReader = New StreamReader(filePath + "LName.txt")
    Public ReadCollege As StreamReader = New StreamReader(filePath + "Colleges.txt")
End Module
