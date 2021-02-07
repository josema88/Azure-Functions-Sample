using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Company.Function
{
    public static class DepartamentosFunctions
    {
        private static Serializator serializator = new Serializator();
        
        [FunctionName("InsertDepartments")]
        public static async Task<IActionResult> Insert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "departamentos")] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("Insert");

            string name = req.Query["name"];
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {name}");
            if(!String.IsNullOrEmpty(name))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // insert
                        string query = String.Format("INSERT INTO [dbo].[departamento] ([nombre])  VALUES ('{0}')", name);
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }	
                    }
                    successful = true;
                }
                catch (Exception x)
                {
                    log.LogInformation("exception: " + x.StackTrace.ToString());
                    successful = false;
                }	
            }
            else
                successful = false;
            return !successful
                    ? new BadRequestObjectResult("The request failed")
                    : (ActionResult)new OkObjectResult($"Data {name} stored succesfully");
        }

        [FunctionName("GetDepartments")]
        public static async Task<IActionResult> Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "departamentos/{id:int?}")] HttpRequest req,
            int? id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Get");

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
            string connectionString = config["ConnectionString"];

            var successful = false;
            object data = null;
            log.LogInformation($"Parameter: {id}");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = (id != null) 
                        ? String.Format("SELECT * FROM [dbo].[departamento] WHERE id = {0}", id.ToString())
                        : "SELECT * FROM [dbo].[departamento]";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        SqlDataReader dataReader;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        connection.Open();
                        dataReader = cmd.ExecuteReader();
                        data = serializator.Serialize(dataReader);
                        if(((IEnumerable<object>)data).Count()==1)
                            data=((IEnumerable<object>)data).First();
                    }	
                }
                successful = true;
            }
            catch (Exception x)
            {
                log.LogInformation("exception: " + x.StackTrace.ToString());
                successful = false;
            }	
            return !successful
                    ? new BadRequestObjectResult("The request failed")
                    : (ActionResult)new OkObjectResult(data);
        }

        [FunctionName("UpdateDepartments")]
        public static async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "departamentos/{id:int}")] HttpRequest req,
            int id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Update");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {name}");
            if(!String.IsNullOrEmpty(name))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Update
                        string query = String.Format("UPDATE [dbo].[departamento] SET [nombre]='{0}' WHERE [id]={1}", name, id);
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }	
                    }
                    successful = true;
                }
                catch (Exception x)
                {
                    log.LogInformation("exception: " + x.StackTrace.ToString());
                    successful = false;
                }	
            }
            else
                successful = false;
            return !successful
                    ? new BadRequestObjectResult("The request failed")
                    : (ActionResult)new OkObjectResult($"Data for id {id} update succesfully");
        }
        
        [FunctionName("DeleteDepartments")]
        public static async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "departamentos/{id:int}")] HttpRequest req,
            int id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Delete");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;

            var config = new ConfigurationBuilder()
                            .SetBasePath(context.FunctionAppDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {name}");
            if(!String.IsNullOrEmpty(name))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Delete
                        string query = String.Format("DELETE FROM [dbo].[departamento] WHERE [id]={1}", id);
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            connection.Open();
                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }	
                    }
                    successful = true;
                }
                catch (Exception x)
                {
                    log.LogInformation("exception: " + x.StackTrace.ToString());
                    successful = false;
                }	
            }
            else
                successful = false;
            return !successful
                    ? new BadRequestObjectResult("The request failed")
                    : (ActionResult)new OkObjectResult($"Data for id {id} deleted succesfully");
        }
    }

    public class Serializator
    {
        public IEnumerable<Dictionary<string, object>> Serialize(SqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var cols = new List<string>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                var colName = reader.GetName(i);
                var camelCaseName = Char.ToLowerInvariant(colName[0]) + colName.Substring(1);
                cols.Add(camelCaseName);
            }

            while (reader.Read())
                results.Add(SerializeRow(cols, reader));

            return results;
        }

        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols,SqlDataReader reader) 
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }
    }
}
