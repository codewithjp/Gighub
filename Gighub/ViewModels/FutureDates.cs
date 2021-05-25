using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.ViewModels
{
    public class FutureDates:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var IsValid = DateTime.TryParseExact(value.ToString(), "d MMM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateTime);
            return (IsValid && dateTime > DateTime.Now);
        }
    }
}
