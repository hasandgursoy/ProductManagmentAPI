using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Entities;
using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Filters
{   
    // Eğer bir Filter service'i constructor olarak alıyorsa Controller'da tanımlarken [ServiceFilter()] olarak tanımlamamız lazım ve bunu DI'a tanıtacaz.
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {   

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {   // ActionMethod'a daha tam olarak girmeden müdahale ediyoruz.

            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue is null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }


            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) not found"));
        }
    }
}
