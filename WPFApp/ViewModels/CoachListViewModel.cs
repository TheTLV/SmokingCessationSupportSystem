using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace WPFApp.ViewModels
{
    public class CoachListViewModel : INotifyPropertyChanged
    {
        private readonly ICoachService iCoachService;
        public ObservableCollection<Coach> Coaches { get; }

        private Coach _selectedCoach;
        public Coach SelectedCoach
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

        public ICommand SelectCoachCommand { get; set; }
        public Action<Coach> OpenChatAction { get; set; }

        public CoachListViewModel()
        {
            Coaches = new ObservableCollection<Coach>();
            SelectCoachCommand = new RelayCommand(SelectCoach);
            iCoachService = new CoachService();

            LoadCoaches();
        }

        private void SelectCoach(object parameter)
        {
            if (parameter is Coach coach)
            {
                SelectedCoach = coach;
                OpenChatAction?.Invoke(coach);
            }
        }

        private void LoadCoaches()
        {
            var coaches = iCoachService.GetAllCoaches();
            Coaches.Clear();
            foreach (var coach in coaches)
            {
                Coaches.Add(coach);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}