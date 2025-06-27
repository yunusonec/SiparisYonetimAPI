﻿namespace SiparisYonetimAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
