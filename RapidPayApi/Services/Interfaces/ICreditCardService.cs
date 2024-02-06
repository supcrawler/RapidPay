using RapidPayApi.Data.Models;

namespace RapidPayApi.Services.Interfaces;

public interface ICreditCardService
{
    Task<bool> AddCreditCardAsync(CreditCard card);
    
    Task<decimal> GetCreditCardBalanceAsync(string cardNumber);
    
    Task<PaymentResponse> PayAsync(string? cardNumber, decimal amount);
    
    Task<bool> IsValidCardNumber(string cardNumber);
}