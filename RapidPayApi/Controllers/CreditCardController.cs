using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RapidPayApi.Data.Models;
using RapidPayApi.Services;
using System.Net;
using RapidPay.Core;
using RapidPayApi.Models;
using RapidPayApi.Services.Interfaces;

namespace RapidPayApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("credit-card")]
    public class CreditCardController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(IUserService userService, ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
            _userService = userService;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            if(model.Username == null || model.Password == null)
                return BadRequest(new { message = Constants.MESSAGE_AUTH_BLANK_CREDENTIALS });

            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = Constants.MESSAGE_AUTH_WRONG_CREDENTIALS });

            return Ok(user);
        }

        [HttpGet("{cardNumber}")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            try
            {
                
                var balance = await _creditCardService.GetCreditCardBalanceAsync(cardNumber);
                return Ok(balance.ToString("F2"));
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateCardRequest cardRequest)
        {
            try 
            {
                if(!await _creditCardService.AddCreditCardAsync(cardRequest.ToCreditCard()))
                    return BadRequest(Constants.MESSAGE_CARD_NUMBER_ALREADY_EXISTS);

                return StatusCode((int)HttpStatusCode.Created, Constants.MESSAGE_CARD_CREATED);
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(PayRequest payRequest)
        {
            try
            {
                PaymentResponse paymentResponse = await _creditCardService.PayAsync(payRequest.CardNumber, payRequest.Amount);
        
                string paymentDetails = $"Old Balance - Amount - Fee = New Balance{Environment.NewLine}" +
                                        $"{paymentResponse.OldBalance.ToString("F2")} - " +
                                        $"{paymentResponse.Amount.ToString("F2")} - " +
                                        $"{paymentResponse.FeeApplied.ToString("F2")} = " +
                                        $"{paymentResponse.NewBalance.ToString("F2")}";

                return Ok(paymentDetails);
            }
            catch (ManagedExceptions ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, Constants.MESSAGE_SYSTEM_EXCEPTION);
            }
        }

    }
}