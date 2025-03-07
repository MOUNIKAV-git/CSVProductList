using ReadFromCSV;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var context = new AppDbContext();
        context.Database.EnsureCreated();

        string filepath = "https://program.com/files/product.csv";

        DataTable dt = GetDataFromCSV(filepath);
        try
        {
            string categoryCode = string.Empty;
            string categoryName = string.Empty;
            string productCode = string.Empty;
            string productName = string.Empty;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    categoryCode = (string)row["CategoryCode"];
                    categoryName = (string)row["CategoryName"];
                    productCode = (string)row["ProductCode"];
                    productName = (string)row["ProductName"];
                    AddCategory(categoryCode, categoryName, context);
                    AddProduct(productCode, productName, Convert.ToInt32(categoryCode), context);
                }
            }

            else
            {
                Console.WriteLine("file Has no Data");
            }
        }
       catch(Exception ex)
       {
            Console.WriteLine(ex.Message);
        }


    }
    private static DataTable GetDataFromCSV(string filepath)
    {
        DataTable CSVdata = new DataTable();
        StreamReader sr = new StreamReader(filepath);
        string[] headers = sr.ReadLine().Split(',');
        foreach (var header in headers)
        {
            CSVdata.Columns.Add(header);
        }
        while (!sr.EndOfStream)
        {
            string[] rows = sr.ReadLine().Split(',');
            DataRow dr = CSVdata.NewRow();
            for (int i = 0; i < headers.Length; i++)
            {
                dr[i] = rows[i];
            }
            CSVdata.Rows.Add(dr);
        }
        return CSVdata;

    }
    public static void AddCategory(string categoryCode, string categoryName, AppDbContext context)
    {
        if (context.Categories.Any(c => c.Code == categoryCode))
        {
            throw new InvalidOperationException("CategoryCode already exists");
        }
        var category = new Category { Name = categoryName, Code = categoryCode, CreationDate = DateTime.UtcNow };
        context.Categories.Add(category);
        context.SaveChanges();
    }
    public static void AddProduct(string productCode, string productName, int categoryCode, AppDbContext context)
    {
        if (context.Products.Any(p => p.Code == productCode))
        {
            throw new InvalidOperationException("ProductCode already exists");

        }
        var product = new Product { Name = productName, Code = productCode, CategoryCode = categoryCode, CreationDate = DateTime.UtcNow };
        context.Products.Add(product);
        context.SaveChanges();
    }
    
}
