using Database.Entities;
using Database;
using Stripe;

namespace UnitTest.Scenarios
{
    public static class ProductScenario
    {
        public static void CreateProduct(this DatabaseContext databaseContext)
        {
            Database.Entities.Product product = new()
            {
                ProductName = "TestProduct",
                Ean = "39057380573",
                Price = 12,
                PriceWithoutTax = 10,
                Description = "un produit test",
                CurrentStock = 40,
                Weight = 20,
                IsDeactivated = false,
                Reduction = 20
            };
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }

        public static void CreateProducts(this DatabaseContext databaseContext)
        {
            Database.Entities.Product product2 = new()
            {
                ProductName = "TestProduct2",
                Ean = "390572",
                Price = 12,
                PriceWithoutTax = 10,
                Description = "un produit test2",
                CurrentStock = 40,
                Weight = 20,
                IsDeactivated = false,
                Reduction = 2
            };

            Database.Entities.Product product3 = new()
            {
                ProductName = "TestProduct3",
                Ean = "390573",
                Price = 12,
                PriceWithoutTax = 10,
                Description = "un produit test3",
                CurrentStock = 40,
                Weight = 20,
                IsDeactivated = false,
                Reduction = 0
            };

            Database.Entities.Product product4 = new()
            {
                ProductName = "TestProduct4",
                Ean = "390574",
                Price = 12,
                PriceWithoutTax = 10,
                Description = "un produit test4",
                CurrentStock = 40,
                Weight = 270,
                IsDeactivated = false,
                Reduction = 0
            };
            databaseContext.Products.AddRange(product2, product3, product4);
            databaseContext.SaveChanges();
        }
    }
}
