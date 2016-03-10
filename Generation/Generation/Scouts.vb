Public Class Scouts
    Inherits Personnel

    Public Sub GenScouts(ByVal NumScouts As Integer)

        Dim SQLFieldNames As String = "ScoutID int Not NULL, TeamID int Not NULL, FName varchar(20) Not NULL, LName varchar(20) Not NULL, College varchar(50) Not NULL, Age int Not NULL, DOB varchar(12) NOT NULL, Experience int Not NULL, OffPhil varchar(20) Not NULL,
DefPhil varchar(20) Not NULL, JudgingAct int Not NULL, JudgingPot int Not NULL, JudgingQB Int Not NULL, JudgingRB int Not NULL, JudgingRec int Not NULL, JudgingOL int Not NULL, JudgingDL int Not NULL, JudgingLB int Not NULL, JudgingCB int Not NULL, 
JudgingSF int Not NULL, FBInt int Not NULL, ValuesCharacter int Not NULL, ProductionVsCombine int Not NULL, AthleticismVsMental int Not NULL,  WorkEthic int Not NULL, CONSTRAINT Scout_ID PRIMARY KEY(ScoutID)"

        GetTables.CreateTable(ScoutDT, "Scouts", SQLFieldNames)
        GetTables.DeleteTable(ScoutDT, "Scouts")
        GetTables.LoadTable(ScoutDT, "Scouts")
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

            GenNames(ScoutDT, i, "Scout")
            ScoutDT.Rows(i).Item("Experience") = MT.GenerateInt32(0, (ScoutDT.Rows(i).Item("Age") - 32))
            ScoutDT.Rows(i).Item("OffPhil") = GetOffPhil()
            ScoutDT.Rows(i).Item("DefPhil") = GetDefPhil()
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

        GetTables.UpdateTable(ScoutDT, "Scouts")

    End Sub
End Class
