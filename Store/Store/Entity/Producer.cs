using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Entity
{
    public class Producer
    {
        public static int TempIdForNew;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
