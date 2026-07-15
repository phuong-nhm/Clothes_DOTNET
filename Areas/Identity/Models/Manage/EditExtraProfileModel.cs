using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Tên tài khoản")]
      public string UserName { get; set; }

      [Display(Name = "Địa chỉ email")]
      public string UserEmail { get; set; }
      [Display(Name = "Số điện thoại")]
      public string PhoneNumber { get; set; }

      [Display(Name = "Địa chỉ")]
      [StringLength(400, ErrorMessage = "Địa chỉ không được vượt quá 400 ký tự.")]
      public string? HomeAdress { get; set; }
      
      [Display(Name = "Họ và tên")]
      public string FullName { get; set; }
      [Display(Name = "Giới tính")]
      public string Gender { get; set; }



        [Display(Name = "Ngày sinh")]
      public DateTime? BirthDate { get; set; }
  }
}