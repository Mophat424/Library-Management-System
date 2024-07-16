Imports System.Data.SqlClient

' Connection String and SQL Connection
Public Class Form1
    Private connectionString As String = "Data Source=DESKTOP-1OETD7S;Initial Catalog=LibraryDB;Integrated Security=True"
    Private connection As SqlConnection

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection = New SqlConnection(connectionString)
        LoadBooks()
    End Sub

    'Loading Data into DataGridView
    Private Sub LoadBooks()
        Dim query As String = "SELECT * FROM Books"
        Dim adapter As New SqlDataAdapter(query, connection)
        Dim table As New DataTable()
        adapter.Fill(table)
        dgvBooks.DataSource = table
    End Sub

    'Code for btnAdd Click Event
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim query As String = "INSERT INTO Books (Title, Author, YearPublished, Genre) VALUES (@Title, @Author, @YearPublished, @Genre)"
        Dim command As New SqlCommand(query, connection)
        command.Parameters.AddWithValue("@Title", txtTitle.Text)
        command.Parameters.AddWithValue("@Author", txtAuthor.Text)
        command.Parameters.AddWithValue("@YearPublished", Convert.ToInt32(txtYearPublished.Text))
        command.Parameters.AddWithValue("@Genre", txtGenre.Text)

        connection.Open()
        command.ExecuteNonQuery()
        connection.Close()
        LoadBooks()
    End Sub

    ' Code for btnUpdate Click Event
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If dgvBooks.SelectedRows.Count > 0 Then
            Dim id As Integer = Convert.ToInt32(dgvBooks.SelectedRows(0).Cells("ID").Value)
            Dim query As String = "UPDATE Books SET Title=@Title, Author=@Author, YearPublished=@YearPublished, Genre=@Genre WHERE ID=@ID"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ID", id)
            command.Parameters.AddWithValue("@Title", txtTitle.Text)
            command.Parameters.AddWithValue("@Author", txtAuthor.Text)
            command.Parameters.AddWithValue("@YearPublished", Convert.ToInt32(txtYearPublished.Text))
            command.Parameters.AddWithValue("@Genre", txtGenre.Text)

            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()
            LoadBooks()
        End If
    End Sub


    'Code for btnDelete Click Event
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        If dgvBooks.SelectedRows.Count > 0 Then
            Dim id As Integer = Convert.ToInt32(dgvBooks.SelectedRows(0).Cells("ID").Value)
            Dim query As String = "DELETE FROM Books WHERE ID=@ID"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@ID", id)
            connection.Open()
            command.ExecuteNonQuery()
            connection.Close()
            LoadBooks()
        End If

    End Sub


    'Code for btnClear Click Event
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click

        txtTitle.Clear()
        txtAuthor.Clear()
        txtYearPublished.Clear()
        txtGenre.Clear()


    End Sub

    ' Code for dgvBooks CellClick Event
    Private Sub dgvBooks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBooks.CellContentClick

        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBooks.Rows(e.RowIndex)
            txtTitle.Text = row.Cells("Title").Value.ToString()
            txtAuthor.Text = row.Cells("Author").Value.ToString()
            txtYearPublished.Text = row.Cells("YearPublished").Value.ToString()
            txtGenre.Text = row.Cells("Genre").Value.ToString()
        End If


    End Sub
End Class
