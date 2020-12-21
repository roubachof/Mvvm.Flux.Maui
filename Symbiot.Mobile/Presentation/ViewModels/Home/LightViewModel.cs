using Prism.Mvvm;

using Symbiot.Mobile.Domain.Lights;

namespace Symbiot.Mobile.Presentation.ViewModels.Home
{
    public class LightViewModel : BindableBase
    {
        private readonly Light _light;

        private bool _isOn;

        public LightViewModel(Light light)
        {
            _light = light;

            _isOn = _light.IsOn;
        }

        public string Name => _light.Name;

        public bool IsOn
        {
            get => _isOn;
            set => SetProperty(ref _isOn, value);
        }

        public Light GetEntity()
        {
            _light.IsOn = _isOn;
            return _light;
        }
    }
}