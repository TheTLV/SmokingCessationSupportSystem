using BusinessObjects;
using Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public class QuitPlanViewModel : INotifyPropertyChanged
    {
        private readonly IQuitPlanService iQuitPlanService;

        private string _newPlanReason;
        private DateTime _newPlanStartDate = DateTime.Today;
        private DateTime _newPlanTargetDate = DateTime.Today.AddMonths(1);
        private QuitPlan _currentPlan;
        private double _planProgress;
        private bool _hasCurrentPlan;

        public ObservableCollection<QuitPlanStage> StageTemplates { get; set; }
        public ObservableCollection<QuitPlanStage> SelectedStages { get; set; }
        public ObservableCollection<string> CurrentStages { get; set; } = new ObservableCollection<string>();

        public string NewPlanReason
        {
            get => _newPlanReason;
            set
            {
                _newPlanReason = value;
                OnPropertyChanged(nameof(NewPlanReason));
            }
        }

        public DateTime NewPlanStartDate
        {
            get => _newPlanStartDate;
            set
            {
                _newPlanStartDate = value;
                OnPropertyChanged(nameof(NewPlanStartDate));
            }
        }

        public DateTime NewPlanTargetDate
        {
            get => _newPlanTargetDate;
            set
            {
                _newPlanTargetDate = value;
                OnPropertyChanged(nameof(NewPlanTargetDate));
            }
        }

        public QuitPlan CurrentPlan
        {
            get => _currentPlan;
            set
            {
                _currentPlan = value;
                OnPropertyChanged(nameof(CurrentPlan));
                HasCurrentPlan = _currentPlan != null;
                LoadCurrentStages();
            }
        }

        public double PlanProgress
        {
            get => Math.Round(_planProgress, 2);
            set
            {
                _planProgress = value;
                OnPropertyChanged(nameof(PlanProgress));
            }
        }

        public bool HasCurrentPlan
        {
            get => _hasCurrentPlan;
            set
            {
                if (_hasCurrentPlan != value)
                {
                    _hasCurrentPlan = value;
                    OnPropertyChanged(nameof(HasCurrentPlan));
                }
            }
        }

        public ICommand CreatePlanCommand { get; }

        public QuitPlanViewModel()
        {
            StageTemplates = new ObservableCollection<QuitPlanStage>();
            SelectedStages = new ObservableCollection<QuitPlanStage>();
            iQuitPlanService = new QuitPlanService();

            CreatePlanCommand = new RelayCommand(obj => CreateQuitPlan());

            LoadStageTemplates();
            LoadCurrentPlan();
        }

        private void LoadStageTemplates()
        {
            var templates = new[]
            {
                new QuitPlanStage { Name = "Giảm số điếu hút mỗi ngày", Description = "Giảm dần số điếu hút mỗi ngày trong tuần đầu tiên." },
                new QuitPlanStage { Name = "Chỉ hút vào buổi sáng", Description = "Chỉ hút thuốc vào buổi sáng trong tuần thứ hai." },
                new QuitPlanStage { Name = "Không hút thuốc trong 1 ngày", Description = "Cố gắng không hút thuốc trong một ngày bất kỳ." },
                new QuitPlanStage { Name = "Không hút thuốc trong 3 ngày", Description = "Cố gắng không hút thuốc trong ba ngày liên tiếp." },
                new QuitPlanStage { Name = "Không hút thuốc trong 1 tuần", Description = "Cố gắng không hút thuốc trong một tuần." },
                new QuitPlanStage { Name = "Không hút thuốc trong 1 tháng", Description = "Cố gắng không hút thuốc trong một tháng." },
                new QuitPlanStage { Name = "Tập thể dục thay thế", Description = "Thay thế thời gian hút thuốc bằng các hoạt động thể dục." },
                new QuitPlanStage { Name = "Tìm hiểu về tác hại thuốc lá", Description = "Đọc và tìm hiểu về tác hại của thuốc lá đối với sức khỏe." }
            };
            StageTemplates.Clear();
            foreach (var template in templates)
            {
                StageTemplates.Add(template);
                template.PropertyChanged += Stage_PropertyChanged;
            }
            UpdateSelectedStages();
        }

        private void Stage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(QuitPlanStage.IsSelected))
            {
                UpdateSelectedStages();
            }
        }

        private void UpdateSelectedStages()
        {
            SelectedStages.Clear();
            foreach (var stage in StageTemplates.Where(s => s.IsSelected))
            {
                SelectedStages.Add(stage);
            }
        }

        private void LoadCurrentPlan()
        {
            var totalDays = 0;
            var completedDays = 0;
            PlanProgress = 0;
            CurrentPlan = iQuitPlanService.GetCurrentQuitPlanById(AppSession.CurrentUser.Id);
            HasCurrentPlan = CurrentPlan != null;
            if (CurrentPlan != null)
            {
                totalDays = (CurrentPlan.TargetDate - CurrentPlan.StartDate).Days;
                completedDays = (DateTime.Today - CurrentPlan.StartDate).Days;
                PlanProgress = Math.Min(100, Math.Max(0, (double)completedDays / totalDays * 100));
            }
        }

        private void LoadCurrentStages()
        {
            CurrentStages.Clear();
            if (CurrentPlan != null && !string.IsNullOrEmpty(CurrentPlan.Stages))
            {
                var stages = CurrentPlan.Stages.Split(',');
                foreach (var stage in stages)
                {
                    CurrentStages.Add(stage.Trim());
                }
            }
        }

        public void CreateQuitPlan()
        {
            if (string.IsNullOrEmpty(NewPlanReason))
            {
                MessageBox.Show("Vui lòng nhập lý do cai thuốc!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (NewPlanStartDate >= NewPlanTargetDate)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedStages = StageTemplates.Where(s => s.IsSelected).ToList();
            if (!selectedStages.Any())
            {
                MessageBox.Show("Vui lòng chọn ít nhất một giai đoạn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CurrentPlan != null)
            {
                var totalDays = (CurrentPlan.TargetDate - CurrentPlan.StartDate).Days;
                var completedDays = (DateTime.Today - CurrentPlan.StartDate).Days;
                PlanProgress = Math.Min(100, Math.Max(0, (double)completedDays / totalDays * 100));
                if (PlanProgress != 100)
                {
                    MessageBox.Show("Bạn đã có một kế hoạch cai thuốc hiện tại. Vui lòng hoàn thành kế hoạch này trước khi tạo kế hoạch mới.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;

                }
            }

            var newPlan = new QuitPlan
            {
                UserId = AppSession.CurrentUser.Id,
                Reason = NewPlanReason,
                StartDate = NewPlanStartDate,
                TargetDate = NewPlanTargetDate,
                Stages = selectedStages.ToArray().Select(s => s.Name).Aggregate((current, next) => current + ", " + next)
            };

            CurrentPlan = newPlan;

            NewPlanReason = "";
            NewPlanStartDate = DateTime.Today;
            NewPlanTargetDate = DateTime.Today.AddMonths(1);
            PlanProgress = 0;

            foreach (var stage in StageTemplates)
            {
                stage.IsSelected = false;
            }

            if (iQuitPlanService.AddQuitPlan(newPlan))
            {
                MessageBox.Show("Kế hoạch cai thuốc đã được tạo thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Không thể tạo kế hoạch cai thuốc. Vui lòng thử lại sau.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}