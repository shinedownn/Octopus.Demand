using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Demands.ValidationRules;
using System.ComponentModel.DataAnnotations;
using System;
using Business.Helpers;
using DataAccess.Abstract.ErcanProduct;

namespace Business.Handlers.Demands.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMainDemandCommand : IRequest<IResult>
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string DemandChannel { get; set; }
        public bool IsFirm { get; set; }
        public string FirmName { get; set; }
        public string FirmTitle { get; set; }
        public string AreaCode { get; set; }
        public string CountryCode { get; set; }

        public class CreateDemandCommandHandler : IRequestHandler<CreateMainDemandCommand, IResult>
        {
            private readonly IMainDemandRepository _demandRepository;
            private readonly INumberRangeRepository _numberRangeRepository;
            public CreateDemandCommandHandler(IMainDemandRepository demandRepository, IMediator mediator, INumberRangeRepository numberRangeRepository)
            {
                _demandRepository = demandRepository;
                _numberRangeRepository = numberRangeRepository;
            }

            [ValidationAspect(typeof(CreateMainDemandValidator), Priority = 1)]
            [LogAspect(typeof(PostgreSqlLogger), "Genel talep bilgisi oluşturuldu", Priority = 3)]
            public async Task<IResult> Handle(CreateMainDemandCommand request, CancellationToken cancellationToken)
            {
                return await Task.Run<IResult>(() =>
                {
                    var numberRange = _numberRangeRepository.GetAsync(x => x.Prefix == "REQ").GetAwaiter().GetResult();
                    numberRange.Value++;
                    _numberRangeRepository.Update(numberRange);
                    _numberRangeRepository.SaveChangesAsync().GetAwaiter().GetResult();
                    string newNumberRange = "TLP" + Convert.ToInt32(numberRange.Value + 1).ToString().PadLeft(6, '0');

                    var isThereDemandRecord = _demandRepository.Query().Any(u => u.Name == request.Name);

                    if (isThereDemandRecord == true)
                        return new ErrorResult(Messages.NameAlreadyExist);

                    var addedDemand = new MainDemand
                    {
                        ContactId = request.ContactId,
                        Name = request.Name,
                        Surname = request.Surname,
                        PhoneNumber = request.PhoneNumber,
                        Email = request.Email,
                        Description = request.Description,
                        DemandChannel = request.DemandChannel,
                        IsOpen = true,
                        IsDeleted = false,
                        CreatedUserName = JwtHelper.GetValue("name").ToString(),
                        CreateDate = DateTime.Now,
                        IsFirm = request.IsFirm,
                        FirmName = request.FirmName,
                        FirmTitle = request.FirmTitle,
                        AreaCode = request.AreaCode,
                        CountryCode = request.CountryCode,
                        RequestCode = newNumberRange
                    };

                    _demandRepository.Add(addedDemand);
                    _demandRepository.SaveChangesAsync().GetAwaiter();
                    return new SuccessResult(Messages.Added);
                });
            }
        }
    }
}