using BusinessObjects;
using Repositories;

namespace Services
{
    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository communityPostRepository;
        public CommunityPostService()
        {
            communityPostRepository = new CommunityPostRepository();
        }
        public List<CommunityPost> GetAllPosts()
        {
            return communityPostRepository.GetAllPosts();
        }

        public void AddPost(CommunityPost post)
        {
            if (post != null)
            {
                communityPostRepository.AddPost(post);
            }
        }
    }
}
