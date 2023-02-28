using Xunit;
using static System.Net.WebRequestMethods;

namespace TestProject1
{
    public class ApiCustomerTest
    {
        /// <summary>
        /// Method to test success response
        /// </summary>
        [Fact]
        public async void Test_200_Response()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", "this-is-the-test-apikey");

            string getCustomer = "https://localhost:44311/api/Customer";

            var jsonList = await client.GetAsync(getCustomer);

            Assert.True((jsonList != null && jsonList.IsSuccessStatusCode));
            var response = jsonList.Content.ReadAsStringAsync();

        }

        /// <summary>
        /// Method to test 404 response
        /// </summary>
        [Fact]
        public async void Test_404_Response()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("ApiKey", "this-is-the-test-apikey");

            string getCustomer = "https://localhost:44311/api/CustomerAAAAA";

            var jsonList = await client.GetAsync(getCustomer);

            Assert.True((jsonList != null && jsonList.StatusCode== System.Net.HttpStatusCode.NotFound));

        }

    }
}