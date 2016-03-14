Public Class Coaches
    Inherits Personnel


    Public SQLFieldNames As String = "CoachID int NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, Experience int NULL, OffPhil varchar(50) NULL, 
DefPhil Varchar(50) NULL, LoyaltyPlayers int NULL, LoyaltyCoaches int NULL, Ego int NULL, OffAbility int NULL, DefAbility int NULL, Stability int NULL, DevPlayers int NULL, JudgingAct int NULL, JudgingPot int NULL,
JudgingQB int NULL, JudgingRB int NULL, JudgingRec int NULL, JudgingOL int NULL, JudgingDL int NULL, JudgingLB int NULL, JudgingCB int NULL, JudgingSF int NULL, Accountability int NULL, Motivating int NULL, 
TimeMgmt int NULL, Clutch int NULL, Conservative int NULL, FBInt int NULL, Adjustments int NULL, ValuesST int NULL, ValuesCharacter int NULL, WorkEthic int NULL, Preparation int NULL, Focus int NULL, 
PlaycallingSkill int NULL, CONSTRAINT Coach_ID PRIMARY KEY(CoachID)"
    Public Sub GenCoaches(ByVal NumCoaches As Integer)

        SQLiteTables.CreateTable("Football", CoachDT, "Coaches", SQLFieldNames)
        'SQLCmd.DeleteTable("Football", CoachDT, "Coaches")
        SQLiteTables.LoadTable("Football", CoachDT, "Coaches")
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
            Try
                CoachDT.Rows(i).Item("Experience") = MT.GenerateInt32(0, (CoachDT.Rows(i).Item("Age") - 32))
                CoachDT.Rows(i).Item("OffPhil") = String.Format("'{0}'", GetOffPhil())
                CoachDT.Rows(i).Item("DefPhil") = String.Format("'{0}'", GetDefPhil())
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
            Catch ex As System.InvalidCastException
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.Data)
            End Try
        Next i

        SQLiteTables.BulkInsert("Football", CoachDT, "Coaches")
        'SQLiteTables.UpdateTable(CoachDT, "Coaches")
    End Sub
End Class
