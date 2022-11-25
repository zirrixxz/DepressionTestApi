using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepressionTestLib.Models
{
    [Table("DepressionTestHistory")]
    public class DepressionTestHistory

    {
        [Key]
        public string UserId { get; set; }

        public string TestId { get; set; }
        public DateTime LastUpdated { get; set; }
        public int ScoreResult { get; set; }
        public string LevelResult { get; set; }
        public string Comment { get; set; }
       
    }
}
