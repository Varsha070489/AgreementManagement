
using SoftwareManagement.Application.DTOs.Settings;
using SoftwareManagement.Application.Interfaces;
using SoftwareManagement.Application.Interfaces.Shared;
using SoftwareManagement.Infrastructure.DbContexts;
using SoftwareManagement.Infrastructure.Shared.Services;
using AspNetCoreHero.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

using SoftwareManagement.Api.Services;

namespace SoftwareManagement.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));
            services.AddTransient<IDateTimeService, SystemDateTimeService>();
            services.AddTransient<IMailService, SMTPMailService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        public static void AddEssentials(this IServiceCollection services)
        {
            services.RegisterSwagger();
            services.AddVersioning();
            services.AddCors();
        }

        private static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //TODO - Lowercase Swagger Documents
                //c.DocumentFilter<LowercaseDocumentFilter>();
                //Refer - https://gist.github.com/rafalkasa/01d5e3b265e5aa075678e0adfd54e23f
                c.IncludeXmlComments(string.Format(@"{0}\SoftwareManagement.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "GirmeSoftTech Manufacturing ERP",
                    Description = "An API to perform ERP operations",
                    TermsOfService = new Uri("https://girmesofttech.com/services.html"),
                    Contact = new OpenApiContact
                    {
                        Name = "Girme Softtech LLP",
                        Email = "info@girmesofttech.com",
                        Url = new Uri("https://girmesofttech.com/contact.html"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Copyright © 2020 varsha. All rights reserved.",
                        Url = new Uri("https://girmesofttech.com/contact.html")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        private static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection")));
            services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("TenantConnection")));
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(o =>
                 {
                     o.RequireHttpsMetadata = false;
                     o.SaveToken = false;
                     o.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero,
                         ValidIssuer = configuration["JWTSettings:Issuer"],
                         ValidAudience = configuration["JWTSettings:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                     };
                     o.Events = new JwtBearerEvents()
                     {
                         OnAuthenticationFailed = c =>
                         {
                             c.NoResult();
                             c.Response.StatusCode = 500;
                             c.Response.ContentType = "text/plain";
                             return c.Response.WriteAsync(c.Exception.ToString());
                         },
                         OnChallenge = context =>
                         {
                             context.HandleResponse();
                             context.Response.StatusCode = 401;
                             context.Response.ContentType = "application/json";
                             var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized"));
                             return context.Response.WriteAsync(result);
                         },
                         OnForbidden = context =>
                         {
                             context.Response.StatusCode = 403;
                             context.Response.ContentType = "application/json";
                             var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource"));
                             return context.Response.WriteAsync(result);
                         },
                     };
                 });
        }
    }
}