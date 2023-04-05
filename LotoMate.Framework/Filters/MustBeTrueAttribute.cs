using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LotoMate.Framework.Filters
{
    public sealed class MustBeTrueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Object.Equals(value, true);
        }
    }
}
