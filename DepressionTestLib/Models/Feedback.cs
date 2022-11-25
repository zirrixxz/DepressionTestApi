using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Models
{
    [Table("Feedback")]
    public class Feedback

    {
        [Key]
        public string FeedbackId { get; set; }

        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime LastUpdated { get; set; }
        

    }
}
