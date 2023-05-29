using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.AccessDeniedMiddleware;
public class AccessDeniedMiddleware
{
    private readonly RequestDelegate _next;

    public AccessDeniedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "text/plain";
            await context.Response.WriteAsync("Bạn không có quyền sử dụng chức năng này");
        }
    }
}

