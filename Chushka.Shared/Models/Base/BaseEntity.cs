using System;
using System.ComponentModel.DataAnnotations;

namespace Chushka.Shared.Models.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
