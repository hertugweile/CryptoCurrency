using System;
using System.Collections.Generic;

namespace CryptoCurrency
{
    public class Converter
    {
        public Dictionary<String, double> currencies = new Dictionary<String, double>();

        /// <summary>
        /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
        /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
        /// bliver den gamle værdi overskrevet af den nye værdi
        /// </summary>
        /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
        /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
        public void SetPricePerUnit(String currencyName, double price)
        {
            if (price < 0)
            {
                throw new InvalidOperationException("Prisen må ikke være mindre end nul");
            } else {
                if (currencies.ContainsKey(currencyName)) {
                    currencies[currencyName] = price;
                } else {
                    currencies.Add(currencyName, price);
                }
            }
        }

        /// <summary>
        /// Konverterer fra en kryptovaluta til en anden. 
        /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
        /// 
        /// </summary>
        /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
        /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
        /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
        /// <returns>Værdien af beløbet i toCurrencyName</returns>
        public double Convert(String fromCurrencyName, String toCurrencyName, double amount) 
        {
            // BTC -> 10000;
            // Litecoin -> 2500;

            double value = 0;

            try
            {
                value = currencies[fromCurrencyName] / currencies[toCurrencyName];
            } catch (Exception ex)
            {
                throw new InvalidOperationException("Der er fejl i en af parametrene");
            }
            return value * amount;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var cur = new Converter();
            cur.SetPricePerUnit("BTC", -10000);
            cur.SetPricePerUnit("LTE", 2500);
            Console.WriteLine(cur.Convert("BTC", "LTE", 1)); // <--- Skal være 4
        }
    }



}
