using Database.Entities;
using Database;
using Data.Managers;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Scenarios
{
    public static class UserScenario
    {
        public static void CreateUser(this DatabaseContext databaseContext)
        {
            User user = new()
            {
                Id = 1,
                Email = "descry@gmail.com",
                Password = IdentityManager.HashPassword("password"),
                LastName = "testLastName",
                Name = "TestName"
            };
            databaseContext.User.Add(user);
            databaseContext.SaveChanges();
        }

        public static void CreateUsers(this DatabaseContext databaseContext)
        {
            User user = new()
            {
                Id = 4,
                Email = "test4@gmail.com",
                Password = IdentityManager.HashPassword("password"),
                LastName = "testLastName4",
                Name = "TestName4",
            };

            User user1 = new()
            {
                Id = 2,
                Email = "test2@gmail.com",
                Password = IdentityManager.HashPassword("password"),
                LastName = "testLastNam2",
                Name = "TestName2",
            };
            User user2 = new()
            {
                Id = 3,
                Email = "test3@gmail.com",
                Password = IdentityManager.HashPassword("password"),
                LastName = "testLastName3",
                Name = "TestName3",
            };
            databaseContext.User.AddRange(user, user1, user2);
            databaseContext.SaveChanges();

            foreach (var entity in databaseContext.ChangeTracker.Entries())
            {
                entity.State = EntityState.Detached;
            }
        }
    }
}
