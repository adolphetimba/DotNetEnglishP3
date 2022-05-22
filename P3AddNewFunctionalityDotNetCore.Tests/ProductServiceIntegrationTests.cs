using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceIntegrationTests : IClassFixture<InMemoryDbContextFixture>
    {
        private ProductService _productService;
        private Cart _cart;
        private ProductRepository _productRepository;
        private OrderRepository _orderRepository;
        private ILanguageService _languageService;
        private ProductController _productController;
        private InMemoryDbContextFixture _fixure;

        public ProductServiceIntegrationTests(InMemoryDbContextFixture fixure)
        {
            _fixure = fixure;
        }


        [Fact]
        public void CreateProduct()
        {
            //Arrange
            var productViewModel = new ProductViewModel
            {
                Price = "29",
                Stock = "50",
                Name = "Product 50"
            };

            _cart = new Cart();
            _productRepository = new ProductRepository(_fixure.DbContext);
            _orderRepository = new OrderRepository(_fixure.DbContext);

            _languageService = new LanguageService();


            var localizer = new Mock<IStringLocalizer<ProductService>>();

            _productService = new ProductService(_cart, _productRepository,
                _orderRepository, localizer.Object);
            _productController = new ProductController(_productService, _languageService);

            //Act
            var result = _productController.Create(productViewModel);


            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Admin", redirectToActionResult.ActionName);
        }


        [Fact]
        public void DeleteProduct()
        {
            //Arrange
            _cart = new Cart();
            _productRepository = new ProductRepository(_fixure.DbContext);
            _orderRepository = new OrderRepository(_fixure.DbContext);

            _languageService = new LanguageService();


            var localizer = new Mock<IStringLocalizer<ProductService>>();

            _productService = new ProductService(_cart, _productRepository,
                _orderRepository, localizer.Object);

            _productController = new ProductController(_productService, _languageService);

            Exception exception = Assert.Throws<InvalidOperationException>(() =>
            {
                // Act
                var result = _productController.DeleteProduct(1);

                var viewResult = result as ViewResult;

                var model = viewResult.Model as IEnumerable<ProductViewModel>;

                //Assert
                var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
                Assert.Null(redirectToActionResult.ControllerName);
                Assert.Equal("Admin", redirectToActionResult.ActionName);

            });
        }

        [Fact]
        public void GetProducts()
        {
            //Arrange
            _cart = new Cart();
            _productRepository = new ProductRepository(_fixure.DbContext);
            _orderRepository = new OrderRepository(_fixure.DbContext);

            _languageService = new LanguageService();


            var localizer = new Mock<IStringLocalizer<ProductService>>();

            _productService = new ProductService(_cart, _productRepository,
                _orderRepository, localizer.Object);
            _productController = new ProductController(_productService, _languageService);


            //Act
            var result = _productController.Index();

            var viewResult = result as ViewResult;

            var model = viewResult.Model as IEnumerable<ProductViewModel>;


            //Assert
            Assert.Single(model);
            Assert.Equal("Product 50", model.First().Name);
            Assert.Equal("29", model.First().Price);
            Assert.Equal("50", model.First().Stock);
        }
    }
}
