using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentityPr.ViewModels
{
    public class AlbumUpdateViewModel
    {
        [Display(Name = "專輯")]
        [Required(ErrorMessage = "{0}必填"), MaxLength(100, ErrorMessage = "{0}長度不可超過{1}")]
        public string Title { get; set; }

        [Display(Name = "藝人")]
        [Required(ErrorMessage = "{0}必填"), MaxLength(100, ErrorMessage = "{0}長度不可超過{1}")]
        public string Artist { get; set; }

        [Display(Name = "發行日期")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "價格")]
        [Required(ErrorMessage = "{0}必填"), Range(1, 10000000, ErrorMessage = "{0}的範圍再{1}-{2}之間")]
        public decimal Price { get; set; }

        [Display(Name = "封面地址")]
        [Required(ErrorMessage = "{0}必填"), MaxLength(1000, ErrorMessage = "{0}長度不可超過{1}")]
        public string CoverUrl { get; set; }
    }
}
