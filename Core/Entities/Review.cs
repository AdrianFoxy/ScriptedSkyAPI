﻿using Core.Entities.Base;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class Review : BaseEntity
    {
        public required string ReviewText { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [JsonIgnore]
        public AppUser AppUser { get; set; } = null!;
    }
}
