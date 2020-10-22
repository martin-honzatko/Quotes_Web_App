using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuotesWebApp.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tag's name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Tag's category")]
        public Category Category { get; set; }

        [JsonIgnore]
        public ICollection<QuoteTag> Quotes { get; set; }
    }
}
