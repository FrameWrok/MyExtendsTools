using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartJSFrameWork.WebSite
{
    public class PageAuth : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            context.PreRequestHandlerExecute += Context_PreRequestHandlerExecute;
        }

        private void Context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            //Conditionally replace the response if this is a postback
            if (context.Request.Path.EndsWith(".aspx") && context.Request.HttpMethod == "POST")
            {
                //This is late enough in the pipeline to access the request stream
                var find = context.Request.Form["TBFind"];
                if (!string.IsNullOrEmpty(find))
                {
                    //Sample demonstrating rewriting the response
                    var responseCallback = new Func<string, string>(content =>
                    {
                        return content.Replace(find, "<strong>" + find + "</strong>");
                    });
                    context.Response.Filter = new SmartJSFrameWork.WebSite.App_Code.ResponseFilter(context.Response.Filter, context.Response.ContentEncoding, responseCallback);
                }
            }
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            if (context.Request.Path.EndsWith(".aspx") && context.Request.HttpMethod == "POST")
            {
                //Sample demonstrating rewriting the request
                var requestCallback = new Func<string, string>(content =>
                {

                    //Read postback form and rewrite the request.
                    var httpValueCollection = HttpUtility.ParseQueryString(content);
                    httpValueCollection["TBUsername"] = httpValueCollection["TBUsername"].ToUpper();

                    return httpValueCollection.ToString();
                });
                context.Request.Filter = new SmartJSFrameWork.WebSite.App_Code.RequestFilter(context.Request.Filter, context.Request.ContentEncoding, requestCallback);
            }
        }
    }
}