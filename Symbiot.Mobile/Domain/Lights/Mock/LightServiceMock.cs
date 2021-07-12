using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MetroLog;

using Symbiot.Mobile.Infrastructure.Logging;
using Symbiot.Mobile.Infrastructure.Mocking;

using Xamarin.Forms;

namespace Symbiot.Mobile.Domain.Lights.Mock
{
    public class LightServiceMock : ILightService
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(LightServiceMock));

        private readonly Dictionary<int, Light> _lights = new()
            {
                { 1, new Light(1, "Salon 1", false) },
                { 2, new Light(2, "Salon 2", true) },
                { 3, new Light(2, "Couloir", false) },
                { 4, new Light(2, "Chambre 1", false) },
                { 5, new Light(2, "Chambre 1", false) },
                { 6, new Light(2, "Salle de bain", true) },
                { 7, new Light(2, "Garage", false) },
                { 8, new Light(2, "Jardin 1", true) },
                { 9, new Light(2, "Jardin 2", false) },
            };

        private readonly RemoteCallEmulator _remoteCallEmulator;

        public LightServiceMock()
        {
            _remoteCallEmulator = new RemoteCallEmulator(exceptionProbability: 0.5, exceptionCycle: true);
        }

        public event EventHandler<Light> LightUpdated;

        public async Task<List<Light>> GetLightsAsync()
        {
            Log.Info("GetLightsAsync()");

            await _remoteCallEmulator.EmulateRemoteCallDefault()
                .ConfigureAwait(false);
            var result = _remoteCallEmulator.Clone(_lights.Values.ToList());

            Log.Info($"returning {result.Count} lights");
            return result;
        }

        public async Task<Light> GetLightAsync(int lightId)
        {
            Log.Info($"GetLightAsync( lightId: {lightId} )");

            await _remoteCallEmulator.EmulateRemoteCallDefault()
                .ConfigureAwait(false);
            var result = _remoteCallEmulator.Clone(_lights[lightId]);

            return result;
        }

        public async Task UpdateLightAsync(Light light)
        {
            Log.Info($"UpdateLightAsync( light: {light.Name} )");

            await _remoteCallEmulator.EmulateRemoteCallDefault()
                .ConfigureAwait(false);
            _lights[light.Id] = light;
            var result = _remoteCallEmulator.Clone(_lights[light.Id]);
            DispatchLightUpdated(result);
        }

        private void DispatchLightUpdated(Light updatedLight)
        {
            Log.Info("DispatchLightUpdated()");
            Device.InvokeOnMainThreadAsync(() => LightUpdated?.Invoke(this, updatedLight));
        }
    }
}