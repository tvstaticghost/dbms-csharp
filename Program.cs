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
        UserInterface ui = new(sys);
        ui.Run();

        // List<string> keyCopy = [];

        // foreach (var key in tableData.Keys) {
        //     keyCopy.Add(key);
        // }

        // keyCopy.Remove("employee_id");
    }
}