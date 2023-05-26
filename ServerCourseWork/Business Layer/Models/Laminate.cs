using System;
using System.ComponentModel;

namespace ServerCourseWork.Business_Layer.Models
{
    [Serializable]
    class Laminate
    {
        [DisplayName("Название")]
        public string Name { get; private set; }

        [DisplayName("Производитель")]
        public string Manufacture { get; private set; }

        [DisplayName("Длина ламината")]
        public int Length { get; private set; }

        [DisplayName("Ширина ламината")]
        public int Width { get; private set; }

        [DisplayName("Цена за упаковку")]
        public int Price { get; private set; }

        [DisplayName("Количество в упаковке")]
        public int Amount { get; private set; }

        public Laminate(string name, string manufacture, int length, int width, int price, int amount)
        {
            Name = name;
            Manufacture = manufacture;
            Length = length;
            Width = width;
            Price = price;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Название ламината: {Name}\nПроизводитель ламината: {Manufacture}\nДлина ламината: {Length} мм\nШирина ламина: {Width} мм\nЦена: {Price}\nКоличество в упаковке: {Amount}\n";
        }
    }
}
