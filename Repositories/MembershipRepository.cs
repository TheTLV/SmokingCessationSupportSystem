using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class MembershipRepository : IMembershipRepository
    {
        public bool AddMembershipPackage(Membership package)
        {
            return MembershipDAO.AddMembershipPackage(package);
        }

        public void DeleteMembershipPackage(Membership membership)
        {
            MembershipDAO.DeleteMembershipPackage(membership);
        }

        public Membership GetCurrentMembership(int userId)
        {
            return MembershipDAO.GetCurrentMembership(userId);
        }

        public bool IsExistingPackage(int userId)
        {
            return MembershipDAO.IsExistingPackage(userId);
        }
    }
}
