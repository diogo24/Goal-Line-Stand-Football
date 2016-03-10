
''' <summary>
''' Creates the GM's as requested by the number supplied.  32 GM's will randomly be placed on a team
''' the other GM's will be unemployed and will have a chance to get hired when a GM gets fired
''' </summary>
Public Class GeneralManager
    Inherits Personnel
    Public Sub GenGMs(ByVal NumGMs As Integer)

        Dim SQLFieldNames As String = "GMID int NOT NULL, TeamID int NOT NULL, FName varchar(20) NOT NULL, LName varchar(20) NOT NULL, College varchar(50) NOT NULL, Age int NOT NULL, DOB varchar(12) NOT NULL, Experience int NOT NULL, CoachPatience int NOT NULL, 
Risktaker int NOT NULL, ValuesDraftPicks int NOT NULL, ValuesCombine int NOT NULL, ValuesCharacter int NOT NULL, ValuesProduction int NOT NULL, FranchiseTag int NOT NULL, JudgingDraft int NOT NULL, JudgingFA int NOT NULL, JudgingOwn int NOT NULL, 
JudgingQB int NOT NULL, JudgingRB int NOT NULL, JudgingRec int NOT NULL, JudgingOL int NOT NULL, JudgingDL int NOT NULL, JudgingLB int NOT NULL, JudgingCB int NOT NULL, JudgingSF int NOT NULL, Loyalty int NOT NULL, Ego int NOT NULL, 
OffPhil varchar(20) NOT NULL, QBImp int NOT NULL, RBImp int NOT NULL, FBImp int NOT NULL, WRImp int NOT NULL, WR2Imp int NOT NULL, WR3Imp int NOT NULL, LTImp int NOT NULL, LGImp int NOT NULL, CImp int NOT NULL, RGImp int NOT NULL, RTImp int NOT NULL, 
TEImp int NOT NULL, DefPhil varchar(20) NOT NULL, DEImp int NOT NULL, DE2Imp int NOT NULL, DTImp int NOT NULL, DT2Imp int NOT NULL, NTImp int NOT NULL, MLBImp int NOT NULL, WLBImp int NOT NULL, SLBImp int NOT NULL, ROLBImp int NOT NULL, 
LOLBImp int NOT NULL, CB1Imp int NOT NULL, CB2Imp int NOT NULL, CB3Imp int NOT NULL, FSImp int NOT NULL, SSImp int NOT NULL, DraftStrategy varchar(20) NOT NULL, TeamBuilding varchar(20) NOT NULL,  CONSTRAINT GM_ID PRIMARY KEY(GMID)"

        GetTables.CreateTable(GMDT, "GMs", SQLFieldNames)
        GetTables.DeleteTable(GMDT, "GMs")
        GetTables.LoadTable(GMDT, "GMs")
        GMDT.Rows.Add(0)

        For i As Integer = 1 To NumGMs

            GMDT.Rows.Add(i)
            If i < 33 Then
                GMDT.Rows(i).Item("TeamID") = i
            Else
                GMDT.Rows(i).Item("TeamID") = 0
            End If
            GenNames(GMDT, i, "GM")
            'GMDT.Rows(i).Item("Age") = MT.GenerateInt32(35, 70)
            GMDT.Rows(i).Item("Experience") = MT.GenerateInt32(0, (GMDT.Rows(i).Item("Age") - 35))
            GMDT.Rows(i).Item("Coachpatience") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingDraft") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingFA") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingOwn") = MT.GetGaussian(49.5, 16.5)
            'GMDT.Rows(i).Item("JudgingAct") = MT.GetGaussian(49.5, 16.5)
            'GMDT.Rows(i).Item("JudgingPot") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingQB") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingRB") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingRec") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingOL") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingDL") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingLB") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingCB") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("JudgingSF") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("Loyalty") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("Ego") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("OffPhil") = GetOffPhil()
            GMDT.Rows(i).Item("DefPhil") = GetDefPhil()
            GMDT.Rows(i).Item("DraftStrategy") = DraftStrategy()
            GMDT.Rows(i).Item("TeamBuilding") = TeamBuilding()
            PositionalImp(i, GMDT.Rows(i).Item("OffPhil"), GMDT.Rows(i).Item("DefPhil"))
            GMDT.Rows(i).Item("Risktaker") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("ValuesDraftPicks") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("FranchiseTag") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("ValuesCombine") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("ValuesCharacter") = MT.GetGaussian(49.5, 16.5)
            GMDT.Rows(i).Item("ValuesProduction") = MT.GetGaussian(49.5, 16.5)

        Next i

        GetTables.UpdateTable(GMDT, "GMs")

    End Sub

    Private Function DraftStrategy() As String
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 15 : Return "DraftTradeDown"
            Case 16 To 30 : Return "DraftBestAvail"
            Case 31 To 45 : Return "DraftTheirGuy"
            Case 46 To 60 : Return "DraftBiggestNeed"
            Case 61 To 75 : Return "DraftTradeUp"
            Case 76 To 85 : Return "TradePicksForPlayers"
            Case 86 To 100 : Return "TradePlayersForPicks"
        End Select
    End Function
    Private Function TeamBuilding() As String
        Select Case MT.GenerateInt32(1, 90)
            Case 1 To 15 : Return "DraftKeepCore"
            Case 16 To 30 : Return "DraftLetVetsGo"
            Case 31 To 45 : Return "DraftGetVets"
            Case 46 To 60 : Return "FAKeepCore"
            Case 61 To 75 : Return "FALetVetsGo"
            Case 76 To 90 : Return "FAGetVets"

        End Select
    End Function
    Private Sub PositionalImp(ByVal i As Integer, ByVal OffPhil As String, ByVal DefPhil As String)
        Select Case OffPhil
            'Sets the Importance of Various Positions for the offenses--over 100 is above normal
            'Under 100 is below normal---100 is average importance
            '1170 Points Allotted
            Case "BalPass"
                GMDT.Rows(i).Item("QBImp") = 110
                GMDT.Rows(i).Item("RBImp") = 90
                GMDT.Rows(i).Item("FBImp") = 74
                GMDT.Rows(i).Item("WRImp") = 110
                GMDT.Rows(i).Item("WR2Imp") = 95
                GMDT.Rows(i).Item("WR3Imp") = 85
                GMDT.Rows(i).Item("LTImp") = 110
                GMDT.Rows(i).Item("LGImp") = 95
                GMDT.Rows(i).Item("CImp") = 105
                GMDT.Rows(i).Item("RGImp") = 95
                GMDT.Rows(i).Item("RTImp") = 95
                GMDT.Rows(i).Item("TEImp") = 106

            Case "BalRun"
                GMDT.Rows(i).Item("QBImp") = 100
                GMDT.Rows(i).Item("RBImp") = 110
                GMDT.Rows(i).Item("FBImp") = 103
                GMDT.Rows(i).Item("WRImp") = 90
                GMDT.Rows(i).Item("WR2Imp") = 80
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 105
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 110
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 95
                GMDT.Rows(i).Item("TEImp") = 108

            Case "VertPass"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100

            Case "Smashmouth"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100

            Case "WCPass"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "WCRun"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "WCBal"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "SpreadRun"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "SpreadPass"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "SpreadBal"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100
            Case "Run-N-Shoot"
                GMDT.Rows(i).Item("QBImp") = 120
                GMDT.Rows(i).Item("RBImp") = 85
                GMDT.Rows(i).Item("FBImp") = 65
                GMDT.Rows(i).Item("WRImp") = 120
                GMDT.Rows(i).Item("WR2Imp") = 110
                GMDT.Rows(i).Item("WR3Imp") = 70
                GMDT.Rows(i).Item("LTImp") = 120
                GMDT.Rows(i).Item("LGImp") = 105
                GMDT.Rows(i).Item("CImp") = 115
                GMDT.Rows(i).Item("RGImp") = 105
                GMDT.Rows(i).Item("RTImp") = 105
                GMDT.Rows(i).Item("TEImp") = 100

        End Select

        Select Case DefPhil
            Case "4-3Attack"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "4-3Cover"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "4-3Bal"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "4-3StuffRun"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "3-4Attack"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "3-4Cover"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "3-4Bal"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "3-4StuffRun"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Attack"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Cover"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "Cover2Bal"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
            Case "46"
                GMDT.Rows(i).Item("DEImp") = 120
                GMDT.Rows(i).Item("DE2Imp") = 85
                GMDT.Rows(i).Item("DTImp") = 65
                GMDT.Rows(i).Item("DT2Imp") = 120
                GMDT.Rows(i).Item("NTImp") = 110
                GMDT.Rows(i).Item("MLBImp") = 70
                GMDT.Rows(i).Item("WLBImp") = 120
                GMDT.Rows(i).Item("SLBImp") = 105
                GMDT.Rows(i).Item("ROLBImp") = 115
                GMDT.Rows(i).Item("LOLBImp") = 105
                GMDT.Rows(i).Item("CB1Imp") = 105
                GMDT.Rows(i).Item("CB2Imp") = 100
                GMDT.Rows(i).Item("CB3Imp") = 100
                GMDT.Rows(i).Item("FSImp") = 100
                GMDT.Rows(i).Item("SSImp") = 100
        End Select


    End Sub
End Class
