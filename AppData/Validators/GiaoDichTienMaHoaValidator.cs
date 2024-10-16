using AppData.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Validators
{
    public class GiaoDichTienMaHoaValidator : AbstractValidator<GiaoDichTienMaHoa>
    {
        public GiaoDichTienMaHoaValidator()
        {
            RuleFor(g => g.TenNguoiGui)
                .NotEmpty().WithMessage("Tên người gửi không được để trống");
            RuleFor(g => g.TenNguoiNhan)
                .NotEmpty().WithMessage("Tên người nhận không được để trống");
            RuleFor(g => g.LoaiTienMaHoa)
                .NotEmpty().WithMessage("Loại tiền mã hóa không được để trống")
                .Must(BeAValidCurrencyType).WithMessage("Loại tiền mã hóa không hợp lệ");
            RuleFor(g => g.SoLuong)
                .GreaterThan(0).WithMessage("Số lượng phải lớn hơn 0");
            RuleFor(g => g.NgayGiaoDich)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày giao dịch không được trong tương lai");
            RuleFor(g => g.PhiGiaoDich)
                .GreaterThanOrEqualTo(0).WithMessage("Phí giao dịch phải lớn hơn hoặc bằng 0");
            RuleFor(g => g.TrangThai)
                .NotEmpty().WithMessage("Trạng thái không được để trống")
                .Must(BeAValidStatus).WithMessage("Trạng thái phải là một trong các giá trị hợp lệ: Đang xử lý, Thành công, Thất bại");
        }
        private bool BeAValidCurrencyType(string currency)
        {
            var validCurrencies = new[] { "Bitcoin", "Ethereum", "Litecoin", "Ripple" };
            return Array.Exists(validCurrencies, c => c.Equals(currency, StringComparison.OrdinalIgnoreCase));
        }

        private bool BeAValidStatus(string status)
        {
            var validStatuses = new[] { "Đang xử lý", "Thành công", "Thất bại" };
            return Array.Exists(validStatuses, s => s.Equals(status, StringComparison.OrdinalIgnoreCase));
        }
    }
}
