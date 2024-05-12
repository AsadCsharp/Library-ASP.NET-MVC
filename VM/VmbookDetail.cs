using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.VM
{
    public class VmBookDetail 
    {
        public int BookDetailId { get; set; }
        public string AuthorName { get; set; }
        public string Price { get; set; }
        public int BookId { get; set; }
    }
}