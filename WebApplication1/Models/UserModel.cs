using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    /// <summary>
    /// 会员视图模型
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }

        [Display(Name ="会员名")]
        [Required(ErrorMessage ="{0}必须填写")]
        public string Name { get; set; }

        [Display(Name = "住址")]
        public string  Address { get; set; }

        [Display(Name = "真实姓名")]
        [Required(ErrorMessage = "{0}必须填写")]
        public string UserName { get; set; }

        [Display(Name = "邮箱")]
        [Required(ErrorMessage = "{0}必须填写")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}必须填写")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength =3)]
        public string Pwd { get; set; }

        [Display(Name = "创建日期")]
        public DateTime AddedDate { get; set; }

        [Display(Name = "更新日期")]
        public DateTime ModifiedDate { get; set; }

        public byte[] RowVersion { get; set; }





    }
}