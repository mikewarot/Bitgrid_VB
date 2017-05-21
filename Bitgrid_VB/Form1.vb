﻿Public Class Form1
    Dim X, Y As Integer
    Dim InputBits As Integer

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()

    End Sub

    Private Sub AboutBitgridSimulatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutBitgridSimulatorToolStripMenuItem.Click
        MsgBox("Bitgrid Simulator - Version 0.0001", MsgBoxStyle.OkCancel)
    End Sub

    Private Sub Update_Status_Line
        ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
        ToolStripStatusLabel2.Text = "Input(" & InputBits.ToString("X") & ")  Output(" & "0" & ")"
    End Sub

    Private Sub ButtonRight_Click(sender As Object, e As EventArgs) Handles ButtonRight.Click
        X = X + 1
        If X > 7 Then
            If XNavigationWrapped.Checked Then
                X = 0
            Else
                X = 7
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonLeft_Click(sender As Object, e As EventArgs) Handles ButtonLeft.Click
        X = X - 1
        If X < 0 Then
            If XNavigationWrapped.Checked Then
                X = 7
            Else
                X = 0
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        Y = Y - 1
        If Y < 0 Then
            If YNavigationWrapped.Checked Then
                Y = 7
            Else
                Y = 0
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        Y = Y + 1
        If Y > 7 Then
            If YNavigationWrapped.Checked Then
                Y = 0
            Else
                Y = 7
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ComputeInputs()
        InputBits = 0
        If BottomInputON.Checked Then InputBits = InputBits + &H8
        If TopInputON.Checked Then InputBits = InputBits + &H4
        If LeftInputON.Checked Then InputBits = InputBits + &H2
        If RightInputON.Checked Then InputBits = InputBits + &H1
    End Sub

    Private Sub InputOptionChanged(sender As Object, e As EventArgs) Handles TopInputON.CheckedChanged, TopInputON.Click, TopInputOff.Click, TopInputNormal.Click, RightInputON.Click, RightInputOff.Click, RightInputNormal.Click, LeftInputON.Click, LeftInputOff.Click, LeftInputNormal.Click, BottomInputON.Click, BottomInputOff.Click, BottomInputNormal.Click
        ComputeInputs()
        Update_Status_Line()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        X = 0
        Y = 0
        InputBits = 0  ' default to all off for now
        ' ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
        Update_Status_Line()
    End Sub
End Class
