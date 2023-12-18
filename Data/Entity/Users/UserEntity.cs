using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Common.Dto;
using Common.Enums;
using Microsoft.Extensions.Logging.Abstractions;

namespace Data.Entity
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsVerifiedForTransporting { get; set; } = false;
        public bool Active { get; set; } = false;
        public bool Banned { get; set; } = false;
        [MaxLength(100)]
        public string Name { get; set; } = "";
        [MaxLength(100)]
        public string Surname { get; set; } = "";
        [MaxLength(100)]
        public string PhoneNumber { get; set; } = "";
        [MaxLength(100)]
        public string Ico { get; set; } = "";
        [MaxLength(100)]
        public string Dic { get; set; } = "";
        public bool IsCompany { get; set; } = false;
        public ICollection<Notifications> Notifications { get; set; } = new List<Notifications> { };

        // relations
        public int? AddressId { get; set; }
        public virtual UserAddress Address { get; set; }
        public int? MailingAddressId { get; set; }
        public virtual UserAddress MailingAddress { get; set; }
        public int? TransferInfoId { get; set; }
        public int? TransporterRequirementsId { get; set; }

        public UserEntity(RegistrationUserRequestDto req)
        {
            Email = req.Email;
            Password = req.Password;
            Role = req.Role;
        }

        public UserEntity()
        { }

        public bool IsNotificationEnabled(Notifications notification)
        {
            return this.Notifications.Contains(notification);
        }

        public UserDetailResponseDto convertToResponse()
        {
            var response = new UserDetailResponseDto();
            response.Id = this.Id;

            return response;
        }

        public UserSettingsResponseDto convertToUserSettingsResponse()
        {
            var response = new UserSettingsResponseDto();
            response.Name = this.Name;
            response.Surname = this.Surname;
            response.PhoneNumber = this.PhoneNumber;
            response.Dic = this.Dic;
            response.Ico = this.Ico;
            response.Notifications = this.Notifications;
            response.IsCompany = this.IsCompany;
            response.IsVerifiedForTransporting = this.IsVerifiedForTransporting;
            response.Address = this.Address == null ? null : this.Address.convertToResponse();
            response.MailingAddress = this.MailingAddress == null ? null : this.MailingAddress.convertToResponse();
            response.TransferInfo = null;
            response.TransporterRequirements = null;

            return response;

        }

        public AdminUserDetailResponseDto convertToAdminUserDetailResponse()
        {
            var response = new AdminUserDetailResponseDto();
            response.Id = this.Id;
            response.Email = this.Email;
            response.Active = this.Active;
            response.Banned = this.Banned;
            response.Name = this.Name;
            response.Surname = this.Surname;
            response.PhoneNumber = this.PhoneNumber;
            response.Ico = this.Ico;
            response.IsCompany = this.IsCompany;
            response.IsVerifiedForTransporting = this.IsVerifiedForTransporting;
            response.Address = this.Address == null ? null : this.Address.convertToResponse();
            response.MailingAddress = this.MailingAddress == null ? null : this.MailingAddress.convertToResponse();
            response.TransferInfo = null;
            response.TransporterRequirements = null;
            response.Vehicles = null;

            return response;
        }

        public UserAddress? getValidInvoiceAddress()
        {
            if (this.MailingAddress != null && this.MailingAddress.isValidForInvoice())
            {
                return this.MailingAddress;
            }
            if (this.Address != null && this.Address.isValidForInvoice())
            {
                return this.Address;
            }
            return null;
        }

        public bool hasValidInvoiceInformation()
        {
            if (String.IsNullOrEmpty(this.Name) || String.IsNullOrEmpty(this.Surname))
            {
                return false;
            }
            if (this.IsCompany && String.IsNullOrEmpty(this.Ico)) 
            {
                return false;
            }
            if ((this.MailingAddress != null && this.MailingAddress.isValidForInvoice()) ||
                this.Address != null && this.Address.isValidForInvoice())
            {
                return true;
            }
            return false;
        }
    }
}
