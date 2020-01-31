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
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //該Computed選項指定屬性值將在首次保存時由數據庫生成，並在每次更新值時重新生成。這樣做的實際效果是，實體框架不會在INSERT或UPDATE語句中包括該屬性，而是在檢索時從數據庫中獲取計算值。
        //需 using System.ComponentModel.DataAnnotations.Schema，但 EF5 Code First 並不支援設定預設值，而此舉的意義為讓 EF 不再追蹤此屬性的任何物件變化
        public DateTime? InitDate { get; set; } //資料庫要同步改為非必填


        // [NotMapped] 不在資料庫產生對應欄位，需 using System.ComponentModel.DataAnnotations.Schema



        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "生日")] //顯示的名稱，但在DB及程式碼仍用英文
        public DateTime? BirthDate { get; set; } //資料庫要同步改為非必填

        [Display(Name = "申請類別")] //顯示的名稱，但在DB及程式碼仍用英文
        public ApplicationType ApplicationType { get; set; } //資料庫要同步改為非必填

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "手機")] //顯示的名稱，但在DB及程式碼仍用英文
        public string CellPhoneNumber { get; set; }

        [Display(Name = "地址")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Address { get; set; }


        [Display(Name = "國際會籍")] //顯示的名稱，但在DB及程式碼仍用英文
        public BooleanType InternationalMembership { get; set; }

        [Display(Name = "國際會籍會員證影本 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String InternationalMembershipFile { get; set; }

        [Display(Name = "現職單位 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String Company { get; set; }

        [Display(Name = "最高學歷 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String HighEducation { get; set; }



        [Display(Name = "經歷單位1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpCom1 { get; set; }


        [Display(Name = "經歷職稱1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpJob1 { get; set; }


        [Display(Name = "經歷起年1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDY1 { get; set; }


        [Display(Name = "經歷起月1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDM1 { get; set; }

        [Display(Name = "經歷迄年1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDY1 { get; set; }


        [Display(Name = "經歷迄月1 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDM1 { get; set; }








        [Display(Name = "經歷單位2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpCom2 { get; set; }


        [Display(Name = "經歷職稱2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpJob2 { get; set; }


        [Display(Name = "經歷起年2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDY2 { get; set; }


        [Display(Name = "經歷起月2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDM2 { get; set; }

        [Display(Name = "經歷迄年2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDY2 { get; set; }


        [Display(Name = "經歷迄月2 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDM2 { get; set; }









        [Display(Name = "經歷單位3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpCom3 { get; set; }


        [Display(Name = "經歷職稱3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public String ExpJob3 { get; set; }


        [Display(Name = "經歷起年3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDY3 { get; set; }


        [Display(Name = "經歷起月3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpSDM3 { get; set; }

        [Display(Name = "經歷迄年3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDY3 { get; set; }


        [Display(Name = "經歷迄月3 ")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? ExpEDM3 { get; set; }








        [Display(Name = "相關年資合計年")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? TotalJobYear { get; set; }


        [Display(Name = "相關年資合計月")] //顯示的名稱，但在DB及程式碼仍用英文
        public int? TotalJobMonth { get; set; }


    }
}