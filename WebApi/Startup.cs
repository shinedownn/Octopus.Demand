using Business;
using Business.Helpers;
using Consul;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.IdentityServer;
using Core.Utilities.Security.Jwt;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Steeltoe.Discovery.Client;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebAPI.Hubs;
using WebAPI.Roles;
using Action = Entities.Concrete.Action;
namespace WebAPI
{
    /// <summary>
    ///
    /// </summary>
    /// 
    public partial class Startup : BusinessStartup
    {
        //private const string TokenUrl = "https://teststsidentity.gezinomi.com/connect/token";
        //private const string AuthorizeUrl = "https://teststsidentity.gezinomi.com/connect/authorize";
        //string Audience = "demand_api_resource";
        //string client_id = "octopus_demand";
        //string client_secret = "b35add90-0e8c-3df5-896f-2dff1f8bfa37";
        //string client_name = "demand_test";
        //string AuthorityUrl = "https://teststsidentity.gezinomi.com";
        readonly string MyAllowSpecificOrigins = "AllowOrigin";
        /// <summary>
        ///
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostEnvironment"></param>
        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
            : base(configuration, hostEnvironment)
        {
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            // Business katmanında olan dependency tanımlarının bir metot üzerinden buraya implemente edilmesi.             
            services.AddDiscoveryClient(Configuration);

            services.AddAutoMapper(typeof(AutoMapperHelper).Assembly);

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials()
                        .Build();
                });
            });
            services.AddSignalR();
            services.AddControllers(opt =>
            {

            })
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //options.JsonSerializerOptions.IgnoreNullValues = true; 
            }).AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            var identityServerTokenOptions = Configuration.GetSection("IdentityServerConfiguration").Get<IdentityServerTokenOptions>();

            services.AddAuthorization(option =>
            {
                option.AddPolicy("PublicSecure", new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                .RequireAuthenticatedUser().RequireRole(DemandRoles.Read, DemandRoles.Write, DemandRoles.Admin).Build());
                option.DefaultPolicy = option.GetPolicy("PublicSecure");
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityServerTokenOptions.AuthorityUrl;
                options.Audience = identityServerTokenOptions.Audience;
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    RoleClaimType = ClaimTypes.Role,
                    ValidateAudience = false,
                };
                options.Validate();
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = (context) =>
                    {

                        if (string.IsNullOrEmpty(context.Token))
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/fgshub"))
                            {
                                context.Token = accessToken;
                            }
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Response.OnStarting(async () =>
                        {
                            //context.NoResult();
                            context.Response.Headers.Add("Token-Expired", "true");
                            context.Response.ContentType = "text/plain";
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                            await context.Response.WriteAsync(context.Exception.Message);
                        });

                        return Task.CompletedTask;
                    },
                };
            });


            services.AddSwaggerGen(c =>
            {
                //c.IncludeXmlComments(Path.ChangeExtension(typeof(Startup).Assembly.Location, ".xml"));
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
                {
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows()
                    {
                        Password = new Microsoft.OpenApi.Models.OpenApiOAuthFlow()
                        {

                            TokenUrl = new Uri(identityServerTokenOptions.TokenUrl),
                            AuthorizationUrl = new Uri(identityServerTokenOptions.AuthorizeUrl),

                            Scopes = new Dictionary<string, string> {
                                { "demand_api", "demand_api" },
                                { "email","email" },
                                { "octopus_identity_admin_api","octopus_identity_admin_api" },
                                { "offline_access","offline_access" },
                                { "openid","openid" },
                                { "profile","profile" },
                                { "roles","roles" }
                            },
                        }
                    }
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Type=SecuritySchemeType.OAuth2,
                             In=ParameterLocation.Header,

                        },new List<string>()
                    }
                });

                c.AddSignalRSwaggerGen();
            });
            services.AddTransient<FileLogger>();
            services.AddTransient<PostgreSqlLogger>();
            services.AddTransient<MsSqlLogger>();
            services.AddCors();

            base.ConfigureServices(services);
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param> 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // VERY IMPORTANT. Since we removed the build from AddDependencyResolvers, let's set the Service provider manually.
            // By the way, we can construct with DI by taking type to avoid calling static methods in aspects.
            ServiceTool.ServiceProvider = app.ApplicationServices;
            var configurationManager = app.ApplicationServices.GetService<Business.ConfigurationManager>();
            switch (configurationManager.Mode)
            {
                case ApplicationMode.Development:
                    Seeder();
                    break;
                case ApplicationMode.Profiling:
                    Seeder();
                    break;
                case ApplicationMode.Staging: 
                    Seeder();
                    break;
                case ApplicationMode.Production: 
                    Seeder();
                    break;
            }
            app.UseCors(MyAllowSpecificOrigins);
            app.UseDeveloperExceptionPage();

            app.ConfigureCustomExceptionMiddleware();
            var identityServerTokenOptions = Configuration.GetSection("IdentityServerConfiguration").Get<IdentityServerTokenOptions>();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Octopus.Demand"); 
                c.OAuthClientId(identityServerTokenOptions.ClientId);
                c.OAuthClientSecret(identityServerTokenOptions.ClientSecret);
                c.EnablePersistAuthorization();
                c.OAuthUsePkce();
            });

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            // Make Turkish your default language. It shouldn't change according to the server.
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("tr-TR"),
            });
            app.UseStatusCodePages();

            var cultureInfo = new CultureInfo("tr-TR");
            cultureInfo.DateTimeFormat.ShortTimePattern = "HH:mm";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<FgsHub>("/fgshub");
            });

        }

        private void Seeder()
        {
            var context = ServiceTool.ServiceProvider.GetRequiredService<ProjectDbContext>();
            //context.Database.Migrate();
            if (context.Actions.Count() == 0)
            {
                context.Actions.AddRange(new List<Action>() {
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Rezervasyon Tamamlandı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Fiyat ve Müsaitlik Bilgisi Verildi", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Başka Firmadan Alındı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="İletişim Bilgisi Hatalı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Mükerrer Kayıt", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Acente Misafiri", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Chat", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Anket Yapıldı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Misafir Tatile Gitmekten Vazgeçti - Diğer", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Misafir Tatile Gitmekten Vazgeçti  - Fiyat Sorunu", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Misafir Tatile Gitmekten Vazgeçti  - Kontenjan Sorunu", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Main", IsDeleted=false, IsOpen=false, Name="Misafire Satış Sonrası Bilgilendirme Yapıldı", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak-Ulaşılamadı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak-Ulaşılamadı", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak-Müşteri Bilgi Bekliyor", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak-Müşteri Bilgi Bekliyor", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak- Randevu Alındı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Tekrar Aranacak- Randevu Alındı", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Fiyat ve Müsaitlik Bilgisi Verildi - Dönüş Bekleniyor", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Fiyat ve Müsaitlik Bilgisi Verildi - Dönüş Bekleniyor", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Satış Temsilcisine Aktarıldı", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Satış Temsilcisine Aktarıldı", CreateDate=DateTime.Now, CreatedUserName="system" },

                   new Action(){ ActionType="Hotel", IsDeleted=false, IsOpen=true, Name="Ön Rezervasyon Oluşturuldu-Ödeme Bekleniyor", CreateDate=DateTime.Now, CreatedUserName="system" },
                   new Action(){ ActionType="Tour", IsDeleted=false, IsOpen=true, Name="Ön Rezervasyon Oluşturuldu-Ödeme Bekleniyor", CreateDate=DateTime.Now, CreatedUserName="system" },
                });
            };
            if (context.OnRequests.Count() == 0)
            {
                context.OnRequests.Add(new OnRequest() { Name = "Havale", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Çekim Hatası (Ödeme alınamadı)", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Çekim İptali", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Müsaitlik", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Çocuk Free", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Bay Konaklama", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Diğer Operatör Fiyat Farkı", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "İade", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Şikayet", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Yapılan Rezervasyon Değişikliği", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Bilgilendirme - Call Center", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                context.OnRequests.Add(new OnRequest() { Name = "Bilgilendirme - Kontrat", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });
                //context.OnRequests.Add(new OnRequest() { Name = "", CreateDate = DateTime.Now, IsDeleted = false, CreatedUserName = "system"  });




                //context.OnRequests.AddRange(new List<OnRequest>()
                //{
                //    new OnRequest(){ Name="Havale", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Çekim Hatası (Ödeme alınamadı)", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Çekim İptali", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Müsaitlik", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Çocuk Free", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Bay Konaklama", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Diğer Operatör Fiyat Farkı", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="İade", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Şikayet", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Yapılan Rezervasyon Değişikliği", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Bilgilendirme - Call Center", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //    new OnRequest(){ Name="Bilgilendirme - Kontrat", CreateDate=DateTime.Now, IsDeleted=false, CreatedUserName="system" },
                //});
            }
            if (context.Departments.Count() == 0)
            {
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Call Center", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Yurt içi operasyon Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Yurtdışı Operasyon Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Kültür Turları Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Uçak Bileti Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Müşteri İlişkileri Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Satış Sonrası Destek", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Kontrat", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Fiyatlandırma Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Muhasebe Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Pazarlama Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Acente Müdürü", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - İnsan Kaynakları", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Kıbrıs Operasyon Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" });
                context.Departments.Add(new Department() { Name = "Bilgilendirme - Kurumsal", CreateDate = DateTime.Now, CreatedUserName = "system" });
                //context.Departments.Add(new Department() { Name = "Bilgilendirme - Operasyon Departmanı", CreateDate = DateTime.Now, CreatedUserName = "system" }); 
            }
            if (context.OnRequestApprovements.Count() == 0)
            {
                context.OnRequestApprovements.AddRange(new List<OnRequestApprovement>()
                {
                    //Bilgilendirme - Muhasebe Departmanı (10)
                    new OnRequestApprovement(){ OnRequestId=1, DepartmentId=10, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=2, DepartmentId=10, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=3, DepartmentId=10, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Operasyon Departmanı (16)
                    new OnRequestApprovement(){ OnRequestId=1, DepartmentId=16, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=5, DepartmentId=16, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=6, DepartmentId=16, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Fiyatlandırma Departmanı (9)
                    new OnRequestApprovement(){ OnRequestId=7, DepartmentId=9, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Kültür Turları Departmanı (4)
                    new OnRequestApprovement(){ OnRequestId=4, DepartmentId=1, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Müşteri İlişkileri Departmanı (6)
                    new OnRequestApprovement(){ OnRequestId=8, DepartmentId=6, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=9, DepartmentId=6, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Kıbrıs Departmanı (14) 
                    new OnRequestApprovement(){ OnRequestId=1, DepartmentId=14, CreatedUserName="system", CreateDate= DateTime.Now},
                    new OnRequestApprovement(){ OnRequestId=5, DepartmentId=14, CreatedUserName="system", CreateDate= DateTime.Now},

                    //Bilgilendirme - Satış Sonrası Destek (7)
                    new OnRequestApprovement(){ OnRequestId=10, DepartmentId=7, CreatedUserName="system", CreateDate= DateTime.Now},  
                });
            }
            if (context.RequestChannels.Count() == 0)
            {
                context.RequestChannels.AddRange(new List<RequestChannel>()
                { 
                    new RequestChannel(){ Name="Otel Booking",                                        DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Gezinomi",                                            DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Chat Talep",                                          DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Sizi Arayalım Formu",                                 DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Gezinomi Biz",                                        DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Rehber Kanalı",                                       DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Kurumsal",                                            DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Grup İçi Firmalar",                                   DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Yurtiçi Operatörler",                                 DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Şubeler",                                             DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                    new RequestChannel(){ Name="Fly Express",                                         DepartmentId=1, IsDeleted=false, CreateDate=DateTime.Now, CreatedUserName="system" }, 
                     
                });
            }

            context.SaveChanges();
        }
    }
}