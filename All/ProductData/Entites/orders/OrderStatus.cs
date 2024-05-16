﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Entites.orders
{
    public enum OrderStatus
    {
        [EnumMember(Value ="Pending")]
        Pending,
        [EnumMember(Value = "PaymentSucceded")]
        PaymentSucceded,
        [EnumMember(Value = "PaymentFailed")]
        PaymentFailed,
    }
}
