using System;
using BankLibary;

namespace BankApplication
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("UnitBank");
            bool alive = true;

            while (alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Open account  \t 2. Withdraw money \t 3. Add money to account");
                Console.WriteLine("4. Close account \t 5. Skip day         \t 6. Exit program");
                Console.WriteLine("7. Show account balance ");
                Console.ForegroundColor = color;

                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());

                    switch (command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            Withdraw(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            break;
                        case 7:
                            ShowBalance(bank);
                            continue;
                    }
                    bank.CalculatePercentage();
                }
                catch (Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }

        private static void OpenAccount(Bank<Account> bank)
        {
            Console.WriteLine("Input sum for opening account");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Choose account type: 1 Ordinary 2. Deposit");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
            {
                accountType = AccountType.Deposit;
            }
            else
            {
                accountType = AccountType.Ordinary;
            }

            bank.Open(accountType,
                      sum,
                      AddSumHandler,
                      WithdrawSumHandler,
                      (o, e) => Console.WriteLine(e.Message),
                      CloseAccountHandler,
                      OpenAccountHandler);
        }

        private static void Withdraw(Bank<Account> bank)
        {
            Console.WriteLine("Input sum for withdraw");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Input account id");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }

        private static void Put(Bank<Account> bank)
        {
            Console.WriteLine("Input sum for put :");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Input account id");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }

        private static void ShowBalance(Bank<Account> bank)
        {
            Console.WriteLine("Input account id");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Account balance is : " + bank.ShowBalance(id));
        }

        private static void CloseAccount(Bank<Account> bank)
        {
            Console.WriteLine("Input id account which you want to close");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }

        private static void OpenAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void AddSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static void WithdrawSumHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
            {
                Console.WriteLine("Let is go spend money");
            }
        }

        private static void CloseAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
