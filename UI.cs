using System.Runtime.CompilerServices;

class UserInterface(DBSystem sys)
{
private readonly string WelcomeMessage = "Welcome to SQLShite";
private readonly string ExitMessage = "Closing SQLShite...Goodbye";
private readonly List<string> MenuOptions = ["1: Create Table", "2: Add Rows to Table", "3: Select All Data From Table", "4: View Table List", "5: Describe Table", "Q: Quit SQLShite"];
private readonly DBSystem sys = sys;

    public void DisplayWelcomeMessage() {
        Console.WriteLine("+".PadRight(30, '-'));
        Console.WriteLine(WelcomeMessage);
        Console.WriteLine("+".PadLeft(30, '-'));
    }

    public void DisplayClosingMessage() {
        Console.WriteLine("+".PadRight(30, '-'));
        Console.WriteLine(ExitMessage);
        Console.WriteLine("+".PadLeft(30, '-'));
    }

    public void DisplayMenuOptions() {
        foreach (string option in MenuOptions) {
            Console.WriteLine(option);
        }
        Console.WriteLine("+".PadLeft(30, '-'));
    }

    public void Run() {
        DisplayWelcomeMessage();
        DisplayMenuOptions();
        string selectionMessage = $"Make a menu selection using ";
        bool running = true;

        string userInput = Helper.GetUserInput(selectionMessage);
        while (running) {
            switch(userInput.ToLower()) {
                case "1":
                    sys.CreateTable();
                    break;
                case "2":
                    sys.AddRowsToTable();
                    break;
                case "3":
                    sys.SelectAllFromTable();
                    break;
                case "4":
                    sys.PrintTableList();
                    break;
                case "5":
                    sys.DescribeTable();
                    break;
                case "q":
                    running = false;
                    break;
                case "m":
                    DisplayMenuOptions();
                    break;
                default:
                    Console.WriteLine("Invalid Menu Selection...");
                    break;
            }

            if (running == false) {
                DisplayClosingMessage();
                return;
            }

            userInput = Helper.GetUserInput(selectionMessage);
        }
    }
}