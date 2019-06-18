using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;

namespace manageContacts.Entities.Models
{
    public class SimContact : IEntity
    {
        public SimContact()
        {
            this.name = "";
            this.surname = "";
            this.guid = Guid.NewGuid();
            this.MobileNumbers = new HashSet<MobileNumber>();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "GUID is required")]
        public Guid guid { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public virtual ICollection<MobileNumber> MobileNumbers { get; set; }

        public void addMobileNumber(MobileNumber mobileNumber)
        {
            this.MobileNumbers.Add(mobileNumber);
        }

        public void removeMobileNumber(MobileNumber mobileNumber)
        {
            if (this.MobileNumbers.Contains(mobileNumber))
                this.MobileNumbers.Remove(mobileNumber);
        }

        public void setMobileNumbers(ICollection<MobileNumber> mobileNumbers)
        {
            this.MobileNumbers = mobileNumbers;
        }
    }
}