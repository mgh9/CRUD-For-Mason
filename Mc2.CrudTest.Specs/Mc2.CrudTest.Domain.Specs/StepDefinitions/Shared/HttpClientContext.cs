namespace Mc2.CrudTest.Specs.StepDefinitions.Shared
{
    public class HttpClientContext
    {
        private const string BASE_URL = "https://localhost:7114/";
        public HttpClient HttpClient { get; private set; }

        public HttpClientContext()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL)
            };
        }
    }

}
