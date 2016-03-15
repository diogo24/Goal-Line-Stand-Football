﻿Public Class Scouts
    Inherits Personnel

    Public Sub GenScouts(ByVal NumScouts As Integer)

        Dim MyOPhil As String
        Dim MyDPhil As String
        Dim SQLFieldNames As String = "ScoutID int NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, Experience int NULL, OffPhil varchar(20) NULL,
DefPhil varchar(20) NULL, JudgingAct int NULL, JudgingPot int NULL, JudgingQB Int NULL, JudgingRB int NULL, JudgingRec int NULL, JudgingOL int NULL, JudgingDL int NULL, JudgingLB int NULL, JudgingCB int NULL, 
JudgingSF int NULL, FBInt int NULL, ValuesCharacter int NULL, ProductionVsCombine int NULL, AthleticismVsMental int NULL,  WorkEthic int NULL, CONSTRAINT Scout_ID PRIMARY KEY(ScoutID)"

        SQLiteTables.CreateTable(MyDB, ScoutDT, "Scouts", SQLFieldNames)
        'SQLiteTables.DeleteTable(MyDB, ScoutDT, "Scouts")
        SQLiteTables.LoadTable(MyDB, ScoutDT, "Scouts")
        ScoutDT.Rows.Add(0)

        For i As Integer = 1 To NumScouts
            ScoutDT.Rows.Add(i)

            If i < 33 Then
                ScoutDT.Rows(i).Item("TeamID") = i
            ElseIf i > 32 And i < 385 Then
                Dim remain As Integer = i \ 32
                Dim rem2 As Integer = i - (32 * remain)
                If rem2 = 0 Then
                    ScoutDT.Rows(i).Item("TeamID") = 32
                Else : ScoutDT.Rows(i).Item("TeamID") = rem2
                End If
            Else : ScoutDT.Rows(i).Item("TeamID") = 0
            End If
            MyOPhil = GetOffPhil()
            MyDPhil = GetDefPhil()
            GenNames(ScoutDT, i, "Scout")
            ScoutDT.Rows(i).Item("Experience") = MT.GenerateInt32(0, (ScoutDT.Rows(i).Item("Age") - 32))
            ScoutDT.Rows(i).Item("OffPhil") = String.Format("'{0}'", MyOPhil)
            ScoutDT.Rows(i).Item("DefPhil") = String.Format("'{0}'", MyDPhil)
            ScoutDT.Rows(i).Item("JudgingAct") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingPot") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingQB") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingRB") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingRec") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingOL") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingDL") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingLB") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingCB") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("JudgingSF") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("FBInt") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("ValuesCharacter") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("ProductionVsCombine") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("AthleticismVsMental") = MT.GetGaussian(49.5, 16.5)
            ScoutDT.Rows(i).Item("WorkEthic") = MT.GetGaussian(49.5, 16.5)

        Next i

        SQLiteTables.BulkInsert(MyDB, ScoutDT, "Scouts")

    End Sub
End Class
