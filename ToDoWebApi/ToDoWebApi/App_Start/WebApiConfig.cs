using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ToDoWebApi
{
    public static class WebApiConfig
    {
        public class CustomJsonFormatter :  JsonMediaTypeFormatter
        {
            public CustomJsonFormatter()
            {
                this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            }
            public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
            {
                base.SetDefaultContentHeaders(type, headers, mediaType);
                headers.ContentType = new MediaTypeHeaderValue("application/json");

            }

        }
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



            /*if user is making request of text/html type and you want to replace it with application/json
             * it means you want to force web api to response in application/json when text/html expected
             * 2 approaches are there: and this is approach 1
             * approach 2 is to create custom class and then define the constraints in constructor and ovverride its
             * SetDefaultContentHeaders method
             * => and add to config.Formatter.Add 
             * please see above
             */

            config.Formatters.Add(new CustomJsonFormatter());
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            /*To Remove the formatter in which even if request in particular format 
             * the respinse will eliminate the expected formatter
             */
            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            /*To place elements in intended form*/
            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;


            /*To return key names in Camel case(Firstname) rather than Pascal Case(FirstName)*/
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        }
    }
}
