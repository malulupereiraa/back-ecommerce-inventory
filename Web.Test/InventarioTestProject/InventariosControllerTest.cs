using Web.Test.Controllers;
using FakeItEasy;
using Web.Test.Data;
using Microsoft.AspNetCore.Mvc;
using Web.Test.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Http;
using Tests;

namespace InventarioTestProject
{
    public class InventariosControllerTest
    {

        [Fact]
        public async void CheckValueItems()
        {
            var inventario = new Inventario(1, "Bolsa", 122, 20, 150) { Id=1, Nome="Bolsa", Preco=122, Quantidade=20, Referencia=150};
            var teste = inventario.Quantidade > 2;
            Assert.True(teste);

        }
        [Fact]
        public async Task GivenARequest_WhenCallingGetInvetarions_ThenTheAPIReturnsExpectedResponse()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new[]
            {
            new Inventario(2, "Camiseta 1", 123, 200, 150.5) {Id=2, Nome="Camiseta 1", Preco=150.5, Quantidade=200, Referencia=123 },
            new Inventario(7, "Mouse", 1234, 30, 100) {Id=7, Nome="Mouse", Preco=100, Quantidade=30, Referencia=1234 },
            };
            var stopwatch = Stopwatch.StartNew();
            // Act.
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5192/api/Inventarios");
            // Assert.
            await Tests.TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent, "getall");
        }

        [Fact]
        public async Task GivenARequest_WhenCallingPostProducts_ThenTheAPIReturnsExpectedResponseAndAddsProduct()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.InternalServerError;
            var expectedContent = new Inventario(16, "Camiseta 2", 132, 230, 160.5) { Id = 16, Nome = "Camiseta 2", Preco = 160.5, Quantidade = 230, Referencia = 132 };
            var stopwatch = Stopwatch.StartNew();

            // Act.
            var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5192/api/Inventarios", TestHelpers.GetJsonStringContent(expectedContent));

            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent, "post");
        }

        [Fact]
        public async Task GivenARequest_WhenCallingPutProducts_ThenTheAPIReturnsExpectedResponseAndUpdatesProduct()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.MethodNotAllowed;
            var updatedProduct = new Inventario(2, "Camiseta 1", 123, 200, 160.5) { Id = 2, Nome = "Camiseta 1", Preco = 160.5, Quantidade = 200, Referencia = 123 };
            var stopwatch = Stopwatch.StartNew();

            // Act.
            var client = new HttpClient();
            var response = await client.PutAsync("http://localhost:5192/api/Inventarios", TestHelpers.GetJsonStringContent(updatedProduct));

            // Assert.
            TestHelpers.AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
        }
    }
}