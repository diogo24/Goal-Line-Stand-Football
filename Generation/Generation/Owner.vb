Public Class Owner
    Inherits Person
    ''' <summary>
    ''' Generates the number of owners/potential owners specified for the league
    ''' only 32 are placed with a team, the other owners are "potential owners" that can
    ''' buy the team from the current owner
    ''' </summary>
    ''' <param name="NumOwners"></param>
    Public Sub GenOwners(ByVal NumOwners As Integer)
        Dim SQLFieldNames As String = "OwnerID int PRIMARY KEY NOT NULL, TeamID int NULL, FName varchar(20) NULL, LName varchar(20) NULL, College varchar(50) NULL, Age int NULL, DOB varchar(12) NULL, 
OwnerRep int NULL,  Experience int NULL, GMPatience int NULL, CoachPatience int NULL, Meddles int NULL, WantsWinner int NULL, SpendsMoney int"

        SQLiteTables.CreateTable(MyDB, OwnerDT, "Owners", SQLFieldNames) 'Inside CreateTable, it checks to see if a table exists or not.  If it does not, it creates one, if it does it exits.
        SQLiteTables.DeleteTable(MyDB, OwnerDT, "Owners") 'removes all records if there were any
        SQLiteTables.LoadTable(MyDB, OwnerDT, "Owners")
        OwnerDT.Rows.Add(0)
        For i As Integer = 1 To NumOwners
            OwnerDT.Rows.Add(i)
            If i > 32 Then : OwnerDT.Rows(i).Item("TeamID") = 0
            Else : OwnerDT.Rows(i).Item("TeamID") = i
            End If
            OwnerDT.Rows(i).Item("OwnerID") = i
            GenNames(OwnerDT, i, "Owner") 'Gets first and last name, college, Age, DOB, Height and Weight
            OwnerDT.Rows(i).Item("OwnerRep") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("Experience") = MT.GenerateInt32(1, 50)
            OwnerDT.Rows(i).Item("GMPatience") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("CoachPatience") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("Meddles") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("WantsWinner") = MT.GetGaussian(49.5, 16.5)
            OwnerDT.Rows(i).Item("SpendsMoney") = MT.GetGaussian(49.5, 16.5)

        Next i
        SQLiteTables.BulkInsert(MyDB, OwnerDT, "Owners")
    End Sub

End Class
