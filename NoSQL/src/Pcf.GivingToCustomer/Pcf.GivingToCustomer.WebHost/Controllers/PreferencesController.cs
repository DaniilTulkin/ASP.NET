using Microsoft.AspNetCore.Mvc;
using Pcf.GivingToCustomer.Core.Abstractions.Gateways;
using Pcf.GivingToCustomer.Core.Abstractions.Repositories;
using Pcf.GivingToCustomer.Core.Domain;
using Pcf.GivingToCustomer.Integration;
using Pcf.GivingToCustomer.WebHost.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.WebHost.Controllers
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

        public PreferencesController(IRepository<Preference> preferencesRepository,
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
            // var preferences = await _preferencesRepository.GetAllAsync();
            var preferences = await _preferenceGateway.GetPreferences();

            var response = preferences.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(response);
        }
    }
}