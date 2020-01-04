using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace junto_test_api.Entity
{
    /// <summary>
    /// A account with users
    /// </summary>
    public class Account : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string PublicKey { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }
    }




}
