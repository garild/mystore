using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyStore.Core.Domain;
using MyStore.Infrastructure;
using MyStore.Infrastructure.EF;
using MyStore.Infrastructure.Jwt;
using MyStore.Services;
using MyStore.Web.Framework;
using MyStore.Web.Services;

namespace MyStore.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMemoryCache();
            services.AddResponseCaching();
            services.Configure<AppOptions>(Configuration.GetSection("app"));
            services.Configure<SqlOptions>(Configuration.GetSection("sql"));
            services.Configure<JwtOptions>(Configuration.GetSection("jwt"));
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
//            services.AddTransient<ICartProvider, CartProvider>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var jwtSection = Configuration.GetSection("jwt");
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(c =>
                {
                    c.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                        ValidIssuer = jwtOptions.Issuer,
                        ValidateAudience = jwtOptions.ValidateAudience,
                        ValidateLifetime = jwtOptions.ValidateLifetime
                    };
                })
                .AddCookie(c =>
                {
                    c.LoginPath = new PathString("/login");
                    c.AccessDeniedPath = new PathString("/forbidden");
                    c.ExpireTimeSpan = TimeSpan.FromDays(1);
                });
            

            services.AddAuthorization(c => c.AddPolicy("admin", p => { p.RequireRole("admin"); }));

            services.AddEntityFrameworkSqlServer()
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<MyStoreContext>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsImplementedInterfaces();
            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<ServicesModule>();
            
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime lifetime, IOptions<AppOptions> appOptions,
            ILoggerFactory loggerFactory, MyStoreContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
//                app.UseHsts();
            }

//            app.UseHttpsRedirection();

            context.Database.EnsureCreated();
            context.Database.Migrate();
            
            app.UseResponseCaching();
            app.UseAuthentication();
            
            Console.WriteLine($"Started application: {appOptions.Value.Name}");
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseStaticFiles();
            app.UseCookiePolicy();

//            app.Use(async (ctx, next) =>
//            {
//                Console.WriteLine("Start");
//                await next();
//                Console.WriteLine("End");
//            });
//
//            app.Run(async ctx => { Console.WriteLine("RUN"); });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            lifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
