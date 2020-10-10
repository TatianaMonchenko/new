using LibraryCodeFirst.Mapping;
using LibraryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Authors authors = new Authors
            {
                FirstName = "Isaac",
                LastName = "Azimovvvvv"
            };
            using (LibraryContext db = new LibraryContext())
            {
                db.Authors.Add(authors);
                db.SaveChanges();
                var ac = db.Authors.ToList();
                foreach (var a in ac)
                {
                    Console.WriteLine(a.FirstName + " " + a.LastName);
                }
            }
        }
    }
}
