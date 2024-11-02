using StudentManager.Models;
using StudentManager.Utils;

namespace StudentManager.Pages.Students;

public partial class EditStudentPage : ContentPage
{
	public EditStudentPage(HocSinh studentNeedEdit)
	{
		InitializeComponent();
		BindingContext = studentNeedEdit;
    }

    private void editBtn_Clicked(object sender, EventArgs e)
    {
        string hoTen = hoTenEntry.Text;
        string ngaySinh = ngaySinhEntry.Text;

        if (string.IsNullOrWhiteSpace(hoTen) || string.IsNullOrWhiteSpace(ngaySinh))
        {
            DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
            return;
        }

        Database.updateStudent(new HocSinh
        {
            Mssv = ((HocSinh)BindingContext).Mssv,
            HoTen = hoTen,
            NgaySinh = ngaySinh,
            MaLop = ((HocSinh)BindingContext).MaLop
        });

        Navigation.PopAsync();
    }

    private void cancelBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}