using BankingApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    public interface IAccount
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void FundTransfer(IAccount targetAccount, double amount);
        //string ToString();
        event TransactionEventHandler DepositEvent;
        event TransactionEventHandler WithdrawEvent;
    }
}
