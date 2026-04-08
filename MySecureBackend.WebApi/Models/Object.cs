using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace MySecureBackend.WebApi.Models
{
    public class ObjectRepo
    {
        [Required]
        public Guid ObjGuid { get; set; }

        [Required]
        public float PrefabId { get; set; }

        [Required]
        public float PositionX { get; set; }

        [Required]
        public float PositionY { get; set; }

        [Required, Range(1, 50)]
        public float ScaleX { get; set; }

        [Required, Range(1, 50)]
        public float ScaleY { get; set; }

        [Required]
        public float RotationZ { get; set; }

        [Required, Range(1,10)]
        public int SortingLayer { get; set; }

        [Required]
        public Guid EnvironmentGuid { get; set; }

        [Required, MaxLength(25)]
        public string ObjName { get; set; }
    }
}