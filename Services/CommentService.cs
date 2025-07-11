using BusinessObjects;
using Repositories;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void AddComment(Comment comment)
        {
            _commentRepository.AddComment(comment);
        }

        public void DeleteComment(int commentId)
        {
            _commentRepository.DeleteComment(commentId);
        }
    }
}
