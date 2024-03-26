using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    internal class Savings : Account
    {
        public Savings(AccountInfo info) : base(info) { }
        public Savings(int id, string name, double amount) : base(id, name, AccountType.Savings, amount) { }

        public sealed override void Deposit(double amount)
        {
            
                info.Balance += amount;
                OnDepositEvent(amount);
            
        }

        public sealed override void Withdraw(double amount)
        {
            if ((info.Balance - amount) > 5000)
            {
                info.Balance -= amount;
                OnWithdrawEvent(amount);
            }
            else
            {
                throw new MinimumBalanceException("Insufficient funds in the account. Transaction was cancelled");
            }
            //info.Balance -= amount;
        }
    }
}
