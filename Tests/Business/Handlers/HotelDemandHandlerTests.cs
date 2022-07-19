using AutoMapper;
using Business.Constants;
using Business.Handlers.HotelDemands.Commands;
using Business.Handlers.HotelDemands.Queries;
using Business.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Business.Handlers.HotelDemands.Commands.CreateHotelDemandCommand;
using static Business.Handlers.HotelDemands.Commands.DeleteHotelDemandCommand;
using static Business.Handlers.HotelDemands.Commands.UpdateHotelDemandCommand;
using static Business.Handlers.HotelDemands.Queries.GetHotelDemandQuery;
using static Business.Handlers.HotelDemands.Queries.GetHotelDemandsQuery;

namespace Tests.Business.Handlers
{
    public class HotelDemandHandlerTests
    {
        Mock<IHotelDemandRepository> _hotelDemandRepository;
        Mock<IMediator> _mediator;
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _hotelDemandRepository = new Mock<IHotelDemandRepository>();
            _mediator = new Mock<IMediator>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperHelper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        List<HotelDemand> hotels = new List<HotelDemand> {
                    new HotelDemand{
                      HotelDemandId=1,
                      AdultCount=3,
                      ChildCount=2,
                      TotalCount=5,
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now.AddDays(1), 
                      IsDeleted=false
                    },
                    new HotelDemand{
                      HotelDemandId=2,
                      AdultCount=3,
                      ChildCount=1,
                      TotalCount=4,
                      CheckIn=DateTime.Now.AddDays(1),
                      CheckOut=DateTime.Now.AddDays(2), 
                      IsDeleted=false
                    },
                    new HotelDemand{
                      HotelDemandId=3,
                      AdultCount=2,
                      ChildCount=1,
                      TotalCount=3,
                      CheckIn=DateTime.Now.AddDays(2),
                      CheckOut=DateTime.Now.AddDays(4), 
                      IsDeleted=false
                    },
                    new HotelDemand{
                      HotelDemandId=4,
                      AdultCount=1,
                      ChildCount=2,
                      TotalCount=3,
                      CheckIn=DateTime.Now.AddDays(3),
                      CheckOut=DateTime.Now.AddDays(6), 
                      IsDeleted=false
                    }
                };

        [Test]
        public async Task HotelDemand_GetQuery_Success()
        {

            var query = new GetHotelDemandQuery();
            query.HotelDemandId = 1;
            _hotelDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>())).ReturnsAsync((Expression<Func<HotelDemand, bool>> expression) =>
            {
                return hotels.Single(expression.Compile());
            }); 

            //var handler = new GetHotelDemandQueryHandler(_hotelDemandRepository.Object, _mapper);
            //var x = await handler.Handle(query, new System.Threading.CancellationToken());
            //x.Success.Should().BeTrue();
            //x.Data.HotelDemandId.Should().Be(1);
        }

        [Test]
        public async Task HotelDemands_GetListQuery_Success()
        {
            var query = new GetHotelDemandsQuery();
            _hotelDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>()))
                .ReturnsAsync(hotels);


            var handler = new GetHotelDemandsQueryHandler(_hotelDemandRepository.Object, _mapper);
            var x = await handler.Handle(query, new System.Threading.CancellationToken());
            x.Success.Should().BeTrue();
            x.Data.Should().HaveCount(4); 
        }

        [Test]
        public async Task HotelDemand_CreateCommand_Success()
        {
             
            var command = new CreateHotelDemandCommand();
             
            command.Name = "Test Hotel";
            command.AdultCount = 3;
            command.ChildCount = 2;
            command.CheckIn = DateTime.Now;
            command.CheckOut = DateTime.Now; 

            var handler = new CreateHotelDemandCommandHandler(_hotelDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hotelDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task HotelDemand_UpdateCommand_Success()
        {
            var command = new UpdateHotelDemandCommand();
            command.HotelDemandId = 1; 
            command.AdultCount = 3;
            command.ChildCount = 2;
            command.CheckIn = DateTime.Now;
            command.CheckOut = DateTime.Now; 

            _hotelDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>())).ReturnsAsync((Expression<Func<HotelDemand, bool>> expression) => {
                return hotels.Single(expression.Compile());
            });
             
            var handler = new UpdateHotelDemandCommandHandler(_hotelDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hotelDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task HotelDemand_DeleteCommand_Success()
        {
            var command = new DeleteHotelDemandCommand();
            command.HotelDemandId = 1;

            var updateTo = new HotelDemand
            {
                HotelDemandId = 1,
                Name = "Test Hotel",
                AdultCount = 3,
                ChildCount = 2,
                TotalCount = 5,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now, 
                IsDeleted = false
            };

            _hotelDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>())).ReturnsAsync((Expression<Func<HotelDemand, bool>> expression)=> {
                return hotels.Single(expression.Compile());
            });

            _hotelDemandRepository.Setup(x => x.Delete(It.IsAny<HotelDemand>()));

            var handler = new DeleteHotelDemandCommandHandler(_hotelDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _hotelDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}
