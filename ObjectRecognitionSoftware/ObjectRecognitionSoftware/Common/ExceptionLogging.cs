using ObjectRecognitionSoftware.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectRecognitionSoftware.Common
{
    public class ExceptionLogging
    {
        private static SQLiteConnection m_SqlConnection = new SQLiteConnection("Data Source=ExceptionLogTable.sqlite;Version=3;");
        private SQLiteDataAdapter m_DataAdapter;
        private DataSet m_DataSet;
        private DataTable m_DataTable;
        
        public ExceptionLogging()
        {
            InitiateConnection();
        }

        public void InitiateConnection()
        {
            m_SqlConnection = new SQLiteConnection("Data Source=ExceptionLogTable.sqlite;Version=3;datetimeformat=CurrentCulture");
            m_SqlConnection.Open();
            CreateExceptionLogTable();
        }

        private void CreateExceptionLogTable()
        {
            string sqlCreateTable = "CREATE TABLE IF NOT EXISTS ExceptionLog (Id int AUTO_INCREMENT, Exception varchar, Time TIME)";
            SQLiteCommand sqlCreateComand = new SQLiteCommand(sqlCreateTable, m_SqlConnection);
            sqlCreateComand.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
            m_SqlConnection.Close();
        }

        public static List<ExceptionEntity> GetExceptions()
        {
            var exceptionList = new List<ExceptionEntity>();

            string sql = "SELECT Exception,Time FROM ExceptionLog";
            SQLiteCommand command = new SQLiteCommand(sql, m_SqlConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var exception = reader["Exception"];
                exceptionList.Add(new ExceptionEntity()
                {
                    Exception = exception.ToString(),
                    Time = (DateTime)reader["Time"]
                });
            }
            return exceptionList;
        }

        public static void LogException(string exception)
        {
            string sql = string.Format("INSERT INTO ExceptionLog VALUES((SELECT Id FROM ExceptionLog)+1, '{0}', '{1}')", exception, DateTime.Now);
            SQLiteCommand command = new SQLiteCommand(sql, m_SqlConnection);
            command.ExecuteNonQuery();
        }
    }
}
