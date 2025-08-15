Imports System.Globalization
Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms.Application
Imports Excel = Microsoft.Office.Interop.Excel

Public Class FrmMain

    Dim dpivalue As Double
    Dim borderCount As Integer

    Private Const ExcelTitlePlaceholder As String = "여기를 클릭하여 제목을 입력하세요."

    Private sourceList As New List(Of Double)
    Private medianList As New List(Of Double)

    Private Shared ReadOnly patternFindNumbers As String =
        "[+-]?(\d+(,\d{3})*|(?=\.\d))((\.\d+([eE][+-]\d+)?)|)"
    Private Shared ReadOnly patternHtmlParse As String =
        "(?<=>.*)[+-]?" & patternFindNumbers & "(?=[^>]*<)"

    Private Shared ReadOnly regexNumbers As New Regex(patternFindNumbers, RegexOptions.Compiled)
    Private Shared ReadOnly regexHtmlNumbers As New Regex(patternHtmlParse, RegexOptions.Compiled)
    Private Shared ReadOnly regexStripTags As New Regex("<.*?>", RegexOptions.Compiled)

    Public Sub New()
        InitializeComponent()

        Me.KeyPreview = True
    End Sub

    Sub MiddleMedian()
        Dim n = sourceList.Count
        If n = 0 Then Return

        Dim arr = sourceList.ToArray()
        Dim buffer(n - 1) As Double

        buffer(0) = arr(0)
        If n > 1 Then buffer(1) = arr(1)
        If n > 2 Then buffer(n - 2) = arr(n - 2)
        buffer(n - 1) = arr(n - 1)

        Parallel.For(2, n - 2, Sub(i)
                                   Dim win(4) As Double
                                   ' i-2 .. i+2 복사
                                   For k = 0 To 4
                                       win(k) = arr(i + k - 2)
                                   Next
                                   Quicksort(win, 0, 4)
                                   buffer(i) = win(2)
                               End Sub)

        medianList.Clear()
        medianList.AddRange(buffer)
    End Sub


    Sub AllMedian()
        Dim n = sourceList.Count
        If n = 0 Then Return

        Dim arr = sourceList.ToArray()
        Dim buffer(n - 1) As Double

        Parallel.For(0, n, Sub(i)
                               Dim iMin = If(i < 2, 0, i - 2)
                               Dim iMax = If(i > n - 3, n - 1, i + 2)
                               Dim win(4) As Double
                               Dim k = 0
                               For j = iMin To iMax
                                   win(k) = arr(j)
                                   k += 1
                               Next
                               Quicksort(win, 0, 4)
                               buffer(i) = win(2)
                           End Sub)

        medianList.Clear()
        medianList.AddRange(buffer)
    End Sub

    Private Function MedianOf5(a As Double, b As Double,
                           c As Double, d As Double, e As Double) As Double
        If a > b Then Swap(a, b)
        If d > e Then Swap(d, e)
        If a > c Then Swap(a, c)
        If b > c Then Swap(b, c)
        If a > d Then Swap(a, d)
        If c > e Then Swap(c, e)
        If b > d Then Swap(b, d)
        Return c
    End Function

    Private Function MedianOfWindow(w As Double(), length As Integer) As Double
        For i As Integer = 1 To length - 1
            Dim key = w(i), j = i - 1
            While j >= 0 AndAlso w(j) > key
                w(j + 1) = w(j)
                j -= 1
            End While
            w(j + 1) = key
        Next
        Return w(length \ 2)
    End Function

    Private Sub Swap(ByRef x As Double, ByRef y As Double)
        Dim t = x : x = y : y = t
    End Sub

    Private Sub ComputeMedians(
        useMiddle As Boolean,
        kernelWidth As Integer,
        borderCount As Integer,
        progress As IProgress(Of Integer)
    )
        Dim n = sourceList.Count
        If n = 0 Then
            progress.Report(0)
            Return
        End If

        Dim arr = sourceList.ToArray()
        Dim buffer(n - 1) As Double

        Dim offsetLow = (kernelWidth - 1) \ 2
        Dim offsetHigh = (kernelWidth - 1) - offsetLow

        Dim processed As Integer = 0
        Dim reportInterval = Math.Max(1, n \ 200)
        progress.Report(0)

        Dim localWin As New ThreadLocal(Of Double())(
            Function() New Double(kernelWidth - 1) {}
        )

        If useMiddle Then
            For i As Integer = 0 To borderCount - 1
                buffer(i) = arr(i)
                buffer(n - 1 - i) = arr(n - 1 - i)
                processed += 2
                If processed Mod reportInterval = 0 Then
                    progress.Report(processed)
                End If
            Next
        End If

        Dim startIdx = If(useMiddle, borderCount, 0)
        Dim endIdx = If(useMiddle, n - borderCount - 1, n - 1)

        Parallel.For(startIdx, endIdx + 1, Sub(i)
                                               Dim win = localWin.Value

                                               Dim iMin = Math.Max(0, i - offsetLow)
                                               Dim iMax = Math.Min(n - 1, i + offsetHigh)
                                               Dim length = iMax - iMin + 1

                                               For k As Integer = 0 To length - 1
                                                   win(k) = arr(iMin + k)
                                               Next

                                               buffer(i) = GetWindowMedian(win, length)

                                               Dim cnt = Interlocked.Increment(processed)
                                               If cnt Mod reportInterval = 0 Then
                                                   progress.Report(cnt)
                                               End If
                                           End Sub)

        progress.Report(n)

        medianList.Clear()
        medianList.AddRange(buffer)
    End Sub

    Private Function GetWindowMedian(win() As Double, length As Integer) As Double
        Dim slice = win.Take(length).ToArray()
        Array.Sort(slice)
        Dim mid = length \ 2
        If length Mod 2 = 0 Then
            Return (slice(mid - 1) + slice(mid)) / 2.0
        Else
            Return slice(mid)
        End If
    End Function

    Private Sub addButton_Click(sender As Object, e As EventArgs) Handles addButton.Click
        Dim inputText = TextBox1.Text
        Dim v As Double

        If Double.TryParse(inputText, v) Then
            ListBox1.Items.Add(v)
            lblCnt1.Text = $"데이터 개수 : {ListBox1.Items.Count}"
        End If

        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
        TextBox1.Clear()
    End Sub

    Public Sub Quicksort(ByVal list() As Double, ByVal min As Integer, ByVal max As Integer)
        Dim random_number As New Random
        Dim med_value As Double
        Dim hi As Integer
        Dim lo As Integer
        Dim i As Integer

        If min >= max Then Exit Sub

        i = random_number.Next(min, max + 1)
        med_value = list(i)

        list(i) = list(min)

        lo = min
        hi = max
        Do
            Do While list(hi) >= med_value
                hi = hi - 1
                If hi <= lo Then Exit Do
            Loop
            If hi <= lo Then
                list(lo) = med_value
                Exit Do
            End If

            list(lo) = list(hi)

            lo = lo + 1
            Do While list(lo) < med_value
                lo = lo + 1
                If lo >= hi Then Exit Do
            Loop
            If lo >= hi Then
                lo = hi
                list(hi) = med_value
                Exit Do
            End If

            list(hi) = list(lo)
        Loop

        Quicksort(list, min, lo - 1)
        Quicksort(list, lo + 1, max)
    End Sub

    Sub DisplayResults()
        ListBox2.BeginUpdate()
        For Each v As Double In medianList
            ListBox2.Items.Add(v)
        Next
        ListBox2.EndUpdate()
    End Sub

    Private Async Sub calcButton_Click(sender As Object, e As EventArgs) Handles calcButton.Click
        If ListBox1.Items.Count = 0 Then
            Return
        End If

        Dim parsedList As New List(Of Double)
        For Each item As Object In ListBox1.Items
            Dim strValue As String = item.ToString()
            Dim dValue As Double
            If Double.TryParse(strValue, dValue) Then
                parsedList.Add(dValue)
            Else
                MessageBox.Show(
                    $"값 '{strValue}'(을)를 숫자로 변환할 수 없습니다.",
                    "Avocado Smoothie",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Return
            End If
        Next

        sourceList = parsedList
        Dim total = sourceList.Count
        Dim useMiddle = RadioButton1.Checked

        Dim radius As Integer
        If Not Integer.TryParse(cbxKernelWidth.Text, radius) Then
            MessageBox.Show(
                "커널 반경을 선택해 주세요.",
                "Avocado Smoothie",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Return
        End If

        Dim kernelWidth As Integer = 2 * radius + 1

        Dim borderCount As Integer
        If Not Integer.TryParse(cbxBorderCount.Text, borderCount) Then
            MessageBox.Show(
                "경계 계수를 선택해 주세요.",
                "Avocado Smoothie",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Return
        End If

        If Not ValidateSmoothingParameters(total, kernelWidth, borderCount, useMiddle) Then
            Return
        End If

        progressBar1.Minimum = 0
        progressBar1.Maximum = total
        progressBar1.Value = 0
        Dim progress = New Progress(Of Integer)(
        Sub(v) progressBar1.Value = Math.Min(v, total)
    )

        Await Task.Run(Sub()
                           ComputeMedians(
                           useMiddle:=useMiddle,
                           kernelWidth:=kernelWidth,
                           borderCount:=borderCount,
                           progress:=progress
                       )
                       End Sub)

        ListBox2.BeginUpdate()
        ListBox2.Items.Clear()
        ListBox2.Items.AddRange(medianList.Cast(Of Object).ToArray())
        ListBox2.EndUpdate()
        ListBox2.TopIndex = ListBox2.Items.Count - 1

        lblCnt1.Text = $"데이터 개수 : {total}"
        lblCnt2.Text = $"데이터 개수 : {medianList.Count}"
        slblCalibratedType.Text = If(useMiddle, "중앙 구간 이동 중간 값", "전체 구간 이동 중간 값")
        slblKernelWidth.Text = $"{kernelWidth}"
        slblBorderCount.Text = $"{borderCount}"

        slblBorderCount.Visible = useMiddle
        tlblBorderCount.Visible = useMiddle
        slblSeparator2.Visible = useMiddle

        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)

        Await Task.Delay(200)
        progressBar1.Value = 0
    End Sub



    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputText = TextBox1.Text
            Dim v As Double

            If Double.TryParse(inputText, v) Then
                ListBox1.Items.Add(v)
            End If

            lblCnt1.Text = $"데이터 개수 : {ListBox1.Items.Count}"
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)

            TextBox1.Clear()
            e.SuppressKeyPress = True
        End If

    End Sub

    Private Sub copyButton1_Click(sender As Object, e As EventArgs) Handles copyButton1.Click
        Dim doubles As New List(Of Double)
        Dim source = If(ListBox1.SelectedItems.Count > 0, ListBox1.SelectedItems, ListBox1.Items)

        For Each itm As Object In source
            Dim txt = itm.ToString()
            Dim num As Double
            If Double.TryParse(txt, num) Then
                doubles.Add(num)
            Else
                MessageBox.Show($"값 '{txt}'(을)를 숫자로 변환할 수 없습니다.",
                                "복사 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        If doubles.Any() Then
            Clipboard.SetText(String.Join(Environment.NewLine, doubles))
        End If
    End Sub

    Private Sub copyButton2_Click(sender As Object, e As EventArgs) Handles copyButton2.Click
        Dim doubles As New List(Of Double)
        Dim source = If(ListBox1.SelectedItems.Count > 0, ListBox1.SelectedItems, ListBox2.Items)

        For Each itm As Object In source
            Dim txt = itm.ToString()
            Dim num As Double
            If Double.TryParse(txt, num) Then
                doubles.Add(num)
            Else
                MessageBox.Show($"값 '{txt}'(을)를 숫자로 변환할 수 없습니다.",
                                "복사 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        If doubles.Any() Then
            Clipboard.SetText(String.Join(Environment.NewLine, doubles))
        End If
    End Sub


    Private Sub clearButton1_Click(sender As Object, e As EventArgs) Handles clearButton1.Click
        Dim itemCount As Integer = ListBox1.Items.Count

        Dim result As DialogResult = MessageBox.Show(
            $"초기 데이터 목록에서 {itemCount}개 항목을 모두 삭제합니다." & vbCrLf &
            "정제된 데이터 목록의 모든 항목도 함께 삭제됩니다." & vbCrLf & vbCrLf &
            "계속 진행하시겠습니까?",
            "삭제 확인",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

        If result = DialogResult.No Then
            Return
        End If


        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        txtDatasetTitle.Text = ExcelTitlePlaceholder
        txtDatasetTitle.ForeColor = Color.Gray
        txtDatasetTitle.TextAlign = HorizontalAlignment.Center

        TextBox1.Text = String.Empty

        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)

        lblCnt1.Text = "데이터 개수 : " & ListBox1.Items.Count
        TextBox1.Select()
    End Sub

    Private Sub clearButton2_Click(sender As Object, e As EventArgs) Handles clearButton2.Click
        Dim itemCount As Integer = ListBox2.Items.Count

        Dim result As DialogResult = MessageBox.Show(
            $"정제된 데이터 목록에서 {itemCount}개 항목을 모두 삭제합니다." & vbCrLf & vbCrLf &
            "계속 진행하시겠습니까?",
            "삭제 확인",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        )

        If result = DialogResult.No Then
            Return
        End If

        ListBox2.Items.Clear()
        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)

        lblCnt2.Text = "데이터 개수 : " & ListBox2.Items.Count
        ListBox2.Select()
    End Sub

    Private Async Function DeleteSelectedItemsPreserveSelection(lb As ListBox, progressBar As ProgressBar, lblCount As Label) As Task

        Dim indices = lb.SelectedIndices.Cast(Of Integer)().OrderByDescending(Function(i) i).ToArray()
        Dim total = indices.Length
        If total = 0 Then Return

        progressBar.Minimum = 0
        progressBar.Maximum = total
        progressBar.Value = 0

        Dim newSelections = New List(Of Integer)
        Dim shiftMap = New Dictionary(Of Integer, Boolean)

        For Each idx In indices
            shiftMap(idx) = True
            If idx < lb.Items.Count - 1 Then
                newSelections.Add(idx) ' 동일 위치에 선택 유지 시도
            End If
        Next

        lb.SuspendLayout()
        lb.BeginUpdate()

        Dim updateInterval = Math.Max(1, total \ 100)
        For i = 0 To total - 1
            lb.Items.RemoveAt(indices(i))
            If ((i + 1) Mod updateInterval = 0) OrElse i = total - 1 Then
                progressBar.Value = i + 1
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        lb.EndUpdate()
        lb.ResumeLayout()

        lb.SelectedIndices.Clear()
        lb.ClearSelected()

        For Each idx In newSelections
            If idx < lb.Items.Count Then lb.SelectedIndices.Add(idx)
        Next

        lblCount.Text = $"데이터 개수 : {lb.Items.Count}"
        Await Task.Delay(200)
        progressBar.Value = 0
    End Function

    Private Async Sub deleteButton1_Click(sender As Object, e As EventArgs) Handles deleteButton1.Click
        Dim selectedCount As Integer = ListBox1.SelectedIndices.Count
        Dim totalCount As Integer = ListBox1.Items.Count
        Dim selectedItems As Boolean = ListBox1.SelectedItems.Count > 0
        Dim message As String

        If selectedCount = 0 Then
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
            Return
        End If

        If selectedCount = totalCount Then
            message = $"초기 데이터 목록에서 {totalCount} 개의 항목을 모두 삭제합니다." & vbCrLf &
              "정제된 데이터 목록의 모든 항목도 함께 삭제됩니다." & vbCrLf & vbCrLf &
              "계속 진행하시겠습니까?"
        Else
            message = $"초기 데이터 목록에서 선택한 {selectedCount} 개의 항목을 삭제합니다." &
              vbCrLf & vbCrLf & "계속 진행하시겠습니까?"
        End If

        Dim result As DialogResult = MessageBox.Show(message, "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If result = DialogResult.No Then
            Return
        End If

        If selectedCount = totalCount Then
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            copyButton1.Enabled = False
            lblCnt1.Text = "데이터 개수 : " & ListBox1.Items.Count
            lblCnt2.Text = "데이터 개수 : " & ListBox2.Items.Count
            progressBar1.Value = 0

            txtDatasetTitle.Text = ExcelTitlePlaceholder
            txtDatasetTitle.ForeColor = Color.Gray
            txtDatasetTitle.TextAlign = HorizontalAlignment.Center

            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)

            TextBox1.Select()
            Return
        End If

        Await DeleteSelectedItemsPreserveSelection(ListBox1, progressBar1, lblCnt1)
        lblCnt1.Text = "데이터 개수 : " & ListBox1.Items.Count

        ListBox1.Select()

        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
    End Sub

    Private Async Sub ListBox1_DragDrop(sender As Object, e As DragEventArgs) Handles ListBox1.DragDrop
        calcButton.Enabled = False
        progressBar1.Style = ProgressBarStyle.Continuous
        progressBar1.Minimum = 0
        progressBar1.Maximum = 100
        progressBar1.Value = 0

        Try
            Dim htmlFormat As String = If(e.Data.GetDataPresent("HTML Format"),
            e.Data.GetData("HTML Format").ToString(),
            String.Empty)

            Dim raw As String = If(
            Not String.IsNullOrEmpty(htmlFormat) AndAlso
            htmlFormat.Contains("urn:schemas-microsoft-com:office:word"),
            regexStripTags.Replace(htmlFormat, ""),
            e.Data.GetData("Text").ToString())

            progressBar1.Value = 10

            If String.IsNullOrWhiteSpace(raw) Then Return

            If raw.IndexOf("<html", StringComparison.OrdinalIgnoreCase) >= 0 Then
                raw = Await Task.Run(Function()
                                         Return regexStripTags.Replace(raw, " ")
                                     End Function)
                progressBar1.Value = 25

                If String.IsNullOrWhiteSpace(raw) Then Return
            End If

            Dim parsed As Double() = Await Task.Run(Function()
                                                        Dim ci = Globalization.CultureInfo.InvariantCulture
                                                        Return regexNumbers.Matches(raw) _
                .Cast(Of Match)() _
                .AsParallel() _
                .AsOrdered() _
                .WithDegreeOfParallelism(Environment.ProcessorCount) _
                .Select(Function(m)
                            Dim tok = m.Value.Replace(",", "").Trim()
                            Dim d As Double
                            If Double.TryParse(tok, Globalization.NumberStyles.Any, ci, d) Then
                                Return d
                            Else
                                Return Double.NaN
                            End If
                        End Function) _
                .Where(Function(d) Not Double.IsNaN(d)) _
                .ToArray()
                                                    End Function)
            progressBar1.Value = 60

            If parsed.Length = 0 Then Return

            Dim baseProgress As Integer = 60
            Dim progressReporter As IProgress(Of Integer) = New Progress(Of Integer)(
            Sub(pct)
                Dim adjustedPct = Math.Max(baseProgress, Math.Min(100, pct))
                progressBar1.Value = adjustedPct
                progressBar1.Refresh()
            End Sub)

            Await AddItemsInBatches(ListBox1, parsed, progressReporter, baseProgress)

            progressBar1.Value = 100
            lblCnt1.Text = "데이터 개수 : " & ListBox1.Items.Count
            Await Task.Delay(200)
        Finally
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)

            progressBar1.Value = 0
            calcButton.Enabled = True
        End Try
    End Sub

    Private Async Function AddItemsInBatches(
    box As System.Windows.Forms.ListBox,
    items As Double(),
    progress As IProgress(Of Integer),
    baseProgress As Integer
) As Task
        Const BatchSize As Integer = 1000
        Dim total As Integer = items.Length
        Dim done As Integer = 0

        box.BeginUpdate()

        While done < total
            Dim cnt As Integer = Math.Min(BatchSize, total - done)
            Dim chunk As Object() = items _
            .Skip(done) _
            .Take(cnt) _
            .Cast(Of Object)() _
            .ToArray()

            box.Items.AddRange(chunk)
            done += cnt

            Dim pct As Integer = baseProgress + CInt(done * (100L - baseProgress) / total)
            progress.Report(pct)

            Await Task.Delay(1)
        End While

        box.EndUpdate()
        box.TopIndex = box.Items.Count - 1
    End Function

    Private Sub ListBox1_DragEnter(sender As Object, e As DragEventArgs) Handles ListBox1.DragEnter
        e.Effect = If(
             e.Data.GetDataPresent(DataFormats.Text) OrElse
             e.Data.GetDataPresent("HTML Format"),
             DragDropEffects.Copy, DragDropEffects.None)
    End Sub

    Private Async Sub pasteButton_Click(sender As Object, e As EventArgs) Handles pasteButton.Click
        progressBar1.Style = ProgressBarStyle.Continuous
        progressBar1.Minimum = 0
        progressBar1.Maximum = 100
        progressBar1.Value = 0
        calcButton.Enabled = False

        Try
            Dim text As String = Clipboard.GetText()
            progressBar1.Value = 10

            Dim matches = regexNumbers.Matches(text) _
            .Cast(Of Match)() _
            .Where(Function(m) Not String.IsNullOrEmpty(m.Value)) _
            .ToArray()
            progressBar1.Value = 30

            Dim values As Double() = Await Task.Run(Function()
                                                        Return matches _
                .AsParallel() _
                .WithDegreeOfParallelism(Environment.ProcessorCount) _
                .Select(Function(m) Double.Parse(
                    m.Value,
                    Globalization.NumberStyles.Any,
                    Globalization.CultureInfo.InvariantCulture
                )) _
                .ToArray()
                                                    End Function)

            progressBar1.Value = 70

            If values.Length = 0 Then
                Return
            End If

            ListBox1.BeginUpdate()
            ListBox1.Items.AddRange(values.Cast(Of Object)().ToArray())
            ListBox1.EndUpdate()
            ListBox1.TopIndex = ListBox1.Items.Count - 1

            progressBar1.Value = 100
            lblCnt1.Text = $"데이터 개수 : {ListBox1.Items.Count}"
            Await Task.Delay(200)
        Finally
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
            UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)

            progressBar1.Value = 0
            calcButton.Enabled = True
        End Try
    End Sub


    Private Async Sub selectAll1_Click(sender As Object, e As EventArgs) Handles selectAllButton1.Click
        Await SelectAllWithProgress(ListBox1, progressBar1)
    End Sub

    Private Async Sub selectAll2_Click(sender As Object, e As EventArgs) Handles selectAllButton2.Click
        Await SelectAllWithProgress(ListBox2, progressBar1)
    End Sub


    Private Async Function SelectAllWithProgress(lb As ListBox, progressBar As ProgressBar) As Task
        Dim count As Integer = lb.Items.Count
        If count = 0 Then Return

        progressBar.Minimum = 0
        progressBar.Maximum = count
        progressBar.Value = 0

        lb.SuspendLayout()
        lb.BeginUpdate()

        Dim updateInterval As Integer = Math.Max(1, count \ 100)

        For i As Integer = 0 To count - 1
            lb.SetSelected(i, True)

            If ((i + 1) Mod updateInterval = 0) OrElse i = count - 1 Then
                progressBar.Value = i + 1
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        lb.EndUpdate()
        lb.ResumeLayout()
        lb.Focus()

        progressBar.Value = count

        Await Task.Delay(200)
        progressBar.Value = 0
    End Function

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyData = Keys.Delete Then
            deleteButton1.PerformClick()
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.Delete Then
            clearButton1.PerformClick()
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.C Then
            copyButton1.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.V Then
            pasteButton.PerformClick()
            UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.A Then
            selectAllButton1.PerformClick()
        End If

        If (e.KeyData = Keys.F2) Then
            If ListBox1.SelectedItems.Count >= 0 Then
                FrmModify.ShowDialog(Me)
            End If
        End If

        If e.KeyData = Keys.Escape Then
            sClrButton1.PerformClick()
        End If

        lblCnt1.Text = "데이터 개수 : " & ListBox1.Items.Count
    End Sub


    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.C Then
            e.Handled = True
            copyButton2.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.A Then
            e.Handled = True
            UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.Delete Then
            clearButton2.PerformClick()
            UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
        End If

        If e.KeyData = Keys.Escape Then
            sClrButton2.PerformClick()
            UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
        End If

        lblCnt2.Text = "데이터 개수 : " & ListBox2.Items.Count
    End Sub

    Private Async Function ClearSelectionWithProgress(lb As ListBox,
                                                  progressBar As ProgressBar,
                                                  lblCount As Label) As Task
        Dim count As Integer = lb.SelectedIndices.Count
        If count = 0 Then Return

        progressBar.Minimum = 0
        progressBar.Maximum = count
        progressBar.Value = 0

        lb.SuspendLayout()
        lb.BeginUpdate()

        ' 선택 Index 복사 (변경 시 오류 방지)
        Dim selectedIndices = lb.SelectedIndices.Cast(Of Integer).ToArray()
        Dim updateInterval As Integer = Math.Max(1, count \ 100)

        For i As Integer = 0 To selectedIndices.Length - 1
            lb.SetSelected(selectedIndices(i), False)

            If ((i + 1) Mod updateInterval = 0) OrElse i = count - 1 Then
                progressBar.Value = i + 1
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        lb.EndUpdate()
        lb.ResumeLayout()
        lb.Select()

        lblCount.Text = "데이터 개수 : " & lb.Items.Count

        Await Task.Delay(200)
        progressBar.Value = 0
    End Function

    Private Async Sub sClrButton1_Click(sender As Object, e As EventArgs) Handles sClrButton1.Click
        Await ClearSelectionWithProgress(ListBox1, progressBar1, lblCnt1)
    End Sub

    Private Async Sub sClrButton2_Click(sender As Object, e As EventArgs) Handles sClrButton2.Click
        Await ClearSelectionWithProgress(ListBox2, progressBar1, lblCnt2)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        addButton.Enabled = CBool(TextBox1.TextLength) AndAlso CBool(IsNumeric(TextBox1.Text))
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cbxBorderCount.SelectedIndex = 0
        cbxKernelWidth.SelectedItem = "5"

        If String.IsNullOrWhiteSpace(txtDatasetTitle.Text) Then
            txtDatasetTitle.Text = ExcelTitlePlaceholder
            txtDatasetTitle.ForeColor = Color.Gray
        End If

        AddHandler ListBox1.SelectedIndexChanged, AddressOf UpdateListBox1ButtonsState
        AddHandler ListBox2.SelectedIndexChanged, AddressOf UpdateListBox2ButtonsState

        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)
        UpdateListBox2ButtonsState(Nothing, EventArgs.Empty)
    End Sub

    Private Sub UpdateListBox1ButtonsState(s As Object, e As EventArgs)
        Dim hasItems As Boolean = (ListBox1.Items.Count > 0)
        Dim hasSelection As Boolean = (ListBox1.SelectedItems.Count > 0)
        Dim titleValid As Boolean = (txtDatasetTitle.TextLength > 0 AndAlso txtDatasetTitle.Text <> ExcelTitlePlaceholder)
        Dim canSync As Boolean = (ListBox1.Items.Count = ListBox2.Items.Count) AndAlso hasSelection

        copyButton1.Enabled = hasItems
        editButton.Enabled = hasSelection
        deleteButton1.Enabled = hasSelection
        selectAllButton1.Enabled = hasItems
        sClrButton1.Enabled = hasSelection
        clearButton1.Enabled = hasItems
        calcButton.Enabled = hasItems
        btnExport.Enabled = hasItems AndAlso titleValid
        syncButton1.Enabled = canSync
    End Sub

    Private Sub UpdateListBox2ButtonsState(s As Object, e As EventArgs)
        Dim hasItems As Boolean = (ListBox2.Items.Count > 0)
        Dim hasSelection As Boolean = (ListBox2.SelectedItems.Count > 0)
        Dim canSync As Boolean = (ListBox2.Items.Count = ListBox1.Items.Count) AndAlso hasSelection
        copyButton2.Enabled = hasItems
        clearButton2.Enabled = hasItems
        selectAllButton2.Enabled = hasItems
        sClrButton2.Enabled = hasSelection
        syncButton2.Enabled = canSync
    End Sub


    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            lblBorderCount.Enabled = False
            cbxBorderCount.Enabled = False
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            lblBorderCount.Enabled = True
            cbxBorderCount.Enabled = True
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles editButton.Click
        FrmModify.ShowDialog(Me)
    End Sub

    Dim maxRows = 1048573
    Dim EXCEL_MAX_ROW = 1048576


    Private Async Function ExportCsvAsync() As Task
        ' ProgressBar 초기화
        progressBar1.Style = ProgressBarStyle.Continuous
        progressBar1.Minimum = 0
        progressBar1.Maximum = 100
        progressBar1.Value = 0

        ' Parameters 읽기
        Dim kernelWidth As Integer
        If Not Integer.TryParse(cbxKernelWidth.Text, kernelWidth) Then
            MessageBox.Show("커널 반경을 선택해 주세요.", "CSV 내보내기",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        Dim borderCount As Integer
        If Not Integer.TryParse(cbxBorderCount.Text, borderCount) Then
            MessageBox.Show("경계 계수를 선택해 주세요.", "CSV 내보내기",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' 원본 데이터 파싱
        Dim initialData = ListBox1.Items.Cast(Of Object)() _
                    .Select(Function(x)
                                Dim d As Double
                                Return If(Double.TryParse(x?.ToString(),
                                                          NumberStyles.Any,
                                                          CultureInfo.InvariantCulture,
                                                          d),
                                          d, Double.NaN)
                            End Function) _
                    .Where(Function(d) Not Double.IsNaN(d)) _
                    .ToArray()
        Dim n = initialData.Length
        If n = 0 Then
            MessageBox.Show("내보낼 데이터가 없습니다.", "CSV 내보내기",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' All Median / Middle Median 계산
        Dim middleMedian(n - 1) As Double
        Dim allMedian(n - 1) As Double

        sourceList = initialData.ToList()
        Dim middleProg = New Progress(Of Integer)(
        Sub(v) progressBar1.Value = Math.Max(progressBar1.Minimum,
                                             Math.Min(v, progressBar1.Maximum))
    )
        Await Task.Run(Sub()
                           ComputeMedians(True, kernelWidth, borderCount, middleProg)
                           medianList.CopyTo(0, middleMedian, 0, n)
                       End Sub)

        sourceList = initialData.ToList()
        Dim allProg = New Progress(Of Integer)(
        Sub(v) progressBar1.Value = Math.Max(progressBar1.Minimum,
                                             Math.Min(v, progressBar1.Maximum))
    )
        Await Task.Run(Sub()
                           ComputeMedians(False, kernelWidth, borderCount, allProg)
                           medianList.CopyTo(0, allMedian, 0, n)
                       End Sub)

        ' 저장 경로 지정
        Dim basePath As String
        Using dlg As New SaveFileDialog()
            dlg.Filter = "CSV files (*.csv)|*.csv"
            dlg.DefaultExt = "csv"
            dlg.AddExtension = True
            If dlg.ShowDialog(Me) <> DialogResult.OK Then Return
            basePath = dlg.FileName
        End Using

        ' 분할 저장 설정
        Const EXCEL_MAX_ROW As Integer = 1048576
        Const HEADER_LINES As Integer = 10
        Dim maxDataRows = EXCEL_MAX_ROW - HEADER_LINES - 1
        Dim partCount = CInt(Math.Ceiling(n / CDbl(maxDataRows)))

        Dim dir = Path.GetDirectoryName(basePath)
        Dim nameOnly = Path.GetFileNameWithoutExtension(basePath)
        Dim ext = Path.GetExtension(basePath)

        ' 파트별 파일 쓰기
        Dim createdFiles As New List(Of String)()   ' 추가: 생성된 파일 경로 저장용

        For part = 0 To partCount - 1
            Dim startIdx = part * maxDataRows
            Dim count = Math.Min(maxDataRows, n - startIdx)
            Dim filePath As String

            If partCount > 1 Then
                filePath = Path.Combine(dir, $"{nameOnly}_Part{part + 1}{ext}")
            Else
                filePath = basePath
            End If

            Using fs As New FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None),
              sw As New StreamWriter(fs, Encoding.UTF8)

                ' 제목 & Parameters
                sw.WriteLine(txtDatasetTitle.Text)
                sw.WriteLine($"Part {part + 1} of {partCount}")
                sw.WriteLine()
                sw.WriteLine("보정 설정")
                sw.WriteLine($"측정 오차 제거 필터 반경 :  {kernelWidth}")
                sw.WriteLine($"경계 유지 계수 : {borderCount}")
                sw.WriteLine()
                sw.WriteLine($"생성 일시 : {DateTime.Now.ToString("G", CultureInfo.CurrentCulture)}")
                sw.WriteLine()
                sw.WriteLine("초기 데이터,중앙 구간 이동 중간 값,전체 구간 이동 중간 값")

                ' 데이터 쓰기
                For i = startIdx To startIdx + count - 1
                    Dim line = String.Join(",",
                        initialData(i).ToString("G17", CultureInfo.InvariantCulture),
                        middleMedian(i).ToString("G17", CultureInfo.InvariantCulture),
                        allMedian(i).ToString("G17", CultureInfo.InvariantCulture))
                    sw.WriteLine(line)

                    ' 진행률 업데이트
                    Dim pct = CInt((i + 1) / CSng(n) * 100)
                    progressBar1.Value = Math.Max(progressBar1.Minimum,
                                              Math.Min(pct, progressBar1.Maximum))
                Next
            End Using

            createdFiles.Add(filePath)   ' 추가
        Next

        For Each file In createdFiles
            Try
                Process.Start(New ProcessStartInfo(file) With {
                .UseShellExecute = True
            })
            Catch ex As System.ComponentModel.Win32Exception
                Process.Start(New ProcessStartInfo("rundll32.exe",
                       $"shell32.dll,OpenAs_RunDLL ""{file}""") With {
                .UseShellExecute = True
            })
            Catch ex As Exception
                MessageBox.Show($"죄송합니다. 파일을 열 수 없습니다: {file}{vbCrLf}{ex.Message}",
                "파일 열기 오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Next
    End Function


    Private Function ValidateSmoothingParameters(dataCount As Integer, kernelWidth As Integer, borderCount As Integer, useMiddle As Boolean) As Boolean
        Dim windowSize As Integer = kernelWidth
        Dim radius As Integer = Integer.Parse(cbxKernelWidth.Text)
        Dim borderTotalWidth As Integer

        If windowSize > dataCount Then
            MessageBox.Show(
                $"커널 반경 ({kernelWidth}) 이 너무 큽니다.{Environment.NewLine}" &
                $"윈도우 크기 (2 × 반경 + 1 = {windowSize}) 는 데이터 개수 ({dataCount}) 를 초과할 수 없습니다.",
                "파라미터 오류",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Return False
        End If

        If borderCount > dataCount Then
            MessageBox.Show(
                $"경계 계수가 너무 큽니다.{Environment.NewLine}" &
                $"경계 계수 ({borderCount}) 는 데이터 개수({dataCount})를 초과할 수 없습니다.",
                "파라미터 오류",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Return False
        End If

        If useMiddle AndAlso borderTotalWidth >= windowSize Then
            MessageBox.Show(
                $"경계 너비가 윈도우 크기에 비해 너무 큽니다.{Environment.NewLine}" &
                $"총 경계 너비 ({borderTotalWidth}) 는 윈도우 크기 (2 × 반경 + 1 = {windowSize}) 보다 작아야 합니다.",
                "파라미터 오류",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Return False
        End If

        Return True
    End Function

    Private Function IsRangeEmpty(rng As Excel.Range) As Boolean
        Dim v = rng.Value2
        If v Is Nothing Then Return True
        If TypeOf v Is Object(,) Then
            Dim arr = CType(v, Object(,))
            For Each cell In arr
                If cell IsNot Nothing AndAlso cell.ToString <> "" Then Return False
            Next
            Return True
        ElseIf TypeOf v Is Object() Then
            Dim arr = CType(v, Object())
            For Each cell In arr
                If cell IsNot Nothing AndAlso cell.ToString <> "" Then Return False
            Next
            Return True
        Else
            Return v.ToString = ""
        End If
    End Function

    Private Async Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        If rbtnCSV.Checked Then
            Await ExportCsvAsync()
            Return
        ElseIf rbtnXLSX.Checked Then
            progressBar1.Style = ProgressBarStyle.Continuous
            progressBar1.Minimum = 0
            progressBar1.Maximum = 100
            progressBar1.Value = 0

            ' Kernel / 경계 값 읽기
            Dim radius As Integer
            If Not Integer.TryParse(cbxKernelWidth.Text, radius) Then
                MessageBox.Show("커널 반경을 선택해 주세요.", "엑셀 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim kernelWidth As Integer = 2 * radius + 1

            Dim borderCount As Integer
            If Not Integer.TryParse(cbxBorderCount.Text, borderCount) Then
                MessageBox.Show("경계 계수를 선택해 주세요.", "엑셀 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim initialData = ListBox1.Items.Cast(Of Object)().
        Select(Function(x)
                   Dim d As Double
                   If Double.TryParse(x.ToString(), d) Then Return d Else Return Double.NaN
               End Function).
        Where(Function(d) Not Double.IsNaN(d)).
        ToArray()

            Dim n = initialData.Length
            If n = 0 Then
                MessageBox.Show("내보낼 데이터가 없습니다.", "엑셀 내보내기", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            If Not ValidateSmoothingParameters(n, radius, borderCount, True) Then
                Return
            End If

            ' Middle Median 계산
            Dim middleMedian(n - 1) As Double
            sourceList = initialData.ToList()
            Dim middleProgress = New Progress(Of Integer)(Sub(v) progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(v, progressBar1.Maximum)))
            Await Task.Run(Sub()
                               ComputeMedians(True, kernelWidth, borderCount, middleProgress)
                               medianList.CopyTo(0, middleMedian, 0, n)
                           End Sub)

            If Not ValidateSmoothingParameters(n, radius, borderCount, False) Then
                Return
            End If

            ' All Median 계산
            Dim allMedian(n - 1) As Double
            sourceList = initialData.ToList()
            Dim allProgress = New Progress(Of Integer)(Sub(v) progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(v, progressBar1.Maximum)))
            Await Task.Run(Sub()
                               ComputeMedians(False, kernelWidth, borderCount, allProgress)
                               medianList.CopyTo(0, allMedian, 0, n)
                           End Sub)

            Dim excel As Excel.Application = Nothing
            Dim wb As Excel.Workbook = Nothing
            Dim ws As Excel.Worksheet = Nothing

            Dim EXCEL_MAX_ROW = 1048576
            Dim DATA_START_ROW = 4

            Try
                excel = New Excel.Application()
                'excel.Visible = True
                wb = excel.Workbooks.Add()
                ws = CType(wb.Worksheets(1), Excel.Worksheet)

                ws.Cells(1, 1) = txtDatasetTitle.Text
                ws.Cells(3, 1) = "보정 설정"
                ws.Cells(4, 1) = $"측정 오차 제거 필터 반경 :  {kernelWidth}"
                ws.Cells(5, 1) = $"경계 유지 계수 : {borderCount}"

                ' 데이터를 분산 저장하는 함수
                Dim WriteDistributed =
            Function(data As Double(), startCol As Integer, title As String) As List(Of Tuple(Of Integer, Integer, Integer))
                Dim ranges As New List(Of Tuple(Of Integer, Integer, Integer))
                Dim idx = 0
                Dim col = startCol
                Dim firstCol = col
                While idx < data.Length
                    Dim count = Math.Min(EXCEL_MAX_ROW - DATA_START_ROW + 1, data.Length - idx)
                    Dim arr2D(count - 1, 0) As Object
                    For r = 0 To count - 1
                        arr2D(r, 0) = data(idx)
                        idx += 1
                    Next
                    Dim startRow = DATA_START_ROW
                    Dim endRow = startRow + count - 1
                    If col = firstCol Then ws.Cells(3, col) = title
                    ws.Range(ws.Cells(startRow, col), ws.Cells(endRow, col)).Value2 = arr2D
                    ranges.Add(Tuple.Create(col, startRow, endRow))
                    col += 1
                End While
                Return ranges
            End Function

                ' 각 Median 결과를 엑셀에 분산 저장
                Dim initialRanges = WriteDistributed(initialData, 3, "초기 데이터")
                progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(30, progressBar1.Maximum))
                Dim middleRanges = WriteDistributed(middleMedian, initialRanges.Last.Item1 + 2, "중앙 구간 이동 중간 값")
                progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(60, progressBar1.Maximum))
                Dim allRanges = WriteDistributed(allMedian, middleRanges.Last.Item1 + 2, "전체 구간 이동 중간 값")
                progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(80, progressBar1.Maximum))

                ' 차트 생성 (기존 로직 유지)
                Dim lastCol = Math.Max(Math.Max(initialRanges.Last.Item1, middleRanges.Last.Item1), allRanges.Last.Item1)
                Dim chartBaseCol = lastCol + 2
                Dim chartBaseRow = DATA_START_ROW

                Dim chartObjects = CType(ws.ChartObjects(), Excel.ChartObjects)
                Dim chartLeft = ws.Cells(chartBaseRow, chartBaseCol).Left
                Dim chartTop = ws.Cells(chartBaseRow, chartBaseCol).Top
                Dim chartWidth = 900
                Dim chartHeight = 600
                Dim chartObj = chartObjects.Add(chartLeft, chartTop, chartWidth, chartHeight)
                Dim chart = chartObj.Chart

                chart.ChartType = Microsoft.Office.Interop.Excel.XlChartType.xlLine
                chart.HasTitle = True
                ' chart.ChartTitle.Text = "Symphony of Boundaries And Flow : Avocado Smoothie 's All-Median & Middle-Median"
                chart.ChartTitle.Text = txtDatasetTitle.Text
                chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue).HasTitle = True
                chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlValue).AxisTitle.Text = "값"
                chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory).HasTitle = True
                chart.Axes(Microsoft.Office.Interop.Excel.XlAxisType.xlCategory).AxisTitle.Text = "순번"

                Dim seriesCollection = chart.SeriesCollection()

                Dim GetExcelColumnName As Func(Of Integer, String) =
                    Function(columnNumber As Integer) As String
                        Dim colName As String = ""
                        While columnNumber > 0
                            Dim modulo = (columnNumber - 1) Mod 26
                            colName = Chr(65 + modulo) & colName
                            columnNumber = (columnNumber - modulo) \ 26
                        End While
                        Return colName
                    End Function

                Dim AddSeries = Sub(ranges As List(Of Tuple(Of Integer, Integer, Integer)), name As String)
                                    Dim multiRange As Excel.Range = Nothing
                                    Dim totalCount As Integer = 0
                                    For Each rng In ranges
                                        Dim col = rng.Item1
                                        Dim startRow = rng.Item2
                                        Dim endRow = rng.Item3
                                        Dim colLetter = GetExcelColumnName(col)
                                        Dim singleRange = ws.Range($"{colLetter}{startRow}:{colLetter}{endRow}")
                                        If multiRange Is Nothing Then
                                            multiRange = singleRange
                                        Else
                                            multiRange = excel.Union(multiRange, singleRange)
                                        End If
                                        totalCount += (endRow - startRow + 1)
                                    Next
                                    Dim series As Excel.Series = CType(seriesCollection.Add(Source:=multiRange, RowCol:=Microsoft.Office.Interop.Excel.XlRowCol.xlColumns), Excel.Series)
                                    series.Name = name
                                End Sub

                AddSeries(initialRanges, "초기 데이터")
                AddSeries(middleRanges, "중앙 구간 이동 중간 값")
                AddSeries(allRanges, "전체 구간 이동 중간 값")

                progressBar1.Value = Math.Max(progressBar1.Minimum, Math.Min(100, progressBar1.Maximum))
                Await Task.Delay(200)
                progressBar1.Value = 0
                excel.Visible = True
            Catch ex As Exception
                MessageBox.Show("엑셀 내보내기 도중 실패했습니다 : " & ex.Message, "내보내기 오류", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If ws IsNot Nothing Then Marshal.ReleaseComObject(ws)
                If wb IsNot Nothing Then Marshal.ReleaseComObject(wb)
                If excel IsNot Nothing Then Marshal.ReleaseComObject(excel)
            End Try
        End If
    End Sub

    Private Sub btnInfo_Click(sender As Object, e As EventArgs) Handles btnInfo.Click
        AboutBox.ShowDialog()
    End Sub

    Private Sub txtExcelTitle_Enter(sender As Object, e As EventArgs) Handles txtDatasetTitle.Enter
        If txtDatasetTitle.Text = ExcelTitlePlaceholder Then
            txtDatasetTitle.Text = ""
            txtDatasetTitle.ForeColor = Color.Black
        End If
        txtDatasetTitle.TextAlign = HorizontalAlignment.Left
    End Sub

    Private Sub txtExcelTitle_Leave(sender As Object, e As EventArgs) Handles txtDatasetTitle.Leave
        If String.IsNullOrWhiteSpace(txtDatasetTitle.Text) Then
            txtDatasetTitle.Text = ExcelTitlePlaceholder
            txtDatasetTitle.ForeColor = Color.Gray
            txtDatasetTitle.TextAlign = HorizontalAlignment.Center
        End If
    End Sub

    Private Sub txtExcelTitle_TextChanged(sender As Object, e As EventArgs) Handles txtDatasetTitle.TextChanged
        UpdateListBox1ButtonsState(Nothing, EventArgs.Empty)

        If txtDatasetTitle.Text = ExcelTitlePlaceholder Then
            txtDatasetTitle.TextAlign = HorizontalAlignment.Center
        Else
            txtDatasetTitle.TextAlign = HorizontalAlignment.Left
        End If
    End Sub

    Private Sub syncButton1_Click(sender As Object, e As EventArgs) Handles syncButton1.Click
        If ListBox1.Items.Count <> ListBox2.Items.Count Then Return
        If ListBox1.SelectedIndices.Count = 0 Then Return

        ListBox2.BeginUpdate()
        Try
            ListBox2.ClearSelected()
            Dim indices = New Integer(ListBox1.SelectedIndices.Count - 1) {}
            ListBox1.SelectedIndices.CopyTo(indices, 0)
            For i As Integer = 0 To indices.Length - 1
                ListBox2.SetSelected(indices(i), True)
            Next

            If ListBox1.TopIndex >= 0 Then
                ListBox2.TopIndex = ListBox1.TopIndex
            End If
        Finally
            ListBox2.EndUpdate()
        End Try
    End Sub

    Private Sub syncButton2_Click(sender As Object, e As EventArgs) Handles syncButton2.Click
        If ListBox2.Items.Count <> ListBox1.Items.Count Then Return
        If ListBox2.SelectedIndices.Count = 0 Then Return

        ListBox1.BeginUpdate()
        Try
            ListBox1.ClearSelected()
            Dim indices = New Integer(ListBox2.SelectedIndices.Count - 1) {}
            ListBox2.SelectedIndices.CopyTo(indices, 0)
            For i As Integer = 0 To indices.Length - 1
                ListBox1.SetSelected(indices(i), True)
            Next
            If ListBox2.TopIndex >= 0 Then
                ListBox1.TopIndex = ListBox2.TopIndex
            End If
        Finally
            ListBox1.EndUpdate()
        End Try
    End Sub

End Class
