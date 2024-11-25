using SingleResponsibilityPrinciple.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SingleResponsibilityPrinciple
{
    public class URLAsyncProvider : ITradeDataProvider
    {
        private readonly ITradeDataProvider baseProvider;

        public URLAsyncProvider(ITradeDataProvider baseProvider)
        {
            this.baseProvider = baseProvider;
        }

        // Updated GetTradeData method to return a Task
        public async Task<IEnumerable<string>> GetTradeDataAsync()
        {
            return await Task.Run(() => baseProvider.GetTradeData());
        }
    }
}
