namespace RapidPayApi.Services.Interfaces;

public interface IUniversalFeesExchangeService
{
    Task<decimal> GetFee();
}