using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForEvolve.DynamicInternalServerError.TestWebServer
{
    [Route("api/validate/oneproperty")]
    public class SomeModelWithOnePropertyController : Controller
    {
        [HttpPost]
        public IActionResult Post(SomeModelWithOneProperty model)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

    [Route("api/validate/multipleproperties")]
    public class SomeModelWithMultiplePropertiesController : Controller
    {
        [HttpPost]
        public IActionResult Post(SomeModelWithMultipleProperties model)
        {
            if (ModelState.IsValid)
            {
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

    public class SomeModelWithOneProperty
    {
        [Required]
        public string Prop1 { get; set; }
    }

    public class SomeModelWithMultipleProperties
    {
        [Required]
        public string Prop1 { get; set; }

        //[Required]
        //[StringLength(5, MinimumLength = 2)]
        //[RegularExpression("[a-z]{2,5}")]
        public string Prop2 { get; set; }

        [Range(1, 10)]
        public int Prop3 { get; set; }

        [Required(ErrorMessage = "Some custom required error message.")]
        public string Prop4 { get; set; }
    }
}
