using System;
using System.ComponentModel;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms;
using Sharpnado.Presentation.Forms.CustomViews;
using Sharpnado.Tasks;

namespace Symbiot.Mobile.Presentation.Behaviors
{
    public class ResetOnCompletionBehavior : BehaviorBase<TaskLoaderView>
    {
        public ResetOnCompletionBehavior()
        {
            DelayBeforeResetInSeconds = 5;
        }

        public int DelayBeforeResetInSeconds
        {
            get;
            set;
        }

        protected override void OnAttachedTo(TaskLoaderView bindable)
        {
            base.OnAttachedTo(bindable);

            SubscribeToNotifier();
        }

        protected override void OnDetachingFrom(TaskLoaderView bindable)
        {
            UnsubscribeFromNotifier();

            base.OnDetachingFrom(bindable);
        }

        protected override void OnAssociatedObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnAssociatedObjectPropertyChanged(sender, e);
            if (e.PropertyName == nameof(TaskLoaderView.TaskLoaderNotifier))
            {
                SubscribeToNotifier();
            }
        }

        private void SubscribeToNotifier()
        {
            if (AssociatedObject.TaskLoaderNotifier == null)
            {
                return;
            }

            ((TaskLoaderNotifierBase)AssociatedObject.TaskLoaderNotifier).PropertyChanged += NotifierPropertyChanged;
        }

        private void UnsubscribeFromNotifier()
        {
            if (AssociatedObject.TaskLoaderNotifier == null)
            {
                return;
            }

            ((TaskLoaderNotifierBase)AssociatedObject.TaskLoaderNotifier).PropertyChanged += NotifierPropertyChanged;
        }

        private void NotifierPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var notifier = (ITaskLoaderNotifier)sender;
            if (e.PropertyName == nameof(ITaskLoaderNotifier.IsCompleted) && notifier.IsCompleted)
            {
                TaskMonitor.Create(
                    async () =>
                    {
                        await Task.Delay(TimeSpan.FromSeconds(DelayBeforeResetInSeconds));
                        notifier.Reset();
                    });
            }
        }
    }
}
