using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace manageContacts.Entities.Models
{
    [Table("mobileNumber")]
    public class MobileNumber : IEntity
    {
        public MobileNumber()
        {
            this.number = "";
            this.guid = Guid.NewGuid();
        }

        [Key]
        [Column("id")]
        public int id { get; set; }

        [Required(ErrorMessage = "GUID is required")]
        public Guid guid { get; set; }

        [Required(ErrorMessage = "Number is required")]
        [StringLength(64, MinimumLength = 3)]
        public string number { get; set; }

        [JsonIgnore]
        public SimContact contact { get; set; }
    }
}