using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Converters
{
    public class JsonValueConverter<T> : ValueConverter<T, string>
    {
        public JsonValueConverter() : base(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<T>(v)
            )
        {
        }
    }

    public class JsonArrayConverter<T> : ValueConverter<ICollection<T>, string> 
    {
        public JsonArrayConverter() : base(
            v => JsonConvert
                .SerializeObject(v),
            v => JsonConvert
                .DeserializeObject<ICollection<T>>(v).ToList())
        {
        }
    }
}
