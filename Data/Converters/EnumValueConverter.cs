using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Converters
{
    public class EnumValueConverter<T> : ValueConverter<T, string>
    {
        public EnumValueConverter() : base(
            v => v.ToString(),
            v => (T)Enum.Parse(typeof(T), v)
            )
        {
        }
    }
}
