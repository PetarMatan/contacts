using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;

namespace manageContacts.Entities.Models
{
    [Table("phoneContactTags")]
    public class PhoneContactTags
    {
        public PhoneContactTags()
        {
            this.phoneContact = new PhoneContact();
            this.contactTag = new ContactTag();
        }

        public int phoneContactId { get; set; }
        public PhoneContact phoneContact { get; set; }

        public int tagId { get; set; }
        public ContactTag contactTag { get; set; }

    }
}