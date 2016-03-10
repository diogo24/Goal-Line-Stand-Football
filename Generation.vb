Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.Strings
Imports System.String

Public Class Generation

    Public Function GetName(ByVal TextFile As String, ByVal Count As Double) As String
        Dim SR As StreamReader = New StreamReader(TextFile)
        Dim EOF As String
        Dim Num As Double
        Dim Min As Double
        Dim Min2 As Double
        Dim Max As Double
        Dim Name As ArrayList = New ArrayList
        'Get a random #
        If TextFile = "Colleges.txt" Then
            Num = MT.GenerateInt32(0, Count)
        Else
            Num = Math.Round(MT.GenerateDouble(0, Count), 3)
            If Num > Count Then
                Num = Count
            End If
        End If
        Do While Name.Count = 0
            'If SR.EndOfStream = True Then
            'SR.Close()
            'SR.
            'End If
            EOF = SR.ReadLine
            Min = Regex.Match(EOF, "(?<=\p{L}+.*\,)\d.*(?=\,)").Value
            If TextFile = "Colleges.txt" Then
                Max = Regex.Match(EOF, "(?<=\p{L}+.*\,\d.*\,)\d+").Value
            Else
                Max = Regex.Match(EOF, "(?<=\p{L}+.*\,\d.*\,)\d+\.\d+").Value
            End If
            If Num >= Min And Num <= Max Then
                Name.Add(Regex.Match(EOF, "\p{L}+.*(?=\,.*\,.*)").ToString)
                'check to see if the next line has the same value...
                Do
                    EOF = SR.ReadLine()
                    If Num <> Count Then
                        If SR.EndOfStream <> True Then
                            Min2 = Regex.Match(EOF, "(?<=\p{L}+.*\,)\d.*(?=\,)").Value
                            If Min2 = Min Then
                                Name.Add(Regex.Match(EOF, "\p{L}+.*(?=\,.*\,.*)").ToString)
                            End If
                        End If
                    End If
                Loop While Min2 = Min
                If Name.Count > 1 Then
                    'Console.WriteLine(Name(MT.GenerateInt32(1, Name.Count)))
                    Return Name(MT.GenerateInt32(1, Name.Count))

                    'Return StrConv(Name(MT.GenerateInt32(1, Name.Count)), VbStrConv.ProperCase)
                Else
                    'Console.WriteLine(Name(0))
                    Return Name(0)
                    'Return StrConv(Name(0), VbStrConv.ProperCase)
                End If
            End If
        Loop
    End Function

    Public Sub GenOwners(ByVal NumOwners As Integer)
        Dim SQLFieldNames As String = "OwnerID int NOT NULL, TeamID int NOT NULL, FName varchar(20) NOT NULL, LName varchar(20) NOT NULL, College varchar(50) NOT NULL, OwnerRep int NOT NULL, Age int NOT NULL, Experience int NOT NULL, 
GMPatience int NOT NULL, CoachPatience int NOT NULL, Meddles int NOT NULL, WantsWinner int NOT NULL, SpendsMoney int NOT NULL CONSTRAINT Owner_ID PRIMARY KEY(OwnerID)"

        GetTables.CreateTable(OwnerDT, "Owners", SQLFieldNames) 'Inside CreateTable, it checks to see if a table exists or not.  If it does not, it creates one, if it does it exits.
        GetTables.DeleteTable(OwnerDT, "Owners")
        GetTables.LoadTable(OwnerDT, "Owners")
        OwnerDT.Rows.Add(0)
        For i As Integer = 1 To NumOwners
            OwnerDT.Rows.Add(i)
            If i > 32 Then : OwnerDT.Rows(i).Item("TeamID") = 0
            Else : OwnerDT.Rows(i).Item("TeamID") = i
            End If
            OwnerDT.Rows(i).Item("OwnerID") = i
            OwnerDT.Rows(i).Item("FName") = GetName("FName.txt", 90.04)
            OwnerDT.Rows(i).Item("LName") = GetName("LName.txt", 90.4832)
            OwnerDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            OwnerDT.Rows(i).Item("OwnerRep") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("Age") = MT.GenerateInt32(45, 89)
            OwnerDT.Rows(i).Item("Experience") = MT.GenerateInt32(1, 50)
            OwnerDT.Rows(i).Item("GMPatience") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("CoachPatience") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("Meddles") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("WantsWinner") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("SpendsMoney") = MT.GetGaussian(49.5, 16.5)

        Next i
        GetTables.UpdateTable(OwnerDT, "Owners")
    End Sub

    Public Sub GenGMs(ByVal NumGMs As Integer)
        Dim SQLFieldNames As String = "GMID int NOT NULL, TeamID int NOT NULL, FName varchar(20) NOT NULL, LName varchar(20) NOT NULL, College varchar(50) NOT NULL, Age int NOT NULL, Experience int NOT NULL, CoachPatience int NOT NULL, " +
"Risktaker int NOT NULL, ValuesDraftPicks int NOT NULL, ValuesCombine int NOT NULL, ValuesCharacter int NOT NULL, ValuesProduction int NOT NULL, FranchiseTag int NOT NULL, JudgingDraft int NOT NULL, JudgingFA int NOT NULL, JudgingOwn int NOT NULL, " +
"JudgingQB int NOT NULL, JudgingRB int NOT NULL, JudgingRec int NOT NULL, JudgingOL int NOT NULL, JudgingDL int NOT NULL, JudgingLB int NOT NULL, JudgingCB int NOT NULL, JudgingSF int NOT NULL, Loyalty int NOT NULL, Ego int NOT NULL, " +
"OffPhil varchar(20) NOT NULL, QBImp int NOT NULL, RBImp int NOT NULL, FBImp int NOT NULL, WRImp int NOT NULL, WR2Imp int NOT NULL, WR3Imp int NOT NULL, LTImp int NOT NULL, LGImp int NOT NULL, CImp int NOT NULL, RGImp int NOT NULL, RTImp int NOT NULL, " +
"TEImp int NOT NULL, DefPhil varchar(20) NOT NULL, DEImp int NOT NULL, DE2Imp int NOT NULL, DTImp int NOT NULL, DT2Imp int NOT NULL, NTImp int NOT NULL, MLBImp int NOT NULL, WLBImp int NOT NULL, SLBImp int NOT NULL, ROLBImp int NOT NULL, " +
"LOLBImp int NOT NULL, CB1Imp int NOT NULL, CB2Imp int NOT NULL, CB3Imp int NOT NULL, FSImp int NOT NULL, SSImp int NOT NULL, DraftStrategy varchar(20) NOT NULL, TeamBuilding varchar(20) NOT NULL,  CONSTRAINT GM_ID PRIMARY KEY(GMID)"

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
            GMDT.Rows(i).Item("FName") = GetName("Fname.txt", 90.04)
            GMDT.Rows(i).Item("LName") = GetName("LName.txt", 90.4832)
            GMDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            GMDT.Rows(i).Item("Age") = MT.GenerateInt32(35, 80)
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
    Public Sub GenCoaches(ByVal NumCoaches As Integer)

        Dim SQLFieldNames As String = "CoachID int NOT NULL, TeamID int NOT NULL, FName varchar(20) NOT NULL, LName varchar(20) NOT NULL, College varchar(50) NOT NULL, Age int NOT NULL, Experience int NOT NULL, OffPhil varchar(50) NOT NULL, 
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

            CoachDT.Rows(i).Item("FName") = GetName("Fname.txt", 90.04)
            CoachDT.Rows(i).Item("LName") = GetName("LName.txt", 90.4832)
            CoachDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            CoachDT.Rows(i).Item("Age") = MT.GenerateInt32(32, 70)
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
    Public Sub GenScouts(ByVal NumScouts As Integer)

        Dim SQLFieldNames As String = "ScoutID int Not NULL, TeamID int Not NULL, FName varchar(20) Not NULL, LName varchar(20) Not NULL, College varchar(50) Not NULL, Age int Not NULL, Experience int Not NULL, OffPhil varchar(20) Not NULL,
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

            ScoutDT.Rows(i).Item("FName") = GetName("Fname.txt", 90.04)
            ScoutDT.Rows(i).Item("LName") = GetName("LName.txt", 90.4832)
            ScoutDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            ScoutDT.Rows(i).Item("Age") = MT.GenerateInt32(32, 70)
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
    Public Sub GenDraftPlayers(ByVal NumPlayers As Integer)

        Dim SQLFieldNames As String = "DraftID int Not NULL, FName varchar(20) not NULL, LName varchar(20) Not NULL, College varchar(50) Not NULL, Age int Not NULL, CollegePOS varchar(5) Not NULL, ActualGrade decimal(5,2) Not NULL, 
ProjNFLPos varchar(5) Not NULL, Height int Not Null, Weight int Not NULL, FortyYardTime decimal(3,2) Not NULL, TwentyYardTime decimal(3,2) Not NULL, TenYardTime decimal(3,2) Not NULL, ShortShuttle decimal(3,2) Not NULL, BroadJump int Not NULL, 
VertJump decimal(3,1) Not NULL, ThreeConeDrill decimal(3,2) Not NULL, BenchPress int Not NULL, InterviewSkills int Not NULL, WonderlicTest int Not NULL, SkillsTranslateToNFL int Not NULL, QBDropQuickness decimal(3,1) NULL, 
QBSetupQuickness decimal(3,1) NULL, QBReleaseQuickness decimal(3,1) NULL, QBShortAcc decimal(3,1) NULL, QBMedAcc decimal(3,1) NULL, QBLongAcc decimal(3,1) NULL, QBDecMaking decimal(3,1) NULL, QBFieldVision decimal(3,1) NULL, QBPoise decimal(3,1) NULL, 
QBBallHandling decimal(3,1) NULL, QBTiming decimal(3,1) NULL, QBDelivery decimal(3,1) NULL, QBFollowThrough decimal(3,1) NULL, QBAvoidRush decimal(3,1) NULL, QBEscape decimal(3,1) NULL, QBScrambling decimal(3,1) NULL, QBRolloutRight decimal(3,1) NULL, 
QBRolloutLeft decimal(3,1) NULL, QBArmStrength decimal(3,1) NULL, QBZip decimal(3,1) NULL, QBTouchScreenPass decimal(3,1) NULL, QBTouchSwingPass decimal(3,1) NULL, QBEffectiveShortOut decimal(3,1) NULL, QBEffectiveGoRoute decimal(3,1) NULL, 
QBEffectivePostRoute decimal(3,1) NULL, QBEffectiveCornerRoute decimal(3,1) NULL, QBEffectiveDeepOut decimal(3,1) NULL, RBEffortBlocking decimal(3,1) NULL, RBDurability decimal(3,1) NULL, RBBallSecurity decimal(3,1) NULL, RBPassBlocking decimal(3,1) NULL, 
RBHands decimal(3,1) NULL, RBStart decimal(3,1) NULL, RBRunVision decimal(3,1) NULL, RBInsideAbility decimal(3,1) NULL, RBOutsideAbility decimal(3,1) NULL, RBElusiveAbility decimal(3,1) NULL, RBPowerAbility decimal(3,1) NULL, 
RBRunBlocking decimal(3,1) NULL, RBRouteRunning decimal(3,1) NULL, RBRunningStyle varchar(15) NULL, Athleticism decimal(3,1) NULL, QAB decimal(3,1) NULL, COD decimal(3,1) NULL, WRShortRoute decimal(3,1) NULL, WRCrowdReaction decimal(3,1) NULL, 
WRConcentration decimal(3,1) NULL, WRFieldAwareness decimal(3,1) NULL, WRBodyCatch decimal(3,1) NULL, WRBlocking decimal(3,1) NULL, WRRelease decimal(3,1) NULL, WRStart decimal(3,1) NULL, WRPatterns decimal(3,1) NULL, WRMedRoute decimal(3,1) NULL, 
WRDeepRoute decimal(3,1) NULL, WRBallAdjust decimal(3,1) NULL, WRHandCatch decimal(3,1) NULL, WRRAC decimal(3,1) NULL, WRCatchWhenHit decimal(3,1) NULL, TEGetOffLineRunBlock decimal(3,1) NULL, TEOneOnOneBlocking decimal(3,1) NULL, 
TEDoubleTeamBlocking decimal(3,1) NULL, TEDownBlocking decimal(3,1) NULL, TETurnAndWallBlocking decimal(3,1) NULL, TEsustainBlocking decimal(3,1) NULL, TECrowdReaction decimal(3,1) NULL, TECatchWhenHit decimal(3,1) NULL, TEConcentration decimal(3,1) NULL,
TEBodyCatch decimal(3,1) NULL, TEFieldAwareness decimal(3,1) NULL, TEPassProtect decimal(3,1) NULL, TEDriveIntoPassRoute decimal(3,1) NULL, TEPatterns decimal(3,1) NULL, TEShortRoute decimal(3,1) NULL, TEMedRoute decimal(3,1) NULL, 
TEDeepRoute decimal(3,1) NULL, TEBallAdjust decimal(3,1) NULL, TEHandCatch decimal(3,1) NULL, TERAC decimal(3,1) NULL, OLGetOutside decimal(3,1) NULL, OLReachBlock decimal(3,1) NULL, OLTurnDefender decimal(3,1) NULL, OLPulling decimal(3,1) NULL, 
OLTrapBlock decimal(3,1) NULL, OL2ndLevelPull decimal(3,1) NULL, OLAdjusttoLB decimal(3,1) NULL, OLSlide decimal(3,1) NULL, OLHandUse decimal(3,1) NULL, OLHandPop decimal(3,1) NULL, OLLongSnapPotential decimal(3,1) NULL, 
OLGetOffLineRunBlock decimal(3,1) NULL, OLOneOnOneBlocking decimal(3,1) NULL, OLDriveBlocking decimal(3,1) NULL, OLDownBlocking decimal(3,1) NULL, OLSustainBlock decimal(3,1) NULL, OLPassBlocking decimal(3,1) NULL, OLPassDrops decimal(3,1) NULL, 
OLFeetSetup decimal(3,1) NULL, OLAnchorAbility decimal(3,1) NULL, OLRecover decimal(3,1) NULL, OLStrength decimal(3,1) NULL, DLStyle varchar(15) NULL, DLRunAtHim decimal(3,1) NULL, DLTackling decimal(3,1) NULL, DLAgainstTrapAbility decimal(3,1) NULL, 
DLSlideAbility decimal(3,1) NULL, DLRunPursuit decimal(3,1) NULL, DLPassRushTechnique varchar(15) NULL, DLHandUse decimal(3,1) NULL, DLShedVsRunAway decimal(3,1) NULL, DLShedRunBlock decimal(3,1) NULL, DLTackleVsRunAway decimal(3,1) NULL, 
DLChangeDirection decimal(3,1) NULL, DLReleaseOffBall decimal(3,1) NULL, DLOneOnOneAbility decimal(3,1) NULL, DLDoubleTeamAbility decimal(3,1) NULL, DLDefeatBlock decimal(3,1) NULL, DLFirstStepPassRush decimal(3,1) NULL, DLShedPassBlock decimal(3,1) NULL,
DLBurst decimal(3,1) NULL, DLPressure decimal(3,1) NULL, DLFinish decimal(3,1) NULL, LBDropDepth decimal(3,1) NULL, LBCoverage decimal(3,1) NULL, LBHands decimal(3,1) NULL, LBBlitz decimal(3,1) NULL, LBPassRushType varchar(15) NULL, 
LBRead decimal(3,1) NULL, LBInstincts decimal(3,1) NULL, LBDefeatBlocks decimal(3,1) NULL, LBShedBlocks decimal(3,1) NULL, LBInsideTackle decimal(3,1) NULL, LBOutsideTackle decimal(3,1) NULL, DBPressBailCoverage decimal(3,1) NULL, 
DBBallReaction decimal(3,1) NULL, DBTackling decimal(3,1) NULL, DBZoneCoverage decimal(3,1) NULL, DBRuncontain decimal(3,1) NULL, DBWardOffBlockers decimal(3,1) NULL, DBRunTackling decimal(3,1) NULL, DBHands decimal(3,1) NULL,
DBBump decimal(3,1) NULL, DDBRunContain decimal(3,1) NULL, DBBlitz decimal(3,1) NULL, DBRead decimal(3,1) NULL, DBInstincts decimal(3,1) NULL, DBBackpedal decimal(3,1) NULL, DBTurn decimal(3,1) NULL, DBClose decimal(3,1) NULL, DBRange decimal(3,1) NULL, 
DBCatchUpSpeed decimal(3,1) NULL, DBManToManCoverage decimal(3,1) NULL, DBBurst decimal(3,1) NULL, DBCOBP decimal(3,1) NULL, DBFeet decimal(3,1) NULL, KPlantRelationship varchar(15) NULL, KApproachAngle varchar(15) NULL, KBallFlight varchar(15) NULL, 
KSteppingPattern varchar(15) NULL, KKickingStyle varchar(15) NULL, KHandlingWind decimal(3,1) NULL, KTackling decimal(3,1) NULL, KRunAndPassAbility decimal(3,1) NULL, KKOFootSpeed decimal(3,1) NULL, KFGOperationTimes decimal(3,1) NULL, 
KAccuracy decimal(3,1) NULL, KHandlingPressure decimal(3,1) NULL, KFootSpeed decimal(3,1) NULL, KKickQuickness decimal(3,1) NULL, KKickRise decimal(3,1) NULL, KKOProduction decimal(3,1) NULL, KKOMentalStability decimal(3,1) NULL, 
PFootSpeed decimal(3,1) NULL, PApproachLine decimal(3,1) NULL, PHandlingTime decimal(3,1) NULL, PSteppingPattern varchar(15) NULL, PHands decimal(3,1) NULL, PTackling decimal(3,1) NULL, PRunAndPassAbility decimal(3,1) NULL, PDistance decimal(3,1) NULL, 
PHangTime decimal(3,1) NULL, PPressureKicking decimal(3,1) NULL, PBlockZone decimal(3,1) NULL, PHandToFootTime decimal(3,1) NULL, PTiming decimal(3,1) NULL, Flexibility decimal(3,1) NULL, Clutch decimal(3,1) NULL, Production decimal(3,1) NULL, 
Consistency decimal(3,1) NULL, TeamPlayer decimal(3,1) NULL, Instincts decimal(3,1) NULL, Focus decimal(3,1) NULL, PlayStrength decimal(3,1) NULL, Durability decimal(3,1) NULL, Explosion decimal(3,1) NULL, DeliversBlow decimal(3,1) NULL,
Leadership decimal(3,1) NULL, Character decimal(3,1) NULL CONSTRAINT Draft_ID PRIMARY KEY(DraftID)"

        GetTables.CreateTable(DraftDT, "DraftPlayers", SQLFieldNames)
        GetTables.DeleteTable(DraftDT, "DraftPlayers")
        GetTables.LoadTable(DraftDT, "DraftPlayers")
        DraftDT.Rows.Add(0)

        For i As Integer = 1 To NumPlayers
            DraftDT.Rows.Add(i)
            DraftDT.Rows(i).Item("FName") = GetName("Fname.txt", 90.04)
            DraftDT.Rows(i).Item("LName") = GetName("Lname.txt", 90.4832)
            DraftDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            DraftDT.Rows(i).Item("Age") = GetDraftAge()
            DraftDT.Rows(i).Item("CollegePOS") = GetCollegePos()
            GetDraftGrades(i, DraftDT.Rows(i).Item("CollegePos")) '#### TODO---> Convert from a sub to a function since it's returning a value
            DraftDT.Rows(i).Item("Height") = GetHeight(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("Weight") = GetWeight(DraftDT.Rows(i).Item("CollegePOS"), DraftDT.Rows(i).Item("Height"))
            'DraftDT.Rows(i).Item("ArmLength")=
            'DraftDT.Rows(i).Item("HandLength")=
            DraftDT.Rows(i).Item("FortyYardTime") = Get40Time(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("TwentyYardTime") = Get20Time(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("TenYardTime") = Get10Time(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("ShortShuttle") = GetShortShuttle(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("BroadJump") = GetBroadJump(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("VertJump") = GetVertJump(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("ThreeConeDrill") = Get3Cone(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("BenchPress") = GetBenchPress(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("InterviewSkills") = CInt(MT.GetGaussian(49.5, 16.5))
            DraftDT.Rows(i).Item("WonderlicTest") = GetWonderlic(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("SkillsTranslateToNFL") = GetSkillsTranslate(DraftDT.Rows(i).Item("CollegePOS"))
            DraftDT.Rows(i).Item("ProjNFLPos") = GetNFLPos(DraftDT.Rows(i).Item("CollegePOS"))
        Next i

        GetTables.UpdateTable(DraftDT, "DraftPlayers")
    End Sub
    Public Sub GetRosterPlayers(ByVal numplayers As Integer)

        Dim SQLFieldNames As String = "PlayerID int Not NULL, TeamID int NULL, FName varchar(20) Not NULL, LName varchar(20) Not NULL, College varchar(50) Not NULL, CollegePOS varchar(5) Not NULL, DOB nvarchar(12) not NULL, Age int Not NULL,
Height int Not NULL, Weight int Not NULL, QBDropQuickness int NULL, QBSetUpQuickness int NULL, QBReleaseQuickness int NULL, QBShortAcc int NULL,  QBMedAcc int NULL, QBLongAcc int NULL, QBDecMaking int NULL, QBFieldVision int NULL, QBPoise int NULL, 
QBBallHandling int NULL, QBTiming int NULL, QBDeliveryQuickness int NULL, QBAvoidRush int NULL, QBScrambling int NULL, QBRolloutRight int NULL, QBRolloutLeft int NULL, QBArmStrength int NULL, QBTouch int NULL, RBRunVision int NULL, 
RBInsideAbility int NULL, RBOutsideAbility int NULL, RBElusiveAbility int NULL, RBPowerAbility int NULL, RBRouteRunning int NULL, RBRunningStyle varchar(20) Not NULL, ORunAfterCatch int NULL, OReleasePassRoute int NULL, OShortRoute int NULL, 
OMedRoute int NULL, ODeepRoute int NULL, OScreenRoute int NULL, OSwingRoute int NULL, ORunBlock int NULL, OPassBlock int NULL, OSustainBlock int NULL, OLDriveBlocking int NULL, OLPulling int NULL, OLInSpace int NULL, OLHandUse int NULL, 
OLAnchorAbility int NULL, OLRecover int NULL, OLLongSnapPot int NULL, DLOneOnOneAbility int NULL, DLStyle varchar(20)  NULL, DLDoubleTeamAbility int NULL, DLRunAtHim int NULL, DLHandUse int NULL, DDefeatBlock int NULL, DShedBlock int NULL, 
DBallPursuit int NULL, DReadPlay int NULL, DMantoManCoverage int NULL, DZoneCoverage int NULL, DBlitzAbility int NULL, LBDropDepth int NULL, LBFillGaps int NULL, DBTurnAndRun int NULL, DBBreakOnBall int NULL, DBClosingSpeed int NULL,
DBPressCoverage int NULL, DBRunContain int NULL, KickAccuracy int NULL, KickQuickness int NULL, KickStrength int NULL, KickHandlingwind int NULL, KickRunningAbility int NULL, KickPassingAbility int NULL, PuntHangTime int NULL, Strength int NULL, 
Speed int NULL, Acceleration int NULL, Agility int NULL, Intelligence int NULL, Explosion int NULL, Athleticism int NULL, JumpingAbility int NULL, ChangeDirections int NULL, Character int NULL, Durability int NULL, Instincts int NULL, Focus int NULL, 
TeamPlayer int NULL, Consistency int NULL, Leadership int NULL, FieldAwareness int NULL, Clutch int NULL, Fearless int NULL, Aggressive int NULL, RiskTaker int NULL, FilmStudy int NULL, WorkEthic int NULL, BallSecurity int NULL, HandCatch int NULL, 
Bodycatch int NULL, CatchWhenHit int NULL, AdjustToBall int NULL, Tackling int NULL, RETKickReturn int NULL, RETPuntReturn int NULL, STCoverage int NULL, STWillingness int NULL, STAssignment int NULL, STDiscipline int NULL, LSSnapTime int NULL,
LSSnapAccuracy int NULL CONSTRAINT PID PRIMARY KEY(PlayerID)"

        GetTables.CreateTable(PlayerDT, "Players", SQLFieldNames)
        GetTables.DeleteTable(PlayerDT, "Players")
        GetTables.LoadTable(PlayerDT, "Players")
        PlayerDT.Rows.Add(0)

        For i As Integer = 1 To numplayers
            PlayerDT.Rows.Add(i)

            PlayerDT.Rows(i).Item("FName") = GetName("Fname.txt", 90.04)
            PlayerDT.Rows(i).Item("LName") = GetName("Lname.txt", 90.4832)
            PlayerDT.Rows(i).Item("College") = GetName("Colleges.txt", 2182)
            PlayerDT.Rows(i).Item("CollegePOS") = GetCollegePos()
            PlayerDT.Rows(i).Item("Age") = GetPlayerAge(PlayerDT.Rows(i).Item("CollegePOS"))
            PlayerDT.Rows(i).Item("Height") = GetHeight(PlayerDT.Rows(i).Item("CollegePOS"))
            PlayerDT.Rows(i).Item("Weight") = GetWeight(PlayerDT.Rows(i).Item("CollegePOS"), PlayerDT.Rows(i).Item("Height"))
            GetPosSkills(PlayerDT.Rows(i).Item("CollegePos"), i, PlayerDT)
            PlayerDT.Rows(i).Item("Strength") = GetBenchPress(PlayerDT.Rows(i).Item("CollegePos"))
            PlayerDT.Rows(i).Item("Speed") = Get40Time(PlayerDT.Rows(i).Item("CollegePos"))
            PlayerDT.Rows(i).Item("Acceleration") = Get10Time(PlayerDT.Rows(i).Item("CollegePos"))
            PlayerDT.Rows(i).Item("Agility") = Get3Cone(PlayerDT.Rows(i).Item("CollegePos"))
            PlayerDT.Rows(i).Item("Intelligence") = GetWonderlic(PlayerDT.Rows(i).Item("CollegePos"))
            PlayerDT.Rows(i).Item("Explosion") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Athleticism") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("JumpingAbility") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("ChangeDirections") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Character") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Durability") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Instincts") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Focus") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("TeamPlayer") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Consistency") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Leadership") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("FieldAwareness") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Clutch") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Fearless") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Aggressive") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("RiskTaker") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("FilmStudy") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("WorkEthic") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("BallSecurity") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("HandCatch") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("BodyCatch") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("CatchWhenHit") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("AdjustToBall") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("Tackling") = MT.GetGaussian(49.5, 16.5)
            PlayerDT.Rows(i).Item("RETKickReturn") = GetKickRetAbility(PlayerDT.Rows(i).Item("CollegePOS"), i)
            PlayerDT.Rows(i).Item("RETPuntReturn") = GetPuntRetAbility(PlayerDT.Rows(i).Item("CollegePOS"), i)
            PlayerDT.Rows(i).Item("DOB") = GetDOB(PlayerDT.Rows(i).Item("Age"), i)
            'Console.WriteLine(PlayerDT.Rows(i).Item("DOB"))
            GetSTAbility(PlayerDT.Rows(i).Item("CollegePOS"), i)
            GetLSAbility(PlayerDT.Rows(i).Item("CollegePOS"), i)
        Next i

        For i As Integer = 1 To 17
            PutPlayerOnTeam(i)
        Next i

        For i As Integer = 0 To PlayerDT.Rows.Count - 1
            For col As Integer = 0 To PlayerDT.Columns.Count - 1
                If PlayerDT.Rows(i).Item(col) Is DBNull.Value Then
                    PlayerDT.Rows(i).Item(col) = 0
                End If
            Next col
        Next i

        PlayerDT.Rows(0).Delete()

        GetTables.UpdateTable(PlayerDT, "Players")
    End Sub
    Public Function GetArmLength(ByVal Pos As String)


    End Function
    Private Sub PutPlayerOnTeam(ByVal Pos As Integer)
        'determines what team player is on via generic position limits

        Dim NumAllowed As Integer
        Dim MinAllowed As Integer
        Dim SelectTeam As Integer
        Dim Count As Integer
        Dim i As Integer
        Dim PosString As String
        Dim RowArray As New ArrayList

        i = 1
        Count = 0
        NumAllowed = 0

        Select Case Pos
            Case 1, 2, 5, 7 : NumAllowed = 4 : MinAllowed = 3 '1 QB, 2 RB, 5 TE
            Case 6, 12 : NumAllowed = 3 : MinAllowed = 2 '6 C, 12 ILB
            Case 8, 9, 10, 11 : NumAllowed = 5 : MinAllowed = 3 '7 OG, 8 OT, 9 DE, 10 DT, 11 OLB
            Case 14, 15 : NumAllowed = 4 : MinAllowed = 2 '14 FS, 15 SS
            Case 3 : NumAllowed = 2 : MinAllowed = 1 '16 K, 17 P, 3 FB
            Case 16, 17 : NumAllowed = 1 : MinAllowed = 1
            Case 4, 13 : NumAllowed = 6 : MinAllowed = 4 '4 WR, 13 CB
        End Select

        Select Case Pos
            Case 1 : PosString = "QB"
            Case 2 : PosString = "RB"
            Case 3 : PosString = "FB"
            Case 4 : PosString = "WR"
            Case 5 : PosString = "TE"
            Case 6 : PosString = "C"
            Case 7 : PosString = "OG"
            Case 8 : PosString = "OT"
            Case 9 : PosString = "DE"
            Case 10 : PosString = "DE"
            Case 11 : PosString = "OLB"
            Case 12 : PosString = "ILB"
            Case 13 : PosString = "CB"
            Case 14 : PosString = "FS"
            Case 15 : PosString = "SS"
            Case 16 : PosString = "K"
            Case 17 : PosString = "P"
        End Select

        For x As Integer = 1 To PlayerDT.Rows.Count - 1
            If PlayerDT.Rows(x).Item("CollegePos") = PosString Then
                RowArray.Add(x) 'adds all rows to an arraylist
            End If
        Next x

        For i = 1 To 32 'numteams
            Dim getnumpos As Integer = MT.GenerateInt32(MinAllowed, NumAllowed)

            For n As Integer = 1 To getnumpos
                If RowArray.Count > 0 Then
                    Dim ChooseArray As Integer = MT.GenerateInt32(0, RowArray.Count - 1)
                    Dim GetRow As Integer = RowArray.Item(ChooseArray)
                    RowArray.RemoveAt(ChooseArray)
                    PlayerDT.Rows(GetRow).Item("TeamID") = i
                End If
            Next n

        Next i

    End Sub
    Public Sub GenScoutGrades(ByVal NumScouts As Integer, ByVal NumPlayers As Integer)

        'GetTables.LoadTable(ScoutGradeDT, "ScoutGrades")
        'If ScoutGradeDT.Rows.Count = 0 Then
        'Dim SQLFields As String = "(PID INTEGER PRIMARY KEY,"

        'For ScoutID As Integer = 0 To NumScouts
        'If ScoutID <> NumScouts Then
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2),"
        'Else
        'SQLFields += "Scout" & ScoutID & " Decimal(9,2));"
        'End If
        'Next ScoutID

        GetTables.DeleteTable(ScoutGradeDT, "ScoutsGrade")
        GetTables.LoadTable(ScoutGradeDT, "ScoutsGrade")

        ScoutGradeDT.Rows.Add(0)

        For PlayerId As Integer = 1 To NumPlayers
            ScoutGradeDT.Rows.Add(PlayerId)
        Next PlayerId

        Eval.ScoutPlayerEval()
        'End If
    End Sub
    Private Function GetDraftAge() As Integer
        Dim i As Integer = MT.GenerateInt32(1, 100)
        Select Case i
            Case 1 To 82
                Return 22
            Case 83 To 92
                Return 21
            Case 93 To 96
                Return 23
            Case 97
                Return 20
            Case 98 To 99
                Return 24
            Case 100
                Return 25
        End Select

    End Function
    Private Function GetPlayerAge(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 97)
                    Case 1 To 2 : Return 21
                    Case 3 To 5 : Return 22
                    Case 6 To 11 : Return 23
                    Case 12 To 25 : Return 24
                    Case 26 To 35 : Return 25
                    Case 36 To 47 : Return 26
                    Case 48 To 54 : Return 27
                    Case 55 To 62 : Return 28
                    Case 63 To 71 : Return 29
                    Case 72 To 76 : Return 30
                    Case 77 To 78 : Return 31
                    Case 79 To 84 : Return 32
                    Case 85 To 87 : Return 33
                    Case 88 To 90 : Return 34
                    Case 91 : Return 35
                    Case 92 : Return 36
                    Case 93 To 94 : Return 37
                    Case 95 : Return 38
                    Case 96 To 97 : Return 39
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 128)
                    Case 1 To 2 : Return 21
                    Case 3 To 18 : Return 22
                    Case 19 To 31 : Return 23
                    Case 32 To 56 : Return 24
                    Case 57 To 71 : Return 25
                    Case 72 To 89 : Return 26
                    Case 90 To 99 : Return 27
                    Case 100 To 106 : Return 28
                    Case 107 To 112 : Return 29
                    Case 113 To 121 : Return 30
                    Case 122 To 124 : Return 31
                    Case 125 To 126 : Return 32
                    Case 127 To 128 : Return 33
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 39)
                    Case 1 To 5 : Return 23
                    Case 6 To 9 : Return 24
                    Case 10 To 11 : Return 25
                    Case 12 To 15 : Return 26
                    Case 16 To 19 : Return 27
                    Case 20 To 23 : Return 28
                    Case 24 To 31 : Return 29
                    Case 32 To 33 : Return 30
                    Case 34 To 35 : Return 31
                    Case 36 To 37 : Return 32
                    Case 38 To 39
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Return 33
                            Case 2 : Return 34
                            Case 3 : Return 35
                            Case 4 : Return 36
                            Case 5 : Return 37
                        End Select
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 4 : Return 21
                    Case 5 To 23 : Return 22
                    Case 24 To 48 : Return 23
                    Case 49 To 75 : Return 24
                    Case 76 To 98 : Return 25
                    Case 99 To 121 : Return 26
                    Case 122 To 135 : Return 27
                    Case 136 To 157 : Return 28
                    Case 158 To 162 : Return 29
                    Case 163 To 174 : Return 30
                    Case 175 To 179 : Return 31
                    Case 180 To 182 : Return 32
                    Case 183 To 188 : Return 33
                    Case 189 : Return 34
                    Case 190 To 191 : Return 35
                    Case 192 To 194 : Return 36
                    Case 195 : Return 37
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 115)
                    Case 1 To 9 : Return 22
                    Case 10 To 17 : Return 23
                    Case 18 To 33 : Return 24
                    Case 34 To 50 : Return 25
                    Case 51 To 63 : Return 26
                    Case 64 To 72 : Return 27
                    Case 73 To 79 : Return 28
                    Case 80 To 92 : Return 29
                    Case 93 To 100 : Return 30
                    Case 101 To 103 : Return 31
                    Case 104 To 110 : Return 32
                    Case 111 To 115 : Return 33
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 90)
                    Case 1 : Return 22
                    Case 2 To 9 : Return 23
                    Case 10 To 17 : Return 24
                    Case 18 To 23 : Return 25
                    Case 24 To 34 : Return 26
                    Case 35 To 45 : Return 27
                    Case 46 To 53 : Return 28
                    Case 54 To 60 : Return 29
                    Case 61 To 67 : Return 30
                    Case 68 To 69 : Return 31
                    Case 70 To 81 : Return 32
                    Case 82 : Return 33
                    Case 83 To 85 : Return 34
                    Case 86 : Return 35
                    Case 87 : Return 36
                    Case 88 : Return 37
                    Case 89 To 90 : Return 38
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 4 : Return 22
                    Case 5 To 15 : Return 23
                    Case 16 To 26 : Return 24
                    Case 27 To 36 : Return 25
                    Case 37 To 50 : Return 26
                    Case 51 To 66 : Return 27
                    Case 67 To 72 : Return 28
                    Case 73 To 81 : Return 29
                    Case 82 To 88 : Return 30
                    Case 89 To 93 : Return 31
                    Case 94 To 97 : Return 32
                    Case 98 To 100 : Return 33
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 143)
                    Case 1 : Return 21
                    Case 2 To 5 : Return 22
                    Case 6 To 25 : Return 23
                    Case 26 To 45 : Return 24
                    Case 46 To 67 : Return 25
                    Case 68 To 88 : Return 26
                    Case 89 To 95 : Return 27
                    Case 96 To 104 : Return 28
                    Case 105 To 115 : Return 29
                    Case 116 To 126 : Return 30
                    Case 127 To 129 : Return 31
                    Case 130 To 132 : Return 32
                    Case 133 To 137 : Return 33
                    Case 138 To 141 : Return 34
                    Case 142 : Return 35
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 155)
                    Case 1 To 4 : Return 21
                    Case 5 To 13 : Return 22
                    Case 14 To 35 : Return 23
                    Case 36 To 53 : Return 24
                    Case 54 To 70 : Return 25
                    Case 71 To 87 : Return 26
                    Case 88 To 98 : Return 27
                    Case 99 To 109 : Return 28
                    Case 110 To 118 : Return 29
                    Case 119 To 128 : Return 30
                    Case 129 To 134 : Return 31
                    Case 135 To 142 : Return 32
                    Case 143 To 148 : Return 33
                    Case 149 To 151 : Return 34
                    Case 152 To 154 : Return 35
                    Case 155 : Return 36
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 150)
                    Case 1 To 9 : Return 22
                    Case 10 To 26 : Return 23
                    Case 27 To 47 : Return 24
                    Case 48 To 67 : Return 25
                    Case 68 To 87 : Return 26
                    Case 88 To 99 : Return 27
                    Case 100 To 109 : Return 28
                    Case 110 To 120 : Return 29
                    Case 121 To 131 : Return 30
                    Case 132 To 137 : Return 31
                    Case 138 To 142 : Return 32
                    Case 143 To 145 : Return 33
                    Case 146 To 147 : Return 34
                    Case 148 To 150
                        Select Case MT.GenerateInt32(1, 5)
                            Case 1 : Return 35
                            Case 2 : Return 36
                            Case 3 : Return 37
                            Case 4 : Return 38
                            Case 5 : Return 39
                        End Select
                End Select
            Case "OLB", "ILB"
                Select Case MT.GenerateInt32(1, 238)
                    Case 1 To 15 : Return 22
                    Case 16 To 45 : Return 23
                    Case 46 To 80 : Return 24
                    Case 81 To 114 : Return 25
                    Case 115 To 143 : Return 26
                    Case 144 To 166 : Return 27
                    Case 167 To 182 : Return 28
                    Case 183 To 197 : Return 29
                    Case 198 To 210 : Return 30
                    Case 211 To 217 : Return 31
                    Case 218 To 228 : Return 32
                    Case 229 To 232 : Return 33
                    Case 233 To 236 : Return 34
                    Case 237 To 238 : Return 35
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 195)
                    Case 1 To 5 : Return 21
                    Case 6 To 23 : Return 22
                    Case 24 To 54 : Return 23
                    Case 55 To 87 : Return 24
                    Case 88 To 109 : Return 25
                    Case 110 To 133 : Return 26
                    Case 134 To 147 : Return 27
                    Case 148 To 163 : Return 28
                    Case 164 To 167 : Return 29
                    Case 168 To 176 : Return 30
                    Case 177 To 181 : Return 31
                    Case 182 To 186 : Return 32
                    Case 187 To 189 : Return 33
                    Case 190 To 193 : Return 34
                    Case 194 To 195 : Return 35
                End Select
            Case "FS", "SS"
                Select Case MT.GenerateInt32(1, 138)
                    Case 1 To 10 : Return 22
                    Case 11 To 18 : Return 23
                    Case 19 To 42 : Return 24
                    Case 43 To 57 : Return 25
                    Case 58 To 86 : Return 26
                    Case 87 To 101 : Return 27
                    Case 102 To 110 : Return 28
                    Case 111 To 119 : Return 29
                    Case 120 To 125 : Return 30
                    Case 126 To 130 : Return 31
                    Case 132 To 133 : Return 32
                    Case 134 To 135 : Return 33
                    Case 136 : Return 34
                    Case 137 To 138 : Return 35
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 37)
                    Case 1 To 2 : Return 22
                    Case 3 To 5 : Return 23
                    Case 6 To 7 : Return 24
                    Case 8 To 10 : Return 25
                    Case 11 To 12 : Return 26
                    Case 13 To 14 : Return 27
                    Case 15 To 16 : Return 28
                    Case 17 : Return 29
                    Case 18 : Return 30
                    Case 19 To 22 : Return 31
                    Case 23 To 24 : Return 32
                    Case 25 To 27 : Return 33
                    Case 28 To 29 : Return 34
                    Case 30 : Return 35
                    Case 31 To 33 : Return 36
                    Case 37 : Return 1
                    Case 38 : Return 1
                    Case 39 : Return 1
                    Case 40 : Return 1
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 34)
                    Case 1 : Return 22
                    Case 2 To 3 : Return 23
                    Case 4 : Return 24
                    Case 5 To 7 : Return 25
                    Case 8 To 9 : Return 26
                    Case 10 To 13 : Return 27
                    Case 14 To 16 : Return 28
                    Case 17 To 19 : Return 29
                    Case 20 To 21 : Return 30
                    Case 22 To 23 : Return 31
                    Case 24 To 25 : Return 32
                    Case 26 To 27 : Return 33
                    Case 28 : Return 34
                    Case 29 : Return 35
                    Case 30 : Return 36
                    Case 31 : Return 37
                    Case 32 : Return 38
                    Case 33 : Return 39
                    Case 34 : Return 40
                End Select
        End Select

    End Function
    Private Function GetWeight(ByVal Pos As String, ByVal Height As Integer) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(3.0, 0.16) * Height)
            Case "RB" : Return CInt(MT.GetGaussian(3.07, 0.16) * Height)
            Case "FB" : Return CInt(MT.GetGaussian(3.39, 0.13) * Height)
            Case "WR" : Return CInt(MT.GetGaussian(2.74, 0.15) * Height)
            Case "TE" : Return CInt(MT.GetGaussian(3.37, 0.14) * Height)
            Case "OT" : Return CInt(MT.GetGaussian(4.07, 0.17) * Height)
            Case "OG" : Return CInt(MT.GetGaussian(4.13, 0.18) * Height)
            Case "C" : Return CInt(MT.GetGaussian(4.01, 0.16) * Height)
            Case "DE" : Return CInt(MT.GetGaussian(3.61, 0.22) * Height)
            Case "DT" : Return CInt(MT.GetGaussian(4.09, 0.22) * Height)
            Case "NT" : Return CInt(MT.GetGaussian(4.29, 0.27) * Height)
            Case "LB" : Return CInt(MT.GetGaussian(3.26, 0.14) * Height)
            Case "OLB" : Return CInt(MT.GetGaussian(3.29, 0.15) * Height)
            Case "ILB" : Return CInt(MT.GetGaussian(3.3, 0.11) * Height)
            Case "CB" : Return CInt(MT.GetGaussian(2.7, 0.11) * Height)
            Case "DB" : Return CInt(MT.GetGaussian(2.77, 0.15) * Height)
            Case "SS" : Return CInt(MT.GetGaussian(2.92, 0.06) * Height)
            Case "FS" : Return CInt(MT.GetGaussian(2.83, 0.11) * Height)
            Case "K" : Return CInt(MT.GetGaussian(2.81, 0.21) * Height)
            Case "P" : Return CInt(MT.GetGaussian(2.9, 0.21) * Height)
        End Select

    End Function
    Private Function GetHeight(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 141)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 : Return 70
                    Case 4 : Return 71
                    Case 5 To 12 : Return 72
                    Case 13 To 29 : Return 73
                    Case 30 To 68 : Return 74
                    Case 69 To 93 : Return 75
                    Case 94 To 119 : Return 76
                    Case 120 To 138 : Return 77
                    Case 139 To 141 : Return 78
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 To 2 : Return 66
                    Case 3 To 6 : Return 67
                    Case 7 To 14 : Return 68
                    Case 15 To 41 : Return 69
                    Case 42 To 88 : Return 70
                    Case 89 To 125 : Return 71
                    Case 126 To 155 : Return 72
                    Case 156 To 176 : Return 73
                    Case 177 To 181 : Return 74
                    Case 182 : Return 75
                    Case 183 : Return 76
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 92)
                    Case 1 : Return 69
                    Case 2 To 6 : Return 70
                    Case 7 To 25 : Return 71
                    Case 26 To 54 : Return 72
                    Case 55 To 70 : Return 73
                    Case 71 To 84 : Return 74
                    Case 85 To 88 : Return 75
                    Case 89 To 92 : Return 76
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 375)
                    Case 1 : Return 67
                    Case 2 To 11 : Return 68
                    Case 12 To 27 : Return 69
                    Case 28 To 65 : Return 70
                    Case 66 To 112 : Return 71
                    Case 113 To 174 : Return 72
                    Case 175 To 233 : Return 73
                    Case 234 To 286 : Return 74
                    Case 287 To 329 : Return 75
                    Case 330 To 358 : Return 76
                    Case 359 To 370 : Return 77
                    Case 371 To 375 : Return 78
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 182)
                    Case 1 : Return 72
                    Case 2 To 5 : Return 73
                    Case 6 To 16 : Return 74
                    Case 17 To 52 : Return 75
                    Case 53 To 104 : Return 76
                    Case 105 To 147 : Return 77
                    Case 148 To 170 : Return 78
                    Case 171 To 177 : Return 79
                    Case 178 To 182 : Return 80
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 213)
                    Case 1 : Return 74
                    Case 2 To 14 : Return 75
                    Case 15 To 54 : Return 76
                    Case 55 To 106 : Return 77
                    Case 107 To 158 : Return 78
                    Case 159 To 195 : Return 79
                    Case 196 To 210 : Return 80
                    Case 211 To 213 : Return 81
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 183)
                    Case 1 : Return 73
                    Case 2 To 21 : Return 74
                    Case 22 To 69 : Return 75
                    Case 70 To 125 : Return 76
                    Case 126 To 162 : Return 77
                    Case 163 To 175 : Return 78
                    Case 176 To 182 : Return 79
                    Case 183 : Return 80
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 82)
                    Case 1 : Return 72
                    Case 2 To 5 : Return 73
                    Case 6 To 21 : Return 74
                    Case 22 To 48 : Return 75
                    Case 49 To 70 : Return 76
                    Case 71 To 81 : Return 77
                    Case 82 : Return 78
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 231)
                    Case 1 : Return 71
                    Case 2 : Return 72
                    Case 3 To 10 : Return 73
                    Case 11 To 42 : Return 74
                    Case 43 To 98 : Return 75
                    Case 99 To 159 : Return 76
                    Case 160 To 202 : Return 77
                    Case 203 To 225 : Return 78
                    Case 226 To 231 : Return 79
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 194)
                    Case 1 To 2 : Return 71
                    Case 3 To 13 : Return 72
                    Case 14 To 34 : Return 73
                    Case 35 To 77 : Return 74
                    Case 78 To 132 : Return 75
                    Case 133 To 167 : Return 76
                    Case 168 To 183 : Return 77
                    Case 184 To 191 : Return 78
                    Case 192 To 194 : Return 79
                End Select
            Case "NT"
                Select Case MT.GenerateInt32(1, 26)
                    Case 1 To 3 : Return 72
                    Case 4 To 8 : Return 73
                    Case 9 To 16 : Return 74
                    Case 17 To 20 : Return 75
                    Case 21 To 24 : Return 76
                    Case 25 : Return 77
                    Case 26 : Return 78
                End Select
            Case "LB"
                Select Case MT.GenerateInt32(1, 227)
                    Case 1 : Return 69
                    Case 2 To 6 : Return 70
                    Case 7 To 14 : Return 71
                    Case 9 To 51 : Return 72
                    Case 52 To 111 : Return 73
                    Case 112 To 161 : Return 74
                    Case 162 To 209 : Return 75
                    Case 210 To 218 : Return 76
                    Case 219 To 226 : Return 77
                    Case 227 : Return 78
                End Select
            Case "OLB"
                Select Case MT.GenerateInt32(1, 83)
                    Case 1 To 3 : Return 71
                    Case 4 To 19 : Return 72
                    Case 20 To 30 : Return 73
                    Case 31 To 46 : Return 74
                    Case 47 To 66 : Return 75
                    Case 67 To 77 : Return 76
                    Case 78 To 82 : Return 77
                    Case 83 : Return 78
                End Select
            Case "ILB"
                Select Case MT.GenerateInt32(1, 48)
                    Case 1 : Return 70
                    Case 2 To 3 : Return 71
                    Case 4 To 8 : Return 72
                    Case 9 To 25 : Return 73
                    Case 26 To 42 : Return 74
                    Case 43 To 46 : Return 75
                    Case 47 To 48 : Return 76
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 173)
                    Case 1 To 5 : Return 68
                    Case 6 To 21 : Return 69
                    Case 22 To 66 : Return 70
                    Case 67 To 109 : Return 71
                    Case 110 To 140 : Return 72
                    Case 141 To 166 : Return 73
                    Case 167 To 171 : Return 74
                    Case 172 : Return 75
                    Case 173 : Return 76
                End Select
            Case "DB"
                Select Case MT.GenerateInt32(1, 222)
                    Case 1 To 4 : Return 68
                    Case 5 To 19 : Return 69
                    Case 20 To 58 : Return 70
                    Case 59 To 104 : Return 71
                    Case 105 To 139 : Return 72
                    Case 140 To 188 : Return 73
                    Case 189 To 210 : Return 74
                    Case 211 To 222 : Return 75
                End Select
            Case "SS"
                Select Case MT.GenerateInt32(1, 47)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 8 : Return 70
                    Case 9 To 13 : Return 71
                    Case 14 To 29 : Return 72
                    Case 30 To 40 : Return 73
                    Case 41 To 45 : Return 74
                    Case 46 To 47 : Return 75
                End Select
            Case "FS"
                Select Case MT.GenerateInt32(1, 49)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 7 : Return 70
                    Case 8 To 21 : Return 71
                    Case 22 To 30 : Return 72
                    Case 31 To 36 : Return 73
                    Case 37 To 46 : Return 74
                    Case 47 : Return 75
                    Case 48 : Return 76
                    Case 49 : Return 77
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 53)
                    Case 1 : Return 68
                    Case 2 To 4 : Return 69
                    Case 5 To 10 : Return 70
                    Case 11 To 19 : Return 71
                    Case 20 To 33 : Return 72
                    Case 34 To 42 : Return 73
                    Case 43 To 49 : Return 74
                    Case 50 To 51 : Return 75
                    Case 52 : Return 76
                    Case 53 : Return 77
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 63)
                    Case 1 : Return 68
                    Case 2 : Return 69
                    Case 3 To 5 : Return 70
                    Case 6 To 8 : Return 71
                    Case 9 To 16 : Return 72
                    Case 17 To 27 : Return 73
                    Case 28 To 40 : Return 74
                    Case 41 To 50 : Return 75
                    Case 51 To 56 : Return 76
                    Case 57 To 61 : Return 77
                    Case 62 : Return 78
                    Case 63 : Return 79
                End Select
        End Select
    End Function
    Private Function GetCollegePos() As String
        Select Case MT.GenerateDouble(0, 100)
            Case 0 To 5.186 : Return "QB"
            Case 5.187 To 12.102 : Return "RB"
            Case 12.103 To 14.209 : Return "FB"
            Case 14.21 To 24.743 : Return "WR"
            Case 24.744 To 30.956 : Return "TE"
            Case 30.957 To 35.764 : Return "C"
            Case 35.765 To 41.167 : Return "OG"
            Case 41.168 To 48.892 : Return "OT"
            Case 48.893 To 57.266 : Return "DE"
            Case 57.267 To 65.37 : Return "DT"
            Case 65.371 To 74.029 : Return "OLB"
            Case 74.03 To 78.23 : Return "ILB"
            Case 78.231 To 83.581 : Return "FS"
            Case 83.582 To 88.763 : Return "SS"
            Case 88.764 To 96.218 : Return "CB"
            Case 96.219 To 98.217 : Return "K"
            Case Else : Return "P"
        End Select
    End Function
    Private Function GetKickRetAbility(ByVal Pos As String, ByVal i As Integer) As Integer
        'gets the ability to return kicks
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case Pos
                    Case "RB", "WR", "CB"
                        Return MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Private Function GetPuntRetAbility(ByVal Pos As String, ByVal i As Integer) As Integer
        Select Case MT.GenerateInt32(1, 100)
            Case 1 To 75
                Return 0
            Case Else
                Select Case Pos
                    Case "RB", "WR", "CB", "FS"
                        Return PlayerDT.Rows(i).Item("RETKickReturn") = MT.GetGaussian(49.5, 16.5)
                    Case Else
                        Return 0
                End Select
        End Select
    End Function
    Private Sub GetSTAbility(ByVal Pos As String, ByVal i As Integer)
        Select Case MT.GenerateInt32(1, 100)
            Case 26 To 75
                Select Case Pos
                    Case "QB", "K", "P", "DT"
                        PlayerDT.Rows(i).Item("STCoverage") = 0
                        PlayerDT.Rows(i).Item("STWillingness") = 0
                        PlayerDT.Rows(i).Item("STAssignment") = 0
                        PlayerDT.Rows(i).Item("STDiscipline") = 0
                    Case Else
                        PlayerDT.Rows(i).Item("STCoverage") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STWillingness") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STAssignment") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("STDiscipline") = MT.GetGaussian(49.5, 16.5)
                End Select
        End Select
    End Sub
    Private Sub GetLSAbility(ByVal Pos As String, ByVal i As Integer)
        Select Case Pos
            Case "C", "TE", "DE", "OG"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 90
                        PlayerDT.Rows(i).Item("LSSnapTime") = 0
                        PlayerDT.Rows(i).Item("LSSnapAccuracy") = 0
                    Case Else
                        PlayerDT.Rows(i).Item("LSSnapTime") = MT.GetGaussian(49.5, 16.5)
                        PlayerDT.Rows(i).Item("LSSnapAccuracy") = MT.GetGaussian(49.5, 16.5)
                End Select
            Case Else
                PlayerDT.Rows(i).Item("LSSnapTime") = 0
                PlayerDT.Rows(i).Item("LSSnapAccuracy") = 0
        End Select
    End Sub
    Private Function GetDOB(ByVal Age As Integer, ByVal i As Integer) As String
        Dim Day As Integer
        Dim Month As Integer
        Dim Year As Integer
        'Dim TestDate As Date

        Month = MT.GenerateInt32(1, 12)

        Select Case Month
            Case 1, 3, 5, 7, 8, 10, 12
                Day = MT.GenerateInt32(1, 31)
            Case 2
                Day = MT.GenerateInt32(1, 28)
            Case Else
                Day = MT.GenerateInt32(1, 30)
        End Select

        Year = Date.Today.Year - Age
        Dim DOB As String = "" & Month & "/" & Day & "/" & Year & ""
        Return DOB
        'TestDate = Convert.ToDateTime("" & Month & "/" & Day & "/" & Year).Date
        'Return Convert.ToDateTime("" & Month & "/" & Day & "/" & Year).ToShortDateString
    End Function
    ''' <summary>
    ''' Common Position switches include:
    ''' QB ---> WR ---Typically very athletic QB's that aren't good enough at QB for the NFL(Julian Edelman for example)
    ''' QB ---> RB ---Typically very athletic QB's that are option type QB's in college who do a lot of running(Michael Robinson for example)
    ''' DE ---> OLB ---Typically "smaller", athletic DE's in college are too small to play DE in the NFL(Jerry Hughes for example)
    ''' CB ---> SF ---Typically slower type CB's in college that have good ball and football instincts but lack the speed to cover WR's or hands to catch the ball in the NFL(Jairius Byrd, Aaron Williams, and Devin McCourty for example)
    ''' WR ---> SF ---Typically slower type WR's that are good playing the ball but lack hands needed at WR(George Wilson for example)
    ''' OT ---> OG ---Typically "smaller" OT's in college that are the size of guards in the NFL(one of the most common--numerous examples)
    ''' LB ---> SF ---Typically "smaller" LB's in college that are athletic and fast enough to play safety but don't have enough size to play LB(Adam Archuleta for example)
    ''' FB ---> TE ---Typically the more athletic FB's in college in a run heavy offensive scheme can make more use of their skills as a TE or H-Back(Charles Clay for example)
    ''' other examples and less common changes occur---
    ''' 
    ''' Need to figure out how often and under what circumstances a player would have a different position---currently it sets it to the same position as they are in college
    ''' </summary>
    ''' <param name="Pos"></param>
    ''' <returns></returns>
    Private Function GetNFLPos(ByVal Pos As String) As String '####TODO: Determine how often and waht percentage of players would play a different position ni the NFL than in college(I'm thinking maybe 5-7%, most common is OT to OG and CB to SF
        'Players who are too small/light/slow for their current college positions
        'can be projected to play a different position in the NFL
        '
        Return Pos
    End Function
    Private Sub GetPosSkills(ByVal Pos As String, ByVal i As Integer, ByVal DT As DataTable)
        Select Case Pos
            Case "QB"
                DT.Rows(i).Item("QBDropQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBSetUpQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBReleaseQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBShortAcc") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBMedAcc") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBLongAcc") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBDecMaking") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBFieldVision") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBPoise") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBBallHandling") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBTiming") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBDeliveryQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBAvoidRush") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBScrambling") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBRolloutRight") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBRolloutLeft") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBArmStrength") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("QBTouch") = MT.GetGaussian(49.5, 16.5)
            Case "RB"
                DT.Rows(i).Item("RBRunVision") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBInsideAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBOutsideAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBElusiveAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBPowerAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBRouteRunning") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBRunningStyle") = "NONE"
                DT.Rows(i).Item("ORunAfterCatch") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OReleasePassRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OShortRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OMedRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ODeepRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OScreenRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSwingRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ORunBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OPassBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSustainBlock") = MT.GetGaussian(49.5, 16.5)
            Case "FB"
                DT.Rows(i).Item("RBRunVision") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBInsideAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBOutsideAbility") = MT.GetGaussian(25, 8.33)
                DT.Rows(i).Item("RBElusiveAbility") = MT.GetGaussian(25, 8.33)
                DT.Rows(i).Item("RBPowerAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBRouteRunning") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("RBRunningStyle") = "NONE"
                DT.Rows(i).Item("ORunAfterCatch") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OReleasePassRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OShortRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OMedRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ODeepRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OScreenRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSwingRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ORunBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OPassBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSustainBlock") = MT.GetGaussian(49.5, 16.5)
            Case "WR", "TE"
                DT.Rows(i).Item("ORunAfterCatch") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OReleasePassRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ORunAfterCatch") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OReleasePassRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OShortRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OMedRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ODeepRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OScreenRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSwingRoute") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ORunBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OPassBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSustainBlock") = MT.GetGaussian(49.5, 16.5)
            Case "OT", "OG", "C"
                DT.Rows(i).Item("OLDriveBlocking") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLPulling") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLInSpace") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLHandUse") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLAnchorAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLRecover") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OLLongSnapPot") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("ORunBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OPassBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("OSustainBlock") = MT.GetGaussian(49.5, 16.5)
            Case "DE", "DL"
                DT.Rows(i).Item("DLOneOnOneAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DLStyle") = "NONE"
                DT.Rows(i).Item("DLDoubleTeamAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DLRunAtHim") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DLHandUse") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DDefeatBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DShedBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBallPursuit") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DReadPlay") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DManToManCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DZoneCoverage") = MT.GetGaussian(49.5, 16.5)
            Case "OLB", "ILB"
                DT.Rows(i).Item("LBDropDepth") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("LBFillGaps") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DDefeatBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DShedBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBallPursuit") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DReadPlay") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DManToManCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DZoneCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBlitzAbility") = MT.GetGaussian(49.5, 16.5)
            Case "CB", "FS", "SS"
                DT.Rows(i).Item("DBTurnAndRun") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBBreakOnBall") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBClosingSpeed") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBPressCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBRunContain") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DDefeatBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DShedBlock") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBallPursuit") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DReadPlay") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DManToManCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DZoneCoverage") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("DBlitzAbility") = MT.GetGaussian(49.5, 16.5)
            Case "K"
                DT.Rows(i).Item("KickAccuracy") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickStrength") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickHandlingWind") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickRunningAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickPassingAbility") = MT.GetGaussian(49.5, 16.5)
            Case "P"
                DT.Rows(i).Item("KickAccuracy") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickQuickness") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickStrength") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickHandlingWind") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickRunningAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("KickPassingAbility") = MT.GetGaussian(49.5, 16.5)
                DT.Rows(i).Item("PuntHangTime") = MT.GetGaussian(49.5, 16.5)
        End Select
    End Sub
    Private Function GetSkillsTranslate(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" 'QB 53% Bust 33% ProBowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 53 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 54 To 66 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "RB" 'RB Skills 49% Bust 36% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 49 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 50 To 63 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FB" 'FB Skills usually translate pretty well to the NFL as well..
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 30 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 31 To 75 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "WR" 'WR 45% Bust 31% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 68 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "TE" 'TE Skills are fairly translatable
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 35 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 36 To 67 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OT", "C", "OG" '31% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 73 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DE" '31% Bust 33% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 31 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 32 To 66 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "DT" '33% Bust 40% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 33 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 34 To 59 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "OLB", "ILB" '16% Bust 26% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 16 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 17 To 73 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "CB" '29% Bust 23% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 29 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 30 To 77 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "FS", "SS" '11% Bust 53% Pro Bowls
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 11 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 12 To 46 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 45 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 46 To 76 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 100)
                    Case 1 To 40 : Return CInt(MT.GetGaussian(16.5, 5.5))
                    Case 41 To 76 : Return CInt(MT.GetGaussian(50, 6.667))
                    Case Else : Return CInt(MT.GetGaussian(85, 5))
                End Select
        End Select
    End Function
    Private Function GetWonderlic(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(26.678, 6.521))
            Case "RB" : Return CInt(MT.GetGaussian(18.68, 6.1561))
            Case "FB" : Return CInt(MT.GetGaussian(19.5313, 6.13384))
            Case "WR" : Return CInt(MT.GetGaussian(19.6814, 6.14898))
            Case "TE" : Return CInt(MT.GetGaussian(24.0909, 8.18485))
            Case "OT" : Return CInt(MT.GetGaussian(24.881, 7.15376))
            Case "C" : Return CInt(MT.GetGaussian(26.9697, 6.28844))
            Case "OG" : Return CInt(MT.GetGaussian(24.0339, 6.09241))
            Case "DE" : Return CInt(MT.GetGaussian(20.1348, 7.59389))
            Case "DT" : Return CInt(MT.GetGaussian(19.4054, 7.94052))
            Case "OLB" : Return CInt(MT.GetGaussian(20.2623, 5.85872))
            Case "ILB" : Return CInt(MT.GetGaussian(22.4286, 6.27976))
            Case "CB" : Return CInt(MT.GetGaussian(18.4881, 5.74351))
            Case "FS" : Return CInt(MT.GetGaussian(21.0213, 5.71854))
            Case "SS" : Return CInt(MT.GetGaussian(19.2553, 5.38109))
            Case "K" : Return CInt(MT.GetGaussian(24.32, 6.21431))
            Case "P" : Return CInt(MT.GetGaussian(25.875, 5.38952))

        End Select
    End Function
    Private Function GetBenchPress(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(19.8, 2.85657))
            Case "RB" : Return CInt(MT.GetGaussian(20.3148, 4.51673))
            Case "FB" : Return CInt(MT.GetGaussian(23.4848, 4.28614))
            Case "WR" : Return CInt(MT.GetGaussian(16.7586, 3.38001))
            Case "TE" : Return CInt(MT.GetGaussian(21.3333, 4.45034))
            Case "OT" : Return CInt(MT.GetGaussian(24.9524, 4.60651))
            Case "C" : Return CInt(MT.GetGaussian(26.5313, 4.24253))
            Case "OG" : Return CInt(MT.GetGaussian(26.4419, 4.79487))
            Case "DE" : Return CInt(MT.GetGaussian(24.6825, 5.9089))
            Case "DT" : Return CInt(MT.GetGaussian(28.3519, 5.78362))
            Case "OLB" : Return CInt(MT.GetGaussian(22.5873, 4.95899))
            Case "ILB" : Return CInt(MT.GetGaussian(22.8947, 3.85793))
            Case "CB" : Return CInt(MT.GetGaussian(15.4, 3.32265))
            Case "FS" : Return CInt(MT.GetGaussian(16.5, 3.59048))
            Case "SS" : Return CInt(MT.GetGaussian(17.2333, 4.63093))
        End Select
    End Function
    Private Function Get3Cone(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(7.16143, 0.27591), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(7.00848, 0.16455), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(7.30794, 0.26069), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(6.95564, 0.16844), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(7.15089, 0.20702), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(7.80527, 0.30271), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(7.71718, 0.25131), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(7.87723, 0.32694), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(7.33298, 0.27823), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(7.70881, 0.22825), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(7.12055, 0.23154), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(7.222, 0.21437), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(6.984, 0.22688), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(7.02656, 0.19514), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(7.06844, 0.24738), 2)
        End Select
    End Function
    Private Function GetVertJump(ByVal Pos As String) As Double
        Dim NumString As String = ""
        Dim Num As Integer
        Dim NumStr As Double
        Select Case Pos
            Case "QB" : NumString = CStr(Math.Round(MT.GetGaussian(30.551, 3.69258), 1))
            Case "RB" : NumString = CStr(Math.Round(MT.GetGaussian(33.3657, 2.86198), 1))
            Case "FB" : NumString = CStr(Math.Round(MT.GetGaussian(32.0172, 2.91395), 1))
            Case "WR" : NumString = CStr(Math.Round(MT.GetGaussian(34.3807, 3.20614), 1))
            Case "TE" : NumString = CStr(Math.Round(MT.GetGaussian(32.1915, 3.69956), 1))
            Case "OT" : NumString = CStr(Math.Round(MT.GetGaussian(26.8952, 3.42092), 1))
            Case "C" : NumString = CStr(Math.Round(MT.GetGaussian(28.2368, 3.332601), 1))
            Case "OG" : NumString = CStr(Math.Round(MT.GetGaussian(26.1935, 2.41976), 1))
            Case "DE" : NumString = CStr(Math.Round(MT.GetGaussian(32.4123, 3.98806), 1))
            Case "DT" : NumString = CStr(Math.Round(MT.GetGaussian(29, 3.07743), 1))
            Case "OLB" : NumString = CStr(Math.Round(MT.GetGaussian(33.5635, 4.15808), 1))
            Case "ILB" : NumString = CStr(Math.Round(MT.GetGaussian(32.9865, 3.51538), 1))
            Case "CB" : NumString = CStr(Math.Round(MT.GetGaussian(35.5467, 3.33027), 1))
            Case "FS" : NumString = CStr(Math.Round(MT.GetGaussian(35.0238, 3.37348), 1))
            Case "SS" : NumString = CStr(Math.Round(MT.GetGaussian(35.2439, 3.07433), 1))
            Case "K", "P" : NumString = CStr(0)
        End Select

        NumStr = CInt(NumString)
        If NumString = NumStr Then : Return NumStr

        Else
            Num = Regex.Match(NumString, "(?<=\d+\.)\d").Value
            If Num < 4 Then : Return NumStr
            ElseIf Num = 4 Then : Return CDbl(NumString) + 0.1
            ElseIf Num = 5 Then : Return CDbl(NumString)
            ElseIf Num = 6 Then : Return CDbl(NumString) - 0.1
            ElseIf Num = 7 Then : Return CDbl(NumString) - 0.2
            ElseIf Num = 8 Then : Return CDbl(NumString) + 0.2
            ElseIf Num = 9 Then : Return CDbl(NumString) + 0.1
            End If
        End If
    End Function
    Private Function GetBroadJump(ByVal Pos As String) As Integer
        Select Case Pos
            Case "QB" : Return CInt(MT.GetGaussian(109.792, 5.32666))
            Case "RB" : Return CInt(MT.GetGaussian(117.897, 6.53304))
            Case "FB" : Return CInt(MT.GetGaussian(112.414, 6.87583))
            Case "WR" : Return CInt(MT.GetGaussian(120.213, 4.79013))
            Case "TE" : Return CInt(MT.GetGaussian(113.587, 5.61665))
            Case "OT" : Return CInt(MT.GetGaussian(102.7, 5.56567))
            Case "C" : Return CInt(MT.GetGaussian(102.432, 4.95133))
            Case "OG" : Return CInt(MT.GetGaussian(100.839, 5.82041))
            Case "DE" : Return CInt(MT.GetGaussian(113.898, 5.58356))
            Case "DT" : Return CInt(MT.GetGaussian(106.217, 4.38829))
            Case "OLB" : Return CInt(MT.GetGaussian(116.323, 5.55841))
            Case "ILB" : Return CInt(MT.GetGaussian(113.263, 5.00886))
            Case "CB" : Return CInt(MT.GetGaussian(121.865, 5.82475))
            Case "FS" : Return CInt(MT.GetGaussian(119.432, 6.13162))
            Case "SS" : Return CInt(MT.GetGaussian(119.075, 6.38117))
        End Select
    End Function
    Private Function GetShortShuttle(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(4.37, 0.18174), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(4.31, 0.13471), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(4.4, 0.16438), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(4.25, 0.12744), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(4.39, 0.16592), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(4.76, 0.19038), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(4.62, 0.17958), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(4.81, 0.18228), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(4.45, 0.18935), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(4.64, 0.18484), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(4.31, 0.15413), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(4.32, 0.12603), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(4.24, 0.13143), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(4.28, 0.16186), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(4.23, 0.15594), 2)
        End Select

    End Function
    Private Function Get40Time(ByVal Pos As String) As Double
        Select Case Pos
            'Case "QB" : Return Math.Round(MT.GetGaussian(4.84335, 0.17588), 2)
            Case "QB"
                Select Case MT.GenerateInt32(1, 168)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.37, 4.39), 2)
                    Case 2 To 3 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 4 To 15 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 16 To 30 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 31 To 71 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 72 To 111 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 112 To 135 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 136 To 153 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 154 To 163 : Return Math.Round(MT.GenerateDouble(5.1, 5.19), 2)
                    Case 164 To 167 : Return Math.Round(MT.GenerateDouble(5.2, 5.29), 2)
                    Case 168 : Return Math.Round(MT.GenerateDouble(5.4, 5.49), 2)
                End Select
                'Case "RB" : Return Math.Round(MT.GetGaussian(4.56, 0.097434), 2)
            Case "RB"
                Select Case MT.GenerateInt32(1, 144)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 2 To 5 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 6 To 37 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 38 To 102 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 103 To 135 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 136 To 143 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 144 : Return Math.Round(MT.GenerateDouble(4.8, 4.84), 2)
                End Select
            Case "FB" : Return Math.Round(MT.GetGaussian(4.76488, 0.1367), 2)

            Case "WR"
                Select Case MT.GenerateInt32(1, 305)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 2 To 16 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 17 To 75 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 76 To 203 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 204 To 293 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 294 To 305 : Return Math.Round(MT.GenerateDouble(4.7, 4.84), 2)
                End Select
                'Case "TE" : Return Math.Round(MT.GetGaussian(4.82, 0.132), 2)
            Case "TE"
                Select Case MT.GenerateInt32(1, 158)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.37, 4.49), 2)
                    Case 2 To 5 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 6 To 26 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 27 To 67 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 68 To 110 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 111 To 141 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 142 To 157 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 158 : Return Math.Round(MT.GenerateDouble(5.1, 5.15), 2)
                End Select
            Case "OT" : Return Math.Round(MT.GetGaussian(5.30864, 0.180568), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(5.32628, 0.192915), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(5.25084, 0.18133), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(4.84454, 0.132509), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(5.10542, 0.147088), 2)
                'Case "OLB" : Return Math.Round(MT.GetGaussian(4.67983, 0.12102), 2)
            Case "OLB"
                Select Case MT.GenerateInt32(1, 177)
                    Case 1 To 8 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 9 To 42 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 43 To 107 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 108 To 153 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 154 To 165 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 166 To 174 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 175 To 177 : Return Math.Round(MT.GenerateDouble(5.0, 5.09), 2)
                End Select
                'Case "ILB" : Return Math.Round(MT.GetGaussian(4.77248, 0.12737), 2)

            Case "ILB"
                Select Case MT.GenerateInt32(1, 126)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.42, 4.49), 2)
                    Case 2 To 11 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 12 To 37 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 38 To 73 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 74 To 106 : Return Math.Round(MT.GenerateDouble(4.8, 4.89), 2)
                    Case 107 To 121 : Return Math.Round(MT.GenerateDouble(4.9, 4.99), 2)
                    Case 122 To 124 : Return Math.Round(MT.GenerateDouble(5, 5.09), 2)
                    Case 125 : Return Math.Round(MT.GenerateDouble(5.1, 5.19), 2)
                    Case 126 : Return Math.Round(MT.GenerateDouble(5.2, 5.25), 2)
                End Select
                'Case "CB" : Return Math.Round(MT.GetGaussian(4.51607, 0.086921), 2)
            Case "CB"
                Select Case MT.GenerateInt32(1, 201)
                    Case 1 To 4 : Return Math.Round(MT.GenerateDouble(4.25, 4.29), 2)
                    Case 5 To 21 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 22 To 80 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 81 To 174 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 175 To 201 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                End Select
                'Case "SS" : Return Math.Round(MT.GetGaussian(4.57, 0.08), 2)
            Case "SS"
                Select Case MT.GenerateInt32(1, 94)
                    Case 1 : Return Math.Round(MT.GenerateDouble(4.35, 4.39), 2)
                    Case 2 To 7 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 8 To 36 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 37 To 90 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 91 To 93 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                    Case 94 : Return Math.Round(MT.GenerateDouble(4.8, 4.85), 2)
                End Select
                'Case "FS" : Return Math.Round(MT.GetGaussian(4.56, 0.08), 2)
            Case "FS"
                Select Case MT.GenerateInt32(1, 102)
                    Case 1 To 2 : Return Math.Round(MT.GenerateDouble(4.3, 4.39), 2)
                    Case 3 To 13 : Return Math.Round(MT.GenerateDouble(4.4, 4.49), 2)
                    Case 14 To 63 : Return Math.Round(MT.GenerateDouble(4.5, 4.59), 2)
                    Case 64 To 100 : Return Math.Round(MT.GenerateDouble(4.6, 4.69), 2)
                    Case 101 To 102 : Return Math.Round(MT.GenerateDouble(4.7, 4.79), 2)
                End Select
            Case "K" : Return Math.Round(MT.GetGaussian(4.94, 0.114), 2)
            Case "P" : Return Math.Round(MT.GetGaussian(4.93, 0.127), 2)
        End Select
    End Function
    Private Function Get20Time(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(2.8, 0.094), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(2.63, 0.082), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(2.74, 0.0776), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(2.62, 0.0715), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(2.76, 0.0889), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(3.04, 0.1187), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(3.01, 0.0839), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(3.07, 0.1067), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(2.8, 0.086), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(2.96, 0.0906), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(2.7, 0.0809), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(2.75, 0.0885), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(2.595, 0.0704), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(2.644, 0.0675), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(2.649, 0.08177), 2)
        End Select
    End Function
    Private Function Get10Time(ByVal Pos As String) As Double
        Select Case Pos
            Case "QB" : Return Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "RB" : Return Math.Round(MT.GetGaussian(1.6, 0.0519), 2)
            Case "FB" : Return Math.Round(MT.GetGaussian(1.67, 0.056), 2)
            Case "WR" : Return Math.Round(MT.GetGaussian(1.59, 0.0535), 2)
            Case "TE" : Return Math.Round(MT.GetGaussian(1.69, 0.0573), 2)
            Case "OT" : Return Math.Round(MT.GetGaussian(1.84, 0.064), 2)
            Case "OG" : Return Math.Round(MT.GetGaussian(1.84, 0.0695), 2)
            Case "C" : Return Math.Round(MT.GetGaussian(1.8, 0.0632), 2)
            Case "DE" : Return Math.Round(MT.GetGaussian(1.68, 0.0605), 2)
            Case "DT" : Return Math.Round(MT.GetGaussian(1.77, 0.0636), 2)
            Case "OLB" : Return Math.Round(MT.GetGaussian(1.63, 0.0548), 2)
            Case "ILB" : Return Math.Round(MT.GetGaussian(1.66, 0.0638), 2)
            Case "CB" : Return Math.Round(MT.GetGaussian(1.49, 0.0465), 2)
            Case "SS" : Return Math.Round(MT.GetGaussian(1.51, 0.0403), 2)
            Case "FS" : Return Math.Round(MT.GetGaussian(1.51, 0.0386), 2)
        End Select
    End Function
    Public Sub GenDraftClass(ByVal NumPlayers As Integer)
       
        'Generates a draft class
        '-------------------------------------------------
        'Gens strength of draft class for each position
        'Mean Number of players drafted at that position
        'Std Dev to get min and max numbers
        '---------------------------------------------------
    End Sub

    Private Function GetOffPhil() As String
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
        End Select
    End Function
    Private Function GetDefPhil() As String
        Select Case MT.GenerateInt32(1, 93)
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

        End Select
    End Function
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
    Private Sub GetDraftGrades(ByVal Num As Integer, ByVal Pos As String)
        Dim OverallGrade As Single
        Select Case Pos
            Case "QB"
                Select Case MT.GenerateInt32(1, 1982)
                    Case 1 To 6 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 7 To 14 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 15 To 26 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 27 To 59 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 60 To 85 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 86 To 125 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 126 To 155 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 156 To 206 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 207 To 260 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 261 To 335 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 336 To 435 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 436 To 560 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 561 To 710 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 711 To 1982 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "RB"
                Select Case MT.GenerateInt32(1, 3708)
                    Case 1 To 9 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 10 To 20 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 21 To 35 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 36 To 65 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 66 To 121 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 122 To 203 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 204 To 257 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 258 To 313 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 314 To 392 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 393 To 485 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 486 To 735 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 736 To 1135 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1136 To 1735 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1736 To 3708 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "FB"
                Select Case MT.GenerateInt32(1, 1092)
                    Case 1 To 3 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 4 To 7 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 13 To 29 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 30 To 57 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 58 To 102 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 103 To 142 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 143 To 235 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 236 To 365 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 366 To 495 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 496 To 1092 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "WR"
                Select Case MT.GenerateInt32(1, 6510)
                    Case 1 To 21 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 22 To 46 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 47 To 75 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 76 To 103 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 104 To 201 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 202 To 301 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 302 To 383 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 384 To 486 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 487 To 603 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 604 To 731 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 732 To 1231 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 1232 To 1931 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1932 To 2931 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2932 To 6510 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "TE"
                Select Case MT.GenerateInt32(1, 2020)
                    Case 1 To 5 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 6 To 10 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 11 To 18 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 31 To 74 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 75 To 109 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 110 To 153 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 154 To 207 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 208 To 272 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 273 To 361 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 362 To 485 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 486 To 682 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 683 To 982 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 983 To 2020 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OT"
                Select Case MT.GenerateInt32(1, 3608)
                    Case 1 To 8 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 9 To 22 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 23 To 40 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 41 To 84 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 85 To 138 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 139 To 213 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 214 To 267 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 268 To 335 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 336 To 414 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 415 To 512 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 513 To 613 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 614 To 763 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 764 To 1050 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1051 To 3608 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OG"
                Select Case MT.GenerateInt32(1, 3290)
                    Case 1 To 5 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 6 To 10 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 11 To 18 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 31 To 74 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 75 To 109 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 110 To 153 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 154 To 207 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 208 To 272 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 273 To 361 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 362 To 485 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 486 To 682 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 683 To 982 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 983 To 3290 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "C"
                Select Case MT.GenerateInt32(1, 1675)
                    Case 1 To 2 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 3 To 5 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 6 To 9 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 10 To 14 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 15 To 33 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 34 To 61 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 62 To 87 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 88 To 115 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 116 To 162 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 163 To 227 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 228 To 327 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 328 To 477 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 478 To 677 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 678 To 1675 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "DE"
                Select Case MT.GenerateInt32(1, 3786)
                    Case 1 To 10 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 11 To 29 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 30 To 55 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 56 To 96 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 97 To 171 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 172 To 241 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 242 To 302 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 303 To 379 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 380 To 470 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 471 To 563 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 564 To 763 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 764 To 1063 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1064 To 1663 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1664 To 3786 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "DT"
                Select Case MT.GenerateInt32(1, 3112)
                    Case 1 To 9 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 10 To 25 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 26 To 45 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 46 To 75 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 76 To 133 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 134 To 189 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 190 To 252 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 253 To 317 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 318 To 401 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 402 To 497 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 498 To 673 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 674 To 998 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 999 To 1398 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 1399 To 3112 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "OLB"
                Select Case MT.GenerateInt32(1, 4801)
                    Case 1 To 7 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 8 To 19 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 20 To 35 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 36 To 65 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 66 To 116 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 117 To 179 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 180 To 235 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 236 To 296 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 297 To 380 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 381 To 485 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 486 To 885 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 886 To 1485 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1486 To 2285 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2286 To 4801 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "ILB"
                Select Case MT.GenerateInt32(1, 2186)
                    Case 1 To 4 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 5 To 9 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 10 To 15 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 16 To 24 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 25 To 44 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 45 To 93 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 94 To 144 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 145 To 193 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 194 To 244 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 245 To 314 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 315 To 454 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 455 To 654 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 655 To 954 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 955 To 2186 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "CB"
                Select Case MT.GenerateInt32(1, 4768)
                    Case 1 To 12 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 13 To 30 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 31 To 55 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 56 To 100 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 101 To 191 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 192 To 287 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 288 To 364 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 365 To 448 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 449 To 532 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 533 To 639 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 640 To 939 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 940 To 1439 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 1440 To 2439 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 2440 To 4768 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "FS"
                Select Case MT.GenerateInt32(1, 2815)
                    Case 1 To 3 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 4 To 7 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 13 To 19 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 20 To 49 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 50 To 93 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 94 To 140 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 141 To 196 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 197 To 259 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 260 To 348 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 349 To 448 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 449 To 598 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 599 To 848 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 849 To 2815 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "SS"
                Select Case MT.GenerateInt32(1, 2589)
                    Case 1 To 4 : OverallGrade = MT.GetGaussian(8.75, 0.083333) '1
                    Case 5 To 7 : OverallGrade = MT.GetGaussian(8.5, 0.083333) '1
                    Case 8 To 12 : OverallGrade = MT.GetGaussian(8.25, 0.083333) '1
                    Case 13 To 23 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 24 To 46 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 47 To 95 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 96 To 128 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 129 To 170 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 171 To 226 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 227 To 289 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 290 To 369 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 370 To 494 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 495 To 794 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 795 To 2589 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "K"
                Select Case MT.GenerateInt32(1, 1045)
                    Case 1 : OverallGrade = MT.GetGaussian(7.75, 0.083333) '1
                    Case 2 To 6 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 7 To 11 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 12 To 18 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 19 To 30 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 31 To 44 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 45 To 88 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 89 To 138 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 139 To 203 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 204 To 305 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 306 To 1045 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
            Case "P"
                Select Case MT.GenerateInt32(1, 1086)
                    Case 1 : OverallGrade = MT.GetGaussian(7.25, 0.083333) '2
                    Case 2 To 3 : OverallGrade = MT.GetGaussian(6.75, 0.083333) '3
                    Case 4 To 16 : OverallGrade = MT.GetGaussian(6.25, 0.083333) '4
                    Case 17 To 30 : OverallGrade = MT.GetGaussian(5.75, 0.083333) '5
                    Case 31 To 46 : OverallGrade = MT.GetGaussian(5.25, 0.083333) '6
                    Case 47 To 90 : OverallGrade = MT.GetGaussian(4.75, 0.083333) '7
                    Case 91 To 145 : OverallGrade = MT.GetGaussian(4.37, 0.083333) 'PFA
                    Case 146 To 215 : OverallGrade = MT.GetGaussian(4.13, 0.0833333) 'LFA
                    Case 216 To 315 : OverallGrade = MT.GetGaussian(3.87, 0.083333) 'PSquad
                    Case 316 To 1086 : OverallGrade = MT.GetGaussian(3.25, 0.203333) 'Reject
                End Select
        End Select

        DraftDT.Rows(Num).Item("ActualGrade") = Math.Round(OverallGrade, 2)
        Console.WriteLine(Math.Round(OverallGrade, 2))
        GetIndGrades(Num, Pos, OverallGrade)


    End Sub
    Private Sub GetIndGrades(ByVal Num As Integer, ByVal Pos As String, ByVal Grade As Single)
        Select Case Pos
            Case "QB"
                Select Case Grade
                    Case Is > 7.49 '1st round talent
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                    Case 7.0 To 7.49
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                    Case 6.5 To 6.99 '3rd round
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)

                    Case 6.0 To 6.49
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.75, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5.75, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)

                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5.5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5.5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)

                    Case Is < 5
                        DraftDT.Rows(Num).Item("QBDropQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBSetUpQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBReleaseQuickness") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBShortAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBMedAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBLongAcc") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDecMaking") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFieldVision") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBPoise") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBBallHandling") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTiming") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBDelivery") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBFollowThrough") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBAvoidRush") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEscape") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBScrambling") = Math.Round(MT.GetGaussian(5, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutRight") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBRolloutLeft") = Math.Round(MT.GetGaussian(5.25, 0.83333), 1)
                        DraftDT.Rows(Num).Item("QBArmStrength") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBZip") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBTouchScreenPass") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBTouchSwingPass") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveShortOut") = Math.Round(MT.GetGaussian(5, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveDeepOut") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveGoRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectivePostRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QBEffectiveCornerRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "FB"
                DraftDT.Rows(Num).Item("RBEffortBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBDurability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBBallSecurity") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.5), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(5.5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                End Select
            Case "RB"
                DraftDT.Rows(Num).Item("RBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBEffortBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRunBlocking") = Math.Round(MT.GetGaussian(3, 0.583333), 1)
                DraftDT.Rows(Num).Item("RBDurability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBPowerAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBBallSecurity") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRouteRunning") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("RBRunningStyle") = "NONE"
                DraftDT.Rows(Num).Item("RBPassBlocking") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.41666733), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6.25, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.5), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(6, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.75, 0.416667), 1)
                    Case 5.0 To 5.99
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.33333), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("RBStart") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBRunVision") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBInsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBOutsideAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("RBElusiveAbility") = Math.Round(MT.GetGaussian(5, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.33333), 1)
                End Select
            Case "WR"
                DraftDT.Rows(Num).Item("WRShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRCrowdReaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRCatchWhenHit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRConcentration") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRFieldAwareness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRBodyCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("WRRelease") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7.0 To 7.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.58333), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("WRStart") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRPatterns") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRMedRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRDeepRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRBallAdjust") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRHandCatch") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("WRRAC") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "TE"
                DraftDT.Rows(Num).Item("TEGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEDoubleTeamBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TETurnAndWallBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TESustainBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TECrowdReaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TECatchWhenHit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEConcentration") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEBodyCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEFieldAwareness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("TEPassProtect") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("TEDriveIntoPassRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEPatterns") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEShortRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEMedRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEDeepRoute") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEBallAdjust") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TEHandCatch") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("TERAC") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "OT", "OG", "OC"
                DraftDT.Rows(Num).Item("OLGetOutside") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLReachBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLTurnDefender") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLPulling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLTrapBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OL2ndLevelPull") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLAdjustToLB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLSlide") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLHandUse") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLHandPop") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("OLLongSnapPotential") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.4166673), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("OLGetOffLineRunBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLOneOnOneBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDriveBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLDownBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLSustainBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassBlocking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLPassDrops") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLFeetSetup") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLAnchorAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLRecover") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("OLStrength") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        'DraftDT.Rows(Num).Item("OLSustain") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "DL", "DE"
                DraftDT.Rows(Num).Item("DLStyle") = "NONE"
                DraftDT.Rows(Num).Item("DLRunAtHim") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLAgainstTrapAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLSlideAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLRunPursuit") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLPassRushTechnique") = "NONE"
                DraftDT.Rows(Num).Item("DLHandUse") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLShedVsRunAway") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLTackleVsRunAway") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DLChangeDirection") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.583333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DLReleaseOffBall") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLOneOnOneAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDoubleTeamAbility") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLDefeatBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedRunBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFirstStepPassRush") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLShedPassBlock") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLPressure") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DLFinish") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "OLB", "ILB"
                DraftDT.Rows(Num).Item("LBDropDepth") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBBlitz") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("LBPassRushType") = "NONE"
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("LBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBDefeatBlocks") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBShedBlocks") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBInsideTackle") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("LBOutsideTackle") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "CB"
                DraftDT.Rows(Num).Item("DBPressBailCoverage") = Math.Round(MT.GetGaussian(6, 0.6667), 1)

                DraftDT.Rows(Num).Item("DBBallReaction") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBWardOffBlockers") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunTackling") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBHands") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBBump") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                DraftDT.Rows(Num).Item("DBBlitz") = Math.Round(MT.GetGaussian(6, 0.6667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 4.16667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 4.16667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchupSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "FS", "SS"
                DraftDT.Rows(Num).Item("DBBackpedal") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBTurn") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBPressBailCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBBump") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBManToManCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBRunContain") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBWardOffBlockers") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBBlitz") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("DBHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("DBRead") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBInstincts") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBClose") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBRange") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBurst") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCatchUpSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBBallreaction") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBZoneCoverage") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBTackling") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBCOBP") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("DBFeet") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "K"
                DraftDT.Rows(Num).Item("KPlantRelationship") = "NONE"
                DraftDT.Rows(Num).Item("KApproachAngle") = "NONE"
                DraftDT.Rows(Num).Item("KBallFlight") = "NONE"
                DraftDT.Rows(Num).Item("KSteppingPattern") = "NONE"
                DraftDT.Rows(Num).Item("KKickingStyle") = "NONE"
                DraftDT.Rows(Num).Item("KHandlingWind") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KRunAndPassAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KKOFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("KFGOperationTimes") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("KAccuracy") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KHandlingPressure") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KFootSpeed") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickQuickness") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKickRise") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOProduction") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("KKOMentalStability") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                End Select
            Case "P"
                DraftDT.Rows(Num).Item("PFootSpeed") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PApproachLine") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PHandlingTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PSteppingPattern") = "NONE"
                DraftDT.Rows(Num).Item("PHands") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PTackling") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("PRunAndPassAbility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("Athleticism") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("QAB") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                DraftDT.Rows(Num).Item("COD") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                Select Case Grade
                    Case Is > 7.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 7 To 7.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.5, 0.416667), 1)
                    Case 6.5 To 6.99
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6.25, 0.58333), 1)
                    Case 6 To 6.49
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
                    Case 5 To 5.99
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(5.5, 0.66667), 1)
                    Case Is < 5
                        DraftDT.Rows(Num).Item("PDistance") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHangTime") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PPressureKicking") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PBlockZone") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PHandToFootTime") = Math.Round(MT.GetGaussian(5, 0.66667), 1)
                        DraftDT.Rows(Num).Item("PTiming") = Math.Round(MT.GetGaussian(5, 0.66667), 1)


                End Select
        End Select

        DraftDT.Rows(Num).Item("Flexibility") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Clutch") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Production") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Consistency") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("TeamPlayer") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Instincts") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Focus") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("PlayStrength") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Durability") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Explosion") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("DeliversBlow") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Leadership") = Math.Round(MT.GetGaussian(6, 0.66667), 1)
        DraftDT.Rows(Num).Item("Character") = Math.Round(MT.GetGaussian(6, 0.66667), 1)

    End Sub

    
    
End Class
