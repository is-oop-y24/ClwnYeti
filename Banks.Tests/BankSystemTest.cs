using System;
using System.Text.RegularExpressions;
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

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank();
        }

        [Test]
        public void AddClient()
        {
            var client = new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258");
            Bank bank = _centralBank.AddBank("Банк Приколов");
            Client clientValue = bank.GetClient(bank.AddClient(client).Id);
            Assert.AreEqual(client.Name, clientValue.Name);
            Assert.AreEqual(client.Surname, clientValue.Surname);
            Assert.AreEqual(client.Address, clientValue.Address);
            Assert.AreEqual(client.PassportId, clientValue.PassportId);
        }
        
        [Test]
        public void AddAccountsForClientAndMakeATransferAndCancelIt_ClientIsVerified()
        {
            Bank bank = _centralBank.AddBank("Банк Приколов");
            Client clientValue = bank.AddClient(new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258"));
            IAccount firstAccount = bank.AddCreditAccountForClient(clientValue.Id);
            string firstInfoOfFirstAccount = firstAccount.GetInfo();
            IAccount secondAccount = bank.AddCreditAccountForClient(clientValue.Id);
            string firstInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreNotEqual(firstInfoOfFirstAccount, firstInfoOfSecondAccount);
            Transaction transaction = bank.TransferBetweenAccounts(firstAccount.GetId(), 100, secondAccount.GetId());
            string secondInfoOfFirstAccount = firstAccount.GetInfo();
            string secondInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreEqual(transaction.AccountIdFrom, firstAccount.GetId());
            Assert.AreEqual(transaction.AccountIdTo, secondAccount.GetId());
            Assert.AreNotEqual(firstInfoOfFirstAccount, secondInfoOfFirstAccount);
            Assert.AreNotEqual(firstInfoOfSecondAccount, secondInfoOfSecondAccount);
            _centralBank.CancelLastTransactionOfAccountInBank(bank.Id, firstAccount.GetId());
            string thirdInfoOfFirstAccount = firstAccount.GetInfo();
            string thirdInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreEqual(firstInfoOfFirstAccount, thirdInfoOfFirstAccount);
            Assert.AreEqual(firstInfoOfSecondAccount, thirdInfoOfSecondAccount);
        }
        
        [Test]
        public void AddAccountsForClientAndMakeATransfer_ClientIsNotVerified()
        {
            Assert.Catch<BankException>(() =>
            {
                Bank bank = _centralBank.AddBank("Банк Приколов");
                Client clientValue = bank.AddClient(new Client("Миксаил", "Кузутов", string.Empty, string.Empty));
                IAccount firstAccount = bank.AddCreditAccountForClient(clientValue.Id);
                IAccount secondAccount = bank.AddCreditAccountForClient(clientValue.Id);
                bank.TransferBetweenAccounts(firstAccount.GetId(), 2000, secondAccount.GetId());
            });
        }
        
        [Test]
        public void AddAccountsForClientAndSkipTime()
        {
            Bank bank = _centralBank.AddBank("Банк Приколов");
            Client clientValue = bank.AddClient(new Client("Миксаил", "Кузутов", string.Empty, string.Empty));
            IAccount account = bank.AddDebitAccountForClient(clientValue.Id);
            string firstInfo = account.GetInfo();
            Transaction transaction = bank.ReplenishToAccount(100, account.GetId());
            Assert.AreEqual(transaction.AccountIdTo, account.GetId());
            string secondInfo = account.GetInfo();
            Assert.AreNotEqual(firstInfo, secondInfo);
            _centralBank.SkipTime(new TimeSpan(40, 0, 0, 0));
            string thirdInfo = account.GetInfo();
            Assert.AreNotEqual(firstInfo, thirdInfo);
        }
    }
}