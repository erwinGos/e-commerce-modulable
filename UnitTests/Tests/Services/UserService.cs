using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Data.Services.Contract;
using Database.Entities;
using NUnit.Framework;
using userMicroService.Tests.Common;

namespace userMicroServices.Unit.userService
{
    [TestFixture]
    public class UserServiceUnitTest : TestBase
    {
        private IUserService _userService;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            SetUpTest();
            _mapper = _serviceProvider?.GetService<IMapper>();
            _userService = _serviceProvider?.GetService<IUserService>();
            _context.CreateUsers();
        }

        [TearDown]
        public void TearDown()
        {
            CleanTest();
        }

        [Test]
        public async Task GetAllUsers_ReturnsListOfUsers()
        {
            var users = await _userService.GetUserById(1);
            Assert.That(users, Is.Not.Null);
            Assert.That(users, Is.TypeOf<List<User>>());
        }
    }
}