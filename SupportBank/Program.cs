using System;
using System.Collections.Generic;
using System.Transactions;

namespace SupportBank
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class Transaction
    {
        public Transaction(string date, string from, string to, string narrative, double amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }

        public string Date { get;  }
        public string From { get; }
        public string To { get; }
        public string Narrative { get; }
        public double Amount { get; }
    }

    class Account
    {
        public Account(string name)
        {
            BalanceDue = 0.0;
            Name = name;
            Transactions = new List<Transaction>();

        }
        
        public string Name { get; }
        public double BalanceDue { get; set; }
        public List<Transaction> Transactions { get; set; };

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            if (transaction.To.Equals(Name))
            {
                BalanceDue += transaction.Amount;
            }

            if (transaction.From.Equals(Name))
            {
                BalanceDue -= transaction.Amount;
            }
        }


    }
}
