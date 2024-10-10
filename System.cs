using System.Reflection.Metadata.Ecma335;

class DBSystem {
    private string tableNameMessage = "Enter the table name: ";
    private string ColumnMessage = "Add column?\n[y]es or [n]o: ";
    private string AnotherColumnMessage = "Add another column?\n[y]es or [n]o: ";
    private string ColumnNameMessage = "Enter column name: ";
    private string WhatTableMessage = "Enter table name to describe: ";
    public List<Table> tables = [];

    public void CreateTable() {
        //define a dictionary to add column names and data attributes
        Dictionary<string, string[]> cols = [];
        //get the name of the table
        string tableName = Helper.GetUserInput(tableNameMessage);

        bool pkPresent = false;
        bool addColumn = Helper.AddColumn(ColumnMessage);

        //loop through and gather column data using Helper class methods
        while (addColumn) {
            string columnName = Helper.GetUserInput(ColumnNameMessage);
            string dataType = Helper.GetColumnDataType(columnName);
            string keyType = "";
            if (!pkPresent) {
                keyType = Helper.IsPrimaryKey(columnName);
                pkPresent = true;   
            }

            cols[columnName] = [dataType, keyType];

            addColumn = Helper.AddColumn(AnotherColumnMessage); //continue or end the loop
        }
        //tables.Add();

        Table newTable = new(tableName, cols);
        tables.Add(newTable);

        //test current table information
        foreach (KeyValuePair<string, string[]> entry in cols) {
            Console.WriteLine($"{entry.Key} - {entry.Value[0]} {entry.Value[1]}");
        }
    }

    public void DescribeTable() {
        string tableToQuery = Helper.GetUserInput(WhatTableMessage);
        bool tableFound = false;

        foreach(Table table in tables) {
            if (table.GetTableName() == tableToQuery) {
                tableFound = true;
                Helper.FormatTableDescription(table.GetTableName(), table.GetCols());
                return;
            }
        }

        if (!tableFound) {
            Console.WriteLine($"{tableToQuery} not found in database...");
            return;
        }
    }

    //Add method to append rows to a table (check for datatypes and validate)

    public void PrintTableList() { 
        Console.WriteLine("=====Table List=====");
        if (tables.Count > 0) {
            foreach (Table table in tables) {
                Console.WriteLine(table.GetTableName());
            }
        }
        else {
            Console.WriteLine("No tables in database...");
        }
        Console.WriteLine("====================");
     }
}