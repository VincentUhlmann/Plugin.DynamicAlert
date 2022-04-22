namespace Plugin.DynamicAlert;

public partial class DynamicAlert
{
    private static readonly Lazy<DynamicAlert> _current = new(true);
    public static DynamicAlert Current => _current.Value;

    public void Show(string title, string message) =>
        PlatformShow(title, message);

    public void Update(string message) =>
        PlatformUpdate(message);

    public void Dismiss() =>
        PlatformDismiss();
}