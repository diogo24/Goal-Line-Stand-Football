'generates a schedule for the season using NFL rules
Imports System.IO
Public Class Scheduler
    Dim HomeTeam As New Collection
    Dim AwayTeam As New Collection
    Dim ScheduleH(17, 16) As Integer
    Dim ScheduleA(17, 16) As Integer
    Dim HadToExitSub As Boolean
    Dim GameH As New Collection
    Dim GameA As New Collection
    Dim BoolCheck As Boolean
    Dim StartedOver As Integer
    Dim TeamsLeftToPlay As New Collection
    Dim RedoGame As Boolean
    ''' <summary>
    ''' Creates a schedule following all appropriate NFL Rules
    ''' </summary>
    ''' <param name="NumGames"></param>
    Public Sub GetSchedule(ByVal NumGames As Integer)
        Dim SW As StreamWriter
        SW = New StreamWriter("Schedule.txt")

        Dim SQLString As String = "TeamID int Not NULL, DivID int NOT NULL, ConfID int NOT NULL, TeamFName varchar(20) NOT NULL, TeamLName varchar(20) NOT NULL, LastYearFinish int NOT NULL, DivOutConfID int NOT NULL, DivInConfID int NOT NULL, LYFinishHGSched int NOT NULL, TeamNick char(10) NOT NULL CONSTRAINT Team_ID PRIMARY KEY(TeamID)"
        GetTables.CreateTable(CityInfo, "CityInfo", SQLString)
        GetTables.LoadTable(CityInfo, "CityInfo")

        For i As Integer = 1 To CityInfo.Rows.Count 'fills the DT with the Database information
            InitTeams(i).TeamId = CityInfo.Rows(i - 1).Item("TeamID")
            InitTeams(i).DivID = CityInfo.Rows(i - 1).Item("DivID")
            InitTeams(i).ConfID = CityInfo.Rows(i - 1).Item("ConfID")
            InitTeams(i).TeamFName = CityInfo.Rows(i - 1).Item("TeamFName").ToString.Trim
            InitTeams(i).TeamLName = CityInfo.Rows(i - 1).Item("TeamLName").ToString.Trim
            InitTeams(i).DivFinishLastYear = CityInfo.Rows(i - 1).Item("LastYearFinish")
            InitTeams(i).DivOutConfID = CityInfo.Rows(i - 1).Item("DivOutConfID")
            InitTeams(i).DivInConfID = CityInfo.Rows(i - 1).Item("DivInConfID")
            InitTeams(i).LYFinishHGSched = CityInfo.Rows(i - 1).Item("LYFinishHGSched")
            InitTeams(i).TeamNick = CityInfo.Rows(i - 1).Item("TeamNick")
        Next i

        ScheduleDivGames() 'schedules divisional games for each teams
        ScheduleInConfGames() 'schedules games against the one non-divisional in the same conference here you play all teams
        OutofConfGames() 'schedules the Out of conference games for each team
        InConfFinish() 'schedules the 2 games each team plays against the same place finisher of the other 2 divisions you don't play all the teams in
        GetByeWeek() 'Schdules Bye Weeks for teams


        For i As Integer = 1 To 32
            'Console.WriteLine("Team " & i & " " & InitTeams(i).TeamFName & "")
        Next
        Dim TeamsOnBye As String = ""
        For i As Integer = 1 To 15 'sets the schedule to 15 games since the last 2 divisional games have already been scheduled
            Dim countbye As Integer

            countbye = 0
            For checkbye As Integer = 1 To 32
                If InitTeams(checkbye).ByeWeek = i Then
                    TeamsOnBye += InitTeams(checkbye).TeamNick & ", "
                    countbye += 1
                End If
            Next checkbye
            CreateWeeklySchedule(i, countbye)

            Do While GameH.Count = 0
                CreateWeeklySchedule(i, countbye)
            Loop

            Console.WriteLine("-----------------------------")
            Console.WriteLine("Week " & i & " schedule")
            Console.WriteLine("-----------------------------")

            SW.WriteLine("-----------------------") 'sends output to file
            SW.WriteLine("WEEK " & i & " SCHEDULE")
            SW.WriteLine("-----------------------")

            If TeamsOnBye <> "" Then 'Only runs if teams have a ByeWeek
                Console.WriteLine("Teams On Bye: " & TeamsOnBye.Trim)
                SW.WriteLine("Teams On Bye: " & TeamsOnBye.Trim)
            End If

            TeamsOnBye = ""

            For X As Integer = 1 To GameH.Count
                ScheduleH(i, X) = GameH.Item(X)
                ScheduleA(i, X) = GameA.Item(X)
                Console.WriteLine("" & InitTeams(GameH.Item(X)).TeamNick & "(" & GameH.Item(X) & ") vs. " & InitTeams(GameA.Item(X)).TeamNick & "(" & GameA.Item(X) & ")")
                SW.WriteLine("" & InitTeams(GameH.Item(X)).TeamNick & "(" & GameH.Item(X) & ") vs. " & InitTeams(GameA.Item(X)).TeamNick & "(" & GameA.Item(X) & ")")
            Next X

            'verification only 1 team per week
            For teamcheck As Integer = 1 To 32
                For x As Integer = 1 To GameH.Count
                    Dim countdup As Integer
                    countdup = 0
                    If teamcheck = GameH.Item(x) Or GameA.Item(x) Then
                        countdup += 1
                        If countdup > 1 Then
                            Console.WriteLine("Team " & teamcheck & " Is playing more than 1 game In week " & i & "")
                        End If
                    End If
                Next x
            Next teamcheck

            GameH.Clear() 'clears this weeks slate of games
            GameA.Clear() 'clears this weeks slate of games
            SW.Flush() 'flushes the buffer to prevent truncated outputs
        Next i

    End Sub
    Public Structure Teams
        Dim TeamId As Integer
        Dim TeamFName As String
        Dim TeamLName As String
        Dim DivID As Integer
        Dim ConfID As Integer
        Dim YearNum As Integer
        Dim DivFinishLastYear As Integer
        Dim DivOutConfID As Integer
        Dim DivInConfID As Integer
        Dim ByeWeek As Integer
        Dim SchedID As Integer
        Dim LYFinishHGSched As Integer
        Dim LYFinishAGSched As Integer
        Dim TeamNick As String
    End Structure
    ''' <summary>
    ''' Schedules 1 Home and 1 Away game against each team in the same division
    ''' </summary>
    Private Sub ScheduleDivGames()
        'schedules all the division games
        Dim Teams As New Collection
        Dim Div1 As New Collection
        Dim i As Integer

        For DivID As Integer = 1 To 8 'cycles through the 8 divisions
            Do While Div1.Count < 4 And i < 33 'Number of teams in the division are under 4(4th team already has all games scheduled) and teams are under 33
                If InitTeams(i).DivID = DivID Then 'checks to see if that team is in the division
                    Div1.Add((InitTeams(i).TeamId)) 'Adds team to the count of teams in the division.
                End If
                i += 1
            Loop
            HomeTeam.Add(Div1.Item(1)) 'adds 3 homes games for team
            HomeTeam.Add(Div1.Item(1))
            HomeTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2)) 'adds away games
            AwayTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(2))
            HomeTeam.Add(Div1.Item(2))
            HomeTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(3))
            HomeTeam.Add(Div1.Item(3))
            HomeTeam.Add(Div1.Item(3))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            HomeTeam.Add(Div1.Item(4))
            AwayTeam.Add(Div1.Item(1))
            AwayTeam.Add(Div1.Item(2))
            AwayTeam.Add(Div1.Item(3))
            'Teams.Add(1)

            EndWithDivisionGames(Div1.Item(1), Div1.Item(2), Div1.Item(3), Div1.Item(4), Teams)


            Div1.Clear()
            i = 0
        Next DivID


    End Sub
    ''' <summary>
    ''' Each division plays one other division in the same conference each year.
    ''' </summary>
    Private Sub ScheduleInConfGames()
        Dim div1 As New Collection
        Dim div2 As New Collection
        Dim i As Integer
        Dim Sched(8) As Boolean
        Dim oppdiv As Integer

        For DivID As Integer = 1 To 8

            If Sched(DivID) = False Then
                Sched(DivID) = True
                Do While div1.Count < 4 And i < 33
                    If InitTeams(i).DivID = DivID Then
                        div1.Add(InitTeams(i).TeamId)
                        oppdiv = InitTeams(i).DivInConfID
                    End If
                    i += 1
                Loop
                i = 0
                Do Until div2.Count = 4 And i > 32
                    If InitTeams(i).DivID = oppdiv Then
                        div2.Add(InitTeams(i).TeamId)
                    End If
                    i += 1
                Loop
                Sched(oppdiv) = True
                'randomize the teams positions in the divisions
                Dim div3 As New Collection
                Dim div4 As New Collection

                For count As Integer = 1 To div1.Count
                    Dim Mixup1 As Integer = MT.GenerateInt32(1, div1.Count)
                    Dim Mixup2 As Integer = MT.GenerateInt32(1, div2.Count)
                    div3.Add(div1.Item(Mixup1))
                    div4.Add(div2.Item(Mixup2))
                    div1.Remove(Mixup1)
                    div2.Remove(Mixup2)
                Next count
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(1))

                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(2))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(2))

                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(3))
                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(3))

                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(4))
            Else
            End If
            div1.Clear()
            div2.Clear()
            i = 0
        Next DivID

    End Sub

    ''' <summary>
    ''' Schedules out of conference games for each division against another division in the other conference(ie, AFC East Vs. NFC South)
    ''' </summary>
    Private Sub OutofConfGames()
        Dim div1 As New Collection
        Dim div2 As New Collection
        Dim i As Integer
        Dim Sched(8) As Boolean
        Dim oppdiv As Integer

        For DivID As Integer = 1 To 8

            If Sched(DivID) = False Then
                Sched(DivID) = True
                Do While div1.Count < 4 And i < 33
                    If InitTeams(i).DivID = DivID Then
                        div1.Add(InitTeams(i).TeamId)
                        oppdiv = InitTeams(i).DivOutConfID
                    End If
                    i += 1
                Loop

                i = 0
                Do Until div2.Count = 4 And i > 32
                    If InitTeams(i).DivID = oppdiv Then
                        div2.Add(InitTeams(i).TeamId)
                    End If
                    i += 1
                Loop
                Sched(oppdiv) = True
                'randomize the teams positions in the divisions
                Dim div3 As New Collection
                Dim div4 As New Collection

                For count As Integer = 1 To div1.Count
                    Dim Mixup1 As Integer = MT.GenerateInt32(1, div1.Count)
                    Dim Mixup2 As Integer = MT.GenerateInt32(1, div2.Count)
                    div3.Add(div1.Item(Mixup1))
                    div4.Add(div2.Item(Mixup2))
                    div1.Remove(Mixup1)
                    div2.Remove(Mixup2)
                Next count
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(1))
                HomeTeam.Add(div3.Item(1))
                AwayTeam.Add(div4.Item(1))

                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(2))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div3.Item(2))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(2))

                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(3))
                HomeTeam.Add(div4.Item(1))
                AwayTeam.Add(div3.Item(3))
                HomeTeam.Add(div3.Item(3))
                AwayTeam.Add(div4.Item(2))
                HomeTeam.Add(div4.Item(4))
                AwayTeam.Add(div3.Item(3))

                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(1))
                HomeTeam.Add(div4.Item(2))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div4.Item(3))
                AwayTeam.Add(div3.Item(4))
                HomeTeam.Add(div3.Item(4))
                AwayTeam.Add(div4.Item(4))
            Else
            End If
            div1.Clear()
            div2.Clear()
            i = 0
        Next DivID
    End Sub
    '''<summary>In the NFL, each team plays 12 games in their own conference. 6 games are played against teams in their division(the teams play twice),
    '''4 games are played against a single other division(each team played once), and the remaining two games are played against the other 2 divisions' teams
    '''that finished in the same place in their division(ie, 3rd place team from Div1 plays 3rd place team from Div3 and Div4 because they already are playing</summary> 
    Private Sub InConfFinish()


        Dim HTeam As New Collection
        Dim ATeam As New Collection
        Dim RunOnce As Boolean = False

        For Team As Integer = 1 To 32 'cycle through the teams in the conference
            'Do While 1 < 33
            For OppTeam As Integer = 1 To 32 'cycle through the teams in the conference
                If InitTeams(Team).DivInConfID <> InitTeams(OppTeam).DivID And Team < OppTeam And InitTeams(Team).DivID <> InitTeams(OppTeam).DivID And
                    InitTeams(Team).DivFinishLastYear = InitTeams(OppTeam).DivFinishLastYear And InitTeams(Team).ConfID = InitTeams(OppTeam).ConfID Then
                    'selects teams in the same conference, Not in the same division, who aren't playing all teams in that division already and finds teams with the same records

                    If HTeam.Count = 16 Then 'Conference Change
                        RunOnce = False
                    End If

                    If RunOnce = False Then
                        Dim rndInt As Integer = MT.GenerateInt32(0, 100)
                        If rndInt < 51 Then 'chooses first game to be home game
                            HTeam.Add(Team)
                            ATeam.Add(OppTeam)
                            InitTeams(Team).LYFinishHGSched = 1
                            InitTeams(OppTeam).LYFinishAGSched = 1
                            Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                        Else
                            HTeam.Add(OppTeam)
                            ATeam.Add(Team)
                            InitTeams(OppTeam).LYFinishHGSched = 1
                            InitTeams(Team).LYFinishAGSched = 1
                            Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                        End If

                    Else
                        For m As Integer = 1 To HTeam.Count  'checks to see if the team already has a homegame scheduled
                            'Dim Check As Boolean = False
                            If (m = Team Or ATeam.Item(m) = OppTeam) And InitTeams(m).LYFinishAGSched = 0 And InitTeams(m).LYFinishHGSched = 0 And InitTeams(OppTeam).LYFinishHGSched = 0 And InitTeams(OppTeam).LYFinishAGSched = 0 Then
                                Dim rndInt As Integer = MT.GenerateInt32(0, 100)
                                If rndInt < 51 Then 'chooses first game to be home game
                                    HTeam.Add(Team)
                                    ATeam.Add(OppTeam)
                                    InitTeams(Team).LYFinishHGSched = 1
                                    InitTeams(OppTeam).LYFinishAGSched = 1
                                    Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                                    'Exit For
                                Else
                                    HTeam.Add(OppTeam)
                                    ATeam.Add(Team)
                                    InitTeams(OppTeam).LYFinishHGSched = 1
                                    InitTeams(Team).LYFinishAGSched = 1
                                    Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                                    'Exit For
                                End If

                            ElseIf (m = Team Or ATeam.Item(m) = OppTeam) And InitTeams(OppTeam).LYFinishHGSched <> 1 And InitTeams(m).LYFinishAGSched = 0 Then 'This team already has a home game scheduled, must be the away team
                                HTeam.Add(OppTeam)
                                ATeam.Add(Team)
                                InitTeams(OppTeam).LYFinishHGSched = 1
                                InitTeams(Team).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                                Exit For
                            ElseIf (m = Team Or HTeam.Item(m) = OppTeam) And InitTeams(m).LYFinishHGSched <> 1 And InitTeams(OppTeam).LYFinishAGSched = 0 Then 'This team already has an away game scheduled, must be the away team
                                HTeam.Add(Team)
                                ATeam.Add(OppTeam)
                                InitTeams(Team).LYFinishHGSched = 1
                                InitTeams(OppTeam).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                                Exit For
                            ElseIf m = Team And InitTeams(m).LYFinishAGSched = 1 And InitTeams(OppTeam).LYFinishAGSched = 0 Then 'team must be home team
                                HTeam.Add(Team)
                                ATeam.Add(OppTeam)
                                InitTeams(Team).LYFinishHGSched = 1
                                InitTeams(OppTeam).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(Team).TeamFName & " vs. " & InitTeams(OppTeam).TeamFName)
                            ElseIf m = Team And InitTeams(OppTeam).LYFinishAGSched = 1 And InitTeams(m).LYFinishAGSched = 0 Then
                                HTeam.Add(OppTeam)
                                ATeam.Add(Team)
                                InitTeams(OppTeam).LYFinishHGSched = 1
                                InitTeams(Team).LYFinishAGSched = 1
                                Console.WriteLine(InitTeams(OppTeam).TeamFName & " vs. " & InitTeams(Team).TeamFName)
                            End If
                        Next m
                    End If
                End If

                If RunOnce = False And HTeam.Count <> 0 Then
                    RunOnce = True
                End If
            Next OppTeam
        Next Team

        For i As Integer = 1 To HTeam.Count 'adds games to the schedule.
            HomeTeam.Add(HTeam.Item(i))
            AwayTeam.Add(ATeam.Item(i))
        Next i
    End Sub
    Private Sub CreateWeeklySchedule(ByVal WeekNum As Integer, ByVal NumByeTeams As Integer)
        'runs through the weekly schedule
        Dim count As Integer
        Dim Team As Integer = 1
        HadToExitSub = False

        count = 1
        For games As Integer = 1 To (16 - (NumByeTeams / 2))
            'schedules the games for the week

            Dim PickGameNum As Integer
            PickGameNum = MT.GenerateInt32(1, HomeTeam.Count) 'picks a random game to schedule


            For i As Integer = 1 To 32
                If InitTeams(i).ByeWeek = WeekNum And (InitTeams(i).TeamId = HomeTeam.Item(PickGameNum) Or _
                InitTeams(i).TeamId = AwayTeam.Item(PickGameNum)) Then
                    PickGameNum = MT.GenerateInt32(1, HomeTeam.Count)
                    i = 0
                End If
            Next i

            If GameH.Count > 0 Then 'runs if there has been a game scheduled already this week
                CanGameBePlayed(WeekNum, games) 'checks to see if every team has at least 1 game that can be played

                If RedoGame = True Then 'redogame is a boolean flag meaning it restarted the weekly schedule
                    If StartedOver > 30 Then
                        games = 1
                        RedoGame = False
                        StartedOver = 0
                    Else
                        games -= 1
                        RedoGame = False
                    End If
                End If
                If games <= (16 - (NumByeTeams / 2)) Then
                    count = 1
                    For teamid As Integer = 1 To GameH.Count
                        'ensures only a game that actually can be played is chosen
                        Do While HomeTeam.Item(PickGameNum) = GameH.Item(teamid) Or HomeTeam.Item(PickGameNum) = _
                        GameA.Item(teamid) Or AwayTeam.Item(PickGameNum) = GameH.Item(teamid) Or _
                        AwayTeam.Item(PickGameNum) = GameA.Item(teamid) Or InitTeams(HomeTeam.Item(PickGameNum)).ByeWeek = WeekNum Or _
                        InitTeams(AwayTeam.Item(PickGameNum)).ByeWeek = WeekNum
                            PickGameNum = MT.GenerateInt32(1, HomeTeam.Count)
                            teamid = 1
                            count += 1
                            If count > 1750 Then
                                For i As Integer = 1 To GameH.Count
                                    HomeTeam.Add(GameH.Item(i))
                                    AwayTeam.Add(GameA.Item(i))
                                Next i
                                GameH.Clear()
                                GameA.Clear()
                                Exit Sub
                            End If
                        Loop
                    Next teamid

                End If
            End If
            If count > 1750 Then
                count = 0
                Team = 1
            Else
                GameH.Add(HomeTeam.Item(PickGameNum))
                GameA.Add(AwayTeam.Item(PickGameNum))
                HomeTeam.Remove(PickGameNum)
                AwayTeam.Remove(PickGameNum)
            End If
        Next games

    End Sub
    '''<summary>creates a schedule where the last 2 games in the season are divisional games. 
    '''currently in the NFL the last 2 games each teams play every year are games within
    '''the division. Takes the 4 Div teams from ScheduleDivGames and then schedules 2 games checking 
    '''to make sure they both can be played and are not both the same game</summary>
    Private Function EndWithDivisionGames(ByVal Team1 As Integer, ByVal Team2 As Integer, ByVal Team3 As Integer, ByVal Team4 As Integer, ByVal Teams As Collection) As Collection

        Teams.Add(Team1)
        Teams.Add(Team2)
        Teams.Add(Team3)
        Teams.Add(Team4)

        Dim HTeam As New Collection
        Dim ATeam As New Collection
        Dim PickHome As Integer
        Dim PickHome2 As Integer
        Dim PickAway As Integer
        Dim PickAway2 As Integer

        'schedules the games
        PickHome = MT.GenerateInt32(1, Teams.Count) 'randomly chooses the home team
        PickHome2 = PickHome 'temp stores the integer for easy removal from the list
        PickHome = Teams.Item(PickHome)
        Teams.Remove(PickHome2) 'removes this team from the list

        PickAway = MT.GenerateInt32(1, Teams.Count) 'randomly chooses the away team from the remaining teams
        PickAway2 = PickAway
        PickAway = Teams.Item(PickAway)
        Teams.Remove(PickAway2)
        Console.WriteLine("Team " & PickHome & " vs. " & PickAway)
        HTeam.Add(PickHome)
        ATeam.Add(PickAway)

        PickHome = MT.GenerateInt32(1, Teams.Count) 'Now need to randomly make one of the reamining teams the home team
        PickHome2 = PickHome
        PickHome = Teams.Item(PickHome)
        Teams.Remove(PickHome2)
        PickAway = Teams.Item(1) 'only team left in the list
        Teams.Clear() 'removes last team
        Console.WriteLine("Team " & PickHome & " vs. " & PickAway)
        HTeam.Add(PickHome)
        ATeam.Add(PickAway) 'Schedules the first games
        '---------------------------------------------------------------------------------Scheduling the 2nd games------------------------------

        Teams.Add(ATeam.Item(1)) 'adds the away teams back in to schedule the second divisional game---these teams MUST be the home team now!
        Teams.Add(ATeam.Item(2))

        PickAway = MT.GenerateInt32(1, HTeam.Count) 'chooses an away team from the teams that were the home team previously
        PickAway2 = PickAway
        PickAway = HTeam.Item(PickAway) 'takes a home team from the previous week and makes it an away team this week
        Teams.Remove(PickAway2) 'removes this team from the list

        PickHome = MT.GenerateInt32(1, Teams.Count) 'randomly chooses a home team---need to make sure it picks a team that it didn't play the week before...

        'Teams.Add(HTeam.Item(1)) 'adds in other teams to give a full list of divisional opponents to choose game from
        'Teams.Add(HTeam.Item(2))

        For i As Integer = 1 To HTeam.Count 'cycles through the HTeam.list

            While HTeam(i) = PickAway And ATeam(i) = PickHome And PickAway = PickHome 'continues to re-choose team if teams have already played
                PickHome = MT.GenerateInt32(1, Teams.Count)
            End While
        Next i

        PickHome2 = PickHome
        PickHome = Teams.Item(PickHome)
        Teams.Remove(PickHome2)
        HTeam.Add(PickHome)
        ATeam.Add(PickAway)

        Dim T1 As Boolean = False
        Dim T2 As Boolean = False
        Dim T3 As Boolean = False
        Dim T4 As Boolean = False
        Console.WriteLine("Team " & PickHome & " vs. " & PickAway)
        Teams.Clear()

        For i As Integer = 1 To HTeam.Count
            If HTeam.Item(i) = Team1 Then
                T1 = True
            ElseIf HTeam.Item(i) = Team2 Then
                T2 = True
            ElseIf HTeam.Item(i) = Team3 Then
                T3 = True
            ElseIf HTeam.Item(i) = Team4 Then
                T4 = True
            End If
        Next i

        If T1 = False Then HTeam.Add(Team1)
        If T2 = False Then HTeam.Add(Team2)
        If T3 = False Then HTeam.Add(Team3)
        If T4 = False Then HTeam.Add(Team4)

        T1 = False
        T2 = False
        T3 = False
        T4 = False

        For i As Integer = 1 To ATeam.Count
            If ATeam.Item(i) = Team1 Then
                T1 = True
            ElseIf ATeam.Item(i) = Team2 Then
                T2 = True
            ElseIf ATeam.Item(i) = Team3 Then
                T3 = True
            ElseIf ATeam.Item(i) = Team4 Then
                T4 = True
            End If
        Next i

        If T1 = False Then ATeam.Add(Team1)
        If T2 = False Then ATeam.Add(Team2)
        If T3 = False Then ATeam.Add(Team3)
        If T4 = False Then ATeam.Add(Team4)

        Console.WriteLine("Team " & HTeam.Item(4) & " vs. " & ATeam.Item(4))
        ' ATeam.Add(Teams(1))

        Teams.Add(HTeam)
        Teams.Add(ATeam)
        Return Teams 'returns a collection of collections

    End Function
    Private Sub GetByeWeek()
        'Creates 5 Weeks with 4 teams having byes and 2 weeks with 6 teams having byes
        Dim ByeWeek As New Collection
        Dim AlreadyHadBye As Boolean 'True if team already had bye

        Dim SixTeams1 As Integer = MT.GenerateInt32(4, 10)
        Dim SixTeams2 As Integer = MT.GenerateInt32(4, 10)

        Do While SixTeams1 = SixTeams2
            SixTeams2 = MT.GenerateInt32(4, 10)
        Loop

        For week As Integer = 4 To 10 'Bye weeks are week 4 thru 10
            Dim count As Integer = 0
            If SixTeams1 = week Or SixTeams2 = week Then 'Six teams are on bye this week
                Do Until count = 6
                    Dim Team As Integer = MT.GenerateInt32(1, 32)
                    If ByeWeek.Count > 0 Then
                        For check As Integer = 1 To ByeWeek.Count
                            If ByeWeek.Item(check) = Team Then
                                AlreadyHadBye = True
                                Exit For
                            End If
                        Next check
                        If AlreadyHadBye = False Then
                            InitTeams(Team).ByeWeek = week
                            ByeWeek.Add(Team)
                            count += 1
                        End If
                    Else
                        If AlreadyHadBye = False Then
                            InitTeams(Team).ByeWeek = week
                            ByeWeek.Add(Team)
                            count += 1
                        End If
                    End If
                    AlreadyHadBye = False
                Loop
            Else 'Four teams are on bye this week
                Do Until count = 4
                    Dim Team As Integer = MT.GenerateInt32(1, 32)
                    If ByeWeek.Count > 0 Then
                        For check As Integer = 1 To ByeWeek.Count
                            If ByeWeek.Item(check) = Team Then
                                AlreadyHadBye = True
                                Exit For
                            End If
                        Next check
                        If AlreadyHadBye = False Then
                            InitTeams(Team).ByeWeek = week
                            ByeWeek.Add(Team)
                            count += 1
                        End If
                    Else
                        If AlreadyHadBye = False Then
                            InitTeams(Team).ByeWeek = week
                            ByeWeek.Add(Team)
                            count += 1
                        End If
                    End If
                    AlreadyHadBye = False
                Loop
            End If
        Next week
    End Sub
    Private Sub CanGameBePlayed(ByVal weeknum As Integer, ByVal games As Integer)
        Dim team As Integer
        team = 1
        Do Until team = 33
            For Gamecheck As Integer = 1 To GameH.Count
                If team = GameH.Item(Gamecheck) Or team = GameA.Item(Gamecheck) Then
                    BoolCheck = True
                    Exit For
                End If
            Next Gamecheck

            If InitTeams(team).ByeWeek = weeknum Then
                BoolCheck = True
            End If

            If BoolCheck = False Then

                For i As Integer = 1 To HomeTeam.Count 'cycle thru all scheduled games

                    If HomeTeam.Item(i) = team Then
                        If HomeTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
                            TeamsLeftToPlay.Add(HomeTeam.Item(i))
                        ElseIf AwayTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
                            TeamsLeftToPlay.Add(AwayTeam.Item(i))
                        End If
                    End If

                    If AwayTeam.Item(i) = team Then
                        If HomeTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
                            TeamsLeftToPlay.Add(HomeTeam.Item(i))
                        ElseIf AwayTeam.Item(i) <> team And InitTeams(team).ByeWeek <> weeknum Then
                            TeamsLeftToPlay.Add(AwayTeam.Item(i))
                        End If
                    End If
                Next i

                For Runcheck As Integer = 1 To GameH.Count 'cycle thru games scheduled for the week

                    Dim CheckTeam As Integer = 1

                    Do While CheckTeam <= TeamsLeftToPlay.Count

                        If GameH.Item(Runcheck) = TeamsLeftToPlay.Item(CheckTeam) _
                        Or GameA.Item(Runcheck) = TeamsLeftToPlay.Item(CheckTeam) Then
                            TeamsLeftToPlay.Remove(CheckTeam)
                            CheckTeam = 0
                        End If
                        CheckTeam += 1
                    Loop

                Next Runcheck
                If TeamsLeftToPlay.Count < 1 And GameH.Count > 0 Then 'no teams left to play for this team,
                    'must restart the scheduler for the week.
                    StartedOver += 1
                    'increments a counter to redo the weekly schedule from the beginning, as this attempted schedule is not working for whatever reason
                    If StartedOver > 30 Then
                        For i As Integer = 1 To GameH.Count
                            HomeTeam.Add(GameH.Item(i))
                            AwayTeam.Add(GameA.Item(i))
                        Next i
                        GameH.Clear()
                        GameA.Clear()
                    Else
                        HomeTeam.Add(GameH.Item(GameH.Count))
                        AwayTeam.Add(GameA.Item(GameH.Count))
                        GameH.Remove(GameH.Count)
                        GameA.Remove(GameA.Count)
                    End If
                    RedoGame = True 'flags boolean
                    TeamsLeftToPlay.Clear()
                    BoolCheck = False
                    Exit Do
                End If
            End If
            If team = 32 Then
                Exit Do
            End If
            team += 1
            TeamsLeftToPlay.Clear()
            BoolCheck = False
        Loop
    End Sub

End Class
