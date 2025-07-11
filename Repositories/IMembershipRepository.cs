using BusinessObjects;

namespace Repositories
{
    public interface IMembershipRepository
    {
        bool AddMembershipPackage(Membership package);
        bool IsExistingPackage(int userId);
        Membership GetCurrentMembership(int userId);
        void DeleteMembershipPackage(Membership membership);
    }
}
