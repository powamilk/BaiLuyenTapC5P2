using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public interface IGiaoDichTienMaHoaRepository
    {
        Task<IEnumerable<GiaoDichTienMaHoa>> GetAllAsync();
        Task<GiaoDichTienMaHoa> GetByIdAsync(Guid id);
        Task AddAsync(GiaoDichTienMaHoa giaoDich);
        Task UpdateAsync(GiaoDichTienMaHoa giaoDich);
        Task DeleteAsync(Guid id);
        Task<decimal> TinhTongPhiGiaoDichAsync(List<Guid> giaoDichIds);
    }
}
