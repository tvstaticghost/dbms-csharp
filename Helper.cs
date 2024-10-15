using System.ComponentModel;
using System.Data;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;
using System.Threading.Channels;

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

    public static bool GetYesOrNo(string message) {
        Console.WriteLine(message + ": [y]es or [n]o");
        string userInput = Console.ReadLine().ToLower();

        while (userInput != "y" && userInput != "yes" && userInput != "n" && userInput != "no") {
            Console.WriteLine("Invalid input...");
            userInput = Console.ReadLine().ToLower();
        }

        if (userInput == "y" || userInput == "yes") {
            return true;
        }
        else {
            return false;
        }
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
                if (userInput[i] < '0' || userInput[i] > '9') { allNumbers = false; break; }
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

    public static string GetDateTime(string message) {
        string userInput = GetUserInput(message);
        bool correctFormat;

        //check for a YYYY-MM-DD HH:MI:SS format
        while(true) {
            correctFormat = true;

            if (userInput.Length == 19) {
                for (int i = 0; i < userInput.Length; i++) {
                    if ((i == 4 || i == 7) && userInput[i] != '-') {
                        correctFormat = false;
                        break;
                    }

                    if (i != 4 && i != 7 && i != 10 && i != 13 && i != 16 && (userInput[i] < '0' || userInput[i] > '9')) {
                        correctFormat = false;
                        break;
                    }

                    if ((i == 10) && userInput[i] != ' ') {
                        correctFormat = false;
                        break;
                    }

                    if ((i == 13 || i == 16) && userInput[i] != ':') {
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
        Console.WriteLine("-".PadRight(40, '-') + '-');

        if (cols.Count <= 0) { Console.WriteLine("No columns in table..."); }
        else {
            foreach(KeyValuePair<string, string[]> col in cols) {
                Console.WriteLine(col.Key.PadRight(10) + "|".PadRight(5) + col.Value[0].PadRight(10) + "|".PadRight(5) + col.Value[1].PadRight(10));
            }
        }
        Console.WriteLine("+".PadRight(40, '-') + '+');
    }

    public static void SelectFromTable(string tableName, Dictionary<string, string[]> tableCols, Dictionary<string, string[]> tableRows) {
        Console.WriteLine($"\n{tableName}");
        Console.WriteLine("+".PadRight(40, '-') + '+');
        foreach (KeyValuePair<string, string[]> col in tableCols) {
            Console.Write($"{col.Key}".PadRight(10) + "|".PadRight(5));
        }
        Console.WriteLine("");
        Console.WriteLine("-".PadRight(40, '-') + '-');

        bool noRows = true;
        for (int i = 0; i < tableRows.FirstOrDefault().Value.Length; i++) {
            noRows = false;
            foreach(var key in tableRows.Keys) {
                Console.Write($"{tableRows[key][i]}".PadRight(10) + "|".PadRight(5));
            }
            Console.WriteLine("");
        }

        if (noRows) {Console.WriteLine("No rows added to table...");}
        Console.WriteLine("+".PadRight(40, '-') + '+');
    }

    public static void SelectSpecificFromTable(string tableName, Dictionary<string, string[]> tableCols, Dictionary<string, string[]> tableRows) {
        List<string> selectedColumns = [];
        List<string> columnCopy = [];

        foreach (var key in tableCols.Keys) {
            columnCopy.Add(key);
        }

        do {
            string colOptions = "(";
            foreach (string col in columnCopy) {
                colOptions += col + " ";
            }
            colOptions = colOptions.TrimEnd();
            colOptions += ")";

            string userColumnSelection = GetUserInput($"Which column in {tableName} do you want to select?\n{colOptions}");

            if (columnCopy.Contains(userColumnSelection)) {
                selectedColumns.Add(userColumnSelection);
                columnCopy.Remove(userColumnSelection);

                if (columnCopy.Count >= 1) {
                    bool anotherRow = GetYesOrNo("Select data from another row? ");
                    if (!anotherRow) {
                        break;
                    }
                }
                else {
                    break;
                }
            }

        } while (true);

        //shows all selected columns to query in the selectedCols list. 
        //ADD functionality to select data based on 1 or more selected columns.

        //create a copy of tableRows and tableCols Dictionaries to pass to SelectFromTable function to print out the info

        Dictionary<string, string[]> selectedCols = [];
        Dictionary<string, string[]> selectedRows = [];

        //populate the selectedCols and selectedRows Dictionaries with the selected Column Keys
        foreach (string item in selectedColumns) {
            selectedCols[item] = [];
            selectedRows[item] = [];
        }

        foreach (KeyValuePair<string, string[]> col in tableCols) {
            foreach (KeyValuePair<string, string[]> newCol in selectedCols) {
                if (col.Key == newCol.Key) {
                    selectedCols[col.Key] = col.Value;
                }
            }
        }

        //copy over the row array if the dictionary keys match between the tableRows and the selectedRows
        foreach (KeyValuePair<string, string[]> row in tableRows) {
            foreach (KeyValuePair<string, string[]> otherRow in selectedRows) {
                if (row.Key == otherRow.Key) {
                    selectedRows[row.Key] = row.Value;
                }
            }
        }

        bool filterResults = GetYesOrNo("Would you like to filter the column results? ");

        if (filterResults) {
            FilterSpecificColumns(tableName, selectedCols, selectedRows);
        }
        else {
            //print the specific rows of a table
            SelectFromTable(tableName, selectedCols, selectedRows);
        }
    }

    public static void FilterSpecificColumns(string tableName, Dictionary<string, string[]> tableCols, Dictionary<string, string[]> tableRows) {
        List<string> filterOptions = [];

        foreach (var key in tableCols.Keys) {
            filterOptions.Add(key);
        }

        if (filterOptions.Count > 1) {
            string message = "Which column would you like to filter by? (";
            for (int i = 0; i < filterOptions.Count; i++) {
                if (i < filterOptions.Count - 1) {
                    message += filterOptions[i] + ", ";
                }
                else {
                    message += filterOptions[i];
                }
            }
            message += ")";
        }
        
    }

    public static void AddRows(Dictionary<string, string[]> columns, Dictionary<string, string[]> rows) {
        string continueDecision;
        do {
            foreach(KeyValuePair<string, string[]> col in columns) {
                string newValue;
                string message = $"Please enter a {col.Value[0]} value for the {col.Key} column";
                if (col.Value[0] == "varchar") {
                    newValue = GetVarchar(message);
                    AppendRowValue(newValue, col.Key, rows);
                }
                else if (col.Value[0] == "int") {
                    newValue = GetInt(message);
                    AppendRowValue(newValue, col.Key, rows);
                }
                else if (col.Value[0] == "date") {
                    newValue = GetDate(message + " (YYYY-MM-DD format):");
                    AppendRowValue(newValue, col.Key, rows);
                }
                //ADD A DATETIME HELPER FUNCTION AND AN OPTION TO VALIDATE
                else if (col.Value[0] == "datetime") {
                    newValue = GetDateTime(message + " (YYYY-MM-DD HH:MI:SS format):");
                    AppendRowValue(newValue, col.Key, rows);
                }
            }

            continueDecision = GetUserInput("Add another row? [y]es or [n]o").ToLower();
            while (continueDecision != "y" && continueDecision != "yes" && continueDecision != "n" && continueDecision != "no") {
                Console.WriteLine("Invalid Selection");
                continueDecision = GetUserInput("Add another row? [y]es or [n]o").ToLower();
            }
        } while (continueDecision == "y" || continueDecision == "yes");
        
    }

    public static void AppendRowValue(string valueToAppend, string columnToAppendTo, Dictionary<string, string[]> rows) {
        string[] currentRow = rows[columnToAppendTo];
        string[] newRow = new string[currentRow.Length + 1];

        Array.Copy(currentRow, newRow, currentRow.Length);
        newRow[newRow.Length - 1] = valueToAppend;

        rows[columnToAppendTo] = newRow;
    }
}