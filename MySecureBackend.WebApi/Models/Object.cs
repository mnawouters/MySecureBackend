using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MySecureBackend.WebApi.Models
{
    public class ObjectRepo
    {
        public Guid ObjGuid { get; set; }

        [Required, MaxLength(25)]
        public string? ObjName { get; set; }

        public float PrefabId { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        [Range(1, 50)]
        public float ScaleX { get; set; }

        [Range(1, 50)]
        public float ScaleY { get; set; }

        public float RotationZ { get; set; }

        [Required]
        public int SortingLayer { get; set; }

        [Required]
        public Guid EnvironmentGuid { get; set; }
    }
}