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

        private readonly RemoteCallEmulator _remoteCallEmulator;

        private readonly Dictionary<int, Light> _lights = new ()
            {
                { 1, new Light { Id = 1, IsOn = false, Name = "Salon 1", } },
                { 2, new Light { Id = 2, IsOn = true, Name = "Salon 2", } },
                { 3, new Light { Id = 3, IsOn = false, Name = "Couloir", } },
                { 4, new Light { Id = 4, IsOn = false, Name = "Chambre 1", } },
                { 5, new Light { Id = 5, IsOn = false, Name = "Chambre 2", } },
                { 6, new Light { Id = 6, IsOn = true, Name = "Salle de bain", } },
                { 7, new Light { Id = 7, IsOn = false, Name = "Garage", } },
                { 8, new Light { Id = 8, IsOn = true, Name = "Jardin 1", } },
                { 9, new Light { Id = 9, IsOn = false, Name = "Jardin 2", } },
            };

        public LightServiceMock()
        {
            _remoteCallEmulator = new RemoteCallEmulator(exceptionProbability: 0.5, exceptionCycle: true);
        }

        public event EventHandler<Light> LightUpdated;

        public async Task<List<Light>> GetLightsAsync()
        {
            Log.Info("GetLightsAsync()");

            await _remoteCallEmulator.EmulateRemoteCallDefault().ConfigureAwait(false);
            var result = _remoteCallEmulator.Clone(_lights.Values.ToList());

            Log.Info($"returning {result.Count} lights");
            return result;
        }

        public async Task<Light> GetLightAsync(int lightId)
        {
            Log.Info($"GetLightAsync( lightId: {lightId} )");

            await _remoteCallEmulator.EmulateRemoteCallDefault().ConfigureAwait(false);
            var result = _remoteCallEmulator.Clone(_lights[lightId]);

            return result;
        }

        public async Task UpdateLightAsync(Light light)
        {
            Log.Info($"UpdateLightAsync( light: {light.Name} )");

            await _remoteCallEmulator.EmulateRemoteCallDefault().ConfigureAwait(false);
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