using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CoreLogic.Services.Commands;
using CoreLogic.Models;
using System.Collections.Generic;
using MediatR;
using System.Threading;
using Microsoft.Azure.WebJobs.Host;
using System.Web.Http;

namespace IQL.Currency.Feeder;

public class CurrencyFeeder
{
    private readonly IMediator Mediator;

    public CurrencyFeeder(IMediator mediator)
    {
        Mediator = mediator;
    }
    [FunctionName("CurrencyFeeder")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject<List<CurrencyPrice>>(requestBody);

            var command = new CurrencyPriceFeedCommand(data);
            await Mediator.Send(command);
        }
        catch (Exception ex)
        {
            log.LogError($"Please investigate the global error: {ex}");
            return new ExceptionResult(ex, true);
        }

        string responseMessage = "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

        return new OkObjectResult(responseMessage);
    }
}
