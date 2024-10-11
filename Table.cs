using System.Runtime.CompilerServices;

class Table {
    private string tableName;
    private Dictionary<string, string[]> cols = [];
    private Dictionary<string, string[]> rows = [];

    public Table(string tableName, Dictionary<string,string[]> cols, Dictionary<string,string[]> rows) {
        this.tableName = tableName;
        this.cols = cols;
        this.rows = rows;
    }

    public string GetTableName() { return tableName; }

    public Dictionary<string, string[]> GetCols() { return cols; }

    public Dictionary<string, string[]> GetRows() { return rows; }

    public void AddRows(Dictionary<string, string[]> rows) { this.rows = rows; }
}