Imports System.Text.RegularExpressions
Imports System.IO

Public Class PBP
    Private Reg As Regex
    Private i As Integer
    Private EOF As String
    Private Time1 As TimeSpan
    Private Time2 As TimeSpan
    Private Team1 As String
    Private Team2 As String
    Private PrevTeam As String
    Private PrevQuart As Integer
    Private TmScore1 As Integer
    Private TmScore2 As Integer
    Private TeamWithBall As String
    Private PointDiff As Integer
    Private SQL As String

    Public Sub GetPBP(ByVal FileName As String)
        ReadPBP(Filename)
    End Sub
    
    Private Sub ReadPBP(ByVal FileName As String)
        'Dim InitTxt As FileStream = New FileStream("pbp2002.txt", FileMode.Open)
        Dim SR As StreamReader = New StreamReader(FileName)

        Do Until SR.EndOfStream = True 'Loop until the end of the file
            EOF = SR.ReadLine
            'Console.WriteLine(EOF)
            PBPDT.Rows.Add(i)

            'Get Team With Ball
            If Regex.Match(EOF, "(?<=\p{L}{3,}.*)\w{4,}(?=\ at \d{1,2}\:\d{1,2})").Success Then


                TeamWithBall = TeamAbbrev(Regex.Match(EOF, "(?<=\p{L}{3,}.*)\w{4,}(?=\ at \d{1,2}\:\d{1,2})").ToString)
            End If

            'Get Quarter of play
            If Regex.Match(EOF, "Quarter", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("Quarter") = GetQuarter()
                PrevQuart = PBPDT.Rows(i).Item("Quarter")
            ElseIf Regex.Match(EOF, "Overtime").Success Then
                PBPDT.Rows(i).Item("Quarter") = 5
            Else : PBPDT.Rows(i).Item("Quarter") = PrevQuart 'gets the quarter
            End If

            'Grab Possession Change:
            If Regex.Match(EOF, "(?<=\w{4,}\ at\ )\d{0,2}\:\d{2}").Success Then
                GetPossChange()
            ElseIf Regex.Match(EOF, "First Quarter").Success Then
                Team1 = Nothing
                Team2 = Nothing
                TmScore1 = Nothing
                TmScore2 = Nothing
            Else : PBPDT.Rows(i).Item("TeamAbbrev") = PrevTeam 'continues possession by that team
            End If

            'Grab KO Data
            If Regex.Match(EOF, "kicks", RegexOptions.IgnoreCase).Success Then
                GetKO()
            End If

            'Grab Punt
            If Regex.Match(EOF, "punts").Success Then
                GetPunt()
            End If

            'Grab Downed
            If Regex.Match(EOF, "downed").Success Then
                PBPDT.Rows(i).Item("PuntDowned") = Regex.Match(EOF, "(?<=downed by \p{L}{2,3}\-)\p{L}+\.\p{L}+").ToString
            End If

            'Grab Fair Catch
            If Regex.Match(EOF, "fair catch").Success Then
                PBPDT.Rows(i).Item("FairCatch") = Regex.Match(EOF, "(?<=fair catch by )\p{L}+\.\p{L}+").ToString
            End If

            'Grab KO OOB
            If Regex.Match(EOF, "(?<!\(.*)(?<=\b\w+\.\w+\ kicks .*)\, out of bounds").Success Then
                PBPDT.Rows(i).Item("KOOOB") = Regex.Match(EOF, "(?<!\(.*)\b\w+\.\w+(?=\ kicks .*\, out of bounds)").ToString
            End If

            'Grab Touchback:
            If Regex.Match(EOF, "Touchback", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("Touchback") = "TB"
            End If

            'Grab Run OOB
            If Regex.Match(EOF, "ran ob", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("RanOOB") = "RanOOB"
            End If

            'Grab Pushed OOB
            If Regex.Match(EOF, "pushed ob", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("PushedOOB") = "PushedOOB"
            End If

            'Grab Safety
            If Regex.Match(EOF, "SAFETY").Success Then
                GetSafety()
            End If

            'Grab Touchdown
            If Regex.Match(EOF, "TOUCHDOWN").Success Then
                PBPDT.Rows(i).Item("Touchdown") = "TD"
            End If
            'Grab 2 Point Conversions
            If Regex.Match(EOF, "TWO-POINT").Success Then
                get2Point()
            End If

            'Grab INT
            If Regex.Match(EOF, "INTERCEPTED").Success Then
                GetInt()
            End If
            'Grab FG Data
            If Regex.Match(EOF, "yard field goal is").Success Then
                GetFG()
            End If

            'Grab XP Data
            If Regex.Match(EOF, "extra point").Success Then
                GetXP()
            End If

            'Get Sack Data
            If Regex.Match(EOF, "sacked").Success Then
                PBPDT.Rows(i).Item("Sacked") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ sacked)").ToString
                If Regex.Match(EOF, "FUMBLES").Success Then
                    PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("sacked")
                End If
            End If

            'Get Muff
            If Regex.Match(EOF, "MUFFS").Success Then
                PBPDT.Rows(i).Item("PuntMuff") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ MUFFS)").ToString
                GetMuff()
            End If

            'Get Down
            If Regex.Match(EOF, "\d(?=\-\d{1,})", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("Down") = CInt(Regex.Match(EOF, "\d(?=\-\d{1,})", RegexOptions.IgnoreCase).Value)
            End If

            'Get Distance
            If Regex.Match(EOF, "(?<=\d\-)\d{1,2}(?=\-)", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("Dist") = CInt(Regex.Match(EOF, "(?<=\d\-)\d{1,2}(?=\-)", RegexOptions.IgnoreCase).Value)
            End If

            'Get YardLine
            GetYardline()

            'Get TimeLeft
            If Regex.Match(EOF, "\d{0,2}\:\d{2}(?=.*)", RegexOptions.IgnoreCase).Success Then
                If Regex.Match(EOF, "\d{1,2}(?=\:\d{2}\).*)").Success Then
                    PBPDT.Rows(i).Item("MinLeft") = CInt(Regex.Match(EOF, "\d{1,2}(?=\:\d{2}\).*)", RegexOptions.IgnoreCase).Value)
                    PBPDT.Rows(i).Item("secLeft") = CInt(Regex.Match(EOF, "(?<=\d{1,2}\:)\d{2}(?=\).*)", RegexOptions.IgnoreCase).Value)
                ElseIf Regex.Match(EOF, "\:\d{2}\)\ \w+").Success Then
                    PBPDT.Rows(i).Item("MinLeft") = 0
                    PBPDT.Rows(i).Item("secLeft") = CInt(Regex.Match(EOF, "(?<=\:)\d{2}(?=\).*)", RegexOptions.IgnoreCase).Value)
                End If
            End If

                If Regex.Match(EOF, "(?<=\()Shotgun", RegexOptions.IgnoreCase).Success Then
                    PBPDT.Rows(i).Item("Shotgun") = "SG"
                End If

                'Get Type of Play
                GetPlay()

                'Get Tackler(s) involved in the plays
                GetTackler()

                'Grab Challenge
                If Regex.Match(EOF, "Challenge").Success Then
                    GetChallenge()
                End If
                'Get an accepted Penalty
                If Regex.Match(EOF, "PENALTY").Success Then
                    GetPenaltyAccept()
                End If

                'Get a declined penalty
                If Regex.Match(EOF, "Penalty").Success Then
                    GetPenaltyDecline()
                End If

                'Get kneel down
                If Regex.Match(EOF, "kneels").Success Then
                    PBPDT.Rows(i).Item("kneeldown") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ kneels)").ToString
                End If

                'Get offsetting penalties
                If Regex.Match(EOF, "offsetting").Success Then
                    PBPDT.Rows(i).Item("PenTeam") = "Offsetting"
                End If

                'Grabs the time between plays
                PBPDT.Rows(i).Item("TimeBetPlays") = TimeBetPlays()

                'Grab Num Plays on scoring drive
                If Regex.Match(EOF, "(?<=Plays: )\d+").Success Then
                    PBPDT.Rows(i).Item("NumPlays") = CInt(Regex.Match(EOF, "(?<=Plays: )\d+").Value)
                End If

                'Grab Yards on Drive
                If Regex.Match(EOF, "(?<=Yards: )\d+").Success Then
                    PBPDT.Rows(i).Item("DriveYards") = CInt(Regex.Match(EOF, "(?<=Yards: )\d+").Value)
                End If

                'Grab TOP
                If Regex.Match(EOF, "(?<=Possession: )\d{0,2}\:\d{2}").Success Then
                    PBPDT.Rows(i).Item("DriveTime") = Regex.Match(EOF, "(?<=Possession: )\d{0,2}\:\d{2}").ToString
                    GetPointDiff()
                Else
                    PBPDT.Rows(i).Item("PointDiff") = PointDiff
                End If

                'Get Fumble Data
                If Regex.Match(EOF, "FUMBLES").Success Then
                    GetFumble()
                End If

                i += 1
        Loop

    End Sub

    Private Function GetQuarter() As Integer

        If Regex.Match(EOF, "First", RegexOptions.IgnoreCase).Success Then
            Return 1
        ElseIf Regex.Match(EOF, "Second", RegexOptions.IgnoreCase).Success Then
            Return 2
        ElseIf Regex.Match(EOF, "Third", RegexOptions.IgnoreCase).Success Then
            Return 3
        ElseIf Regex.Match(EOF, "Fourth", RegexOptions.IgnoreCase).Success Then
            Return 4
       
        End If

    End Function
    Private Function TimeBetPlays() As String
        Dim TimeBet As TimeSpan
        If Regex.Match(EOF, "Quarter", RegexOptions.IgnoreCase).Success Then
            Time1 = TimeSpan.Parse("0:15:00").Duration
        ElseIf Regex.Match(EOF, "(?<=\()\d{0,2}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).Success Then
            If Time2 <> Nothing Then
                If Regex.Match(EOF, "(?<=\()\d{1,}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).Success Then
                    Time1 = TimeSpan.Parse("0:" & Regex.Match(EOF, "(?<=\()\d{0,2}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).ToString)
                ElseIf Regex.Match(EOF, "(?<=\()\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).Success Then
                    Time1 = TimeSpan.Parse("0:0" & Regex.Match(EOF, "(?<=\()\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).ToString)
                End If
            Else
                If Regex.Match(EOF, "(?<=\()\d{1}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).Success Then
                    Time2 = TimeSpan.Parse("0:" & Regex.Match(EOF, "(?<=\()\d{0,2}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).ToString)
                ElseIf Regex.Match(EOF, "(?<=\()\d{0,2}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).Success Then
                    Time2 = TimeSpan.Parse("0:0" & Regex.Match(EOF, "(?<=\()\d{0,2}\:\d{2}(?=\)\ \w+)", RegexOptions.IgnoreCase).ToString)
                End If
            End If
        End If
        TimeBet = Time2 - Time1
        Time2 = Time1
        Return TimeBet.Seconds.ToString
    End Function
    Private Sub GetPlay()

        If Regex.Match(EOF, "intended").Success Then
            PBPDT.Rows(i).Item("QB") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+(?=\ pass intended)").ToString
            PBPDT.Rows(i).Item("Receiver") = Regex.Match(EOF, "(?<=\ intended for )\p{L}+\.\p{L}+(?=\ INTERCEPTED)").ToString

        ElseIf Regex.Match(EOF, "(?<=pass\ to )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Receiver") = Regex.Match(EOF, "(?<=pass\ to )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("QB") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ pass\ to \p{L}+\.\p{L}+)", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("PassYds") = GetYards()
            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Receiver")
            End If

        ElseIf Regex.Match(EOF, "incomplete", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("PassInc") = "Inc"
            PBPDT.Rows(i).Item("QB") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ pass\ incomplete)", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("Receiver") = Regex.Match(EOF, "(?<=\ pass incomplete to )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            If Regex.Match(EOF, "(?<=incomplete to \p{L}+\.\p{L}+\ \()\p{L}+\.\p{L}+").Success Then
                PBPDT.Rows(i).Item("PassDef") = Regex.Match(EOF, "(?<=incomplete to \p{L}+\.\p{L}+\ \()\p{L}+\.\p{L}+").ToString
            ElseIf Regex.Match(EOF, "(?<=incomplete \()\p{L}+\.\p{L}+").Success Then
                PBPDT.Rows(i).Item("PassDef") = Regex.Match(EOF, "(?<=incomplete \()\p{L}+\.\p{L}+").ToString
            End If


        ElseIf Regex.Match(EOF, "left end", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RLE") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "right end", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RRE") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "left tackle", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RLT") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "left guard", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RLG") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "up the middle", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RMD") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "right guard", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RRG") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        ElseIf Regex.Match(EOF, "right tackle", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Rusher") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("RRG") = GetYards()

            If Regex.Match(EOF, "FUMBLES").Success Then
                PBPDT.Rows(i).Item("Fumble") = PBPDT.Rows(i).Item("Rusher")
            End If

        End If
    End Sub
    Private Function TeamAbbrev(ByVal TeamName As String) As String
        Select Case TeamName
            Case "49ers" : Return "SFO"
            Case "Bengals" : Return "CIN"
            Case "Bills" : Return "BUF"
            Case "Buccaneers" : Return "TAM"
            Case "Cardinals" : Return "ARI"
            Case "Chargers" : Return "SD"
            Case "Chiefs" : Return "KC"
            Case "Cowboys" : Return "DAL"
            Case "Colts" : Return "IND"
            Case "Browns" : Return "CLE"
            Case "Dolphins" : Return "MIA"
            Case "Patriots" : Return "NWE"
            Case "Jets" : Return "NYJ"
            Case "Steelers" : Return "PIT"
            Case "Ravens" : Return "BAL"
            Case "Jaguars" : Return "JAX"
            Case "Titans" : Return "TEN"
            Case "Texans" : Return "HOU"
            Case "Broncos" : Return "DEN"
            Case "Raiders" : Return "OAK"
            Case "Giants" : Return "NYG"
            Case "Redskins" : Return "WAS"
            Case "Eagles" : Return "PHI"
            Case "Packers" : Return "GB"
            Case "Lions" : Return "DET"
            Case "Vikings" : Return "MIN"
            Case "Bears" : Return "CHI"
            Case "Panthers" : Return "CAR"
            Case "Falcons" : Return "ATL"
            Case "Saints" : Return "NWO"
            Case "Seahawks" : Return "SEA"
            Case "Rams" : Return "LAR"

        End Select

    End Function
    Private Function GetYards() As Integer
        If Regex.Match(EOF, "\-\d+(?=\ yards \()", RegexOptions.IgnoreCase).Success Then
            Return CInt(Regex.Match(EOF, "\-\d+(?=\ yards \()", RegexOptions.IgnoreCase).Value)
        ElseIf Regex.Match(EOF, "\-\d+(?=\ yard \()", RegexOptions.IgnoreCase).Success Then
            Return CInt(Regex.Match(EOF, "\-\d+(?=\ yard \()", RegexOptions.IgnoreCase).Value)
        ElseIf Regex.Match(EOF, "\d+(?=\ yards \()", RegexOptions.IgnoreCase).Success Then
            Return CInt(Regex.Match(EOF, "\d+(?=\ yards \()", RegexOptions.IgnoreCase).Value)
        ElseIf Regex.Match(EOF, "\d+(?=\ yard \()", RegexOptions.IgnoreCase).Success Then
            Return CInt(Regex.Match(EOF, "\d+(?=\ yard \()", RegexOptions.IgnoreCase).Value)
        ElseIf Regex.Match(EOF, "no gain").Success Then
            Return 0
        End If
    End Function
    Private Sub GetTackler()
        'Grab Tackler1
        If Regex.Match(EOF, "(?<=\ yards\ \()\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler1") = Regex.Match(EOF, "(?<=\ yards\ \()\w+\.\w+", RegexOptions.IgnoreCase).ToString

        ElseIf Regex.Match(EOF, "(?<=\ yard\ \()\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler1") = Regex.Match(EOF, "(?<=\ yard\ \()\w+\.\w+", RegexOptions.IgnoreCase).ToString

        ElseIf Regex.Match(EOF, "(?<=\ no gain\ \()\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler1") = Regex.Match(EOF, "(?<=\ no gain\ \()\w+\.\w+", RegexOptions.IgnoreCase).ToString
        End If

        'Grab Tackler2
        If Regex.Match(EOF, "(?<=\ no gain\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler2") = Regex.Match(EOF, "(?<=\ no gain\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).ToString

        ElseIf Regex.Match(EOF, "(?<=\ yards\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler2") = Regex.Match(EOF, "(?<=\ yards\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).ToString

        ElseIf Regex.Match(EOF, "(?<=\ yard\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("Tackler2") = Regex.Match(EOF, "(?<=\ yard\ \(\w+\.\w+\,\ )\w+\.\w+", RegexOptions.IgnoreCase).ToString
        End If
    End Sub
    Private Sub GetKO()
        'Grab Kickoff Player:
        If Regex.Match(EOF, "\w+\.\w+(?=\ kicks)", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KOPlayer") = Regex.Match(EOF, "\w+\.\w+(?=\ kicks)", RegexOptions.IgnoreCase).ToString
        End If

        'Grab KO Distance
        If Regex.Match(EOF, "(?<=\ kicks )\d{1,}", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KODist") = Regex.Match(EOF, "(?<=\ kicks )\d{1,}", RegexOptions.IgnoreCase).Value
        End If

        'Grab KOR Player:
        If Regex.Match(EOF, "(?<=kicks \d+\ yards from \w{2,3}\ \d+\ to \w{2,3}\ \d+\.)\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KORPlayer") = Regex.Match(EOF, "(?<=kicks \d+\ yards from \w{2,3}\ \d+\ to \w{2,3}\ \d+\.)\w+\.\w+", RegexOptions.IgnoreCase).ToString
        ElseIf Regex.Match(EOF, "(?<=kicks \d+\ yards from \w{2,3}\ \d+\ to \w{2,3}\ -\d+\.)\w+\.\w+", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KORPlayer") = Regex.Match(EOF, "(?<=kicks \d+\ yards from \w{2,3}\ \d+\ to \w{2,3}\ -\d+\.)\w+\.\w+", RegexOptions.IgnoreCase).ToString
        End If

        'Grab KORYards
        If Regex.Match(EOF, "\d+(?=\ yards \()", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KORDist") = CInt(Regex.Match(EOF, "\d+(?=\ yards \()", RegexOptions.IgnoreCase).Value)
        End If

        If Regex.Match(EOF, "\-\d+(?=\ yards \()", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("KORDist") = CInt(Regex.Match(EOF, "\-\d+(?=\ yards \()", RegexOptions.IgnoreCase).Value)
        End If

        If Regex.Match(EOF, "out of bounds").Success Then
            PBPDT.Rows(i).Item("PuntOOB") = "KickOOB"
        End If
    End Sub
    Private Sub GetPossChange()
        If Team1 = Nothing Then
            Team1 = TeamAbbrev(Regex.Match(EOF, "\w{4,}(?=\ at\ \d{0,2}\:\d{2})", RegexOptions.IgnoreCase).ToString)
        ElseIf Team2 = Nothing Then
            Team2 = TeamAbbrev(Regex.Match(EOF, "\w{4,}(?=\ at\ \d{0,2}\:\d{2})", RegexOptions.IgnoreCase).ToString)
            PBPDT.Rows(i).Item("TeamAbbrev") = Team2
        End If

        If Team1 = TeamAbbrev(Regex.Match(EOF, "\w{4,}(?=\ at\ \d{0,2}\:\d{2})", RegexOptions.IgnoreCase).ToString) Then
            PBPDT.Rows(i).Item("TeamAbbrev") = Team1
            PointDiff = TmScore1 - TmScore2
            PrevTeam = Team1
        ElseIf Team2 = TeamAbbrev(Regex.Match(EOF, "\w{4,}(?=\ at\ \d{0,2}\:\d{2})", RegexOptions.IgnoreCase).ToString) Then
            PBPDT.Rows(i).Item("TeamAbbrev") = Team2
            PointDiff = TmScore2 - TmScore1
            PrevTeam = Team2
        End If
    End Sub
    Private Sub GetFG()
        PBPDT.Rows(i).Item("FGPlayer") = Regex.Match(EOF, "(?<=\) )\p{L}+\.\p{L}+").ToString
        PBPDT.Rows(i).Item("FGDist") = CInt(Regex.Match(EOF, "\d+(?=\ yard field goal)").Value)
        If Regex.Match(EOF, "GOOD").Success Then
            PBPDT.Rows(i).Item("FGGood") = "Good"
        ElseIf Regex.Match(EOF, "No Good").Success Then
            PBPDT.Rows(i).Item("FGGood") = "No Good"
        ElseIf Regex.Match(EOF, "BLOCKED").Success Then
            PBPDT.Rows(i).Item("FGGood") = "No Good"
            PBPDT.Rows(i).Item("Blocked") = "FG Blocked"
        End If
    End Sub
    Private Sub GetXP()
        PBPDT.Rows(i).Item("XPPlayer") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ extra point)").ToString
        If Regex.Match(EOF, "GOOD").Success Then
            PBPDT.Rows(i).Item("XPGood") = "Good"
        ElseIf Regex.Match(EOF, "No Good").Success Then
            PBPDT.Rows(i).Item("XPGood") = "No Good"
        ElseIf Regex.Match(EOF, "Blocked").Success Then
            PBPDT.Rows(i).Item("XPGood") = "No Good"
            PBPDT.Rows(i).Item("Blocked") = "XP Blocked"
        End If
    End Sub
    Private Sub GetPunt()
        'Grab Punt Player
        PBPDT.Rows(i).Item("PuntPlayer") = Regex.Match(EOF, "(?<=\)\ )\p{L}+\.\p{L}+").ToString
        'If Regex.Match(EOF, "BLOCKED").Success = False Then
        PBPDT.Rows(i).Item("PuntDist") = CInt(Regex.Match(EOF, "(?<=punts\ )\d+(?=\ yards)").Value)
        'End If
        PBPDT.Rows(i).Item("PRPlayer") = Regex.Match(EOF, "(?<=\.)\p{L}+\.\p{L}+(?=\ \w+)").ToString
        If Regex.Match(EOF, "\-\d+(?=\ yards\ \(\p{L}+\.\p{L}+)").Success Then
            PBPDT.Rows(i).Item("PRdist") = CInt(Regex.Match(EOF, "\-\d+(?=\ yards\ \(\p{L}+\.\p{L}+)").Value)
        ElseIf Regex.Match(EOF, "\d+(?=\ yards \(\p{L}+\.\p{L}+)").Success Then
            PBPDT.Rows(i).Item("PRdist") = CInt(Regex.Match(EOF, "\d+(?=\ yards\ \(\p{L}+\.\p{L}+)").Value)
        ElseIf Regex.Match(EOF, "\d+(?=\ yard\ \(\p{L}+\.\p{L}+)").Success Then
            PBPDT.Rows(i).Item("PRDist") = CInt(Regex.Match(EOF, "\d+(?=\ yard\ \(\p{L}+\.\p{L}+)").Value)
        End If
        If Regex.Match(EOF, "out of bounds").Success Then
            PBPDT.Rows(i).Item("PuntOOB") = "PuntOOB"
        End If
    End Sub
    Private Sub GetInt()
        PBPDT.Rows(i).Item("IntPlayer") = Regex.Match(EOF, "(?<=INTERCEPTED by )\p{L}+\.\p{L}+").ToString
        PBPDT.Rows(i).Item("IntYards") = GetYards()
        PBPDT.Rows(i).Item("PassDef") = Regex.Match(EOF, "(?<=INTERCEPTED by \p{L}+\.\p{L}+\ \()\p{L}+\.\p{L}+").ToString
    End Sub
    Private Sub GetPenaltyAccept()
        If Regex.Match(EOF, "offsetting").Success = True Then
            PBPDT.Rows(i).Item("PenType") = "Offsetting"
            PBPDT.Rows(i).Item("PenTeam") = "Both"
        Else
            PBPDT.Rows(i).Item("PenPlayer") = Regex.Match(EOF, "(?<=PENALTY on \p{L}{2,3}\-).*(?=\, )").ToString
            PBPDT.Rows(i).Item("PenTeam") = Regex.Match(EOF, "(?<=PENALTY on )\p{L}{2,3}").ToString
            PBPDT.Rows(i).Item("PenAccepted") = "Accepted"
            If Regex.Match(EOF, "\d+(?=\ yards enforced)").Success Then
                PBPDT.Rows(i).Item("PenYards") = CInt(Regex.Match(EOF, "\d+(?=\ yards enforced)").Value)
            ElseIf Regex.Match(EOF, "\d+(?=\ yard enforced)").Success Then
                PBPDT.Rows(i).Item("PenYards") = CInt(Regex.Match(EOF, "\d+(?=\ yard enforced)").Value)
            End If
            PBPDT.Rows(i).Item("PenType") = Regex.Match(EOF, "(?<=PENALTY\ on\ \p{L}{2,3}.*\ ).*(?=\ \d+\ yards\ enforced)").ToString
        End If
    End Sub
    Private Sub GetPenaltyDecline()
        If Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\ ).*(?=declined)").Success Then
            PBPDT.Rows(i).Item("PenType") = Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\ ).*(?=declined)").ToString
        ElseIf Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\-\p{L}+\.\p{L}+\ ).*(?=\ declined)").Success Then
            PBPDT.Rows(i).Item("PenType") = Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\-\p{L}+\.\p{L}+\ ).*(?=\ declined)").ToString
        ElseIf Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\-\p{L}\.\p{L}+\,\ ).*(\, declined)").Success Then
            PBPDT.Rows(i).Item("PenType") = Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\-\p{L}\.\p{L}+\,\ ).*(\, declined)").ToString
        End If
        PBPDT.Rows(i).Item("PenPlayer") = Regex.Match(EOF, "(?<=Penalty on \p{L}{2,3}\-)\p{L}+\.\p{L}+").ToString
        PBPDT.Rows(i).Item("PenTeam") = Regex.Match(EOF, "(?<=Penalty on )\p{L}{2,3}(?=\-)").ToString
        PBPDT.Rows(i).Item("penaccepted") = "Declined"
    End Sub
    Private Sub GetChallenge()
        PBPDT.Rows(i).Item("Challenged") = Regex.Match(EOF, "(?<=Challenged by )\p{L}{2,3}").ToString
        If Regex.Match(EOF, "REVERSED").Success Then
            PBPDT.Rows(i).Item("Reversed") = "Reversed"
        Else
            PBPDT.Rows(i).Item("Reversed") = "Upheld"
        End If
    End Sub
    Private Sub GetSafety()
        PBPDT.Rows(i).Item("Safety") = "SF"
        'One tackler for safety
        If Regex.Match(EOF, "SAFETY \(\p{L}+\.\p{L}+\)").Success Then
            PBPDT.Rows(i).Item("Tackler1") = Regex.Match(EOF, "(?<=SAFETY \()\p{L}+\.\p{L}+").ToString
        ElseIf Regex.Match(EOF, "(?<=SAFETY \(\p{L}+\.\p{L}+\ )\p{L}+\.\p{L}+").Success Then
            PBPDT.Rows(i).Item("Tackler2") = Regex.Match(EOF, "(?<=SAFETY \(\p{L}+\.\p{L}+\ )\p{L}+\.\p{L}+").ToString
        End If
    End Sub
    Private Sub Get2Point()
        If Regex.Match(EOF, "ATTEMPT SUCCEEDS").Success Then
            PBPDT.Rows(i).Item("TwoPoint") = "Good"
        ElseIf Regex.Match(EOF, "ATTEMPT FAILS").Success Then
            PBPDT.Rows(i).Item("TwoPoint") = "Fails"
        End If
    End Sub
    Private Sub GetFumble()
        If Regex.Match(EOF, "(?<=FUMBLES \()\p{L}+\.\p{L}+").Success Then
            PBPDT.Rows(i).Item("FFPlayer") = Regex.Match(EOF, "(?<=FUMBLES \()\p{L}+\.\p{L}+").ToString
        ElseIf Regex.Match(EOF, "Aborted").Success Then
            PBPDT.Rows(i).Item("FFPlayer") = "Aborted"
        End If
        If Regex.Match(EOF, "recovers").Success Then
            PBPDT.Rows(i).Item("FRPlayer") = PBPDT.Rows(i).Item("FFPlayer")
            If PBPDT.Rows(i).Item("TeamAbbrev") = Team1 Then
                PBPDT.Rows(i).Item("FRteam") = Team1
            ElseIf PBPDT.Rows(i).Item("TeamAbbrev") = Team2 Then
                PBPDT.Rows(i).Item("FRTeam") = Team2
            End If
        End If
        If Regex.Match(EOF, "RECOVERED").Success Then
            PBPDT.Rows(i).Item("FRTeam") = Regex.Match(EOF, "(?<=RECOVERED by )\p{L}{2,3}").ToString
            PBPDT.Rows(i).Item("FRPlayer") = Regex.Match(EOF, "(?<=RECOVERED by \p{L}{2,3}\-)\p{L}+\.\p{L}+").ToString
            If Regex.Match(EOF, "(?<=RECOVERED by .*)\d+(?=\ yards \(\p{L}+\.\p{L}+)").Success Then
                PBPDT.Rows(i).Item("FRYards") = CInt(Regex.Match(EOF, "(?<=RECOVERED by .*)\d+(?=\ yards \(\p{L}+\.\p{L}+)").Value)
            ElseIf Regex.Match(EOF, "(?<=RECOVERED by .*)\d+(?=\ yard \(\p{L}+\.\p{L}+)").Success Then
                PBPDT.Rows(i).Item("FRYards") = CInt(Regex.Match(EOF, "(?<=RECOVERED by .*)\d+(?=\ yard \(\p{L}+\.\p{L}+)").Value)
            ElseIf Regex.Match(EOF, "(?<=RECOVERED by .*)\ no gain(?=\ \(\p{L}+\.\p{L}+)").Success Then
                PBPDT.Rows(i).Item("FRYards") = 0
            End If
            'Get FR Tacklers
            If Regex.Match(EOF, "(?<=RECOVERED .*)\p{L}+\.\p{L}+(?=\))").Success Then
                PBPDT.Rows(i).Item("FRTackler1") = Regex.Match(EOF, "(?<=RECOVERED .*)\p{L}+\.\p{L}+(?=\))").ToString
            ElseIf Regex.Match(EOF, "(?<=RECOVERED .*)\p{L}+\.\p{L}+(?=\, \p{L}+\.\p{L}+\))").Success Then
                PBPDT.Rows(i).Item("FRTackler1") = Regex.Match(EOF, "(?<=RECOVERED .*)\p{L}+\.\p{L}+(?=\, \p{L}+\.\p{L}+\))").ToString
                PBPDT.Rows(i).Item("FRTackler2") = Regex.Match(EOF, "(?<=RECOVERED .*\p{L}+\.\p{L}+\, )\p{L}+\.\p{L}+(?=\))").ToString
            End If
            If Regex.Match(EOF, "ball out of bounds").Success Then
                PBPDT.Rows(i).Item("BallOOB") = "FumOOB"
            End If
        End If
    End Sub
    Private Sub GetMuff()
        If Regex.Match(EOF, "recovers").Success Then
            PBPDT.Rows(i).Item("FRPlayer") = PBPDT.Rows(i).Item("PRPlayer")
            'player recovers own fumble
            If PBPDT.Rows(i).Item("TeamAbbrev") = Team1 Then
                PBPDT.Rows(i).Item("FRteam") = Team2
            ElseIf PBPDT.Rows(i).Item("TeamAbbrev") = Team2 Then
                PBPDT.Rows(i).Item("FRTeam") = Team1
            End If
            'Other Team recovers ball
        ElseIf Regex.Match(EOF, "RECOVERED", RegexOptions.IgnoreCase).Success Then
            PBPDT.Rows(i).Item("FRTeam") = Regex.Match(EOF, "(?<=RECOVERED by )\p{L}{2,3}", RegexOptions.IgnoreCase).ToString
            PBPDT.Rows(i).Item("FRPlayer") = Regex.Match(EOF, "(?<=RECOVERED by .*\-)\p{L}+\.\p{L}+", RegexOptions.IgnoreCase).ToString
            If Regex.Match(EOF, "(?<=RECOVERED .*\ for\ )\d+(?=\ yards \()", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("FRYards") = CInt(Regex.Match(EOF, "(?<=RECOVERED .*\ for\ )\d+(?=\ yards \()", RegexOptions.IgnoreCase).Value)
            ElseIf Regex.Match(EOF, "(?<=RECOVERED .*\ for\ )\d+(?=\ yard \()", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("FRYards") = CInt(Regex.Match(EOF, "(?<=RECOVERED .*\ for\ )\d+(?=\ yard \()", RegexOptions.IgnoreCase).Value)
            ElseIf Regex.Match(EOF, "(?<=RECOVERED .*)\ no gain", RegexOptions.IgnoreCase).Success Then
                PBPDT.Rows(i).Item("FRYards") = 0
            End If

        ElseIf Regex.Match(EOF, "ball out of bounds").Success Then
            PBPDT.Rows(i).Item("ballOOB") = "MuffOOB"
        End If

    End Sub
    Private Sub GetPointDiff()
        'checks to see if score is given in the PBP
        If Regex.Match(EOF, "\p{L}{2,3} \d+   \p{L}{2,3} \d+").Success Then
            If Team1 = Regex.Match(EOF, "\p{L}{2,3}(?=\ \d+   \p{L}{2,3} \d+)").ToString Then
                TmScore1 = CInt(Regex.Match(EOF, "(?<=\p{L}{2,3} )\d+(?=   \p{L}{2,3} \d+)").Value)
                TmScore2 = CInt(Regex.Match(EOF, "(?<=\p{L}{2,3} \d+   \p{L}{2,3} )\d+").Value)
            ElseIf Team2 = Regex.Match(EOF, "\p{L}{2,3}(?=\ \d+   \p{L}{2,3} \d+)").ToString Then
                TmScore2 = CInt(Regex.Match(EOF, "(?<=\p{L}{2,3} )\d+(?=   \p{L}{2,3} \d+)").Value)
                TmScore1 = CInt(Regex.Match(EOF, "(?<=\p{L}{2,3} \d+   \p{L}{2,3} )\d+").Value)
            End If
            If PBPDT.Rows(i).Item("TeamAbbrev") = Team1 Then
                PBPDT.Rows(i).Item("PointDiff") = TmScore1 - TmScore2
                PointDiff = TmScore1 - TmScore2
            ElseIf PBPDT.Rows(i).Item("TeamAbbrev") = Team2 Then
                PBPDT.Rows(i).Item("Pointdiff") = TmScore2 - TmScore1
                PointDiff = TmScore2 - TmScore1
            End If
        End If
    End Sub
    Private Sub GetInjury()
        If Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ \(.* injured)").Success Then
            PBPDT.Rows(i).Item("Injury") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ \(.* injured)").ToString
        ElseIf Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ injured)").Success Then
            PBPDT.Rows(i).Item("Injury") = Regex.Match(EOF, "\p{L}+\.\p{L}+(?=\ injured)").ToString
        ElseIf Regex.Match(EOF, "(?<=.* \()\p{L}+(?=\)\ injured)").Success Then
            PBPDT.Rows(i).Item("Injury") = Regex.Match(EOF, "(?<=.* \()\p{L}+(?=\)\ injured)").ToString
        ElseIf Regex.Match(EOF, "(?<=\-)\p{L}+\.\p{L}+(\ injured)").Success Then
            PBPDT.Rows(i).Item("injury") = Regex.Match(EOF, "(?<=\-)\p{L}+\.\p{L}+(\ injured)").ToString
        ElseIf Regex.Match(EOF, "(?<=\-)\p{L}+(?=\ injured)").Success Then
            PBPDT.Rows(i).Item("Injury") = Regex.Match(EOF, "(?<=\-)\p{L}+(?=\ injured)").ToString

        End If
    End Sub
    Private Function GetYardline() As Integer
        If Regex.Match(EOF, "(?<=\d{1,2}\-)\w{2,3}\d{1,2}", RegexOptions.IgnoreCase).Success Then
            'PBPDT.Rows(i).Item("YardLine") = Regex.Match(EOF, "(?<=\d{1,2}\-)\w{2,3}\d{1,2}", RegexOptions.IgnoreCase).ToString
            If Team1 = TeamWithBall Then
                If Team1 = Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-)\p{L}{2,3}(?=\d{1,2})").ToString Then
                    PBPDT.Rows(i).Item("Yardline") = CInt(Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-\p{L}{2,3})\d{1,2}").Value)
                ElseIf Team2 = Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-)\p{L}{2,3}(?=\d{1,2})").ToString Then
                    PBPDT.Rows(i).Item("Yardline") = (50 - CInt(Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-\p{L}{2,3})\d{1,2}").Value)) + 50
                End If
            ElseIf Team2 = TeamWithBall Then
                If Team2 = Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-)\p{L}{2,3}(?=\d{1,2})").ToString Then
                    PBPDT.Rows(i).Item("Yardline") = CInt(Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-\p{L}{2,3})\d{1,2}").Value)
                ElseIf Team1 = Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-)\p{L}{2,3}(?=\d{1,2})").ToString Then
                    PBPDT.Rows(i).Item("Yardline") = (50 - CInt(Regex.Match(EOF, "(?<=\d{1,2}\-\d{1,2}\-\p{L}{2,3})\d{1,2}").Value)) + 50
                End If
            End If
        End If



        'End If

    End Function


End Class
