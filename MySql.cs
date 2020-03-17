using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    partial class MySql : IDisposable
    {
        private MySqlConnection Connection;
        private string Server;
        private string Database;
        private string User;
        private string Password;
        public MySql()
        {
            Initialize();
        }
        private string GetConnectionString()
        {
            return "SERVER=" + Server + ";"
                + "DATABASE=" + Database + ";"
                + "UID=" + User + ";"
                + "PASSWORD=" + Password + ";";
        }
        private void Initialize()
        {
            Server = "localhost";
            Database = "TodoDatabase";
            User = "root";
            Password = "953624187";
            Connection = new MySqlConnection(GetConnectionString());
        }
        private bool OpenConnection()
        {
            try
            {
                Connection.Open();
                return true;
            }
            catch (MySqlException EX)
            {
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (EX.Number)
                {
                    case 0:
                        Debug.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Debug.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }
        private bool CloseConnection()
        {
            try
            {
                Connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
