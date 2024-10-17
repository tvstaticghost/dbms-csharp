using System.Runtime.InteropServices;

class Program {
    static void Main(string[] args) {
        // Dictionary<string, string[]> tableData = new Dictionary<string, string[]>();
        // Dictionary<string, string[]> practiceRows =  new Dictionary<string, string[]>();

        // tableData["employee_id"] = ["varchar", "PK"];
        // tableData["first_name"] = ["varchar", ""];
        // tableData["age"] = ["integer", ""];

        // practiceRows["employee_id"] = ["1" , "2", "3"];
        // practiceRows["first_name"] = ["Ray" , "Sofia", "roy"];
        // practiceRows["age"] = ["29" , "28", "30"];

        // string parameter = "y";
        // string selectedRow = "first_name";
        // List<int> indexesToExclude = new List<int>();

        // for (int i = 0; i < practiceRows[selectedRow].Length; i++) {
        //     if (parameter.Length > practiceRows[selectedRow][i].Length) {
        //         indexesToExclude.Add(i);
        //     }
        //     else {
        //         bool endsWith = true;
        //         for (int j = 0; j < parameter.Length; j++) {
        //             if (parameter[parameter.Length - (1 + j)] != practiceRows[selectedRow][i][practiceRows[selectedRow][i].Length - (1 + j)]) {endsWith = false;}
        //         }
        //         if (!endsWith) {indexesToExclude.Add(i);}
        //     }
        // }

        // Dictionary<string, List<string>> newRows =  new Dictionary<string, List<string>>();

        // foreach (var key in practiceRows.Keys) {
        //     newRows[key] = new List<string>();
        // }

        // foreach (var key in practiceRows.Keys) {
        //     for (int i = 0 ; i < practiceRows[key].Length; i++) {
        //         if (!indexesToExclude.Contains(i)) {
        //             newRows[key].Add(practiceRows[key][i]);
        //         }
        //     }
        // }


        // //test
        // foreach (var key in newRows.Keys) {
        //     Console.Write(key + " - ");
        //     for (int i = 0; i < newRows[key].Count; i++) {
        //         Console.Write(newRows[key][i] + " ");
        //     }
        //     System.Console.WriteLine("");
        // }

        DBSystem sys = new();
        UserInterface ui = new(sys);
        ui.Run();
    }
}