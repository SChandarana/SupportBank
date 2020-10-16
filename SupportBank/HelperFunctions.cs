using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.TypeConversion;
using NLog;
using NLog.Config;

namespace SupportBank
{
    class HelperFunctions
    {
        public Dictionary<string, Account> Accounts { get; private set; } = new Dictionary<string, Account>();
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        
        public void ListAll()
        {
            foreach (var account in Accounts.Values)
            {
                Console.WriteLine($"Name: {account.Name} -- Pending Account Balance: {account.BalanceDue}");
            }
        }

        public void ListAccount(string name)
        {
            if (Accounts.TryGetValue(name, out Account account))
            {
                foreach (var transaction in account.Transactions)
                {
                    Console.WriteLine($"Date: {transaction.Date} - From: {transaction.From} - To: {transaction.To} - Narrative: {transaction.Narrative} - Amount: {transaction.Amount}");
                }
            }
            else
            {
                Console.WriteLine($"Account with name {name} does not exist");
            }
        }

        public void ParseFileAndPopulateDictionary(string filename)
        {
            logger.Info($"Reading file {filename}");
            using var reader = new StreamReader($"./{filename}");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
            var options = new TypeConverterOptions
            {
                DateTimeStyle = DateTimeStyles.None,
                Formats = new[] {"dd/MM/yyyy"},
            };
            csv.Configuration.TypeConverterOptionsCache.AddOptions<DateTime>(options);
            logger.Info("File read successfully - Beginning to add transactions");
            var records = csv.GetRecords<Transaction>();
            try
            {
                foreach (var transaction in records)
                {
                    AddTransaction(transaction);
                }
                logger.Info("Transactions added with no issues");
            }
            catch (TypeConverterException e)
            {
                var message = $"Error on row {e.ReadingContext.Row} {e.Text} is not of correct data type in {filename}";
                logger.Fatal(message);
                Console.WriteLine(message + "\nRest of file has been ignored, please fix errors and run again. Or continue using with part of data missing\n");
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            var toName = transaction.To;
            var fromName = transaction.From;
            Accounts.TryAdd(toName, new Account(toName));
            Accounts.TryAdd(fromName, new Account(fromName));
            Accounts[toName].AddTransaction(transaction);
            Accounts[fromName].AddTransaction(transaction);
        }
    }
}
