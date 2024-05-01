using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using MySqlConnector;


namespace Novel_Nest_DAL
{
    public class MyDbContext 
    {
        private readonly string _connectionString;
        

        public MyDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public MySqlConnection OpenConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
        
    }

}
