using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFromCSV
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
