using System.Runtime.InteropServices;

class Program {
    static void Main(string[] args) {
        Dictionary<string, string[]> tableData = [];
        Dictionary<string, string[]> practiceRows = [];

        tableData["employee_id"] = ["varchar", "PK"];
        tableData["first_name"] = ["varchar", ""];
        tableData["age"] = ["integer", ""];

        practiceRows["employee_id"] = ["1" , "2", "3"];
        practiceRows["first_name"] = ["Ray" , "Sofia", "Bob"];
        practiceRows["age"] = ["29" , "28", "30"];

        string parameter = "So";
        string selectedRow = "first_name";
        List<int> indexesToExclude = [];

        for (int i = 0; i < practiceRows["first_name"].Length; i++) {
            System.Console.WriteLine(practiceRows[selectedRow][i]);
            if (!practiceRows[selectedRow][i].Contains(parameter)) {
                indexesToExclude.Add(i);
            }
        }

        Dictionary<string, List<string>> newRows = [];

        foreach (var key in practiceRows.Keys) {
            newRows[key] = new List<string>();
        }

        foreach (var key in practiceRows.Keys) {
            for (int i = 0 ; i < practiceRows[key].Length; i++) {
                if (!indexesToExclude.Contains(i)) {
                    newRows[key].Add(practiceRows[key][i]);
                }
            }
        }


        //test
        foreach (var key in newRows.Keys) {
            Console.Write(key + " - ");
            for (int i = 0; i < newRows[key].Count; i++) {
                Console.Write(newRows[key][i] + " ");
            }
            System.Console.WriteLine("");
        }

        // DBSystem sys = new();
        // UserInterface ui = new(sys);
        // ui.Run();
    }
}