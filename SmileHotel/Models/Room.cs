﻿namespace SmileHotel.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int Capacity { get; set; }

        public float PricePerNight { get; set; }
    }
}