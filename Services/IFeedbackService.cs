using BusinessObjects;

namespace Services
{
    public interface IFeedbackService
    {
        void AddFeedback(Feedback feedback);
        List<Feedback> GetAllFeedbacks();
        void DeleteFeedback(int feedbackId);
    }
}
