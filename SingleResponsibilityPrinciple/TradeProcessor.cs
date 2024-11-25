using SingleResponsibilityPrinciple.Contracts;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class TradeProcessor
    {
        public TradeProcessor(ITradeDataProvider tradeDataProvider, ITradeParser tradeParser, ITradeStorage tradeStorage)
        {
            this.tradeDataProvider = tradeDataProvider;
            this.tradeParser = tradeParser;
            this.tradeStorage = tradeStorage;
        }

        public async Task ProcessTradesAsync()
        {
            var lines = await tradeDataProvider.GetTradeDataAsync(); // Await the asynchronous call
            var trades = tradeParser.Parse(lines); // Parse the trades
            tradeStorage.Persist(trades); // Persist the trades
        }

        private readonly ITradeDataProvider tradeDataProvider;
        private readonly ITradeParser tradeParser;
        private readonly ITradeStorage tradeStorage;
    }
}
