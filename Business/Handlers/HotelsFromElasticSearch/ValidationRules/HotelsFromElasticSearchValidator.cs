using Business.Handlers.HotelsFromElasticSearch.Queries;
using Entities.AutoCompletes.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.HotelsFromElasticSearch.ValidationRules
{
    public class GetHotelsAutoCompleteQueryValidator : AbstractValidator<GetHotelsAutoCompleteQuery>
    {
        public GetHotelsAutoCompleteQueryValidator()
        {
            RuleFor(x => x.HotelName).MinimumLength(3);
        }
    }
}
