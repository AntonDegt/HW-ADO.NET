using System;

namespace ADO.NET.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }  // !! тип FLOAT в БД соответствует типу double в C#

        public String ToShortString()
        {
            return $"{Id.ToString()[..4]}... {Name} ({Price} грн)";
        }
    }
}