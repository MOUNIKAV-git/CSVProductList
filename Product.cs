using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFromCSV
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int CategoryCode { get; set; }

        public DateTime CreationDate { get; set; }

        [ForeignKey("CategoryCode")]
        public Category Category { get; set; }

    }
}
