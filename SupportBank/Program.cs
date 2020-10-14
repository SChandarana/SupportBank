using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace SupportBank
{
    class Program
    {
        private static Dictionary<string,Account> _accounts;
        
        static void Main(string[] args)
        {
            _accounts = new Dictionary<string, Account>();
            CSVReader();
            foreach (var account in _accounts.Values)
            {
                Console.WriteLine($"Name: {account.Name} -- Pending Account Balance: {account.BalanceDue}");
            }
        }

        static void CSVReader()
        {
            using (var reader = new StreamReader("./Transactions2014.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Transaction>();
                foreach (var transaction in records)
                {
                    AddTransaction(transaction);
                }
            }
        }

        static void AddTransaction(Transaction transaction)
        {
            var toName = transaction.To;
            var fromName = transaction.From;
            _accounts.TryAdd(toName, new Account(toName));
            _accounts.TryAdd(fromName, new Account(fromName));
            _accounts[toName].AddTransaction(transaction);
            _accounts[fromName].AddTransaction(transaction);
        }
    }
}
