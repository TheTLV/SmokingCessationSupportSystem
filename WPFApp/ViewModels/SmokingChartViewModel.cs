using BusinessObjects;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFApp.ViewModels
{
    public class SmokingChartViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SmokingStatus> _smokingData;
        public ObservableCollection<SmokingStatus> SmokingData
        {
            get => _smokingData;
            set
            {
                _smokingData = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public SmokingChartViewModel(IEnumerable<SmokingStatus> data)
        {
            SmokingData = new ObservableCollection<SmokingStatus>(data);
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N0");
            UpdateChart();
        }

        private void UpdateChart()
        {
            var cigarettesData = SmokingData
                .OrderBy(x => x.RecordDate)
                .Select(x => (double)x.CigarettesPerDay)
                .ToArray();

            var packsData = SmokingData
                .OrderBy(x => x.RecordDate)
                .Select(x => (double)x.PacksPerWeek)
                .ToArray();

            Labels = SmokingData
                .OrderBy(x => x.RecordDate)
                .Select(x => x.RecordDate.ToString("dd/MM"))
                .ToArray();

            SeriesCollection.Clear();
            SeriesCollection.Add(new LineSeries
            {
                Title = "Số điếu/ngày",
                Values = new ChartValues<double>(cigarettesData),
                PointGeometry = DefaultGeometries.Circle,
                PointGeometrySize = 10,
                StrokeThickness = 2,
                Fill = System.Windows.Media.Brushes.LightSkyBlue
            });
            SeriesCollection.Add(new LineSeries
            {
                Title = "Số bao/tuần",
                Values = new ChartValues<double>(packsData),
                PointGeometry = DefaultGeometries.Square,
                PointGeometrySize = 10,
                StrokeThickness = 2,
                Stroke = System.Windows.Media.Brushes.Gold,
                Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromArgb(80, 255, 215, 0))
            });

            OnPropertyChanged(nameof(SeriesCollection));
            OnPropertyChanged(nameof(Labels));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
