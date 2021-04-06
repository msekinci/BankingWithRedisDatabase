using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tringle.Banking.Business.Interfaces;
using Tringle.Banking.Entities.Concrete;

namespace Tringle.Banking.XUnitTest
{
    public class ApiServiceFake : IAccountService, ITransferService
    {
        private readonly List<Account> _accounts;
        private readonly List<Transfer> _transfers;

        public ApiServiceFake()
        {
            _accounts = new List<Account>()
            {
                new Account() { AccountNumber = 99, CurrencyCode = "TRY", Balance = 150 },
                new Account() { AccountNumber = 98, CurrencyCode = "TRY", Balance = 350 },
                new Account() { AccountNumber = 97, CurrencyCode = "USD", Balance = 100 },
                new Account() { AccountNumber = 96, CurrencyCode = "USD", Balance = 0 },
                new Account() { AccountNumber = 95, CurrencyCode = "EUR", Balance = 150 },
            };

            _transfers = new List<Transfer>()
            {
                new Transfer() { SenderAccountNumber = 99, ReceiverAccountNumber = 98, Amount = 50},
                new Transfer() { SenderAccountNumber = 97, ReceiverAccountNumber = 96, Amount = 150}
            };
        }

        public Task<bool> CreateHashSetDataAsync(string tableKey, int key, Account obj)
        {
            _accounts.Add(obj);
            return Task.FromResult(true);
        }

        public Task<bool> CreateHashSetDataAsync(string tableKey, int key, Transfer obj)
        {
            _transfers.Add(obj);
            return Task.FromResult(true);
        }

        public Task<List<Account>> GetHashAllAsync(string tableKey)
        {
            return Task.FromResult(_accounts);
        }

        public Task<Account> GetHashOneAsync(string tableKey, int key)
        {
            return Task.FromResult(_accounts.Where(x => x.AccountNumber == key).FirstOrDefault());
        }

        public Task<bool> IsExistKey(string tableKey, int key)
        {
            return Task.FromResult(_accounts.Where(x => x.AccountNumber == key).Any());
        }

        public Task<bool> MakeTransfer(Transfer transfer)
        {
            _transfers.Add(transfer);
            return Task.FromResult(true);
        }

        Task<List<Transfer>> IRedisService<Transfer>.GetHashAllAsync(string tableKey)
        {
            return Task.FromResult(_transfers);
        }

        Task<Transfer> IRedisService<Transfer>.GetHashOneAsync(string tableKey, int key)
        {
            throw new NotImplementedException();
        }
    }
}
