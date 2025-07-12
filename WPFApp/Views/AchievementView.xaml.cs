using Services;
using System.Windows;
using WPFApp.ViewModels;

namespace WPFApp.Views
{
    public partial class AchievementView : Window
    {
        private readonly IQuitPlanService quitPlanService;

        public AchievementView()
        {
            InitializeComponent();
            quitPlanService = new QuitPlanService();
            var viewModel = new AchievementViewModel();
            DataContext = viewModel;

            // Lấy số ngày không hút thuốc từ tất cả QuitPlan, bỏ qua kế hoạch tương lai, không trùng ngày
            var allPlans = quitPlanService.GetAllQuitPlansByUserId(AppSession.CurrentUser.Id);
            var noSmokingDays = new HashSet<DateTime>();
            foreach (var plan in allPlans)
            {
                if (plan.StartDate > System.DateTime.Today)
                    continue;
                var start = plan.StartDate;
                var end = plan.TargetDate < System.DateTime.Today ? plan.TargetDate : System.DateTime.Today;
                for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
                {
                    noSmokingDays.Add(date);
                }
            }
            var sortedDays = noSmokingDays.OrderBy(d => d).ToList();
            int daysNoSmoking = sortedDays.Count;
            viewModel.UpdateAchievements(daysNoSmoking, sortedDays);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboardView = new DashboardView();
            dashboardView.Show();
            this.Close();
        }
    }
}
