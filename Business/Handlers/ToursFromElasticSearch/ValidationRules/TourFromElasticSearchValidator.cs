using Business.Handlers.ToursFromElasticSearch.Queries;
using Entities.AutoCompletes.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.ToursFromElasticSearch.ValidationRules
{
    public class TourFromElasticSearchValidator : AbstractValidator<GetToursAutoCompleteQuery>
    {
        public TourFromElasticSearchValidator()
        {
            RuleFor(x => x.TourName).MinimumLength(3);
        }
    }
}
