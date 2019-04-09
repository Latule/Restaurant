using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Data.Entities;

namespace Repositories.Dtos
{
    public class CommandDto
    {
        public double Price { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public List<String> CommandMenus { get; set; }
    }
}
