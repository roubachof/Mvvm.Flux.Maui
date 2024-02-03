﻿using Symbiot.Mobile.Presentation.Navigation;

namespace Symbiot.Mobile.Presentation.Pages.Common.Popup
{
    public class AlertDialogPopupPageViewModel : ADialogPopupPageViewModel
    {
        public AlertDialogPopupPageViewModel(
            IPopupService popupService,
            string title,
            string contentText = null,
            string confirmText = null)
            : base(popupService, title, contentText, confirmText, null, PopupActions.Ok)
        {
        }
    }
}