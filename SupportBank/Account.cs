using System.Collections.Generic;

namespace SupportBank
{
    class Account
    {
        public string Name { get; }
        public decimal BalanceDue { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public Account(string name)
        {
            BalanceDue = 0;
            Name = name;
            Transactions = new List<Transaction>();

        }

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