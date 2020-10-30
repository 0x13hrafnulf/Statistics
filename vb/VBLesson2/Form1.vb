Public Class Form1



    Private Sub startForm()
        Me.RichTextBox2.Text = "DATA STREAM" & Environment.NewLine
        Me.RichTextBox2.AppendText("Student#|Age|Avg_Grade|Number_of_Exams]" & Environment.NewLine)
        Me.RichTextBox2.AppendText(Environment.NewLine + Environment.NewLine)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        startForm()
        Timer1.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        number_of_students += 1
        generate_object()
        print_summary()
    End Sub


    Private Sub generate_object()

        Dim age As Integer = rnd.Next(21, 35)
        Dim avg_grade As Integer = rnd.Next(18, 30)
        Dim number_of_exams As Integer = rnd.Next(1, 6)
        Dim stud As Student = New Student(age, avg_grade, number_of_exams)

        print_object_info(stud)
        update_mean(stud)
        update_distribution(stud)

    End Sub


    Private Sub update_mean(stdnt As Student)

        mean_grade += Math.Round(CType((stdnt.m_avg_grade - mean_grade), Double) / number_of_students, 2)

        mean_exam += Math.Round(CType((stdnt.m_number_of_exams - mean_exam), Double) / number_of_students, 2)

    End Sub

    Private Sub update_distribution(stdnt As Student)
        If grades.ContainsKey(stdnt.m_avg_grade) Then
            grades(stdnt.m_avg_grade) += 1
        Else
            grades.Add(stdnt.m_avg_grade, 1)
        End If

        If number_of_exams.ContainsKey(stdnt.m_number_of_exams) Then
            number_of_exams(stdnt.m_number_of_exams) += 1
        Else
            number_of_exams.Add(stdnt.m_number_of_exams, 1)
        End If
    End Sub

    Private Sub print_object_info(stdnt As Student)
        Me.RichTextBox2.AppendText(("[ Student#").PadRight(11) & number_of_students _
                                                & " | " & (stdnt.m_age.ToString()).PadRight(2) _
                                                & " | " & (stdnt.m_avg_grade.ToString()).PadRight(2) _
                                                & " | " & (stdnt.m_number_of_exams.ToString()).PadRight(2) _
                                                & " ]" & Environment.NewLine)
    End Sub


    Private Sub print_summary()


        RichTextBox1.Text = "SUMMARY" & Environment.NewLine
        RichTextBox1.AppendText("AVerage Grade: " & mean_grade.ToString() & Environment.NewLine)
        RichTextBox1.AppendText("Grade Distribution" & Environment.NewLine)
        RichTextBox1.AppendText("Grade[#]".PadRight(12) & ": Count" & Environment.NewLine)
        RichTextBox1.AppendText("------------------" & Environment.NewLine)
        For Each grade In grades
            RichTextBox1.AppendText(("Grade[" & grade.Key & "]").PadRight(12) & ": " & grade.Value & Environment.NewLine)
        Next
        RichTextBox1.AppendText("__________________" & Environment.NewLine & Environment.NewLine)

        RichTextBox1.AppendText("Average Number of Exams: " & mean_exam.ToString() & Environment.NewLine)
        RichTextBox1.AppendText("Number of Exams Distribution" & Environment.NewLine)
        RichTextBox1.AppendText("Exams[#]".PadRight(12) & ": Count" & Environment.NewLine)
        RichTextBox1.AppendText("------------------" & Environment.NewLine)

        For Each exams In number_of_exams
            RichTextBox1.AppendText(("Exams[" & exams.Key & "]").PadRight(12) & ": " & exams.Value & Environment.NewLine)
        Next
        RichTextBox1.AppendText("__________________" & Environment.NewLine & Environment.NewLine)



    End Sub






End Class
