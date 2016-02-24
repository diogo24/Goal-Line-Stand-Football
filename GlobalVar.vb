Module GlobalVar
    'Module contains Global Variables for various functions
    Public GetTables As New DataFunctions
    Public GetPBP As New PBP
    Public GetSQL As New GenSQL
    Public Eval As New Evaluation
    Public PBPDT As New DataTable
    Public OwnerDT As New DataTable
    Public PlayerDT As New DataTable
    Public DraftDT As New DataTable
    Public GMDT As New DataTable
    Public CoachDT As New DataTable
    Public TrainerDT As New DataTable
    Public ScoutDT As New DataTable
    Public CityInfo As New DataTable
    Public HomeTeamDT As New DataTable
    Public AwayTeamDT As New DataTable
    Public FBall As New DataSet
    Public PlayStats As New DataTable
    Public PlayData As New DataTable
    Public MT As New Random.MersenneTwister
    Public GenData As New Generation
    Public ScoutGradeDT As New DataTable
    Public TeamDraft As New Drafting
    Public TeamDraftDT As New DataTable
    Public WithEvents Game As New GameEngine
    Public WithEvents GetDec As New AIDecisionsInGame
    Public WithEvents GameClock As New Stopwatch
    Public WithEvents PlayClock As New Stopwatch
    Public InitTeams(32) As Scheduler.Teams
    Public GenSchedule As New Scheduler

    Public Sub GetDS() 'Adds tables to the dataset
        FBall.Tables.Add(PBPDT)
        FBall.Tables.Add(OwnerDT)
        FBall.Tables.Add(PlayerDT)
        FBall.Tables.Add(GMDT)
        FBall.Tables.Add(DraftDT)
        FBall.Tables.Add(TrainerDT)
        FBall.Tables.Add(ScoutDT)
        FBall.Tables.Add(CoachDT)
        FBall.Tables.Add(PlayStats)
        FBall.Tables.Add(PlayData)
        FBall.Tables.Add(ScoutGradeDT)
        FBall.Tables.Add(TeamDraftDT)
        FBall.Tables.Add(CityInfo)
        FBall.Tables.Add(HomeTeamDT)
        FBall.Tables.Add(AwayTeamDT)
    End Sub
    Public Sub Instantiate()
        GetDS()
        GenSchedule.GetSchedule(16)
        'GenData.GetRosterPlayers(2600)
        'GenData.GenOwners(96)
        'GenData.GenGMs(96)
        'GenData.GenCoaches(800)
        'GenData.GenScouts(800)
        'GenData.GenDraftPlayers(2555)
        'GenData.GenDraftClass(2555)
        'Game.InGame()
        'Eval.ScoutPlayerEval()
        'GenData.GenScoutGrades(800, 2555)
        'GetTables.DeleteTable(PBPDT, "PBP")
        'GetTables.LoadTable(PBPDT, "PBP")
        'GetPBP.GenSQL()
        'GetPBP.GetPBP("pbptest.txt")
        'GetPBP.GetPBP("fix2002pbp.txt")
        'GetPBP.GetPBP("2003pbp.txt")
        'GetTables.UpdateTable(PBPDT, "PBP")
        'GetSQL.StoredProcs()
    End Sub
    
End Module
