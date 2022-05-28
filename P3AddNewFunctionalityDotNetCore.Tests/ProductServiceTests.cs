using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests
    {

        [Fact]
        public void CheckProductName()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
            .UseInMemoryDatabase(databaseName: "In_memory_db")
            .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);

            var productViewModel = new ProductViewModel
            {
                Price = "29",
                Stock = "50",
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "MissingName";
            string value = "Please enter a name";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository, 
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);


            //Assert
            Assert.Contains(value, modelErrors);        
        }

        [Fact]
        public void CheckPrice()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
           .UseInMemoryDatabase(databaseName: "In_memory_db")
           .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Stock = "50",
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "MissingPrice";
            string value = "Please enter a price";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);


            //Assert
            Assert.Contains(value, modelErrors);
        }

        [Fact]
        public void CheckIfPriceIsNumber()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
            .UseInMemoryDatabase(databaseName: "In_memory_db")
            .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Price = "sdsdsds",
                Stock = "50",
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "PriceNotANumber";
            string value = "The value entered for the price must be a number";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);


            //Assert
            Assert.Contains(value, modelErrors);
        }

        [Fact]
        public void CheckIfPriceIsGreaterThanZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
           .UseInMemoryDatabase(databaseName: "In_memory_db")
           .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Price = "0",
                Stock = "50",
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "PriceNotGreaterThanZero";
            string value = "The price must be greater than zero";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);


            //Assert
            Assert.Contains(value, modelErrors);
        }

        [Fact]
        public void CheckStock()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
           .UseInMemoryDatabase(databaseName: "In_memory_db")
           .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Price = "10"
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "MissingStock";
            string value = "Please enter a stock value";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);


            //Assert
            Assert.Contains(value, modelErrors);
        }

        [Fact]
        public void CheckIfStockIsInteger()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
            .UseInMemoryDatabase(databaseName: "In_memory_db")
            .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Price = "10",
                Stock = "sdsd"
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "StockNotAnInteger";
            string value = "The value entered for the stock must be a integer";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);

            //Assert
            Assert.Contains(value, modelErrors);
        }

        [Fact]
        public void CheckIfStockIsGreatherThanZero()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<P3Referential>()
           .UseInMemoryDatabase(databaseName: "In_memory_db")
           .Options;

            var dbContext = new P3Referential(options);
            var cart = new Cart();
            var productRepository = new ProductRepository(dbContext);
            var orderRepository = new OrderRepository(dbContext);


            var productViewModel = new ProductViewModel
            {
                Name = "Product 01",
                Price = "10",
                Stock = "0"
            };

            var fakeStringLocalizer = new Mock<IStringLocalizer<ProductService>>();
            string key = "StockNotGreaterThanZero";
            string value = "The stock must greater than zero";

            var localizer = new LocalizedString(key, value);
            fakeStringLocalizer.Setup(_ => _[key]).Returns(localizer);


            IProductService productService = new ProductService(cart, productRepository,
                orderRepository, fakeStringLocalizer.Object);

            //Act
            var modelErrors = productService.CheckProductModelErrors(productViewModel);

            //Assert
            Assert.Contains(value, modelErrors);
        }
    }
}