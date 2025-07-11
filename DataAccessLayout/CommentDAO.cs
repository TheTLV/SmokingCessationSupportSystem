using BusinessObjects;

namespace DataAccessLayout
{
    public static class CommentDAO
    {
        public static void AddComment(Comment comment)
        {
            using var db = new AppDbContext();
            db.Comments.Add(comment);
            db.SaveChanges();
        }

        public static void DeleteComment(int commentId)
        {
            using var db = new AppDbContext();
            var comment = db.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment != null)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
        }
    }
}
