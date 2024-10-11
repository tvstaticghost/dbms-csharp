using System.Runtime.InteropServices;

class Program {
    static void Main(string[] args) {
        Dictionary<string, string[]> tableData = [];
        Dictionary<string, string[]> practiceRows = [];

        tableData["employee_id"] = ["varchar", "PK"];
        tableData["first_name"] = ["varchar", ""];
        tableData["age"] = ["integer", ""];

        //Console.Write(tableData.FirstOrDefault().Value.Length);

        DBSystem sys = new();
        // sys.CreateTable();
        // sys.PrintTableList();
        // sys.DescribeTable();

        sys.CreateTable();
        sys.DescribeTable();
        sys.AddRowsToTable();
        sys.SelectAllFromTable();

        // practiceRows["first_name"] = ["Ray", "Sofia", "Otto"];

        // string userInput = Helper.GetUserInput("add column to first_name field? [y]es or [n]o").ToLower();
        // while (userInput == "yes" || userInput == "y") {
        //     string rowToAppend = Helper.GetVarchar($"Please enter a varchar value for the first_name column");

        //     string[] currentArray = practiceRows["first_name"];
        //     string[] newArray = new string[currentArray.Length + 1];

        //     Array.Copy(currentArray, newArray, currentArray.Length);
        //     newArray[newArray.Length - 1] = rowToAppend;

        //     practiceRows["first_name"] = newArray;

        //     userInput = Helper.GetUserInput("add column to first_name field? [y]es or [n]o").ToLower();
        // }


        // foreach (KeyValuePair<string, string[]> row in practiceRows) {
        //     System.Console.WriteLine(row.Key);
        //     for (int i = 0; i < row.Value.Length; i++) {
        //         System.Console.WriteLine(row.Value[i]);
        //     }
        // }
    }
}