using Microsoft.AspNetCore.Mvc;
using Pcf.Preference.Core.Repositories;
using Pcf.Preference.WebHost.Cache;
using Pcf.Preference.WebHost.Models;
using System.Collections.Generic;

namespace Pcf.Preference.WebHost.Controllers
{
    /// <summary>
    /// Предпочтения клиентов
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PreferencesController
        : ControllerBase
    {
        private readonly IRepository<Core.Domain.Preference> _preferencesRepository;
        private readonly ICacheService _cacheService;

        public PreferencesController(
            IRepository<Core.Domain.Preference> preferencesRepository, 
            ICacheService cacheService)
        {
            _preferencesRepository = preferencesRepository;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Получить список предпочтений
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesAsync()
        {

            var cacheData = _cacheService.GetData<IEnumerable<Core.Domain.Preference>>("preferences");
            if (cacheData != null)
            {
                var resFromCache = cacheData.ToList();
                Response.Headers.Append("Access-Control-Expose-Headers", "X-Total-Count");
                Response.Headers.Append("X-Total-Count", resFromCache.Count.ToString());
                return Ok(resFromCache);
            }
            var preferences = await _preferencesRepository.GetAllAsync();
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            cacheData = preferences;
            _cacheService.SetData("preferences", cacheData, expirationTime);

            var response = preferences.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferenceByIdAsync(Guid id)
        {

            var cacheData = _cacheService.GetData<Core.Domain.Preference>($"preference-{id}");
            if (cacheData != null)
            {
                return Ok(cacheData);
            }
            var preference = await _preferencesRepository.GetByIdAsync(id);
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            _cacheService.SetData($"preference-{id}", preference, expirationTime);
            var response = new PreferenceResponse() { Id = id, Name = preference.Name };
            return Ok(response);
        }

        [HttpPost("range")]
        public async Task<ActionResult<List<PreferenceResponse>>> GetPreferencesByIdsAsync([FromBody] List<Guid> Ids)
        {
            List<Guid> missingIds = new List<Guid>();
            List<Core.Domain.Preference> Result = new();
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            foreach (var cachedId in Ids)
            {
                var cached = _cacheService.GetData<Core.Domain.Preference>($"preference-{cachedId}");

                if (cached == null)
                    missingIds.Add(cachedId);
                else
                    Result.Add(cached);
            }
            if (missingIds.Count > 0)
            {
                var missingPreferences = await _preferencesRepository.GetRangeByIdsAsync(missingIds);
                if (missingPreferences != null)
                {

                    Result.AddRange(missingPreferences);
                    foreach (var preference in missingPreferences)
                    {
                        _cacheService.SetData($"preference-{preference.Id}", preference, expirationTime);
                    }

                }
            }

            var response = Result.Select(x => new PreferenceResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return Ok(response);
        }
    }
}
