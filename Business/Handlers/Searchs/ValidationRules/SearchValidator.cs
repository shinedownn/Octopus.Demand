using Business.Handlers.Searchs.Queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Searchs.ValidationRules
{
    public class GetOnRequestsSearchQueryValidator : AbstractValidator<GetOnRequestsSearchQuery>
    {
        public GetOnRequestsSearchQueryValidator()
        {
            RuleFor(x => x.StartDate).NotNull().When(x => x.IsOpen == false);
            RuleFor(x => x.EndDate).NotNull().When(x => x.IsOpen == false);
        }
    }

    public class GetDemandsSearchByContactIdQueryValidator : AbstractValidator<GetDemandsSearchByContactIdQuery>
    {
        public GetDemandsSearchByContactIdQueryValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
        }
    }

    public class GetOnRequestsSearchByContactIdQueryValidator : AbstractValidator<GetOnRequestsSearchByContactIdQuery>
    {
        public GetOnRequestsSearchByContactIdQueryValidator()
        {
            RuleFor(x => x.ContactId).GreaterThan(0);
        }
    }
}
