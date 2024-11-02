using StudentManager.Models;
using StudentManager.Utils;

namespace StudentManager.Pages;

public partial class EditClassPage : ContentPage
{
	public EditClassPage(LopHoc dataNeedEdit)
	{
		InitializeComponent();
		BindingContext = dataNeedEdit;
    }

    private void cancelBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void editBtn_Clicked(object sender, EventArgs e)
    {
        string tenLop = tenLopEntry.Text;

        if (string.IsNullOrWhiteSpace(tenLop))
        {
            DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
            return;
        }

        Database.updateClass(new LopHoc
        {
            MaLop = ((LopHoc)BindingContext).MaLop,
            TenLop = tenLop
        });
        Navigation.PopAsync();
    }
}