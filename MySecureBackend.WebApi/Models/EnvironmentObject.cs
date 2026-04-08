using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MySecureBackend.WebApi.Models
{
    public class EnvironmentObject
    {
        [Required]
        public Guid EnvGuid { get; set; }

        [Required, MaxLength(25)]
        public string? EnvName { get; set; }

        [Range(10, 100)]
        public int MaxHeight { get; set; }

        [Range(20, 200)]
        public int MaxLenght { get; set; }

        [ValidateNever]
        public string UserId { get; set; }
    }
}
