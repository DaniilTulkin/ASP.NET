using Microsoft.AspNetCore.Mvc;
using Pcf.ReceivingFromPartner.Core.Abstractions.Gateways;
using Pcf.ReceivingFromPartner.Core.Abstractions.Repositories;
using Pcf.ReceivingFromPartner.Core.Domain;
using Pcf.ReceivingFromPartner.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения клиентов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController
        : ControllerBase
    {
        private readonly IRepository<Preference> _preferencesRepository;
        private readonly IPreferenceGateway _preferenceGateway;

        public PreferencesController(
            IRepository<Preference> preferencesRepository,
            IPreferenceGateway preferenceGateway)
        {
            _preferencesRepository = preferencesRepository;
            _preferenceGateway = preferenceGateway;
        }
        
        /// <summary>
        /// Получить список предпочтений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesAsync()
        {
            //var preferences = await _preferencesRepository.GetAllAsync();
            var preferences = await _preferenceGateway.GetPreferenses();

            var response = preferences.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(response);
        }
    }
}