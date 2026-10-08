using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiniSupermarket.WinForms
{
    public static class ApiClientService
    {
        public static HttpClient Client { get; private set; } = CreateClient("http://localhost:5186/api/");

        private static HttpClient CreateClient(string baseUrl)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            return new HttpClient(handler)
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(8)
            };
        }

        public static void SetBaseUrl(string baseUrl)
        {
            Client = CreateClient(baseUrl);
        }

        /// <summary>
        /// Tự động dò tìm cổng API đang chạy (5186 hoặc 7118)
        /// </summary>
        public static async Task<bool> EnsureConnectionAsync()
        {
            string[] testUrls = { "http://localhost:5186/api/", "https://localhost:7118/api/" };
            foreach (var url in testUrls)
            {
                try
                {
                    var testClient = CreateClient(url);
                    var res = await testClient.GetAsync("categories");
                    if (res.IsSuccessStatusCode)
                    {
                        Client = testClient;
                        return true;
                    }
                }
                catch
                {
                    // Tiếp tục thử URL tiếp theo
                }
            }
            return false;
        }
    }
}
