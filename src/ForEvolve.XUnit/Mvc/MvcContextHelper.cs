using ForEvolve.XUnit.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForEvolve.XUnit.Mvc
{
    public class MvcContextHelper
    {
        public HttpContextHelper HttpContextHelper { get; }

        public ActionContext ActionContext { get; }
        public RouteData RouteData { get; }
        public ActionDescriptor ActionDescriptor { get; }
        public ModelStateDictionary ModelStateDictionary { get; }

        public Mock<IUrlHelper> UrlHelperMock { get; }

        public MvcContextHelper()
        {
            HttpContextHelper = new HttpContextHelper();

            RouteData = new RouteData();
            ActionDescriptor = new ActionDescriptor();
            ModelStateDictionary = new ModelStateDictionary();
            ActionContext = new ActionContext(
                HttpContextHelper.HttpContextMock.Object, 
                RouteData, 
                ActionDescriptor, 
                ModelStateDictionary
            );
            UrlHelperMock = new Mock<IUrlHelper>();

            UrlHelperMock
                .Setup(x => x.ActionContext)
                .Returns(ActionContext);
        }
    }
}
