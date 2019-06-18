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
    public class MobileNumberRepository : RepositoryBase<MobileNumber>
    {
        RepositoryContext _repositoryContext;
        public MobileNumberRepository(RepositoryContext repositoryContext)
                    : base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void createMobileNumber(MobileNumber mobileNum)
        {
            mobileNum.guid = Guid.NewGuid();
            Create(mobileNum);
            Save();
        }

        public MobileNumber findByNumber(string number)
        {
            return _repositoryContext.mobileNumbers.Where(m => m.number == number).Include(m => m.contact).FirstOrDefault();
        }
    }
}