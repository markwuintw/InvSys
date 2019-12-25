using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sys.Models
{
    public class ContactViewModel
    {
        //// 無須id
        //[Key] //主索引，需 Int
        //[Required] //必填
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] //自動產生編號
        //public int Id { get; set; }


        [Required(ErrorMessage = "{0}必填")] //必填
        [MaxLength(50)] //限定字元
        [Display(Name = "名稱")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Name { get; set; }


        [Required(ErrorMessage = "{0}必填")] //必填
        [EmailAddress(ErrorMessage = "{0} 格式錯誤")]
        [MaxLength(200)] //限定字元
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Email { get; set; }


        [MaxLength(200)] //限定字元
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "電話")] //顯示的名稱，但在DB及程式碼仍用英文
        public string PhoneNumber { get; set; }


        [DataType(DataType.PhoneNumber)]
        [Display(Name = "註解")] //顯示的名稱，但在DB及程式碼仍用英文
        public string Content { get; set; }
    }


}