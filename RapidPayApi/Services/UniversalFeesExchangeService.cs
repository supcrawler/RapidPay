using RapidPay.Core;
using RapidPayApi.Services.Interfaces;

namespace RapidPayApi.Services
{
    public class UniversalFeesExchangeService : IUniversalFeesExchangeService
    {
        private decimal _fee = Constants.FEE_INITIALVALUE;
        private readonly Random _random;

        public UniversalFeesExchangeService()
        {
            _random = new();
            UpdateFee();

            _ = new Timer(e => UpdateFee(), null, TimeSpan.Zero, TimeSpan.FromSeconds(Constants.UFE_INTERVAL_SECONDS));
        }

        private void UpdateFee()
        {
            decimal newRandom = 0;
            while (newRandom == 0)
                newRandom = (decimal)_random.NextDouble();

            _fee *= newRandom * 2;
        }

        public async Task<decimal> GetFee()
        {
            return await Task.FromResult(_fee);
        }
    }
}