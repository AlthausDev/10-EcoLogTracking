using System.ComponentModel.DataAnnotations;

namespace EcoLogTracking.Shared.Models.Enum
{
    public enum UserTypeEnum
    {
        [Display(Name = "ADMINISTRADOR")]
        ADMINISTRADOR,

        [Display(Name = "USUARIO")]
        USUARIO
    }
}
