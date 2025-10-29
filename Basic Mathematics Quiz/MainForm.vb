Imports System.Collections.Generic
Imports System.Math

Public Class MainForm
    ' Quiz data
    Private advancedQuestions As New List(Of String)()
    Private advancedAnswers As New List(Of Double)()
    Private currentQuestionIndex As Integer = 0
    Private totalQuestions As Integer = 0
    Private attempts As Integer = 0
    Private correctAnswers As Integer = 0
    Private failedQuestions As Integer = 0
    Private currentChances As Integer = 3
    Private currentQuestionAnswer As Double = 0
    Private random As New Random()
    Private currentQuizType As String = ""
    Private currentOperation As String = ""

    ' Form controls
    Private WithEvents pnlMainMenu As New Panel()
    Private WithEvents pnlQuiz As New Panel()
    Private WithEvents pnlSetup As New Panel()
    Private WithEvents pnlStatistics As New Panel()

    ' Main menu controls
    Private WithEvents lblTitle As New Label()
    Private WithEvents btnAddition As New Button()
    Private WithEvents btnSubtraction As New Button()
    Private WithEvents btnDivision As New Button()
    Private WithEvents btnMultiplication As New Button()
    Private WithEvents btnAdvanced As New Button()
    Private WithEvents btnExit As New Button()

    ' Quiz controls
    Private WithEvents lblQuestion As New Label()
    Private WithEvents txtAnswer As New TextBox()
    Private WithEvents btnSubmitAnswer As New Button()
    Private WithEvents btnBack As New Button()

    ' Setup controls
    Private WithEvents lblSetupTitle As New Label()
    Private WithEvents btnStartBasic As New Button()
    Private WithEvents btnStartAdvanced As New Button()

    ' Statistics controls
    Private WithEvents lstStatistics As New ListBox()
    Private WithEvents btnBackToMenu As New Button()

    Public Sub New()
        InitializeCustomComponent()
        InitializeAdvancedQuestions()
        ApplyStyling()
        ShowMainMenu()
    End Sub

    Private Sub InitializeCustomComponent()
        ' Form setup
        Me.Text = "Mathematics Quiz Application"
        Me.Size = New Drawing.Size(600, 500)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Drawing.Color.FromArgb(240, 240, 240)

        ' Main Menu Panel
        SetupMainMenuPanel()

        ' Quiz Panel  
        SetupQuizPanel()

        ' Setup Panel
        SetupSetupPanel()

        ' Statistics Panel
        SetupStatisticsPanel()

        ' Add all panels to form
        Me.Controls.Add(pnlMainMenu)
        Me.Controls.Add(pnlQuiz)
        Me.Controls.Add(pnlSetup)
        Me.Controls.Add(pnlStatistics)
    End Sub

    Private Sub SetupMainMenuPanel()
        ' Title
        lblTitle.Text = "Mathematics Quiz"
        lblTitle.Size = New Drawing.Size(500, 60)
        lblTitle.Location = New Drawing.Point(50, 30)
        lblTitle.TextAlign = ContentAlignment.MiddleCenter

        ' Buttons
        btnAddition.Text = "1. Addition"
        btnAddition.Size = New Drawing.Size(400, 40)
        btnAddition.Location = New Drawing.Point(100, 100)

        btnSubtraction.Text = "2. Subtraction"
        btnSubtraction.Size = New Drawing.Size(400, 40)
        btnSubtraction.Location = New Drawing.Point(100, 150)

        btnDivision.Text = "3. Division"
        btnDivision.Size = New Drawing.Size(400, 40)
        btnDivision.Location = New Drawing.Point(100, 200)

        btnMultiplication.Text = "4. Multiplication"
        btnMultiplication.Size = New Drawing.Size(400, 40)
        btnMultiplication.Location = New Drawing.Point(100, 250)

        btnAdvanced.Text = "5. Advanced Mathematics Quiz"
        btnAdvanced.Size = New Drawing.Size(400, 40)
        btnAdvanced.Location = New Drawing.Point(100, 300)

        btnExit.Text = "0. Exit"
        btnExit.Size = New Drawing.Size(400, 40)
        btnExit.Location = New Drawing.Point(100, 350)

        ' Add controls to panel
        pnlMainMenu.Controls.AddRange({lblTitle, btnAddition, btnSubtraction, btnDivision,
                                      btnMultiplication, btnAdvanced, btnExit})
        pnlMainMenu.Size = Me.Size
    End Sub

    Private Sub SetupQuizPanel()
        lblQuestion.Text = "(Question 1):"
        lblQuestion.Size = New Drawing.Size(400, 30)
        lblQuestion.Location = New Drawing.Point(50, 150)
        lblQuestion.TextAlign = ContentAlignment.MiddleLeft

        txtAnswer.Size = New Drawing.Size(100, 30)
        txtAnswer.Location = New Drawing.Point(250, 150)
        txtAnswer.BackColor = Drawing.Color.White
        txtAnswer.ForeColor = Drawing.Color.DarkBlue
        txtAnswer.BorderStyle = BorderStyle.FixedSingle


        'added
        'txtAnswer.Font = New Drawing.Font("Segeo UI", 11, Drawing.FontStyle.Bold)
        'txtAnswer.ReadOnly = False
        'txtAnswer.Enabled = True

        btnSubmitAnswer.Text = "Submit Answer"
        btnSubmitAnswer.Size = New Drawing.Size(120, 35)
        btnSubmitAnswer.Location = New Drawing.Point(370, 148)

        btnBack.Text = "Back to Main Menu"
        btnBack.Size = New Drawing.Size(200, 35)
        btnBack.Location = New Drawing.Point(200, 250)

        pnlQuiz.Controls.AddRange({lblQuestion, txtAnswer, btnSubmitAnswer, btnBack})
        pnlQuiz.Size = Me.Size
        pnlQuiz.Visible = False
    End Sub

    Private Sub SetupSetupPanel()
        lblSetupTitle.Text = "You selected Advanced Mathematics Quiz." & vbCrLf & "This quiz will have 5 questions."
        lblSetupTitle.Size = New Drawing.Size(500, 60)
        lblSetupTitle.Location = New Drawing.Point(50, 150)
        lblSetupTitle.TextAlign = ContentAlignment.MiddleCenter

        btnStartBasic.Text = "Start Quiz"
        btnStartBasic.Size = New Drawing.Size(150, 35)
        btnStartBasic.Location = New Drawing.Point(225, 250)
        btnStartBasic.Visible = False

        btnStartAdvanced.Text = "Start Quiz"
        btnStartAdvanced.Size = New Drawing.Size(150, 35)
        btnStartAdvanced.Location = New Drawing.Point(225, 250)

        pnlSetup.Controls.AddRange({lblSetupTitle, btnStartBasic, btnStartAdvanced})
        pnlSetup.Size = Me.Size
        pnlSetup.Visible = False
    End Sub

    Private Sub SetupStatisticsPanel()
        lstStatistics.Size = New Drawing.Size(400, 200)
        lstStatistics.Location = New Drawing.Point(100, 100)

        btnBackToMenu.Text = "Back to Main Menu"
        btnBackToMenu.Size = New Drawing.Size(200, 35)
        btnBackToMenu.Location = New Drawing.Point(200, 350)

        pnlStatistics.Controls.AddRange({lstStatistics, btnBackToMenu})
        pnlStatistics.Size = Me.Size
        pnlStatistics.Visible = False
    End Sub

    Private Sub InitializeAdvancedQuestions()
        ' Pre-defined advanced math questions
        advancedQuestions.Add("3 x 6 x 9")
        advancedAnswers.Add(162)

        advancedQuestions.Add("2 / 6 + 3 x 2")
        advancedAnswers.Add(6.333)

        advancedQuestions.Add("1 x 4 + 1 - 1 + 4 / 7")
        advancedAnswers.Add(4.571)

        advancedQuestions.Add("5 + 8 x 3 - 2")
        advancedAnswers.Add(27)

        advancedQuestions.Add("12 / 4 x 3 + 5")
        advancedAnswers.Add(14)

        advancedQuestions.Add("7 x 2 - 4 / 2 + 8")
        advancedAnswers.Add(20)

        advancedQuestions.Add("15 / 3 + 6 x 2 - 1")
        advancedAnswers.Add(16)

        advancedQuestions.Add("9 + 3 x 4 / 2 - 5")
        advancedAnswers.Add(10)

        advancedQuestions.Add("20 / 5 x 2 + 8 - 3")
        advancedAnswers.Add(13)

        advancedQuestions.Add("6 x 3 + 12 / 4 - 2")
        advancedAnswers.Add(19)
    End Sub

    Private Sub ApplyStyling()
        ' Title styling
        lblTitle.Font = New Drawing.Font("Segoe UI", 18, Drawing.FontStyle.Bold)
        lblTitle.ForeColor = Drawing.Color.FromArgb(0, 102, 204)

        ' Button styling
        Dim buttons As List(Of Button) = New List(Of Button) From {
            btnAddition, btnSubtraction, btnDivision, btnMultiplication,
            btnAdvanced, btnExit, btnSubmitAnswer, btnBack,
            btnStartBasic, btnStartAdvanced, btnBackToMenu
        }

        For Each btn As Button In buttons
            btn.BackColor = Drawing.Color.FromArgb(0, 102, 204)
            btn.ForeColor = Drawing.Color.White
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderSize = 0
            btn.FlatAppearance.MouseOverBackColor = Drawing.Color.FromArgb(0, 122, 204)
            btn.FlatAppearance.MouseDownBackColor = Drawing.Color.FromArgb(0, 82, 164)
            btn.Font = New Drawing.Font("Segoe UI", 10, Drawing.FontStyle.Bold)
        Next

        ' Label styling
        lblQuestion.Font = New Drawing.Font("Segoe UI", 12, Drawing.FontStyle.Bold)
        lblQuestion.ForeColor = Drawing.Color.FromArgb(51, 51, 51)

        lblSetupTitle.Font = New Drawing.Font("Segoe UI", 12, Drawing.FontStyle.Bold)
        lblSetupTitle.ForeColor = Drawing.Color.FromArgb(0, 102, 204)

        ' Textbox styling
        txtAnswer.Font = New Drawing.Font("Segoe UI", 11, Drawing.FontStyle.Regular)
        txtAnswer.BackColor = Drawing.Color.White
        txtAnswer.ForeColor = Drawing.Color.Black

        ' Listbox styling
        lstStatistics.Font = New Drawing.Font("Consolas", 10, Drawing.FontStyle.Regular)
        lstStatistics.BackColor = Drawing.Color.FromArgb(248, 248, 248)
    End Sub

    Private Sub ShowMainMenu()
        pnlMainMenu.Visible = True
        pnlQuiz.Visible = False
        pnlSetup.Visible = False
        pnlStatistics.Visible = False
        lblTitle.Text = "Mathematics Quiz"
    End Sub

    Private Sub ShowQuizPanel()
        pnlMainMenu.Visible = False
        pnlQuiz.Visible = True
        pnlSetup.Visible = False
        pnlStatistics.Visible = False
    End Sub

    Private Sub ShowSetupPanel()
        pnlMainMenu.Visible = False
        pnlQuiz.Visible = False
        pnlSetup.Visible = True
        pnlStatistics.Visible = False
    End Sub

    Private Sub ShowStatistics()
        pnlMainMenu.Visible = False
        pnlQuiz.Visible = False
        pnlSetup.Visible = False
        pnlStatistics.Visible = True

        ' Display statistics
        lstStatistics.Items.Clear()
        lstStatistics.Items.Add("STATISTICS")
        lstStatistics.Items.Add("====================")
        lstStatistics.Items.Add($"Attempts:   {attempts}")
        lstStatistics.Items.Add($"Correct:    {correctAnswers}")
        lstStatistics.Items.Add($"Failed:     {failedQuestions}")
        lstStatistics.Items.Add("")
        lstStatistics.Items.Add("Thank you for participating!")
    End Sub

    ' Event Handlers for Basic Operations
    Private Sub btnAddition_Click(sender As Object, e As EventArgs) Handles btnAddition.Click
        ShowBasicSetup("Addition")
    End Sub

    Private Sub btnSubtraction_Click(sender As Object, e As EventArgs) Handles btnSubtraction.Click
        ShowBasicSetup("Subtraction")
    End Sub

    Private Sub btnDivision_Click(sender As Object, e As EventArgs) Handles btnDivision.Click
        ShowBasicSetup("Division")
    End Sub

    Private Sub btnMultiplication_Click(sender As Object, e As EventArgs) Handles btnMultiplication.Click
        ShowBasicSetup("Multiplication")
    End Sub

    Private Sub btnAdvanced_Click(sender As Object, e As EventArgs) Handles btnAdvanced.Click
        ShowAdvancedSetup()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        If MessageBox.Show("Are you sure you want to exit?", "Exit Application",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Application.Exit()
        End If
    End Sub

    Private Sub btnStartBasic_Click(sender As Object, e As EventArgs) Handles btnStartBasic.Click
        StartBasicQuiz(currentOperation)
    End Sub

    Private Sub btnStartAdvanced_Click(sender As Object, e As EventArgs) Handles btnStartAdvanced.Click
        StartAdvancedQuiz()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ShowMainMenu()
    End Sub

    Private Sub btnBackToMenu_Click(sender As Object, e As EventArgs) Handles btnBackToMenu.Click
        ShowMainMenu()
    End Sub

    ' Setup Methods
    Private Sub ShowBasicSetup(operation As String)
        currentQuizType = "Basic"
        currentOperation = operation

        lblSetupTitle.Text = $"You selected {operation}." & vbCrLf & "This quiz will have 5 questions."
        btnStartBasic.Visible = True
        btnStartAdvanced.Visible = False

        ShowSetupPanel()
    End Sub

    Private Sub ShowAdvancedSetup()
        currentQuizType = "Advanced"

        lblSetupTitle.Text = "You selected Advanced Mathematics Quiz." & vbCrLf & "This quiz will have 5 questions."
        btnStartBasic.Visible = False
        btnStartAdvanced.Visible = True

        ShowSetupPanel()
    End Sub

    ' Quiz Logic Methods
    Private Sub StartBasicQuiz(operation As String)
        totalQuestions = 5 ' Fixed to 5 questions for all operations
        currentQuestionIndex = 0
        attempts = 0
        correctAnswers = 0
        failedQuestions = 0

        lblTitle.Text = $"{operation} Quiz - In Progress"
        ShowQuizPanel()
        ShowNextBasicQuestion(operation)
    End Sub

    Private Sub StartAdvancedQuiz()
        totalQuestions = 5 ' Fixed to 5 questions for advanced quiz
        currentQuestionIndex = 0
        attempts = 0
        correctAnswers = 0
        failedQuestions = 0

        lblTitle.Text = "Advanced Mathematics Quiz - In Progress"
        ShowQuizPanel()
        ShowNextAdvancedQuestion()
    End Sub

    Private Sub ShowNextBasicQuestion(operation As String)
        If currentQuestionIndex < totalQuestions Then
            Dim num1, num2 As Integer
            Select Case operation
                Case "Addition"
                    num1 = random.Next(1, 50)
                    num2 = random.Next(1, 50)
                    currentQuestionAnswer = num1 + num2
                    lblQuestion.Text = $"(Question {currentQuestionIndex + 1}): {num1} + {num2} = "

                Case "Subtraction"
                    num1 = random.Next(1, 100)
                    num2 = random.Next(1, num1)
                    currentQuestionAnswer = num1 - num2
                    lblQuestion.Text = $"(Question {currentQuestionIndex + 1}): {num1} - {num2} = "

                Case "Multiplication"
                    num1 = random.Next(1, 12)
                    num2 = random.Next(1, 12)
                    currentQuestionAnswer = num1 * num2
                    lblQuestion.Text = $"(Question {currentQuestionIndex + 1}): {num1} × {num2} = "

                Case "Division"
                    num2 = random.Next(1, 12)
                    num1 = num2 * random.Next(1, 12) ' Ensure divisible
                    currentQuestionAnswer = num1 / num2
                    lblQuestion.Text = $"(Question {currentQuestionIndex + 1}): {num1} ÷ {num2} = "
            End Select

            currentChances = 3
            txtAnswer.Text = ""
            txtAnswer.Focus()
        Else
            ShowStatistics()
        End If
    End Sub

    Private Sub ShowNextAdvancedQuestion()
        If currentQuestionIndex < totalQuestions Then
            Dim questionIndex As Integer = random.Next(0, advancedQuestions.Count)
            lblQuestion.Text = $"(Question {currentQuestionIndex + 1}): {advancedQuestions(questionIndex)} = "
            currentQuestionAnswer = advancedAnswers(questionIndex)
            currentChances = 3
            txtAnswer.Text = ""
            txtAnswer.Focus()
        Else
            ShowStatistics()
        End If
    End Sub

    Private Sub btnSubmitAnswer_Click(sender As Object, e As EventArgs) Handles btnSubmitAnswer.Click
        If String.IsNullOrWhiteSpace(txtAnswer.Text) Then
            MessageBox.Show("Please enter an answer.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim userAnswer As Double
        If Double.TryParse(txtAnswer.Text, userAnswer) Then
            attempts += 1
            CheckAnswer(userAnswer)
        Else
            MessageBox.Show("Please enter a valid number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtAnswer.SelectAll()
            txtAnswer.Focus()
        End If
    End Sub

    Private Sub CheckAnswer(userAnswer As Double)
        ' Allow small rounding differences for floating point calculations
        If Math.Abs(userAnswer - currentQuestionAnswer) < 0.1 Then
            ' Correct answer
            correctAnswers += 1
            currentQuestionIndex += 1
            MessageBox.Show("The answer is correct.", "Correct!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ShowNextQuestion()
        Else
            ' Wrong answer
            currentChances -= 1
            If currentChances > 0 Then
                MessageBox.Show($"The answer is wrong. Try again. You have {currentChances} chances remaining.",
                              "Incorrect", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtAnswer.SelectAll()
                txtAnswer.Focus()
            Else
                MessageBox.Show($"Correct answer is:" & vbCrLf & $"{lblQuestion.Text} {currentQuestionAnswer}",
                              "Out of Chances", MessageBoxButtons.OK, MessageBoxIcon.Information)
                failedQuestions += 1
                currentQuestionIndex += 1
                ShowNextQuestion()
            End If
        End If
    End Sub

    Private Sub ShowNextQuestion()
        If currentQuizType = "Advanced" Then
            ShowNextAdvancedQuestion()
        Else
            ' For basic operations, we need to determine which operation to continue with
            If currentQuestionIndex < totalQuestions Then
                ShowNextBasicQuestion(currentOperation)
            Else
                ShowStatistics()
            End If
        End If
    End Sub

    Private Sub txtAnswer_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAnswer.KeyPress
        If e.KeyChar = ChrW(Keys.Enter) Then
            btnSubmitAnswer.PerformClick()
            e.Handled = True
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
