using manageContacts.Entities.Models;
using manageContacts.Repositories;
using manageContacts.Entities.ExtendedModels;
using manageContacts.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using manageContacts.Extensions;
using System.Collections.Generic;

namespace manageContacts.Services
{
    public class ContactService
    {
        private contactTagRepository _contantTagRepo;

        private MobileNumberRepository _mobileNumberRepo;

        private SimContactRepository _simContactRepo;

        private PhoneContactTagRepository _phoneContactTagRepo;

        public ContactService(
            contactTagRepository contactTagRepository,
            MobileNumberRepository mobileNumberRepository,
            SimContactRepository simContactRepository,
            PhoneContactTagRepository PhoneContactTagRepository
            )
        {
            _contantTagRepo = contactTagRepository;
            _mobileNumberRepo = mobileNumberRepository;
            _simContactRepo = simContactRepository;
            _phoneContactTagRepo = PhoneContactTagRepository;
        }


        public List<SimContactExtended> getAllExtendedContacts()
        {
            return _simContactRepo.getAllContacts();
        }

        public void deleteContact(Guid guid)
        {
            SimContact contact = _simContactRepo.findByGuid(guid);
            if (contact == null) return;
            this.deleteMobileNumber(contact);
            if (contact.GetType() == typeof(PhoneContact))
                this.deleteContactTags(contact);
            _simContactRepo.Delete(contact);
            _simContactRepo.Save();
        }

        public IEntity editContact(ClientContactModel clientContactModel)
        {
            switch (clientContactModel.contactType)
            {
                case ClientContactModel.contact:
                    return updatePhoneContact(clientContactModel);
                default:
                    return updateSimContact(clientContactModel);
            }
        }

        public IEntity createContact(ClientContactModel clientContactModel)
        {
            switch (clientContactModel.contactType)
            {
                case ClientContactModel.contact:
                    return createPhoneContact(clientContactModel);
                default:
                    return createSimContact(clientContactModel);
            }
        }

        private SimContact createSimContact(ClientContactModel clientContactModel)
        {
            SimContact simContact = new SimContact();
            simContact.name = clientContactModel.name;
            simContact.surname = clientContactModel.surname;
            simContact.setMobileNumbers(this.createMobileNumbers(clientContactModel.mobileNumbers));
            _simContactRepo.Create(simContact);
            _simContactRepo.Save();
            return simContact;
        }
        private PhoneContact createPhoneContact(ClientContactModel clientContactModel)
        {
            PhoneContact phoneContact = new PhoneContact();
            phoneContact.name = clientContactModel.name;
            phoneContact.surname = clientContactModel.surname;
            phoneContact.email = clientContactModel.email;
            phoneContact.address = clientContactModel.address;
            _simContactRepo.Create(phoneContact);
            _simContactRepo.Save();
            phoneContact.setMobileNumbers(this.createMobileNumbers(clientContactModel.mobileNumbers));
            phoneContact.setContactTags(this.createContactTags(clientContactModel.contactTags, phoneContact));
            return phoneContact;
        }

        private ICollection<MobileNumber> createMobileNumbers(List<string> mobileNumbers)
        {
            ICollection<MobileNumber> mobileNumberEntities = new List<MobileNumber>();
            foreach (string mobNum in mobileNumbers)
            {
                var dbMobileNum = _mobileNumberRepo.findOneBy(MobileNumber => MobileNumber.number.Equals(mobNum));
                if (dbMobileNum != null && dbMobileNum.contact != null)
                    throw new Exception("Mobile number " + dbMobileNum.number + " is already in use");
                if (dbMobileNum == null)
                {
                    MobileNumber newMobileNum = new MobileNumber();
                    newMobileNum.number = mobNum;
                    _mobileNumberRepo.Create(newMobileNum);
                    _mobileNumberRepo.Save();
                    mobileNumberEntities.Add(newMobileNum);
                    continue;
                }
                mobileNumberEntities.Add(dbMobileNum);
            }
            return mobileNumberEntities;
        }

        private ICollection<PhoneContactTags> createContactTags(List<Dictionary<string, string>> contactTags, PhoneContact phoneContact)
        {
            ICollection<ContactTag> contactTagEntities = new List<ContactTag>();
            foreach (Dictionary<string, string> conTag in contactTags)
            {
                ContactTag dbContantTag = _contantTagRepo.findOneBy(c => c.tag.ToString().Equals(conTag["value"]));
                if (phoneContact.contactContainsTag(dbContantTag))
                    continue;

                if (dbContantTag == null)
                {
                    dbContantTag = new ContactTag();
                    dbContantTag.tag = conTag["value"];
                    _contantTagRepo.Create(dbContantTag);
                    _contantTagRepo.Save();
                }
                contactTagEntities.Add(dbContantTag);
            }
            return this.createPhoneContactTags(contactTagEntities, phoneContact);
        }


        private SimContact updateSimContact(ClientContactModel clientContactModel)
        {
            if (clientContactModel.guid == null) throw new Exception("Guid is not defined");
            SimContact dbContact = _simContactRepo.findByGuid(clientContactModel.guid);
            if (dbContact == null)
                throw new Exception("Contact by guid: " + clientContactModel.guid + " is not found in db");
            dbContact.name = clientContactModel.name;
            dbContact.surname = clientContactModel.surname;
            this.updateMobileNumbers(clientContactModel.mobileNumbers, dbContact);
            _simContactRepo.Save();
            return dbContact;
        }

        private PhoneContact updatePhoneContact(ClientContactModel clientContactModel)
        {
            PhoneContact dbContact = _simContactRepo.findPhoneContactByGuid(clientContactModel.guid);
            if (dbContact == null)
                throw new Exception("Contact by guid: " + clientContactModel.guid + " is not found in db");

            dbContact.name = clientContactModel.name;
            dbContact.surname = clientContactModel.surname;
            dbContact.email = clientContactModel.email;
            dbContact.address = clientContactModel.address;
            this.updateMobileNumbers(clientContactModel.mobileNumbers, dbContact);
            dbContact.setContactTags(this.updateContactTags(clientContactModel.contactTags, dbContact));
            return dbContact;
        }

        private void updateMobileNumbers(List<string> clientMobileNumbers, SimContact contact)
        {
            List<string> contactNumbers = new List<string>();
            List<MobileNumber> numbersToDelete = new List<MobileNumber>();
            foreach (MobileNumber mobileNumber in contact.MobileNumbers)
            {
                contactNumbers.Add(mobileNumber.number);
                if (!clientMobileNumbers.Contains(mobileNumber.number))
                    numbersToDelete.Add(mobileNumber);

            }
            foreach (MobileNumber mobileNumber in numbersToDelete)
            {
                contact.removeMobileNumber(mobileNumber);
                _mobileNumberRepo.Delete(mobileNumber);
            }
            foreach (string clientNumber in clientMobileNumbers)
            {
                if (!contactNumbers.Contains(clientNumber))
                {
                    MobileNumber newMobileNumber = new MobileNumber()
                    {
                        number = clientNumber
                    };
                    _mobileNumberRepo.Create(newMobileNumber);
                    contact.addMobileNumber(newMobileNumber);
                }
            }
            _mobileNumberRepo.Save();
        }

        private ICollection<PhoneContactTags> updateContactTags(List<Dictionary<string, string>> contactTags, PhoneContact phoneContact)
        {
            this.deleteContactTags(phoneContact);
            ICollection<ContactTag> newContactTags = new HashSet<ContactTag>();
            foreach (Dictionary<string, string> contactTag in contactTags)
            {
                ContactTag dbContactTag = _contantTagRepo.findOneBy(c => c.tag.ToString().ToLower().Equals(contactTag["value"].ToLower()));
                if (dbContactTag == null)
                {
                    dbContactTag = new ContactTag();
                    dbContactTag.tag = contactTag["value"];
                    _contantTagRepo.Create(dbContactTag);
                    _contantTagRepo.Save();
                    newContactTags.Add(dbContactTag);
                    continue;
                }

                if (dbContactTag != null && !phoneContact.contactContainsTag(dbContactTag))
                {
                    newContactTags.Add(dbContactTag);
                }
            }
            return this.createPhoneContactTags(newContactTags, phoneContact);
        }

        private ICollection<PhoneContactTags> createPhoneContactTags(ICollection<ContactTag> contactTags, PhoneContact contact)
        {
            ICollection<PhoneContactTags> conTags = new HashSet<PhoneContactTags>();
            foreach (ContactTag contactTag in contactTags)
            {
                PhoneContactTags phoneContactTag = new PhoneContactTags();
                phoneContactTag.contactTag = contactTag;
                phoneContactTag.phoneContact = contact;
                _phoneContactTagRepo.Create(phoneContactTag);
                _phoneContactTagRepo.Save();
                conTags.Add(phoneContactTag);
            }
            return conTags;
        }


        private void deleteMobileNumber(SimContact contact)
        {
            ICollection<MobileNumber> mobileNumbers = contact.MobileNumbers;
            foreach (MobileNumber mobileNumber in mobileNumbers)
            {
                _mobileNumberRepo.Delete(mobileNumber);
            }
            contact.MobileNumbers.Clear();
            _mobileNumberRepo.Save();
        }

        private void deleteContactTags(dynamic contact)
        {
            //this is hack, should iterate to see which ones are deleted 
            List<PhoneContactTags> contactTags = _phoneContactTagRepo.findAllByCondition(contact.id);
            foreach (PhoneContactTags contactTag in contactTags)
            {
                _phoneContactTagRepo.Delete(contactTag);
            }
            _phoneContactTagRepo.Save();
        }
    }
}