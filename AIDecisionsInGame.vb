Public Class AIDecisionsInGame
    'this class stores all the in-game subs and functions for the AI
    Public Function CoinTossDecision(ByVal VisitingTeam As Integer, ByVal HomeTeam As Integer)
        'decision on whether to receive or defer to 2nd half
        Select Case MT.GenerateInt32(0, 100)
            Case Is <= 50
                Return HomeTeam
            Case Else
                Return VisitingTeam
        End Select
    End Function

    Public Sub GetWeather(ByVal CityID As Integer, ByVal month As Integer, ByVal cityInfo As DataTable)
        'gets the gametime weather by 
        GameEngine.Weather.Temp = MT.GenerateInt32(cityInfo.Rows(CityID).Item("SepLo"), cityInfo.Rows(CityID).Item("SepHi"))
        GameEngine.Weather.WeatherCondition = MT.GenerateInt32(1, 4)
        GameEngine.Weather.WindSpd = MT.GenerateInt32(3, 21)
        GameEngine.Weather.WindDir = MT.GenerateInt32(0, 360)

    End Sub

    'Public Event GetWeat(ByVal CityID As Integer)


End Class
