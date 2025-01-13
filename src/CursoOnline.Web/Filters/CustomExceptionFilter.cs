using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoOnline.Web.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        bool isAjax = context.HttpContext.Request.Headers["x-requested-with"] == "XMLHttpRequest";
        
        if (isAjax)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = 500;
            var message = context.Exception is ArgumentException ? context.Exception.Message : "An error occured";
            context.Result = new JsonResult(message);
            context.ExceptionHandled = true;
        }
        
        base.OnException(context);
    }
}