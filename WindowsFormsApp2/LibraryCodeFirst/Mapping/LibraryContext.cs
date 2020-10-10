using LibraryCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCodeFirst.Mapping
{
    class LibraryContext: DbContext
    {
        public LibraryContext(): base()
        {

        }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
