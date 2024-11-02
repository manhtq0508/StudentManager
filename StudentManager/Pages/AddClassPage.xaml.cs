using StudentManager.Data;
using StudentManager.Models;
using StudentManager.Utils;

namespace StudentManager.Pages;

public partial class AddClassPage : ContentPage
{
	public AddClassPage()
	{
		InitializeComponent();
	}

    private void addBtn_Clicked(object sender, EventArgs e)
    {
        string maLop = maLopEntry.Text;
        string tenLop = tenLopEntry.Text;

        if (string.IsNullOrWhiteSpace(maLop) || string.IsNullOrWhiteSpace(tenLop))
        {
            DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
            return;
        }

        Database.addClass(new LopHoc
        {
            MaLop = maLop,
            TenLop = tenLop
        });

        Navigation.PopAsync();
    }

    private void cancelBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}