Public Class Form1
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()

    End Sub

    Private Sub AboutBitgridSimulatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutBitgridSimulatorToolStripMenuItem.Click
        MsgBox("Bitgrid Simulator - Version 0.0001", MsgBoxStyle.OkCancel)
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class
