﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    internal class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (email == "a@a.com")
            {
                return false;
            }

            return true;
        }
    }
}
