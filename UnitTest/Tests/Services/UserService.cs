using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Data.Services.Contract;
using Database.Entities;
using UnitTests.Scenarios;
using userMicroService.Tests.Common;
using Data.DTO.User;
using Data.Services;
using Data.Managers;

namespace UnitTest.Tests.Services
{
    [TestFixture]
    public class UserServiceUnitTest : TestBase
    {
        private IUserService _userService;

        private IAuthenticationService _authenticationService;

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            SetUpTest();
            _mapper = _serviceProvider?.GetService<IMapper>();
            _userService = _serviceProvider?.GetService<IUserService>();
            _authenticationService = _serviceProvider?.GetService<IAuthenticationService>();
            _context.CreateUser();
        }

        [TearDown]
        public void TearDown()
        {
            CleanTest();
        }

        [Test]
        public async Task GetSingleUser_ReturnUser()
        {
            var user = await _userService.GetUserById(1);
            Assert.That(user, Is.Not.Null);
            Assert.That(user, Is.TypeOf<User>());
        }

        [Test]
        public async Task GetSingleUser_ReturnError()
        {
            var user = await _userService.GetUserById(2);
            Assert.That(user, Is.Null);
        }

        [Test]
        public async Task UpdateUser_ReturnUpdatedUser()
        {
            UpdateUser updateUser = new()
            {
                Email = "newemail@gmail.com",
                Password = IdentityManager.HashPassword("newPassword"),
                LastName = "testLastName",
                Name = "TestName",
                OldPassword = "password"
            };
            User checkUser = await _userService.GetUserById(1);
            User user = await _authenticationService.SignIn(new SignInUser { Email = checkUser.Email, Password = updateUser.OldPassword });

            User updatedUser = await _userService.UpdateUser(updateUser, user);


            User testNewPassword = await _authenticationService.SignIn(new SignInUser { Email = updateUser.Email, Password = "newPassword" });
            Assert.That(testNewPassword, Is.TypeOf<User>());
            Assert.That(updatedUser.Email, Is.EqualTo(updateUser.Email));
        }
    }
}