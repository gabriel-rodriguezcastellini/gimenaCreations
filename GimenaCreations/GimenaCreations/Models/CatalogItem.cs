﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GimenaCreations.Models
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [Display(Name = "Picture")]
        public string PictureFileName { get; set; }

        public int CatalogTypeId { get; set; }

        [Display(Name = "Catalog type")]
        public CatalogType CatalogType { get; set; }

        [Display(Name = "Available stock")]
        public int AvailableStock { get; set; }

        [Display(Name = "Restock threshold")]
        public int RestockThreshold { get; set; }

        [Display(Name = "Max stock threshold")]
        public int MaxStockThreshold { get; set; }

        [Display(Name = "On reorder")]
        public bool OnReorder { get; set; }

        public ICollection<OrderItem> Items { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }

        [NotMapped, Display(Name = "Picture")]
        public IFormFile FormFile { get; set; }
    }
}
