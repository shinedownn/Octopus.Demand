using AutoMapper;
using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.TourDemands.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.TourDemands.Commands
{
    public class CreateTourDemandCommand : IRequest<IResult>
    {
        public int MainDemandId { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public string Description { get; set; }

        public class CreateTourDemandCommandHandler : IRequestHandler<CreateTourDemandCommand, IResult>
        {
            private readonly ITourDemandRepository _tourDemandRepository;

            public CreateTourDemandCommandHandler(ITourDemandRepository tourDemandRepository)
            {
                _tourDemandRepository = tourDemandRepository;
            }
            [ValidationAspect(typeof(CreateTourDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi oluşturuldu",Priority =3)]
            public async Task<IResult> Handle(CreateTourDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var addedTourDemand = new TourDemand
                    {
                        MainDemandId = request.MainDemandId,
                        Period = request.Period,
                        TourId = request.TourId,
                        Name = request.Name,
                        Description = request.Description,
                        AdultCount = request.AdultCount,
                        ChildCount = request.ChildCount,
                        TotalCount = request.AdultCount + request.ChildCount,
                        IsOpen = true,
                        IsDeleted = false,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.Now,
                    };

                    _tourDemandRepository.Add(addedTourDemand);
                    _tourDemandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                }); 
            }
        }
    }
}
