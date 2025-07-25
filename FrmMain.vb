Imports System.Text.RegularExpressions
Imports System.Threading

Public Class FrmMain

    Dim dpivalue As Double
    Dim borderCount As Integer

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

        AddHandler ListBox1.SelectedIndexChanged, Sub(s, e)
                                                      Dim itemsCount As Boolean = ListBox1.Items.Count > 0
                                                      Dim hasSelection As Boolean = ListBox1.SelectedIndex >= 0
                                                      editButton.Enabled = hasSelection
                                                      deleteButton1.Enabled = hasSelection
                                                      sClrButton1.Enabled = hasSelection
                                                  End Sub

        AddHandler ListBox2.SelectedIndexChanged, Sub(s, e)
                                                      Dim itemsCount As Boolean = ListBox2.Items.Count > 0
                                                      Dim hasSelection As Boolean = ListBox2.SelectedIndex >= 0
                                                      sClrButton2.Enabled = hasSelection
                                                  End Sub
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
            lblCnt1.Text = $"Count : {ListBox1.Items.Count}"
            copyButton1.Enabled = ListBox1.Items.Count > 0
        End If

        TextBox1.Clear()
    End Sub

    Sub FillTestData()
        Dim r As New Random
        Dim rCount As Integer = 0
        Do Until rCount = 5000
            sourceList.Add(r.Next(0, 250))
            rCount += 1
        Loop

        ListBox1.BeginUpdate()
        For Each value As Double In sourceList
            ListBox1.Items.Add(value)
        Next
        ListBox1.EndUpdate()
    End Sub

    Private Function GetWindowMedian(
            arr As Double(),
            startIdx As Integer,
            endIdx As Integer
        ) As Double

        Dim length = endIdx - startIdx + 1
        Dim temp(length - 1) As Double
        Array.Copy(arr, startIdx, temp, 0, length)

        ' insertion sort (최대 5개)
        For i As Integer = 1 To length - 1
            Dim key = temp(i)
            Dim j = i - 1
            While j >= 0 AndAlso temp(j) > key
                temp(j + 1) = temp(j)
                j -= 1
            End While
            temp(j + 1) = key
        Next

        Return temp(length >> 1)
    End Function


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
                    $"'{strValue}'을(를) 숫자로 변환할 수 없습니다.",
                    "RunningMedian",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                )
                Return
            End If
        Next

        sourceList = parsedList
        Dim total = sourceList.Count
        Dim useMiddle = RadioButton1.Checked

        Dim kernelWidth As Integer
        If Not Integer.TryParse(cbxKernelWidth.Text, kernelWidth) Then
            MessageBox.Show(
                "Please select a kernel width.",
                "RunningMedian",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
            Return
        End If

        Dim borderCount As Integer
        If Not Integer.TryParse(cbxBorderCount.Text, borderCount) Then
            MessageBox.Show(
                "Please select a border count.",
                "RunningMedian",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
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

        lblCnt1.Text = $"Count : {total}"
        lblCnt2.Text = $"Count : {medianList.Count}"
        slblCalibratedType.Text = If(useMiddle, "Middle Median", "All Median")
        slblKernelWidth.Text = $"{kernelWidth}"
        slblBorderCount.Text = $"{borderCount}"

        slblBorderCount.Visible = useMiddle
        tlblBorderCount.Visible = useMiddle
        slblSeparator2.Visible = useMiddle

        copyButton2.Enabled = True

        Await Task.Delay(200)
        progressBar1.Value = 0
    End Sub



    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim inputText = TextBox1.Text
            Dim v As Double

            If Double.TryParse(inputText, v) Then
                ListBox1.Items.Add(v)
                lblCnt1.Text = $"Count : {ListBox1.Items.Count}"
                copyButton1.Enabled = ListBox1.Items.Count > 0
            End If

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
                MessageBox.Show($"'{txt}' 값을 숫자로 변환할 수 없습니다.",
                            "Copy Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
        Next

        If doubles.Any() Then
            Clipboard.SetText(String.Join(Environment.NewLine, doubles))
        End If
    End Sub

    Private Sub copyButton2_Click(sender As Object, e As EventArgs) Handles copyButton2.Click
        If medianList.Count > 0 Then
            Clipboard.SetText(String.Join(Environment.NewLine, medianList))
        End If
    End Sub


    Private Sub clearButton1_Click(sender As Object, e As EventArgs) Handles clearButton1.Click
        ListBox1.Items.Clear()
        copyButton1.Enabled = False

        lblCnt1.Text = "Count : " & ListBox1.Items.Count
        ListBox1.Select()
    End Sub

    Private Sub clearButton2_Click(sender As Object, e As EventArgs) Handles clearButton2.Click
        ListBox2.Items.Clear()
        copyButton2.Enabled = False

        lblCnt2.Text = "Count : " & ListBox2.Items.Count
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
                Application.DoEvents()
            End If
        Next

        lb.EndUpdate()
        lb.ResumeLayout()

        lb.SelectedIndices.Clear()
        For Each idx In newSelections
            If idx < lb.Items.Count Then lb.SelectedIndices.Add(idx)
        Next

        lblCount.Text = $"Count : {lb.Items.Count}"
        Await Task.Delay(200)
        progressBar.Value = 0
    End Function

    Private Async Sub deleteButton1_Click(sender As Object, e As EventArgs) Handles deleteButton1.Click
        If ListBox1.SelectedIndices.Count = ListBox1.Items.Count Then
            ListBox1.Items.Clear()
            copyButton1.Enabled = False
            lblCnt1.Text = "Count : 0"
            ListBox1.Select()
            progressBar1.Value = 0
            Return
        End If

        Await DeleteSelectedItemsPreserveSelection(ListBox1, progressBar1, lblCnt1)
        lblCnt1.Text = "Count : " & ListBox1.Items.Count
        ListBox1.Select()
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
            lblCnt1.Text = "Count : " & ListBox1.Items.Count
            Await Task.Delay(200)
        Finally
            Dim hasItems As Boolean = ListBox1.Items.Count > 0
            copyButton1.Enabled = hasItems
            deleteButton1.Enabled = hasItems

            progressBar1.Value = 0
            calcButton.Enabled = True
        End Try
    End Sub

    Private Async Function AddItemsInBatches(
    box As ListBox,
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
            lblCnt1.Text = $"Count : {ListBox1.Items.Count}"
            Await Task.Delay(200)
        Finally
            If ListBox1.Items.Count > 0 Then
                copyButton1.Enabled = True
                deleteButton1.Enabled = True
            Else
                copyButton1.Enabled = False
                deleteButton1.Enabled = False
            End If

            progressBar1.Value = 0
            calcButton.Enabled = True
        End Try
    End Sub


    Private Async Sub selectAll1_Click(sender As Object, e As EventArgs) Handles selectAll1.Click
        Await SelectAllWithProgress(ListBox1, progressBar1)
    End Sub

    Private Async Sub selectAll2_Click(sender As Object, e As EventArgs) Handles selectAll2.Click
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
                Application.DoEvents()
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
        If ListBox1.SelectedItems.Count = 0 Then
            editButton.Enabled = False
        Else
            editButton.Enabled = True
        End If
    End Sub

    Private Sub ListBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox1.KeyDown
        If e.KeyData = Keys.Delete Then
            deleteButton1.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.Delete Then
            clearButton1.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.C Then
            copyButton1.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.V Then
            pasteButton.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.A Then
            selectAll1.PerformClick()
        End If

        If (e.KeyData = Keys.F2) Then
            If ListBox1.SelectedIndex >= 0 Then
                FrmModify.ShowDialog(Me)
            End If
        End If

        If e.KeyData = Keys.Escape Then
            sClrButton1.PerformClick()
        End If

        lblCnt1.Text = "Count : " & ListBox1.Items.Count
    End Sub


    Private Sub ListBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListBox2.KeyDown

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.C Then
            e.Handled = True
            copyButton2.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.A Then
            e.Handled = True
            selectAll2.PerformClick()
        End If

        If (e.Modifiers And Keys.Control) = Keys.Control AndAlso e.KeyCode = Keys.Delete Then
            clearButton2.PerformClick()
        End If

        If e.KeyData = Keys.Escape Then
            sClrButton2.PerformClick()
        End If

        lblCnt2.Text = "Count : " & ListBox2.Items.Count
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

        ' 선택 인덱스를 복사 (변경 시 오류 방지)
        Dim selectedIndices = lb.SelectedIndices.Cast(Of Integer).ToArray()
        Dim updateInterval As Integer = Math.Max(1, count \ 100)

        For i As Integer = 0 To selectedIndices.Length - 1
            lb.SetSelected(selectedIndices(i), False)

            If ((i + 1) Mod updateInterval = 0) OrElse i = count - 1 Then
                progressBar.Value = i + 1
                Application.DoEvents()
            End If
        Next

        lb.EndUpdate()
        lb.ResumeLayout()
        lb.Select()

        lblCount.Text = "Count : " & lb.Items.Count

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

        RadioButton2.Checked = True
        lblBorderCount.Enabled = False
        cbxBorderCount.Enabled = False

        AddHandler ListBox1.SelectedIndexChanged, AddressOf UpdateListBox1Buttons
        AddHandler ListBox2.SelectedIndexChanged, AddressOf UpdateListBox2Buttons
    End Sub

    Private Sub UpdateListBox1Buttons(s As Object, e As EventArgs)
        copyButton1.Enabled = (ListBox1.Items.Count > 0)
        editButton.Enabled = (ListBox1.SelectedIndex >= 0)
        deleteButton1.Enabled = (ListBox1.SelectedIndex >= 0)
        sClrButton1.Enabled = (ListBox1.SelectedIndex >= 0)
    End Sub

    Private Sub UpdateListBox2Buttons(s As Object, e As EventArgs)
        copyButton2.Enabled = (ListBox2.Items.Count > 0)
        sClrButton2.Enabled = (ListBox2.SelectedIndex >= 0)
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
End Class
