using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Models
{
    [Table("BookMaster")]
    public class BookMaster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        public string BookName { get; set; }
        [Column(TypeName = "date")]
        public DateTime PublishDate { get; set; }
        public string Available { get; set; }
        public string Image { get; set; }
        public virtual List<BookDetail> BookDetails { get; set; }


    }
}