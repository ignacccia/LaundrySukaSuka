Imports System.Data.OracleClient

Public Class LoginForm1
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
    Public Sub hapus()
        UsernameTextBox.Clear()
        PasswordTextBox.Clear()
        UsernameTextBox.Focus()
    End Sub

    Public Sub tampil()
        CONN.Close()
        DA = New OracleDataAdapter("select * from Login", CONN)
        DS = New DataSet
        DA.Fill(DS, "Login")
    End Sub

    Private Sub LoginForm1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil()
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
            Call koneksi()
            CONN.Open()
        CMD = New OracleCommand("select * from Login where USERNAME='" & UsernameTextBox.Text & "'", CONN)
        DR = CMD.ExecuteReader()
            DR.Read()
            If DR.HasRows Then
                If PasswordTextBox.Text = DR.Item("PASSWORD") Then
                    MsgBox("Login Berhasil")
                    Form1.Show()
                Me.Hide()
                Else
                MsgBox("Password Salah")
                PasswordTextBox.Clear()
                PasswordTextBox.Focus()
                End If
            Else
                MsgBox("Username Tidak Ada")
                Call hapus()
            End If
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            PasswordTextBox.PasswordChar = "*"
        ElseIf CheckBox1.Checked = True Then
            PasswordTextBox.PasswordChar = ""
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        LoginForm2.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoginForm3.Show()
        Me.Hide()
    End Sub
End Class