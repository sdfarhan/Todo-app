

using MySql.Data.MySqlClient;
using System;

namespace WindowsFormsApp1
{
    partial class MySql
    {
        public void Insert(SingleTask Task,DateTime Date)
        {
            string query = $"INSERT INTO tasks (TimeCreated, Date, Task, TimeScheduled) VALUES('{Task.TimeCreated.ToString(@"hh\:mm\:ss")}','{Date.Date.ToString(@"yyyy-MM-dd")}','{Task.Task}','{Task.TimeScheduled.ToString(@"hh\:mm\:ss")}')";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
        public void Delete(SingleTask Task)
        {
            string query = $"DELETE FROM tasks WHERE TimeCreated='{Task.TimeCreated.ToString(@"hh\:mm\:ss")}';";
            if (OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
    }
}