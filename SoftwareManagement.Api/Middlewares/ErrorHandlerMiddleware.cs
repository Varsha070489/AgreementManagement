
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SoftwareManagement.Application.Exceptions;

namespace SoftwareManagement.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<ErrorHandlerMiddleware> _loggerInstance;
        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _loggerInstance = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = Result<string>.Fail(error.Message);

                switch (error)
                {
                    case ApiException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _loggerInstance.LogError(new EventId(), e, "Api Exception");
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        _loggerInstance.LogError(new EventId(), e, "Key Not Found Exception");
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _loggerInstance.LogError(new EventId(), error, "General Exception");
                        break;
                }
                //if (responseModel.Data==null)
                //{
                //    responseModel.Data = "0";
                //}
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}