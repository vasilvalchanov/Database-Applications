using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    using System.Data;

    public static class ATMManager
    {
        public static void WithdrawMoney(string cardNumber, string cardPin, decimal money)
        {
             var context = new ATMEntities();

            using (var dbContextTransaction = context.Database.BeginTransaction(IsolationLevel.RepeatableRead))
            {
                try
                {
                    if (cardNumber.Length != 10 || cardPin.Length != 4)
                    {
                        throw new InvalidOperationException("Invalid length of CardNumber or CardPin");
                    }

                    var accountCard =
                        context.CardAccounts.FirstOrDefault(ac => ac.CardNumber == cardNumber);

                    if (accountCard == null)
                    {
                        throw new ArgumentNullException("There isn't account with such card number in the database.");
                    }

                    if (accountCard.CardPIN != cardPin)
                    {
                        throw new ArgumentException("The entered PIN number is wrong");
                    }

                    if (accountCard.CardCash < money)
                    {
                        throw new InvalidOperationException("You are trying to withdraw more than available money.");
                    }

                    accountCard.CardCash -= money;
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                    FillNewDataInTransactionHistory(context, accountCard.CardNumber, money);

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        private static void FillNewDataInTransactionHistory(ATMEntities context, string cardNumber, decimal money)
        {
            context.TransactionHistories.Add(
                new TransactionHistory() { CardNumber = cardNumber, TransactionDate = DateTime.Now, Amount = money });
            context.SaveChanges();
        }
    }
}
