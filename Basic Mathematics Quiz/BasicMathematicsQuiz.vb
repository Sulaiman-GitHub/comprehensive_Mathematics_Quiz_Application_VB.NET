Imports System
Imports System.Collections.Generic
Imports System.IO

'Group Members
'NAME: BWAMBALE SULAIT
'REG:   2024-04-26358    
'PROGRAM: BCS

'NAME: SHARIF SSEBUGUZI
'REG:   2024-04-25983    
'PROGRAM: BCS

Module Module1
    ' Quiz data
    Private advancedQuestions As New List(Of String)()
    Private advancedAnswers As New List(Of Double)()
    Private totalQuestions As Integer = 0
    Private attempts As Integer = 0
    Private correctAnswers As Integer = 0
    Private failedQuestions As Integer = 0
    Private currentChances As Integer = 3
    Private currentQuestionAnswer As Double = 0
    Private CurrentOperation As String = ""
    Private random As New Random()
    Private quizStartTime As DateTime
    Private quizEndTime As DateTime

    Sub Main()
        InitializeAdvancedQuestions()

        While True
            ShowMainMenu()
            Dim choice As String = GetUserChoice()

            Select Case choice
                Case "1"
                    StartBasicQuiz("Addition")
                Case "2"
                    StartBasicQuiz("Subtraction")
                Case "3"
                    StartBasicQuiz("Division")
                Case "4"
                    StartBasicQuiz("Multiplication")
                Case "5"
                    StartAdvancedQuiz()
                Case "0"
                    ExitApplication()
                    Exit While
                Case Else
                    Console.WriteLine("Invalid choice. Please try again.")
                    Console.WriteLine()
            End Select
        End While
    End Sub

    Private Sub ShowMainMenu()
        Console.WriteLine("Basic Mathematics Quiz")
        Console.WriteLine("1. Addition")
        Console.WriteLine("2. Subtraction")
        Console.WriteLine("3. Division")
        Console.WriteLine("4. Multiplication")
        Console.WriteLine("5. Advanced Mathematics Quiz")
        Console.WriteLine("0. Exit")
        Console.Write("Pick a choice to start: ")
    End Sub

    Private Function GetUserChoice() As String
        Return Console.ReadLine()
    End Function

    Private Function GetNumberOfQuestions() As Integer
        Console.Write("Set the number of questions: ")
        Dim input As String = Console.ReadLine()
        Dim number As Integer

        While Not Integer.TryParse(input, number) OrElse number <= 0 OrElse number > 50
            Console.Write("Please enter a valid number (1-50): ")
            input = Console.ReadLine()
        End While

        Return number
    End Function

    Private Sub StartBasicQuiz(operation As String)
        CurrentOperation = operation
        Console.WriteLine()
        Console.WriteLine($"You selected {operation}.")
        totalQuestions = GetNumberOfQuestions()
        Console.WriteLine()

        attempts = 0
        correctAnswers = 0
        failedQuestions = 0
        quizStartTime = DateTime.Now

        For i As Integer = 1 To totalQuestions ' in this case "i" works as the "CurrentQuestionIndex" used to track number
            ShowNextBasicQuestion(operation, i)
        Next

        ShowStatistics()
    End Sub

    Private Sub StartAdvancedQuiz()
        CurrentOperation = "Advanced"
        Console.WriteLine()
        Console.WriteLine("You selected Advanced Mathematics Quiz.")
        totalQuestions = GetNumberOfQuestions()
        Console.WriteLine()

        attempts = 0
        correctAnswers = 0
        failedQuestions = 0
        quizStartTime = DateTime.Now

        For i As Integer = 1 To totalQuestions
            ShowNextAdvancedQuestion(i)
        Next

        ShowStatistics()
    End Sub

    Private Sub ShowNextBasicQuestion(operation As String, questionNumber As Integer)
        Dim num1, num2 As Integer
        Dim operatorSymbol As String = ""

        Select Case operation
            Case "Addition"
                num1 = random.Next(1, 50)
                num2 = random.Next(1, 50)
                currentQuestionAnswer = num1 + num2
                operatorSymbol = "+"

            Case "Subtraction"
                num1 = random.Next(1, 100)
                num2 = random.Next(1, num1)
                currentQuestionAnswer = num1 - num2
                operatorSymbol = "-"

            Case "Multiplication"
                num1 = random.Next(1, 12)
                num2 = random.Next(1, 12)
                currentQuestionAnswer = num1 * num2
                operatorSymbol = "×"

            Case "Division"
                num2 = random.Next(1, 12)
                num1 = num2 * random.Next(1, 12) ' Ensure divisible
                currentQuestionAnswer = num1 / num2
                operatorSymbol = "÷"
        End Select

        currentChances = 3
        AskQuestion($"(Question {questionNumber}): {num1} {operatorSymbol} {num2} = ")
    End Sub

    Private Sub ShowNextAdvancedQuestion(questionNumber As Integer)
        Dim questionIndex As Integer = random.Next(0, advancedQuestions.Count)
        currentQuestionAnswer = advancedAnswers(questionIndex)
        currentChances = 3
        AskQuestion($"(Question {questionNumber}): {advancedQuestions(questionIndex)} = ")
    End Sub

    Private Sub AskQuestion(questionText As String)
        Console.Write(questionText)

        While currentChances > 0
            Dim userInput As String = Console.ReadLine()
            Dim userAnswer As Double

            If Double.TryParse(userInput, userAnswer) Then
                attempts += 1

                If Math.Abs(userAnswer - currentQuestionAnswer) < 0.1 Then
                    Console.WriteLine("The answer is correct.")
                    Console.WriteLine()
                    correctAnswers += 1
                    Exit While
                Else
                    currentChances -= 1
                    If currentChances > 0 Then
                        Console.WriteLine($"The answer is wrong. Try again. You have {currentChances} chances remaining.")
                        Console.Write(questionText)
                    Else
                        Console.WriteLine($"The answer is wrong. Try again. You have {currentChances} chances remaining.")
                        Console.WriteLine($"Correct answer is:")
                        Console.WriteLine($"{questionText} {currentQuestionAnswer}")
                        Console.WriteLine()
                        failedQuestions += 1
                    End If
                End If
            Else
                currentChances -= 1
                If currentChances > 0 Then
                    Console.WriteLine("Please enter a valid number. Try again.")
                    Console.Write(questionText)
                Else
                    Console.WriteLine("Please enter a valid number.")
                    Console.WriteLine($"Correct answer is:")
                    Console.WriteLine($"{questionText} {currentQuestionAnswer}")
                    Console.WriteLine()
                    failedQuestions += 1
                End If
            End If
        End While
    End Sub

    Private Sub ShowStatistics()
        quizEndTime = DateTime.Now

        Console.WriteLine("STATISTICS")
        Console.WriteLine("====================")
        Console.WriteLine($"Type Of Quiz:   {CurrentOperation}")
        Console.WriteLine($"Number of Questions Attempted: {totalQuestions}")
        Console.WriteLine($"Attempts:   {attempts}")
        Console.WriteLine($"Correct:    {correctAnswers}")
        Console.WriteLine($"Failed:     {failedQuestions}")
        Console.WriteLine($"Start Time: {quizStartTime:yyyy-MM-dd HH:mm:ss}")
        Console.WriteLine($"End Time:   {quizEndTime:yyyy-MM-dd HH:mm:ss}")
        Console.WriteLine()
        Console.WriteLine("Thank you for participating!")
        Console.WriteLine()

        ' Save statistics to file
        SaveStatisticsToFile()

        Console.WriteLine("Press any key to return to main menu...")
        Console.ReadKey()
        Console.WriteLine()
    End Sub

    Private Sub SaveStatisticsToFile()
        Try
            Dim filename As String = $"QuizResults_{DateTime.Now:yyyyMMdd_HHmmss}.txt"
            Using writer As New StreamWriter(filename)
                writer.WriteLine("MATHEMATICS QUIZ RESULTS")
                writer.WriteLine("========================")
                writer.WriteLine($"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
                writer.WriteLine($"Type Of Quiz: {CurrentOperation}")
                writer.WriteLine($"Number of Questions: {totalQuestions}")
                writer.WriteLine()
                writer.WriteLine($"Total Attempts: {attempts}")
                writer.WriteLine($"Correct Answers: {correctAnswers}")
                writer.WriteLine($"Failed Questions: {failedQuestions}")
                writer.WriteLine($"Start Time: {quizStartTime:yyyy-MM-dd HH:mm:ss}")
                writer.WriteLine($"End Time: {quizEndTime:yyyy-MM-dd HH:mm:ss}")
                writer.WriteLine($"Duration: {quizEndTime - quizStartTime}")
                writer.WriteLine()
                writer.WriteLine("Thank you for participating!")
            End Using
            Console.WriteLine($"Statistics saved to {filename}")
            Console.WriteLine("in folder bin/Debug/" & vbCrLf)

        Catch ex As Exception
            Console.WriteLine("Error saving statistics to file: " & ex.Message)
        End Try
    End Sub

    Private Sub ExitApplication()
        Console.WriteLine()
        Console.WriteLine("Thank you for using the Mathematics Quiz Application!")
        Console.WriteLine("Goodbye!")
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
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
End Module