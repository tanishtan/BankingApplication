using BankingApp.Common;
using BankingApp.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.DataAccess
{
    public class AccountInfoFileCollection
    {
        private static readonly string _filename;

        static AccountInfoFileCollection()
        {
            // Use Path.Combine for correct path construction
            _filename = Path.Combine(Environment.CurrentDirectory, "Accounts.txt"); // Use default filename
            File.Create(_filename).Close();
            Console.WriteLine($"File created at: {_filename}");
        }

        

        private List<AccountInfo> ReadAccounts()
        {
            List<AccountInfo> accounts = new List<AccountInfo>();
            using (StreamReader reader = new StreamReader(_filename))
            {
                // Skip the header line
                reader.ReadLine();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split('|');
                    accounts.Add(new AccountInfo(int.Parse(data[0]), data[1], Enum.Parse<AccountType>(data[2], true), double.Parse(data[3])));
                }
            }
            return accounts;
        }

        private void WriteAccounts(List<AccountInfo> accounts)
        {
            using (StreamWriter writer = new StreamWriter(_filename, false))
            {
                // Write the header line
                if (writer.BaseStream.Length == 0)
                {
                    writer.WriteLine("ID|Name|AccountType|Balance");
                }
                foreach (AccountInfo account in accounts)
                {
                    writer.WriteLine($"{account.Id}|{account.Name}|{account.Type}|{account.Balance}");
                }
            }
        }

        public void AddNew(AccountInfo accountInfo)
        {
            List<AccountInfo> accounts = ReadAccounts();
            int nextId = 1;
            if (accounts.Any())
            {
                nextId = accounts.Max(a => a.Id) + 1;
            }
            accountInfo.Id = nextId;
            accounts.Add(accountInfo);
            WriteAccounts(accounts);
        }

        public AccountInfo FindAccountById(int id)
        {
            List<AccountInfo> accounts = ReadAccounts();
            return accounts.FirstOrDefault(a => a.Id == id);
        }

        public List<AccountInfo> GetAllAccounts()
        {
            return ReadAccounts();
        }

        public void Update(AccountInfo accountInfo)
        {
            List<AccountInfo> accounts = ReadAccounts();
            bool found = false;
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Id == accountInfo.Id)
                {
                    accounts[i] = accountInfo;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                throw new ArgumentException($"Account with ID {accountInfo.Id} not found");
            }
            WriteAccounts(accounts);
        }

        public void Remove(int id)
        {
            List<AccountInfo> accounts = ReadAccounts();
            int removedCount = accounts.RemoveAll(a => a.Id == id);
            if (removedCount == 0)
            {
                throw new ArgumentException($"Account with ID {id} not found");
            }
            WriteAccounts(accounts);
        }
    }
}
