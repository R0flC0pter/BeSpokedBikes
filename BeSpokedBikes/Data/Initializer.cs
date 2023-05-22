using BeSpokedBikes.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedBikes.Data
{
    public static class Initializer
    {
        public static void Initialize(SalesContext context)
        {
            if (context.Sellers.Any())
            {
                return;
            }
            var salespeople = new Sellers[]
            {
                new Sellers{SellerID=1 ,FirstName = "Joe",LastName = "Thorton", Address = "1234 Main Street", Phone = 1234567894 , StartDate = DateTime.Parse("2019-09-01"), EndDate = DateTime.Parse("2022-12-01"),Manager= "Michelle" },
                new Sellers{SellerID=2 ,FirstName = "Bob",LastName = "Snow", Address = "1234 Main Street", Phone = 1234567894, StartDate =DateTime.Parse("2017-09-01"), EndDate = null,Manager= "Michelle"},
                new Sellers{SellerID=3 ,FirstName = "Pedro",LastName = "Ramos", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2018-09-01"), EndDate = null,Manager= "Michelle"},
                new Sellers{SellerID=4 ,FirstName = "Amanda",LastName = "Lincoln", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2017-09-01"), EndDate = null,Manager= "Andi"},
                new Sellers{SellerID=5 ,FirstName = "Timothy",LastName = "Johnson", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2017-09-01"), EndDate = null,Manager= "Andi"},
                new Sellers{SellerID=6 ,FirstName = "Timothy",LastName = "Johnson", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2016-09-01"), EndDate = DateTime.Parse("2019-09-01"),Manager= "Andi"},
                new Sellers{SellerID=7 ,FirstName = "John",LastName = "Gordon", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2018-09-01"), EndDate = null,Manager= "Andi"},
                new Sellers{SellerID=8 ,FirstName = "Al",LastName = "Sharpe", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2019-09-01"), EndDate = null,Manager= "Andi"},
                new Sellers{SellerID=9 ,FirstName = "Michelle",LastName = "Fishe", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2010-09-01"), EndDate = null,Manager= "Michelle"},
                new Sellers{SellerID=10 ,FirstName = "Andi",LastName = "Twente", Address = "1234 Main Street", Phone = 1234567894, StartDate = DateTime.Parse("2009-09-01"), EndDate = null,Manager= "Andi"}
            };
            context.Sellers.AddRange(salespeople);
            var products = new Products[] {
            new Products{ProductID = 1 ,Name = "Commuter Max", Manufacturer = "Shwyin",   Style = "Cruiser", SalePrice = 250.00M,QuantityInStock = 20,CommissionPercentage = 5},
            new Products{ProductID = 2, Name = "Comfort Plus", Manufacturer = "Shwyin", Style = "Cruiser",  SalePrice = 595.00M, QuantityInStock = 18, CommissionPercentage = 4},
            new Products{ProductID = 3, Name = "Cruiser", Manufacturer = "Shwyin", Style = "Cruiser", SalePrice =250.50M , QuantityInStock = 5, CommissionPercentage = 9},
            new Products{ProductID = 4 ,Name = "Lowride", Manufacturer = "Street Demons", Style = "Street", SalePrice =999.99M,QuantityInStock = 30,CommissionPercentage = 19},
            new Products{ProductID = 5 ,Name = "Sweeper", Manufacturer = "Street Demons", Style = "Street", SalePrice = 1500.00M,QuantityInStock = 50,CommissionPercentage = 20},
            new Products{ProductID = 6 ,Name = "Drag 5000", Manufacturer = "Street Demons", Style = "Multi", SalePrice =1400.50M ,QuantityInStock = 10,CommissionPercentage = 4},
            new Products{ProductID = 7 ,Name = "230i", Manufacturer = "eBikers", Style = "Electric", SalePrice =2600,QuantityInStock = 1,CommissionPercentage = 3},
            new Products{ProductID = 8 ,Name = "5 Series", Manufacturer = "eBikers", Style = "Electric", SalePrice = 3650.65M,QuantityInStock = 0,CommissionPercentage = 5 },
            new Products{ProductID = 9 ,Name = "Rugged Climber Max", Manufacturer = "Tough Mudder", Style = "Trail" , SalePrice =99.99M,QuantityInStock = 30,CommissionPercentage = 30},
            new Products{ProductID = 10,Name = "Rugged Climber", Manufacturer = "Tough Mudder", Style = "Trail", SalePrice = 35,QuantityInStock = 15,CommissionPercentage = 10},
            new Products{ProductID = 11,Name = "Trail Carver", Manufacturer = "Rough Riders", Style = "Trail", SalePrice = 20,QuantityInStock = 32,CommissionPercentage = 20}
            };
            context.Products.AddRange(products);
            var customers = new Customers[] { 
            new Customers{CustomerID = 1,FirstName = "Colin",LastName= "Frankley",Address = "500 Peachtree Road",Phone = 7079844551,StartDate = DateTime.Parse("2023-01-01")},
            new Customers{CustomerID = 2,FirstName = "Frank",LastName= "Collins",Address = "500 Peachtree Road",Phone = 7079844551,StartDate = DateTime.Parse("2022-11-29")},
            new Customers{CustomerID = 3,FirstName = "Charley",LastName= "McArthur",Address = "500 Peachtree Road",Phone = 7079844551,StartDate = DateTime.Parse("2018-05-05")},
            new Customers{CustomerID = 4,FirstName = "Kirk",LastName= "Montgomery",Address = "500 Peachtree Road",Phone = 7079844551,StartDate = DateTime.Parse("2019-07-04")},
            new Customers{CustomerID = 5,FirstName = "Joseph",LastName= "Patton",Address = "500 Peachtree Road",Phone = 7079844551,StartDate = DateTime.Parse("2020-06-23")}
            };
            context.Customers.AddRange(customers);
            var sales = new Sales[] { 
            new Sales{SalesID = 1,ProductID = 3,SellerID = 5,CustomerID = 2,SalesDate = DateTime.Parse("2023-01-01")},
            new Sales{SalesID = 2,ProductID = 4,SellerID = 2,CustomerID = 3,SalesDate = DateTime.Parse("2019-02-14")},
            new Sales{SalesID = 3,ProductID = 5,SellerID = 1,CustomerID = 4,SalesDate = DateTime.Parse("2022-03-03")},
            new Sales{SalesID = 4,ProductID = 7,SellerID = 4,CustomerID = 2,SalesDate = DateTime.Parse("2022-12-19")}
            };
            context.Sales.AddRange(sales);
            var discount = new Discounts[] {
            new Discounts{ProductID = 3,BeginDate = DateTime.Parse("2020-01-01"),EndDate = DateTime.Parse("2020-06-01"),DiscountPercentage = 2.9M} ,
            new Discounts{ProductID = 4,BeginDate = DateTime.Parse("2019-01-01"),EndDate = DateTime.Parse("2019-06-01"),DiscountPercentage = 50M} ,
            new Discounts{ProductID = 5,BeginDate = DateTime.Parse("2023-01-01"),EndDate = DateTime.Parse("2023-06-01"),DiscountPercentage = 2M} ,
            new Discounts{ProductID = 7,BeginDate = DateTime.Parse("2022-07-01"),EndDate = DateTime.Parse("2022-12-31"),DiscountPercentage = 0.5M}
            };
            context.Discounts.AddRange(discount);
            context.SaveChanges();
            foreach (var product in context.Products)
            {
                var discountsForProduct = context.Discounts.Where(d => d.ProductID == product.ProductID && d.BeginDate <= DateTime.Now && d.EndDate >= DateTime.Now);
                var applicableDiscount = discountsForProduct.OrderByDescending(d => d.DiscountPercentage).FirstOrDefault();

                if (applicableDiscount != null)
                {
                    product.PurchasePrice = product.SalePrice - (product.SalePrice * (applicableDiscount.DiscountPercentage / 100));
                }
                else
                {
                    product.PurchasePrice = product.SalePrice;
                }
            }

            context.SaveChanges();
        }
    }
}
