using Banking.DataAccess;
using BankingApp.Common;
using BankingApp.Entities;
using BankingApp.Process;
using Microsoft.VisualBasic;
using System.Security.AccessControl;
using System.Text;
using System.Xml.Linq;

namespace BankingApp
{
    internal class Program
    {
        static BankingProcess process = new BankingProcess();

        static int DisplayMenuAndGetChoice()
        {
            Console.WriteLine("************** Banking System ***********");
            Console.WriteLine("******* 1. Create a New Account ");
            Console.WriteLine("******* 2. List All the Accounts");
            Console.WriteLine("******* 3. Find account by ID");
            Console.WriteLine("******* 4. Update Account Details");
            Console.WriteLine("******* 5. Remove an account");
            Console.WriteLine("******* 6. Perform Transaction");
            Console.WriteLine("******* ");
            Console.WriteLine("******* 0. Quit ");
            Console.WriteLine("***********************************");
            Console.Write("\nEnter Choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice > 0 && choice < 9) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;
        }

        static void UpdateAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("***********Update Account Details");

            Console.WriteLine("Enter you accountId ");
            int id = int.Parse(Console.ReadLine());
            var pro = process.findAccountById(id);
            if (pro is null)
            {
                Console.WriteLine("Account not found");
            }
            else
            {

                Console.Write($"Id {pro.Id}: ");
                string ids = Console.ReadLine();


                Console.Write($"Name {pro.Name}: ");
                string name = Console.ReadLine();

                Console.Write($"Balance {pro.Balance}: ");
                string balance = Console.ReadLine();


                var x = process.UpdateAccountDetails(pro, ids, name, balance);
                PrintAccountDetails(x);
            }
            Console.ReadKey();
        }

        static void PerformTransaction()
        {
            Console.Clear();
            Console.WriteLine("***********Transactions");
            Console.WriteLine("Enter your account id");
            int.TryParse(Console.ReadLine(), out int accId);
            var exist = process.findAccountById(accId);
            if (exist is null)
            {
                Console.WriteLine("Account not found");
                return;
            }
            else
            {
                Console.WriteLine("*****1. Deposit");
                Console.WriteLine("*****2. Withdraw");
                Console.WriteLine("*****3. Fund Transfer");
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice == 1 || choice == 2)
                {
                    if (choice == 1)
                    {
                        Console.WriteLine($"Enter the amount to Deposit ");
                        Console.WriteLine("You have an Amount of rs. " + exist.Balance);
                        int.TryParse(Console.ReadLine(), out int balance);
                        process.AccountTransaction(exist, balance, choice);
                        Console.Clear();
                        Console.WriteLine("Your account balance is");
                        PrintAccountDetails(exist);
                        Console.ReadKey();
                    }
                    else if (choice == 2)
                    {
                        Console.WriteLine($"Enter the amount to Withdraw ");
                        Console.WriteLine("You have an Amount of rs. " + exist.Balance);
                        int.TryParse(Console.ReadLine(), out int balance);
                        process.AccountTransaction(exist, balance, choice);
                        Console.Clear();
                        Console.WriteLine("Your account balance is");
                        PrintAccountDetails(exist);
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.Write("Enter the account to transfer: ");
                    int.TryParse(Console.ReadLine(), out int destacc);
                    var y = process.findAccountById(destacc);
                    if (y is null)
                    {
                        Console.WriteLine("Account not found");
                        return;
                    }
                    else
                    {
                        process.fundTrans(exist, y);
                        Console.Clear();
                        Console.WriteLine("Source account is");
                        PrintAccountDetails(exist);
                        Console.WriteLine("Destination account is");
                        PrintAccountDetails(y);
                        Console.ReadKey();
                    }

                }
            }
        }

        static void PrintAccountDetails(AccountInfo info)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Account Details:\n\tId: {info.Id}")
                .AppendLine($"\tName: {info.Name}")
                .AppendLine($"\tType: {info.Type}")
                .AppendLine($"\tBalance: {info.Balance}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(builder.ToString());
            Console.ResetColor();
        }
        static void AddNewAccount()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations :: Add New Account ****** ");
            Console.Write("\nAccount holder Name: ");
            string name = Console.ReadLine();
            Console.Write("Account Type [1-Savings, 2-Current] : ");
            int accType = int.Parse(Console.ReadLine());
            Console.Write("Initial Amount: ");
            double amount = double.Parse(Console.ReadLine());

            try
            {
                BankingProcess process = new BankingProcess();
                var info = process.CreateNewAccount(0, name, (AccountType)accType, amount);
                //process.CreateNewAccount(0, name, (AccountType)accType, amount);
                if (info is not null)
                {
                    PrintAccountDetails(info);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAccount created successfully.");
                    Console.ResetColor();

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nAccount could not be created. Try again with different values.");
                    Console.ResetColor();
                }
            }catch(NullReferenceException me)
            {
                LoggingService.WriteToLog("AddnewAccount", "Main", me.Message);
                Console.WriteLine("Insufficient balance");
            }
        }

        static void ListAllAccounts()
        {
            Console.Clear();
            Console.WriteLine("************ Banking Operations :: Add New Account ****** ");
            BankingProcess process = new BankingProcess();
            var acc = process.GetAllAccounts();
            foreach (var account in acc)
            {
                PrintAccountDetails(account);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ResetColor();
            }
        }

        static void RemoveAccount()
        {
            Console.Clear();
            Console.WriteLine("*********Remove account");
            Console.WriteLine("Enter the account to be removed");
            int.TryParse(Console.ReadLine(), out int remove);
            process.RemoveAccountById(remove);
            Console.WriteLine();
            Console.WriteLine("Account removed");
            Console.ReadKey();
        }

        static void FindAccountByID()
        {
            Console.WriteLine("Enter the account ID");
            int accountID = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("************ Banking Operations :: Add New Account ****** ");
            BankingProcess findId = new BankingProcess();
            var x = findId.findAccountById(accountID);
            if (x != null)
            {
                PrintAccountDetails(x);
            }
            else
            {
                Console.WriteLine("Account does not exist");
            }
        }
        static void Main(string[] args)
        {

            int choice = 0;
            do
            {
                Console.Clear();
                choice = DisplayMenuAndGetChoice();
                if (choice == 1)
                {
                    AddNewAccount();
                }
                else if (choice == 2)
                {
                    ListAllAccounts();
                }
                else if (choice == 3)
                {
                    FindAccountByID();
                }
                else if (choice == 4)
                {
                    UpdateAccountDetails();
                }
                /*else if (choice == 5)
                {
                   RemoveAccount();
                }*/
                else if (choice == 6)
                {
                    PerformTransaction();
                }
                Console.WriteLine("Press a key to continue...");
                Console.ReadKey();
            } while (choice != 0);

            /*Account acc = new Account(11);
            acc.Withdraw();*/

            //Account acc = new Account(101, "Sample1", Common.AccountType.Savings, 10000);
            //IAccount acc = AccountFactory.Create(101, "Sample1", Common.AccountType.Savings, 20000);
            //acc.info = new Entities.AccountInfo(1, "", Common.AccountType.None, 123); 

            /*IAccount acc=null, acc2=null;
            try
            {
                acc = AccountFactory.Create(101, "Sample1", AccountType.Savings, 20000);
            }
            catch(MinimumBalanceException mbe)
            {
                Console.WriteLine(mbe.Message);
            }

            if (acc is null)
            {
                Console.WriteLine("One or more objects could not be created.");
                return; //terminate the execution, in this case. 
            }
            acc.DepositEvent += (sender, e) => Console.WriteLine(e);
            acc.WithdrawEvent += (sender, e) => Console.WriteLine(e);
            Console.WriteLine($"New account:\n{acc}");
            acc.Deposit(10000);
            Console.WriteLine($"After deposit:\n{acc}");
            try
            {
                acc.Withdraw(5432);
                Console.WriteLine($"After withdraw:\n{acc}");
            }
            catch (MinimumBalanceException mbe)
            {
                Console.WriteLine(mbe.Message);
            }
            
            

            //create another account 
            //Account acc2 = new Account(102, "Sample2", Common.AccountType.Current, 25000);
            //IAccount acc2 = AccountFactory.Create(102, "Sample2", Common.AccountType.Current, 200000);

            try
            {
                acc2 = AccountFactory.Create(102, "Sample2", AccountType.Current, 25000);
            }
            catch (MinimumBalanceException mbe)
            {
                Console.WriteLine(mbe.Message);
            }
            if (acc2 is null)
            {
                Console.WriteLine("One or more objects could not be created.");
                return; //terminate the execution, in this case. 
            }
            acc2.DepositEvent += (sender, e) => Console.WriteLine(e);
            try
            {
                acc2.WithdrawEvent += (sender, e) => Console.WriteLine(e);
                Console.WriteLine($"Another account:\n{acc2}");
            }
            catch (MinimumBalanceException mbe)
            {
                Console.WriteLine(mbe.Message);
            }
            try
            {
                acc.FundTransfer(acc2, 5432);
                Console.WriteLine($"After fund transfer:\nSource:{acc}\nDestination:{acc2}");
            }
            catch (MinimumBalanceException mbe)
            {
                Console.WriteLine(mbe.Message);
            }
            //acc = AccountFactory.Create(101, "", Common.AccountType.None, 0);
            Console.WriteLine($"\n\nFrom the Array: {acc}");


            Console.WriteLine("Press a key to terminate.");
            Console.ReadKey();
        }

        private static void Acc_DepositEvent(object sender, TransactionEventArgs e)
        {
            throw new NotImplementedException();
        }*/
        }
    }
}