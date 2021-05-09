using Newtonsoft.Json;
using Shouldly;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi_JWT_Security.API.Tests.Fixtures;
using WebApi_JWT_Security.DTOs.User;
using Xunit;

namespace WebApi_JWT_Security.API.Tests.Controllers
{
    public class AccountControllerTests : IClassFixture<InMemoryApplicationFactory>
    {
        private readonly InMemoryApplicationFactory _factory;

        public AccountControllerTests(InMemoryApplicationFactory factory)
        {
            _factory = factory;
            SeedTestData.CreateUsersAsync(_factory).Wait();
        }

        [Theory]
        [InlineData("/api/Account/login")]
        public async Task Login_response_token(string url)
        {

            var client = _factory.CreateClient();

            var request = new LoginRequest { UserName = "Jaime", Password = "secretPassword@123" };
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            responseContent.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData("/api/Account/login")]
        public async Task Incorrect_password_response_unauthorized(string url)
        {

            var client = _factory.CreateClient();

            var request = new LoginRequest { UserName = "Jaime", Password = "badpassword" };
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
            responseContent.ShouldNotBeEmpty();
        }

        [Theory]
        [InlineData("/api/Account/login")]
        public async Task Empty_password_response_badrequest(string url)
        {

            var client = _factory.CreateClient();

            var request = new LoginRequest { UserName = "Jaime", Password = "" };
            var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, httpContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
            responseContent.ShouldNotBeEmpty();
        }

    }
}
