﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Entities
{
   public class ProviderIngredient
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
        public int Quantity { get; set; }
        public  double Price { get; set; }

    }
}
