using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesWebApp.Data
{
    public class QuoteTag
    {
        [Key]
        public int QuoteId { get; set; }

        [Key]
        public IEnumerable<int> TagIds { get; set; }

        [ForeignKey("QuoteId")]
        public Quote Quote { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
