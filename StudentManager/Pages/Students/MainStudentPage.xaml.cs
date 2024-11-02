using StudentManager.Models;
using StudentManager.Utils;
using System.Collections.ObjectModel;

namespace StudentManager.Pages.Students;

public partial class MainStudentPage : ContentPage
{
	public ObservableCollection<HocSinh> DsHocSinh { get; set; }

	private ObservableCollection<HocSinh> dsGoc;
    private ObservableCollection<HocSinh> dsChon;
    private bool isInDeleteMode = false;
    private bool isInEditMode = false;
    private string maLop;

    public MainStudentPage(string maLop)
	{
		InitializeComponent();
        DsHocSinh = new ObservableCollection<HocSinh>();
        dsChon = new ObservableCollection<HocSinh>();
        dsGoc = new ObservableCollection<HocSinh>();
        this.maLop = maLop;
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await loadData();
    }

    private async Task loadData()
    {
        dsChon.Clear();
        dsGoc.Clear();
        DsHocSinh.Clear();
        foreach (var i in await Database.getStudents(maLop))
        {
            DsHocSinh.Add(i);
            dsGoc.Add(i);
        }
    }

    private void searchBtn_Clicked(object sender, EventArgs e)
    {
        string searchText = searchEntry.Text;

        if (string.IsNullOrWhiteSpace(searchText)) return;

        DsHocSinh.Clear();
        foreach (var i in dsGoc)
        {
            if (i.Mssv.Contains(searchText) || i.HoTen.Contains(searchText) || i.NgaySinh.Contains(searchText))
            {
                DsHocSinh.Add(i);
            }
        }
    }

    private void searchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            DsHocSinh.Clear();
            foreach (var i in dsGoc)
            {
                DsHocSinh.Add(i);
            }
        }
    }

    private void addBtn_Clicked(object sender, EventArgs e)
    {
        if (isInDeleteMode)
        {
            DisplayAlert("Thông báo", "Vui lòng thoát chế độ xoá trước khi thêm", "OK");
            return;
        }

        if (isInEditMode)
        {
            DisplayAlert("Thông báo", "Vui lòng thoát chế độ chỉnh sửa trước khi thêm", "OK");
            return;
        }

        Navigation.PushAsync(new AddStudentPage(maLop));
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
            studentLv.SelectionMode = SelectionMode.Multiple;
            return;
        }

        if (dsChon.Count > 0)
        {
            if (await DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn xoá " + dsChon.Count + " học sinh không?", "Có", "Không"))
            {
                Database.deleteStudents(dsChon);
                dsChon.Clear();
                await loadData();
            }
        }

        isInDeleteMode = false;
        deleteBtn.Text = "Chọn Xoá";
        studentLv.SelectionMode = SelectionMode.Single;
    }

    private void studentLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        dsChon.Clear();
        foreach (var i in e.CurrentSelection)
        {
            dsChon.Add((HocSinh)i);
        }

        if (isInEditMode && dsChon.Count > 0)
        {
            Navigation.PushAsync(new EditStudentPage(dsChon.First()));
        }
    }
}