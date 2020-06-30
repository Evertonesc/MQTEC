using System.ComponentModel.DataAnnotations;

namespace MQTEC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Login { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get; set; }
    }
}
