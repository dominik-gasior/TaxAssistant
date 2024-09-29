using Microsoft.AspNetCore.Mvc;
using TaxAssistant.Services;

namespace TaxAssistant.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IEterytService _eterytService;

        public AddressController(IEterytService eterytService)
        {
            _eterytService = eterytService;
        }

        [HttpGet("voivodeships")]
        public async Task<IActionResult> GetVoivodeships()
        {
            var voivodeships = _eterytService.GetVoivodeships();

            return Ok(voivodeships);
        }

        [HttpGet("voivodeships/{voivodeshipID}/provinces")]
        public async Task<IActionResult> GetProvinces(string voivodeshipID)
        {
            var provinces = _eterytService.GetProvinces(voivodeshipID);

            return Ok(provinces);
        }

        [HttpGet("voivodeships/{voivodeshipID}/provinces/{provinceID}/municipalities")]
        public async Task<IActionResult> GetMunicipalities(string voivodeshipID, string provinceID)
        {
            var provinces = _eterytService.GetMunicipalities(voivodeshipID, provinceID);

            return Ok(provinces);
        }

        [HttpGet("voivodeships/{voivodeshipID}/provinces/{provinceID}/municipalities/{municipalityID}/cities")]
        public async Task<IActionResult> GetCities(string voivodeshipID, string provinceID, string municipalityID)
        {
            var streets = _eterytService.GetCities(voivodeshipID, provinceID, municipalityID);

            return Ok(streets);
        }

        [HttpGet("voivodeships/{voivodeshipID}/provinces/{provinceID}/municipalities/{municipalityID}/streets")]
        public async Task<IActionResult> GetStreets(string voivodeshipID, string provinceID, string municipalityID)
        {
            var streets = _eterytService.GetStreets(voivodeshipID, provinceID, municipalityID);

            return Ok(streets);
        }
    }
}
