using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManager.Models
{
    public class HocSinh
    {
        [Key]
        public string Mssv { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }

        [ForeignKey("LopHoc")]
        public string MaLop { get; set; }
        public LopHoc LopHoc { get; set; } = null!;
    }
}
