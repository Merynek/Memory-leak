using Common.Dto;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entity
{
    public class UserAddress
    {
        public int Id { get; set; }
        public Country? Country { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        public string Street { get; set; }        
        public string HouseNumber { get; set; }

        public UserAddress()
        { }

        public UserAddress(UserAddressRequestDto req)
        {
            Country = req.Country;
            City = req.City;
            PSC = req.PSC;
            Street = req.Street;
            HouseNumber = req.HouseNumber;
        }

        public UserAddressResponseDto convertToResponse()
        {
            var response = new UserAddressResponseDto();
            response.City = this.City;
            response.HouseNumber = this.HouseNumber;
            response.PSC = this.PSC;
            response.Street = this.Street;
            response.Country = this.Country;

            return response;
        }

        public bool isValidForInvoice()
        {
            if (!String.IsNullOrEmpty(this.HouseNumber) &&
                    !String.IsNullOrEmpty(this.Street) &&
                    !String.IsNullOrEmpty(this.City) &&
                    !String.IsNullOrEmpty(this.PSC) &&
                    this.Country != null)
            {
                return true;
            }
            return false;
        }
    }
}
