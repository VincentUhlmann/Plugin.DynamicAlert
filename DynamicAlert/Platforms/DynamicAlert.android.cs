using Android.App;
using Android.Content;
using Android.OS;

namespace Plugin.DynamicAlert
{
    public partial class DynamicAlert
    {
        private Activity activity;
        public Activity Activity {
            get {
                if (activity == null)
                    throw new System.Exception("Not initialized");

                return activity;
            }
            set => activity = value;
        }

        private const string FRAGMENT_TAG = "DynamicAlert_Fragment";
        private DynamicAlertDialogFragment fragment;

        private void PlatformShow(string title, string message)
        {
            var fragMgr = Activity.FragmentManager;
            var fragTransaction = fragMgr.BeginTransaction();
            var previous = fragMgr.FindFragmentByTag(FRAGMENT_TAG);
            if (previous != null) {
                fragTransaction.Remove(previous);
            }
            fragTransaction.DisallowAddToBackStack();
            fragment = DynamicAlertDialogFragment.Instance(Activity, title, message);
            fragment.Show(fragMgr, FRAGMENT_TAG);
        }

        private void PlatformUpdate(string message)
        {
            if (fragment == null)
                return;

            fragment.SetMessage(message);
        }

        private void PlatformDismiss()
        {
            if (fragment == null)
                return;

            fragment.Dismiss();
            fragment.Dispose();
            fragment = null;
        }

        private class DynamicAlertDialogFragment : DialogFragment
        {
            private AlertDialog alertDialog;
            private readonly Context context;

            public static DynamicAlertDialogFragment Instance(Context context, string title, string message)
            {
                var fragment = new DynamicAlertDialogFragment(context);
                var bundle = new Bundle();
                bundle.PutString("title", title);
                bundle.PutString("message", message);
                fragment.Arguments = bundle;
                return fragment;
            }

            public DynamicAlertDialogFragment(Context context)
            {
                this.context = context;
            }

            public override Dialog OnCreateDialog(Bundle savedInstanceState)
            {
                var title = Arguments.GetString("title");
                var message = Arguments.GetString("message");
                alertDialog = new AlertDialog.Builder(context)
                            .SetTitle(title)
                            .SetMessage(message)
                            .Create();
                return alertDialog;
            }

            public void SetMessage(string message)
            {
                (context as Activity).RunOnUiThread(() => { alertDialog.SetMessage(message); });
            }
        }
    }
}