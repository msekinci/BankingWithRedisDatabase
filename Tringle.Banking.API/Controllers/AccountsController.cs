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
    public class AccountsController : ControllerBase
    {
        private readonly IRedisService<Account> _redisService;
        private const string accountTableKey = "Accounts";

        public AccountsController(IRedisService<Account> redisService)
        {
            _redisService = redisService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountAddModel account)
        {
            ResultLog resultLog = new ResultLog();
            if (ModelState.IsValid)
            {
                if (!await _redisService.IsExistKey(accountTableKey, account.AccountNumber))
                {
                    //ToDo: Use AutoMapper and DTO
                    resultLog.IsError = !await _redisService.CreateHashSetDataAsync(accountTableKey, account.AccountNumber,
                        new Account
                        {
                            AccountNumber = account.AccountNumber,
                            Balance = Math.Round(account.Balance, 2), //limited 2 digits
                            CurrencyCode = account.CurrencyCode.ToString()
                        });

                    if (resultLog.IsError)
                        return StatusCode((int)Enums.Enums.StatusCode.ISSUER_OR_SWITCH_INOPERATIVE, resultLog);
                    return StatusCode((int)Enums.Enums.StatusCode.CREATED, resultLog);
                }
                else
                {
                    return StatusCode((int)Enums.Enums.StatusCode.CONFLICT, 
                        ActionResultHelper.CreateActionResultJson(Enums.Enums.StatusCode.CONFLICT, "AccountNumber should be unique"));
                }
            }
            return ValidationProblem();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _redisService.GetHashAllAsync(accountTableKey));
        }
    }
}
