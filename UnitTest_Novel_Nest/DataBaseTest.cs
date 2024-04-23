using System.Reflection;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Novel_Nest_DAL;
using UnitTest_Novel_Nest;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using MySqlConnector;

namespace UnitTest_Novel_Nest
{
	[TestClass]
	public class DataBaseTest
	{
		[TestMethod]
		public void DataBaseConnectionTestShouldReturnTrue()
		{
			//arrange
			string connectionString = "Server=127.0.0.1;Database=Novel_Nest_Db;Uid=Root;";
			MyDbContext dbContext = new MyDbContext(connectionString);

			//act
			using (MySqlConnection connection = dbContext.OpenConnection())
			{
				//assert
				//Assert.IsNotNull(connection);
				Assert.Equals(System.Data.ConnectionState.Open, connection.State);
			}
		}
	}
}