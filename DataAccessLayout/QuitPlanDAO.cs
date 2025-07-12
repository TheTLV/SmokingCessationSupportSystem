using BusinessObjects;

namespace DataAccessLayout
{
    public static class QuitPlanDAO
    {
        public static bool AddQuitPlan(QuitPlan quitPlan)
        {
            using var db = new AppDbContext();
            db.QuitPlans.Add(quitPlan);
            return db.SaveChanges() > 0;
        }

        public static QuitPlan? GetCurrentQuitPlanById(int userId)
        {
            using var db = new AppDbContext();
            return db.QuitPlans
                .Where(qp => qp.UserId == userId && qp.TargetDate >= DateTime.Today)
                .OrderByDescending(qp => qp.StartDate)
                .FirstOrDefault();
        }

        public static List<QuitPlan> GetAllQuitPlansByUserId(int userId)
        {
            using var db = new AppDbContext();
            return db.QuitPlans
                .Where(qp => qp.UserId == userId)
                .OrderByDescending(qp => qp.StartDate)
                .ToList();
        }
    }
}
