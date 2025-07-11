using BusinessObjects;

namespace WPFApp
{
    public static class AppSession
    {
        public static User CurrentUser { get; set; }
        public static Views.CoachListView CurrentCoachListView { get; set; } // Thêm property để quản lý cửa sổ CoachList hiện tại
    }
}
