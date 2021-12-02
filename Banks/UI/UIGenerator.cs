using System;
using Banks.Tools;

namespace Banks.UI
{
    public class UIGenerator
    {
        private readonly UIHandler _handler;
        private bool _isRunning;

        public UIGenerator()
        {
            _handler = new UIHandler();
            _isRunning = false;
        }

        public void Run()
        {
            _isRunning = true;
            while (_isRunning)
            {
                ReadACommand();
            }
        }

        private void ReadACommand()
        {
            string[] commandLine = Console.ReadLine()?.Split(" ");
            if (commandLine == null) throw new BankException("Invalid command");
            if (commandLine.Length == 0)
            {
                _isRunning = false;
                throw new BankException("Invalid command");
            }

            string command = commandLine[0];
            commandLine = commandLine[1..];
            switch (command)
            {
                case "AddBankWithDefaultConfiguration":
                {
                    _handler.AddBankWithDefaultConfiguration(commandLine);
                    break;
                }

                case "AddBankWithCreatedConfiguration":
                {
                    _handler.AddBankWithCreatedConfiguration(commandLine);
                    break;
                }

                case "SkipTime":
                {
                    _handler.SkipTime(commandLine);
                    break;
                }

                case "SetCreatedDebitConfigurationForUsedBankConfiguration":
                {
                    _handler.SetCreatedDebitConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetCreatedDepositConfigurationForUsedBankConfiguration":
                {
                    _handler.SetCreatedDepositConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetCreatedCreditConfigurationForUsedBankConfiguration":
                {
                    _handler.SetCreatedCreditConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetDefaultDebitConfigurationForUsedBankConfiguration":
                {
                    _handler.SetDefaultDebitConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetDefaultDepositConfigurationForUsedBankConfiguration":
                {
                    _handler.SetDefaultDepositConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetDefaultCreditConfigurationForUsedBankConfiguration":
                {
                    _handler.SetDefaultCreditConfigurationForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetCriticalAmountOfMoneyForUsedBankConfiguration":
                {
                    _handler.SetCriticalAmountOfMoneyForUsedBankConfiguration(commandLine);
                    break;
                }

                case "SetInterestForUsedDebitAccountConfiguration":
                {
                    _handler.SetInterestForUsedDebitAccountConfiguration(commandLine);
                    break;
                }

                case "SetDefaultInterestForUsedDepositAccountConfiguration":
                {
                    _handler.SetDefaultInterestForUsedDepositAccountConfiguration(commandLine);
                    break;
                }

                case "SetCreditLimitForUsedCreditAccountConfiguration":
                {
                    _handler.SetCreditLimitForUsedCreditAccountConfiguration(commandLine);
                    break;
                }

                case "SetCommissionForUsedCreditAccountConfiguration":
                {
                    _handler.SetCommissionForUsedCreditAccountConfiguration(commandLine);
                    break;
                }

                case "SetInterestRangesForUsedDepositAccountConfiguration":
                {
                    _handler.SetInterestRangesForUsedDepositAccountConfiguration(commandLine);
                    break;
                }

                case "SetUsedDebitAccountConfigurationToDefault":
                {
                    _handler.SetUsedDebitAccountConfigurationToDefault(commandLine);
                    break;
                }

                case "SetUsedDepositAccountConfigurationToDefault":
                {
                    _handler.SetUsedDepositAccountConfigurationToDefault(commandLine);
                    break;
                }

                case "SetUsedCreditAccountConfigurationToDefault":
                {
                    _handler.SetUsedCreditAccountConfigurationToDefault(commandLine);
                    break;
                }

                case "AddCreatedClientForBank":
                {
                    _handler.AddCreatedClientForBank(commandLine);
                    break;
                }

                case "SetNameForUsedClient":
                {
                    _handler.SetNameForUsedClient(commandLine);
                    break;
                }

                case "SetSurnameForUsedClient":
                {
                    _handler.SetSurnameForUsedClient(commandLine);
                    break;
                }

                case "SetAddressForUsedClient":
                {
                    _handler.SetAddressForUsedClient(commandLine);
                    break;
                }

                case "SetUsedClientToExistingClient":
                {
                    _handler.SetUsedClientToExistingClient(commandLine);
                    break;
                }

                case "SetUsedClientToDefault":
                {
                    _handler.SetUsedClientToDefault(commandLine);
                    break;
                }

                case "SetDebitAccountConfigurationForExistingBank":
                {
                    _handler.SetDebitAccountConfigurationForExistingBank(commandLine);
                    break;
                }

                case "SetPassportIdForUsedClient":
                {
                    _handler.SetPassportIdForUsedClient(commandLine);
                    break;
                }

                case "SetDepositAccountConfigurationForExistingBank":
                {
                    _handler.SetDepositAccountConfigurationForExistingBank(commandLine);
                    break;
                }

                case "SetCreditAccountConfigurationForExistingBank":
                {
                    _handler.SetCreditAccountConfigurationForExistingBank(commandLine);
                    break;
                }

                case "UpdateExistingClientToCreatedClientInBank":
                {
                    _handler.UpdateExistingClientToCreatedClientInBank(commandLine);
                    break;
                }

                case "ReplenishToAccountInBank":
                {
                    _handler.ReplenishToAccountInBank(commandLine);
                    break;
                }

                case "WithdrawFromAccountInBank":
                {
                    _handler.WithdrawFromAccountInBank(commandLine);
                    break;
                }

                case "TransferBetweenAccountsInBank":
                {
                    _handler.TransferBetweenAccountsInBank(commandLine);
                    break;
                }

                case "CancelLastTransferOfAccountInBank":
                {
                    _handler.CancelLastTransferOfAccountInBank(commandLine);
                    break;
                }

                case "CreateDebitAccountForClientInBank":
                {
                    _handler.CreateDebitAccountForClientInBank(commandLine);
                    break;
                }

                case "CreateDepositAccountForClientInBank":
                {
                    _handler.CreateDepositAccountForClientInBank(commandLine);
                    break;
                }

                case "CreateCreditAccountForClientInBank":
                {
                    _handler.CreateCreditAccountForClientInBank(commandLine);
                    break;
                }

                case "Stop":
                {
                    _isRunning = false;
                    break;
                }

                default:
                {
                    _isRunning = false;
                    throw new BankException("Invalid command");
                }
            }
        }
    }
}