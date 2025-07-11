using BusinessObjects;

namespace Services
{
    public interface ICommentService
    {
        void AddComment(Comment comment);
        void DeleteComment(int commentId);
    }
}
