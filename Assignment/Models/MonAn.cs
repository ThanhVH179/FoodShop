using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public enum PhanLoai
    {
        [Display(Name = "Món")]
        MonAn = 1,
        [Display(Name = "Combo")]
        Combo = 2,
        [Display(Name = "Nước")]
        Nuoc = 3
    }
    public class MonAn
    {
        [Key]
        public int MonAnID { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên món ăn")]
        [Required(ErrorMessage = "Mời nhập tên")]
        public string Name { get; set; }

        [Display(Name = "Giá"), Range(0, double.MaxValue, ErrorMessage = "Mời nhập giá")]
        [Required(ErrorMessage = "Mời nhập giá")]
        public double Price { get; set; }

        [Display(Name = "Phân loại")]
        [Required(ErrorMessage = "Mời chọn loại"), Range(1, int.MaxValue, ErrorMessage = "Mời chọn loại")]
        public PhanLoai Type { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình")]
        public string Picture { get; set; }

        [NotMapped]
        [Display(Name = "Chọn hình")]
        public IFormFile ImageFile { get; set; }

        [StringLength(150)]
        [Display(Name = "Mô tả")]
        public string Describe { get; set; }

        [Display(Name = "Đang phục vụ")]
        public bool Status { get; set; }
    }
}
