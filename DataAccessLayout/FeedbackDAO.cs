using BusinessObjects;

namespace DataAccessLayout
{
    public static class FeedbackDAO
    {
        public static void AddFeedback(Feedback feedback)
        {
            using var db = new AppDbContext();
            db.Feedbacks.Add(feedback);
            db.SaveChanges();
        }

        public static List<Feedback> GetAllFeedbacks()
        {
            using var db = new AppDbContext();
            return db.Feedbacks.ToList();
        }

        public static void DeleteFeedback(int feedbackId)
        {
            using var db = new AppDbContext();
            var feedback = db.Feedbacks.Find(feedbackId);
            if (feedback != null)
            {
                db.Feedbacks.Remove(feedback);
                db.SaveChanges();
            }
        }
    }
}
