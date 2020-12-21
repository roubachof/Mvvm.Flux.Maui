using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Symbiot.Mobile.Domain.Lights
{
    public interface ILightService
    {
        event EventHandler<Light> LightUpdated;

        Task<List<Light>> GetLightsAsync();

        Task<Light> GetLightAsync(int lightId);

        Task UpdateLightAsync(Light light);
    }
}