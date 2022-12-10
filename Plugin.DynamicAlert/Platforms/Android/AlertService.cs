using Plugin.DynamicAlert.Android;

namespace Plugin.DynamicAlert.Services;

[Obsolete(nameof(AlertService))]
public sealed partial class AlertService
{
    private AlertDialogFragment? _fragment;

    public void Show(string title, string message)
    {
        Guard.IsNotNullOrWhiteSpace(title, nameof(title));
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        if (Platform.CurrentActivity is null)
            throw new Exception($"{nameof(Platform.CurrentActivity)} is null.");

        var fragmentManager = Platform.CurrentActivity.FragmentManager;

        if (fragmentManager is null)
            throw new Exception($"{nameof(fragmentManager)} is null.");

        var fragmentTransaction = fragmentManager.BeginTransaction();

        if (fragmentTransaction is null)
            throw new Exception($"{nameof(fragmentTransaction)} is null.");

        var previousFragment = fragmentManager.FindFragmentByTag(nameof(AlertDialogFragment));

        if (previousFragment is not null)
            fragmentTransaction.Remove(previousFragment);

        fragmentTransaction.DisallowAddToBackStack();
        _fragment = new AlertDialogFragment(Platform.CurrentActivity, title, message);
        _fragment.Show(fragmentManager, nameof(AlertDialogFragment));
    }

    public void Update(string message)
    {
        Guard.IsNotNullOrWhiteSpace(message, nameof(message));

        if (_fragment is null) {
            _logger.LogInformation($"{nameof(AlertDialogFragment)} is null. Text cannot be updated.");
            return;
        }

        _fragment.UpdateMessage(message);
    }

    public void Dismiss()
    {
        if (_fragment is null) {
            _logger.LogInformation($"{nameof(AlertDialogFragment)} is null. Text cannot be dismissed.");
            return;
        }

        _fragment.Dismiss();
        _fragment.Dispose();
        _fragment = null;
    }
}