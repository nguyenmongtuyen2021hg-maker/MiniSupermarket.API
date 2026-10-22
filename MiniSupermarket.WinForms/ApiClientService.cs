using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiniSupermarket.WinForms
{
    public static class ApiClientService
    {
        private static string _bearerToken = string.Empty;
        // Ưu tiên HTTPS: API chuyển hướng HTTP 5186 sang HTTPS 7118. Nếu gọi HTTP,
        // HttpClient có thể loại bỏ Authorization khi đi theo redirect khác cổng.
        public static HttpClient Client { get; private set; } = CreateClient("https://localhost:7118/api/");

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
            ApplyAuthorization(Client);
        }

        public static void SetAuthorizationToken(string token)
        {
            _bearerToken = token ?? string.Empty;
            ApplyAuthorization(Client);
        }

        private static void ApplyAuthorization(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = string.IsNullOrWhiteSpace(_bearerToken)
                ? null
                : new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _bearerToken);
        }

        /// <summary>
        /// Tự động dò tìm cổng API đang chạy (5186 hoặc 7118)
        /// </summary>
        public static async Task<bool> EnsureConnectionAsync()
        {
            string[] testUrls = { "https://localhost:7118/api/", "http://localhost:5186/api/" };
            foreach (var url in testUrls)
            {
                try
                {
                    var testClient = CreateClient(url);
                    ApplyAuthorization(testClient);
                    var res = await testClient.GetAsync("users/health");
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
