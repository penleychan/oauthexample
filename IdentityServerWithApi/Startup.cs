using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServerWithApi.QuickStart;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityServerWithApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();

			services.AddIdentityServer()
				.AddDeveloperSigningCredential()
				.AddInMemoryIdentityResources(_identityResources)
				.AddInMemoryApiResources(_apiResources)
				.AddInMemoryClients(_clients)
				.AddTestUsers(TestUsers.Users);

			services.AddAuthentication("Bearer")
				.AddIdentityServerAuthentication(options =>
				{
					options.Authority = "https://localhost:57282";
					options.RequireHttpsMetadata = false;
					options.ApiName = "api1";
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			loggerFactory.AddConsole();

			app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

			app.UseAuthentication();
			app.UseIdentityServer();

			app.UseStaticFiles();
			app.UseMvcWithDefaultRoute();
		}

		private readonly List<IdentityResource> _identityResources = new List<IdentityResource>
		{
			new IdentityResources.OpenId(),
			new IdentityResources.Profile()
		};

		private readonly List<ApiResource> _apiResources = new List<ApiResource>
		{
			new ApiResource("api1", "My Api #1")
		};

		private readonly List<Client> _clients = new List<Client>
		{
			new Client
			{
				ClientId = "angular_spa",
				ClientName = "Angular 4 Client",
				AllowedGrantTypes = GrantTypes.Implicit,
				AllowedScopes = new List<string> {"openid", "profile", "api1"},
				RedirectUris = new List<string> {"http://localhost:4200/auth-callback", "http://localhost:4200/silent-refresh.html"},
				PostLogoutRedirectUris = new List<string> {"http://localhost:4200/"},
				AllowedCorsOrigins = new List<string> {"http://localhost:4200"},
				AllowAccessTokensViaBrowser = true
			}
		};
	}
}