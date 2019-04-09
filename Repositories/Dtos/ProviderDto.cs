using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Repositories.Dtos
{
    public class ProviderDto
    {
    
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<IngredientDto> Ingredients { get; set; }

    }
}
