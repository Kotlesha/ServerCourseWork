namespace ServerCourseWork.Business_Layer.Models
{
    class CalculationData
    {
        public Laminate Laminate { get; private set; }
        public decimal LengthRoom { get; private set; }
        public decimal WidthRoom { get; private set; }
        public int Indent { get; private set; }
        public int MinLength { get; private set; }

        public CalculationData(Laminate laminate, decimal lengthRoom, decimal widthRoom, int indent, int minLength)
        {
            Laminate = laminate;
            LengthRoom = lengthRoom;
            WidthRoom = widthRoom;
            Indent = indent;
            MinLength = minLength;
        }

        public override string ToString()
        {
            return $"Данные ламината:\n{Laminate}\nДлина комнаты:{LengthRoom} м\nШирина комнаты: {WidthRoom} м\nМинимальная длина остатка: {MinLength} мм\nОтсуп от стен: {Indent} мм\n";
        }
    }
}
