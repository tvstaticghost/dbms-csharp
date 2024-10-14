using System.Reflection.Metadata.Ecma335;

class DBSystem {
    private string tableNameMessage = "Enter the table name: ";
    private string ColumnMessage = "Add column?\n[y]es or [n]o: ";
    private string AnotherColumnMessage = "Add another column?\n[y]es or [n]o: ";
    private string ColumnNameMessage = "Enter column name: ";
    private string WhatTableMessage = "Enter table name to describe: ";
    private string TableUpdateMessage = "Enter table name to update: ";
    public List<Table> tables = [];

    public void CreateTable() { //Need to check forduplicate tables!!!
        //define a dictionary to add column names and data attributes
        Dictionary<string, string[]> cols = [];
        Dictionary<string, string[]> rows = [];
        //get the name of the table
        string tableName = Helper.GetUserInput(tableNameMessage);
        bool tableCheck = DoesTableExist(tableName);

        //check to see if the table already exists in the table list to prevent duplicates
        if (tableCheck) {Console.WriteLine($"Table already exists"); return;}

        bool pkPresent = false;
        bool addColumn = Helper.AddColumn(ColumnMessage);

        //loop through and gather column data using Helper class methods
        while (addColumn) {
            string columnName = Helper.GetUserInput(ColumnNameMessage);
            string dataType = Helper.GetColumnDataType(columnName);
            string keyType = "";
            if (!pkPresent) {
                keyType = Helper.IsPrimaryKey(columnName);
                if (keyType == "pk") { pkPresent = true; }  
            }

            cols[columnName] = [dataType, keyType];
            rows[columnName] = [];

            addColumn = Helper.AddColumn(AnotherColumnMessage); //continue or end the loop
        }
        
        if (cols.Count == 0) {
            Console.WriteLine("Newly created tables must contain atleast 1 column. Table discarded...");
            return;
        }
        Table newTable = new(tableName, cols, rows);
        tables.Add(newTable);
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

    public void AddRowsToTable() {
        string tableToUpdate = Helper.GetUserInput(TableUpdateMessage);
        bool tablePresent = false;

        foreach(Table table in tables) {
            if (table.GetTableName() == tableToUpdate) {
                Helper.AddRows(table.GetCols(), table.GetRows());
                tablePresent = true;
            }
        }

        if (!tablePresent) { Console.WriteLine($"{tableToUpdate} not found..."); }
    }

    //Add method to append rows to a table (check for datatypes and validate)

    public void SelectAllFromTable() {
        string tableToQuery = Helper.GetUserInput(WhatTableMessage);
        bool tableFound = false;

        foreach(Table table in tables) {
            if (table.GetTableName() == tableToQuery) {
                tableFound = true;
                Helper.SelectFromTable(table.GetTableName(), table.GetCols(), table.GetRows());
                return;
            }
        }

        if (!tableFound) {
            Console.WriteLine($"{tableToQuery} not found in database...");
            return;
        }
    }

    public void SelectSpecificDataFromTable() {
        string tableToQuery = Helper.GetUserInput(WhatTableMessage);
        bool tableFound = false;

        foreach (Table table in tables) {
            if (table.GetTableName() == tableToQuery) {
                tableFound = true;
                //Add Helper Function
                Helper.SelectSpecificFromTable(table.GetTableName(), table.GetCols(), table.GetRows());
                return;
            }
        }

        if (!tableFound) {
            Console.WriteLine($"{tableToQuery} not found in database...");
            return;
        }
    }

    public void PrintTableList() { 
        Console.WriteLine("+".PadRight(10, '-') + "Table List" + "+".PadLeft(10, '-'));
        if (tables.Count > 0) {
            foreach (Table table in tables) {
                Console.WriteLine(table.GetTableName());
            }
        }
        else {
            Console.WriteLine("No tables in database...");
        }
        Console.WriteLine("+".PadRight(15, '-') + "+".PadLeft(15, '-'));
     }

     public bool DoesTableExist(string tableName) {
        foreach (Table table in tables) {
            if (table.GetTableName() == tableName) {
                return true;
            }
        }
        return false;
    }
}