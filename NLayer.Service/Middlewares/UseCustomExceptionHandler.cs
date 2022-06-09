using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NLayer.Service.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        // Middleware yazabilmek için IApplicationBuilder'ı extend etmemiz gerekiyor.
        public static void UseCustomExcepiton(this IApplicationBuilder app)
        {

            app.UseExceptionHandler(config =>
            {

                config.Run(async context =>
                {   
                    // Hatanın Veriliş Türü 
                    context.Response.ContentType = "application/json";

                    // Hatanın içeriği nerden gelsin
                    var excepitonFeature = context.Features.Get<IExceptionHandlerFeature>();
                    
                    // Hata Client'dan mı geldi yoksa Server'dan mı 
                    var statusCode = excepitonFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundException => 404,
                        _ => 500
                    };


                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, excepitonFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });

            });

        }

    }
}
