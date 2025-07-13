using BusinessObjects;
using Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public class SmokingStatusViewModel : INotifyPropertyChanged
    {
        private readonly ISmokingStatusRepository _smokingStatusRepository;

        public ObservableCollection<SmokingStatus> HistoryRecords { get; set; }

        private int _cigarettesPerDay;
        private decimal _costPerPack;
        private int _packsPerWeek;
        private DateTime _recordDate = DateTime.Today;

        public int CigarettesPerDay
        {
            get => _cigarettesPerDay;
            set
            {
                _cigarettesPerDay = value;
                OnPropertyChanged(nameof(CigarettesPerDay));
                OnPropertyChanged(nameof(WeeklyCost));
                OnPropertyChanged(nameof(MonthlyCost));
                OnPropertyChanged(nameof(TotalCigarettes));
            }
        }

        public decimal CostPerPack
        {
            get => _costPerPack;
            set
            {
                _costPerPack = value;
                OnPropertyChanged(nameof(CostPerPack));
                OnPropertyChanged(nameof(WeeklyCost));
                OnPropertyChanged(nameof(MonthlyCost));
            }
        }

        public int PacksPerWeek
        {
            get => _packsPerWeek;
            set
            {
                _packsPerWeek = value;
                OnPropertyChanged(nameof(PacksPerWeek));
                OnPropertyChanged(nameof(WeeklyCost));
                OnPropertyChanged(nameof(MonthlyCost));
                OnPropertyChanged(nameof(TotalCigarettes));
            }
        }

        public DateTime RecordDate
        {
            get => _recordDate;
            set
            {
                _recordDate = value;
                OnPropertyChanged(nameof(RecordDate));
            }
        }

        public decimal WeeklyCost => CostPerPack * PacksPerWeek;
        public decimal MonthlyCost => WeeklyCost * 4;
        public int TotalCigarettes => CigarettesPerDay * 7;

        public ICommand SaveCommand { get; }
        public ICommand ExportCommand { get; }
        public ICommand ViewChartCommand { get; }

        public SmokingStatusViewModel()
        {
            _smokingStatusRepository = new SmokingStatusRepository();
            HistoryRecords = new ObservableCollection<SmokingStatus>();

            SaveCommand = new RelayCommand(_ => SaveSmokingStatus(), _ => CanSaveSmokingStatus());
            ExportCommand = new RelayCommand(_ => ExportReport());
            ViewChartCommand = new RelayCommand(_ => ViewChart());

            LoadSmokingHistory();
        }

        private void LoadSmokingHistory()
        {
            try
            {
                var history = _smokingStatusRepository.GetSmokingStatusesByUserId(AppSession.CurrentUser.Id);
                HistoryRecords.Clear();

                // Sắp xếp theo ngày mới nhất trước
                var sortedHistory = history.OrderByDescending(x => x.RecordDate);
                foreach (var record in sortedHistory)
                {
                    HistoryRecords.Add(record);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải lịch sử: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanSaveSmokingStatus()
        {
            return CigarettesPerDay > 0 && CostPerPack > 0 && PacksPerWeek > 0 && RecordDate <= DateTime.Today;
        }

        private void SaveSmokingStatus()
        {
            try
            {
                // Kiểm tra validation
                if (!CanSaveSmokingStatus())
                {
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin nhập liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool result = _smokingStatusRepository.AddSmokingStatus(
                    AppSession.CurrentUser.Id,
                    CigarettesPerDay,
                    CostPerPack,
                    PacksPerWeek,
                    RecordDate
                );

                if (result)
                {
                    MessageBox.Show("Thêm thông tin hút thuốc thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Clear form
                    CigarettesPerDay = 0;
                    CostPerPack = 0;
                    PacksPerWeek = 0;
                    RecordDate = DateTime.Today;

                    // Refresh lịch sử
                    LoadSmokingHistory();
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi khi thêm thông tin hút thuốc. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportReport()
        {
            MessageBox.Show("Tính năng xuất báo cáo sẽ được phát triển sau.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ViewChart()
        {
            MessageBox.Show("Tính năng xem biểu đồ sẽ được phát triển sau.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}