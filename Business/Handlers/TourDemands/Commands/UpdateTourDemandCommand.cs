using Business.BusinessAspects;
using Business.Constants;
using Business.Handlers.Demands.ValidationRules;
using Business.Handlers.TourDemands.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
    public class UpdateTourDemandCommand : IRequest<IResult>
    {
        public int TourDemandId { get; set; }
        public int MainDemandId { get; set; }
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public bool IsOpen { get; set; }
        public string Description { get; set; }

        public class UpdateTourDemandCommandHandler : IRequestHandler<UpdateTourDemandCommand, IResult>
        {
            private readonly ITourDemandRepository _tourDemandRepository;
            public UpdateTourDemandCommandHandler(ITourDemandRepository tourDemandRepository)
            {
                _tourDemandRepository = tourDemandRepository;
            }

            [ValidationAspect(typeof(UpdateTourDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi güncellendi",Priority =3)]
            public async Task<IResult> Handle(UpdateTourDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var isThereTourDemand = _tourDemandRepository.GetAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult();

                    if (isThereTourDemand == null)
                        return new ErrorResult(Messages.TourDemandNotFound);

                    isThereTourDemand.MainDemandId = request.MainDemandId;
                    isThereTourDemand.AdultCount = request.AdultCount;
                    isThereTourDemand.ChildCount = request.ChildCount;
                    isThereTourDemand.Period = request.Period;
                    isThereTourDemand.TourId = request.TourId;
                    isThereTourDemand.Name = request.Name;
                    isThereTourDemand.Description = request.Description;
                    isThereTourDemand.TotalCount = request.AdultCount + request.ChildCount;
                    isThereTourDemand.IsOpen = request.IsOpen;

                    _tourDemandRepository.Update(isThereTourDemand);
                    _tourDemandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                }); 
            }
        }
    }
}
