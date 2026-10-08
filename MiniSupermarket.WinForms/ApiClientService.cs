using System;
using System.Net.Http;

namespace MiniSupermarket.WinForms
{
    public static class ApiClientService
    {
        // Khởi tạo HttpClient dùng chung kết nối Web API
        public static HttpClient Client { get; } = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7118/api/")
        };
    }
}
