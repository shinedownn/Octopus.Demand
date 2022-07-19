
using Business.Handlers.OnRequestApprovements.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.OnRequestApprovements.Queries.GetOnRequestApprovementQuery;
using Entities.Concrete;
using static Business.Handlers.OnRequestApprovements.Queries.GetOnRequestApprovementsQuery;
using static Business.Handlers.OnRequestApprovements.Commands.CreateOnRequestApprovementCommand;
using Business.Handlers.OnRequestApprovements.Commands;
using Business.Constants;
using static Business.Handlers.OnRequestApprovements.Commands.UpdateOnRequestApprovementCommand;
using static Business.Handlers.OnRequestApprovements.Commands.DeleteOnRequestApprovementCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class OnRequestApprovementHandlerTests
    {
        Mock<IOnRequestApprovementRepository> _onRequestApprovementRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _onRequestApprovementRepository = new Mock<IOnRequestApprovementRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task OnRequestApprovement_GetQuery_Success()
        {
            //Arrange
            var query = new GetOnRequestApprovementQuery();

            _onRequestApprovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OnRequestApprovement, bool>>>())).ReturnsAsync(new OnRequestApprovement()
//propertyler buraya yazılacak
//{																		
//OnRequestApprovementId = 1,
//OnRequestApprovementName = "Test"
//}
);

            var handler = new GetOnRequestApprovementQueryHandler(_onRequestApprovementRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.OnRequestApprovementId.Should().Be(1);

        }

        [Test]
        public async Task OnRequestApprovement_GetQueries_Success()
        {
            //Arrange
            var query = new GetOnRequestApprovementsQuery();

            _onRequestApprovementRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<OnRequestApprovement, bool>>>()))
                        .ReturnsAsync(new List<OnRequestApprovement> { new OnRequestApprovement() { /*TODO:propertyler buraya yazılacak OnRequestApprovementId = 1, OnRequestApprovementName = "test"*/ } });

            var handler = new GetOnRequestApprovementsQueryHandler(_onRequestApprovementRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<OnRequestApprovement>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task OnRequestApprovement_CreateCommand_Success()
        {
            OnRequestApprovement rt = null;
            //Arrange
            var command = new CreateOnRequestApprovementCommand();
            //propertyler buraya yazılacak
            //command.OnRequestApprovementName = "deneme";

            _onRequestApprovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OnRequestApprovement, bool>>>()))
                        .ReturnsAsync(rt);

            _onRequestApprovementRepository.Setup(x => x.Add(It.IsAny<OnRequestApprovement>())).Returns(new OnRequestApprovement());

            var handler = new CreateOnRequestApprovementCommandHandler(_onRequestApprovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _onRequestApprovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task OnRequestApprovement_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateOnRequestApprovementCommand();
            //propertyler buraya yazılacak 
            //command.OnRequestApprovementName = "test";

            _onRequestApprovementRepository.Setup(x => x.Query())
                                           .Returns(new List<OnRequestApprovement> { new OnRequestApprovement() { /*TODO:propertyler buraya yazılacak OnRequestApprovementId = 1, OnRequestApprovementName = "test"*/ } }.AsQueryable());

            _onRequestApprovementRepository.Setup(x => x.Add(It.IsAny<OnRequestApprovement>())).Returns(new OnRequestApprovement());

            var handler = new CreateOnRequestApprovementCommandHandler(_onRequestApprovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task OnRequestApprovement_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateOnRequestApprovementCommand();
            //command.OnRequestApprovementName = "test";

            _onRequestApprovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OnRequestApprovement, bool>>>()))
                        .ReturnsAsync(new OnRequestApprovement() { /*TODO:propertyler buraya yazılacak OnRequestApprovementId = 1, OnRequestApprovementName = "deneme"*/ });

            _onRequestApprovementRepository.Setup(x => x.Update(It.IsAny<OnRequestApprovement>())).Returns(new OnRequestApprovement());

            var handler = new UpdateOnRequestApprovementCommandHandler(_onRequestApprovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _onRequestApprovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task OnRequestApprovement_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteOnRequestApprovementCommand();

            _onRequestApprovementRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<OnRequestApprovement, bool>>>()))
                        .ReturnsAsync(new OnRequestApprovement() { /*TODO:propertyler buraya yazılacak OnRequestApprovementId = 1, OnRequestApprovementName = "deneme"*/});

            _onRequestApprovementRepository.Setup(x => x.Delete(It.IsAny<OnRequestApprovement>()));

            var handler = new DeleteOnRequestApprovementCommandHandler(_onRequestApprovementRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _onRequestApprovementRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

