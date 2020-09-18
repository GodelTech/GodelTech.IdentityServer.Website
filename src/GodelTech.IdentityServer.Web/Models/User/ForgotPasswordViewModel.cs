﻿using System.ComponentModel.DataAnnotations;

 namespace GodelTech.IdentityServer.Web.Models.User
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
