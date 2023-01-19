using AndroidX.AppCompat.App;

namespace Plugin.DynamicAlert;

public sealed partial class DynamicAlert
{
    private readonly AlertDialog _alertDialog;

    public DynamicAlert(string title, string message)
    {
        Guard.IsNotNullOrWhiteSpace(title, nameof(title));
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        _alertDialog = new AlertDialog.Builder(Platform.CurrentActivity!)!.SetTitle(title)!.SetMessage(message)!.SetCancelable(false)!.Create();
        _alertDialog.Show();
    }

    public void Update(string message)
    {
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        _alertDialog.SetMessage(message);
    }

    public void Dismiss()
    {
        _alertDialog.Dismiss();
        _alertDialog.Dispose();
    }
}