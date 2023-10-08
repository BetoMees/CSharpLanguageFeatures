using CSharpLanguageFeatures.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CSharpLanguageFeatures.PatternMatching
{
    [Show]
    internal class PatternChallenge
    {

        // The rules:
        // A Stock trade of 0 shares should be caught and flagged as invalid
        // A Stock trade that is less than $5,000 is a 0.1% commission
        // A Stock trade that is more or equal to $5,000 is a 0.05% commission
        // Any stock trade of 1,000 shares or more is a flat fee of $10
        // Any Stock trade of 1,000 shares or more with a value of $10,000 or more is a flat fee of $5
        // A Bond trade of 5 years duration is $20, or $15 if the total trade value is $10,000 or more
        // Any bond trade of 10 years duration is $12
        // A Bond trade of 20 years duration is $10, or $5 if the total value is $5,000 or more
        // A Bond trade of any other duration should be caught and flagged as invalid

        public static void Main()
        {
            SecuritiesTrade[] tradeList = new SecuritiesTrade[] {
                new StockTrade(){Symbol="ABCD", Quantity=1200, Price=27.81m},
                new StockTrade(){Symbol="WXYZ", Quantity=1000, Price=7.92m},
                new StockTrade(){Symbol="ABCD", Quantity=200, Price=27.81m},
                new StockTrade(){Symbol="WXYZ", Quantity=400, Price=7.92m},
                new StockTrade(){Symbol="WXYZ", Quantity=0, Price=9.55m},
                new BondTrade(){Name="Abcd 5yr", Duration=5, Price=100.0m, Quantity=10},
                new BondTrade(){Name="Qwert 10yr", Duration=10, Price=100.0m, Quantity=10},
                new BondTrade(){Name="Abcd 20yr", Duration=20, Price=100.0m, Quantity=100},
                new BondTrade(){Name="Qwert 20yr", Duration=20, Price=50.0m, Quantity=10},
                new BondTrade(){Name="Qwert 50yr", Duration=50, Price=50.0m, Quantity=10},
            };

            foreach (var tradeItem in tradeList)
            {
                try
                {
                    PrintTradeCommission(tradeItem);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            }
        }


        #region CommisionCalculator

        static decimal CalculateTradeCommission(SecuritiesTrade trade) => trade switch
        {
            StockTrade stock => stock switch
            {
                var s when s.Quantity <= 0 => throw new ArgumentException("Invalid trade, can't be 0 shares"),
                var s when s.Quantity >= 1000 && (s.Quantity * s.Price) >= 10000 => 5m,
                var s when s.Quantity >= 1000 => 10m,
                var s when (s.Quantity * s.Price) < 5000 => (s.Quantity * s.Price) * 0.01m,
                var s when (s.Quantity * s.Price) >= 5000 => (s.Quantity * s.Price) * 0.005m,
                _ => throw new ArgumentOutOfRangeException(nameof(stock), $"Unexpected stock given.")
            },
            BondTrade bond => bond switch
            {
                var b when b.Duration == 5 && b.Quantity * b.Price >= 10000 => 15m,
                var b when b.Duration == 5 => 20m,
                var b when b.Duration == 10 => 12m,
                var b when b.Duration == 20 && b.Quantity * b.Price >= 5000 => 5m,
                var b when b.Duration == 20 => 10m,
                _ => throw new ArgumentOutOfRangeException(nameof(bond), $"Unexpected bond given.")
            },
            _ => throw new ArgumentOutOfRangeException(nameof(trade), $"Unexpected trade type.")
        };

        static void PrintTradeCommission(SecuritiesTrade trade)
        {
            decimal commission = CalculateTradeCommission(trade);
            if (trade is StockTrade st)
            {
                Console.WriteLine($"Stock trade of {st.Quantity} of {st.Symbol} is {commission:C}");
            }
            else if (trade is BondTrade bt)
            {
                Console.WriteLine($"Bond trade of {bt.Quantity} of {bt.Name} is {commission:C}");
            }
        }
        #endregion

        #region Security

        class SecuritiesTrade
        {
            public int Quantity;
            public decimal Price;
        }

        class StockTrade : SecuritiesTrade
        {
            public string? Symbol;
        }

        class BondTrade : SecuritiesTrade
        {
            public string? Name;
            public int Duration;
        }
        #endregion
    }
}
