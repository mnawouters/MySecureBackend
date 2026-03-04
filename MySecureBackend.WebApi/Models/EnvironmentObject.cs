using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MySecureBackend.WebApi.Models
{
    public class EnvironmentObject
    {
        public Guid EnvGuid { get; set; }

        [Required, MaxLength(25)]
        public string? EnvName { get; set; }

        [Range(10, 100)]
        public int MaxHeight { get; set; }

        [Range(20, 200)]
        public int MaxLenght { get; set; }

        [Required]
        public Guid Id { get; set; }
    }
}
