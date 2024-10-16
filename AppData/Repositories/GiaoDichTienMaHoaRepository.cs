using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class GiaoDichTienMaHoaRepository : IGiaoDichTienMaHoaRepository
    {
        private readonly AppDbContext _context;

        public GiaoDichTienMaHoaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GiaoDichTienMaHoa>> GetAllAsync()
        {
            return await _context.GiaoDichTienMaHoas.ToListAsync();
        }

        public async Task<GiaoDichTienMaHoa> GetByIdAsync(Guid id)
        {
            return await _context.GiaoDichTienMaHoas.FindAsync(id);
        }

        public async Task AddAsync(GiaoDichTienMaHoa giaoDich)
        {
            await _context.GiaoDichTienMaHoas.AddAsync(giaoDich);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GiaoDichTienMaHoa giaoDich)
        {
            _context.GiaoDichTienMaHoas.Update(giaoDich);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var giaoDich = await _context.GiaoDichTienMaHoas.FindAsync(id);
            if (giaoDich != null && giaoDich.TrangThai != "Đang xử lý")
            {
                _context.GiaoDichTienMaHoas.Remove(giaoDich);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<decimal> TinhTongPhiGiaoDichAsync(List<Guid> giaoDichIds)
        {
            var tongPhi = await _context.GiaoDichTienMaHoas
                .Where(g => giaoDichIds.Contains(g.ID))
                .SumAsync(g => g.PhiGiaoDich);
            return tongPhi;
        }
    }
}
