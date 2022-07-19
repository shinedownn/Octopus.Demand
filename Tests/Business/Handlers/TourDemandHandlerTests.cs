using AutoMapper;
using Business.Constants;
using Business.Handlers.TourDemands.Commands;
using Business.Handlers.TourDemands.Queries;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.TourDemands.Dtos;
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
using static Business.Handlers.TourDemands.Commands.CreateTourDemandCommand;
using static Business.Handlers.TourDemands.Commands.DeleteTourDemandCommand;
using static Business.Handlers.TourDemands.Commands.UpdateTourDemandCommand;
using static Business.Handlers.TourDemands.Queries.GetTourDemandQuery;
using static Business.Handlers.TourDemands.Queries.GetTourDemandsQuery;

namespace Tests.Business.Handlers
{
    public class TourDemandHandlerTests
    {
        Mock<ITourDemandRepository> _tourDemandRepository;
        Mock<IMediator> _mediator;
        Mock<IMapper> _mapper;
        [SetUp]
        public void Setup()
        {
            _tourDemandRepository = new Mock<ITourDemandRepository>();
            _mediator = new Mock<IMediator>();
            _mapper = new Mock<IMapper>();
        }

        List<TourDemand> tours = new List<TourDemand> {
              new TourDemand{
                TourDemandId=1,
                MainDemandId=1, 
                AdultCount=3,
                ChildCount=2,
                TotalCount=5,
               Period="Şubat 2022",
                IsDeleted=false
              },
              new TourDemand{
                TourDemandId=2,
                MainDemandId=1,
                AdultCount=3,
                ChildCount=1,
                TotalCount=4,
               Period="Mart 2022",
                IsDeleted=false
              },
              new TourDemand{
                TourDemandId=3,
                MainDemandId=2,
                AdultCount=2,
                ChildCount=1,
                TotalCount=3,
                Period="Nisan 2022",
                IsDeleted=false
              },
              new TourDemand{
                TourDemandId=4,
                MainDemandId=3,
                AdultCount=1,
                ChildCount=2,
                TotalCount=3,
                Period="Mayıs 2022",
                IsDeleted=false
              }
            };

        [Test]
        public async Task TourDemand_GetQuery_Success()
        {
            var query = new GetTourDemandQuery();
            query.TourDemandId = 1;

            _tourDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync((Expression<Func<TourDemand, bool>> expression)=> {
                return tours.Single(expression.Compile());
            });

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TourDemand, TourDemandDto>()); 

            //var handler = new GetTourDemandQueryHandler(_tourDemandRepository.Object, config.CreateMapper());
            //var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //x.Success.Should().BeTrue();
            //x.Data.TourDemandId.Should().Be(1);
        }
        [Test]
        public async Task TourDemand_GetQueryList_Success()
        {
            var query = new GetTourDemandsQuery();

            _tourDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync(tours);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TourDemand, TourDemandDto>()); 

            var handler = new GetTourDemandsQueryHandler(_tourDemandRepository.Object, config.CreateMapper());
            var x = await handler.Handle(query, new System.Threading.CancellationToken());
            x.Success.Should().BeTrue();
            x.Data.Should().HaveCount(4);
        }
        [Test]
        public async Task TourDemand_CreateCommand_Success()
        { 
            var command = new CreateTourDemandCommand(); 
            command.MainDemandId = 1;
            command.AdultCount = 2;
            command.ChildCount = 3;
            command.Period = "Şubat 2022";  

            //_tourDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync(new TourDemand());
            var handler = new CreateTourDemandCommandHandler(_tourDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _tourDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }
        [Test]
        public async Task TourDemand_UpdateCommand_Success()
        {
            var command = new UpdateTourDemandCommand();
            command.TourDemandId = 1; 
            command.AdultCount = 3;
            command.ChildCount = 2;
            command.Period = "Şubat 2022";
            command.MainDemandId = 1; 

            _tourDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync((Expression<Func<TourDemand, bool>> expression)=> {
                return tours.Single(expression.Compile());
            }); 

            var handler = new UpdateTourDemandCommandHandler(_tourDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _tourDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Message.Should().Be(Messages.Updated);
        }
        [Test]
        public async Task TourDemand_DeleteCommand_Success()
        {
            var command = new DeleteTourDemandCommand();
            command.TourDemandId = 1;

            _tourDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync((Expression<Func<TourDemand, bool>> expression)=> {
                return tours.Single(expression.Compile());
            });

            _tourDemandRepository.Setup(x => x.Delete(It.IsAny<TourDemand>()));

            var handler = new DeleteTourDemandCommandHandler(_tourDemandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());
            _tourDemandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}
