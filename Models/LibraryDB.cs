using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class LibraryDB:DbContext
    {
        public DbSet<BookMaster> BookMasters { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
    }
}