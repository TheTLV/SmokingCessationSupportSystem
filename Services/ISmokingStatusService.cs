using BusinessObjects;

namespace Services
{
    public interface ISmokingStatusService
    {
        bool AddSmokingStatus(int userId, int cigarettesPerDay, decimal costPerPack, int packsPerWeek, DateTime recordDate);
        List<SmokingStatus> GetSmokingStatusesByUserId(int userId);
    }
}
