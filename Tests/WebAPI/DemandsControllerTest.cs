using Business.Handlers.Demands.Commands;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Entities.Concrete;
using Entities.Demands.Dtos;
using Entities.Dtos;
using Entities.HotelDemands.Dtos;
using Entities.MainDemands.Dtos;
using Entities.TourDemands.Dtos;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Tests.Helpers;
using Tests.Helpers.Token;

namespace Tests.WebAPI
{
    [TestFixture]
    public class DemandsControllerTest : BaseIntegrationTest
    {
        [Test]
        public async Task GetById()
        {
            const string authenticationScheme = "Bearer";
            const string requestUri = "api/demands/getbyid/1";

            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationScheme, token);
            var cache = new MemoryCacheManager();

            cache.Add($"{CacheKeys.MainDemandId}=1", new List<string>() { "GetDemandQuery" });

            var response = await HttpClient.GetAsync(requestUri);
            response.StatusCode.Should()?.Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task Add()
        {
            const string authenticationScheme = "Bearer";
            const string requestUri = "api/demands/add";

            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationScheme, token);

            var demand = new CreateDemandCommand();
            demand.MainDemandDto= new MainDemandInsertDto
            {
                Name = "Test",
                Surname = "Demand", 
                Description = "açıklama",
                Email = "test@test.com",
                PhoneNumber = "0900900900"
            };

            demand.HotelDemandDtos= new List<HotelDemandInsertDto>()
            {
                new HotelDemandInsertDto{ 
                      AdultCount=3,
                      ChildCount=2, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    },
                new HotelDemandInsertDto{ 
                      AdultCount=2,
                      ChildCount=1, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    }
            };

            demand.TourDemandDtos= new List<TourDemandInsertDto>() {
                new TourDemandInsertDto{ 
                    AdultCount=2,
                    ChildCount=3,
                    Period = "Şubat 2022",
                    },
                new TourDemandInsertDto{ 
                    AdultCount=1,
                    ChildCount=2,
                    Period = "Mart 2022",
              }
            }; 

            var cache = new MemoryCacheManager();
            cache.Add($"{CacheKeys.AddDemand}=${demand}", new List<string>() { "AddDemand" });

            var response = await HttpClient.PostAsJsonAsync(requestUri, demand);
            response.StatusCode.Should()?.Be(HttpStatusCode.OK);

        }
        [Test]
        public async Task Update()
        {
            const string authenticationScheme = "Bearer";
            const string requestUri = "api/demands/update";

            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationScheme, token);

            var demand = new UpdateDemandCommand();
            demand.MainDemandDto = new MainDemandUpdateDto
            {
                Name = "Test",
                Surname = "Demand", 
                Description = "açıklama",
                Email = "test@test.com",
                PhoneNumber = "0900900900"
            };

            demand.HotelDemandDtos = new List<HotelDemandUpdateDto>()
            {
                new HotelDemandUpdateDto{
                      HotelDemandId=1,
                      AdultCount=3,
                      ChildCount=2, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    },
                new HotelDemandUpdateDto{
                      HotelDemandId=1,
                      AdultCount=2,
                      ChildCount=1, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now, 
                    }
            };

            demand.TourDemandDtos = new List<TourDemandUpdateDto>() {
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

            var cache = new MemoryCacheManager();
            cache.Add($"{CacheKeys.AddDemand}=${demand}", new List<string>() { "UpdateDemand" });

            var response = await HttpClient.PostAsJsonAsync(new Uri(requestUri), demand);
            response.StatusCode.Should()?.Be(HttpStatusCode.OK);
        }
        [Test]
        public async Task Delete()
        {
            const string authenticationScheme = "Bearer";
            const string requestUri = "api/demands/delete";

            var token = MockJwtTokens.GenerateJwtToken(ClaimsData.GetClaims());
            HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(authenticationScheme, token);

            var demand = new DemandsDto();
            demand.MainDemandDto = new MainDemandDto
            {
                Name = "Test",
                Surname = "Demand", 
                Description = "açıklama",
                Email = "test@test.com",
                PhoneNumber = "0900900900"
            };

            demand.HotelDemandDtos = new List<HotelDemandDto>()
            {
                new HotelDemandDto{
                      HotelDemandId=1,
                      AdultCount=3,
                      ChildCount=2, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    },
                new HotelDemandDto{
                      HotelDemandId=1,
                      AdultCount=2,
                      ChildCount=1, 
                      CheckIn=DateTime.Now,
                      CheckOut=DateTime.Now,  
                    }
            };

            demand.TourDemandDtos = new List<TourDemandDto>() {
                new TourDemandDto{
                    TourDemandId=1, 
                    AdultCount=2,
                    ChildCount=3, 
                    Period="Şubat 2022", 
                    },
                new TourDemandDto{
                    TourDemandId=1, 
                    AdultCount=1,
                    ChildCount=2, 
                    Period="Mart 2022", 
              }
            };

            var cache = new MemoryCacheManager();
            cache.Add($"{CacheKeys.DeleteDemand}=${demand}", new List<string>() { "DeleteDemand" });

            var response = await HttpClient.PostAsJsonAsync(new Uri(requestUri), demand);
            response.StatusCode.Should()?.Be(HttpStatusCode.OK);
        }
    }
}
