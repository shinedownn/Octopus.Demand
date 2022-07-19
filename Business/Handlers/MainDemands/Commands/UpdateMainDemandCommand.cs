
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Demands.ValidationRules;
using System.ComponentModel.DataAnnotations;

namespace Business.Handlers.Demands.Commands
{


    public class UpdateMainDemandCommand : IRequest<IResult>
    {
        public int ContactId { get; set; }
        public int MainDemandId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DemandChannel { get; set; }
        public bool IsOpen { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
        public string CountryCode { get; set; }
        public string AreaCode { get; set; }

        public class UpdateDemandCommandHandler : IRequestHandler<UpdateMainDemandCommand, IResult>
        {
            private readonly IMainDemandRepository _demandRepository;
            public UpdateDemandCommandHandler(IMainDemandRepository demandRepository)
            {
                _demandRepository = demandRepository;
            }

            [ValidationAspect(typeof(UpdateDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Genel talep bilgisi güncellendi", Priority = 3)]
            public async Task<IResult> Handle(UpdateMainDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var isThereDemandRecord = _demandRepository.GetAsync(u => u.MainDemandId == request.MainDemandId).GetAwaiter().GetResult();

                    if (isThereDemandRecord == null)
                        return new ErrorResult(Messages.RecordNotFound);
                    isThereDemandRecord.ContactId = request.ContactId;
                    isThereDemandRecord.MainDemandId = request.MainDemandId;
                    isThereDemandRecord.Name = request.Name;
                    isThereDemandRecord.Surname = request.Surname;
                    isThereDemandRecord.PhoneNumber = request.PhoneNumber;
                    isThereDemandRecord.Email = request.Email;
                    isThereDemandRecord.Description = request.Description;
                    isThereDemandRecord.IsOpen = request.IsOpen;
                    isThereDemandRecord.DemandChannel = request.DemandChannel;
                    isThereDemandRecord.IsFirm = request.IsFirm;
                    isThereDemandRecord.FirmName = request.FirmName;
                    isThereDemandRecord.FirmTitle = request.FirmTitle;
                    isThereDemandRecord.AreaCode = request.AreaCode;
                    isThereDemandRecord.CountryCode = request.CountryCode;

                    _demandRepository.Update(isThereDemandRecord);
                    _demandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Updated);
                });
            }
        }
    }
}

