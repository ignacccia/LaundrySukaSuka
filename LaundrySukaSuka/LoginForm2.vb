Imports System.Data.OracleClient

Public Class LoginForm2
    Public DS As New DataSet
    Public DA As New OracleDataAdapter
    Public CONN As New OracleConnection
    Public CMD As New OracleCommand
    Public DR As OracleDataReader

    Public Sub koneksi()
        Dim konek As String
        konek = "data source=MyOracleDB; Data Source=XE; User Id=DBLogin; Password=system"
        Try
            CONN = New OracleConnection(konek)
            CONN.Open()
            CONN.Close()
            MsgBox("Koneksi Berhasil!")
        Catch ex As Exception
            MessageBox.Show("Koneksi Gagal!" & ex.Message)
        End Try
    End Sub


    Public Sub tampil()
        CONN.Close()
        DA = New OracleDataAdapter("select * from Login", CONN)
        DS = New DataSet
        DA.Fill(DS, "Login")
    End Sub

    Private Sub LoginForm2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Call koneksi()
        CONN.Open()
        CMD = New OracleCommand("select * from TLogin where USERNAME='" & UsernameTextBox.Text & "'", CONN)
        DR = CMD.ExecuteReader()
        DR.Read()
        If DR.HasRows Then
            If PasswordLamaTextBox.Text = DR.Item("PASSWORD") Then
                CMD = New OracleCommand("update TLogin set USERNAME='" & UsernameTextBox.Text & "', PASSWORD='" & PasswordBaruTextBox.Text & "' where USERNAME='" & UsernameTextBox.Text & "'", CONN)
                DR = CMD.ExecuteReader
                DR.Read()
                MsgBox("Password Berhasil di Ubah")
                Call tampil()
            Else
                MsgBox("Password Lama Salah")
            End If
        End If
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        LoginForm1.Show()
        Me.Hide()
    End Sub

End Class
