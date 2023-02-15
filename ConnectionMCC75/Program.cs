using ConnectionMCC75.Command;
using ConnectionMCC75.Models;

namespace ConnectionMCC75;

public class Program
{
    public static void Main(string[] args)
    {
        Region region = new Region();
        region.Name = "cek 124";
        region.Id = 1;

        RegionCommands regionCommands = new RegionCommands();
        //int result = commands.Insert(region);
        //if (result > 0)
        //{
        //    Console.WriteLine("Data Berhasil Disimpan");
        //}
        //else
        //{
        //    Console.WriteLine("Data Gagal Disimpan");
        //}

        //var results = commands.GetAll();
        //if (results == null)
        //{
        //    Console.WriteLine("Data tidak ditemukan");
        //}
        //else
        //{
        //    foreach (var item in results)
        //    {
        //        Console.WriteLine("Id : " + item.Id);
        //        Console.WriteLine("Name : " + item.Name);
        //    }
        //}

        CountryCommands countryCommands = new CountryCommands();

        // Method Syntax
        var resultMethod = countryCommands.GetAll()
            .Join(regionCommands.GetAll(),
            c => c.RegionId,
            r => r.Id,
            (c, r) => new
            {
                Id = c.Id,
                Name = c.Name,
                RegionName = r.Name
            });

        // Query Syntax
        var resultQuery = from c in countryCommands.GetAll()
                          join r in regionCommands.GetAll()
                          on c.RegionId equals r.Id
                          select new
                          {
                              Id = c.Id,
                              Name = c.Name,
                              RegionName = r.Name
                          };


        foreach (var item in resultQuery)
        {
            Console.WriteLine("Id : " + item.Id);
            Console.WriteLine("Name : " + item.Name);
            Console.WriteLine("Region Name : " + item.RegionName);
            Console.WriteLine();
        }
    }
}
