using Business.Constants;
using Business.Handlers.TourDemandActions.ValidationRules;
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

namespace Business.Handlers.TourDemandActions.Commands
{
    public class DeleteTourDemandActionCommand : IRequest<IResult>
    {
         public int ActionId { get; set; }

        public class DeleteTourDemandActionCommandHandler : IRequestHandler<DeleteTourDemandActionCommand, IResult>
        {
            private readonly ITourDemandActionRepository _tourDemandActionRepository;
            public DeleteTourDemandActionCommandHandler(ITourDemandActionRepository tourDemandActionRepository)
            {
                _tourDemandActionRepository = tourDemandActionRepository;
            }
            [ValidationAspect(typeof(DeleteTourDemandActionValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger),"Tur talebi silindi",Priority =3)]
            public async Task<IResult> Handle(DeleteTourDemandActionCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() => {
                    var deleteToAction = _tourDemandActionRepository.GetAsync(x => x.ActionId == request.ActionId).GetAwaiter().GetResult();
                    if (deleteToAction == null) new ErrorResult(Messages.RecordNotFound);
                    if (deleteToAction.IsOpen) return new ErrorResult(Messages.ActionIsOpenCannotDelete);
                    deleteToAction.IsDeleted = true;
                    _tourDemandActionRepository.Update(deleteToAction);
                    _tourDemandActionRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Deleted);
                }); 
            }
        }
    }
}
