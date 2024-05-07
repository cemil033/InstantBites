using InstantBites.Application.Common;
using InstantBites.Application.Services;
using InstantBites.Domain.Services;
using InstantBites.Infrastructure.Authentication;
using InstantBites.Infrastructure.Middleware;
using InstantBites.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantBites.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuth(configuration);

            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = configuration.GetSection(JwtSetting.SectionName).Get<JwtSetting>();
            var sendgrid=configuration.GetSection(SendGridOption.SectionName).Get<SendGridOption>();            
            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton(Options.Create(sendgrid));    
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();            
            services.AddTransient<IEmailService,EmailService>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });
           
            return services;
        }

    }
}
