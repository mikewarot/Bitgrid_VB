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
        For i = 1 To 4
            For j = 1 To 16
                BitGrid(CellX, CellY).ProgramStore(i, j) = ProgramBits(i, j).Checked
            Next
        Next
    End Sub

    Private Sub LoadCell()
        Dim i, j As Integer
        For i = 1 To 4
            For j = 1 To 16
                ProgramBits(i, j).Checked = BitGrid(CellX, CellY).ProgramStore(i, j)
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
        LoadCell()
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
        LoadCell()
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
        LoadCell()
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
        LoadCell()
        Update_Status_Line()
    End Sub

    Private Sub ComputeInputs()
        Dim i, j As Integer

        InputBits = 0
        If BottomInputON.Checked Then InputBits = InputBits + &H8
        If TopInputON.Checked Then InputBits = InputBits + &H4
        If LeftInputON.Checked Then InputBits = InputBits + &H2
        If RightInputON.Checked Then InputBits = InputBits + &H1

        If (InputBits And &H8) <> 0 Then

        End If

        ' highlight the appropriate checkboxes
        For i = 1 To 4
            For j = 1 To 16
                If (InputBits + 1) = j Then
                    ProgramBits(i, j).BackColor = Color.Red
                Else
                    ProgramBits(i, j).BackColor = SystemColors.Control
                End If
            Next
        Next
    End Sub

    Private Sub InputOptionChanged(sender As Object, e As EventArgs) Handles TopInputON.CheckedChanged, TopInputON.Click, TopInputOff.Click, TopInputNormal.Click, RightInputON.Click, RightInputOff.Click, RightInputNormal.Click, LeftInputON.Click, LeftInputOff.Click, LeftInputNormal.Click, BottomInputON.Click, BottomInputOff.Click, BottomInputNormal.Click
        ComputeInputs()
        Update_Status_Line()
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        Dim i, j As Integer
        Dim s As String
        Dim Bits() As String

        Dim OpenFileDialog1 As New OpenFileDialog()
        OpenFileDialog1.InitialDirectory = "c:\bitgrid_VB\"
        OpenFileDialog1.Filter = "bitgrid files (*.bitgrid)|*.bitgrid|All files (*.*)|*.*"
        OpenFileDialog1.FilterIndex = 2
        OpenFileDialog1.RestoreDirectory = True

        If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

            Dim file = My.Computer.FileSystem.OpenTextFileReader(OpenFileDialog1.FileName, System.Text.Encoding.ASCII)
            For i = 1 To 4
                s = file.ReadLine()
                Bits = Split(s, ",")
                For j = 1 To 16
                    ProgramBits(i, j).Checked = Convert.ToBoolean(Bits(j - 1).ToString)

                Next
            Next
            file.Close()
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem3.Click
        Dim i, j As Integer

        Dim file = My.Computer.FileSystem.OpenTextFileWriter("c:\bitgrid_VB\test.bitgrid", False, System.Text.Encoding.ASCII)
        '        file.WriteLine("Here is the first string.")
        For i = 1 To 4
            For j = 1 To 16
                file.Write(ProgramBits(i, j).Checked)
                If j < 16 Then
                    file.Write(",")
                Else
                    file.WriteLine()
                End If
            Next
        Next
        file.Close()
    End Sub

    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles BottomInputBox.Enter

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
        ' Put the checkboxes into the ProgramBits array to make everything else work later
        ' it is a huge kludge, but it is effective... so it stays for now
        ProgramBits(1, 1) = CheckBox1
        ProgramBits(1, 2) = CheckBox2
        ProgramBits(1, 3) = CheckBox3
        ProgramBits(1, 4) = CheckBox4
        ProgramBits(1, 5) = CheckBox5
        ProgramBits(1, 6) = CheckBox6
        ProgramBits(1, 7) = CheckBox7
        ProgramBits(1, 8) = CheckBox8
        ProgramBits(1, 9) = CheckBox9
        ProgramBits(1, 10) = CheckBox10
        ProgramBits(1, 11) = CheckBox11
        ProgramBits(1, 12) = CheckBox12
        ProgramBits(1, 13) = CheckBox13
        ProgramBits(1, 14) = CheckBox14
        ProgramBits(1, 15) = CheckBox15
        ProgramBits(1, 16) = CheckBox16

        ProgramBits(2, 1) = CheckBox17
        ProgramBits(2, 2) = CheckBox18
        ProgramBits(2, 3) = CheckBox19
        ProgramBits(2, 4) = CheckBox20
        ProgramBits(2, 5) = CheckBox21
        ProgramBits(2, 6) = CheckBox22
        ProgramBits(2, 7) = CheckBox23
        ProgramBits(2, 8) = CheckBox24
        ProgramBits(2, 9) = CheckBox25
        ProgramBits(2, 10) = CheckBox26
        ProgramBits(2, 11) = CheckBox27
        ProgramBits(2, 12) = CheckBox28
        ProgramBits(2, 13) = CheckBox29
        ProgramBits(2, 14) = CheckBox30
        ProgramBits(2, 15) = CheckBox31
        ProgramBits(2, 16) = CheckBox32

        ProgramBits(3, 1) = CheckBox65
        ProgramBits(3, 2) = CheckBox66
        ProgramBits(3, 3) = CheckBox67
        ProgramBits(3, 4) = CheckBox68
        ProgramBits(3, 5) = CheckBox69
        ProgramBits(3, 6) = CheckBox70
        ProgramBits(3, 7) = CheckBox71
        ProgramBits(3, 8) = CheckBox72
        ProgramBits(3, 9) = CheckBox73
        ProgramBits(3, 10) = CheckBox74
        ProgramBits(3, 11) = CheckBox75
        ProgramBits(3, 12) = CheckBox76
        ProgramBits(3, 13) = CheckBox77
        ProgramBits(3, 14) = CheckBox78
        ProgramBits(3, 15) = CheckBox79
        ProgramBits(3, 16) = CheckBox80

        ProgramBits(4, 1) = CheckBox81
        ProgramBits(4, 2) = CheckBox82
        ProgramBits(4, 3) = CheckBox83
        ProgramBits(4, 4) = CheckBox84
        ProgramBits(4, 5) = CheckBox85
        ProgramBits(4, 6) = CheckBox86
        ProgramBits(4, 7) = CheckBox87
        ProgramBits(4, 8) = CheckBox88
        ProgramBits(4, 9) = CheckBox89
        ProgramBits(4, 10) = CheckBox90
        ProgramBits(4, 11) = CheckBox91
        ProgramBits(4, 12) = CheckBox92
        ProgramBits(4, 13) = CheckBox93
        ProgramBits(4, 14) = CheckBox94
        ProgramBits(4, 15) = CheckBox95
        ProgramBits(4, 16) = CheckBox96


        ' ToolStripStatusLabel1.Text = "Cell(" & X.ToString & "," & Y.ToString & ")"
        Update_Status_Line()
    End Sub
End Class
