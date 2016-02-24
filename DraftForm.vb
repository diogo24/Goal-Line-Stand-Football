Public Class DraftForm
    Public Sub StartDraft()

    End Sub
    Private Sub DraftForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TableLayoutPanel1.AllowDrop = True
        'TableLayoutPanel1.BackColor = Color.DarkCyan
        TableLayoutPanel1.BackgroundImage = Image.FromFile("C:\Documents and Settings\MNasty\Desktop\nfl_draftLR.jpg")
        TableLayoutPanel1.BackgroundImageLayout = 3




    End Sub

  
   
End Class