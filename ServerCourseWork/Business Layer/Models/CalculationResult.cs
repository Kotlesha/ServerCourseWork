using System;

namespace ServerCourseWork.Business_Layer.Models
{
    class CalculationResult
    {
        public decimal Square { get; private set; }
        public int Amount { get; private set; }
        public int CountOfPackages { get; private set; }
        public int ResultPrice { get; private set; }
        public int Leftover { get; private set; }
        public decimal LeftoverSquare { get; private set; }

        public CalculationResult(decimal square, int amount, int countOfPackages, int resultPrice, int leftover, decimal leftowerSquare)
        {
            Square = square;
            Amount = amount;
            CountOfPackages = countOfPackages;
            ResultPrice = resultPrice;
            Leftover = leftover;
            LeftoverSquare = leftowerSquare;
        }

        public override string ToString()
        {
            return $"Площадь: {Square} м2\nКоличество необходимых панелей: {Amount}\nКоличество упаковок: {CountOfPackages}\n" +
                $"Цена: {ResultPrice}\nКоличество остатков: {Leftover}\nПлощадь остатков: {LeftoverSquare} м2\n";
        }
    }
}
