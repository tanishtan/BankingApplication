using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TransactionEventArgs = BankingApp.Common.TransactionEventArgs;

namespace BankingApp.Process
{
    public abstract class Account : IAccount // it is an OCP now
    {   
        protected AccountInfo info;
        public event TransactionEventHandler DepositEvent;
        public event TransactionEventHandler WithdrawEvent;
        private Account() { }

        public Account(int id, string name, AccountType type, double amount)
        {
            info = new AccountInfo(id, name, type, amount);
        }
        public Account(AccountInfo pInfo) { info = pInfo; }

        ~Account() //Destructor
        {
            info = null;
        }
        protected void OnDepositEvent(double amount)
        {
            TransactionEventArgs args = new TransactionEventArgs
            {
                AccountId = info.Id,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "Deposit"
            };
            DepositEvent?.Invoke(this, args);
        }
        protected void OnWithdrawEvent(double amount)
        {
            TransactionEventArgs args = new Common.TransactionEventArgs
            {
                AccountId = info.Id,
                Amount = amount,
                TransactionDate = DateTime.Now,
                TransactionType = "Withdraw"
            };
            WithdrawEvent?.Invoke(this, args);
        }
        public abstract void Deposit(double amount); /*{ info.Balance += amount; }*/
        public /*virtual*/ abstract void Withdraw(double amount);
        /*{
            if (info.Type == AccountType.Savings && (info.Balance - amount) > 5000)
            {
                info.Balance -= amount;
            }
            else if (info.Type == AccountType.Current && (info.Balance - amount) > 10000)
            {
                info.Balance -= amount;
            }
            //info.Balance -= amount;
        }*/
        
        public void FundTransfer(IAccount targetAccount, double amount) 
        {
            this.Withdraw(amount);
            targetAccount.Deposit(amount);   
        }
        
        public override string ToString()
        {
            return $"Id: {info.Id}, Name: {info.Name}, Type: {info.Type}, Balance: {info.Balance}";
        }

        

    }

    
}
