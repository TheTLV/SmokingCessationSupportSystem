using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public void AddComment(Comment comment)
        {
            CommentDAO.AddComment(comment);
        }

        public void DeleteComment(int commentId)
        {
            CommentDAO.DeleteComment(commentId);
        }
    }
}
