using CursoOnline.Dominio._Base;
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
            context.HttpContext.Response.StatusCode = context.Exception is ExcecaoDeDominio ? 502 : 500;
            context.Result = context.Exception is ExcecaoDeDominio dominio ? 
                new JsonResult(dominio.MensagensDeErro) :
                new JsonResult("An error occured");
            context.ExceptionHandled = true;
        }
        
        base.OnException(context);
    }
}