using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class ChangePasswordViewModel
    {
        public string Password { get; set; }

        public string OriginalPassword { get; set; }
    }
}