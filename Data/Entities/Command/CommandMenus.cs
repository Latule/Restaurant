using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class CommandMenus
    {
        [Key]
        public Guid Id { get; set; }

        public Guid IdMenu { get; set; }
        public virtual Menu.Menu Menu { get; set; }

        public Guid IdCommand { get; set; }
        public virtual Command Command { get; set; }
    }
}
