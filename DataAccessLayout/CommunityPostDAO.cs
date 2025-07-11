using BusinessObjects;

namespace DataAccessLayout
{
    public static class CommunityPostDAO
    {
        public static List<CommunityPost> GetAllPosts()
        {
            using var db = new AppDbContext();
            return db.CommunityPosts
                .ToList();
        }

        public static void AddPost(CommunityPost post)
        {
            using var db = new AppDbContext();
            db.CommunityPosts.Add(post);
            db.SaveChanges();
        }

        public static void DeletePost(int postId)
        {
            using var db = new AppDbContext();
            var post = db.CommunityPosts.FirstOrDefault(p => p.Id == postId);
            if (post != null)
            {
                db.Comments.RemoveRange(post.Comments); // Xóa comment trước nếu có
                db.CommunityPosts.Remove(post);
                db.SaveChanges();
            }
        }
    }
}
