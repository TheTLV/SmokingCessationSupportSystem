using BusinessObjects;
using Repositories;

namespace Services
{
    public class CoachService : ICoachService
    {
        private readonly ICoachRepository _coachRepository;
        public CoachService()
        {
            _coachRepository = new CoachRepository();
        }

        public List<Coach> GetAllCoaches()
        {
            return _coachRepository.GetAllCoaches();
        }
    }
}
