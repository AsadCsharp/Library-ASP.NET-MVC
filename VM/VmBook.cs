using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.VM
{
    public class VmBook
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime PublishDate { get; set; }
        public string Available { get; set; }
        public string Image { get; set; }
        public string[] AuthorName { get; set; } = new string[0];
        public string[] Price { get; set; }
        public List<VmBookDetail> LBookDetails { get; set; } = new List<VmBookDetail>();
    }
}