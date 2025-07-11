using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class CommunityViewModel : INotifyPropertyChanged
    {
        private readonly ICommunityPostRepository _communityPostRepository;
        private readonly ICommentRepository _commentRepository;
        public ObservableCollection<CommunityPostViewModel> CommunityPosts { get; set; }

        private string _newPostContent;
        public string NewPostContent
        {
            get => _newPostContent;
            set
            {
                _newPostContent = value;
                OnPropertyChanged(nameof(NewPostContent));
            }
        }

        public ICommand CreatePostCommand { get; }
        public ICommand DeleteCommentCommand { get; }

        public CommunityViewModel()
        {
            CommunityPosts = new ObservableCollection<CommunityPostViewModel>();
            _communityPostRepository = new CommunityPostRepository();
            _commentRepository = new CommentRepository();
            CreatePostCommand = new RelayCommand(_ => CreatePost());
            DeleteCommentCommand = new RelayCommand(DeleteComment);
            LoadCommunityPosts();
        }

        private void LoadCommunityPosts()
        {
            var posts = _communityPostRepository.GetAllPosts();
            CommunityPosts.Clear();
            foreach (CommunityPost post in posts)
            {
                var postVM = new CommunityPostViewModel(post, AddComment)
                {
                    DeletePostAction = DeletePost
                };
                CommunityPosts.Add(postVM);
            }
        }

        private void CreatePost()
        {
            if (!string.IsNullOrWhiteSpace(NewPostContent))
            {
                var newPost = new CommunityPost
                {
                    Content = NewPostContent,
                    CreatedAt = DateTime.Now,
                    UserId = AppSession.CurrentUser.Id,
                    Comments = new List<Comment>()
                };
                _communityPostRepository.AddPost(newPost);
                // Lấy lại post vừa tạo từ DB để có navigation property User
                var allPosts = _communityPostRepository.GetAllPosts();
                var createdPost = allPosts.OrderByDescending(p => p.Id).FirstOrDefault(p => p.UserId == newPost.UserId && p.Content == newPost.Content && Math.Abs((p.CreatedAt - newPost.CreatedAt).TotalSeconds) < 5);
                var postVM = new CommunityPostViewModel(createdPost ?? newPost, AddComment)
                {
                    DeletePostAction = DeletePost
                };
                CommunityPosts.Insert(0, postVM);
                NewPostContent = string.Empty;
            }
        }

        private void DeletePost(CommunityPostViewModel postVM)
        {
            CommunityPosts.Remove(postVM);
            _communityPostRepository.DeletePost(postVM.Post.Id);
        }

        private void AddComment(CommunityPostViewModel postVM, string commentContent)
        {
            if (!string.IsNullOrWhiteSpace(commentContent))
            {
                var newComment = new Comment
                {
                    Content = commentContent,
                    CreatedAt = DateTime.Now,
                    UserId = AppSession.CurrentUser.Id,
                    PostId = postVM.Post.Id
                };
                _commentRepository.AddComment(newComment);
                // Lấy lại comment vừa tạo từ DB để có navigation property User
                var allPosts = _communityPostRepository.GetAllPosts();
                var updatedPost = allPosts.FirstOrDefault(p => p.Id == postVM.Post.Id);
                var createdComment = updatedPost?.Comments.OrderByDescending(c => c.Id).FirstOrDefault(c => c.UserId == newComment.UserId && c.Content == newComment.Content && Math.Abs((c.CreatedAt - newComment.CreatedAt).TotalSeconds) < 5);
                postVM.Comments.Add(createdComment ?? newComment);
                postVM.NewCommentContent = string.Empty;
            }
        }

        private void DeleteComment(object parameter)
        {
            if (parameter is Comment comment)
            {
                if (MessageBox.Show("Bạn có chắc muốn xóa bình luận này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var postVM = CommunityPosts.FirstOrDefault(p => p.Comments.Any(c => c.Id == comment.Id));
                    if (postVM != null)
                    {
                        var toRemove = postVM.Comments.FirstOrDefault(c => c.Id == comment.Id);
                        if (toRemove != null)
                        {
                            postVM.Comments.Remove(toRemove);
                            _commentRepository.DeleteComment(comment.Id);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Không nhận diện được comment để xóa!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}