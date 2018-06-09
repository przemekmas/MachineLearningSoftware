using MachineLearningSoftware.Entities;
using System;
using System.Collections.ObjectModel;
using System.Data.SQLite;

namespace MachineLearningSoftware.Common
{
    public class ExceptionLogging
    {
        private static SQLiteConnection _sqlConnection = new SQLiteConnection("Data Source=ExceptionLogTable.sqlite;Version=3;");
        
        public ExceptionLogging()
        {
            InitiateConnection();
        }

        public void InitiateConnection()
        {
            _sqlConnection = new SQLiteConnection("Data Source=ExceptionLogTable.sqlite;Version=3;datetimeformat=CurrentCulture");
            _sqlConnection.Open();
            CreateExceptionLogTable();
        }

        private void CreateExceptionLogTable()
        {
            string sqlCreateTable = "CREATE TABLE IF NOT EXISTS ExceptionLog (Id int AUTO_INCREMENT, Exception varchar, Time TIME)";
            SQLiteCommand sqlCreateComand = new SQLiteCommand(sqlCreateTable, _sqlConnection);
            sqlCreateComand.ExecuteNonQuery();
        }

        public void CloseConnection()
        {
            _sqlConnection.Close();
        }

        public static ObservableCollection<ExceptionEntity> GetExceptions()
        {
            var exceptionList = new ObservableCollection<ExceptionEntity>();

            string sql = "SELECT Exception,Time FROM ExceptionLog";
            SQLiteCommand command = new SQLiteCommand(sql, _sqlConnection);
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
            string sql = string.Format("INSERT INTO ExceptionLog VALUES((SELECT Id FROM ExceptionLog)+1, @exception, '{0}')", DateTime.Now);
            SQLiteCommand command = new SQLiteCommand(sql, _sqlConnection);
            command.Parameters.Add(new SQLiteParameter("@exception", exception));
            command.ExecuteNonQuery();
        }
    }
}
