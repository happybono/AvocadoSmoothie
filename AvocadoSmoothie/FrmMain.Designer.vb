<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.lbInitData = New System.Windows.Forms.ListBox()
        Me.lbRefinedData = New System.Windows.Forms.ListBox()
        Me.btnCalibrate = New System.Windows.Forms.Button()
        Me.rbtnMidMedian = New System.Windows.Forms.RadioButton()
        Me.rbtnAllMedian = New System.Windows.Forms.RadioButton()
        Me.txtInitAdd = New System.Windows.Forms.TextBox()
        Me.btnInitAdd = New System.Windows.Forms.Button()
        Me.btnInitCopy = New System.Windows.Forms.Button()
        Me.cbxBorderCount = New System.Windows.Forms.ComboBox()
        Me.btnInitClear = New System.Windows.Forms.Button()
        Me.btnRefClear = New System.Windows.Forms.Button()
        Me.btnInitDelete = New System.Windows.Forms.Button()
        Me.btnInitPaste = New System.Windows.Forms.Button()
        Me.btnRefCopy = New System.Windows.Forms.Button()
        Me.btnInitSelectAll = New System.Windows.Forms.Button()
        Me.btnRefSelectAll = New System.Windows.Forms.Button()
        Me.gbInitData = New System.Windows.Forms.GroupBox()
        Me.btnInitSelectSync = New System.Windows.Forms.Button()
        Me.btnInitEdit = New System.Windows.Forms.Button()
        Me.lblInitCnt = New System.Windows.Forms.Label()
        Me.btnInitSelectClr = New System.Windows.Forms.Button()
        Me.gbRefinedData = New System.Windows.Forms.GroupBox()
        Me.btnRefSelectSync = New System.Windows.Forms.Button()
        Me.lblRefCnt = New System.Windows.Forms.Label()
        Me.btnRefSelectClr = New System.Windows.Forms.Button()
        Me.pbMain = New System.Windows.Forms.ProgressBar()
        Me.sstripMain = New System.Windows.Forms.StatusStrip()
        Me.slblDesc = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tlblCalibratedType = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblCalibratedType = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblSeparator1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tlblKernelRadius = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblKernelRadius = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblSeparator2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tlblBoundaryMethod = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblBoundaryMethod = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblSeparator3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tlblBorderCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.slblBorderCount = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gbSmoothParams = New System.Windows.Forms.GroupBox()
        Me.cbxBoundaryMethod = New System.Windows.Forms.ComboBox()
        Me.lblBoundaryMethod = New System.Windows.Forms.Label()
        Me.lblBorderCount = New System.Windows.Forms.Label()
        Me.cbxKernelRadius = New System.Windows.Forms.ComboBox()
        Me.lblKernelRadius = New System.Windows.Forms.Label()
        Me.gbSmoothMtd = New System.Windows.Forms.GroupBox()
        Me.ttipMain = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnExport = New System.Windows.Forms.Button()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.txtDatasetTitle = New System.Windows.Forms.TextBox()
        Me.gbExportOpts = New System.Windows.Forms.GroupBox()
        Me.rbtnCSV = New System.Windows.Forms.RadioButton()
        Me.rbtnXLSX = New System.Windows.Forms.RadioButton()
        Me.gbInitData.SuspendLayout()
        Me.gbRefinedData.SuspendLayout()
        Me.sstripMain.SuspendLayout()
        Me.gbSmoothParams.SuspendLayout()
        Me.gbSmoothMtd.SuspendLayout()
        Me.gbExportOpts.SuspendLayout()
        Me.SuspendLayout()
        '
        'lbInitData
        '
        Me.lbInitData.AllowDrop = True
        Me.lbInitData.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.lbInitData.FormattingEnabled = True
        Me.lbInitData.ItemHeight = 17
        Me.lbInitData.Location = New System.Drawing.Point(7, 31)
        Me.lbInitData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbInitData.Name = "lbInitData"
        Me.lbInitData.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbInitData.Size = New System.Drawing.Size(294, 514)
        Me.lbInitData.TabIndex = 4
        '
        'lbRefinedData
        '
        Me.lbRefinedData.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.lbRefinedData.FormattingEnabled = True
        Me.lbRefinedData.ItemHeight = 17
        Me.lbRefinedData.Location = New System.Drawing.Point(7, 31)
        Me.lbRefinedData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.lbRefinedData.Name = "lbRefinedData"
        Me.lbRefinedData.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lbRefinedData.Size = New System.Drawing.Size(294, 514)
        Me.lbRefinedData.TabIndex = 21
        '
        'btnCalibrate
        '
        Me.btnCalibrate.Font = New System.Drawing.Font("Segoe Fluent Icons", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCalibrate.Location = New System.Drawing.Point(14, 807)
        Me.btnCalibrate.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnCalibrate.Name = "btnCalibrate"
        Me.btnCalibrate.Size = New System.Drawing.Size(466, 30)
        Me.btnCalibrate.TabIndex = 19
        Me.btnCalibrate.Text = ""
        Me.ttipMain.SetToolTip(Me.btnCalibrate, "Calibrate")
        Me.btnCalibrate.UseVisualStyleBackColor = True
        '
        'rbtnMidMedian
        '
        Me.rbtnMidMedian.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnMidMedian.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.rbtnMidMedian.Location = New System.Drawing.Point(175, 31)
        Me.rbtnMidMedian.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbtnMidMedian.Name = "rbtnMidMedian"
        Me.rbtnMidMedian.Size = New System.Drawing.Size(150, 30)
        Me.rbtnMidMedian.TabIndex = 15
        Me.rbtnMidMedian.Text = "Middle Median"
        Me.rbtnMidMedian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnMidMedian.UseVisualStyleBackColor = True
        '
        'rbtnAllMedian
        '
        Me.rbtnAllMedian.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnAllMedian.Checked = True
        Me.rbtnAllMedian.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.rbtnAllMedian.Location = New System.Drawing.Point(19, 31)
        Me.rbtnAllMedian.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbtnAllMedian.Name = "rbtnAllMedian"
        Me.rbtnAllMedian.Size = New System.Drawing.Size(150, 30)
        Me.rbtnAllMedian.TabIndex = 14
        Me.rbtnAllMedian.TabStop = True
        Me.rbtnAllMedian.Text = "All Median"
        Me.rbtnAllMedian.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnAllMedian.UseVisualStyleBackColor = True
        '
        'txtInitAdd
        '
        Me.txtInitAdd.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.txtInitAdd.Location = New System.Drawing.Point(26, 16)
        Me.txtInitAdd.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtInitAdd.Name = "txtInitAdd"
        Me.txtInitAdd.Size = New System.Drawing.Size(262, 25)
        Me.txtInitAdd.TabIndex = 1
        '
        'btnInitAdd
        '
        Me.btnInitAdd.Enabled = False
        Me.btnInitAdd.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitAdd.Location = New System.Drawing.Point(292, 14)
        Me.btnInitAdd.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitAdd.Name = "btnInitAdd"
        Me.btnInitAdd.Size = New System.Drawing.Size(67, 30)
        Me.btnInitAdd.TabIndex = 2
        Me.btnInitAdd.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitAdd, "Add")
        Me.btnInitAdd.UseVisualStyleBackColor = True
        '
        'btnInitCopy
        '
        Me.btnInitCopy.Enabled = False
        Me.btnInitCopy.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitCopy.Location = New System.Drawing.Point(307, 65)
        Me.btnInitCopy.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitCopy.Name = "btnInitCopy"
        Me.btnInitCopy.Size = New System.Drawing.Size(30, 30)
        Me.btnInitCopy.TabIndex = 6
        Me.btnInitCopy.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitCopy, "Copy")
        Me.btnInitCopy.UseVisualStyleBackColor = True
        '
        'cbxBorderCount
        '
        Me.cbxBorderCount.DropDownHeight = 150
        Me.cbxBorderCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxBorderCount.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.cbxBorderCount.FormattingEnabled = True
        Me.cbxBorderCount.IntegralHeight = False
        Me.cbxBorderCount.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13"})
        Me.cbxBorderCount.Location = New System.Drawing.Point(225, 115)
        Me.cbxBorderCount.Name = "cbxBorderCount"
        Me.cbxBorderCount.Size = New System.Drawing.Size(103, 25)
        Me.cbxBorderCount.TabIndex = 18
        '
        'btnInitClear
        '
        Me.btnInitClear.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitClear.Location = New System.Drawing.Point(307, 31)
        Me.btnInitClear.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitClear.Name = "btnInitClear"
        Me.btnInitClear.Size = New System.Drawing.Size(30, 30)
        Me.btnInitClear.TabIndex = 5
        Me.btnInitClear.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitClear, "Clear")
        Me.btnInitClear.UseVisualStyleBackColor = True
        '
        'btnRefClear
        '
        Me.btnRefClear.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnRefClear.Location = New System.Drawing.Point(307, 31)
        Me.btnRefClear.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnRefClear.Name = "btnRefClear"
        Me.btnRefClear.Size = New System.Drawing.Size(30, 30)
        Me.btnRefClear.TabIndex = 22
        Me.btnRefClear.Text = ""
        Me.ttipMain.SetToolTip(Me.btnRefClear, "Clear")
        Me.btnRefClear.UseVisualStyleBackColor = True
        '
        'btnInitDelete
        '
        Me.btnInitDelete.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitDelete.Location = New System.Drawing.Point(307, 167)
        Me.btnInitDelete.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitDelete.Name = "btnInitDelete"
        Me.btnInitDelete.Size = New System.Drawing.Size(30, 30)
        Me.btnInitDelete.TabIndex = 9
        Me.btnInitDelete.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitDelete, "Delete")
        Me.btnInitDelete.UseVisualStyleBackColor = True
        '
        'btnInitPaste
        '
        Me.btnInitPaste.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitPaste.Location = New System.Drawing.Point(307, 99)
        Me.btnInitPaste.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitPaste.Name = "btnInitPaste"
        Me.btnInitPaste.Size = New System.Drawing.Size(30, 30)
        Me.btnInitPaste.TabIndex = 7
        Me.btnInitPaste.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitPaste, "Paste")
        Me.btnInitPaste.UseVisualStyleBackColor = True
        '
        'btnRefCopy
        '
        Me.btnRefCopy.Enabled = False
        Me.btnRefCopy.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnRefCopy.Location = New System.Drawing.Point(307, 65)
        Me.btnRefCopy.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnRefCopy.Name = "btnRefCopy"
        Me.btnRefCopy.Size = New System.Drawing.Size(30, 30)
        Me.btnRefCopy.TabIndex = 23
        Me.btnRefCopy.Text = ""
        Me.ttipMain.SetToolTip(Me.btnRefCopy, "Copy")
        Me.btnRefCopy.UseVisualStyleBackColor = True
        '
        'btnInitSelectAll
        '
        Me.btnInitSelectAll.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitSelectAll.Location = New System.Drawing.Point(307, 201)
        Me.btnInitSelectAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitSelectAll.Name = "btnInitSelectAll"
        Me.btnInitSelectAll.Size = New System.Drawing.Size(30, 30)
        Me.btnInitSelectAll.TabIndex = 10
        Me.btnInitSelectAll.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitSelectAll, "Select All")
        Me.btnInitSelectAll.UseVisualStyleBackColor = True
        '
        'btnRefSelectAll
        '
        Me.btnRefSelectAll.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnRefSelectAll.Location = New System.Drawing.Point(307, 99)
        Me.btnRefSelectAll.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnRefSelectAll.Name = "btnRefSelectAll"
        Me.btnRefSelectAll.Size = New System.Drawing.Size(30, 30)
        Me.btnRefSelectAll.TabIndex = 24
        Me.btnRefSelectAll.Text = ""
        Me.ttipMain.SetToolTip(Me.btnRefSelectAll, "Select All")
        Me.btnRefSelectAll.UseVisualStyleBackColor = True
        '
        'gbInitData
        '
        Me.gbInitData.Controls.Add(Me.btnInitSelectSync)
        Me.gbInitData.Controls.Add(Me.btnInitEdit)
        Me.gbInitData.Controls.Add(Me.lblInitCnt)
        Me.gbInitData.Controls.Add(Me.btnInitSelectClr)
        Me.gbInitData.Controls.Add(Me.btnInitSelectAll)
        Me.gbInitData.Controls.Add(Me.btnInitPaste)
        Me.gbInitData.Controls.Add(Me.btnInitDelete)
        Me.gbInitData.Controls.Add(Me.btnInitClear)
        Me.gbInitData.Controls.Add(Me.btnInitCopy)
        Me.gbInitData.Controls.Add(Me.lbInitData)
        Me.gbInitData.Font = New System.Drawing.Font("Segoe UI Variable Display Semil", 11.25!)
        Me.gbInitData.Location = New System.Drawing.Point(15, 52)
        Me.gbInitData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbInitData.Name = "gbInitData"
        Me.gbInitData.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbInitData.Size = New System.Drawing.Size(344, 586)
        Me.gbInitData.TabIndex = 3
        Me.gbInitData.TabStop = False
        Me.gbInitData.Text = "Initial Dataset"
        '
        'btnInitSelectSync
        '
        Me.btnInitSelectSync.Font = New System.Drawing.Font("Segoe Fluent Icons", 11.25!)
        Me.btnInitSelectSync.Location = New System.Drawing.Point(307, 269)
        Me.btnInitSelectSync.Name = "btnInitSelectSync"
        Me.btnInitSelectSync.Size = New System.Drawing.Size(30, 30)
        Me.btnInitSelectSync.TabIndex = 12
        Me.btnInitSelectSync.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitSelectSync, "Match Selection" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "( ▶ Refined Dataset )")
        Me.btnInitSelectSync.UseVisualStyleBackColor = True
        '
        'btnInitEdit
        '
        Me.btnInitEdit.Enabled = False
        Me.btnInitEdit.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInitEdit.Location = New System.Drawing.Point(307, 133)
        Me.btnInitEdit.Margin = New System.Windows.Forms.Padding(2)
        Me.btnInitEdit.Name = "btnInitEdit"
        Me.btnInitEdit.Size = New System.Drawing.Size(30, 30)
        Me.btnInitEdit.TabIndex = 8
        Me.btnInitEdit.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitEdit, "Edit")
        Me.btnInitEdit.UseVisualStyleBackColor = True
        '
        'lblInitCnt
        '
        Me.lblInitCnt.AutoSize = True
        Me.lblInitCnt.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.lblInitCnt.Location = New System.Drawing.Point(7, 555)
        Me.lblInitCnt.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblInitCnt.Name = "lblInitCnt"
        Me.lblInitCnt.Size = New System.Drawing.Size(65, 19)
        Me.lblInitCnt.TabIndex = 26
        Me.lblInitCnt.Text = "Count : 0"
        '
        'btnInitSelectClr
        '
        Me.btnInitSelectClr.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnInitSelectClr.Location = New System.Drawing.Point(307, 235)
        Me.btnInitSelectClr.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnInitSelectClr.Name = "btnInitSelectClr"
        Me.btnInitSelectClr.Size = New System.Drawing.Size(30, 30)
        Me.btnInitSelectClr.TabIndex = 11
        Me.btnInitSelectClr.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInitSelectClr, "Deselect All")
        Me.btnInitSelectClr.UseVisualStyleBackColor = True
        '
        'gbRefinedData
        '
        Me.gbRefinedData.Controls.Add(Me.btnRefSelectSync)
        Me.gbRefinedData.Controls.Add(Me.lblRefCnt)
        Me.gbRefinedData.Controls.Add(Me.btnRefSelectClr)
        Me.gbRefinedData.Controls.Add(Me.btnRefSelectAll)
        Me.gbRefinedData.Controls.Add(Me.btnRefCopy)
        Me.gbRefinedData.Controls.Add(Me.btnRefClear)
        Me.gbRefinedData.Controls.Add(Me.lbRefinedData)
        Me.gbRefinedData.Font = New System.Drawing.Font("Segoe UI Variable Display Semil", 11.25!)
        Me.gbRefinedData.Location = New System.Drawing.Point(376, 52)
        Me.gbRefinedData.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbRefinedData.Name = "gbRefinedData"
        Me.gbRefinedData.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.gbRefinedData.Size = New System.Drawing.Size(344, 586)
        Me.gbRefinedData.TabIndex = 20
        Me.gbRefinedData.TabStop = False
        Me.gbRefinedData.Text = "Refined Dataset"
        '
        'btnRefSelectSync
        '
        Me.btnRefSelectSync.Font = New System.Drawing.Font("Segoe Fluent Icons", 11.25!)
        Me.btnRefSelectSync.Location = New System.Drawing.Point(307, 167)
        Me.btnRefSelectSync.Name = "btnRefSelectSync"
        Me.btnRefSelectSync.Size = New System.Drawing.Size(30, 30)
        Me.btnRefSelectSync.TabIndex = 26
        Me.btnRefSelectSync.Text = ""
        Me.ttipMain.SetToolTip(Me.btnRefSelectSync, "Match Selection " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "( ◀ Initial Dataset )")
        Me.btnRefSelectSync.UseVisualStyleBackColor = True
        '
        'lblRefCnt
        '
        Me.lblRefCnt.AutoSize = True
        Me.lblRefCnt.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.lblRefCnt.Location = New System.Drawing.Point(7, 555)
        Me.lblRefCnt.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblRefCnt.Name = "lblRefCnt"
        Me.lblRefCnt.Size = New System.Drawing.Size(65, 19)
        Me.lblRefCnt.TabIndex = 27
        Me.lblRefCnt.Text = "Count : 0"
        '
        'btnRefSelectClr
        '
        Me.btnRefSelectClr.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!)
        Me.btnRefSelectClr.Location = New System.Drawing.Point(307, 133)
        Me.btnRefSelectClr.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.btnRefSelectClr.Name = "btnRefSelectClr"
        Me.btnRefSelectClr.Size = New System.Drawing.Size(30, 30)
        Me.btnRefSelectClr.TabIndex = 25
        Me.btnRefSelectClr.Text = ""
        Me.ttipMain.SetToolTip(Me.btnRefSelectClr, "Deselect All")
        Me.btnRefSelectClr.UseVisualStyleBackColor = True
        '
        'pbMain
        '
        Me.pbMain.Location = New System.Drawing.Point(0, 842)
        Me.pbMain.Name = "pbMain"
        Me.pbMain.Size = New System.Drawing.Size(734, 5)
        Me.pbMain.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.pbMain.TabIndex = 28
        '
        'sstripMain
        '
        Me.sstripMain.AutoSize = False
        Me.sstripMain.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sstripMain.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.sstripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.slblDesc, Me.tlblCalibratedType, Me.slblCalibratedType, Me.slblSeparator1, Me.tlblKernelRadius, Me.slblKernelRadius, Me.slblSeparator2, Me.tlblBoundaryMethod, Me.slblBoundaryMethod, Me.slblSeparator3, Me.tlblBorderCount, Me.slblBorderCount})
        Me.sstripMain.Location = New System.Drawing.Point(0, 847)
        Me.sstripMain.Name = "sstripMain"
        Me.sstripMain.Size = New System.Drawing.Size(734, 24)
        Me.sstripMain.SizingGrip = False
        Me.sstripMain.TabIndex = 29
        Me.sstripMain.Text = "statusStrip1"
        '
        'slblDesc
        '
        Me.slblDesc.AutoSize = False
        Me.slblDesc.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!)
        Me.slblDesc.ForeColor = System.Drawing.Color.White
        Me.slblDesc.Name = "slblDesc"
        Me.slblDesc.Size = New System.Drawing.Size(731, 19)
        Me.slblDesc.Text = "To calibrate, add data to the Initial Dataset, choose a Calibration Method, set S" &
    "moothing Parameters."
        '
        'tlblCalibratedType
        '
        Me.tlblCalibratedType.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlblCalibratedType.ForeColor = System.Drawing.Color.White
        Me.tlblCalibratedType.Name = "tlblCalibratedType"
        Me.tlblCalibratedType.Size = New System.Drawing.Size(114, 19)
        Me.tlblCalibratedType.Text = "Applied Calibration :"
        '
        'slblCalibratedType
        '
        Me.slblCalibratedType.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblCalibratedType.ForeColor = System.Drawing.Color.White
        Me.slblCalibratedType.Name = "slblCalibratedType"
        Me.slblCalibratedType.Size = New System.Drawing.Size(17, 16)
        Me.slblCalibratedType.Text = "--"
        '
        'slblSeparator1
        '
        Me.slblSeparator1.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblSeparator1.ForeColor = System.Drawing.Color.White
        Me.slblSeparator1.Name = "slblSeparator1"
        Me.slblSeparator1.Size = New System.Drawing.Size(10, 16)
        Me.slblSeparator1.Text = " "
        '
        'tlblKernelRadius
        '
        Me.tlblKernelRadius.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlblKernelRadius.ForeColor = System.Drawing.Color.White
        Me.tlblKernelRadius.Name = "tlblKernelRadius"
        Me.tlblKernelRadius.Size = New System.Drawing.Size(173, 16)
        Me.tlblKernelRadius.Text = "Noise Reduction Kernel Radius :"
        '
        'slblKernelRadius
        '
        Me.slblKernelRadius.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblKernelRadius.ForeColor = System.Drawing.Color.White
        Me.slblKernelRadius.Name = "slblKernelRadius"
        Me.slblKernelRadius.Size = New System.Drawing.Size(17, 16)
        Me.slblKernelRadius.Text = "--"
        '
        'slblSeparator2
        '
        Me.slblSeparator2.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblSeparator2.ForeColor = System.Drawing.Color.White
        Me.slblSeparator2.Name = "slblSeparator2"
        Me.slblSeparator2.Size = New System.Drawing.Size(10, 16)
        Me.slblSeparator2.Text = " "
        Me.slblSeparator2.Visible = False
        '
        'tlblBoundaryMethod
        '
        Me.tlblBoundaryMethod.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlblBoundaryMethod.ForeColor = System.Drawing.Color.White
        Me.tlblBoundaryMethod.Name = "tlblBoundaryMethod"
        Me.tlblBoundaryMethod.Size = New System.Drawing.Size(108, 16)
        Me.tlblBoundaryMethod.Text = "Boundary Method :"
        Me.tlblBoundaryMethod.Visible = False
        '
        'slblBoundaryMethod
        '
        Me.slblBoundaryMethod.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblBoundaryMethod.ForeColor = System.Drawing.Color.White
        Me.slblBoundaryMethod.Name = "slblBoundaryMethod"
        Me.slblBoundaryMethod.Size = New System.Drawing.Size(17, 16)
        Me.slblBoundaryMethod.Text = "--"
        Me.slblBoundaryMethod.Visible = False
        '
        'slblSeparator3
        '
        Me.slblSeparator3.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblSeparator3.ForeColor = System.Drawing.Color.White
        Me.slblSeparator3.Name = "slblSeparator3"
        Me.slblSeparator3.Size = New System.Drawing.Size(10, 16)
        Me.slblSeparator3.Text = " "
        Me.slblSeparator3.Visible = False
        '
        'tlblBorderCount
        '
        Me.tlblBorderCount.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tlblBorderCount.ForeColor = System.Drawing.Color.White
        Me.tlblBorderCount.Name = "tlblBorderCount"
        Me.tlblBorderCount.Size = New System.Drawing.Size(83, 16)
        Me.tlblBorderCount.Text = "Border Count :"
        Me.tlblBorderCount.Visible = False
        '
        'slblBorderCount
        '
        Me.slblBorderCount.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblBorderCount.ForeColor = System.Drawing.Color.White
        Me.slblBorderCount.Name = "slblBorderCount"
        Me.slblBorderCount.Size = New System.Drawing.Size(17, 16)
        Me.slblBorderCount.Text = "--"
        Me.slblBorderCount.Visible = False
        '
        'gbSmoothParams
        '
        Me.gbSmoothParams.Controls.Add(Me.cbxBoundaryMethod)
        Me.gbSmoothParams.Controls.Add(Me.lblBoundaryMethod)
        Me.gbSmoothParams.Controls.Add(Me.cbxBorderCount)
        Me.gbSmoothParams.Controls.Add(Me.lblBorderCount)
        Me.gbSmoothParams.Controls.Add(Me.cbxKernelRadius)
        Me.gbSmoothParams.Controls.Add(Me.lblKernelRadius)
        Me.gbSmoothParams.Font = New System.Drawing.Font("Segoe UI Variable Display Semil", 11.25!)
        Me.gbSmoothParams.Location = New System.Drawing.Point(376, 644)
        Me.gbSmoothParams.Name = "gbSmoothParams"
        Me.gbSmoothParams.Size = New System.Drawing.Size(344, 157)
        Me.gbSmoothParams.TabIndex = 16
        Me.gbSmoothParams.TabStop = False
        Me.gbSmoothParams.Text = "Signal Smoothing Parameters"
        '
        'cbxBoundaryMethod
        '
        Me.cbxBoundaryMethod.DropDownHeight = 150
        Me.cbxBoundaryMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxBoundaryMethod.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.cbxBoundaryMethod.FormattingEnabled = True
        Me.cbxBoundaryMethod.IntegralHeight = False
        Me.cbxBoundaryMethod.ItemHeight = 17
        Me.cbxBoundaryMethod.Items.AddRange(New Object() {"Symmetric", "Adaptive", "Replicate", "Zero Padding"})
        Me.cbxBoundaryMethod.Location = New System.Drawing.Point(225, 76)
        Me.cbxBoundaryMethod.Name = "cbxBoundaryMethod"
        Me.cbxBoundaryMethod.Size = New System.Drawing.Size(103, 25)
        Me.cbxBoundaryMethod.TabIndex = 25
        '
        'lblBoundaryMethod
        '
        Me.lblBoundaryMethod.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.lblBoundaryMethod.Location = New System.Drawing.Point(25, 79)
        Me.lblBoundaryMethod.Name = "lblBoundaryMethod"
        Me.lblBoundaryMethod.Size = New System.Drawing.Size(183, 19)
        Me.lblBoundaryMethod.TabIndex = 24
        Me.lblBoundaryMethod.Text = "Boundary Handling Method :"
        '
        'lblBorderCount
        '
        Me.lblBorderCount.AutoSize = True
        Me.lblBorderCount.Enabled = False
        Me.lblBorderCount.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.lblBorderCount.Location = New System.Drawing.Point(76, 118)
        Me.lblBorderCount.Name = "lblBorderCount"
        Me.lblBorderCount.Size = New System.Drawing.Size(98, 19)
        Me.lblBorderCount.TabIndex = 20
        Me.lblBorderCount.Text = "Border Count :"
        '
        'cbxKernelRadius
        '
        Me.cbxKernelRadius.DropDownHeight = 150
        Me.cbxKernelRadius.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxKernelRadius.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbxKernelRadius.FormattingEnabled = True
        Me.cbxKernelRadius.IntegralHeight = False
        Me.cbxKernelRadius.ItemHeight = 17
        Me.cbxKernelRadius.Items.AddRange(New Object() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
        Me.cbxKernelRadius.Location = New System.Drawing.Point(225, 37)
        Me.cbxKernelRadius.Margin = New System.Windows.Forms.Padding(2)
        Me.cbxKernelRadius.Name = "cbxKernelRadius"
        Me.cbxKernelRadius.Size = New System.Drawing.Size(103, 25)
        Me.cbxKernelRadius.TabIndex = 17
        '
        'lblKernelRadius
        '
        Me.lblKernelRadius.AutoSize = True
        Me.lblKernelRadius.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblKernelRadius.Location = New System.Drawing.Point(17, 40)
        Me.lblKernelRadius.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblKernelRadius.Name = "lblKernelRadius"
        Me.lblKernelRadius.Size = New System.Drawing.Size(201, 19)
        Me.lblKernelRadius.TabIndex = 17
        Me.lblKernelRadius.Text = "Noise Reduction Kernel Radius : "
        '
        'gbSmoothMtd
        '
        Me.gbSmoothMtd.Controls.Add(Me.rbtnMidMedian)
        Me.gbSmoothMtd.Controls.Add(Me.rbtnAllMedian)
        Me.gbSmoothMtd.Font = New System.Drawing.Font("Segoe UI Variable Display Semil", 11.25!)
        Me.gbSmoothMtd.Location = New System.Drawing.Point(15, 644)
        Me.gbSmoothMtd.Name = "gbSmoothMtd"
        Me.gbSmoothMtd.Size = New System.Drawing.Size(344, 77)
        Me.gbSmoothMtd.TabIndex = 13
        Me.gbSmoothMtd.TabStop = False
        Me.gbSmoothMtd.Text = "Smoothing Methods"
        '
        'btnExport
        '
        Me.btnExport.Font = New System.Drawing.Font("Segoe Fluent Icons", 14.75!)
        Me.btnExport.Location = New System.Drawing.Point(486, 807)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(234, 30)
        Me.btnExport.TabIndex = 31
        Me.btnExport.Tag = ""
        Me.btnExport.Text = ""
        Me.ttipMain.SetToolTip(Me.btnExport, "Export")
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnInfo
        '
        Me.btnInfo.Font = New System.Drawing.Font("Segoe Fluent Icons", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInfo.ForeColor = System.Drawing.Color.DarkOliveGreen
        Me.btnInfo.Location = New System.Drawing.Point(689, 14)
        Me.btnInfo.Margin = New System.Windows.Forms.Padding(2)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(30, 30)
        Me.btnInfo.TabIndex = 32
        Me.btnInfo.Text = ""
        Me.ttipMain.SetToolTip(Me.btnInfo, "About")
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'txtDatasetTitle
        '
        Me.txtDatasetTitle.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 10.125!, System.Drawing.FontStyle.Bold)
        Me.txtDatasetTitle.Location = New System.Drawing.Point(385, 16)
        Me.txtDatasetTitle.Name = "txtDatasetTitle"
        Me.txtDatasetTitle.Size = New System.Drawing.Size(300, 25)
        Me.txtDatasetTitle.TabIndex = 27
        '
        'gbExportOpts
        '
        Me.gbExportOpts.Controls.Add(Me.rbtnCSV)
        Me.gbExportOpts.Controls.Add(Me.rbtnXLSX)
        Me.gbExportOpts.Font = New System.Drawing.Font("Segoe UI Variable Display Semil", 11.25!)
        Me.gbExportOpts.Location = New System.Drawing.Point(15, 724)
        Me.gbExportOpts.Name = "gbExportOpts"
        Me.gbExportOpts.Size = New System.Drawing.Size(344, 77)
        Me.gbExportOpts.TabIndex = 28
        Me.gbExportOpts.TabStop = False
        Me.gbExportOpts.Text = "Data Export Options"
        '
        'rbtnCSV
        '
        Me.rbtnCSV.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnCSV.Checked = True
        Me.rbtnCSV.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.rbtnCSV.Location = New System.Drawing.Point(175, 31)
        Me.rbtnCSV.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbtnCSV.Name = "rbtnCSV"
        Me.rbtnCSV.Size = New System.Drawing.Size(150, 30)
        Me.rbtnCSV.TabIndex = 30
        Me.rbtnCSV.TabStop = True
        Me.rbtnCSV.Text = "Save as CSV"
        Me.rbtnCSV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnCSV.UseVisualStyleBackColor = True
        '
        'rbtnXLSX
        '
        Me.rbtnXLSX.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbtnXLSX.Font = New System.Drawing.Font("Segoe UI Variable Display", 10.125!)
        Me.rbtnXLSX.Location = New System.Drawing.Point(19, 31)
        Me.rbtnXLSX.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.rbtnXLSX.Name = "rbtnXLSX"
        Me.rbtnXLSX.Size = New System.Drawing.Size(150, 30)
        Me.rbtnXLSX.TabIndex = 29
        Me.rbtnXLSX.Text = "Open in Excel"
        Me.rbtnXLSX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbtnXLSX.UseVisualStyleBackColor = True
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(734, 871)
        Me.Controls.Add(Me.gbExportOpts)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.txtDatasetTitle)
        Me.Controls.Add(Me.gbSmoothMtd)
        Me.Controls.Add(Me.gbSmoothParams)
        Me.Controls.Add(Me.pbMain)
        Me.Controls.Add(Me.sstripMain)
        Me.Controls.Add(Me.gbRefinedData)
        Me.Controls.Add(Me.gbInitData)
        Me.Controls.Add(Me.btnInitAdd)
        Me.Controls.Add(Me.txtInitAdd)
        Me.Controls.Add(Me.btnCalibrate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.MaximizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Avocado Smoothie"
        Me.gbInitData.ResumeLayout(False)
        Me.gbInitData.PerformLayout()
        Me.gbRefinedData.ResumeLayout(False)
        Me.gbRefinedData.PerformLayout()
        Me.sstripMain.ResumeLayout(False)
        Me.sstripMain.PerformLayout()
        Me.gbSmoothParams.ResumeLayout(False)
        Me.gbSmoothParams.PerformLayout()
        Me.gbSmoothMtd.ResumeLayout(False)
        Me.gbExportOpts.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbInitData As ListBox
    Friend WithEvents lbRefinedData As ListBox
    Friend WithEvents btnCalibrate As Button
    Friend WithEvents rbtnMidMedian As RadioButton
    Friend WithEvents rbtnAllMedian As RadioButton
    Friend WithEvents txtInitAdd As TextBox
    Friend WithEvents btnInitAdd As Button
    Friend WithEvents btnInitCopy As Button
    Friend WithEvents btnInitClear As Button
    Friend WithEvents btnRefClear As Button
    Friend WithEvents btnInitDelete As Button
    Friend WithEvents btnInitPaste As Button
    Friend WithEvents btnRefCopy As Button
    Friend WithEvents btnInitSelectAll As Button
    Friend WithEvents btnRefSelectAll As Button
    Friend WithEvents gbInitData As GroupBox
    Friend WithEvents gbRefinedData As GroupBox
    Friend WithEvents btnInitSelectClr As Button
    Friend WithEvents btnRefSelectClr As Button
    Private WithEvents pbMain As ProgressBar
    Private WithEvents sstripMain As StatusStrip
    Private WithEvents tlblCalibratedType As ToolStripStatusLabel
    Private WithEvents slblSeparator1 As ToolStripStatusLabel
    Public WithEvents lblInitCnt As Label
    Private WithEvents lblRefCnt As Label
    Friend WithEvents cbxBorderCount As ComboBox
    Private WithEvents gbSmoothParams As GroupBox
    Private WithEvents lblBorderCount As Label
    Private WithEvents cbxKernelRadius As ComboBox
    Private WithEvents lblKernelRadius As Label
    Private WithEvents gbSmoothMtd As GroupBox
    Private WithEvents btnInitEdit As Button
    Private WithEvents slblCalibratedType As ToolStripStatusLabel
    Private WithEvents tlblKernelRadius As ToolStripStatusLabel
    Private WithEvents slblKernelRadius As ToolStripStatusLabel
    Private WithEvents slblSeparator3 As ToolStripStatusLabel
    Private WithEvents tlblBorderCount As ToolStripStatusLabel
    Private WithEvents slblBorderCount As ToolStripStatusLabel
    Friend WithEvents ttipMain As ToolTip
    Friend WithEvents txtDatasetTitle As TextBox
    Friend WithEvents btnExport As Button
    Private WithEvents btnInfo As Button
    Private WithEvents gbExportOpts As GroupBox
    Friend WithEvents rbtnCSV As RadioButton
    Friend WithEvents rbtnXLSX As RadioButton
    Private WithEvents btnInitSelectSync As Button
    Private WithEvents btnRefSelectSync As Button
    Friend WithEvents slblDesc As ToolStripStatusLabel
    Private WithEvents cbxBoundaryMethod As ComboBox
    Private WithEvents lblBoundaryMethod As Label
    Private WithEvents slblSeparator2 As ToolStripStatusLabel
    Private WithEvents tlblBoundaryMethod As ToolStripStatusLabel
    Private WithEvents slblBoundaryMethod As ToolStripStatusLabel
End Class
