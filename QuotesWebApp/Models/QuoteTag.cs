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
        public int QuoteId { get; set; }

        public int TagId { get; set; }
        public Quote Quote { get; set; }
        public Tag Tag { get; set; }
    }
}
