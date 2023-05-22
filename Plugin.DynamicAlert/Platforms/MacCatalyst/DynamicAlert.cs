using UIKit;

namespace Plugin.DynamicAlert;

/// <summary>
/// A class representing a dynamic alert that can be updated and dismissed.
/// </summary>
public sealed partial class DynamicAlert
{
    private UIAlertController? _alertController;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicAlert"/> class with the specified title and message.
    /// </summary>
    /// <param name="title">The title of the alert.</param>
    /// <param name="message">The message of the alert.</param>
    public DynamicAlert(string title, string message)
    {
        ArgumentException.ThrowIfNullOrEmpty(title, nameof(title));
        ArgumentException.ThrowIfNullOrEmpty(message, nameof(message));

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);

            if (_alertController is not null)
                UIApplication.SharedApplication.ConnectedScenes
                .OfType<UIWindowScene>()
                .SelectMany(s => s.Windows)
                .First(w => w.IsKeyWindow).RootViewController?.PresentViewController(_alertController, true, null);
        });
    }

    /// <summary>
    /// Updates the message of the alert.
    /// </summary>
    /// <param name="message">The new message of the alert.</param>
    public void Update(string message)
    {
        ArgumentException.ThrowIfNullOrEmpty(message, nameof(message));

        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            if (_alertController is not null)
                _alertController.Message = message;
        });
    }

    /// <summary>
    /// Dismisses the alert.
    /// </summary>
    public void Dismiss()
    {
        UIApplication.SharedApplication.InvokeOnMainThread(() => {
            _alertController?.DismissViewController(true, () => {
                _alertController.Dispose();
            });
        });
    }
}
