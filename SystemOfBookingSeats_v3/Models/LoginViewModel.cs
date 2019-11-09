using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemOfBookingSeats_v3.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please write the username.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please write the password.")]
        [DataType(DataType.Password)]
        public string Password
        {
            get; set;
        }
    }
}