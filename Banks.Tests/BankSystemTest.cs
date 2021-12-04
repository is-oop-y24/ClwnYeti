using System;
using System.Collections.Generic;
using Banks.Classes;
using Banks.Interfaces;
using Banks.Services;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class Test
    {
        private ICentralBank _centralBank;
        private Client _client;
        private Bank _bank;
        private Bank _secondBank;
        private Client _secondClient;

        [SetUp]
        public void Setup()
        {
            _centralBank = CentralBank.GetInstance();
            _client = new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258");
            _bank = _centralBank.AddBank("Банк Приколов");
            _secondBank = _centralBank.AddBank("Банк Приколов");
            _secondClient = _secondBank.AddClient(new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258"));
        }

        [Test]
        public void AddClient()
        {
            Client clientValue = _bank.GetClient(_bank.AddClient(_client).Id);
            Assert.AreEqual(_client.Name, clientValue.Name);
            Assert.AreEqual(_client.Surname, clientValue.Surname);
            Assert.AreEqual(_client.Address, clientValue.Address);
            Assert.AreEqual(_client.PassportId, clientValue.PassportId);
        }
        
        [Test]
        public void AddAccountsForClientAndMakeATransferAndCancelIt_ClientIsVerified()
        {
            IAccount firstAccount = _bank.AddCreditAccountForClient(_client.Id);
            string firstInfoOfFirstAccount = firstAccount.GetInfo();
            IAccount secondAccount = _bank.AddCreditAccountForClient(_client.Id);
            string firstInfoOfSecondAccount = secondAccount.GetInfo();
            List<Transaction> firstTransactions = _bank.TransferBetweenAccounts(firstAccount.GetId(), 100, secondAccount.GetId());
            List<Transaction> secondTransactions = _bank.TransferBetweenAccounts(firstAccount.GetId(), 100, secondAccount.GetId());
            string secondInfoOfFirstAccount = firstAccount.GetInfo();
            string secondInfoOfSecondAccount = secondAccount.GetInfo();
            _centralBank.CancelLastTransactionOfAccountInBank(_bank.Id, firstAccount.GetId());
            _centralBank.CancelLastTransactionOfAccountInBank(_bank.Id, firstAccount.GetId());
            string thirdInfoOfFirstAccount = firstAccount.GetInfo();
            string thirdInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreNotEqual(firstInfoOfFirstAccount, firstInfoOfSecondAccount);
            Assert.AreNotEqual(firstInfoOfFirstAccount, firstInfoOfSecondAccount);
            Assert.AreEqual(firstTransactions[0], firstTransactions[1]);
            Assert.AreEqual(firstTransactions[0].Money, firstTransactions[1].Money);
            Assert.AreEqual(secondTransactions[0], secondTransactions[1]);
            Assert.AreNotEqual(secondTransactions[0].Money, secondTransactions[1].Money);
            Assert.AreEqual(firstTransactions[0].AccountIdFrom, firstAccount.GetId());
            Assert.AreEqual(firstTransactions[0].AccountIdTo, secondAccount.GetId());
            Assert.AreNotEqual(firstInfoOfFirstAccount, secondInfoOfFirstAccount);
            Assert.AreNotEqual(firstInfoOfSecondAccount, secondInfoOfSecondAccount);
            Assert.AreEqual(0, firstAccount.CheckCommission(_bank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(_bank.Configuration));
            Assert.AreEqual(firstInfoOfFirstAccount, thirdInfoOfFirstAccount);
            Assert.AreEqual(firstInfoOfSecondAccount, thirdInfoOfSecondAccount);
        }
        
                [Test]
        public void AddAccountsForClientAndMakeATransferAndCancelIt_ClientIsVerified_DifferentBanks()
        {
            IAccount firstAccount = _bank.AddCreditAccountForClient(_client.Id);
            string firstInfoOfFirstAccount = firstAccount.GetInfo();
            IAccount secondAccount = _secondBank.AddCreditAccountForClient(_secondClient.Id);
            string firstInfoOfSecondAccount = secondAccount.GetInfo();
            List<Transaction> firstTransactions = _centralBank.TransferBetweenAccountsFromDifferentBanks(_bank.Id, firstAccount.GetId(), _secondBank.Id, secondAccount.GetId(), 100);
            List<Transaction> secondTransactions = _centralBank.TransferBetweenAccountsFromDifferentBanks(_bank.Id, firstAccount.GetId(), _secondBank.Id, secondAccount.GetId(), 100);
            string secondInfoOfFirstAccount = firstAccount.GetInfo();
            string secondInfoOfSecondAccount = secondAccount.GetInfo();
            _centralBank.CancelLastTransactionOfAccountInBank(_bank.Id, firstAccount.GetId());
            _centralBank.CancelLastTransactionOfAccountInBank(_bank.Id, firstAccount.GetId());
            string thirdInfoOfFirstAccount = firstAccount.GetInfo();
            string thirdInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreNotEqual(firstInfoOfFirstAccount, firstInfoOfSecondAccount);
            Assert.AreEqual(firstTransactions[0], firstTransactions[1]);
            Assert.AreEqual(firstTransactions[0].Money, firstTransactions[1].Money);
            Assert.AreEqual(secondTransactions[0], secondTransactions[1]);
            Assert.AreNotEqual(secondTransactions[0].Money, secondTransactions[1].Money);
            Assert.AreEqual(firstTransactions[0].AccountIdFrom, firstAccount.GetId());
            Assert.AreEqual(firstTransactions[0].AccountIdTo, secondAccount.GetId());
            Assert.AreNotEqual(firstInfoOfFirstAccount, secondInfoOfFirstAccount);
            Assert.AreNotEqual(firstInfoOfSecondAccount, secondInfoOfSecondAccount);
            Assert.AreEqual(0, firstAccount.CheckCommission(_bank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(_secondBank.Configuration));
            Assert.AreEqual(firstInfoOfFirstAccount, thirdInfoOfFirstAccount);
            Assert.AreEqual(firstInfoOfSecondAccount, thirdInfoOfSecondAccount);
        }
        
        [Test]
        public void AddAccountsForClientAndMakeATransfer_ClientIsNotVerified()
        {
            Bank bank = _centralBank.AddBank("Банк Приколов");
            Client clientValue = bank.AddClient(new Client("Миксаил", "Кузутов", string.Empty, string.Empty));
            IAccount firstAccount = bank.AddCreditAccountForClient(clientValue.Id);
            IAccount secondAccount = bank.AddCreditAccountForClient(clientValue.Id);
            Assert.Catch<BankException>(() =>
            {
                bank.TransferBetweenAccounts(firstAccount.GetId(), 2000, secondAccount.GetId());
            });
        }
        
        [Test]
        public void AddAccountsForClientAndSkipTime()
        {
            IAccount account = _bank.AddDebitAccountForClient(_client.Id);
            string firstInfo = account.GetInfo();
            Transaction transaction = _bank.ReplenishToAccount(100, account.GetId());
            string secondInfo = account.GetInfo();
            _centralBank.SkipTime(new TimeSpan(40, 0, 0, 0));
            string thirdInfo = account.GetInfo();
            Assert.AreEqual(transaction.AccountIdTo, account.GetId());
            Assert.AreNotEqual(firstInfo, secondInfo);
            Assert.AreEqual(transaction.AccountIdTo, account.GetId());
            Assert.AreNotEqual(firstInfo, thirdInfo);
        }
    }
}