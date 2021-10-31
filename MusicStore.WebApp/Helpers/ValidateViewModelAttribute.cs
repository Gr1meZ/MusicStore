using System;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MusicStore.WebApp.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CheckRequiredModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requiredParameters = context.ActionDescriptor.Parameters.Where(
                p => ((ControllerParameterDescriptor)p).ParameterInfo.GetCustomAttribute<RequiredModelAttribute>() != null).Select(p => p.Name);

            foreach (var argument in context.ActionArguments.Where(a => requiredParameters.Contains(a.Key, StringComparer.Ordinal)))
            {
                if (argument.Value == null)
                {
                    context.ModelState.AddModelError(argument.Key, $"The argument '{argument.Key}' cannot be null.");
                }
            }

            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                context.Result = new BadRequestObjectResult(errors);
                return;
            }

            base.OnActionExecuting(context);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RequiredModelAttribute : Attribute
    {
    }
    
}