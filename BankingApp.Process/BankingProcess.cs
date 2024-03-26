using Banking.DataAccess;
using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Process
{
    public class BankingProcess
    {
        public BankingProcess() { }
        public AccountInfo CreateNewAccount(int id,  string name, AccountType type, double amount)
        {
            AccountInfoCollection collections = new AccountInfoCollection();   
            Banking.DataAccess.BankingDataAccess collection = new Banking.DataAccess.BankingDataAccess();
            
            var accInfo = collections.FindAccountBy(id);
            if (accInfo == null)
            {
                var len = collections.GetAllAccounts().Count();
                var nextId = len + 113;
                var acc = AccountFactory.Create(nextId, name, type, amount);
                if (acc is null)
                {
                    return null!;
                }
                accInfo = new AccountInfo(nextId, name, type, amount);
                collection.CreateNewAccount(accInfo);
                collections.AddNew(accInfo);
            }
            return accInfo;
        }
        public AccountInfo[] GetAllAccounts()
        {
            AccountInfoCollection collection = new AccountInfoCollection();
            return collection.GetAllAccounts().ToArray();
        }

        public AccountInfo findAccountById(int id)
        {
            //AccountInfoCollection collection = new AccountInfoCollection();
            BankingDataAccess collection = new BankingDataAccess();
            var accounts = collection.GetAllDetails(id);

            foreach (var account in accounts)
            {
                if (account.Id == id)
                {
                    return account;
                }
            }
            return null;
        }

        public AccountInfo UpdateAccountDetails(AccountInfo account, string StringId, string Name, string balance)
        {
            BankingDataAccess collection = new BankingDataAccess();
            collection.UpdateAccount(Name,int.Parse(StringId));
            if (StringId.Length > 0)
                account.Id = int.Parse(StringId);
            if (Name.Length > 0)
                account.Name = Name;
            if (balance.Length > 0)
                account.Balance = int.Parse(balance);
            
            return account;
        }

        public void AccountTransaction(AccountInfo account, int amount, int choice)
        {
            BankingDataAccess collection = new BankingDataAccess();
            if (choice == 1)
                collection.Deposit(account.Id, amount);
                account.Balance += amount;
                //a.Deposit(amount);
                
            if(choice==2)
                collection.Withdraw(account.Id, amount);
            account.Balance -= amount;
        }
        public void fundTrans(AccountInfo srcAcc, AccountInfo destacc)
        {
            BankingDataAccess collection = new BankingDataAccess();
            Console.WriteLine($"Enter the amount to transfer from {srcAcc.Id} to {destacc.Id} ");
            int.TryParse(Console.ReadLine(), out int amount);
            Console.WriteLine("Are you sure u want to continue? y/n");
            string choice = Console.ReadLine();
            if (choice == "y")
            {
                srcAcc.Balance -= amount;
                destacc.Balance += amount;
                collection.FundTransfer(destacc.Id, amount, srcAcc.Id);
            }
            else
            {
                return;
            }
            
        }

        public void RemoveAccountById(int id)
        {
            try
            {
                AccountInfoCollection collection = new AccountInfoCollection();
                collection.Remove(id);
                Console.WriteLine("Account removed successfully!");
            }
            catch (ArgumentException ex)
            {
                LoggingService.WriteToLog("RemoveAccountById", "BankingProcess", ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

    }
}
