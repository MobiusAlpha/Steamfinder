using System;

namespace Steamfinder.Common
{
    public class Currency
    {
        public int Platinum { get; }
        public int Gold { get; }
        public int Silver { get; }
        public int Copper { get; }

        public Currency(int platinum, int gold, int silver, int copper)
        {
            Platinum = platinum;
            Gold = gold;
            Silver = silver;
            Copper = copper;
        }

        public static Currency operator +(Currency c1, Currency c2)
        {
            int copper = c1.Copper + c2.Copper;
            int silver = 0;
            int gold  = 0;
            int platninum = 0;

            int increment = copper / 10;
            
            if (increment > 1)
            {
                silver = increment;
                copper -= increment * 10;
            }

            silver += c1.Silver + c2.Silver;

            increment = silver / 10;

            if (increment > 1)
            {
                gold = increment;
                silver -= increment * 10;
            }

            gold += c1.Gold + c2.Gold;

            increment = gold / 10;

            if (increment > 1)
            {
                platninum = increment;
                gold -= increment * 10;
            }

            platninum += c1.Platinum + c2.Platinum;

            return new Currency(platninum, gold, silver, copper);
        }

        public static Currency operator *(Currency c1, double mult)
        {
            double result = c1.AsGold() * mult;

            return Currency.FromGold(result);
        }

        public static Currency operator *(Currency c1, int mult)
        {
            double result = c1.AsGold() * mult;

            return Currency.FromGold(result);
        }

        public double AsGold()
        {
            return Platinum * 10.0 + Gold * 1.0 + Silver * 0.1 + Copper * 0.01;
        }

        public static Currency FromGold(double gpVal)
        {
            int copper = 0;
            int silver = 0;
            int gold = 0;
            int platninum = 0;

            platninum = (int) Math.Floor(gpVal/10);
            gold = (int) Math.Floor(gpVal - platninum * 10);
            silver = (int) Math.Floor((gpVal - gold * 1.0 - platninum * 10.0)*10);
            copper = (int) Math.Floor((gpVal - silver/10.0 - gold*1.0 - platninum*10.0)*100);

            return new Currency(platninum, gold, silver, copper);
        }

        public Currency Rebalance()
        {
            return this + new Currency(0, 0, 0, 0);
        }

        public override string ToString()
        {
            return $"{Platinum}pp, {Gold}gp, {Silver}sp, {Copper}cp";
        }
    }
}