using UIKit;

namespace Plugin.DynamicAlert;

public sealed partial class DynamicAlert
{
    private UIAlertController? _alertController;

    public DynamicAlert(string title, string message)
    {
        Guard.IsNotNullOrWhiteSpace(title, nameof(title));
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            if (_alertController is not null)
                UIApplication.SharedApplication.ConnectedScenes
                .OfType<UIWindowScene>()
                .SelectMany(s => s.Windows)
                .First(w => w.IsKeyWindow).RootViewController?.PresentViewController(_alertController, true, null);
        });
    }

    public void Update(string message)
    {
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            if (_alertController is not null)
                _alertController.Message = message;
        });
    }

    public void Dismiss()
    {
        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController?.DismissViewController(true, () => {
                _alertController.Dispose();
            });
        });
    }
}