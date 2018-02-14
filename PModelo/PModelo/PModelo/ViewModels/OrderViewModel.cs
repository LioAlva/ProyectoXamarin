﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PModelo.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryInformation { get; set; }

        public string Client { get; set; }
    }
}