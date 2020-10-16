using System;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace SupportBank
{
    class Program
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            LoggerSetup();
            var helper = new HelperFunctions();
            helper.ParseFileAndPopulateDictionary("Transactions2014.csv");
            helper.ParseFileAndPopulateDictionary("DodgyTransactions2015.csv");
            while (true)
            {
                Console.WriteLine("Welcome to the Support Bank. Type 'List All' to see all balances or 'List [Account]' to see the transactions of a particular account. Type 'exit' to exit ");
                var commands = Console.ReadLine().Split(" ");
                logger.Info("User input Recieved");
                if (commands[0].ToLower() == "exit")
                {
                    logger.Info("User has exited program");
                    break;
                }

                if (commands[0].ToLower() == "list" && commands.Length > 1)
                {
                    if (commands[1].ToLower() == "all")
                    {
                        logger.Info("User Requesting all balances");
                        helper.ListAll();
                    }
                    else
                    {
                        var name = String.Join(" ", commands.Skip(1));
                        logger.Info($"User Requesting account information for {name}");
                        helper.ListAccount(name);
                    }
                }

                else
                {
                    logger.Error("Unable to read command");
                }
            }
        }

        private static void LoggerSetup()
        {
            var config = new LoggingConfiguration();
            var target = new FileTarget
                {FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}"};
            config.AddTarget("File Logger", target);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
            config.LoggingRules.Add(new LoggingRule("*",LogLevel.Error, new ConsoleTarget()));
            LogManager.Configuration = config;
            logger.Info("Program and Logger initiated");
        }
    }
}
