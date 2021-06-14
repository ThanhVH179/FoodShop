using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class KhachHang
    {
        [Key]
        public int KhachHangID { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Bạn cần nhập họ tên.")]
        [Display(Name = "Họ & tên")]
        public string FullName { get; set; }

        [Display(Name = "Email"),Required(ErrorMessage = "Mời nhập Email")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Email không hợp lệ")]
        public string EmailKH { get; set; }

        [Display(Name = "Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại"), Display(Name = "Số điện thoại")]
        [Column(TypeName = "varchar(15)"), MaxLength(15)]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{4})[-. ]?([0-9]{3})$", ErrorMessage = "Số điện thoại không đúng")]
        //091-1234-567
        public string PhoneNumber { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu"), Display(Name = "Mật khẩu")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu (2)"), Display(Name = "Mật khẩu (2)")]
        [Column(TypeName = "varchar(50)"), MaxLength(50)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [StringLength(250)]
        [Display(Name = "Mô tả")]
        public string Describe { get; set; }
    }
}
