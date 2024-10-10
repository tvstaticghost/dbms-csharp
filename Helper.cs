using System.Text.RegularExpressions;

public static class Helper {
    public static string GetUserInput(string message) {
        Console.WriteLine(message);
        string userInput = Console.ReadLine();

        while (userInput == "" || userInput == null) {
            Console.WriteLine("Invalid Input");
            userInput = Console.ReadLine();
        }

        Regex r = new("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        return r.Replace(userInput, string.Empty);
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
        foreach(KeyValuePair<string, string[]> col in cols) {
            Console.WriteLine(col.Key.PadRight(10) + "|".PadRight(5) + col.Value[0].PadRight(10) + "|".PadRight(5) + col.Value[1].PadRight(10));
        }
        Console.WriteLine("+".PadRight(40, '-') + '+');
    }
}