using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Entities;
using manageContacts.Extensions;
using System.Collections.Generic;
using manageContacts.Entities.Models;

namespace manageContacts.Entities.ExtendedModels
{

    public class ClientContactModel
    {
        public ClientContactModel()
        {
            this.mobileNumbers = new List<string>();
            this.contactTags = new List<Dictionary<string, string>>();
        }


        public Guid guid { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public List<string> mobileNumbers { get; set; }

        public List<Dictionary<string, string>> contactTags { get; set; }

        public string address { get; set; }

        public string email { get; set; }

        public string contactType { get; set; }

        public const string sim = "sim";
        public const string contact = "phone";
    }
}