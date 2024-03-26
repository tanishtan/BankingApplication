using BankingApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Entities 
{
    public class AccountInfo
    {
        private int _Id = 0;
        private string _name = "";
        private AccountType _type = AccountType.None;
        private double _balance = 0;

        public AccountInfo() { }
        public AccountInfo(int id, string name, AccountType type, double amount)
        {
            _Id = id;
            _name = name;
            _type = type;
            _balance = amount;
        }
        public int Id { get { return _Id; } set { _Id = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public AccountType Type { get { return _type; } set { _type = value; } }
        public double Balance { get { return _balance; } set { _balance = value; } }

    }
    
}
