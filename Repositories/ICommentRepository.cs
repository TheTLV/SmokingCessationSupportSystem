using BusinessObjects;

namespace Repositories
{
    public interface ICommentRepository
    {
        void AddComment(Comment comment);
        void DeleteComment(int commentId);
    }
}
