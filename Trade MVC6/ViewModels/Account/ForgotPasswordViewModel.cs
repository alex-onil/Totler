﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trade_MVC6.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string Email { get; set; }
        }
}
