using BusinessObjects;

namespace Services
{
    public interface IMembershipService
    {
        bool AddMembershipPackage(Membership package);
        bool IsExistingPackage(int userId);
        Membership GetCurrentMembership(int userId);
        void DeleteMembershipPackage(Membership membership);
    }
}
