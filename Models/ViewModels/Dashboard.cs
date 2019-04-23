using System.Collections.Generic;
using WeddingPlanner.Models;

namespace WeddingPlanner
{
    public class Dashboard
    {
        public User CurrentUser { get; set; }
        public List<Wedding> AllWeddings { get; set; }
    }
}