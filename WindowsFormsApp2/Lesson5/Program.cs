using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    class Program
    {
        static void AddAuthor(Author author)
        { 
            using (LibraryDbEntities db = new LibraryDbEntities())
            {
                db.Author.Add(author);
                db.SaveChanges();
                Console.WriteLine("New author added:"+ author.LastName);
            }
        }
        static void GetAllAuthors()
        {
            using (LibraryDbEntities db = new LibraryDbEntities())
            {
                db.Database.Log = Console.WriteLine;
                var au = db.Author.ToList();
                foreach (var a in au)
                {
                    Console.WriteLine(a.FirstName + " " + a.LastName);
                }
            }
        }
        static void DelAllAuthors(string Name)
        {
            using (LibraryDbEntities db = new LibraryDbEntities())
            {
                var au = db.Author.FirstOrDefault( x => x.FirstName == Name);

               if (au != null)
                {
                    Console.WriteLine("Deleted "+ au.Id + " " + au.FirstName + " " + au.FirstName);
                    db.Author.Remove(au);
                    db.SaveChanges();

                }
            }
        }
        //static Author GetAuthordu(Author author)
        //{
        //    using (LibraryDbEntities db = new LibraryDbEntities())
        //    {
        //        var au = db.Author.Find(author);
        //        Console.WriteLine(au.FirstName + " " + au.LastName);
        //        return au;
        //    }
        //}
        static void Main(string[] args)
        {
            Author author = new Author
            {
                FirstName = "Isaac",
                LastName = "Azimov"
            };
            //AddAuthor(author);
            //AddAuthor(author);
            //GetAllAuthors();
            //DelAllAuthors("Isaac");
            GetAllAuthors();
        }
    }
}
