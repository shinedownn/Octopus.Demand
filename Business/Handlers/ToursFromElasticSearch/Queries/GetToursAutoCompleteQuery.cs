using Business.Handlers.ToursFromElasticSearch.ValidationRules;
using Business.Helpers;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.ElasticSearch;
using Core.Utilities.ElasticSearch.Models;
using Core.Utilities.Results;
using Entities.AutoCompletes.Dtos;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.ToursFromElasticSearch.Queries
{
    public class GetToursAutoCompleteQuery : IRequest<IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>>
    {
        public string TourName { get; set; }

        public class GetToursAutoCompleteQueryHandler : IRequestHandler<GetToursAutoCompleteQuery, IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>>
        {
            private readonly IElasticSearch _elasticSearch;
            string indexname = "autocomplate-agave";
            public GetToursAutoCompleteQueryHandler(IElasticSearch elasticSearch)
            {
                _elasticSearch = elasticSearch;
            }
            [ValidationAspect(typeof(TourFromElasticSearchValidator))]
            public async Task<IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>> Handle(GetToursAutoCompleteQuery request, CancellationToken cancellationToken)
            { 
                var tours = await _elasticSearch.GetSearchBySimpleQueryString<AutoComplateGuideDto>(new SearchByQueryParameters()
                {
                    Fields = new string[] { "name", "ReplaceName","type" },
                    IndexName = indexname,
                    From = 0,
                    Size = 10000,
                    Query = "*" + request.TourName + "*",
                }); 
                return new SuccessDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>(tours.Where(x=>x.Item.type=="Tur"));
            }
        }
    }
}
