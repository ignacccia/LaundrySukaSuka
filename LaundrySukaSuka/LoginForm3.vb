Imports System.Data.OracleClient

Public Class LoginForm3
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

    Private Sub LoginForm3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil()
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Call koneksi()
        CONN.Open()
        CMD = New OracleCommand("insert into Login values ('" & UsernameTextBox.Text & "', '" & PasswordTextBox.Text & "')", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        MsgBox("Data Berhasil di Buat")
        Call tampil()
        Call hapus()
    End Sub

    Private Sub Cancel_Click(sender As Object, e As EventArgs) Handles Cancel.Click
        Me.Hide()
        LoginForm1.Show()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False Then
            PasswordTextBox.PasswordChar = "*"
        ElseIf CheckBox1.Checked = True Then
            PasswordTextBox.PasswordChar = ""
        End If
    End Sub

End Class
