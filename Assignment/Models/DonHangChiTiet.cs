using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class DonHangChiTiet
    {
        [Key]
        public int ChiTietID { get; set; }

        [ForeignKey("Donhang")]
        public int DonHangID { get; set; }

        [ForeignKey("MonAn")]
        public int MonAnID { get; set; }

        [Required, Range(0, int.MaxValue, ErrorMessage = "Bạn cần nhập số lượng.")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập thành tiền.")]
        [Display(Name = "Thành tiền")]
        public double Price { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        public DonHang DonHang { get; set; }
        public MonAn MonAn { get; set; }
    }
}
