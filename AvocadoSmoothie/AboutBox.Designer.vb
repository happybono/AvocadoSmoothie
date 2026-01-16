<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutBox
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutBox))
        Me.sstripAbout = New System.Windows.Forms.StatusStrip()
        Me.slblCopyright = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblLicenseTerms = New System.Windows.Forms.Label()
        Me.txtLicenseTerms = New System.Windows.Forms.TextBox()
        Me.lblAppVersion = New System.Windows.Forms.Label()
        Me.lblAppTtl = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnDonation = New System.Windows.Forms.Button()
        Me.picboxAppLogo = New System.Windows.Forms.PictureBox()
        Me.sstripAbout.SuspendLayout()
        CType(Me.picboxAppLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'sstripAbout
        '
        Me.sstripAbout.AutoSize = False
        Me.sstripAbout.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.sstripAbout.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.sstripAbout.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.slblCopyright})
        Me.sstripAbout.Location = New System.Drawing.Point(0, 267)
        Me.sstripAbout.Name = "sstripAbout"
        Me.sstripAbout.Size = New System.Drawing.Size(367, 24)
        Me.sstripAbout.SizingGrip = False
        Me.sstripAbout.Stretch = False
        Me.sstripAbout.TabIndex = 23
        '
        'slblCopyright
        '
        Me.slblCopyright.ActiveLinkColor = System.Drawing.Color.Chartreuse
        Me.slblCopyright.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.slblCopyright.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.slblCopyright.DoubleClickEnabled = True
        Me.slblCopyright.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.slblCopyright.ForeColor = System.Drawing.Color.White
        Me.slblCopyright.IsLink = True
        Me.slblCopyright.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.slblCopyright.LinkColor = System.Drawing.Color.White
        Me.slblCopyright.Margin = New System.Windows.Forms.Padding(0)
        Me.slblCopyright.Name = "slblCopyright"
        Me.slblCopyright.Size = New System.Drawing.Size(352, 24)
        Me.slblCopyright.Spring = True
        Me.slblCopyright.Text = "ⓒ 2025 - 2026 HappyBono. All rights reserved."
        '
        'lblLicenseTerms
        '
        Me.lblLicenseTerms.AutoSize = True
        Me.lblLicenseTerms.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseTerms.Location = New System.Drawing.Point(17, 77)
        Me.lblLicenseTerms.Name = "lblLicenseTerms"
        Me.lblLicenseTerms.Size = New System.Drawing.Size(261, 17)
        Me.lblLicenseTerms.TabIndex = 22
        Me.lblLicenseTerms.Text = "Avocado Smoothie Software License Terms"
        '
        'txtLicenseTerms
        '
        Me.txtLicenseTerms.Font = New System.Drawing.Font("Microsoft NeoGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLicenseTerms.Location = New System.Drawing.Point(19, 100)
        Me.txtLicenseTerms.Multiline = True
        Me.txtLicenseTerms.Name = "txtLicenseTerms"
        Me.txtLicenseTerms.ReadOnly = True
        Me.txtLicenseTerms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtLicenseTerms.Size = New System.Drawing.Size(328, 124)
        Me.txtLicenseTerms.TabIndex = 20
        Me.txtLicenseTerms.Text = resources.GetString("txtLicenseTerms.Text")
        '
        'lblAppVersion
        '
        Me.lblAppVersion.AutoSize = True
        Me.lblAppVersion.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppVersion.Location = New System.Drawing.Point(16, 47)
        Me.lblAppVersion.Name = "lblAppVersion"
        Me.lblAppVersion.Size = New System.Drawing.Size(67, 20)
        Me.lblAppVersion.TabIndex = 19
        Me.lblAppVersion.Text = " v.1.0.0.0"
        '
        'lblAppTtl
        '
        Me.lblAppTtl.AutoSize = True
        Me.lblAppTtl.Font = New System.Drawing.Font("Segoe UI Variable Display Semib", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppTtl.Location = New System.Drawing.Point(15, 9)
        Me.lblAppTtl.Name = "lblAppTtl"
        Me.lblAppTtl.Size = New System.Drawing.Size(191, 28)
        Me.lblAppTtl.TabIndex = 18
        Me.lblAppTtl.Text = "Avocado Smoothie"
        '
        'btnOK
        '
        Me.btnOK.Font = New System.Drawing.Font("Microsoft NeoGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnOK.Location = New System.Drawing.Point(268, 232)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(79, 26)
        Me.btnOK.TabIndex = 25
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnDonation
        '
        Me.btnDonation.Font = New System.Drawing.Font("Microsoft NeoGothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(129, Byte))
        Me.btnDonation.Location = New System.Drawing.Point(19, 232)
        Me.btnDonation.Name = "btnDonation"
        Me.btnDonation.Size = New System.Drawing.Size(125, 26)
        Me.btnDonation.TabIndex = 24
        Me.btnDonation.Text = "Buy Me a Coffee"
        Me.btnDonation.UseVisualStyleBackColor = True
        '
        'picboxAppLogo
        '
        Me.picboxAppLogo.Image = CType(resources.GetObject("picboxAppLogo.Image"), System.Drawing.Image)
        Me.picboxAppLogo.Location = New System.Drawing.Point(282, 9)
        Me.picboxAppLogo.Name = "picboxAppLogo"
        Me.picboxAppLogo.Size = New System.Drawing.Size(65, 65)
        Me.picboxAppLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picboxAppLogo.TabIndex = 26
        Me.picboxAppLogo.TabStop = False
        '
        'AboutBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(367, 291)
        Me.Controls.Add(Me.picboxAppLogo)
        Me.Controls.Add(Me.sstripAbout)
        Me.Controls.Add(Me.lblLicenseTerms)
        Me.Controls.Add(Me.txtLicenseTerms)
        Me.Controls.Add(Me.lblAppVersion)
        Me.Controls.Add(Me.lblAppTtl)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnDonation)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "About Avocado Smoothie"
        Me.sstripAbout.ResumeLayout(False)
        Me.sstripAbout.PerformLayout()
        CType(Me.picboxAppLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents sstripAbout As StatusStrip
    Friend WithEvents slblCopyright As ToolStripStatusLabel
    Friend WithEvents lblLicenseTerms As Label
    Friend WithEvents txtLicenseTerms As TextBox
    Friend WithEvents lblAppVersion As Label
    Friend WithEvents lblAppTtl As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents btnDonation As Button
    Friend WithEvents picboxAppLogo As PictureBox
End Class
