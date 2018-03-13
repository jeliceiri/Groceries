using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryList.Models
{
    public class GroceryItem
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public Store Store { get; set; }

        [Required]
        public int StoreId { get; set; }
    }
}
