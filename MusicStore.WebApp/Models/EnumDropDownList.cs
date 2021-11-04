using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicStore.WebApp.Models
{
    public static class EnumDropDownList
    {
        public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                select new SelectListItem
                {
                    Selected = e.Equals(enumValue),
                    Text = e.ToDescription(),
                    Value = e.ToString()
                };
        }

        public static string ToDescription(this Enum value)
        {
            var attributes = (DescriptionAttribute[]) value.GetType().GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}