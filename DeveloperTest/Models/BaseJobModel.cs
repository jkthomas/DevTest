using System;

namespace DeveloperTest.Models
{
    public class BaseJobModel
    {
        public string Engineer { get; set; }
        public BaseCustomerModel Customer { get; set; }

        public DateTime When { get; set; }
    }
}
