using Android.App;
using Android.OS;

namespace Plugin.DynamicAlert.Android;

[Obsolete(nameof(AlertDialogFragment))]
public class AlertDialogFragment : DialogFragment
{
    private readonly Activity _activity;
    private readonly string _title;
    private readonly string _message;
    private AlertDialog? _alertDialog;

    public AlertDialogFragment(Activity activity, string title, string message)
    {
        _activity = activity;
        _title = title;
        _message = message;
    }

    [Obsolete(nameof(OnCreateDialog))]
    public override Dialog OnCreateDialog(Bundle? savedInstanceState)
    {
        return _alertDialog = new AlertDialog.Builder(_activity)?.SetTitle(_title)?.SetMessage(_message)?.Create()!;
    }

    public void UpdateMessage(string message)
    {
        _activity.RunOnUiThread(() => { _alertDialog?.SetMessage(message); });
    }
}
