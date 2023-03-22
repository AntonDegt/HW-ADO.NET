using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Entity
{
    public class Product
    {
        public static int TempIdForNew;

        public int Id { get; set; }
        public string Name { get; set; }

        public int category_id { get; set; }
        public string Category 
        {
            get 
            {
                Category category = null;
                if (Data.Categories.Count > 0)
                    category = Data.Categories.Where(x => x.Id == category_id).First();

                if (category == null)
                    return "";
                else
                    return category.Name;
            }
            set
            {
                category_id = Data.Categories.Where(x => x.Name == value).First().Id;
            }
        }

        public int producer_id { get; set; }
        public string Producer
        {
            get
            {
                Producer producer = null;
                if (Data.Producers.Count > 0)
                    producer = Data.Producers.Where(x => x.Id == producer_id).First();

                if (producer == null)
                    return "";
                else
                    return producer.Name;
            }
            set
            {
                category_id = Data.Producers.Where(x => x.Name == value).First().Id;
            }
        }

        public float Price { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
