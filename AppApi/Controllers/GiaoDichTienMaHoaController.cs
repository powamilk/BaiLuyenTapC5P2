using AppData.Entities;
using AppData.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoDichTienMaHoaController : ControllerBase
    {
        private readonly IGiaoDichTienMaHoaRepository _repository;
        private readonly IValidator<GiaoDichTienMaHoa> _giaoDichValidator;
        public GiaoDichTienMaHoaController(IGiaoDichTienMaHoaRepository repository, IValidator<GiaoDichTienMaHoa> giaoDichValidator)
        {
            _repository = repository;
            _giaoDichValidator = giaoDichValidator;
        }

        [HttpGet("TinhTongPhi")]
        public async Task<IActionResult> TinhTongPhiGiaoDich([FromQuery] List<Guid> giaoDichIds)
        {
            var tongPhi = await _repository.TinhTongPhiGiaoDichAsync(giaoDichIds);
            return Ok(tongPhi);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var giaoDichs = await _repository.GetAllAsync();
            return Ok(giaoDichs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var giaoDich = await _repository.GetByIdAsync(id);
            if (giaoDich == null) return NotFound();
            return Ok(giaoDich);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GiaoDichTienMaHoa giaoDich)
        {
            var validationResult = await _giaoDichValidator.ValidateAsync(giaoDich);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }
            await _repository.AddAsync(giaoDich);
            return CreatedAtAction(nameof(GetById), new { id = giaoDich.ID }, giaoDich);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, GiaoDichTienMaHoa giaoDich)
        {
            if (id != giaoDich.ID)
            {
                return BadRequest("ID không khớp");
            }
            var validationResult = await _giaoDichValidator.ValidateAsync(giaoDich);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new
                {
                    field = e.PropertyName,
                    message = e.ErrorMessage
                }));
            }
            await _repository.UpdateAsync(giaoDich);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
