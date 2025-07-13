using BusinessObjects;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class FeedbackViewModel : ViewModelBase
    {
        private readonly IFeedbackService _feedbackService;

        private int _rating;
        private string _feedbackContent;
        private string _selectedFeedbackType;

        public ObservableCollection<string> FeedbackTypes { get; } = new ObservableCollection<string>
        {
            "Góp ý tính năng",
            "Báo cáo lỗi",
            "Đánh giá trải nghiệm",
            "Coaching",
            "Khác"
        };

        public string SelectedFeedbackType
        {
            get => _selectedFeedbackType;
            set => Set(ref _selectedFeedbackType, value);
        }

        public int Rating
        {
            get => _rating;
            set => Set(ref _rating, value);
        }

        public string FeedbackContent
        {
            get => _feedbackContent;
            set => Set(ref _feedbackContent, value);
        }

        public ICommand SetRatingCommand { get; }
        public ICommand SubmitFeedbackCommand { get; }

        public FeedbackViewModel()
        {
            _feedbackService = new FeedbackService();

            SetRatingCommand = new RelayCommand<string>(SetRating);
            SubmitFeedbackCommand = new RelayCommand(SubmitFeedback);
        }

        private void SetRating(string rating)
        {
            if (int.TryParse(rating, out int selectedRating))
            {
                Rating = selectedRating;
            }
        }

        private void SubmitFeedback()
        {
            if (Rating == 0)
            {
                MessageBox.Show("Vui lòng chọn mức độ đánh giá", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(FeedbackContent))
            {
                MessageBox.Show("Vui lòng nhập nội dung phản hồi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var feedbackData = new Feedback
            {
                UserId = AppSession.CurrentUser.Id,
                Content = string.IsNullOrWhiteSpace(SelectedFeedbackType) ? FeedbackContent : $"{SelectedFeedbackType}: {FeedbackContent}",
                Rating = Rating,
                CreatedAt = DateTime.Now
            };

            _feedbackService.AddFeedback(feedbackData);
            MessageBox.Show("Cảm ơn bạn đã gửi phản hồi!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

            Rating = 0;
            FeedbackContent = string.Empty;
            SelectedFeedbackType = null;
        }
    }
}