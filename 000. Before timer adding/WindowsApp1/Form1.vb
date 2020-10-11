Public Class Form1

    Dim currentLanguage As String       ' Current language variable
    Dim myCursor As Point               ' Cursor variable (point)
    Dim clickingStatus = False          ' Current clicking status (on - true, off - false)
    Dim clickingSpeed As Integer        ' Clicking speed

    Const F8 = 119, F9 = 120            ' Constants for F8 and F9 buttons (F8 - start, F9 - stop); consisting KeyValue (F8 - 119, F9 - 120)

    ' Global key hooking (API using)
    <Runtime.InteropServices.DllImport("user32.dll")>
    Shared Function GetAsyncKeyState(ByVal vKey As System.Windows.Forms.Keys) As Short
    End Function
    ' Mouse clicking procedure (API using)
    <Runtime.InteropServices.DllImport("user32.dll")>
    Private Shared Sub mouse_event(dwFlags As UInteger, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As Integer)
    End Sub
    ' Cursor position getting function (API using)
    Public Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As Point) As Integer

    ' Set english language procedure
    Sub SetEnglish(ByRef currentLanguage As String)
        If currentLanguage IsNot "english" Then
            Label1.Text = "Speed (required):"
            Label2.Text = "h"
            Label3.Text = "m"
            Label4.Text = "s"
            Label5.Text = "ms"
            Label6.Text = "Optional features:"

            CheckBox1.Text = "Unstable clicking"
            CheckBox2.Text = "Shut down"

            RadioButton1.Text = "Time"
            RadioButton2.Text = "Count"

            currentLanguage = "english"
        End If
    End Sub
    ' Set russian language procedure
    Sub SetRussian(ByRef currentLanguage As String)
        If currentLanguage IsNot "russian" Then
            Label1.Text = "Скорость (обязательно):"
            Label2.Text = "ч"
            Label3.Text = "м"
            Label4.Text = "с"
            Label5.Text = "мс"
            Label6.Text = "Опционально:"

            CheckBox1.Text = "Нестабильные клики"
            CheckBox2.Text = "Выключение"

            RadioButton1.Text = "Время"
            RadioButton2.Text = "Количество"

            currentLanguage = "russian"
        End If
    End Sub
    ' Clicking Function
    Sub Clicking()
        While clickingStatus
            If GetKey() = 120 Then
                clickingStatus = False
            End If
            ' Cursor position getting
            GetCursorPos(myCursor)
            ' LMB click with pause
            mouse_event(&H2, myCursor.X, myCursor.Y, 0, 0)       ' LMB down
            mouse_event(&H4, myCursor.X, myCursor.Y, 0, 0)       ' LMB up
            System.Threading.Thread.Sleep(20)                    ' pause
            Application.DoEvents()
        End While
    End Sub
    ' Get key function
    Function GetKey() As Integer
        Dim result As Integer
        result = 0
        If (GetAsyncKeyState(119) And &H1) = &H1 Then
            result = 119
        End If
        If (GetAsyncKeyState(120) And &H1) = &H1 Then
            result = 120
        End If
        Return result
    End Function

    ' Main Form Loading
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        currentLanguage = "none"
        SetEnglish(currentLanguage)

        ComboBox1.SelectedItem = "100"   ' Set in combobox default speed (100 ms)
        While True
            If GetKey() = F8 And Not clickingStatus Then
                clickingStatus = True
                Clicking()
            End If
        End While
    End Sub

    ' English language button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SetEnglish(currentLanguage)
    End Sub
    ' Russian language button
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SetRussian(currentLanguage)
    End Sub

End Class
