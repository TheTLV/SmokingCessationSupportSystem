using BusinessObjects;

namespace Repositories
{
    public interface IQuitPlanRepository
    {
        bool AddQuitPlan(QuitPlan quitPlan);
        QuitPlan? GetCurrentQuitPlanById(int userId);
        List<QuitPlan> GetAllQuitPlansByUserId(int userId);
    }
}
