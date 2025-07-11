using BusinessObjects;
using Repositories;

namespace Services
{
    public class QuitPlanService : IQuitPlanService
    {
        private readonly IQuitPlanRepository iQuitPlanRepository;
        public QuitPlanService()
        {
            iQuitPlanRepository = new QuitPlanRepository();
        }
        public bool AddQuitPlan(QuitPlan quitPlan)
        {
            return iQuitPlanRepository.AddQuitPlan(quitPlan);
        }

        public QuitPlan? GetCurrentQuitPlanById(int userId)
        {
            return iQuitPlanRepository.GetCurrentQuitPlanById(userId);
        }
    }
}
