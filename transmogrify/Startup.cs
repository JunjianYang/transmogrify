using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace transmogrify
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {

                if (context.Request.Headers["Referer"].ToString().Length == 0)
                {
                    await context.Response.WriteAsync("Please call from referring URI at github.com");
                    return;
                }

                Uri rUri = new Uri(context.Request.Headers["Referer"].ToString());

                string org = rUri.Segments[1].Replace("/","");
                string project = rUri.Segments[2].Replace("/", "");
                string branch = "master";

                if (rUri.Segments.Length > 3)
                {
                    branch = rUri.Segments[4].Replace("/", "");
                }

                https://portal.azure.us/#create/Microsoft.Template/uri/

                string portal = "https://portal.azure.com";
                if (context.Request.Query["environment"] == "gov")
                {
                    portal = "https://portal.azure.us";
                }

                string templateUri = "https://raw.githubusercontent.com/" + org + "/" + project + "/" + branch + context.Request.Path;
                templateUri = System.Web.HttpUtility.UrlEncode(templateUri);

                string redirectUri = portal + "/#create/Microsoft.Template/uri/" + templateUri;

                if (context.Request.Path != "/") {
                    context.Response.Redirect(redirectUri);
                } 

                await context.Response.WriteAsync("Please provide redirect path.");
            });
        }
    }
}
