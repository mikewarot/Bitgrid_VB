Public Class Form1
    Dim CellX, CellY As Integer
    Dim InputBits As Integer
    Dim ProgramBits(4, 16) As System.Windows.Forms.CheckBox
    Structure BitGridCell
        Public OverrideControl(,) As Boolean
        Public ProgramStore(,) As Boolean
    End Structure

    Dim BitGrid(8, 8) As BitGridCell

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Application.Exit()

    End Sub

    Private Sub AboutBitgridSimulatorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutBitgridSimulatorToolStripMenuItem.Click
        MsgBox("Bitgrid Simulator - Version 0.0001", MsgBoxStyle.OkCancel)
    End Sub

    Private Sub Update_Status_Line()
        ToolStripStatusLabel1.Text = "Cell(" & CellX.ToString & "," & CellY.ToString & ")"
        ToolStripStatusLabel2.Text = "Input(" & InputBits.ToString("X") & ")  Output(" & "0" & ")"
    End Sub

    Private Sub StoreCell()
        Dim i, j As Integer
        For i = 1 To 1
            For j = 1 To 4
                BitGrid(CellX, CellY).ProgramStore(i, j) = ProgramBits(i, j).Checked
            Next
        Next
    End Sub

    Private Sub ButtonRight_Click(sender As Object, e As EventArgs) Handles ButtonRight.Click
        StoreCell()
        CellX = CellX + 1
        If CellX > 7 Then
            If XNavigationWrapped.Checked Then
                CellX = 0
            Else
                CellX = 7
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonLeft_Click(sender As Object, e As EventArgs) Handles ButtonLeft.Click
        StoreCell()
        CellX = CellX - 1
        If CellX < 0 Then
            If XNavigationWrapped.Checked Then
                CellX = 7
            Else
                CellX = 0
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonDown_Click(sender As Object, e As EventArgs) Handles ButtonDown.Click
        StoreCell()
        CellY = CellY - 1
        If CellY < 0 Then
            If YNavigationWrapped.Checked Then
                CellY = 7
            Else
                CellY = 0
            End If
        End If
        Update_Status_Line()
    End Sub

    Private Sub ButtonUp_Click(sender As Object, e As EventArgs) Handles ButtonUp.Click
        StoreCell()
        CellY = CellY + 1
        If CellY > 7 Then
            If YNavigationWrapped.Checked Then
                CellY = 0
            Else
                CellY = 7
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
        Dim i, j As Integer
        For i = 0 To 7
            For j = 0 To 7
                ReDim BitGrid(i, j).ProgramStore(4, 16)
                ReDim BitGrid(i, j).OverrideControl(4, 3)

            Next
        Next
        CellX = 0
        CellY = 0
        InputBits = 0  ' default to all off for now
        ProgramBits(1, 1) = CheckBox1
        ProgramBits(1, 2) = CheckBox2
        ProgramBits(1, 3) = CheckBox3
        ProgramBits(1, 4) = CheckBox4
        ' ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
        Update_Status_Line()
    End Sub
End Class
