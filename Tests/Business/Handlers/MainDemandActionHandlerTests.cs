using AutoMapper;
using Business.Constants;
using Business.Handlers.MainDemandActions.Commands;
using Business.Handlers.MainDemandActions.Queries;
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
using static Business.Handlers.MainDemandActions.Commands.CreateMainDemandActionCommand;
using static Business.Handlers.MainDemandActions.Commands.DeleteMainDemandActionCommand;
using static Business.Handlers.MainDemandActions.Commands.UpdateMainDemandActionCommand;
using static Business.Handlers.MainDemandActions.Queries.GetMainDemandActionsQuery;
using static Business.Handlers.MainDemandActions.Queries.GetMainDemandActionQuery;
using Entities.MainDemandActions.Dtos;

namespace Tests.Business.Handlers
{
    [TestFixture]
    public class MainDemandActionHandlerTests
    {
        Mock<IMainDemandActionRepository> _mainDemandActionRepository;
        IMapper _mapper;
        Mock<Mediator> _mediator;

        [SetUp]
        public void SetUp()
        {
            _mainDemandActionRepository = new Mock<IMainDemandActionRepository>();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperHelper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _mediator = new Mock<Mediator>();
        }

        List<MainDemandAction> mainDemandActions = new List<MainDemandAction>() {
                 new MainDemandAction(){ MainDemandActionId=1, MainDemandId=1, ActionId=1, IsDeleted=false },
                 new MainDemandAction(){ MainDemandActionId=2, MainDemandId=1, ActionId=2, IsDeleted=false },
                 new MainDemandAction(){ MainDemandActionId=3, MainDemandId=2, ActionId=3, IsDeleted=false },
                 new MainDemandAction(){ MainDemandActionId=4, MainDemandId=3, ActionId=4, IsDeleted=false },
            };


        [Test]
        public async Task MainDemandAction_GetQuery_Success()
        {
            var query = new GetMainDemandActionQuery();
            query.MainDemandActionId = 1; 

            _mainDemandActionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemandAction, bool>>>())).ReturnsAsync((Expression<Func<MainDemandAction, bool>> expression) =>
            {
                return mainDemandActions.Single(expression.Compile());
            });


            var handler = new GetMainDemandActionQueryHandler(_mainDemandActionRepository.Object, _mapper);
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            x.Success.Should().BeTrue();
            var dto = _mapper.Map<MainDemandAction, MainDemandActionDto>(mainDemandActions.Single(a => a.MainDemandActionId == query.MainDemandActionId));
            x.Data.Should().Equals(dto);
        }
        [Test]
        public async Task MainDemandActionByMainDemandId_GetQuery_Success()
        {
            var query = new GetMainDemandActionsQuery();
            query.MainDemandId = 1; 

            _mainDemandActionRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<MainDemandAction,bool>>>())).ReturnsAsync((Expression<Func<MainDemandAction, bool>> expression) =>
            {
                return mainDemandActions.Where(expression.Compile());
            }); 

             
            var handler = new GetMainDemandActionByMainDemandIdQueryHandler(_mainDemandActionRepository.Object, _mapper);
            var x = await handler.Handle(query, new System.Threading.CancellationToken()); 

            x.Success.Should().BeTrue();
            x.Data.Should().HaveCount(2);
            var dtos = mainDemandActions.Where(x => x.MainDemandId == query.MainDemandId).Select(x => _mapper.Map<MainDemandAction, MainDemandActionDto>(x));
            x.Data.Should().Equals(dtos);
        }
        [Test]
        public async Task MainDemandAction_CreateCommand_Success()
        {
            var command = new CreateMainDemandActionCommand();
            command.MainDemandId = 1;
            command.ActionId = 1; 

            var handler = new CreateMainDemandActionCommandHandler(_mainDemandActionRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());
            _mainDemandActionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }
        [Test]
        public async Task MainDemandAction_UpdateCommand_Success()
        {
             
            var command = new UpdateMainDemandActionCommand();
            command.MainDemandActionId = 1;
            command.MainDemandId = 1;
            command.ActionId = 2; 

            _mainDemandActionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemandAction, bool>>>())).ReturnsAsync((Expression<Func<MainDemandAction, bool>> expression) => {
                return mainDemandActions.Single(expression.Compile());
            });

            var handler = new UpdateMainDemandActionCommandHandler(_mainDemandActionRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());
            _mainDemandActionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }
        [Test]
        public async Task MainDemandAction_DeleteCommand_Success()
        {
             
            var command = new DeleteMainDemandActionCommand();
            command.ActionId = 1;

            _mainDemandActionRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemandAction, bool>>>())).ReturnsAsync((Expression<Func<MainDemandAction, bool>> expression) => {
                return mainDemandActions.Where(expression.Compile()).Single();
            });

            var handler = new DeleteMainDemandActionCommandHandler(_mainDemandActionRepository.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());
            _mainDemandActionRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        } 
    }
}
