using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sys.Models
{
    public class News
    {
        [Key] //主索引
        [Required] //必填
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動產生編號
        public int Id { get; set; }

        [Required] //必填
        [Display(Name = "名稱")] //顯示的名稱，但在DB及程式碼仍用英文
        public string NewsItem { get; set; }

        [Required] //必填
        [Display(Name = "簡介")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Introduction { get; set; }

        [Required] //必填
        [Display(Name = "圖片")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Photo { get; set; }

        [Required] //必填
        [Display(Name = "置頂")] //顯示的名稱，但在DB及程式碼仍用英文
        public BooleanType Top { get; set; }

        [Required] //必填
        [Display(Name = "高亮")] //顯示的名稱，但在DB及程式碼仍用英文
        public BooleanType HighLight { get; set; }

        [Display(Name = "內容")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Content { get; set; }

        [Required] //必填
        [Display(Name = "點閱數")] //顯示的名稱，但在DB及程式碼仍用英文
        public int viewers { get; set; }

        //[Required] //必填
        //[Display(Name = "發布者 ")] //顯示的名稱，但在DB及程式碼仍用英文
        //public string publishUser { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "發布時間 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public DateTime? publishDate { get; set; } //資料庫要同步改為非必填

        //[Required] //必填
        //[Display(Name = "更新者 ")] //顯示的名稱，但在DB及程式碼仍用英文
        //public string UpdateUser { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "建立時間 ")] //顯示的名稱，但在DB及程式碼仍用英文
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? InitDate { get; set; } //資料庫要同步改為非必填
    }
}