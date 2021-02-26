/*
 
The Product Class
 
 */

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        public string Author { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
       

        [Required]
        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
        
    }
}