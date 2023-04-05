using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LotoMate.Framework.Extensions
{
    public static class EnumExtension
    {
        public static List<EnumValue> ToList<T>(this Enum source)
        {
            var result = ((T[])Enum.GetValues(typeof(T)))
                .Select(c => new EnumValue() { Id = (int)c.GetHashCode(), Name = c.ToString() }).ToList();
                return new List<EnumValue>();
        }
       

    }
    public class EnumValue
    {
        public int Id { get; set; }
        public string  Name { get; set; }
    }
}
