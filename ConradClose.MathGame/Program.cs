
bool userIsPlaying = true;
string userInput;
List<string> previousQuestions = new List<string>();
MenuOptions currentQuestionType = MenuOptions.ADDITION;

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
    Console.WriteLine("9. Show previous questions");
    Console.WriteLine("0. Quit");
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
                currentQuestionType = MenuOptions.ADDITION;
                break;
            case 2:
                currentQuestionType = MenuOptions.SUBTRACTION;
                break;
            case 3:
                currentQuestionType = MenuOptions.MULTIPLICATION;
                break;
            case 4:
                currentQuestionType = MenuOptions.DIVISION;
                break;
            case 9:
                currentQuestionType = MenuOptions.NONE;
                break;
            case 0:
                Environment.Exit(0);
                return;
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

void ShowQuestion(MenuOptions questionType)
{
    randomNumber1 = random.Next(numberMin, numberMax + 1);
    randomNumber2 = random.Next(numberMin, numberMax + 1);
    string mathOperator = "";

    switch (questionType)
    {
        case MenuOptions.ADDITION:
            correctAnswer = randomNumber1 + randomNumber2;
            mathOperator = "+";
            break;
        case MenuOptions.SUBTRACTION:
            correctAnswer = randomNumber1 - randomNumber2;
            mathOperator = "-";
            break;
        case MenuOptions.MULTIPLICATION:
            correctAnswer = randomNumber1 * randomNumber2;
            mathOperator = "x";
            break;
        case MenuOptions.DIVISION:
            GenerateDivisionNumbers();
            correctAnswer = randomNumber1 / randomNumber2;
            mathOperator = "/";
            break;
        case MenuOptions.NONE:
            ShowPreviousQuestions();
            return;
    }

    Console.Clear();
    string question = $"What is {randomNumber1} {mathOperator} {randomNumber2}?";
    Console.WriteLine(question);
    ReadUserInput();
    ReportResult(question, userInput, correctAnswer);
}

void GenerateDivisionNumbers()
{
    bool foundValidNumbers = false;
    randomNumber1 = random.Next(1, 101);

    do
    {
        randomNumber2 = random.Next(1, 11);

        if (randomNumber1 % randomNumber2 == 0)
        {
            foundValidNumbers = true;
        }
    }
    while (foundValidNumbers == false);
}

void ReportResult(string question, string userInput, int correctAnswer)
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
            SaveQuestion($"{question}.  You entered: {userInput}, which was CORRECT.");
        }
        else
        {
            Console.WriteLine($"Incorrect.  Press any key to return to the main menu.");
            SaveQuestion($"{question}.  You entered: {userInput}, which was INCORRECT ({correctAnswer}).");
        }
    }
    else
    {
        Console.WriteLine($"Your answer was invalid. Press any key to return to the main menu.");
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

enum MenuOptions
{
    NONE,
    ADDITION,
    SUBTRACTION,
    MULTIPLICATION,
    DIVISION,
    QUIT,
}