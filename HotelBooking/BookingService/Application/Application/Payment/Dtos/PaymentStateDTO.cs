﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payment.Dtos
{
    public enum Status
    {
        Success  = 0,
        Failed = 1,
        Error = 2,
        Undefined = 3
    }
    public class PaymentStateDTO
    {
        public Status Status { get; set; }
        public string PaymentId { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Message { get; set; }
    }
}