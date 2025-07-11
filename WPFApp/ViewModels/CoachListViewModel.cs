using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class CoachListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CoachViewModel> Coaches { get; }

        private CoachViewModel _selectedCoach;
        public CoachViewModel SelectedCoach
        {
            get => _selectedCoach;
            set
            {
                if (_selectedCoach != value)
                {
                    _selectedCoach = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SelectCoachCommand { get; set; } // Chỉ cần property, không khởi tạo ở đây
        public Action<CoachViewModel> OpenChatAction { get; set; }

        public CoachListViewModel()
        {
            Coaches = new ObservableCollection<CoachViewModel>();
            SelectCoachCommand = new RelayCommand(SelectCoach);
            LoadCoaches();
        }

        private void SelectCoach(object parameter)
        {
            if (parameter is CoachViewModel coach)
            {
                SelectedCoach = coach;
                OpenChatAction?.Invoke(coach);
            }
        }

        private void LoadCoaches()
        {
            var coach1 = new CoachViewModel();
            coach1.Name = "John Doe";
            coach1.Specialization = "Behavioral Therapy";
            coach1.ExperienceYears = 5;
            Coaches.Add(coach1);

            var coach2 = new CoachViewModel();
            coach2.Name = "Jane Smith";
            coach2.Specialization = "Cognitive Behavioral Therapy";
            coach2.ExperienceYears = 8;
            Coaches.Add(coach2);

            var coach3 = new CoachViewModel();
            coach3.Name = "Alice Johnson";
            coach3.Specialization = "Motivational Interviewing";
            coach3.ExperienceYears = 6;
            Coaches.Add(coach3);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}