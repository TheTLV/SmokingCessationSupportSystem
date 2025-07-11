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
        public ObservableCollection<Comment> Comments { get; set; }
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
        public Action<CommunityPostViewModel> DeletePostAction { get; set; }

        public CommunityPostViewModel(CommunityPost post, Action<CommunityPostViewModel, string> addCommentAction)
        {
            Post = post;
            Comments = new ObservableCollection<Comment>(post.Comments ?? new List<Comment>());
            AddCommentCommand = new RelayCommand(_ => addCommentAction(this, NewCommentContent));
            ShowPostMenuCommand = new RelayCommand(_ => OnShowPostMenu());
            DeletePostCommand = new RelayCommand(_ => OnDeletePost());
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
}