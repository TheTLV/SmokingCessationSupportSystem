using BusinessObjects;

namespace DataAccessLayout
{
    public static class CoachDAO
    {
        public static List<Coach> GetAllCoach()
        {
            using var db = new AppDbContext();
            return db.Coaches.ToList();
        }
    }
}
