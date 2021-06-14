using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public enum TrangThaiDonHang // Trạng thái đơn hàng
    {
        [Display(Name = "Mới đặt")]
        Moidat = 1,
        [Display(Name = "Đang giao")]
        Danggiao = 2,
        [Display(Name = "Đã giao")]
        Dagiao = 3
    }
    public class DonHang
    {
        [Key]
        public int DonHangID { get; set; }

        [ForeignKey("KhachHang")]
        public int KhachHangID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")]
        [Required(ErrorMessage = "Bạn cần chọn ngày."), Display(Name = "Ngày đặt")]
        public DateTime OrderDay { get; set; }

        [Required, Range(0, double.MaxValue, ErrorMessage = "Bạn cần nhập giá.")]
        [Display(Name = "Tổng tiền")]
        public double Total { get; set; }

        [Display(Name = "Trạng thái")]
        public TrangThaiDonHang Status { get; set; }

        [StringLength(250)]
        [Display(Name = "Ghi chú")]
        public string Note { get; set; }

        public KhachHang KhachHang { get; set; }
        public List<DonHangChiTiet> DonHangChiTiets { get; set; }
    }

}
