using Business.BusinessAspects;
using Business.Constants;
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
    public class DeleteTourDemandCommand : IRequest<IResult>
    {
         public int TourDemandId { get; set; }

        public class DeleteTourDemandCommandHandler : IRequestHandler<DeleteTourDemandCommand, IResult>
        {
            private readonly ITourDemandRepository _tourDemandRepository;
            public DeleteTourDemandCommandHandler(ITourDemandRepository tourDemandRepository)
            {
                _tourDemandRepository = tourDemandRepository;
            }

            [ValidationAspect(typeof(DeleteTourDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteTourDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var tourdemandToDelete = _tourDemandRepository.GetAsync(x => x.TourDemandId == request.TourDemandId).GetAwaiter().GetResult();

                    if (tourdemandToDelete == null)
                        return new ErrorResult(Messages.TourDemandNotFound);
                    if (tourdemandToDelete.IsOpen) return new ErrorResult(Messages.DemandIsOpenCannotDelete);

                    tourdemandToDelete.IsDeleted = true;
                    _tourDemandRepository.SaveChangesAsync();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}
