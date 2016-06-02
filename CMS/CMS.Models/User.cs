using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public int GroupID { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(20,MinimumLength = 4,ErrorMessage = "用户名为{1}到{0}个字符")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "邮箱不能为空")]
        [Display(Name = "邮箱")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 用户状态
        /// 0正常，1锁定，2未通过邮件验证，3未通过管理员
        /// </summary>
        public int Status { get; set; }

        public DateTime RegistTime { get; set; }

        public DateTime LoginTime { get; set; }

        public DateTime LoginIP { get; set; }

        public virtual UserGroup Group { get; set; }
    }
}
