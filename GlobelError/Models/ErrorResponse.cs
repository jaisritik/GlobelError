﻿using System.Text.Json;

namespace GlobelError.Models
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
