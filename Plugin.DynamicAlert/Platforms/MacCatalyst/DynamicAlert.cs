using UIKit;

namespace Plugin.DynamicAlert;

public sealed partial class DynamicAlert
{
    private readonly UIAlertController _alertController;

    public DynamicAlert(string title, string message)
    {
        Guard.IsNotNullOrWhiteSpace(title, nameof(title));
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        _alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

        UIApplication.SharedApplication.ConnectedScenes
            .OfType<UIWindowScene>()
            .SelectMany(s => s.Windows)
            .First(w => w.IsKeyWindow).RootViewController?.PresentViewController(_alertController, true, null);
    }

    public void Update(string message)
    {
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        _alertController.Message = message;
    }

    public void Dismiss()
    {
        _alertController.DismissViewController(true, () => {
            _alertController.Dispose();
        });
    }
}