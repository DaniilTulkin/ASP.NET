using Pcf.GivingToCustomer.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pcf.GivingToCustomer.Core.Abstractions.Gateways
{
    public interface IPreferenceGateway
    {
        Task<List<Preference>> GetPreferences();
        Task<Preference> GetPreferenceByIdAsync(Guid preferenceId);
        Task<List<Preference>> GetPreferenceRangeByIdAsync(List<Guid> preferenceIds);
    }
}
