using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.MvcApp.ViewsModels
{
    public class EnterpriseViewModel
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "公司规模不能为空")]
        [Display(Name = "公司规模")]
        public string CompanySize { get; set; }
        [Required(ErrorMessage = "公司行业不能为空")]
        [Display(Name = "公司行业")]
        public string CompanyIndustry { get; set; }
        [Required(ErrorMessage = "公司地址不能为空")]
        [Display(Name = "公司地址")]
        public string CompanyAddress { get; set; }
        [Required(ErrorMessage = "账号不能为空")]
        [Display(Name = "账号")]
        public string Account { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0}最少{2}个字符,最多{1}字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "验证密码")]
        [Compare("U_Password", ErrorMessage = "验证密码不匹配")]
        public string confirmPassword { get; set; }
        [Required(ErrorMessage = "企业名称不能为空")]
        [Display(Name = "企业名称")]
        public string EnterpriseName { get; set; }
        [Display(Name = "是否认证")]
        public int Certification { get; set; }
        [Required(ErrorMessage = "联系人不能为空")]
        [Display(Name = "联系人")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "联系电话不能为空")]
        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "你输入的电子邮件格式有误。")]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
        [Required(ErrorMessage = "固定电话不能为空")]
        [Display(Name = "固定电话")]
        public string FixedTelephone { get; set; }
        [Display(Name = "信用代码")]
        [Required(ErrorMessage = "信用代码不能为空")]
        public int Code { get; set; }//信用代码
        [Display(Name = "法定代表人")]
        [Required(ErrorMessage = "法定代表人不能为空")]
        public string RepresentativeName { get; set; }//法定代表人
    }
}
