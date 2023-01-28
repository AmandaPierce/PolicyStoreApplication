using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using Xunit;

namespace PolicyStoreApplication.IntegrationTests
{
    public class IntegrationTestBase
    {
        private readonly HttpClient _httpClient;

        public IntegrationTestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("MongoDb"); });
                    });
                });

            _httpClient = appFactory.CreateClient();
        }

    }
}
