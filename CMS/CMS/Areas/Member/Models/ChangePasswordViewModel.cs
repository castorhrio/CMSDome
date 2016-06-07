using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CMS.Areas.Member.Models
{
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Required(ErrorMessage = "请输入原始密码")]
        [Display(Name = "原密码")]
        [StringLength(20,MinimumLength = 6,ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string OriginalPassword { get; set; }

        [Required(ErrorMessage = "请输入新密码")]
        [Display(Name = "新密码")]
        [StringLength(20,MinimumLength = 6,ErrorMessage = "{2}到{1}个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "再次输入新密码")]
        [Compare("Password",ErrorMessage = "两次输入的密码不一致")]
        [Display(Name = "确认密码")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}