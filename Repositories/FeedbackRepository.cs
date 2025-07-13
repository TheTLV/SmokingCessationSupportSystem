using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public void AddFeedback(Feedback feedback)
        {
            FeedbackDAO.AddFeedback(feedback);
        }

        public void DeleteFeedback(int feedbackId)
        {
            FeedbackDAO.DeleteFeedback(feedbackId);
        }

        public List<Feedback> GetAllFeedbacks()
        {
            return FeedbackDAO.GetAllFeedbacks();
        }
    }
}
