﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFlix.Domain.Models
{
    public class JobContact:BaseEntity
    {
        public string Name {  get; set; }
        public string Email {  get; set; }
        public int PartnerShipId {  get; set; }
        public string Message {  get; set; }
        public PartnerShip PartnerShip { get; set; }
    }
}
