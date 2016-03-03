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
        private string notes;
        private string id;
        private string orderDate;
        private string email;

        public string ID
        {
            get { return id; }

            set { id = value; }
        }
        public string Email
        {
            get { return email; }

            set { email = value; }
        }

        public string OrderDate
        {
            get { return orderDate; }

            set { orderDate = value; }
        }

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

        public string Notes
        {
            get { return notes; }

            set { notes = value; }
        }
    }
}