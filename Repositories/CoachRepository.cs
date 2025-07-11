using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class CoachRepository : ICoachRepository
    {
        public List<Coach> GetAllCoaches()
        {
            return CoachDAO.GetAllCoach();
        }
    }
}
