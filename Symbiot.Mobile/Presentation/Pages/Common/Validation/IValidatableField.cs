using System.Collections.Generic;
using System.ComponentModel;

namespace Symbiot.Mobile.Presentation.Pages.Common.Validation
{
    public interface IValidatableField : INotifyPropertyChanged
    {
        List<string> ErrorMessages { get; }

        string FirstErrorMessage { get; }

        bool HasError { get; }

        bool Validate();
    }
}