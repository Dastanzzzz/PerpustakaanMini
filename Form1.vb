Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim judul As String = InputBox("Masukkan Judul Buku:", "Tambah Buku")
        If judul <> "" Then
            System.IO.File.AppendAllText("buku.txt", judul & vbCrLf)
            MessageBox.Show("Buku berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If System.IO.File.Exists("buku.txt") Then
            Dim daftarBuku As String = System.IO.File.ReadAllText("buku.txt")
            MessageBox.Show("Daftar Buku:" & vbCrLf & daftarBuku, "Daftar Buku", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Belum ada buku dalam daftar.", "Daftar Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim keyword As String = InputBox("Masukkan kata kunci:", "Cari Buku")
        Dim hasil As String = ""

        If System.IO.File.Exists("buku.txt") Then
            Dim buku() As String = System.IO.File.ReadAllLines("buku.txt")
            For Each judul In buku
                If judul.ToLower().Contains(keyword.ToLower()) Then
                    hasil &= judul & vbCrLf
                End If
            Next
        End If

        If hasil <> "" Then
            MessageBox.Show("Buku ditemukan:" & vbCrLf & hasil, "Hasil Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Buku tidak ditemukan.", "Hasil Pencarian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' Memastikan file buku.txt ada
        If Not System.IO.File.Exists("buku.txt") Then
            MessageBox.Show("Belum ada buku dalam daftar.", "Edit Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Input buku yang ingin diedit
        Dim judulLama As String = InputBox("Masukkan judul buku yang ingin diedit:", "Edit Buku")
        Dim judulBaru As String = InputBox("Masukkan judul baru:", "Edit Buku")

        If judulLama = "" OrElse judulBaru = "" Then
            MessageBox.Show("Judul tidak boleh kosong!", "Edit Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Baca semua buku dari file
        Dim buku As List(Of String) = System.IO.File.ReadAllLines("buku.txt").ToList()

        ' Cek apakah buku ada dalam daftar
        Dim index As Integer = buku.FindIndex(Function(b) b.ToLower() = judulLama.ToLower())
        If index <> -1 Then
            buku(index) = judulBaru
            System.IO.File.WriteAllLines("buku.txt", buku)
            MessageBox.Show("Buku berhasil diubah!", "Edit Buku", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("Buku tidak ditemukan!", "Edit Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ' Memastikan file buku.txt ada
        If Not System.IO.File.Exists("buku.txt") Then
            MessageBox.Show("Belum ada buku dalam daftar.", "Hapus Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Input judul buku yang ingin dihapus
        Dim judulHapus As String = InputBox("Masukkan judul buku yang ingin dihapus:", "Hapus Buku")

        If judulHapus = "" Then
            MessageBox.Show("Judul tidak boleh kosong!", "Hapus Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Baca semua buku dari file
        Dim buku As List(Of String) = System.IO.File.ReadAllLines("buku.txt").ToList()

        ' Filter buku yang tidak dihapus
        Dim bukuBaru As List(Of String) = buku.Where(Function(b) Not b.ToLower().Equals(judulHapus.ToLower())).ToList()

        ' Jika jumlah buku tidak berubah, berarti buku tidak ditemukan
        If buku.Count = bukuBaru.Count Then
            MessageBox.Show("Buku tidak ditemukan!", "Hapus Buku", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            System.IO.File.WriteAllLines("buku.txt", bukuBaru)
            MessageBox.Show("Buku berhasil dihapus!", "Hapus Buku", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

End Class
