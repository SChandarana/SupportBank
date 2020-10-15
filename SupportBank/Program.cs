using System;
using System.Linq;
using Microsoft.VisualBasic;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new HelperFunctions();
            helper.ParseFileAndPopulateDictionary();
            while (true)
            {
                Console.WriteLine("Welcome to the Support Bank. Type 'List All' to see all balances or 'List [Account]' to see the transactions of a particular account. Type 'exit' to exit ");
                var commands = Console.ReadLine().Split(" ");
                if (commands[0].ToLower() == "exit")
                {
                    break;
                }

                if (commands[0].ToLower() == "list" && commands.Length > 1)
                {
                    if (commands[1].ToLower() == "all")
                    {
                        helper.ListAll();
                    }
                    else
                    {
                        helper.ListAccount(String.Join(" ", commands.Skip(1)));
                    }
                }

                else
                {
                    Console.WriteLine("Input not recognized");
                }
            }
        }
    }
}
