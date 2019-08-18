using Kiper.Condominio.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kiper.Condominio.Tests.IntegrationTests
{
    public class SecurityControllerIntegrationTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public SecurityControllerIntegrationTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("condominio@kiper.com.br", "Admin@2019")]
        public async Task ReturnLoginOk(string email, string password)
        {
            var login = new LoginViewModel()
            {
                Email = email,
                Password = password
            };

            var client = _factory.CreateClient();
            var content = JsonConvert.SerializeObject(login);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync($"/kipercondominio/api/v1/login", byteContent);

            result.EnsureSuccessStatusCode();
            
            Assert.True(result.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("condominio@kiper.com.br", "Admin@201")]
        public async Task ReturnUnauthorized(string email, string password)
        {
            var login = new LoginViewModel()
            {
                Email = email,
                Password = password
            };

            var client = _factory.CreateClient();
            var content = JsonConvert.SerializeObject(login);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync($"/kipercondominio/api/v1/login", byteContent);
            
            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        private class LoginViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
