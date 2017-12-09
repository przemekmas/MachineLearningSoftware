using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRecognitionSoftware.ExceptionLog
{
    class DatabaseConnection
    {
        private SQLiteConnection m_SqlConnection;

        public DatabaseConnection()
        {
            m_SqlConnection = new SQLiteConnection("Data Source=/Tables/ExceptionLogTable.db;Version=3;New=False;Compress=True;");
            m_SqlConnection.Open();

            var sqlCommand = m_SqlConnection.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM ExceptionLog";
            sqlCommand.ExecuteNonQuery();            
        }

        public void CloseConnection()
        {
            m_SqlConnection.Close();
        }
    }
}
