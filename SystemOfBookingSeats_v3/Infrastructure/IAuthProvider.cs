﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemOfBookingSeats_v3.Infrastructure
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}