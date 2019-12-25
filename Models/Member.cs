using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // 標籤
using System.ComponentModel.DataAnnotations.Schema; // [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sys.Models
{
    public class Member
    {
        [Key] //主索引，需 Int
        [Required] //必填
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動產生編號
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}必填")] //必填
        [Display(Name = "帳號")] //顯示的名稱，但在DB及程式碼仍用英文
        [MaxLength(50)] //限定字元
        //[Remote("CheckAccount", "Login",HttpMethod = "POST",ErrorMessage = "此帳號已被申請過")]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Password { get; set; }

        [Display(Name = "密碼鹽")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Salt { get; set; }

        [Required(ErrorMessage = "{0}必填")] //必填
        [MaxLength(50)] //限定字元
        [Display(Name = "管理者名稱")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Name { get; set; }

        [Required]
        [Display(Name = "性別")] //顯示的名稱，但在DB及程式碼仍用英文
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "{0}必填")] //必填
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)] //限定字元
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Email { get; set; }

        [MaxLength(200)] //限定字元
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "公司電話")] //顯示的名稱，但在DB及程式碼仍用英文
        public string PhoneNumber { get; set; }

        [Display(Name = "照片")] //顯示的名稱，但在DB及程式碼仍用英文
        [MaxLength(100)] //限定字元
        public string Image { get; set; }

        [MaxLength(50)] //限定字元
        [Display(Name = "職稱")] //顯示的名稱，但在DB及程式碼仍用英文
        public string JobTitle { get; set; }

        [Display(Name = "管理者類別")]
        public ManagerType ManagerType { get; set; }

        [MaxLength(100)] //限定字元
        [Display(Name = "權限")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Permission { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")] //HTML 日曆只吃yyyy-MM-dd格式
        [Display(Name = "建立日期")] //顯示的名稱，但在DB及程式碼仍用英文
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //該Computed選項指定屬性值將在首次保存時由數據庫生成，並在每次更新值時重新生成。這樣做的實際效果是，實體框架不會在INSERT或UPDATE語句中包括該屬性，而是在檢索時從數據庫中獲取計算值。
        //需 using System.ComponentModel.DataAnnotations.Schema，但 EF5 Code First 並不支援設定預設值，而此舉的意義為讓 EF 不再追蹤此屬性的任何物件變化
        public DateTime? InitDate { get; set; } //資料庫要同步改為非必填


        // [NotMapped] 不在資料庫產生對應欄位，需 using System.ComponentModel.DataAnnotations.Schema
    }
}