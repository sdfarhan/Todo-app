
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace WindowsFormsApp1
{
    partial class MySql
    {
        public List<SingleTask> Select(DateTime Date)
        {
            string query = $"SELECT * FROM tasks where DATE = ${Date.Date}";
            List<SingleTask> Data = new List<SingleTask>();
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, Connection);
                MySqlDataReader DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    TimeSpan TimeCreated = (TimeSpan)DataReader["TimeCreated"];
                    string Task = (String)DataReader["Task"];
                    TimeSpan TimeScheduled = (TimeSpan)DataReader["TimeScheduled"];
                    Data.Add(new SingleTask(TimeCreated, Task, TimeScheduled));
                }
                DataReader.Close();
                CloseConnection();
            }
            return Data;
        }
    }
}