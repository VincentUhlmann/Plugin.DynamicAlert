#if !NET6_0
using Android.App;
using Android.OS;
#endif

namespace Plugin.DynamicAlert;

public partial class DynamicAlert
{
    private Activity? _activity;
    private DynamicAlertDialogFragment? _fragment;

    public void Init(Activity activity)
    {
        _activity = activity;
    }

    private void PlatformShow(string title, string message)
    {
        try {
            if (_activity == null)
                throw new NotInitializedException("Activity is null");

            var fragmentManager = _activity.FragmentManager;
            var fragmentTransaction = fragmentManager?.BeginTransaction();
            var previousFragment = fragmentManager?.FindFragmentByTag(nameof(DynamicAlert));
            if (previousFragment != null) {
                fragmentTransaction?.Remove(previousFragment);
            }
            fragmentTransaction?.DisallowAddToBackStack();
            _fragment = new DynamicAlertDialogFragment(_activity, title, message);
            _fragment.Show(fragmentManager, nameof(DynamicAlert));
        } catch (NotInitializedException) {
            throw;
        } catch (Exception) {
            // Console.WriteLine($"Exception: {e.Message}");
        }
    }

    private void PlatformUpdate(string message)
    {
        try {
            if (_fragment == null)
                return;

            _fragment.UpdateMessage(message);
        } catch (Exception) {
            // Console.WriteLine($"Exception: {e.Message}");
        }
    }

    private void PlatformDismiss()
    {
        try {
            if (_fragment == null)
                return;

            _fragment.Dismiss();
            _fragment.Dispose();
            _fragment = null;
        } catch (Exception) {
            // Console.WriteLine($"Exception: {e.Message}");
        }
    }

    private class NotInitializedException : Exception
    {
        public NotInitializedException(string message) : base(message)
        {
        }
    }

    private class DynamicAlertDialogFragment : DialogFragment
    {
        private readonly Activity _activity;
        private readonly string _title;
        private readonly string _message;
        private AlertDialog? _alertDialog;

        public DynamicAlertDialogFragment(Activity activity, string title, string message)
        {
            _activity = activity;
            _title = title;
            _message = message;
        }

        public override Dialog OnCreateDialog(Bundle? savedInstanceState)
        {
            return _alertDialog = new AlertDialog.Builder(_activity)?.SetTitle(_title)?.SetMessage(_message)?.Create()!;
        }

        public void UpdateMessage(string message)
        {
            _activity.RunOnUiThread(() => { _alertDialog?.SetMessage(message); });
        }
    }
}