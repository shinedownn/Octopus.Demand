using Business.Handlers.Demands.Commands;
using Entities.Concrete;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Demands.ValidationRules
{
    public class CreateDemandValidator : AbstractValidator<CreateDemandCommand>
    {
        public CreateDemandValidator()
        {
            RuleFor(x => x.MainDemandDto.Name).NotEmpty().When(a => !a.MainDemandDto.IsFirm);
            RuleFor(x => x.MainDemandDto.Surname).NotEmpty().When(a => !a.MainDemandDto.IsFirm);
            RuleFor(x => x.MainDemandDto.PhoneNumber).NotEmpty();
            RuleFor(x => x.MainDemandDto.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.MainDemandDto.ContactId).GreaterThan(0);
            //RuleFor(x => x.MainDemandDto.Description).NotEmpty();

            RuleFor(x => x.HotelDemandDtos).Must(x => x.Count > 0).Unless(x => x.TourDemandDtos.Count > 0);
            RuleFor(x => x.TourDemandDtos).Must(x => x.Count > 0).Unless(x => x.HotelDemandDtos.Count > 0);

            // RuleForEach(x => x.TourDemandDtos).Where(x => x.OnRequests.Count > 0).ChildRules(a => a.RuleFor(b => b.OnRequests.Any(o => o.ConfirmationRequested) == false));
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.ApprovalRequestedDepartmentId).GreaterThan(0).When(d => d.ConfirmationRequested)));
            RuleForEach(x => x.TourDemandDtos).Where(x => x.Actions.Any() == false);

            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.Name).NotEmpty()).When(x => x.HotelDemandDtos.Count > 0);
            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.HotelId).GreaterThan(0)).When(x => x.HotelDemandDtos.Count > 0);
            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0)).When(x => x.HotelDemandDtos.Count > 0);
            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.ChildCount).GreaterThanOrEqualTo(0)).When(x => x.HotelDemandDtos.Count > 0);
            //RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.Description).NotEmpty()).When(x => x.HotelDemandDtos.Count > 0);
            //RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.OnRequests.Any(o => o.ConfirmationRequested) == true)).When(x => x.HotelDemandDtos.Count > 0); 
            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.ApprovalRequestedDepartmentId).GreaterThan(0).When(d=>d.ConfirmationRequested)));

            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.ActionId).GreaterThan(0))).When(x => x.HotelDemandDtos.Count > 0);
            //RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));

            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.Name).NotEmpty()).When(x => x.TourDemandDtos.Count > 0);
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.TourId).GreaterThan(0)).When(x => x.TourDemandDtos.Count > 0);
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0)).When(x => x.TourDemandDtos.Count > 0);
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.ChildCount).GreaterThanOrEqualTo(0)).When(x => x.TourDemandDtos.Count > 0);
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.Period).NotNull()).When(x => x.TourDemandDtos.Count > 0);
            //RuleForEach(x => x.TourDemandDtos).Where(x => x.OnRequests.Count > 0).ChildRules(a => a.RuleFor(b => b.OnRequests.Any(o => o.ConfirmationRequested) == false));
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.ActionId).GreaterThan(0)));
            //RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));
        }
    }

    public class UpdateDemandValidator : AbstractValidator<UpdateDemandCommand>
    {
        public UpdateDemandValidator()
        {
            RuleFor(x => x.MainDemandDto.MainDemandId).NotNull().NotEmpty();
            RuleFor(x => x.MainDemandDto.Name).NotEmpty().When(a => !a.MainDemandDto.IsFirm);
            RuleFor(x => x.MainDemandDto.Surname).NotEmpty().When(a => !a.MainDemandDto.IsFirm);
            RuleFor(x => x.MainDemandDto.PhoneNumber).NotEmpty();
            RuleFor(x => x.MainDemandDto.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.MainDemandDto.ContactId).GreaterThan(0);
            //RuleFor(x => x.MainDemandDto.Description).NotEmpty();

            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.MainDemandId).GreaterThan(0));
            RuleForEach(x => x.HotelDemandDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0));

            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.MainDemandId).GreaterThan(0));
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0));
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.ChildCount).GreaterThan(-1));
            RuleForEach(x => x.TourDemandDtos).ChildRules(a => a.RuleFor(b => b.Period).NotNull().NotEmpty());
        }
    }

    public class DeleteDemandValidator : AbstractValidator<DeleteDemandCommand>
    {
        public DeleteDemandValidator()
        {
            RuleFor(x => x.MainDemandId).GreaterThan(0);
        }
    }

    public class UpsertDemandValidator : AbstractValidator<UpsertDemandCommand>
    {
        public UpsertDemandValidator()
        {
            RuleFor(x => x.MainDemandUpsertDto.MainDemandId).NotEqual(0);
            RuleFor(x => x.MainDemandUpsertDto.Name).NotEmpty().When(a => !a.MainDemandUpsertDto.IsFirm);
            RuleFor(x => x.MainDemandUpsertDto.Surname).NotEmpty().When(a => !a.MainDemandUpsertDto.IsFirm);
            RuleFor(x => x.MainDemandUpsertDto.PhoneNumber).NotEmpty();
            RuleFor(x => x.MainDemandUpsertDto.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.MainDemandUpsertDto.ContactId).GreaterThan(0);
            //RuleFor(x => x.MainDemandUpsertDto.Description).NotEmpty();

            RuleFor(x => x.MainDemandUpsertDto).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(b => b.RuleFor(c => c.ActionId).GreaterThan(0)));
            RuleFor(x => x.MainDemandUpsertDto).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(b => b.RuleFor(c => c.MainDemandActionId).NotEqual(0)));
            //RuleFor(x => x.MainDemandUpsertDto).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(b => b.RuleFor(c => c.Description).NotEmpty()));

            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.MainDemandId).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.ChildCount).GreaterThan(-1));
            //RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.Description).NotEmpty());
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.HotelDemandId).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.HotelId).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.Name).NotEmpty());

            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.ActionId).GreaterThan(0)));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.HotelDemandId).GreaterThan(0)));
            //RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.HotelDemandActionId).NotEqual(0)));

            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.MainDemandId).NotEqual(0)));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.HotelDemandId).GreaterThan(0)));
            RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.OnRequestId).GreaterThan(0)));
            //RuleForEach(x => x.MainDemandUpsertDto.HotelDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));

            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.MainDemandId).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.AdultCount).GreaterThan(0));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.ChildCount).GreaterThan(-1));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.Period).NotEmpty());
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.TourId).GreaterThan(-1));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.Name).NotEmpty());
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.TourDemandId).NotEqual(0));
            //RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleFor(b => b.Description).NotEmpty());

            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.ActionId).GreaterThan(0)));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.TourDemandId).NotEqual(0)));
            //RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.Actions).ChildRules(c => c.RuleFor(d => d.TourDemandActionId).NotEqual(0)));

            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.MainDemandId).NotEqual(0)));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.TourDemandId).NotEqual(0)));
            RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.OnRequestId).GreaterThan(0)));
            //RuleForEach(x => x.MainDemandUpsertDto.TourDemandUpsertDtos).ChildRules(a => a.RuleForEach(b => b.OnRequests).ChildRules(c => c.RuleFor(d => d.Description).NotEmpty()));
        }
    }
}
