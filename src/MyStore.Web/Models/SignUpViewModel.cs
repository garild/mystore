using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyStore.Web.Models
{
    public class SignUpViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required]
        public string Role { get; set; }

        public List<SelectListItem> Roles { get; } =
            new List<SelectListItem>
            {
                new SelectListItem {Value = "user", Text = "user"},
                new SelectListItem {Value = "admin", Text = "admin"}
            };
    }
}