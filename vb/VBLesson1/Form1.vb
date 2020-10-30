


Public Class VBLeson1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.RichTextBox1.Text = "Me Is Lesson1, Task 1_A:a"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.RichTextBox1.Clear()
    End Sub

    Private Sub RichTextBox1_MouseEnter(sender As Object, e As EventArgs) Handles RichTextBox1.MouseEnter
        Me.RichTextBox1.BackColor = Color.LightBlue
    End Sub

    Private Sub RichTextBox1_MouseLeave(sender As Object, e As EventArgs) Handles RichTextBox1.MouseLeave
        Me.RichTextBox1.BackColor = Color.White
    End Sub
    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragDrop
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each path In files
            Me.RichTextBox1.AppendText(path)
        Next
    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles RichTextBox1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim student1 As Student = New Student(19, "John", 1)
        Dim student2 As Student = New Student(23, "Stevy", 2)
        Dim student3 As Student = New Student(27, "Stewart", 3)
        Me.RichTextBox1.AppendText("*******REFERENCE TYPE*******" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Initial values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student1: name = " & student1.m_name & "; ID = " & student1.m_ID & "; Age = " & student1.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student2: name = " & student2.m_name & "; ID = " & student2.m_ID & "; Age = " & student2.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student3: name = " & student3.m_name & "; ID = " & student3.m_ID & "; Age = " & student3.m_age & ";" & Environment.NewLine)

        student1 = student3
        Me.RichTextBox1.AppendText(Environment.NewLine & "> Update" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student1 = Student3" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Updated values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student1: name = " & student1.m_name & "; ID = " & student1.m_ID & "; Age = " & student1.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student2: name = " & student2.m_name & "; ID = " & student2.m_ID & "; Age = " & student2.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student3: name = " & student3.m_name & "; ID = " & student3.m_ID & "; Age = " & student3.m_age & ";" & Environment.NewLine)

        student1.m_age = 20
        Me.RichTextBox1.AppendText(Environment.NewLine & "> Update" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student1.m_age = 20" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Updated values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student1: name = " & student1.m_name & "; ID = " & student1.m_ID & "; Age = " & student1.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student2: name = " & student2.m_name & "; ID = " & student2.m_ID & "; Age = " & student2.m_age & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Student3: name = " & student3.m_name & "; ID = " & student3.m_ID & "; Age = " & student3.m_age & ";" & Environment.NewLine)




        ' **************************************
        Dim point1 As Point = New Point(15, 20)
        Dim point2 As Point = New Point(10, 13)
        Dim point3 As Point = New Point(5, 7)

        Me.RichTextBox1.AppendText(Environment.NewLine & "*********VALUE TYPE*********" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Initial values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point2: x = " & point2.m_x & "; y = " & point2.m_y & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3: x = " & point3.m_x & "; y = " & point3.m_y & ";" & Environment.NewLine)
        point3 = point1
        Me.RichTextBox1.AppendText(Environment.NewLine & "> Update" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3 = Point1" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Updated values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point1: x = " & point1.m_x & "; y = " & point1.m_y & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point2: x = " & point2.m_x & "; y = " & point2.m_y & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3: x = " & point3.m_x & "; y = " & point3.m_y & ";" & Environment.NewLine)

        point3.m_x = 20
        point3.m_y = 1
        Me.RichTextBox1.AppendText(Environment.NewLine & "> Update" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3.m_x = 20" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3.m_y = 1" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Updated values: " & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point1: x = " & point1.m_x & "; y = " & point1.m_y & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point2: x = " & point2.m_x & "; y = " & point2.m_y & ";" & Environment.NewLine)
        Me.RichTextBox1.AppendText("> Point3: x = " & point3.m_x & "; y = " & point3.m_y & ";" & Environment.NewLine)

    End Sub
End Class

Public Class Student

    Public Sub New(age As Integer, name As String, id As UInteger)
        m_age = age
        m_name = name
        m_ID = id
    End Sub

    Public m_age As Integer
    Public m_name As String
    Public m_ID As UInteger

End Class
Public Structure Point

    Public Sub New(x As Integer, y As Integer)
        m_x = x
        m_y = y
    End Sub

    Public m_x As Integer
    Public m_y As Integer
End Structure