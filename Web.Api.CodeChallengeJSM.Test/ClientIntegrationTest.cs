using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Web.Api.CodeChallengeJSM.Test
{
    public class ClientIntegrationTest : IClassFixture<TestClient>
    {
        private readonly HttpClient Client;
        public ClientIntegrationTest(TestClient client)
        {
            Client = client.CreateClient();
        }

        [Fact]
        public async Task Test_Get_ReturnClientsWithoutParameters()
        {
            using (Client)
            {
                var response = await Client.GetAsync("/CodeChallenge/v1/ReturnClients");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Get_WithFilter()
        {

                var response = await Client.GetAsync("/CodeChallenge/v1/ReturnClients?RegionClient=northeast&TypeClient=special");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task Test_Get_WithPageSize()
        {

                var response = await Client.GetAsync("/CodeChallenge/v1/ReturnClients?PageSize=20&PageNumber=2");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }

        [Fact]
        public async Task Test_Get_WithAllParameters()
        {

                var response = await Client.GetAsync("/CodeChallenge/v1/ReturnClients?RegionClient=northeast&TypeClient=laborious&PageSize=20&PageNumber=2");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
        }

        [Fact]
        public async Task Test_Get_WithAllParametersWrong()
        {
            using (var client = Client)
            {
                var response = await client.GetAsync("/CodeChallenge/v1/ReturnClients?RegionClient=a&TypeClient=a&PageSize=-1&PageNumber=7000");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

    }
}
