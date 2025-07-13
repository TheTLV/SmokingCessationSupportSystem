using BusinessObjects;
using Repositories;

namespace Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService()
        {
            _feedbackRepository = new FeedbackRepository();
        }

        public void AddFeedback(Feedback feedback)
        {
            _feedbackRepository.AddFeedback(feedback);
        }

        public void DeleteFeedback(int feedbackId)
        {
            _feedbackRepository.DeleteFeedback(feedbackId);
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return _feedbackRepository.GetAllFeedbacks();
        }
    }
}
