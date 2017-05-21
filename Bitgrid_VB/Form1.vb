Public Class Form1
    Dim X, Y As Integer

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()

    End Sub

    Private Sub AboutBitgridSimulatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutBitgridSimulatorToolStripMenuItem.Click
        MsgBox("Bitgrid Simulator - Version 0.0001", MsgBoxStyle.OkCancel)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub

    Private Sub Update_Status_Line
        ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
    End Sub

    Private Sub ButtonRight_Click(sender As Object, e As EventArgs) Handles ButtonRight.Click
        X = X + 1
        If X > 7 Then X = 0
        Update_Status_Line()
    End Sub

    Private Sub ButtonLeft_Click(sender As Object, e As EventArgs) Handles ButtonLeft.Click
        X = X - 1
        If X < 0 Then X = 7
        Update_Status_Line()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        X = 0
        Y = 0
        ' ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
        Update_Status_Line()
    End Sub
End Class
