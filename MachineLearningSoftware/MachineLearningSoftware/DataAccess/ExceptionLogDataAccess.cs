using MachineLearningSoftware.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.SQLite;

namespace MachineLearningSoftware.DataAccess
{
    [Export(typeof(ExceptionLogDataAccess))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ExceptionLogDataAccess : BaseDataAccess
    {
        public ExceptionLogDataAccess()
        {
            CreateExceptionLogTable();
        }

        private void CreateExceptionLogTable()
        {
            string sqlCreateTable = @"CREATE TABLE IF NOT EXISTS ExceptionLog 
    (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Exception varchar, Time DATETIME)";
            using (var cmd = new SQLiteCommand(sqlCreateTable, _sqlConnection))
            {
                cmd.ExecuteNonQuery();
            }            
        }
        
        public ObservableCollection<ExceptionEntity> GetExceptions()
        {
            var exceptionList = new ObservableCollection<ExceptionEntity>();

            var sql = "SELECT Exception,Time FROM ExceptionLog";
            using (var cmd = new SQLiteCommand(sql, _sqlConnection))
            {
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var exception = reader["Exception"];
                    exceptionList.Add(new ExceptionEntity()
                    {
                        Exception = exception.ToString(),
                        Time = (DateTime)reader["Time"]
                    });
                }
            }            
            return exceptionList;
        }

        public void LogException(string exception)
        {
            if (!string.IsNullOrEmpty(exception))
            {
                var sql = "INSERT INTO ExceptionLog(Exception,Time) VALUES(@exception, @date)";
                using (var cmd = new SQLiteCommand(sql, _sqlConnection))
                {
                    cmd.Parameters.Add(new SQLiteParameter("@exception", exception));
                    cmd.Parameters.Add(new SQLiteParameter("@date", DateTime.Now));
                    cmd.ExecuteNonQuery();
                }
            }            
        }
    }
}
