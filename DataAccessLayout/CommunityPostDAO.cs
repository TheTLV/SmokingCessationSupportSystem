using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayout
{
    public static class CommunityPostDAO
    {
        public static List<CommunityPost> GetAllPosts()
        {
            using var db = new AppDbContext();
            return db.CommunityPosts
                .Include(p => p.User) // Bao gồm thông tin người dùng
                .Include(p => p.Comments) // Bao gồm các bình luận
                .ThenInclude(c => c.User) // Bao gồm cả thông tin người dùng của comment
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
