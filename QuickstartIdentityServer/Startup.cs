// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuickstartIdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();

            //            services.AddAuthentication(o =>
            //                {
            //                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //                })
            //                .AddJwtBearer(options =>
            //                {
            //                    // base-address of your identityserver
            //                    options.Authority = "http://localhost:5000/identity";
            //                    // name of the API resource
            //                    options.Audience = "api1";
            //                    options.RequireHttpsMetadata = false;
            //                });

            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Trace);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/identity", idsr =>
            {
                app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                idsr.UseStaticFiles();
                idsr.UseIdentityServer();
                idsr.UseMvcWithDefaultRoute();
            });


            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}