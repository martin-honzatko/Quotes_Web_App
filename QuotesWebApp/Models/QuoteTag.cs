using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesWebApp.Models
{
    public class QuoteTag
    {
        [Key]
        public int Id { get; set; }

        public int QuoteId { get; set; }

        public int TagId { get; set; }

        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
