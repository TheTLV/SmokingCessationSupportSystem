using BusinessObjects;

namespace Repositories
{
    public interface IFeedbackRepository
    {
        void AddFeedback(Feedback feedback);
        List<Feedback> GetAllFeedbacks();
        void DeleteFeedback(int feedbackId);
    }
}
