using BusinessObjects;

namespace Repositories
{
    public interface ISmokingStatusRepository
    {
        bool AddSmokingStatus(int userId, int cigarettesPerDay, decimal costPerPack, int packsPerWeek, DateTime recordDate);
        List<SmokingStatus> GetSmokingStatusesByUserId(int userId);
    }
}
