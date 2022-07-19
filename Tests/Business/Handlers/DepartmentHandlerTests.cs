
using Business.Handlers.Departments.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Departments.Queries.GetDepartmentQuery;
using Entities.Concrete;
using static Business.Handlers.Departments.Queries.GetDepartmentsQuery;
using static Business.Handlers.Departments.Commands.CreateDepartmentCommand;
using Business.Handlers.Departments.Commands;
using Business.Constants;
using static Business.Handlers.Departments.Commands.UpdateDepartmentCommand;
using static Business.Handlers.Departments.Commands.DeleteDepartmentCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class DepartmentHandlerTests
    {
        Mock<IDepartmentRepository> _departmentRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _departmentRepository = new Mock<IDepartmentRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Department_GetQuery_Success()
        {
            //Arrange
            var query = new GetDepartmentQuery();

            _departmentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Department, bool>>>())).ReturnsAsync(new Department()
//propertyler buraya yazılacak
//{																		
//DepartmentId = 1,
//DepartmentName = "Test"
//}
);

            var handler = new GetDepartmentQueryHandler(_departmentRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.DepartmentId.Should().Be(1);

        }

        [Test]
        public async Task Department_GetQueries_Success()
        {
            //Arrange
            var query = new GetDepartmentsQuery();

            _departmentRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                        .ReturnsAsync(new List<Department> { new Department() { /*TODO:propertyler buraya yazılacak DepartmentId = 1, DepartmentName = "test"*/ } });

            var handler = new GetDepartmentsQueryHandler(_departmentRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Department>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Department_CreateCommand_Success()
        {
            Department rt = null;
            //Arrange
            var command = new CreateDepartmentCommand();
            //propertyler buraya yazılacak
            //command.DepartmentName = "deneme";

            _departmentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                        .ReturnsAsync(rt);

            _departmentRepository.Setup(x => x.Add(It.IsAny<Department>())).Returns(new Department());

            var handler = new CreateDepartmentCommandHandler(_departmentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _departmentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Department_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateDepartmentCommand();
            //propertyler buraya yazılacak 
            //command.DepartmentName = "test";

            _departmentRepository.Setup(x => x.Query())
                                           .Returns(new List<Department> { new Department() { /*TODO:propertyler buraya yazılacak DepartmentId = 1, DepartmentName = "test"*/ } }.AsQueryable());

            _departmentRepository.Setup(x => x.Add(It.IsAny<Department>())).Returns(new Department());

            var handler = new CreateDepartmentCommandHandler(_departmentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Department_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateDepartmentCommand();
            //command.DepartmentName = "test";

            _departmentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                        .ReturnsAsync(new Department() { /*TODO:propertyler buraya yazılacak DepartmentId = 1, DepartmentName = "deneme"*/ });

            _departmentRepository.Setup(x => x.Update(It.IsAny<Department>())).Returns(new Department());

            var handler = new UpdateDepartmentCommandHandler(_departmentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _departmentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Department_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteDepartmentCommand();

            _departmentRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Department, bool>>>()))
                        .ReturnsAsync(new Department() { /*TODO:propertyler buraya yazılacak DepartmentId = 1, DepartmentName = "deneme"*/});

            _departmentRepository.Setup(x => x.Delete(It.IsAny<Department>()));

            var handler = new DeleteDepartmentCommandHandler(_departmentRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _departmentRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

