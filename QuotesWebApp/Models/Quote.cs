using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuotesWebApp.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Quote's text")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Date of creation")]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public ICollection<QuoteTag> Tags { get; set; }
    }
}
