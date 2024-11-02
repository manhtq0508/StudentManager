using Microsoft.EntityFrameworkCore;
using StudentManager.Data;
using StudentManager.Models;
using System.Collections.ObjectModel;

namespace StudentManager.Utils
{
    internal class Database
    {
        public Database() { }
        public static async Task pickDatabasePathIfNotExist()
        {
            if (!string.IsNullOrEmpty(Preferences.Get("DATABASE_PATH", ""))) return;

            var fileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".db", ".sqlite", ".sqlite3" } }
            });

            var dbFile = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
                    FileTypes = fileType
                });

            while (dbFile == null)
            {
                if (App.Current?.MainPage == null) return;
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn file cơ sở dữ liệu SQLite", "OK");

                dbFile = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Chọn file cơ sở dữ liệu SQLite",
                    FileTypes = fileType
                });
            }

            Preferences.Set("DATABASE_PATH", dbFile.FullPath);
        }

        public static async Task<List<LopHoc>> getClasses()
        {
            List<LopHoc> result = new List<LopHoc>();

            using (var db = new AppDbContext())
            {
                result = await db.LopHoc.ToListAsync();
            }

            return result;
        }
        public static async Task<List<HocSinh>> getStudents(string classId)
        {
            List<HocSinh> result = new List<HocSinh>();

            using (var db = new AppDbContext())
            {
                result = await db.HocSinh.Where(h => h.MaLop == classId).ToListAsync();
            }

            return result;
        }

        public static void addClass(LopHoc lopHoc)
        {
            using (var db = new AppDbContext())
            {
                if (db.LopHoc.Any(LopHoc => LopHoc.MaLop == lopHoc.MaLop))
                {
                    if (App.Current?.MainPage == null) return;
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã lớp đã tồn tại", "OK");
                    return;
                }

                db.LopHoc.Add(lopHoc);
                db.SaveChanges();
            }
        }
        public static void deleteClasses(ObservableCollection<LopHoc> dsXoa)
        {
            if (dsXoa.Count == 0) return;

            using (var db = new AppDbContext())
            {
                foreach (var i in dsXoa)
                {
                    db.LopHoc.Remove(i);
                }

                db.SaveChanges();
            }
        }
        public static void updateClass(LopHoc lopHoc)
        {
            using (var db = new AppDbContext())
            {
                var lopHocInDb = db.LopHoc.Find(lopHoc.MaLop);

                if (lopHocInDb == null)
                {
                    if (App.Current?.MainPage == null) return;
                    App.Current.MainPage.DisplayAlert("Thông báo", "Không tìm thấy lớp học", "OK");
                    return;
                }

                lopHocInDb.TenLop = lopHoc.TenLop;

                db.SaveChanges();
            }
        }

        public static void addStudent(HocSinh hocSinh)
        {
            using (var db = new AppDbContext())
            {
                if (db.HocSinh.Any(h => h.Mssv == hocSinh.Mssv))
                {
                    if (App.Current?.MainPage == null) return;
                    App.Current.MainPage.DisplayAlert("Thông báo", "Mã số sinh viên đã tồn tại", "OK");
                    return;
                }

                db.HocSinh.Add(hocSinh);
                db.SaveChanges();
            }
        }
        public static void deleteStudents(ObservableCollection<HocSinh> dsXoa)
        {
            if (dsXoa.Count == 0) return;

            using (var db = new AppDbContext())
            {
                foreach (var i in dsXoa)
                {
                    db.HocSinh.Remove(i);
                }

                db.SaveChanges();
            }
        }
        public static void updateStudent(HocSinh hocSinh)
        {
            using (var db = new AppDbContext())
            {
                var hocSinhInDb = db.HocSinh.Find(hocSinh.Mssv);

                if (hocSinhInDb == null)
                {
                    if (App.Current?.MainPage == null) return;
                    App.Current.MainPage.DisplayAlert("Thông báo", "Không tìm thấy sinh viên", "OK");
                    return;
                }

                hocSinhInDb.HoTen = hocSinh.HoTen;
                hocSinhInDb.NgaySinh = hocSinh.NgaySinh;

                db.SaveChanges();
            }
        }
    }
}