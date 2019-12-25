using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sys.Models
{
    public class Permissions
    {
        [Key] //主索引
        [Required] //必填
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動產生編號
        public int Id { get; set; }

        public int PId { get; set; }

        [Display(Name = "權限")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Permission { get; set; }

        [Display(Name = "Controller")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Controller { get; set; }

        [Display(Name = "Action")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Action { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "建立日期")] //顯示的名稱，但在DB及程式碼仍用英文
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? InitDate { get; set; } //資料庫要同步改為非必填


    }
}