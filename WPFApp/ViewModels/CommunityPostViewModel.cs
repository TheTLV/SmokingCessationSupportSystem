using BusinessObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class CommunityPostViewModel : INotifyPropertyChanged
    {
        public CommunityPost Post { get; }
        public ObservableCollection<CommentViewModel> Comments { get; set; }
        private string _newCommentContent;
        public string NewCommentContent
        {
            get => _newCommentContent;
            set
            {
                _newCommentContent = value;
                OnPropertyChanged(nameof(NewCommentContent));
            }
        }

        public ICommand AddCommentCommand { get; }
        public ICommand ShowPostMenuCommand { get; }
        public ICommand DeletePostCommand { get; }
        public ICommand DeleteCommentCommand { get; }
        public Action<CommunityPostViewModel> DeletePostAction { get; set; }

        private readonly Action<Comment> _deleteCommentAction;

        public CommunityPostViewModel(CommunityPost post, Action<CommunityPostViewModel, string> addCommentAction, Action<Comment> deleteCommentAction)
        {
            Post = post;
            Comments = new ObservableCollection<CommentViewModel>((post.Comments ?? new List<Comment>()).Select(c => new CommentViewModel(c, deleteCommentAction)));
            AddCommentCommand = new RelayCommand(_ => addCommentAction(this, NewCommentContent));
            ShowPostMenuCommand = new RelayCommand(_ => OnShowPostMenu());
            DeletePostCommand = new RelayCommand(_ => OnDeletePost());
            _deleteCommentAction = deleteCommentAction;
        }

        public string AuthorName => Post.User?.FullName ?? "Ẩn danh";
        public DateTime CreatedAt => Post.CreatedAt;
        public string Content => Post.Content;
        public bool IsMyPost => Post.UserId == AppSession.CurrentUser.Id;

        private void OnShowPostMenu()
        {
            // Không cần xử lý gì, ContextMenu sẽ tự hiện
        }

        private void OnDeletePost()
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa bài viết này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                DeletePostAction?.Invoke(this);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CommentViewModel : INotifyPropertyChanged
    {
        public Comment Comment { get; }
        public ICommand DeleteCommentCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Content => Comment.Content;
        public string AuthorName => Comment.User?.FullName ?? "Ẩn danh";
        public DateTime CreatedAt => Comment.CreatedAt;
        public int UserId => Comment.UserId;
        public CommentViewModel(Comment comment, Action<Comment> deleteCommentAction)
        {
            Comment = comment;
            DeleteCommentCommand = new RelayCommand(_ => deleteCommentAction?.Invoke(Comment));
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}