using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Dto
{
    public class UserAddressRequestDto
    {
        public Country? Country { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
