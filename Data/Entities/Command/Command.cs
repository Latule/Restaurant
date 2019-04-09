using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
   public class Command
    {
        [Key]
        public Guid Id { get; set; }
  
        [Required]
        [Range(0,double.MaxValue)]
        public  double Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual List<CommandMenus> CommandMenus { get; set; }


    }
}
