using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Company.Function.Data;
using Azure_Functions_Sample.Helpers;

namespace Company.Function
{
    public static class DepartamentsFunctions
    {
        private static DepartmentsRepository departmentsRepository = new DepartmentsRepository();
        private static ConfigurationCustomProvider configurationProvider = new ConfigurationCustomProvider();
        
        [FunctionName("InsertDepartments")]
        public static async Task<IActionResult> Insert(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "departments")] HttpRequest req,
            ILogger log, ExecutionContext context)
        {
            log.LogInformation("Insert");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string description = data?.description;

            var config = configurationProvider.GetConfiguration(context.FunctionAppDirectory);
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {name}");
            if(!String.IsNullOrEmpty(name))
            {
                try
                {
                    await departmentsRepository.InsertDepartment(connectionString, name, description);
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "departments/{id:int?}")] HttpRequest req,
            int? id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Get");

            var config = configurationProvider.GetConfiguration(context.FunctionAppDirectory);
            string connectionString = config["ConnectionString"];

            var successful = false;
            object data = null;
            log.LogInformation($"Parameter: {id}");
            try
            {
                data = await departmentsRepository.GetDepartments(connectionString, id);
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "departments/{id:int}")] HttpRequest req,
            int id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Update");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string description = data?.description;

            var config = configurationProvider.GetConfiguration(context.FunctionAppDirectory);
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {name}");
            if(!String.IsNullOrEmpty(name))
            {
                try
                {
                    await departmentsRepository.UpdateDepartment(connectionString, id, name, description);
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
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "departments/{id:int}")] HttpRequest req,
            int id, ILogger log, ExecutionContext context)
        {
            log.LogInformation("Delete");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            var config = configurationProvider.GetConfiguration(context.FunctionAppDirectory);
            string connectionString = config["ConnectionString"];

            var successful = false;
            log.LogInformation($"Parameter: {id.ToString()}");
            try
            {
                await departmentsRepository.DeleteDepartment(connectionString, id);
                successful = true;
            }
            catch (Exception x)
            {
                log.LogInformation("exception: " + x.StackTrace.ToString());
                successful = false;
            }	
            return !successful
                    ? new BadRequestObjectResult("The request failed")
                    : (ActionResult)new OkObjectResult($"Data for id {id} deleted succesfully");
        }
    }
}
