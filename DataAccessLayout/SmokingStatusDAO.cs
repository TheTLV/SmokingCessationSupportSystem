using BusinessObjects;

namespace DataAccessLayout
{
    public static class SmokingStatusDAO
    {
        public static bool AddSmokingStatus(int UserId, int CigarettesPerDay, decimal CostPerPack, int PacksPerWeek, DateTime RecordDate)
        {
            using var db = new AppDbContext();
            var smokingStatus = new SmokingStatus
            {
                UserId = UserId,
                CigarettesPerDay = CigarettesPerDay,
                CostPerPack = CostPerPack,
                PacksPerWeek = PacksPerWeek,
                RecordDate = RecordDate
            };
            db.SmokingStatuses.Add(smokingStatus);
            db.SaveChanges();
            return (smokingStatus != null);
        }

        public static List<SmokingStatus> GetSmokingStatusesByUserId(int userId)
        {
            using var db = new AppDbContext();
            return db.SmokingStatuses.Where(s => s.UserId == userId).ToList();
        }
    }
}
