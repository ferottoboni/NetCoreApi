using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace junto_test_api.Entity
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }

        private string Encrypt(string clearText)
        {
            return EntityHelper.Encrypt(clearText);
        }
    }
}
