using BusinessObjects;

namespace Repositories
{
    public interface ICommunityPostRepository
    {
        List<CommunityPost> GetAllPosts();
        void AddPost(CommunityPost post);
        void DeletePost(int postId);
    }
}
