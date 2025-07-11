using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class SmokingStatusRepository : ISmokingStatusRepository
    {
        public bool AddSmokingStatus(int userId, int cigarettesPerDay, decimal costPerPack, int packsPerWeek, DateTime recordDate)
        {
            return SmokingStatusDAO.AddSmokingStatus(userId, cigarettesPerDay, costPerPack, packsPerWeek, recordDate);
        }

        public List<SmokingStatus> GetSmokingStatusesByUserId(int userId)
        {
            return SmokingStatusDAO.GetSmokingStatusesByUserId(userId);
        }
    }
}
