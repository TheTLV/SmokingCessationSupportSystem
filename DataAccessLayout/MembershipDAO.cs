using BusinessObjects;

namespace DataAccessLayout
{
    public static class MembershipDAO
    {
        public static bool AddMembershipPackage(Membership package)
        {
            using var db = new AppDbContext();
            db.Memberships.Add(package);
            return db.SaveChanges() > 0;
        }

        public static bool IsExistingPackage(int userId)
        {
            using var db = new AppDbContext();
            return db.Memberships.Any(m => m.UserId == userId && m.EndDate > DateTime.Now);
        }

        public static void DeleteMembershipPackage(Membership membership)
        {
            using var db = new AppDbContext();
            var existingMembership = db.Memberships.FirstOrDefault(m => m.Id == membership.Id);
            if (existingMembership != null)
            {
                db.Memberships.Remove(existingMembership);
                db.SaveChanges();
            }
        }

        public static Membership GetCurrentMembership(int userId)
        {
            using var db = new AppDbContext();
            return db.Memberships.FirstOrDefault(m => m.UserId == userId && m.EndDate > DateTime.Now);
        }
    }
}
