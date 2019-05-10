using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using angu.Data;
using angu.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
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
using Swashbuckle.AspNetCore.Swagger;

namespace angu
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

            services.AddDbContext<DataContext>
                    (options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); 
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(option => {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors();
            services.AddScoped<IAuthRespository , AuthRepository>();
            services.AddScoped<IDatingRespository , DatingRepository>();
            services.AddAutoMapper();
    
             services.AddSwaggerGen(c => {  
                c.SwaggerDoc("v1", new Info {  
                    Version = "v1",  
                        Title = "My First API",  
                        Description = "My First ASP.NET Core 2.0 Web API",  
                        TermsOfService = "None",  
                          
                });  
            });  

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer( options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the ruDefaultConnectionntime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(
                    builder => {
                        builder.Run(async context => {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            var error = context.Features.Get<IExceptionHandlerFeature>();

                            if(error != null){
                                context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message);
                            }

                        });
                    }
                );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }


            // app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() );
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {  
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");  
            }); 
        }
    }
}
