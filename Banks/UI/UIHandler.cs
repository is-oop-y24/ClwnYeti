using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Interfaces;
using Banks.Services;
using Banks.Tools;

namespace Banks.UI
{
    public class UIHandler
    {
        private readonly ICentralBank _centralBank;
        private readonly BankConfigurationBuilder _bankConfigurationBuilder;
        private readonly DebitAccountConfigurationBuilder _debitAccountConfigurationBuilder;
        private readonly DepositAccountConfigurationBuilder _depositAccountConfigurationBuilder;
        private readonly CreditAccountConfigurationBuilder _creditAccountConfigurationBuilder;
        private readonly ClientBuilder _clientBuilder;

        public UIHandler()
        {
            _centralBank = CentralBank.GetInstance();
            _bankConfigurationBuilder = new BankConfigurationBuilder();
            _debitAccountConfigurationBuilder = new DebitAccountConfigurationBuilder();
            _depositAccountConfigurationBuilder = new DepositAccountConfigurationBuilder();
            _creditAccountConfigurationBuilder = new CreditAccountConfigurationBuilder();
            _clientBuilder = new ClientBuilder();
        }

        public void AddBankWithDefaultConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                Bank bank = _centralBank.AddBank(string.Join(" ", arguments));
                Console.WriteLine("Bank was created");
                Console.WriteLine(bank.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddBankWithCreatedConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                Bank bank = _centralBank.AddBank(string.Join(" ", arguments), _bankConfigurationBuilder.Build());
                Console.WriteLine("Bank was created");
                Console.WriteLine(bank.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SkipTime(IEnumerable<string> arguments)
        {
            try
            {
                var time = TimeSpan.Parse(string.Join(" ", arguments));
                _centralBank.SkipTime(time);
                Console.WriteLine("Time was skipped");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCreatedDebitConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithDebitAccountConfiguration(_debitAccountConfigurationBuilder.Build());
                Console.WriteLine("Debit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCreatedDepositConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithDepositAccountConfiguration(_depositAccountConfigurationBuilder.Build());
                Console.WriteLine("Deposit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCreatedCreditConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithCreditAccountConfiguration(_creditAccountConfigurationBuilder.Build());
                Console.WriteLine("Credit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDefaultDebitConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithDebitAccountConfiguration(new DebitAccountConfiguration());
                Console.WriteLine("Debit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDefaultDepositConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithDepositAccountConfiguration(new DepositAccountConfiguration());
                Console.WriteLine("Deposit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDefaultCreditConfigurationForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithCreditAccountConfiguration(new CreditAccountConfiguration());
                Console.WriteLine("Credit Account Configuration was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void SetCriticalAmountOfMoneyForUsedBankConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _bankConfigurationBuilder.WithCriticalAmountOfMoney(decimal.Parse(enumerable[0]));
                Console.WriteLine("Critical amount of money was set to used Bank Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetInterestForUsedDebitAccountConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _debitAccountConfigurationBuilder.WithInterest(decimal.Parse(enumerable[0]));
                Console.WriteLine("Interest was set to used Debit Account Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDefaultInterestForUsedDepositAccountConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _depositAccountConfigurationBuilder.WithDefaultInterest(decimal.Parse(enumerable[0]));
                Console.WriteLine("Default interest was set to used Deposit Account Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCreditLimitForUsedCreditAccountConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _creditAccountConfigurationBuilder.WithCreditLimit(decimal.Parse(enumerable[0]));
                Console.WriteLine("Credit limit was set to used Credit Account Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCommissionForUsedCreditAccountConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _creditAccountConfigurationBuilder.WithCommission(decimal.Parse(enumerable[0]));
                Console.WriteLine("Commission was set to used Credit Account Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetInterestRangesForUsedDepositAccountConfiguration(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() % 3 != 0) throw new BankException("Invalid number of arguments");
                var interestRanges = new List<InterestRange>();
                for (int i = 0; i < enumerable.Count() / 3; i++)
                {
                    interestRanges.Add(new InterestRange(decimal.Parse(enumerable[3 * i]), decimal.Parse(enumerable[(3 * i) + 1]), decimal.Parse(enumerable[(3 * i) + 2])));
                }

                _depositAccountConfigurationBuilder.WithInterestRanges(interestRanges);
                Console.WriteLine("Interest ranges was set to used Deposit Account Configuration");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUsedDebitAccountConfigurationToDefault(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _debitAccountConfigurationBuilder.SetToDefault();
                Console.WriteLine("Debit Account Configuration was set to default");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUsedDepositAccountConfigurationToDefault(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _depositAccountConfigurationBuilder.SetToDefault();
                Console.WriteLine("Deposit Account Configuration was set to default");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUsedCreditAccountConfigurationToDefault(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _creditAccountConfigurationBuilder.SetToDefault();
                Console.WriteLine("Credit Account Configuration was set to default");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddCreatedClientForBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                Client client = _centralBank.GetBank(Guid.Parse(enumerable[0])).AddClient(_clientBuilder.Build());
                Console.WriteLine("Client was created");
                Console.WriteLine(client.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetNameForUsedClient(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _clientBuilder.WithName(enumerable[0]);
                Console.WriteLine("Name was set for used client");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetSurnameForUsedClient(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _clientBuilder.WithSurname(enumerable[0]);
                Console.WriteLine("Surname was set for used client");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetAddressForUsedClient(IEnumerable<string> arguments)
        {
            try
            {
                _clientBuilder.WithAddress(string.Join(" ", arguments));
                Console.WriteLine("Address was set for used client");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetPassportIdForUsedClient(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                _clientBuilder.WithPassportId(enumerable[0]);
                Console.WriteLine("Address was set for used client");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUsedClientToExistingClient(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 2) throw new BankException("Invalid number of arguments");
                _clientBuilder.SetToExistingClient(_centralBank.GetBank(Guid.Parse(enumerable[0])).GetClient(Guid.Parse(enumerable[1])));
                Console.WriteLine("Used client was set");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetUsedClientToDefault(IEnumerable<string> arguments)
        {
            try
            {
                if (arguments.Count() != 0) throw new BankException("Invalid number of arguments");
                _clientBuilder.SetToDefault();
                Console.WriteLine("Used client was set");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDebitAccountConfigurationForExistingBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                Bank bank = _centralBank.GetBank(Guid.Parse(enumerable[0]));
                bank.Configuration.DebitAccountConfiguration = _debitAccountConfigurationBuilder.Build();
                Console.WriteLine("Debit Account Configuration was updated");
                foreach (string s in bank.Notify(new DebitAccount()))
                {
                    Console.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetDepositAccountConfigurationForExistingBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                Bank bank = _centralBank.GetBank(Guid.Parse(enumerable[0]));
                bank.Configuration.DepositAccountConfiguration = _depositAccountConfigurationBuilder.Build();
                Console.WriteLine("Deposit Account Configuration was updated");
                foreach (string s in bank.Notify(new DepositAccount()))
                {
                    Console.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetCreditAccountConfigurationForExistingBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 1) throw new BankException("Invalid number of arguments");
                Bank bank = _centralBank.GetBank(Guid.Parse(enumerable[0]));
                bank.Configuration.CreditAccountConfiguration = _creditAccountConfigurationBuilder.Build();
                Console.WriteLine("Credit Account Configuration was updated");
                foreach (string s in bank.Notify(new DepositAccount()))
                {
                    Console.WriteLine(s);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateExistingClientToCreatedClientInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 2) throw new BankException("Invalid number of arguments");
                _centralBank.GetBank(Guid.Parse(enumerable[0])).UpdateClient(Guid.Parse(enumerable[1]), _clientBuilder.Build());
                Console.WriteLine("Client was updated");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ReplenishToAccountInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 3) throw new BankException("Invalid number of arguments");
                _centralBank.GetBank(Guid.Parse(enumerable[0])).ReplenishToAccount(decimal.Parse(enumerable[2]), Guid.Parse(enumerable[1]));
                Console.WriteLine("Replenish was made");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void WithdrawFromAccountInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 3) throw new BankException("Invalid number of arguments");
                _centralBank.GetBank(Guid.Parse(enumerable[0])).WithdrawFromAccount(Guid.Parse(enumerable[1]), decimal.Parse(enumerable[2]));
                Console.WriteLine("Withdraw was made");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void TransferBetweenAccountsInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 4) throw new BankException("Invalid number of arguments");
                _centralBank.GetBank(Guid.Parse(enumerable[0])).TransferBetweenAccounts(Guid.Parse(enumerable[1]), decimal.Parse(enumerable[3]), Guid.Parse(enumerable[2]));
                Console.WriteLine("Transfer was made");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void TransferBetweenAccountsFromDifferentBanks(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 5) throw new BankException("Invalid number of arguments");
                _centralBank.TransferBetweenAccountsFromDifferentBanks(Guid.Parse(enumerable[0]), Guid.Parse(enumerable[1]), Guid.Parse(enumerable[2]), Guid.Parse(enumerable[3]), decimal.Parse(enumerable[3]));
                Console.WriteLine("Transfer was made");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CancelLastTransferOfAccountInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 2) throw new BankException("Invalid number of arguments");
                _centralBank.CancelLastTransactionOfAccountInBank(Guid.Parse(enumerable[0]), Guid.Parse(enumerable[1]));
                Console.WriteLine("Transfer was canceled");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateDebitAccountForClientInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 2) throw new BankException("Invalid number of arguments");
                IAccount account = _centralBank.GetBank(Guid.Parse(enumerable[0])).AddDebitAccountForClient(Guid.Parse(enumerable[1]));
                Console.WriteLine("Debit Account was created");
                Console.WriteLine(account.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateDepositAccountForClientInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 3) throw new BankException("Invalid number of arguments");
                IAccount account = _centralBank.GetBank(Guid.Parse(enumerable[0])).AddDepositAccountForClient(Guid.Parse(enumerable[1]), int.Parse(enumerable[2]));
                Console.WriteLine("Deposit Account was created");
                Console.WriteLine(account.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CreateCreditAccountForClientInBank(IEnumerable<string> arguments)
        {
            try
            {
                var enumerable = arguments.ToList();
                if (enumerable.Count() != 2) throw new BankException("Invalid number of arguments");
                IAccount account = _centralBank.GetBank(Guid.Parse(enumerable[0])).AddCreditAccountForClient(Guid.Parse(enumerable[1]));
                Console.WriteLine("Credit Account was created");
                Console.WriteLine(account.GetInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}