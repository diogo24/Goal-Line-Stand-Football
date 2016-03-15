''' <summary>
''' These are the "Football" People on the team--GM's, Coaches and Scouts.  They all have football related evaluations they use contained in the functions below 
''' </summary>
Public Class Personnel
    Inherits Person
    Public Sub GenScoutGrades(ByVal NumScouts As Integer, ByVal NumPlayers As Integer)

        'SQLiteTables.LoadTable(ScoutGradeDT, "ScoutGrades")
        'If ScoutGradeDT.Rows.Count = 0 Then
        'Dim SQLFields As String = "(PID INTEGER PRIMARY KEY,"

        'For ScoutID As Integer = 0 To NumScouts
        'If ScoutID <> NumScouts Then
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2),"
        'Else
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2));"
        'End If
        'Next ScoutID

        SQLiteTables.DeleteTable(MyDB, ScoutGradeDT, "ScoutsGrade")
        SQLiteTables.LoadTable(MyDB, ScoutGradeDT, "ScoutsGrade")

        ScoutGradeDT.Rows.Add(0)

        For PlayerId As Integer = 1 To NumPlayers
            ScoutGradeDT.Rows.Add(PlayerId)
        Next PlayerId

        Eval.ScoutPlayerEval()
        'End If
    End Sub
    Public Function GetOffPhil() As String
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 10 : Return "BalPass"
            Case 11 To 20 : Return "BalRun"
            Case 21 To 30 : Return "VertPass"
            Case 31 To 40 : Return "Smashmouth"
            Case 41 To 50 : Return "WCPass"
            Case 51 To 60 : Return "WCRun"
            Case 61 To 70 : Return "WCBal"
            Case 71 To 76 : Return "SpreadRun"
            Case 77 To 82 : Return "SpreadPass"
            Case 83 To 88 : Return "SpreadBal"
            Case 89 To 90 : Return "Run-N-Shoot"
                'Case 91 To 100 : Return "PassHeavy"
        End Select
    End Function
    Public Function GetDefPhil() As String
        Select Case MT.GenerateInt32(1, 94)
            Case 1 To 12 : Return "4-3Attack"
            Case 13 To 23 : Return "4-3Cover"
            Case 23 To 33 : Return "4-3Bal"
            Case 33 To 43 : Return "4-3StuffRun"
            Case 44 To 49 : Return "3-4Attack"
            Case 50 To 55 : Return "3-4Cover"
            Case 56 To 61 : Return "3-4Bal"
            Case 62 To 67 : Return "3-4StuffRun"
            Case 68 To 75 : Return "Cover2Attack"
            Case 76 To 83 : Return "Cover2Cover"
            Case 84 To 91 : Return "Cover2Bal"
            Case 92 To 93 : Return "46"
                'Case 94 To 100 : Return "Hybrid"
        End Select
    End Function
End Class
