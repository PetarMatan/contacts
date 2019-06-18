using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace manageContacts.Entities.Models
{
    public class PhoneContact : SimContact
    {
        public PhoneContact()
        {
            this.address = "";
            this.email = "";
            this.contactTags = new HashSet<PhoneContactTags>();
        }
        public string address { get; set; }

        public string email { get; set; }

        public virtual ICollection<PhoneContactTags> contactTags { get; set; }

        public void addContactTag(ContactTag contactTag)
        {
            PhoneContactTags phoneContactTag = new PhoneContactTags();
            phoneContactTag.contactTag = contactTag;
            phoneContactTag.phoneContact = this;
            this.contactTags.Add(phoneContactTag);
        }

        public void setContactTags(ICollection<PhoneContactTags> phoneContactTagEntities)
        {
            this.contactTags = phoneContactTagEntities;
        }

        public bool contactContainsTag(ContactTag contactTag)
        {
            if (contactTag == null) return false;
            foreach (PhoneContactTags phoneContactTag in this.contactTags)
            {
                if (phoneContactTag.tagId == contactTag.id)
                    return true;
            }

            return false;
        }
    }
}