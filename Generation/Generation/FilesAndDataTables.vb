Imports System.IO
Public Module FilesAndDataTables
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
    Public GetTables As New SQLFunctions.DataFunctions
    Public ReadFName As StreamReader = New StreamReader("FName.txt")
    Public ReadLName As StreamReader = New StreamReader("LName.txt")
    Public ReadCollege As StreamReader = New StreamReader("Colleges.txt")
End Module
