using AndroidX.AppCompat.App;

namespace Plugin.DynamicAlert;

/// <summary>
/// A class representing a dynamic alert that can be updated and dismissed.
/// </summary>
public sealed partial class DynamicAlert
{
    private readonly AlertDialog _alertDialog;

    /// <summary>
    /// Initializes a new instance of the <see cref="DynamicAlert"/> class with the specified title and message.
    /// </summary>
    /// <param name="title">The title of the alert.</param>
    /// <param name="message">The message of the alert.</param>
    public DynamicAlert(string title, string message)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException(nameof(title));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));

        _alertDialog = new AlertDialog.Builder(Platform.CurrentActivity!)!.SetTitle(title)!.SetMessage(message)!.SetCancelable(false)!.Create();
        _alertDialog.Show();
    }

    /// <summary>
    /// Updates the message of the alert.
    /// </summary>
    /// <param name="message">The new message of the alert.</param>
    public void Update(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException(nameof(message));

        _alertDialog.SetMessage(message);
    }

    /// <summary>
    /// Dismisses the alert.
    /// </summary>
    public void Dismiss()
    {
        _alertDialog.Dismiss();
        _alertDialog.Dispose();
    }
}
