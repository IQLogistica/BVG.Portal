using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CoreLogic.Models;
using CoreLogic.Services.Commands;
using MediatR;
using System.Web.Http;
using CoreLogic.Contracts;

namespace IQL.Commodity.Feeder;

public class CommodityFeeder
{
    private readonly IMediator Mediator;
    private readonly IDateTransform _dateTransform;

    public CommodityFeeder(IMediator mediator, IDateTransform dateTransform)
    {
        Mediator = mediator;
        _dateTransform = dateTransform;
    }

    [FunctionName("CommodityFeeder")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        string responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<Commodities>(requestBody);

            var command = new CommodityPriceFeedCommand(data, _dateTransform);
            await Mediator.Send(command);

        }
        catch (Exception ex) { 
            log.LogError($"Please investigate the global error: {ex}");
            return new ExceptionResult(ex, true);
        }

        return new OkObjectResult(responseMessage);
    }
}
