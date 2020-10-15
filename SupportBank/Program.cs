using System;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new LoggingConfiguration();
            var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            LogManager.Configuration = config;
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
