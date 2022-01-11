using UIKit;

namespace Plugin.DynamicAlert
{
    public partial class DynamicAlert
    {
        private UIAlertController alert;

        private void PlatformShow(string title, string message)
        {
            if (alert != null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
                var rootVC = UIApplication.SharedApplication.Windows[0].RootViewController;
                rootVC.PresentViewController(alert, true, null);
            });
        }

        private void PlatformUpdate(string message)
        {
            if (alert == null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                alert.Message = message;
            });
        }

        private void PlatformDismiss()
        {
            if (alert == null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                alert.DismissViewController(true, () => {
                    alert.Dispose();
                    alert = null;
                });
            });
        }
    }
}