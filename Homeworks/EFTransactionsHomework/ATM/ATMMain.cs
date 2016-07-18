using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM
{
    class ATMMain
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the card number: ");
            var cardNumber = Console.ReadLine();
            Console.WriteLine("Enter the card PIN: ");
            var cardPin = Console.ReadLine();
            Console.WriteLine("Enter the amount of money you wish to withdraw: ");
            var money = decimal.Parse(Console.ReadLine());

            try
            {
                ATMManager.WithdrawMoney(cardNumber, cardPin, money);
                Console.WriteLine("You successfully withdrеw {0} money", money);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
