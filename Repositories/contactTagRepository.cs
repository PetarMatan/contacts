using manageContacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using manageContacts.Interfaces;
using manageContacts.Entities.Models;


namespace manageContacts.Repositories
{
    public class contactTagRepository : RepositoryBase<ContactTag>
    {
        RepositoryContext _repositoryContext;
        public contactTagRepository(RepositoryContext repositoryContext)
                    : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

    }
}