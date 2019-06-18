using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using manageContacts.Entities.Models;

namespace manageContacts.Entities.ExtendedModels
{
    public class contactDetailsExtended
    {
        public contactDetailsExtended(dynamic contact)
        {
            this.name = contact.name;
            this.guid = contact.guid;
            this.surname = contact.surname;
            this.mobileNumbers = contact.MobileNumbers;
            if (contact.GetType() != typeof(PhoneContact)) return;
            this.address = contact.address;
            this.email = contact.email;
            this.contactTags = new HashSet<ContactTag>();
            foreach (PhoneContactTags phoneContactTag in contact.contactTags)
            {
                this.contactTags.Add(phoneContactTag.contactTag);
            }
        }


        [Required(ErrorMessage = "GUID is required")]
        public Guid guid { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public ICollection<MobileNumber> mobileNumbers { get; set; }

        public ICollection<ContactTag> contactTags { get; set; }
    }
}