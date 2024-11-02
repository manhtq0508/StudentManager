using StudentManager.Models;
using StudentManager.Utils;

namespace StudentManager.Pages.Students;

public partial class AddStudentPage : ContentPage
{
    private string maLop;
    public AddStudentPage(string maLop)
	{
		InitializeComponent();
        this.maLop = maLop;
    }

    private void addBtn_Clicked(object sender, EventArgs e)
    {
        string mssv = mssvEntry.Text;
        string hoTen = hoTenEntry.Text;
        string ngaySinh = ngaySinhEntry.Text;

        if (string.IsNullOrWhiteSpace(mssv) || string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(ngaySinh))
        {
            DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
            return;
        }

        Database.addStudent(new HocSinh
        {
            Mssv = mssv,
            HoTen = hoTen,
            NgaySinh = ngaySinh,
            MaLop = maLop
        });

        Navigation.PopAsync();
    }

    private void cancelBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}