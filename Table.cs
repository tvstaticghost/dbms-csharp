using System.Runtime.CompilerServices;

class Table {
    private string tableName;
    private Dictionary<string, string[]> cols = [];
    private Dictionary<string, string[]> rows = [];

    public Table(string tableName, Dictionary<string,string[]> cols) {
        this.tableName = tableName;
        this.cols = cols;
    }

    public string GetTableName() { return tableName; }

    public Dictionary<string, string[]> GetCols() { return cols; }

    public void AddRows(Dictionary<string, string[]> rows) { this.rows = rows; }
}