using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class NguoiDung
    { 
        [Key]
        public int NguoiDungID { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Tài khoản"), Required(ErrorMessage = "Mời nhập tài khoản")]
        public string UserName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Họ tên"), Required(ErrorMessage = "Mời nhập tài khoản")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Mời nhập Email")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Display(Name = "Chức danh")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDay { get; set; }

        [Display(Name = "Quản trị")]
        public bool Admin { get; set; }

        [Display(Name = "Sử dụng")]
        public bool Locked { get; set; }

        [Display(Name = "Mật khẩu")]
        [Column(TypeName = "nvarchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Required(ErrorMessage = "Mời nhập lại mật khẩu"), Display(Name = "Nhập lại mật khẩu")]
        [Display(Name = "Nhập lại mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [NotMapped]
        public string ConfirmPass { get; set; }
    }
}
