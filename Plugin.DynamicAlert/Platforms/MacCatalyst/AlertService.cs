using UIKit;

namespace Plugin.DynamicAlert.Services;

public sealed partial class AlertService
{
    private UIAlertController? _alertController;

    public void Show(string title, string message)
    {
        Guard.IsNotNullOrWhiteSpace(title, nameof(title));
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        if (_alertController != null)
            return;

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            var rootViewController = UIApplication.SharedApplication.Windows[0].RootViewController;

            rootViewController?.PresentViewController(_alertController, true, null);
        });
    }

    public void Update(string message)
    {
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        if (_alertController is null)
        {
            _logger.LogInformation($"{nameof(UIAlertController)} is null. Text cannot be updated.");
            return;
        }

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController.Message = message;
        });
    }

    public void Dismiss()
    {
        if (_alertController is null)
        {
            _logger.LogInformation($"{nameof(UIAlertController)} is null. Text cannot be dismissed.");
            return;
        }

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController.DismissViewController(true, () => {
                _alertController.Dispose();
                _alertController = null;
            });
        });
    }
}