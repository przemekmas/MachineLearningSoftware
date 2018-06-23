using System.Data.SQLite;

namespace MachineLearningSoftware.DataAccess
{
    public class BaseDataAccess
    {
        public SQLiteConnection _sqlConnection;

        public BaseDataAccess()
        {
            OpenConnection();
        }

        public void OpenConnection()
        {
            _sqlConnection = new SQLiteConnection("Data Source=MLSDatabase.sqlite;Version=3;datetimeformat=InvariantCulture");
            _sqlConnection.Open();
        }
        
        public void CloseConnection()
        {
            _sqlConnection.Close();
        }
    }
}
