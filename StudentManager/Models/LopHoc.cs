using System.ComponentModel.DataAnnotations;

namespace StudentManager.Models
{
    public class LopHoc
    {
        [Key]
        public string MaLop { get; set; }
        public string TenLop { get; set; }

        public ICollection<HocSinh> HocSinh { get; set; } = new List<HocSinh>();
    }
}
