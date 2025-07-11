using BusinessObjects;
using Repositories;

namespace Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository iMembershipRepository;
        public MembershipService()
        {
            iMembershipRepository = new MembershipRepository();
        }
        public bool AddMembershipPackage(Membership package)
        {
            return iMembershipRepository.AddMembershipPackage(package);
        }

        public void DeleteMembershipPackage(Membership membership)
        {
            iMembershipRepository.DeleteMembershipPackage(membership);
        }

        public Membership GetCurrentMembership(int userId)
        {
            return iMembershipRepository.GetCurrentMembership(userId);
        }

        public bool IsExistingPackage(int userId)
        {
            return iMembershipRepository.IsExistingPackage(userId);
        }
    }
}
