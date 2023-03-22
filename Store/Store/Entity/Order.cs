using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Entity
{
    public class Order
    {
        public static int TempIdForNew;

        public int Id;
        public Guid? User_id;

        public int product_id;

        public int Count;
        public string Message;
    }
}
