using Microsoft.AspNetCore.Mvc.Filters;

namespace admin_cms.Filtros;

public class LogadoAttribute : ActionFilterAttribute
{
public override void OnActionExecuting(ActionExecutingContext filterContext)
{
    if( string.IsNullOrEmpty(filterContext.HttpContext.Request.Cookies["admin-codigo-do-futuro"]) ){
        filterContext.HttpContext.Response.Redirect("/login");
        return;
    }
    base.OnActionExecuting(filterContext);
}
}