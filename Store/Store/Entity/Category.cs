using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Entity
{
    public class Category
    {
        public static int TempIdForNew;

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
