using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApp.Common
{
    public class MinimumBalanceException : System.Exception
    {
        public MinimumBalanceException() { }
        public MinimumBalanceException(string message) : base(message) { }
        public MinimumBalanceException(string message, Exception innerException) : base(message, innerException) { }
    }
}
