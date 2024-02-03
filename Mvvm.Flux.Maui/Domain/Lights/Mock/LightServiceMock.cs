using MetroLog;

using Mvvm.Flux.Maui.Infrastructure.Logging;
using Mvvm.Flux.Maui.Infrastructure.Mocking;

namespace Mvvm.Flux.Maui.Domain.Lights.Mock
{
    public class LightServiceMock : ILightService
    {
        private static readonly ILogger Log = LoggerFactory.GetLogger(nameof(LightServiceMock));

        private readonly Dictionary<int, Light> _lights = new()
            {
                { 1, new Light(1, "Living Room", false) },
                { 2, new Light(2, "Living Room 2", true) },
                { 3, new Light(3, "Corridor", false) },
                { 4, new Light(4, "Master Bedroom", false) },
                { 5, new Light(5, "Bedroom 2", false) },
                { 6, new Light(6, "Bedroom 3", false) },
                { 7, new Light(7, "Bathroom", true) },
                { 8, new Light(8, "Bathroom 2", true) },
                { 9, new Light(9, "Garage", false) },
                { 10, new Light(10, "Garden", true) },
                { 11, new Light(11, "Garden 2", false) },
                { 12, new Light(12, "Swimming Pool", false) },
                { 13, new Light(13, "Pool House", false) },
            };

        private readonly RemoteCallEmulator _remoteCallEmulator;

        public LightServiceMock()
        {
            _remoteCallEmulator = new RemoteCallEmulator(exceptionProbability: 0, exceptionCycle: true);
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