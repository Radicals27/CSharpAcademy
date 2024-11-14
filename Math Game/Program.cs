
bool userIsPlaying = true;
string userInput;
List<string> previousQuestions = new List<string>();
MathQuestion currentQuestionType = MathQuestion.ADDITION;

// Numbers
Random random = new Random();
int randomNumber1;
int randomNumber2;
int numberMin = 1;
int numberMax = 10;
int correctAnswer = 0;

GameLoop();

void GameLoop()
{
    do
    {
        DisplayMenu();
        ReadUserInput();
        HandleUserInput();
    }
    while (userIsPlaying);
}

void DisplayMenu()
{
    Console.Clear();
    Console.WriteLine("Welcome to the Math Game! Choose an option:");
    Console.WriteLine("1. Addition");
    Console.WriteLine("2. Subtraction");
    Console.WriteLine("3. Multiplication");
    Console.WriteLine("4. Division");
    Console.WriteLine("0. Show previous questions");
}

void ReadUserInput()
{
    userInput = Console.ReadLine();
}

void HandleUserInput()
{
    if (userInput == null)
    {
        return;
    }

    if (userInput.Length > 1)
    {
        return;
    }

    if (char.IsDigit(userInput[0]))
    {
        int digit = int.Parse(userInput);

        switch (digit)
        {
            case 1:
                currentQuestionType = MathQuestion.ADDITION;
                break;
            case 2:
                currentQuestionType = MathQuestion.SUBTRACTION;
                break;
            case 3:
                currentQuestionType = MathQuestion.MULTIPLICATION;
                break;
            case 4:
                currentQuestionType = MathQuestion.DIVISION;
                break;
            case 0:
                currentQuestionType = MathQuestion.NONE;
                break;
            default:
                Console.WriteLine($"You entered an invalid digit, try again...");
                Console.ReadKey();
                break;
        }

        ShowQuestion(currentQuestionType);
    }
    else
    {
        Console.WriteLine($"You entered an invalid digit, try again...");
        Console.ReadKey();
    }
}

void ShowQuestion(MathQuestion questionType)
{
    randomNumber1 = random.Next(numberMin, numberMax + 1);
    randomNumber2 = random.Next(numberMin, numberMax + 1);
    string mathOperator = "";

    switch (questionType)
    {
        case MathQuestion.ADDITION:
            correctAnswer = randomNumber1 + randomNumber2;
            mathOperator = "+";
            break;
        case MathQuestion.SUBTRACTION:
            correctAnswer = randomNumber1 - randomNumber2;
            mathOperator = "-";
            break;
        case MathQuestion.MULTIPLICATION:
            correctAnswer = randomNumber1 * randomNumber2;
            mathOperator = "x";
            break;
        case MathQuestion.DIVISION:
            correctAnswer = randomNumber1 / randomNumber2;
            mathOperator = "/";
            break;
        case MathQuestion.NONE:
            ShowPreviousQuestions();
            return;
    }

    Console.Clear();
    string question = $"What is {randomNumber1} {mathOperator} {randomNumber2}?";
    Console.WriteLine(question);
    ReadUserInput();
    CheckAnswer(userInput, correctAnswer);
    SaveQuestion($"{question}.  You entered: {userInput}");
}

void CheckAnswer(string userInput, int correctAnswer)
{
    if (userInput == null)
    {
        Console.WriteLine($"Your input was not a valid number, try again...");
        Console.ReadKey();
        return;
    }

    if (int.TryParse(userInput, out int intValue))
    {
        if (intValue == correctAnswer)
        {
            Console.WriteLine($"Correct! Press any key to return to the main menu.");
        }
        else
        {
            Console.WriteLine($"Incorrect.  Press any key to return to the main menu.");
        }
    }
    else if (double.TryParse(userInput, out double doubleValue))
    {
        if (intValue == correctAnswer)
        {
            Console.WriteLine($"Correct! Press any key to return to the main menu.");
        }
        else
        {
            Console.WriteLine($"Incorrect.  Press any key to return to the main menu.");
        }
    }

    Console.ReadKey();
    return;
}

void SaveQuestion(string question)
{
    previousQuestions.Add(question);
}

void ShowPreviousQuestions()
{
    Console.Clear();

    foreach (string question in previousQuestions)
    {
        Console.WriteLine(question);
    }

    Console.WriteLine("Press any key to return to the main menu.");
    Console.ReadKey();
    return;
}

enum MathQuestion
{
    NONE,
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION,
}