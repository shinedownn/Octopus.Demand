using AutoMapper;
using Business.Constants;
using Business.Handlers.Demands.Commands;
using Business.Handlers.Demands.Queries; 
using Business.Helpers;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using Entities.MainDemands.Dtos; 
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
using static Business.Handlers.Demands.Commands.CreateDemandCommand;
using static Business.Handlers.Demands.Commands.DeleteDemandCommand;
using static Business.Handlers.Demands.Commands.UpdateDemandCommand;
using static Business.Handlers.Demands.Queries.GetDemandQuery;

namespace Tests.Business.Handlers
{
    public class DemandsHandlerTest
    {
        Mock<IMainDemandRepository> _mainDemandRepository;
        Mock<IHotelDemandRepository> _hotelDemandRepository;
        Mock<ITourDemandRepository> _tourDemandRepository;
        Mock<IHotelDemandActionRepository> _hotelDemandActionRepository;
        Mock<ITourDemandActionRepository> _tourDemandActionRepository;
        Mock<IMediator> _mediator;
        Mock<IMapper> _mapper;

        [SetUp]
        public void Setup()
        {
            _mainDemandRepository = new Mock<IMainDemandRepository>();
            _hotelDemandRepository = new Mock<IHotelDemandRepository>();
            _tourDemandRepository = new Mock<ITourDemandRepository>();
            _hotelDemandActionRepository = new Mock<IHotelDemandActionRepository>();
            _tourDemandActionRepository = new Mock<ITourDemandActionRepository>();
            _mediator = new Mock<IMediator>();
            _mapper = new Mock<IMapper>();
        }

        List<MainDemand> mainDemands = new List<MainDemand>()
        {
            new MainDemand()
            {
                MainDemandId = 1,
                Name = "Test 1",
                Surname = "Demand 1",
                IsDeleted = false,
                Description = "açıklama 1",
                Email = "1@test.com",
                PhoneNumber = "0900900901"
            },
            new MainDemand()
            {
                MainDemandId = 2,
                Name = "Test 2",
                Surname = "Demand 2",
                IsDeleted = false,
                Description = "açıklama 2",
                Email = "2@test.com",
                PhoneNumber = "0900900902"
            }
        };

        List<HotelDemand> hotelDemands = new List<HotelDemand>() {
            new HotelDemand
                {
                    HotelDemandId = 1,
                    AdultCount = 3,
                    ChildCount = 2,
                    TotalCount = 5,
                    CheckIn = DateTime.Now,
                    CheckOut = DateTime.Now.AddDays(1), 
                    IsDeleted = false
                },
            new HotelDemand
                {
                    HotelDemandId = 2,
                    AdultCount = 2,
                    ChildCount = 1,
                    TotalCount = 3,
                    CheckIn = DateTime.Now.AddDays(2),
                    CheckOut = DateTime.Now.AddDays(3), 
                    IsDeleted = false
                },
            new HotelDemand
                {
                    HotelDemandId = 3,
                    AdultCount = 1,
                    ChildCount = 3,
                    TotalCount = 4,
                    CheckIn = DateTime.Now.AddDays(2),
                    CheckOut = DateTime.Now.AddDays(4), 
                    IsDeleted = false
                }
        };

        List<TourDemand> tourDemands = new List<TourDemand>
        {
            new TourDemand
            {
                TourDemandId = 1,
                MainDemandId = 1, 
                AdultCount = 2,
                ChildCount = 3,
                TotalCount = 5,
                Period="Şubat 2022",
                IsDeleted = false
            },
            new TourDemand
            {
                TourDemandId = 2,
                MainDemandId = 1, 
                AdultCount = 1,
                ChildCount = 3,
                TotalCount = 4,
                Period="Mart 2022",
                IsDeleted = false
            },
            new TourDemand
            {
                TourDemandId = 3,
                MainDemandId = 2, 
                AdultCount = 3,
                ChildCount = 2,
                TotalCount = 5,
                Period="Nisan 2022",
                IsDeleted = false
            }
        };

        List<OnRequest> onRequests = new List<OnRequest>() {
                new OnRequest(){
                 OnRequestId=1,
                 Name="açıklama 1", 
                 IsDeleted=false
                },
                new OnRequest(){
                 OnRequestId=2,
                 Name="açıklama 2", 
                 IsDeleted=false
                },
                new OnRequest(){
                 OnRequestId=2,
                 Name="açıklama 2", 
                 IsDeleted=false
                }
            };

        [Test]
        public async Task Demands_GetQuery_Success()
        {
            var query = new GetDemandQuery();
            query.MainDemandId = 1;
            _mainDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>())).ReturnsAsync((Expression<Func<MainDemand, bool>> expression) =>
            {
                return mainDemands.Single(expression.Compile());
            });

            _hotelDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>())).ReturnsAsync((Expression<Func<HotelDemand, bool>> expression) =>
            {
                return hotelDemands.Where(expression.Compile());
            });

            _tourDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync((Expression<Func<TourDemand, bool>> expression) =>
            {
                return tourDemands.Where(expression.Compile());
            });

            

            //var handler = new GetDemandQueryHandler(_mainDemandRepository.Object, _hotelDemandRepository.Object, _tourDemandRepository.Object, _mapper.Object,_mediator.Object,_hotelDemandActionRepository.Object,_tourDemandActionRepository.Object);

            //var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //x.Success.Should().BeTrue();
        }
        [Test]
        public async Task Demands_CreateCommand_Success()
        {
            var command = new CreateDemandCommand();
            command.MainDemandDto = new MainDemandInsertDto
            {
                Name = "Test",
                Surname = "Demand", 
                Description = "açıklama",
                Email = "test@test.com",
                PhoneNumber = "0900900900"
            };

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperHelper)));

            command.HotelDemandDtos = mapper.CreateMapper().Map<List<HotelDemandInsertDto>>( hotelDemands);

            command.TourDemandDtos = mapper.CreateMapper().Map<List<TourDemandInsertDto>>(tourDemands); ;
             

            //var handler = new CreateDemandCommandHandler(_mainDemandRepository.Object, _hotelDemandRepository.Object, _tourDemandRepository.Object,_hotelDemandActionRepository.Object,_tourDemandActionRepository.Object,_mapper.Object);
            //var x = await handler.Handle(command, new System.Threading.CancellationToken());
            //_mainDemandRepository.Verify(x => x.SaveChangesAsync());
            //_hotelDemandRepository.Verify(x => x.SaveChangesAsync());
            //_tourDemandRepository.Verify(x => x.SaveChangesAsync());
            

            //x.Success.Should().BeTrue();
            //x.Message.Should().Be(Messages.Added);
        }
        [Test]
        public async Task Demands_Update_Success()
        {
            var command = new UpdateDemandCommand();

            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(AutoMapperHelper)));


            command.MainDemandDto = mapper.CreateMapper().Map<MainDemandUpdateDto>(mainDemands.Single(x => x.MainDemandId == 1));

            command.HotelDemandDtos = new List<HotelDemandUpdateDto>()
            {
                new HotelDemandUpdateDto{ 
                      AdultCount=3,
                      ChildCount=2, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    },
                new HotelDemandUpdateDto{ 
                      AdultCount=2,
                      ChildCount=1, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    }
            };

            command.TourDemandDtos = new List<TourDemandUpdateDto>() {
                new TourDemandUpdateDto{
                    TourDemandId=1,
                    MainDemandId=1, 
                    AdultCount=2,
                    ChildCount=3, 
                    Period = "Şubat 2022", 
                    },
                new TourDemandUpdateDto{
                    TourDemandId=1,
                    MainDemandId=1, 
                    AdultCount=1,
                    ChildCount=2, 
                    Period = "Mart 2022", 
              }
            };

             

            _mainDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>())).ReturnsAsync((Expression<Func<MainDemand, bool>> expression) =>
            {
                return mainDemands.Single(expression.Compile());
            });

            _hotelDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<HotelDemand, bool>>>())).ReturnsAsync((Expression<Func<HotelDemand, bool>> expression) =>
            {
                return hotelDemands.Where(expression.Compile());
            });

            _tourDemandRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<TourDemand, bool>>>())).ReturnsAsync((Expression<Func<TourDemand, bool>> expression) =>
            {
                return tourDemands.Where(expression.Compile());
            });

             

            //var handler = new UpdateDemandCommandHandler(_mainDemandRepository.Object, _hotelDemandRepository.Object, _tourDemandRepository.Object, _mapper.Object);
            //var x = await handler.Handle(command, new System.Threading.CancellationToken());
            //x.Success.Should().BeTrue();
            //x.Message.Should().Be(Messages.Updated);
        }
        [Test]
        public async Task Demands_Delete_Success()
        {
            var command = new DeleteDemandCommand();
            command.MainDemandId = 1;

            _mainDemandRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<MainDemand, bool>>>())).ReturnsAsync((Expression<Func<MainDemand, bool>> expression) =>
            {
                return mainDemands.Single(expression.Compile());
            });

            //var handler = new DeleteDemandCommandHandler(_mainDemandRepository.Object, _hotelDemandRepository.Object, _tourDemandRepository.Object);
            //var x = await handler.Handle(command, new System.Threading.CancellationToken());
            //x.Success.Should().BeTrue();
            //x.Message.Should().Be(Messages.Deleted);
        }
    }
}
