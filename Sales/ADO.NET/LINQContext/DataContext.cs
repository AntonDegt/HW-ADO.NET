using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace ADO.NET.LinqContext
{
    public class DataContext
    {
        public List<Entities.Department> Departments { get; set; }
        public List<Entities.Product> Products { get; set; }
        public List<Entities.Manager> Managers { get; set; }

        public DataContext(String connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Departments = new List<Entities.Department>();
            Products = new List<Entities.Product>();
            Managers = new List<Entities.Manager>();
            SqlCommand cmd = new SqlCommand("SELECT Id, Name FROM Departments", connection);
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Departments.Add(new Entities.Department()
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1)
                    });
                }
                reader.Close();

                cmd.CommandText = "SELECT Id, Name, Price FROM Products";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Products.Add(new Entities.Product()
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2)
                    });
                }
                reader.Close();

                cmd.CommandText = "SELECT Id, Surname, Name, Secname, Id_main_dep, Id_sec_dep, Id_chief FROM Managers";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Guid? a = null; if (reader[5] != DBNull.Value) reader.GetGuid(5);
                    Guid? b = null; if (reader[6] != DBNull.Value) reader.GetGuid(6);
                    Managers.Add(new Entities.Manager()
                    {
                        Id = reader.GetGuid(0),
                        Surname = reader.GetString(1),
                        Name = reader.GetString(2),
                        Secname = reader.GetString(3),
                        IdMainDep = reader.GetGuid(4),
                        IdSecDep = a,
                        IdChief = b
                    });
                }
                reader.Close();
            }
        }
    }
}