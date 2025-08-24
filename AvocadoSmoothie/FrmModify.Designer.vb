<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmModify
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmModify))
        Me.sstripModify = New System.Windows.Forms.StatusStrip()
        Me.slblModify = New System.Windows.Forms.ToolStripStatusLabel()
        Me.txtInitEdit = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.lblModifyDscr = New System.Windows.Forms.Label()
        Me.pbModify = New System.Windows.Forms.ProgressBar()
        Me.lblModifyTtl = New System.Windows.Forms.Label()
        Me.sstripModify.SuspendLayout()
        Me.SuspendLayout()
        '
        'sstripModify
        '
        Me.sstripModify.AutoSize = False
        Me.sstripModify.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sstripModify.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.sstripModify.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.sstripModify.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.slblModify})
        Me.sstripModify.Location = New System.Drawing.Point(0, 119)
        Me.sstripModify.Name = "sstripModify"
        Me.sstripModify.Size = New System.Drawing.Size(438, 24)
        Me.sstripModify.SizingGrip = False
        Me.sstripModify.TabIndex = 28
        Me.sstripModify.Text = "StatusStrip1"
        '
        'slblModify
        '
        Me.slblModify.AutoSize = False
        Me.slblModify.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.slblModify.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.0!)
        Me.slblModify.ForeColor = System.Drawing.Color.White
        Me.slblModify.Name = "slblModify"
        Me.slblModify.Size = New System.Drawing.Size(437, 19)
        Me.slblModify.Text = "Modifying 2147483647 selected items... "
        Me.slblModify.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtInitEdit
        '
        Me.txtInitEdit.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInitEdit.Location = New System.Drawing.Point(14, 81)
        Me.txtInitEdit.Name = "txtInitEdit"
        Me.txtInitEdit.Size = New System.Drawing.Size(411, 25)
        Me.txtInitEdit.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(340, 53)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(85, 24)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = ""
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Segoe Fluent Icons", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOK.Location = New System.Drawing.Point(340, 7)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(85, 44)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = ""
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblModifyDscr
        '
        Me.lblModifyDscr.AutoSize = True
        Me.lblModifyDscr.Font = New System.Drawing.Font("Segoe UI Variable Display", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyDscr.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblModifyDscr.Location = New System.Drawing.Point(12, 39)
        Me.lblModifyDscr.Name = "lblModifyDscr"
        Me.lblModifyDscr.Size = New System.Drawing.Size(270, 34)
        Me.lblModifyDscr.TabIndex = 24
        Me.lblModifyDscr.Text = "Apply changes to the selected items. " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Enter the numeric value you would like to " &
    "set :"
        '
        'pbModify
        '
        Me.pbModify.Location = New System.Drawing.Point(0, 114)
        Me.pbModify.Name = "pbModify"
        Me.pbModify.Size = New System.Drawing.Size(438, 5)
        Me.pbModify.TabIndex = 29
        '
        'lblModifyTtl
        '
        Me.lblModifyTtl.AutoSize = True
        Me.lblModifyTtl.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModifyTtl.Location = New System.Drawing.Point(9, 7)
        Me.lblModifyTtl.Name = "lblModifyTtl"
        Me.lblModifyTtl.Size = New System.Drawing.Size(214, 26)
        Me.lblModifyTtl.TabIndex = 30
        Me.lblModifyTtl.Text = "Modify Selected Entries"
        '
        'FrmModify
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(438, 143)
        Me.Controls.Add(Me.lblModifyTtl)
        Me.Controls.Add(Me.sstripModify)
        Me.Controls.Add(Me.txtInitEdit)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.lblModifyDscr)
        Me.Controls.Add(Me.pbModify)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmModify"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modify Selected Entries"
        Me.sstripModify.ResumeLayout(False)
        Me.sstripModify.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents sstripModify As System.Windows.Forms.StatusStrip
    Friend WithEvents slblModify As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents txtInitEdit As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblModifyDscr As System.Windows.Forms.Label
    Friend WithEvents pbModify As System.Windows.Forms.ProgressBar
    Friend WithEvents lblModifyTtl As System.Windows.Forms.Label
End Class