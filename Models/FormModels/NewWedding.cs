using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner
{
    public class NewWedding
    {
        [Required]
        public string WedderOne{get;set;}
        
        [Required]
        public string WedderTwo{get;set;}
        
        [Required]
        public DateTime Date {get;set;}

        [Required]
        public string Location{get;set;}

        public int UserId {get;set;}
    }
}