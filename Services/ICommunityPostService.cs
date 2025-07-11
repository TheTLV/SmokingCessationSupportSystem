using BusinessObjects;

namespace Services
{
    public interface ICommunityPostService
    {
        List<CommunityPost> GetAllPosts();
        void AddPost(CommunityPost post);
    }
}
