using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogDBContext
{
    [Table("RMLogging")]
    public class RMLoggingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Request { get; set; }

        [Required]
        [MaxLength(45)]
        public string IpAddress { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Result { get; set; }
    }
}
