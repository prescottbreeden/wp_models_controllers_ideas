using System;

namespace WeddingPlanner
{
    public abstract class BaseDataModel
    {
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}