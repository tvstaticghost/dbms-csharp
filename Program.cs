class Program {
    static void Main(string[] args) {
        Dictionary<string, string[]> tableData = [];

        tableData["employee_id"] = ["varchar", "PK"];
        tableData["first_name"] = ["varchar", ""];
        tableData["age"] = ["integer", ""];

        DBSystem sys = new();
        // sys.CreateTable();
        // sys.PrintTableList();
        // sys.DescribeTable();

        sys.CreateTable();
        sys.DescribeTable();
        sys.AddRowsToTable();
    }
}