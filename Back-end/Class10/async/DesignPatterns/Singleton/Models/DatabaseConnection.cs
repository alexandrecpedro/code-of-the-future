namespace Singleton.Models;

public sealed class DatabaseConnection
{
    private static DatabaseConnection? instance;
    public static DatabaseConnection Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DatabaseConnection();
            }

            return instance;
        }
    }
    private DatabaseConnection() { }

    public string ExecutaQuery(string query)
    {
        return "Query executada com sucesso!";
    }
}