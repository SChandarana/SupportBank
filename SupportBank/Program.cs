using System;
using System.Linq;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var bs = new Base();
            bs.ParseFileAndPopulateDictionary();
            while (true)
            {
                Console.WriteLine("Welcome to the Support Bank. Type 'List All' to see all balances or 'List [Account]' to see the transactions of a particular account. Type 'exit' to exit ");
                try
                {
                    var commands = Console.ReadLine().Split(" ");
                    if (commands[0].ToLower() == "exit")
                    {
                        break;
                    }

                    if (commands[0].ToLower() == "list")
                    {
                        if (commands[1].ToLower() == "all")
                        {
                            bs.ListAll();
                        }
                        else
                        {
                            bs.ListAccount(String.Join(" ", commands.Skip(1)));
                        }
                    }

                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Input not recognized");
                }
            }
        }
    }
}
