using Autofac;
using AutoMapper;
using Business.Adapters.ContactService;
using Business.Constants;
using Business.DependencyResolvers;
using Business.Fakes.DArch;
using Business.Helpers;
using Consul;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.ElasticSearch;
using Core.Utilities.IoC;
using Core.Utilities.MessageBrokers.RabbitMq;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Abstract.ErcanProduct;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Concrete.EntityFramework.ErcanProduct;
using DataAccess.Concrete.MongoDb.Context;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using Action = Entities.Concrete.Action;
namespace Business
{
    public partial class BusinessStartup
    {
        public BusinessStartup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }

        protected IHostEnvironment HostEnvironment { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <remarks>
        /// It is common to all configurations and must be called. Aspnet core does not call this method because there are other methods.
        /// </remarks>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            Func<IServiceProvider, ClaimsPrincipal> getPrincipal = (sp) =>
                sp.GetService<IHttpContextAccessor>().HttpContext?.User ??
                new ClaimsPrincipal(new ClaimsIdentity(Messages.Unknown));

            services.AddScoped<IPrincipal>(getPrincipal);
            services.AddMemoryCache();

            var coreModule = new CoreModule();

            services.AddDependencyResolvers(Configuration, new ICoreModule[] { coreModule }); 

            services.AddSingleton<ConfigurationManager>();

            services.AddTransient<IElasticSearch, ElasticSearchManager>();

            services.AddTransient<IMessageBrokerHelper, MqQueueHelper>();
            services.AddTransient<IMessageConsumer, MqConsumerHelper>();
            //services.AddSingleton<ICacheManager, MemoryCacheManager>(); 
            services.AddSingleton<ICacheManager, RedisCacheManager>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //services.AddMediatR(typeof(BusinessStartup).GetTypeInfo().Assembly); 


            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) =>
            {
                return memberInfo.GetCustomAttribute<System.ComponentModel.DataAnnotations.DisplayAttribute>()
                    ?.GetName();
            };
        }

        /// <summary>
        /// This method gets called by the Development
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IReminderRepository, ReminderRepository>();
            services.AddTransient<IOnRequestApprovementRepository, OnRequestApprovementRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IHotelDemandActionRepository, HotelDemandActionRepository>();
            services.AddTransient<IHotelDemandRepository, HotelDemandRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IMainDemandActionRepository, MainDemandActionRepository>();
            services.AddTransient<IMainDemandRepository, MainDemandRepository>();
            services.AddTransient<ITourDemandActionRepository, TourDemandActionRepository>();
            services.AddTransient<ITourDemandRepository, TourDemandRepository>();
            services.AddTransient<ITourDemandOnRequestRepository, TourDemandOnRequestRepository>();
            services.AddTransient<IHotelDemandOnRequestRepository, HotelDemandOnRequestRepository>();
            services.AddTransient<IOnRequestRepository, OnRequestRepository>();
            services.AddTransient<DataAccess.Abstract.IRequestChannelRepository, DataAccess.Concrete.EntityFramework.RequestChannelRepository>();

            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<ITourRepository, TourRepository>();
            services.AddTransient<ITourPeriodRepository, TourPeriodRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<INumberRangeRepository, NumberRangeRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactPhoneRepository, ContactPhoneRepository>();
            services.AddTransient<IContactSegmentTypeRepository, ContactSegmentTypeRepository>();
            services.AddTransient<IHotelPermaLinkRepository, HotelPermaLinkRepository>();
            services.AddTransient<ITourPermaLinkRepository, TourPermaLinkRepository>();
            services.AddTransient<ICallCenterPersonelRepository, CallCenterPersonelRepository>();
            services.AddDbContext<ErcanProductDbContext>();
            services.AddDbContext<ProjectDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }

        /// <summary>
        /// This method gets called by the Staging
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureStagingServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IReminderRepository, ReminderRepository>();
            services.AddTransient<IOnRequestApprovementRepository, OnRequestApprovementRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IHotelDemandActionRepository, HotelDemandActionRepository>();
            services.AddTransient<IHotelDemandRepository, HotelDemandRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IMainDemandActionRepository, MainDemandActionRepository>();
            services.AddTransient<IMainDemandRepository, MainDemandRepository>();
            services.AddTransient<ITourDemandActionRepository, TourDemandActionRepository>();
            services.AddTransient<ITourDemandRepository, TourDemandRepository>();
            services.AddTransient<ITourDemandOnRequestRepository, TourDemandOnRequestRepository>();
            services.AddTransient<IHotelDemandOnRequestRepository, HotelDemandOnRequestRepository>();
            services.AddTransient<IOnRequestRepository, OnRequestRepository>();
            services.AddTransient<DataAccess.Abstract.IRequestChannelRepository, DataAccess.Concrete.EntityFramework.RequestChannelRepository>();

            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<ITourRepository, TourRepository>();
            services.AddTransient<ITourPeriodRepository, TourPeriodRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<INumberRangeRepository, NumberRangeRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactPhoneRepository, ContactPhoneRepository>();
            services.AddTransient<IContactSegmentTypeRepository, ContactSegmentTypeRepository>();
            services.AddTransient<IHotelPermaLinkRepository, HotelPermaLinkRepository>();
            services.AddTransient<ITourPermaLinkRepository, TourPermaLinkRepository>();
            services.AddTransient<ICallCenterPersonelRepository, CallCenterPersonelRepository>();
            services.AddDbContext<ErcanProductDbContext>();
            services.AddDbContext<ProjectDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
            services.AddTransient<IContactService, ContactServiceManager>(); 
        }

        /// <summary>
        /// This method gets called by the Production
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureProductionServices(IServiceCollection services)
        {
            ConfigureServices(services);
            services.AddTransient<IReminderRepository, ReminderRepository>();
            services.AddTransient<IOnRequestApprovementRepository, OnRequestApprovementRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IActionRepository, ActionRepository>();
            services.AddTransient<IHotelDemandActionRepository, HotelDemandActionRepository>();
            services.AddTransient<IHotelDemandRepository, HotelDemandRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IMainDemandActionRepository, MainDemandActionRepository>();
            services.AddTransient<IMainDemandRepository, MainDemandRepository>();
            services.AddTransient<ITourDemandActionRepository, TourDemandActionRepository>();
            services.AddTransient<ITourDemandRepository, TourDemandRepository>();
            services.AddTransient<ITourDemandOnRequestRepository, TourDemandOnRequestRepository>();
            services.AddTransient<IHotelDemandOnRequestRepository, HotelDemandOnRequestRepository>();
            services.AddTransient<IOnRequestRepository, OnRequestRepository>();
            services.AddTransient<DataAccess.Abstract.IRequestChannelRepository, DataAccess.Concrete.EntityFramework.RequestChannelRepository>();

            services.AddTransient<IHotelRepository, HotelRepository>();
            services.AddTransient<ITourRepository, TourRepository>();
            services.AddTransient<ITourPeriodRepository, TourPeriodRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<INumberRangeRepository, NumberRangeRepository>();
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IContactPhoneRepository, ContactPhoneRepository>();
            services.AddTransient<IContactSegmentTypeRepository, ContactSegmentTypeRepository>();
            services.AddTransient<IHotelPermaLinkRepository, HotelPermaLinkRepository>();
            services.AddTransient<ITourPermaLinkRepository, TourPermaLinkRepository>();
            services.AddTransient<ICallCenterPersonelRepository, CallCenterPersonelRepository>();
            services.AddDbContext<ErcanProductDbContext>();
            services.AddDbContext<ProjectDbContext>();
            services.AddSingleton<MongoDbContextBase, MongoDbContext>();
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        { 
            builder.RegisterModule(new AutofacBusinessModule(new ConfigurationManager(Configuration, HostEnvironment)));
        } 
    }
}
