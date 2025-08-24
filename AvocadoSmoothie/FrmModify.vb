Imports System
Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices


Module ControlExtensions
    <Extension()>
    Public Function InvokeAsync(ctrl As Control, action As Action) As Task
        If ctrl.InvokeRequired Then
            Return Task.Factory.StartNew(Sub() ctrl.Invoke(action), Threading.CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)
        Else
            action()
            Return Task.CompletedTask
        End If
    End Function
End Module

Public Class FrmModify

    Private dpiX As Double
    Private dpiY As Double

    Private Async Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim numericValue As Double

        ' mainForm 인스턴스 가져오기
        Dim mainForm = Application.OpenForms _
                        .OfType(Of FrmMain)() _
                        .FirstOrDefault()

        If mainForm Is Nothing Then
            MessageBox.Show("Main form not found.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' 입력 유효성 검사
        If String.IsNullOrEmpty(txtInitEdit.Text) OrElse
           Not Double.TryParse(txtInitEdit.Text, numericValue) Then
            txtInitEdit.Select()
            txtInitEdit.SelectAll()
            Return
        End If

        ' 선택된 Index 정렬 후 배열로
        Dim indices = mainForm.lbInitData _
                        .SelectedIndices _
                        .Cast(Of Integer)() _
                        .OrderBy(Function(x) x) _
                        .ToArray()
        Dim total = indices.Length
        If total = 0 Then Return

        ' ProgressBar 초기화
        pbModify.Minimum = 0
        pbModify.Maximum = total
        pbModify.Value = 0

        ' ListBox 업데이트 일시 중지
        Dim lb = mainForm.lbInitData
        lb.BeginUpdate()

        ' 새로운 값 미리 생성
        Dim newValue As String = numericValue.ToString("G")

        Const BatchSize As Integer = 1000
        Dim done As Integer = 0

        ' 병렬 / 비동기 Batch 처리
        While done < total
            Dim cnt As Integer = Math.Min(BatchSize, total - done)
            Dim batchIndices = indices.Skip(done).Take(cnt).ToArray()
            Dim batchValues = Enumerable.Repeat(newValue, batchIndices.Length).ToArray()

            ' UI Thread 에서 항목 변경
            Await Me.InvokeAsync(Sub()
                                     For i = 0 To batchIndices.Length - 1
                                         lb.Items(batchIndices(i)) = batchValues(i)
                                     Next
                                     pbModify.Value = Math.Min(done + cnt, total)

                                     Dim count As Integer = mainForm.lbInitData.SelectedItems.Count

                                     If count > 1 Then
                                         slblModify.Text = $"Modifying {count} selected items..."
                                     Else
                                         slblModify.Text = "Modifying the selected item..."
                                     End If
                                 End Sub)

            done += cnt
            Await Task.Yield()
        End While

        ' UI Thread 에서 변경된 항목 재선택
        Await Me.InvokeAsync(Sub()
                                 lb.ClearSelected()
                                 For Each idx In indices
                                     If idx >= 0 AndAlso idx < lb.Items.Count Then
                                         lb.SetSelected(idx, True)
                                     End If
                                 Next
                                 lb.EndUpdate()
                                 mainForm.lbInitData.Focus()
                                 pbModify.Value = 0
                                 mainForm.slblDesc.Text = $"Modified {total} item{If(total > 1, "s", "")} to '{newValue}' in Initial Dataset."
                                 Me.Close()
                             End Sub)
    End Sub

    Private Sub FrmModify_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' mainForm 인스턴스 가져오기
        Dim mainForm = Application.OpenForms _
                            .OfType(Of FrmMain)() _
                            .FirstOrDefault()
        If mainForm Is Nothing Then
            slblModify.Text = "Main form not found."
            Return
        End If

        Using g As Graphics = Me.CreateGraphics()
            dpiX = g.DpiX
            dpiY = g.DpiY
        End Using

        pbModify.Size = New Size(
        (438 * dpiX / 96),
        (5 * dpiY / 96))

        slblModify.Size = New Size(
        (438 * dpiX / 96),
        (20 * dpiY / 96))

        Dim count = mainForm.lbInitData.SelectedItems.Count
        If count > 1 Then
            slblModify.Text = $"Enter the new value for the {count} selected items."
        Else
            slblModify.Text = "Enter the new value for the selected item."
        End If

        txtInitEdit.Text = mainForm.lbInitData.SelectedItem.ToString()
        txtInitEdit.SelectAll()
        txtInitEdit.Select()
    End Sub

    Private Sub txtInitEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtInitEdit.KeyDown
        If e.KeyData = Keys.Enter Then
            btnOK.PerformClick()
            e.SuppressKeyPress = True
        ElseIf e.KeyData = Keys.Escape Then
            btnCancel.PerformClick()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub txtInitEdit_TextChanged(sender As Object, e As EventArgs) Handles txtInitEdit.TextChanged
        btnOK.Enabled = txtInitEdit.Text.Length > 0 AndAlso Double.TryParse(txtInitEdit.Text, Nothing)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

#Region "Mouse Hover / Leave Handlers"
    Private Sub MouseLeaveHandler(sender As Object, e As EventArgs)
        Dim count As Integer = FrmMain.lbInitData.SelectedItems.Count
        If count > 1 Then
            ' 다수의 항목이 선택된 경우
            slblModify.Text = $"Enter the new value for the {count} selected items."
        Else
            slblModify.Text = "Enter the new value for the selected item."
        End If
    End Sub

    Private Sub FrmModify_MouseHover(sender As Object, e As EventArgs) Handles MyBase.MouseHover
        MouseLeaveHandler(sender, e)
    End Sub

    Private Sub txtInitEdit_MouseHover(sender As Object, e As EventArgs) Handles txtInitEdit.MouseHover
        slblModify.Text = "To modify the selected items, enter a new value and click 'OK'."
    End Sub

    Private Sub txtInitEdit_MouseLeave(sender As Object, e As EventArgs) Handles txtInitEdit.MouseLeave
        MouseLeaveHandler(sender, e)
    End Sub

    Private Sub btnOK_MouseHover(sender As Object, e As EventArgs) Handles btnOK.MouseHover
        slblModify.Text = "Click to apply the new value to the selected items."
    End Sub

    Private Sub btnOK_MouseLeave(sender As Object, e As EventArgs) Handles btnOK.MouseLeave
        MouseLeaveHandler(sender, e)
    End Sub

    Private Sub btnCancel_MouseHover(sender As Object, e As EventArgs) Handles btnCancel.MouseHover
        slblModify.Text = "Click to cancel the modification and close the dialog."
    End Sub

    Private Sub btnCancel_MouseLeave(sender As Object, e As EventArgs) Handles btnCancel.MouseLeave
        MouseLeaveHandler(sender, e)
    End Sub
#End Region

End Class
