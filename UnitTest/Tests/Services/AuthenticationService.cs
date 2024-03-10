using Data.DTO.User;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Scenarios;
using userMicroService.Tests.Common;

namespace UnitTest.Tests.Services
{
    public class AuthenticationServiceUnitTest : TestBase
    {
        private IAuthenticationService _authenticationService;

        [SetUp]
        public void Setup()
        {
            SetUpTest();
            _authenticationService = _serviceProvider?.GetService<IAuthenticationService>();
            _context.CreateUser();
            _context.CreateUsers();
        }

        [TearDown]
        public void TearDown()
        {
            CleanTest();
        }

        [Test]
        public async Task SignIn_ReturnUser()
        {
            User user = await _authenticationService.SignIn(new SignInUser() { Email = "descry@gmail.com" , Password = "password"});
            Assert.That(user, Is.Not.Null);
            Assert.That(user, Is.TypeOf<User>());
        }

        [Test]
        public async Task SignUp_ReturnUser()
        {
            User user = await _authenticationService.SignUp(new SignUpUser() 
            { 
                Email = "createnewuser@gmail.com", 
                Password = "password",
                Name = "John",
                LastName = "Doe"
            });
            Assert.That(user, Is.Not.Null);
            Assert.That(user, Is.TypeOf<User>());
        }
    }
}
