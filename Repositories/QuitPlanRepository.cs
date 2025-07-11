using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class QuitPlanRepository : IQuitPlanRepository
    {
        public bool AddQuitPlan(QuitPlan quitPlan)
        {
            return QuitPlanDAO.AddQuitPlan(quitPlan);
        }

        public QuitPlan? GetCurrentQuitPlanById(int userId)
        {
            return QuitPlanDAO.GetCurrentQuitPlanById(userId);
        }
    }
}
