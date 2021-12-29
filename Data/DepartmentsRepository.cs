using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Company.Function.Helpers;

namespace Company.Function.Data
{
    public class DepartmentsRepository
    {
        private static Serializator serializator = new Serializator();
        public async Task InsertDepartment(string connectionString, string name, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // insert
                string query = String.Format("INSERT INTO [dbo].[Departments] ([name], [description] )  VALUES ('{0}', '{1}')", name, description);
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }	
            }
        }

        public async Task<object> GetDepartments(string connectionString, int? id)
        {
            object data = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = (id != null) 
                    ? String.Format("SELECT * FROM [dbo].[Departments] WHERE id = {0}", id.ToString())
                    : "SELECT * FROM [dbo].[Departments]";
                using (SqlCommand cmd = new SqlCommand())
                {
                    SqlDataReader dataReader;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    dataReader = await cmd.ExecuteReaderAsync();
                    data = serializator.Serialize(dataReader);
                    if(((IEnumerable<object>)data).Count()==1)
                        data=((IEnumerable<object>)data).First();
                }	
            }
            return data;
        }

        public async Task UpdateDepartment(string connectionString, int id, string name, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Update
                string query = String.Format("UPDATE [dbo].[Departments] SET [name]='{0}', [description]='{1}' WHERE [id]={2}", name, description, id);
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }	
            }
        }

        public async Task DeleteDepartment(string connectionString, int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Delete
                string query = String.Format("DELETE FROM [dbo].[Departments] WHERE [id]={0}", id.ToString());
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    await cmd.ExecuteNonQueryAsync();
                    connection.Close();
                }	
            }
        }
    }
}