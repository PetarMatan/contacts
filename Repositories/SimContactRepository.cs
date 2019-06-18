using manageContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using manageContacts.Interfaces;
using manageContacts.Entities.Models;
using manageContacts.Entities.ExtendedModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace manageContacts.Repositories
{
    public class SimContactRepository : RepositoryBase<SimContact>
    {
        RepositoryContext _repositoryContext;
        public SimContactRepository(RepositoryContext repositoryContext)
                    : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public List<SimContactExtended> getAllContacts()
        {
            ICollection<SimContactExtended> ret = new List<SimContactExtended>();
            List<SimContactExtended> allContacts = _repositoryContext.simContacts.Select(s => new SimContactExtended(s.guid, s.name, s.surname, s.GetType() != typeof(PhoneContact) )).ToList();
            return allContacts;
        }

        public SimContact findByGuid(Guid guid)
        {
            return _repositoryContext.simContacts.Where(s => s.guid == guid).Include(s => s.MobileNumbers).Include(s => (s as PhoneContact).contactTags).ThenInclude(c => c.contactTag).First();
        }

        public PhoneContact findPhoneContactByGuid(Guid guid)
        {
            return _repositoryContext.phoneContacts.Where(s => s.guid == guid).Include(s => s.MobileNumbers).Include(s => s.contactTags).First();
        }

        public Task<SimContact> asyncFindByGuid(Guid guid)
        {   //TODO: not in use, migrate sync calls to async 
            return _repositoryContext.simContacts.Where(s => s.guid == guid).Include(s => s.MobileNumbers).FirstAsync();
        }

    }
}