using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.Models
{
    public class Expert
    {
        [Key] //主索引
        [Required] //必填
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動產生編號
        public int Id { get; set; }

        [Required] //必填
        [Display(Name = "姓名")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Name { get; set; }

        [Required] //必填
        [Display(Name = "現職")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Job { get; set; }

        [Display(Name = "圖片")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Photo { get; set; }

        [AllowHtml]
        [Required] //必填
        [Display(Name = "學歷")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Education { get; set; }

        [AllowHtml]
        [Display(Name = "基本介紹")] //顯示的名稱，但在DB及程式碼仍用英文
        public string BasicIntroduction { get; set; }

        [AllowHtml]
        [Display(Name = "相關介紹")] //顯示的名稱，但在DB及程式碼仍用英文
        public string RelativeIntroduction { get; set; }

        [Required] //必填
        [Display(Name = "發布者 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public string publishUser { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "發布時間 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public DateTime? publishDate { get; set; } //資料庫要同步改為非必填

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "建立時間 ")] //顯示的名稱，但在DB及程式碼仍用英文
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? InitDate { get; set; } //資料庫要同步改為非必填
    }
}