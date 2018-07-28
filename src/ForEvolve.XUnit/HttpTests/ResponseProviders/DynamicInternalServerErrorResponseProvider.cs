using ForEvolve.Contracts.Errors;
using ForEvolve.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.XUnit.HttpTests
{
    public class DynamicInternalServerErrorResponseProvider : DelegateResponseProvider
    {
        public DynamicInternalServerErrorResponseProvider(Func<IErrorFactory, Error> createErrorDelegate)
            : base(context =>
            {
                var errorFactory = context.RequestServices.GetRequiredService<IErrorFactory>();
                var error = createErrorDelegate(errorFactory);

                var result = new ErrorResponse(error);
                return JsonConvert.SerializeObject(result);
            })
        { }

        public DynamicInternalServerErrorResponseProvider(Error error)
            : base(context =>
            {
                var result = new ErrorResponse(error);
                return JsonConvert.SerializeObject(result);
            })
        { }
    }
}
