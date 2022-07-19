
using Business.Handlers.Reminders.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Reminders.Queries.GetReminderQuery;
using Entities.Concrete;
using static Business.Handlers.Reminders.Queries.GetRemindersQuery;
using static Business.Handlers.Reminders.Commands.CreateReminderCommand;
using Business.Handlers.Reminders.Commands;
using Business.Constants;
using static Business.Handlers.Reminders.Commands.UpdateReminderCommand;
using static Business.Handlers.Reminders.Commands.DeleteReminderCommand;
using MediatR;
using System.Linq;
using FluentAssertions;
using AutoMapper;
using Business.Helpers;
using Entities.Dtos.Reminder;

namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class ReminderHandlerTests
    {
        Mock<IReminderRepository> _reminderRepository;
        Mock<IMediator> _mediator;
        Mock<IMapper> _mapper;
        [SetUp]
        public void Setup()
        {
            _reminderRepository = new Mock<IReminderRepository>();
            _mediator = new Mock<IMediator>();
             
            _mapper = new Mock<IMapper>(); 
        }

        [Test]
        public async Task Reminder_GetQuery_Success()
        {
            //Arrange
            var query = new GetReminderQuery();

            _reminderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Reminder, bool>>>())).ReturnsAsync(new Reminder()
            //propertyler buraya yazılacak
            {
                ReminderId = 1,
            }
            );
            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperHelper)));

            var handler = new GetReminderQueryHandler(_reminderRepository.Object, _mediator.Object, config.CreateMapper());

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.ReminderId.Should().Be(1);

        }

        [Test]
        public async Task Reminder_GetQueries_Success()
        {
            //Arrange
            var query = new GetRemindersQuery();

            _reminderRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Reminder, bool>>>()))
                        .ReturnsAsync(new List<Reminder> { new Reminder() { ReminderId = 1, CreateDate = DateTime.Now, CreatedUserName = "e.gazel@gezinomi.com", IsActive = true, HotelDemandActionId = 1 } });

            var config = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperHelper)));

            var handler = new GetRemindersQueryHandler(_reminderRepository.Object, _mediator.Object, config.CreateMapper());

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<ReminderDto>)x.Data).Count.Should().BeGreaterOrEqualTo(1);

        }

        [Test]
        public async Task Reminder_CreateCommand_Success()
        {
            Reminder rt = null;
            //Arrange
            var command = new CreateReminderCommand();
            //propertyler buraya yazılacak
            command.HotelDemandActionId = 1; 

            _reminderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Reminder, bool>>>()))
                        .ReturnsAsync(rt);

            _reminderRepository.Setup(x => x.Add(It.IsAny<Reminder>())).Returns(new Reminder());



            var handler = new CreateReminderCommandHandler(_reminderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _reminderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Reminder_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateReminderCommand();
            command.ReminderId = 1;
            command.HotelDemandActionId = 2; 

            _reminderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Reminder, bool>>>()))
                        .ReturnsAsync(new Reminder() { ReminderId = 1, CreateDate = DateTime.Now, CreatedUserName = "e.gazel@gezinomi.com", IsActive = true, HotelDemandActionId = 1 });

            _reminderRepository.Setup(x => x.Update(It.IsAny<Reminder>())).Returns(new Reminder());

            var handler = new UpdateReminderCommandHandler(_reminderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _reminderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Reminder_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteReminderCommand();
            command.ReminderId = 1;
            _reminderRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Reminder, bool>>>()))
                        .ReturnsAsync(new Reminder() { ReminderId = 1, CreateDate = DateTime.Now, CreatedUserName = "e.gazel@gezinomi.com", IsActive = true, HotelDemandActionId = 1 });

            _reminderRepository.Setup(x => x.Delete(It.IsAny<Reminder>()));

            var handler = new DeleteReminderCommandHandler(_reminderRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _reminderRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

