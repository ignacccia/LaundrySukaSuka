Imports System.Data.OracleClient

Public Class Form1
    Public DS As New DataSet
    Public DA As New OracleDataAdapter
    Public CONN As New OracleConnection
    Public CMD As New OracleCommand
    Public DR As OracleDataReader

    Public Sub koneksi()
        Dim konek As String
        konek = "data source=MyOracleDB; Data Source=XE; User Id=DBUjian; Password=system"
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
        TextBox1.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox1.Focus()
    End Sub

    Public Sub tampil()
        CONN.Close()
        DA = New OracleDataAdapter("select * from TUas", CONN)
        DS = New DataSet
        DA.Fill(DS, "TUas")
        DataGridView1.DataSource = DS.Tables("TUas")
        DataGridView1.ReadOnly = True
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call tampil()
    End Sub
    Public Sub hitung()
        If ComboBox1.SelectedItem = "Cuci Basah" Then
            TextBox4.Text = Val(TextBox3.Text) * Val(3000)
        ElseIf ComboBox1.SelectedItem = "Cuci Kering" Then
            TextBox4.Text = Val(TextBox3.Text) * Val(4000)
        ElseIf ComboBox1.SelectedItem = "Cuci Setrika" Then
            TextBox4.Text = Val(TextBox3.Text) * Val(6000)
        Else
            MsgBox("Data Tidak Boleh Kosong!!!")
        End If
    End Sub
    Private Sub TextBox3_KeyPressed(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            Call hitung()
        End If
    End Sub
    Private Sub TextBox1_KeyPressed(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call koneksi()
            CONN.Open()
            CMD = New OracleCommand("select * from TUas where NAMAPELANGGAN='" & TextBox1.Text & "'", CONN)
            DR = CMD.ExecuteReader()
            DR.Read()
            If DR.HasRows Then
                ComboBox1.Text = DR.Item("JENIS")
                TextBox3.Text = DR.Item("BERAT")
                TextBox4.Text = DR.Item("HARGA")
            Else
                MsgBox("Nama Tidak Valid !")
                Call hapus()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call koneksi()
        CONN.Open()
        CMD = New OracleCommand("insert into TUas values ('" & TextBox1.Text & "', '" & ComboBox1.Text & "', '" & TextBox3.Text & "', '" & TextBox4.Text & "')", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        MsgBox("Data Berhasil di Simpan")
        Call tampil()
        Call hapus()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call koneksi()
        CONN.Open()
        CMD = New OracleCommand("update TUas set NAMAPELANGGAN='" & TextBox1.Text & "', JENIS='" & ComboBox1.Text & "', BERAT='" & TextBox3.Text & "', HARGA='" & TextBox4.Text & "' where NAMAPELANGGAN='" & TextBox1.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        MsgBox("Data Berhasil di Ubah")
        Call tampil()
        Call hapus()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call koneksi()
        CONN.Open()
        CMD = New OracleCommand("delete from TUas where NAMAPELANGGAN='" & TextBox1.Text & "'", CONN)
        DR = CMD.ExecuteReader
        DR.Read()
        MsgBox("Data Berhasil di Hapus")
        Call tampil()
        Call hapus()
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Close()
    End Sub

End Class