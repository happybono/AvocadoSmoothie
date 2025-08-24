Public Class AboutBox
    Private Sub AboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblAppTtl.Select()
        lblAppVersion.Text = " v." & ProductVersion
    End Sub

    Private Sub slblCopyright_Click(sender As Object, e As EventArgs) Handles slblCopyright.Click
        Process.Start("https://www.github.com/happybono")
    End Sub

    Private Sub btnDonation_Click(sender As Object, e As EventArgs) Handles btnDonation.Click
        Process.Start("https://www.paypal.com/ncp/payment/ZYELY39LHBSUU", vbMinimizedNoFocus)
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
End Class