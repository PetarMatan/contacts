using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace manageContacts.Entities.Models
{
    [Table("contactTag")]
    public class ContactTag : IEntity
    {
        public ContactTag()
        {
            this.tag = "";
            this.PhoneContacts = new HashSet<PhoneContactTags>();
        }
        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [StringLength(128, MinimumLength = 1)]
        public string tag { get; set; }

        [JsonIgnore]
        public ICollection<PhoneContactTags> PhoneContacts { get; }

        public void addPhoneContact(PhoneContact phoneContact)
        {
        }
    }
}