using Core.Utilities.Results;
using DataAccess.Abstract.ErcanProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ErcanProduct.NumberRange.Queries
{
    public class GetNumberRangeQuery : IRequest<IDataResult<int>>
    {
        public class GetNumberRangeQueryHandler : IRequestHandler<GetNumberRangeQuery, IDataResult<int>>
        {
            private readonly INumberRangeRepository _numberRangeRepository;
            public GetNumberRangeQueryHandler(INumberRangeRepository numberRangeRepository)
            {
                _numberRangeRepository = numberRangeRepository;
            }
            public async Task<IDataResult<int>> Handle(GetNumberRangeQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(async () =>
                {
                    var numberRange = await _numberRangeRepository.GetAsync(x => x.Prefix == "REQ");
                    numberRange.Value++;
                    _numberRangeRepository.Update(numberRange);
                    await _numberRangeRepository.SaveChangesAsync();
                    return new SuccessDataResult<int>("REQ" + Convert.ToInt32(numberRange.Value + 1).ToString().PadLeft(6, '0'));
                });
            }
        }
    }
}
