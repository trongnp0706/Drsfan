﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Drsfan.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public string Description { get; set; }

        public string Brand { get; set; }  

        public string ModelNumber { get; set; }  

        [Required]
        public double ListPrice { get; set; }  

        
        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Discount Price must be less than or equal to List Price.")]
        public double DiscountPrice { get; set; }


        [Required]
        [Display(Name = "Warranty Period")]
        public string WarrantyPeriod { get; set; } 

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        public string Features { get; set; }  

        public string PowerConsumption { get; set; }

        [ValidateNever]
        public List<ProductImage> ProductImages { get; set; }
    }
}
