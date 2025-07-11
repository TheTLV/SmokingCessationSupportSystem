using BusinessObjects;
using DataAccessLayout;

namespace Repositories
{
    public class CommunityPostRepository : ICommunityPostRepository
    {
        public List<CommunityPost> GetAllPosts()
        {
            return CommunityPostDAO.GetAllPosts();
        }

        public void AddPost(CommunityPost post)
        {
            CommunityPostDAO.AddPost(post);
        }

        public void DeletePost(int postId)
        {
            var post = CommunityPostDAO.GetAllPosts().FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                CommunityPostDAO.DeletePost(postId);
            }
        }
    }
}
