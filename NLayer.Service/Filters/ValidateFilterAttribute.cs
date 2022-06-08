using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {   
        // Öncelikle niye böyle bir filter yapısına ihtiyacımız var onu yazayım
        // Bizim validasyon işlemimizde CustomRepsonseDto dönmüyor kendi oluşturduğu ActionResult'ı dönüyor.
        // Bizde tam o sırada devereye girip yani bizim Action'ımız çalıştığında buna müdahale edicez
        // Ve Kendi CustomResponseDto'umuza uygun hale getireceğiz.
        // Bunu yapabilmek için program.cs 'e gidip ilk önce default filter'ı kapatıyoruz gerekli not orda var.
        // Daha sonra bizim yaptığımız işlemi builder.Services.AddControllers'da tanımlıyoruz.
        
        // context.ModelState hatalar otomatik olarak yüklenir fluent validation kullansakda kullanmasakda.



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, errors));    


            }


            base.OnActionExecuting(context);
        }


    }
}
