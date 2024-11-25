using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class RestfulTradeDataProvider : ITradeDataProvider
    {
        private readonly string url;
        private readonly ILogger logger;
        private readonly HttpClient client = new HttpClient();

        public RestfulTradeDataProvider(string url, ILogger logger)
        {
            this.url = url;
            this.logger = logger;
        }

        private async Task<List<string>> GetTradeAsync()
        {
            logger.LogInfo("Connecting to the Restful server using HTTP");
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Read and deserialize the response content
                    string content = await response.Content.ReadAsStringAsync();
                    var tradesString = JsonSerializer.Deserialize<List<string>>(content);
                    logger.LogInfo("Received trade strings of length = " + tradesString.Count);
                    return tradesString;
                }
                else
                {
                    logger.LogError("Failed to retrieve data. HTTP Status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error fetching trades: {ex.Message}");
            }

            return null; // Return null if an error occurred or the response wasn't successful
        }

        public async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            var tradeList = await GetTradeAsync();

            // Return the result if it's not null; otherwise, return an empty list
            return tradeList ?? new List<string>();
        }
    }
}
