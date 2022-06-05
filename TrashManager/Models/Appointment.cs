using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace TrashManager.Models
{
    public class Appointment : IdentityUser
    {
        

        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }
        public bool Turn { get; set; }
    }
}
