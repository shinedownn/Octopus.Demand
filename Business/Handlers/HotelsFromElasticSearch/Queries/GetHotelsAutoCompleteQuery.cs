using Business.Handlers.HotelsFromElasticSearch.ValidationRules;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.ElasticSearch;
using Core.Utilities.ElasticSearch.Models;
using Core.Utilities.Results;
using Entities.AutoCompletes.Dtos;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.HotelsFromElasticSearch.Queries
{
    public class GetHotelsAutoCompleteQuery : IRequest<IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>>
    {
        public string HotelName { get; set; }

        public class GetHotelsHandler : IRequestHandler<GetHotelsAutoCompleteQuery, IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>>
        {
            private readonly IElasticSearch _elasticSearch;
            public GetHotelsHandler(IElasticSearch elasticSearch)
            {
                _elasticSearch = elasticSearch;
            }
            string indexname= "autocomplate-agave";
            [ValidationAspect(typeof(GetHotelsAutoCompleteQueryValidator))]
            public async Task<IDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>> Handle(GetHotelsAutoCompleteQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => {
                    var hotels = _elasticSearch.GetSearchBySimpleQueryString<AutoComplateGuideDto>(new SearchByQueryParameters()
                    {
                        Fields = new string[] { "name", "ReplaceName" },
                        IndexName = indexname,
                        From = 0,
                        Size = 10000,
                        Query = "*" + request.HotelName + "*",
                    }).GetAwaiter().GetResult();

                    return new SuccessDataResult<IEnumerable<ElasticSearchGetModel<AutoComplateGuideDto>>>(hotels.Where(x=>x.Item.type=="Otel"));
                }); 
            }
        }
    } 
}
