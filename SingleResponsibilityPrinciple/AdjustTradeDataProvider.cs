using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    internal class AdjustTradeDataProvider : ITradeDataProvider
    {

        ITradeDataProvider origProvider;
        AdjustTradeDataProvider(ITradeDataProvider origProvider)
        {
            origProvider = origProvider;
        }

        public IEnumerable<string> GetTradeData()
        {
            // Call original GetTradeData
            var tradeData = origProvider.GetTradeData();

            // Change "GBP" to "EUR" in each trade data entry
            return tradeData.Select(data => data.Replace("GBP", "EUR"));
        }
    }
}
