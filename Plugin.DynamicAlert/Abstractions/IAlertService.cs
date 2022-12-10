namespace Plugin.DynamicAlert.Abstractions;

public interface IAlertService
{
    void Show(string title, string message);
    void Update(string message);
    void Dismiss();
}