using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models
{
    public class Role
    {
        [Key]
        public int GroupID { get; set; }

        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "用户名为{1}到{0}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 0普通类型（普通注册用户），1特权类型（像VIP之类的类型），3管理类型（管理权限的类型）
        /// </summary>
        [Required(ErrorMessage = "用户组类型不能为空")]
        [Display(Name = "用户组类型")]
        public int Type { get; set; }

        [Required(ErrorMessage = "描述不能为空")]
        [StringLength(50, ErrorMessage = "描述不能少于{0}个字")]
        [Display(Name = "描述")]
        public string Description { get; set; }

        /// <summary>
        /// 获取角色类型名称
        /// </summary>
        /// <returns></returns>
        public string TypeToString()
        {
            switch (Type)
            {
                case 0:
                    return "普通";
                case 1:
                    return "特权";
                case 2:
                    return "管理";
                default:
                    return "未知";
            }
        }
    }
}
