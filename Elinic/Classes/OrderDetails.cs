using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Elinic.Classes
{
    public class OrderDetails
    {
        private string numOfDoors;
        private string details;
        private string price;

        public string NumOfDoors
        {
            get { return numOfDoors; }

            set { numOfDoors = value; }
        }

        public string Details
        {
            get { return details; }

            set { details = value; }
        }

        public string Price
        {
            get { return price; }

            set { price = value; }
        }
    }
}