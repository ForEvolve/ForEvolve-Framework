using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForEvolve.DynamicInternalServerError.TWebServer
{
    [Route("api/throw/exception")]
    public class ThrowExceptionController : Controller
    {
        private readonly Exception _exception;

        public ThrowExceptionController(Exception exception)
        {
            _exception = exception;
        }

        // GET: api/values
        [HttpGet]
        public void Get()
        {
            if (_exception == null)
            {
                throw new Exception("An error occured.");
            }
            else
            {
                throw _exception;
            }
        }
    }
}
