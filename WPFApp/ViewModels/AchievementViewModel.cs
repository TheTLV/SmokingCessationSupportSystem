using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows;
using DataAccessLayout;
using BusinessObjects;

namespace WPFApp.ViewModels
{
    public class AchievementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AchievementSystem> Achievements { get; set; }
        public ICommand ShareAchievementCommand { get; }

        public AchievementViewModel()
        {
            Achievements = new ObservableCollection<AchievementSystem>();
            LoadAchievements();
            ShareAchievementCommand = new RelayCommand(ShareAchievement);
        }

        private void LoadAchievements()
        {
            var listAchievements = new List<AchievementSystem>
                {
                new AchievementSystem { Title = "First Login", Description = "Logged in for the first time.", Target = 0 },
                new AchievementSystem { Title = "Khởi đầu hành trình", Description = "Hoàn thành ngày đầu tiên không hút thuốc", Target = 1 },
                new AchievementSystem { Title = "Tuần đầu tiên", Description = "7 ngày liên tiếp không hút thuốc", Target = 7 },
                new AchievementSystem { Title = "Vượt qua thử thách", Description = "10 ngày không hút thuốc", Target = 10 },
                new AchievementSystem { Title = "Nửa tháng kiên cường", Description = "15 ngày không hút thuốc", Target = 15 },
                new AchievementSystem { Title = "Chặng đường 1 tháng", Description = "30 ngày liên tiếp không hút thuốc", Target = 30 },
                new AchievementSystem { Title = "Vượt cột mốc vàng", Description = "50 ngày không hút thuốc", Target = 50 },
                new AchievementSystem { Title = "Hành trình 100 ngày", Description = "100 ngày không hút thuốc", Target = 100 },
                new AchievementSystem { Title = "Nửa năm bền bỉ", Description = "6 tháng không hút thuốc (khoảng 180 ngày)", Target = 180 },
                new AchievementSystem { Title = "Cai thuốc thành công", Description = "1 năm không hút thuốc (365 ngày)", Target = 365 }
            };
            Achievements.Clear();
            foreach (var achievement in listAchievements)
            {
                Achievements.Add(achievement);
            }
        }

        public void UpdateAchievements(int daysNoSmoking, List<DateTime> sortedDays)
        {
            foreach (var achievement in Achievements)
            {
                achievement.Progress = Math.Min(daysNoSmoking, achievement.Target);
                achievement.ProgressText = $"Đã đạt {achievement.Progress}/{achievement.Target} ngày";
                if (daysNoSmoking >= achievement.Target && achievement.Target > 0)
                {
                    achievement.IsUnlocked = true;
                    // Ngày đạt là ngày thứ Target-1 trong sortedDays
                    achievement.UnlockDate = sortedDays[achievement.Target - 1];
                }
                else if (achievement.Target == 0)
                {
                    achievement.IsUnlocked = true;
                    achievement.UnlockDate = sortedDays.Count > 0 ? sortedDays[0] : (DateTime?)null;
                }
                else
                {
                    achievement.IsUnlocked = false;
                    achievement.UnlockDate = null;
                }
                achievement.OnPropertyChanged(nameof(achievement.IsUnlocked));
                achievement.OnPropertyChanged(nameof(achievement.UnlockDate));
                achievement.OnPropertyChanged(nameof(achievement.Progress));
                achievement.OnPropertyChanged(nameof(achievement.ProgressText));
            }
        }

        private void ShareAchievement(object parameter)
        {
            if (parameter is AchievementSystem achievement && achievement.IsUnlocked)
            {
                var post = new CommunityPost
                {
                    UserId = AppSession.CurrentUser.Id,
                    Content = $"Tôi vừa đạt thành tựu: \"{achievement.Title}\"! {achievement.Description} 🎉",
                    CreatedAt = DateTime.Now
                };
                CommunityPostDAO.AddPost(post);
                MessageBox.Show("Đã chia sẻ thành tựu lên cộng đồng!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Bạn chỉ có thể chia sẻ các thành tựu đã đạt!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class AchievementSystem : INotifyPropertyChanged
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Target { get; set; } // Số ngày cần để đạt
        private bool _isUnlocked;
        public bool IsUnlocked
        {
            get => _isUnlocked;
            set { _isUnlocked = value; OnPropertyChanged(nameof(IsUnlocked)); }
        }
        private DateTime? _unlockDate;
        public DateTime? UnlockDate
        {
            get => _unlockDate;
            set { _unlockDate = value; OnPropertyChanged(nameof(UnlockDate)); }
        }
        private int _progress;
        public int Progress
        {
            get => _progress;
            set { _progress = value; OnPropertyChanged(nameof(Progress)); }
        }
        private string _progressText;
        public string ProgressText
        {
            get => _progressText;
            set { _progressText = value; OnPropertyChanged(nameof(ProgressText)); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}