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
            Assert.AreEqual(0, firstAccount.CheckCommission(bank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(bank.Configuration));
            List<Transaction> firstTransactions = bank.TransferBetweenAccounts(firstAccount.GetId(), 100, secondAccount.GetId());
            List<Transaction> secondTransactions = bank.TransferBetweenAccounts(firstAccount.GetId(), 100, secondAccount.GetId());
            Assert.AreEqual(firstTransactions[0], firstTransactions[1]);
            Assert.AreEqual(-700 , ((CreditAccount)firstAccount).Balance);
            Assert.AreEqual(200 , ((CreditAccount)secondAccount).Balance);
            Assert.AreEqual(firstTransactions[0].Money, firstTransactions[1].Money);
            Assert.AreEqual(secondTransactions[0], secondTransactions[1]);
            Assert.AreNotEqual(secondTransactions[0].Money, secondTransactions[1].Money);
            Assert.AreEqual(500, firstAccount.CheckCommission(bank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(bank.Configuration));
            string secondInfoOfFirstAccount = firstAccount.GetInfo();
            string secondInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreEqual(firstTransactions[0].AccountIdFrom, firstAccount.GetId());
            Assert.AreEqual(firstTransactions[0].AccountIdTo, secondAccount.GetId());
            Assert.AreNotEqual(firstInfoOfFirstAccount, secondInfoOfFirstAccount);
            Assert.AreNotEqual(firstInfoOfSecondAccount, secondInfoOfSecondAccount);
            _centralBank.CancelLastTransactionOfAccountInBank(bank.Id, firstAccount.GetId());
            _centralBank.CancelLastTransactionOfAccountInBank(bank.Id, firstAccount.GetId());
            Assert.AreEqual(0, firstAccount.CheckCommission(bank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(bank.Configuration));
            string thirdInfoOfFirstAccount = firstAccount.GetInfo();
            string thirdInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreEqual(firstInfoOfFirstAccount, thirdInfoOfFirstAccount);
            Assert.AreEqual(firstInfoOfSecondAccount, thirdInfoOfSecondAccount);
        }
        
                [Test]
        public void AddAccountsForClientAndMakeATransferAndCancelIt_ClientIsVerified_DifferentBanks()
        {
            Bank firstBank = _centralBank.AddBank("Банк Приколов");
            Client firstClient = firstBank.AddClient(new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258"));
            Bank secondBank = _centralBank.AddBank("Банк Приколов");
            Client secondClient = secondBank.AddClient(new Client("Миксаил", "Кузутов", "Пупкина 12", "159357456258"));
            IAccount firstAccount = firstBank.AddCreditAccountForClient(firstClient.Id);
            string firstInfoOfFirstAccount = firstAccount.GetInfo();
            IAccount secondAccount = secondBank.AddCreditAccountForClient(secondClient.Id);
            string firstInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreNotEqual(firstInfoOfFirstAccount, firstInfoOfSecondAccount);
            Assert.AreEqual(0, firstAccount.CheckCommission(firstBank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(secondBank.Configuration));
            List<Transaction> firstTransactions = _centralBank.TransferBetweenAccountsFromDifferentBanks(firstBank.Id, firstAccount.GetId(), secondBank.Id, secondAccount.GetId(), 100);
            List<Transaction> secondTransactions = _centralBank.TransferBetweenAccountsFromDifferentBanks(firstBank.Id, firstAccount.GetId(), secondBank.Id, secondAccount.GetId(), 100);
            Assert.AreEqual(-700 , ((CreditAccount)firstAccount).Balance);
            Assert.AreEqual(200 , ((CreditAccount)secondAccount).Balance);
            Assert.AreEqual(firstTransactions[0], firstTransactions[1]);
            Assert.AreEqual(firstTransactions[0].Money, firstTransactions[1].Money);
            Assert.AreEqual(secondTransactions[0], secondTransactions[1]);
            Assert.AreNotEqual(secondTransactions[0].Money, secondTransactions[1].Money);
            Assert.AreEqual(500, firstAccount.CheckCommission(firstBank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(secondBank.Configuration));
            string secondInfoOfFirstAccount = firstAccount.GetInfo();
            string secondInfoOfSecondAccount = secondAccount.GetInfo();
            Assert.AreEqual(firstTransactions[0].AccountIdFrom, firstAccount.GetId());
            Assert.AreEqual(firstTransactions[0].AccountIdTo, secondAccount.GetId());
            Assert.AreNotEqual(firstInfoOfFirstAccount, secondInfoOfFirstAccount);
            Assert.AreNotEqual(firstInfoOfSecondAccount, secondInfoOfSecondAccount);
            _centralBank.CancelLastTransactionOfAccountInBank(firstBank.Id, firstAccount.GetId());
            _centralBank.CancelLastTransactionOfAccountInBank(firstBank.Id, firstAccount.GetId());
            Assert.AreEqual(0, firstAccount.CheckCommission(firstBank.Configuration));
            Assert.AreEqual(0, secondAccount.CheckCommission(secondBank.Configuration));
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