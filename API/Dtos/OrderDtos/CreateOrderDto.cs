﻿using System.ComponentModel.DataAnnotations;

namespace API.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        [Required]
        public string CartId { get; set; } = string.Empty;
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public required string ContactEmail { get; set; }
        [Required]
        public required string ContactName { get; set; }
        [Required]
        public string? Adress { get; set; }
    }
}
