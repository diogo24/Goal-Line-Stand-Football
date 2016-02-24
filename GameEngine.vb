Public Class GameEngine

    Public Structure Weather
        Dim name As Integer
        Shared Temp As Integer
        Shared WindSpd As Integer
        Shared WindDir As Integer
        Shared WeatherCondition As CloudCover
    End Structure
    Public Enum CloudCover
        Sunny = 1
        PartlyCloudy = 2
        MostlyCloudy = 3
        Showers = 4
        Rain = 5
        SnowShowers = 6
        Snow = 7
        Fog = 8
        Haze = 9
    End Enum

    Public Class RegKickoff
        Public KickerName As String
        Public KickingTeam As String
        Public ReceivingTeam As String
        Public DeepMan As String
        Public Stadium As String
        Public City As String
        Public State As String
    End Class
    Public Class OffensePlays

    End Class

    Public Sub InGame()
        Dim Home As Integer = MT.GenerateInt32(1, 32)
        Dim Away As Integer = MT.GenerateInt32(1, 32)
        'Dim GetPosRow As Integer
        GetTables.LoadTable(CityInfo, "CityInfo") 'loads cityinfo table into memory
        GetTables.LoadTable(PlayerDT, "Players")
        GetTables.LoadTable(CoachDT, "Coaches")
        GetDec.GetWeather(5, DatePart(DateInterval.Month, Today), CityInfo)
        GetDec.CoinTossDecision(1, 4)
        LoadGameTeams(Home, Away)
        RaiseEvent RegularKickoff(Home, Away, Kick)
        'RaiseEvent KickoffReturn(Home, Away, KickReturn)



    End Sub
    Public Event RegularKickoff(ByVal home As Integer, ByVal away As Integer, ByVal e As RegKickoff)
    Dim Kick As New RegKickoff
    Public Event OffenseOnField(ByVal home As Integer, ByVal away As Integer, ByVal e As OffensePlays)
    Dim Offense As New OffensePlays

    Private Function GetPosRow(ByVal DT As DataTable, ByVal Pos As String) As Integer
        For i As Integer = 1 To HomeTeamDT.Rows.Count - 1
            If DT.Rows(i).Item("CollegePos") = Pos Then
                Return i
            End If
        Next i
    End Function

    Private Sub KickoffHandler(ByVal home As Integer, ByVal away As Integer, ByVal e As RegKickoff) Handles Me.RegularKickoff
        Static OpeningKickoff As Boolean

        If OpeningKickoff = False Then
            Kick.KickerName = HomeTeamDT.Rows(GetPosRow(HomeTeamDT, "K")).Item("LName")
            Kick.KickingTeam = CityInfo.Rows(home).Item("TeamFName")
            Kick.ReceivingTeam = CityInfo.Rows(away).Item("TeamFName")
            Kick.Stadium = CityInfo.Rows(home).Item("StadiumName")
            Kick.City = CityInfo.Rows(home).Item("CityName")
            Kick.State = CityInfo.Rows(home).Item("State")
            Console.WriteLine("" & StrConv(Kick.KickingTeam, VbStrConv.ProperCase).Trim & " kicker " _
                              & StrConv(Kick.KickerName, VbStrConv.ProperCase) & " is ready to kick it off and get this game underway...")
            Console.WriteLine("" & StrConv(Kick.ReceivingTeam, VbStrConv.ProperCase).Trim & " back to receive the opening kickoff...And we are underway from beautiful " & StrConv(Kick.Stadium, VbStrConv.ProperCase).Trim & " in downtown " & StrConv(Kick.City, VbStrConv.ProperCase).Trim & "," & Kick.State & "")
            OpeningKickoff = True
        Else 'Instructions for normal kickoff

        End If

    End Sub

    Private Sub LoadGameTeams(ByVal HomeTeam As Integer, ByVal AwayTeam As Integer)
        'loads the in-game DB for the teams playing right now
        GetTables.LoadTable(HomeTeamDT, "Players")
        GetTables.LoadTable(AwayTeamDT, "Players")
        'Loads PlayerDT into hometeam and awayteam DT, then loops thru and removes rows that don't match their respective teams
        'since both DBs are the same, cycling thru one time can remove all incorrect rows
        For i As Integer = 1 To HomeTeamDT.Rows.Count - 1
            If HomeTeamDT.Rows(i).Item("TeamID") Is DBNull.Value Then
                HomeTeamDT.Rows(i).Delete()
            ElseIf HomeTeamDT.Rows(i).Item("TeamID") <> HomeTeam Then
                HomeTeamDT.Rows(i).Delete()
            End If
        Next i

        For i As Integer = 1 To AwayTeamDT.Rows.Count - 1
            If AwayTeamDT.Rows(i).Item("TeamID") Is DBNull.Value Then
                AwayTeamDT.Rows(i).Delete()
            ElseIf AwayTeamDT.Rows(i).Item("TeamID") <> AwayTeam Then
                AwayTeamDT.Rows(i).Delete()
            End If
        Next
        HomeTeamDT.AcceptChanges()
        AwayTeamDT.AcceptChanges()
    End Sub





End Class
