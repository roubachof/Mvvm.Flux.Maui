﻿using Symbiot.Mobile.Presentation.Navigation;

namespace Symbiot.Mobile.Presentation.Pages.Common.Popup
{
    public class ConfirmDialogPageViewModel : ADialogPopupPageViewModel<bool>
    {
        public ConfirmDialogPageViewModel(
            IPopupService popupService,
            string title,
            string contentText = null,
            string confirmText = null,
            string cancelText = null)
            : base(popupService, title, contentText, confirmText, cancelText, PopupActions.OkCancel)
        {
        }

        public override bool GetResult(bool isOk, object parameter)
        {
            return isOk;
        }
    }
}