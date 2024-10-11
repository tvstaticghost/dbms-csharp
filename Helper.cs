using System.Text.RegularExpressions;

public static class Helper {
    public static string GetUserInput(string message)
    {
        Console.WriteLine(message);
        string userInput = Console.ReadLine();

        while (string.IsNullOrEmpty(userInput)) {
            Console.WriteLine("Invalid Input");
            Console.WriteLine(message); // Re-prompt with the original message
            userInput = Console.ReadLine();
        }

        Regex r = new Regex(@"(?:[^a-z0-9 \-_:]|(?<=['""])\s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        
        // Return sanitized input
        return r.Replace(userInput, string.Empty);
    }


    public static string GetVarchar(string message) {
        string userInput = GetUserInput(message);
        bool allLetters;

        while (true) {
            allLetters = true;
            for (int i = 0; i < userInput.Length; i++) {
                if (userInput[i] >= 48 && userInput[i] <= 57) { allLetters = false; break; }
            }

            if (allLetters) {break;}
            Console.WriteLine("Invalid input...");
            userInput = GetUserInput(message);
        }

        return userInput;
    }

    public static string GetInt(string message) {
        string userInput = GetUserInput(message);
        bool allNumbers;

        while (true) {
            allNumbers = true;
            for (int i = 0; i < userInput.Length; i++) {
                if (userInput[i] >= 57 && userInput[i] >= 48) { allNumbers = false; break; }
            }

            if (allNumbers) {break;}
            Console.WriteLine("Invalid input...");
            userInput = GetUserInput(message);
        }

        return userInput;
    }

    public static string GetDate(string message) {
        string userInput = GetUserInput(message);
        bool correctFormat;

        //check the date format YYYY-MM-DD
        while (true) {
            correctFormat = true;
            if (userInput.Length == 10) {
                for (int i = 0; i < userInput.Length; i++) {
                    if ((i == 4 || i == 7) && userInput[i] != '-') {
                        correctFormat = false;
                        break;
                    }
                    if (i != 4 && i != 7 && (userInput[i] < '0' || userInput[i] > '9')) {
                        correctFormat = false;
                        break;
                    }
                }
            }

            if (correctFormat) {
                if (DateTime.TryParse(userInput, out _)) {
                    return userInput;
                }
                else {
                    Console.WriteLine("Invalid date.");
                }
            }
            userInput = GetUserInput(message);
        }
    }

    public static string GetColumnDataType(string columnName) {
        string message = "Please enter varchar, int, datetime, or date: ";
        Console.WriteLine($"Enter the data type of {columnName}");
        string dataType = GetUserInput(message).ToLower();

        while (dataType != "varchar" && dataType != "datetime" && dataType != "date" && dataType != "int") {
            Console.WriteLine("Invalid datatype...");
            dataType = GetUserInput(message).ToLower();
        }
        return dataType;
    }

    public static bool AddColumn(string columnAddMessage) {
        string userInput = GetUserInput(columnAddMessage).ToLower();
        
        while (userInput != "y" && userInput != "n" && userInput != "yes" && userInput != "no") {
            Console.WriteLine("Invalid input...");
            userInput = GetUserInput(columnAddMessage).ToLower();
        }

        if (userInput == "y" || userInput == "yes") {
            return true;
        }
        else {
            return false;
        }
    }

    public static string IsPrimaryKey(string columnName) {
        string message = "[y]es or [n]o";
        Console.WriteLine($"Is {columnName} the primary key?");

        string userInput = GetUserInput(message).ToLower();
        while (userInput != "y" && userInput != "n" && userInput != "yes" && userInput != "no") {
            Console.WriteLine("Invalid input...");
            userInput = GetUserInput(message).ToLower();
        }

        if (userInput == "y" || userInput == "yes") {
            return "pk";
        }
        else {
            return "";
        }
    }

//make format responsive 
    public static void FormatTableDescription(string tableName, Dictionary<string, string[]> cols) {
        Console.WriteLine($"\n{tableName}");
        Console.WriteLine("+".PadRight(40, '-') + '+');
        Console.WriteLine("Field".PadRight(10) + "|".PadRight(5) + "Type".PadRight(10) + "|".PadRight(5) + "Key".PadRight(10));
        Console.WriteLine("+".PadRight(40, '-') + '+');

        if (cols.Count <= 0) { Console.WriteLine("No columns in table..."); }
        else {
            foreach(KeyValuePair<string, string[]> col in cols) {
                Console.WriteLine(col.Key.PadRight(10) + "|".PadRight(5) + col.Value[0].PadRight(10) + "|".PadRight(5) + col.Value[1].PadRight(10));
            }
        }
        Console.WriteLine("+".PadRight(40, '-') + '+');
    }

    public static void AddRows(Dictionary<string, string[]> columns) {
        foreach(KeyValuePair<string, string[]> col in columns) {
            string message = $"Please enter a {col.Value[0]} value for the {col.Key} column";
            if (col.Value[0] == "varchar") {
                GetVarchar(message);
            }
            else if (col.Value[0] == "int") {
                GetInt(message);
            }
            else if (col.Value[0] == "date") {
                GetDate(message + " (YYYY-MM-DD format):");
            }
        }
    }
}