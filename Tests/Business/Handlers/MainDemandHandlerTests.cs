
using Business.Handlers.Demands.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Demands.Queries.GetMainDemandQuery;
using Entities.Concrete;
using static Business.Handlers.Demands.Queries.GetMainDemandsQuery;
using static Business.Handlers.Demands.Commands.CreateMainDemandCommand;
using Business.Handlers.Demands.Commands;
using Business.Constants;
using static Business.Handlers.Demands.Commands.UpdateMainDemandCommand;
using static Business.Handlers.Demands.Commands.DeleteMainDemandCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using AutoMapper;
using Entities.Dtos;
using Business.Helpers;
using Entities.MainDemands.Dtos;
using DataAccess.Abstract.ErcanProduct;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class MainDemandHandlerTests
    {
        Mock<IMainDemandRepository> _demandRepository;
        Mock<INumberRangeRepository> _numberRangeRepository;
        Mock<IMediator> _mediator;
        IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _demandRepository = new Mock<IMainDemandRepository>();
            _numberRangeRepository = new Mock<INumberRangeRepository>();
            _mediator = new Mock<IMediator>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperHelper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        List<MainDemand> mainDemands = new List<MainDemand>
        {
            new MainDemand()
            {
                MainDemandId = 1,
                Name = "Name 1",
                Surname = "SurName 1",
                Email="email 1",
                Description="açıklama 1",
                PhoneNumber="phone 1",
                IsDeleted=false
            },
            new MainDemand()
            {
                MainDemandId = 2,
                Name = "Name 2",
                Surname = "SurName 2",
                Email="email 2",
                Description="açıklama 2",
                PhoneNumber="phone 2",
                IsDeleted=false
            },
            new MainDemand()
            {
                MainDemandId = 3,
                Name = "Name 3",
                Surname = "SurName 3",
                Email="email 3",
                Description="açıklama 3",
                PhoneNumber="phone 3",
                IsDeleted=false
            },
            new MainDemand()
            {
                MainDemandId = 4,
                Name = "Name 4",
                Surname = "SurName 4",
                Email="email 4",
                Description="açıklama 4",
                PhoneNumber="phone 4",
                IsDeleted=false
            }
        }; 

        [Test]
        public async Task Demand_GetQuery_Success()
        { 
            var query = new GetMainDemandQuery();
            query.MainDemandId = 1;

            _demandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>())).ReturnsAsync((Expression<Func<MainDemand, bool>> expression)=> {
                return mainDemands.Single(expression.Compile());
            });


            var handler = new GetMainDemandQueryHandler(_demandRepository.Object, _mapper);
             
            var x = await handler.Handle(query, new System.Threading.CancellationToken());
             
            x.Success.Should().BeTrue();
            var dto = _mapper.Map< MainDemandDto>(mainDemands.Single(a => a.MainDemandId == query.MainDemandId));
            x.Data.Should().Equals(dto);
        }

        [Test]
        public async Task Demand_GetListQuery_Success()
        { 
            var query = new GetMainDemandsQuery();

            _demandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<MainDemand, bool>>>()))
                        .ReturnsAsync(mainDemands);

            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MainDemand, MainDemandDto>());

            var handler = new GetDemandsQueryHandler(_demandRepository.Object, _mapper);
             
            var x = await handler.Handle(query, new System.Threading.CancellationToken());
             
            x.Success.Should().BeTrue();
            var dtos = mainDemands.Select(a => _mapper.Map<MainDemand, MainDemandDto>(a));
            x.Data.Should().Equals(dtos);
            x.Data.Should().HaveCount(4);

        }

        [Test]
        public async Task Demand_CreateCommand_Success()
        { 
            var command = new CreateMainDemandCommand(); 
            command.Name = "Test";
            command.Surname = "Demand";
            command.Description = "Açıklama";
            command.Email = "test@demand.com";
            command.PhoneNumber = "0900900900"; 

            var handler = new CreateDemandCommandHandler(_demandRepository.Object, _mediator.Object, _numberRangeRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _demandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Demand_UpdateCommand_Success()
        { 
            var command = new UpdateMainDemandCommand(); 
            command.MainDemandId = 1;
            command.Name = "Test Update";
            command.Surname = "Demand Update";
            command.Description = "açıklama update";
            command.Email = "testupdate@demand.com";
            command.PhoneNumber = "0900900900 update"; 

            _demandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>()))
                        .ReturnsAsync((Expression<Func<MainDemand, bool>> expression)=> {
                            return mainDemands.Single(expression.Compile());
                        }); 

            var handler = new UpdateDemandCommandHandler(_demandRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _demandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Demand_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteMainDemandCommand();
            command.MainDemandId = 1;
            _demandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>()))
                        .ReturnsAsync((Expression<Func<MainDemand, bool>> expression)=> {
                            return mainDemands.Single(expression.Compile());
                        }); 

            var handler = new DeleteDemandCommandHandler(_demandRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _demandRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

