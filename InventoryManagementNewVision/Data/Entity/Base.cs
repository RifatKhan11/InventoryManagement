﻿namespace InventoryManagementNewVision.Data.Entity
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime? updatedAt { get; set; }
        public string updatedBy { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public bool isActive { get; set; }=true;
    }
}
