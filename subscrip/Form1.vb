Imports System.IO

Imports System.Text

Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label2.Text = My.Computer.FileSystem.CurrentDirectory + "\1.srt"

        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim filePath As String


        Dim sw As StreamWriter
        Dim fs As FileStream = Nothing

        Dim save_cont(30000) As String
        Dim save_time(30000) As String
        Dim save_text(30000) As String



        filePath = My.Computer.FileSystem.CurrentDirectory + "\1.srt"
        If My.Computer.FileSystem.FileExists(filePath) = False Then
            MsgBox("File Not Found: " & filePath)
        Else

            Dim reader As New StreamReader(filePath, Encoding.Default)
            Dim line As String

            'fileReader = My.Computer.FileSystem.ReadAllText(filePath)
            Dim Count As Integer = 0
            Dim incom As Integer = 0
            Dim dcont As Integer = 0
            'Count = fileReader.Length

            'My.Computer.FileSystem.DeleteFile(My.Computer.FileSystem.CurrentDirectory + "\2.srt")

            ' Create or overwrite the file.
            'Dim fs As FileStream = File.Create(My.Computer.FileSystem.CurrentDirectory + "\2.srt")

            ' Add text to the file.
            If (Not File.Exists(My.Computer.FileSystem.CurrentDirectory & "\2.srt")) Then
                Try
                    fs = File.Create(My.Computer.FileSystem.CurrentDirectory + "\2.srt")
                    sw = File.AppendText(My.Computer.FileSystem.CurrentDirectory + "\2.srt")
                    'sw.WriteLine("Start Error Log for today")

                Catch ex As Exception
                    MsgBox("Error Creating Log File")
                End Try
            Else
                'sw = My.Computer.FileSystem.OpenTextFileWriter(My.Computer.FileSystem.CurrentDirectory + "\2.srt", True)
                sw = File.AppendText(My.Computer.FileSystem.CurrentDirectory + "\2.srt")
            End If
            'For Each line As String In fileReader



            Do
                dcont = incom / 4

                line = reader.ReadLine
                'MsgBox("line: " & line)
                Try
                    If line.Contains(" --> ") Then
                        save_time(dcont) = line.ToString
                        sw.WriteLine(change_time(line))
                    Else
                        save_text(dcont) = line.ToString
                        sw.WriteLine(line)
                    End If
                Catch ex As Exception
                    MsgBox("line: " & dcont)
                End Try

                incom = incom + 1
                'Next


            Loop Until line Is Nothing

            sw.Close()
            MsgBox("line: " & save_cont(dcont))


            End If
    End Sub

    Function change_time(ByVal lineget As String)
        Dim gettext As String
        Dim OutPutArray() As System.String
        Dim startPutArray() As System.String
        Dim starttimeArray() As System.String
        Dim endPutArray() As System.String
        Dim endtimeArray() As System.String

        Dim startsec As Double
        Dim endsec As Double

        Dim start As String
        Dim ending As String

        gettext = lineget

        OutPutArray = Split(lineget, " --> ", -1)
        startPutArray = Split(OutPutArray(0), ",", -1)
        starttimeArray = Split(startPutArray(0), ":", -1)
        endPutArray = Split(OutPutArray(1), ",", -1)
        endtimeArray = Split(endPutArray(0), ":", -1)

        startsec = Integer.Parse(starttimeArray(0)) * 3600 + Integer.Parse(starttimeArray(1)) * 60 + Integer.Parse(starttimeArray(2))
        endsec = Integer.Parse(endtimeArray(0)) * 3600 + Integer.Parse(endtimeArray(1)) * 60 + Integer.Parse(endtimeArray(2))

        If startsec < (Integer.Parse(TextBox1.Text) * 60 + Integer.Parse(TextBox2.Text)) Then
            startsec = 0
        Else
            startsec = startsec - (Integer.Parse(TextBox1.Text) * 60) - Integer.Parse(TextBox2.Text)
        End If

        If endsec < (Integer.Parse(TextBox1.Text) * 60 + Integer.Parse(TextBox2.Text)) Then
            endsec = 0
        Else
            endsec = endsec - (Integer.Parse(TextBox1.Text) * 60) - Integer.Parse(TextBox2.Text)
        End If

        Dim iir As Integer
        iir = startsec - (Math.Floor(startsec / 3600) * 3600)
        Dim isr As Integer
        isr = endsec - (Math.Floor(endsec / 3600) * 3600)





        If CInt(startsec / 3600) < 10 Then
            start = "0" + Math.Floor(startsec / 3600).ToString + ":"
        Else
            start = Math.Floor(startsec / 3600).ToString + ":"
        End If

        If Math.Floor(iir / 60) < 10 Then
            start = start + "0" + Math.Floor(iir / 60).ToString + ":"
        Else
            start = start + Math.Floor(iir / 60).ToString + ":"
        End If

        If CInt(startsec Mod 60) < 10 Then
            start = start + "0" + CInt(startsec Mod 60).ToString
        Else
            start = start + CInt(startsec Mod 60).ToString
        End If



        If CInt(endsec / 3600) < 10 Then
            ending = "0" + Math.Floor(endsec / 3600).ToString + ":"
        Else
            ending = Math.Floor(endsec / 3600).ToString + ":"
        End If

        If Math.Floor(isr / 60) < 10 Then
            ending = ending + "0" + Math.Floor(isr / 60).ToString + ":"
        Else
            ending = ending + Math.Floor(isr / 60).ToString + ":"
        End If

        If CInt(endsec Mod 60) < 10 Then
            ending = ending + "0" + CInt(endsec Mod 60).ToString
        Else
            ending = ending + CInt(endsec Mod 60).ToString
        End If

        'start = CInt(startsec / 3600).ToString + ":" + Math.Floor(iir / 60).ToString + ":" + CInt(startsec Mod 60).ToString
        'ending = CInt(endsec / 3600).ToString + ":" + Math.Floor(isr / 60).ToString + ":" + CInt(endsec Mod 60).ToString

        gettext = start.ToString + "," + startPutArray(1) + " --> " + ending.ToString + "," + endPutArray(1)
        Return gettext
    End Function
End Class
