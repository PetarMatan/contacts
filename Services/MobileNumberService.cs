using manageContacts.Entities.Models;
using manageContacts.Repositories;

namespace manageContacts.Services
{
    public class MobileNumberService
    {
        private MobileNumberRepository _mobileNumberRepo;
        public MobileNumberService(MobileNumberRepository mobileNumberRepository)
        {
            _mobileNumberRepo = mobileNumberRepository;
        }
        public void Patch(MobileNumber mobileNum)
        {
            MobileNumber dbMobileNum = _mobileNumberRepo.findOneBy(MobileNumber => MobileNumber.id.Equals(mobileNum.id));
            dbMobileNum.number = mobileNum.number;
            _mobileNumberRepo.Save();
        }

        public SimContact getContactByMobileNumber(string number)
        {
            MobileNumber mobileNum = _mobileNumberRepo.findByNumber(number);
            if (mobileNum == null || mobileNum.id <= -1) return null;

            return mobileNum.contact;
        }
    }
}