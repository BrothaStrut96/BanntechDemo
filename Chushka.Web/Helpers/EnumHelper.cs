using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Chuska.Shared.Models.Enums;

namespace Chushka.Web.Helpers
{
    public static class EnumHelper
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}

