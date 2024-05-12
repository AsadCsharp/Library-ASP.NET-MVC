using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    [Table("BookDetail")]
    public class BookDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookDetailId { get; set; }
        public string AuthorName { get; set; }
        public string Price { get; set; }
        [ForeignKey("BookMaster")]
        public int BookId { get; set; }
        public BookMaster BookMaster { get; set; }

    }
}