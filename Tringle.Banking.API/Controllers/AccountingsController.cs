using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tringle.Banking.API.Helpers;
using Tringle.Banking.API.Models;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRedisService<Transfer> _redisService;
        private const string accountTableKey = "Accounts";
        private const string transferTableKey = "Transfers";

        public AccountingsController(IAccountService accountService, IRedisService<Transfer> redisService)
        {
            _accountService = accountService;
            _redisService = redisService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _redisService.GetHashAllAsync(transferTableKey));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransferAddModel transfer)
        {
            ResultLog resultLog = new ResultLog();
            if (ModelState.IsValid)
            {
                var isSenderExist = await _redisService.IsExistKey(accountTableKey, transfer.SenderAccountNumber);
                var isReceiverExist = await _redisService.IsExistKey(accountTableKey, transfer.ReceiverAccountNumber);

                //Check accounts
                if (!isSenderExist || !isReceiverExist)
                {
                    return StatusCode((int)Enums.Enums.StatusCode.INVALID_CARD_NUMBER,
                        ActionResultHelper.CreateActionResultJson(
                            Enums.Enums.StatusCode.INVALID_CARD_NUMBER, 
                        "Please check accounts again"));
                }
                var sender = await _accountService.GetHashOneAsync(accountTableKey, transfer.SenderAccountNumber);
                var receiver = await _accountService.GetHashOneAsync(accountTableKey, transfer.ReceiverAccountNumber);

                //Check Currency Type is same
                if (!sender.CurrencyCode.Equals(receiver.CurrencyCode))
                {
                    return StatusCode((int)Enums.Enums.StatusCode.INVALID_TRANSACTION,
                        ActionResultHelper.CreateActionResultJson(
                            Enums.Enums.StatusCode.INVALID_TRANSACTION,
                        "CurrencyCode of the accounts are not same"));
                }

                //Check sender balance
                if (sender.Balance < transfer.Amount)
                {
                    return StatusCode((int)Enums.Enums.StatusCode.NOT_SUFFICIENT_FUNDS,
                        ActionResultHelper.CreateActionResultJson(
                            Enums.Enums.StatusCode.NOT_SUFFICIENT_FUNDS,
                        "There is not enough money in your account"));
                }

                Transfer tr = new Transfer
                {
                    SenderAccountNumber = transfer.SenderAccountNumber,
                    ReceiverAccountNumber = transfer.ReceiverAccountNumber,
                    Amount = transfer.Amount
                };

                var operationResult = await _accountService.MakeTransfer(tr);
                    
                
                if (operationResult)
                {
                    var r = await _redisService.CreateHashSetDataAsync(transferTableKey, new Random().Next(int.MinValue, int.MaxValue), tr);
                    resultLog.IsError = !r;
                    return Ok(resultLog);
                }
                resultLog.IsError = true;
                return BadRequest(resultLog);
            }
            return ValidationProblem();
        }
    }
}
