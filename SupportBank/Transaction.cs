using System;

namespace SupportBank
{
    class Transaction
    {
        public DateTime Date { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public string Narrative { get; private set; }
        public decimal Amount { get; private set; }

        public Transaction(DateTime date, string from, string to, string narrative, decimal amount)
        {
            Date = date;
            From = from;
            To = to;
            Narrative = narrative;
            Amount = amount;
        }
    }
}
