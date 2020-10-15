using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace SupportBank
{
    class HelperFunctions
    {
        public Dictionary<string, Account> Accounts { get; private set; } = new Dictionary<string, Account>();

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

        public void ParseFileAndPopulateDictionary()
        {
            using var reader = new StreamReader("./Transactions2014.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
            var records = csv.GetRecords<Transaction>();
            foreach (var transaction in records)
            {
                AddTransaction(transaction);
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
