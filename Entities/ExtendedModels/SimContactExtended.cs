using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using manageContacts.Entities.Models;

namespace manageContacts.Entities.ExtendedModels
{
    public class SimContactExtended
    {
        public SimContactExtended(
            Guid guid,
            string name,
            string surname,
            bool isSim
        )
        {
            this.name = name;
            this.guid = guid;
            this.surname = surname;
            this.contactType = isSim ? "sim" : "phone";
        }


        [Required(ErrorMessage = "GUID is required")]
        public Guid guid { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string contactType {get;set;}
    }
}