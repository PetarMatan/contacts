using manageContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using manageContacts.Interfaces;
using manageContacts.Entities.Models;

namespace manageContacts.Repositories
{
    public class PhoneContactTagRepository : RepositoryBase<PhoneContactTags>
    {
        private RepositoryContext _repositoryContext;
        public PhoneContactTagRepository(RepositoryContext repositoryContext)
                    : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

         public List<PhoneContactTags> findAllByCondition(int phoneContactId){
            return _repositoryContext.PhoneContactTags.Where(s => s.phoneContactId == phoneContactId).ToList();
        }
    }
}