using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserIdentityPr.Models
{
    public class Album
    {
        public int Id { get; set; }

        [Display(Name = "專輯")]
        public string Title { get; set; }

        [Display(Name = "藝人")]
        public string Artist { get; set; }

        [Display(Name = "發行日期")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "價格")]
        public decimal Price { get; set; }

        [Display(Name = "封面地址")]
        public string CoverUrl { get; set; }
    }
}
