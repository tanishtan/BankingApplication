using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.DataAccess
{
    public class AccountInfoCollection
    {
        static AccountInfo[] items = new AccountInfo[1];
        public AccountInfo[] ?Accounts;

        public void AddNew(AccountInfo item)
        {
            var nextId = ((items[0] is null) ? items.Length : items.Length + 1) + 100;
            item.Id = nextId;
            //Resize the array with plus item from the second time onwards 
            if (items[0] is null)
                items[0] = item;
            else
            {
                //Append an item to the array
                var length = items.Length;
                var newArray = new AccountInfo[length + 1];
                Array.Copy(items, 0, newArray, 0, length);
                newArray[length] = item;
                items = newArray;
            }
        }
        public AccountInfo FindAccountBy(int accountId)
        {
            AccountInfo acc = null;
            if (items.Length == 1 && items[0] is null)
            {
                return acc;
            }
            foreach (var item in items)
            {
                if (item.Id == accountId) { acc = item; break; }
            }
            return acc;
        }

        public object FindAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public AccountInfo[] GetAllAccounts()
        {
            return items;
        }
        public void Remove(int id)
        {/*
            List<AccountInfo> accounts = ReadAccounts();
            int removedCount = accounts.RemoveAll(a => a.Id == id);
            if (removedCount == 0)
            {
                throw new ArgumentException($"Account with ID {id} not found");
            }
            WriteAccounts(accounts);
        }*/
        }
    }
}
