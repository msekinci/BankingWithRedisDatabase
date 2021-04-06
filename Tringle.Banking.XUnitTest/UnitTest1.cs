using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tringle.Banking.API.Controllers;
using Tringle.Banking.API.Enums;
using Tringle.Banking.API.Models;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.Entities.Concrete;
using Xunit;

namespace Tringle.Banking.XUnitTest
{
    public class UnitTest1
    {
        AccountsController _accountsController;
        AccountingsController _accountingController;
        IAccountService _accountService;
        IRedisService<Transfer> _redisService;

        public UnitTest1()
        {
            _accountService = new ApiServiceFake();
            _redisService = new ApiServiceFake();
            _accountsController = new AccountsController(_accountService);
            _accountingController = new AccountingsController(_accountService, _redisService);
        }

        [Fact]
        public void Get_Accounts_WhenCalled_ReturnsOkResult()
        {
            var okResult = _accountsController.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _accountsController.GetAll().Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Account>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public void Add_AlreadyExistObjectPassed_ReturnsConflictRequest()
        {
            // Arrange
            var newAccount = new AccountAddModel
            {
                AccountNumber = 99,
                Balance = 10,
                CurrencyCode = Enums.CurrencyCode.TRY
            };
            // Act
            var conflictResponse = _accountsController.Create(newAccount).Result;
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(conflictResponse);
            Assert.Equal(409, objectResult.StatusCode);
        }

        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var newAccount = new AccountAddModel
            {
                AccountNumber = 88,
                Balance = 100,
                CurrencyCode = Enums.CurrencyCode.TRY
            };
            // Act
            var createdResponse = _accountsController.Create(newAccount).Result;
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(createdResponse);
            Assert.Equal(201, objectResult.StatusCode);
        }

        [Fact]
        public void MakeTransfer_Without_ValidBalance_ReturnsNotSuffientFunds()
        {
            // Arrange
            var transferAddModel = new TransferAddModel()
            {
                Amount = 1000,
                ReceiverAccountNumber = 99,
                SenderAccountNumber = 98
            };

            // Act
            var createdResponse = _accountingController.Create(transferAddModel).Result;
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(createdResponse);
            Assert.Equal(10051, objectResult.StatusCode);
        }

        [Fact]
        public void MakeTransfer_Without_SameCurrencyCode_ReturnsInvalidTransaction()
        {
            // Arrange
            var transferAddModel = new TransferAddModel()
            {
                Amount = 10,
                ReceiverAccountNumber = 99,
                SenderAccountNumber = 97
            };

            // Act
            var createdResponse = _accountingController.Create(transferAddModel).Result;
            // Assert
            var objectResult = Assert.IsType<ObjectResult>(createdResponse);
            Assert.Equal(10012, objectResult.StatusCode);
        }
    }
}
