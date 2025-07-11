using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WPFApp.ViewModels
{
    public class AchievementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AchievementSystem> Achievements { get; set; }

        public AchievementViewModel()
        {
            Achievements = new ObservableCollection<AchievementSystem>();
            LoadAchievements();
        }

        private void LoadAchievements()
        {
            var listAchievements = new List<AchievementSystem>
                {
                new AchievementSystem { Title = "First Login", Description = "Logged in for the first time." },
                new AchievementSystem { Title = "Profile Completed", Description = "Completed profile setup." },
                new AchievementSystem { Title = "10 Days Active", Description = "Logged in for 10 consecutive days." }
            };
            Achievements.Clear();
            foreach (var achievement in listAchievements)
            {
                Achievements.Add(achievement);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public class AchievementSystem
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}