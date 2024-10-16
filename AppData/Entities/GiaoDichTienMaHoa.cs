using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Entities
{
    public class GiaoDichTienMaHoa
    {
        public Guid ID { get; set; }
        public string TenNguoiGui { get; set; }
        public string TenNguoiNhan { get; set; }
        public string LoaiTienMaHoa { get; set; } 
        public decimal SoLuong { get; set; }
        public DateTime NgayGiaoDich { get; set; }
        public decimal PhiGiaoDich { get; set; }
        public string TrangThai { get; set; } 
    }
}
