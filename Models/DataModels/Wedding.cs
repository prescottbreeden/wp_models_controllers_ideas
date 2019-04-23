using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WeddingPlanner.Models
{
    public class Wedding : BaseDataModel
    {
        public int WeddingId { get; set; }
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Reservation> RSVPs { get; set; }

        public Wedding() { }

        public Wedding(NewWedding formData)
        {
            WedderOne = formData.WedderOne;
            WedderTwo = formData.WedderTwo;
            Date = formData.Date;
            Location = formData.Location;
            UserId = formData.UserId;
        }
    }

}