using BusinessObjects;

namespace Services
{
    public interface IQuitPlanService
    {
        bool AddQuitPlan(QuitPlan quitPlan);
        QuitPlan? GetCurrentQuitPlanById(int userId);
        List<QuitPlan> GetAllQuitPlansByUserId(int userId);
    }
}
