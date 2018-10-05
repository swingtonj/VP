using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VP.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Please Enter Username")]
        public string L_Username { get; set; }

        [Required(ErrorMessage = "Please Enter password")]
        public string L_Password { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        public string R_Organisation_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        public string R_User_Name { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        public string R_Email { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        public string R_Passsword { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        public string R_Mobile { get; set; }
        
    }
}