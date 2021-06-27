using System;
using System.ComponentModel.DataAnnotations;

namespace Chuska.Shared.Models.Enums
{
    public enum ProductType
    {
        [Display(Name = "Food")]
        Food,
        [Display(Name = "Domestic")]
        Domestic,
        [Display(Name = "Health")]
        Health,
        [Display(Name = "Cosmetic")]
        Cosmetic,
        [Display(Name = "Other")]
        Other
    }
}
