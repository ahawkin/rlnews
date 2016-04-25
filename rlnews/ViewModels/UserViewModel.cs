using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using rlnews.DAL.Models;

namespace rlnews.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Email is requred.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [RegularExpression(@"^(?=.{4,20}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords don't match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Select Rugby Team")]
        [Required(ErrorMessage = "Team is required.")]
        public string TeamName { get; set; }

        public List<SelectListItem> TeamList { get; set; } 

        public List<Activity> ActivityList { get; set; }
    }
}