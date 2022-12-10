namespace Plugin.DynamicAlert.Services;

public sealed partial class AlertService : IAlertService
{
    private readonly ILogger<AlertService> _logger;

    public AlertService(ILogger<AlertService> logger)
    {
        _logger= logger;
    }
}