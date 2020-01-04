using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace junto_test_api.Entity
{
    public class Token : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Key { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
