using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFApp.ViewModels
{
    public class CoachViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _specialization;
        private int _experienceYears;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Specialization
        {
            get => _specialization;
            set
            {
                _specialization = value;
                OnPropertyChanged();
            }
        }

        public int ExperienceYears
        {
            get => _experienceYears;
            set
            {
                _experienceYears = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CoachViewModel()
        {
            _name = string.Empty;
            _specialization = "Chưa cập nhật";
            _experienceYears = 0;
        }
    }
}