using StudentManager.Models;
using System.Collections.ObjectModel;
using StudentManager.Utils;
using StudentManager.Pages;
using StudentManager.Pages.Students;

namespace StudentManager
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<LopHoc> DsLopHoc { get; set; }

        private ObservableCollection<LopHoc> dsGoc;
        private ObservableCollection<LopHoc> dsChon;
        private bool isInDeleteMode = false;
        private bool isInEditMode = false;

        public MainPage()
        {
            InitializeComponent();
            DsLopHoc = new ObservableCollection<LopHoc>();
            dsChon = new ObservableCollection<LopHoc>();
            dsGoc = new ObservableCollection<LopHoc>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await loadData();
        }

        private async Task loadData()
        {
            await Database.pickDatabasePathIfNotExist();

            dsChon.Clear();
            dsGoc.Clear();
            DsLopHoc.Clear();
            foreach (var i in await Database.getClasses())
            {
                DsLopHoc.Add(i);
                dsGoc.Add(i);
            }
        }

        private void addBtn_Clicked(object sender, EventArgs e)
        {
            if (isInEditMode)
            {
                DisplayAlert("Thông báo", "Vui lòng thoát chế độ chỉnh sửa trước khi thêm", "OK");
                return;
            }

            if (isInDeleteMode)
            {
                DisplayAlert("Thông báo", "Vui lòng thoát chế độ xoá trước khi thêm", "OK");
                return;
            }

            Navigation.PushAsync(new AddClassPage());
        }

        private async void deleteBtn_Clicked(object sender, EventArgs e)
        {
            if (isInEditMode)
            {
                await DisplayAlert("Thông báo", "Vui lòng thoát chế độ chỉnh sửa trước khi xóa", "OK");
                return;
            }

            if (!isInDeleteMode)
            {
                isInDeleteMode = true;
                deleteBtn.Text = "Xoá / Thoát xoá";
                classLv.SelectionMode = SelectionMode.Multiple;
                return;
            }

            if (dsChon.Count > 0)
            {
                if (await DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xoá " + dsChon.Count + " lớp học không?", "Có", "Không"))
                {
                    Database.deleteClasses(dsChon);
                    dsChon.Clear();
                    await loadData();
                }
            }

            isInDeleteMode = false;
            deleteBtn.Text = "Chọn Xoá";
            classLv.SelectionMode = SelectionMode.Single;
        }

        private void classLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dsChon.Clear();
            foreach (var i in e.CurrentSelection)
            {
                dsChon.Add((LopHoc)i);
            }

            if (isInEditMode && dsChon.Count > 0)
            {
                Navigation.PushAsync(new EditClassPage(dsChon.First()));
            }

            if (!isInDeleteMode && !isInEditMode && dsChon.Count > 0)
            {
                Navigation.PushAsync(new MainStudentPage(dsChon.First().MaLop));
            }
        }

        private void searchBtn_Clicked(object sender, EventArgs e)
        {
            string searchText = searchEntry.Text;

            if (string.IsNullOrWhiteSpace(searchText)) return;

            DsLopHoc.Clear();
            foreach (var i in dsGoc)
            {
                if (i.MaLop.Contains(searchText) || i.TenLop.Contains(searchText))
                {
                    DsLopHoc.Add(i);
                }
            }
        }

        private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                DsLopHoc.Clear();
                foreach (var i in dsGoc)
                {
                    DsLopHoc.Add(i);
                }
            }
        }

        private void editBtn_Clicked(object sender, EventArgs e)
        {
            if (isInDeleteMode)
            {
                DisplayAlert("Thông báo", "Vui lòng thoát chế độ xoá trước khi chỉnh sửa", "OK");
                return;
            }

            if (!isInEditMode)
            {
                isInEditMode = true;
                editBtn.Text = "Thoát sửa";
            }
            else
            {
                isInEditMode = false;
                editBtn.Text = "Sửa";
            }
        }
    }
}
