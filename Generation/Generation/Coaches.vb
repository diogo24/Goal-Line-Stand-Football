Public Class Coaches
    Inherits Personnel

    Public Sub GenCoaches(ByVal NumCoaches As Integer)

        Dim SQLFieldNames As String = "CoachID int NOT NULL, TeamID int NOT NULL, FName varchar(20) NOT NULL, LName varchar(20) NOT NULL, College varchar(50) NOT NULL, Age int NOT NULL, DOB varchar(12) NOT NULL, Experience int NOT NULL, OffPhil varchar(50) NOT NULL, 
DefPhil Varchar(50) NOT NULL, LoyaltyPlayers int NOT NULL, LoyaltyCoaches int NOT NULL, Ego int NOT NULL, OffAbility int NOT NULL, DefAbility int NOT NULL, Stability int NOT NULL, DevPlayers int NOT NULL, JudgingAct int Not NULL, JudgingPot int Not NULL,
JudgingQB int NOT NULL, JudgingRB int NOT NULL, JudgingRec int NOT NULL, JudgingOL int NOT NULL, JudgingDL int NOT NULL, JudgingLB int NOT NULL, JudgingCB int NOT NULL, JudgingSF int NOT NULL, Accountability int NOT NULL, Motivating int NOT NULL, 
TimeMgmt int NOT NULL, Clutch int NOT NULL, Conservative int NOT NULL, FBInt int NOT NULL, Adjustments int NOT NULL, ValuesST int NOT NULL, ValuesCharacter int NOT NULL, WorkEthic int NOT NULL, Preparation int NOT NULL, Focus int NOT NULL, 
PlaycallingSkill int NOT NULL, CONSTRAINT Coach_ID PRIMARY KEY(CoachID)"

        GetTables.CreateTable(CoachDT, "Coaches", SQLFieldNames)
        GetTables.DeleteTable(CoachDT, "Coaches")
        GetTables.LoadTable(CoachDT, "Coaches")
        CoachDT.Rows.Add(0)

        For i As Integer = 1 To NumCoaches
            CoachDT.Rows.Add(i)

            If i < 33 Then
                CoachDT.Rows(i).Item("TeamID") = i
            ElseIf i > 32 And i < 385 Then
                Dim remain As Integer = i \ 32
                Dim rem2 As Integer = i - (32 * remain)
                If rem2 = 0 Then
                    CoachDT.Rows(i).Item("TeamID") = 32
                Else : CoachDT.Rows(i).Item("TeamID") = rem2
                End If
            Else : CoachDT.Rows(i).Item("TeamID") = 0
            End If

            GenNames(CoachDT, i, "Coach")

            CoachDT.Rows(i).Item("Experience") = MT.GenerateInt32(0, (CoachDT.Rows(i).Item("Age") - 32))
            CoachDT.Rows(i).Item("OffPhil") = GetOffPhil()
            CoachDT.Rows(i).Item("DefPhil") = GetDefPhil()
            CoachDT.Rows(i).Item("LoyaltyPlayers") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("LoyaltyCoaches") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Ego") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("OffAbility") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("DefAbility") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Stability") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("DevPlayers") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingAct") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingPot") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingQB") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingRB") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingRec") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingOL") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingDL") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingLB") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingCB") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("JudgingSF") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Accountability") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Motivating") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("TimeMgmt") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Clutch") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Conservative") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("FBInt") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Adjustments") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("ValuesST") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("ValuesCharacter") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("WorkEthic") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Preparation") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("Focus") = MT.GetGaussian(49.5, 16.5)
            CoachDT.Rows(i).Item("PlaycallingSkill") = MT.GetGaussian(49.5, 16.5)

        Next i
        GetTables.UpdateTable(CoachDT, "Coaches")
    End Sub
End Class
