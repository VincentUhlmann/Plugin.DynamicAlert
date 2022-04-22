using UIKit;

namespace Plugin.DynamicAlert;

public partial class DynamicAlert
{
    private UIAlertController? _alertController;

    private void PlatformShow(string title, string message)
    {
        try {
            if (_alertController != null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                _alertController = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
#if IOS15_0_OR_GREATER
                var rootViewController = GetRootViewController();
#else
                var rootViewController = UIApplication.SharedApplication.Windows[0].RootViewController;
#endif
                rootViewController?.PresentViewController(_alertController, true, null);
            });
        } catch (Exception) {
            // Console.WriteLine(e.Message);
        }
    }

    private void PlatformUpdate(string message)
    {
        try {
            if (_alertController == null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                _alertController.Message = message;
            });
        } catch (Exception) {
            // Console.WriteLine(e.Message);
        }
    }

    private void PlatformDismiss()
    {
        try {
            if (_alertController == null)
                return;

            UIApplication.SharedApplication.InvokeOnMainThread(() => {
                _alertController.DismissViewController(true, () => {
                    _alertController.Dispose();
                    _alertController = null;
                });
            });
        } catch (Exception) {
            // Console.WriteLine(e.Message);
        }
    }

#if IOS15_0_OR_GREATER
    private UIViewController? GetRootViewController()
    {
        UIViewController? viewController = null;

        try {
            viewController = GetKeyWindow()?.RootViewController;
            if (viewController is UITabBarController) {
                viewController = (viewController as UITabBarController)?.SelectedViewController;
            }

            var presentedController = viewController?.PresentedViewController;

            while(presentedController == viewController?.PresentedViewController) {
                if (presentedController is UITabBarController)
                    viewController = (presentedController as UITabBarController)?.SelectedViewController;
                else
                    viewController = presentedController;
            }
        } catch (Exception) {
            // Console.WriteLine(e.Message);
        }

        return viewController;
    }

    private UIWindow? GetKeyWindow()
    {
        UIWindow? uiWindow = null;

        try {
            List<UIScene> connectedScenes = new();

            foreach (UIScene connectedScene in UIApplication.SharedApplication.ConnectedScenes)
                connectedScenes.Add(connectedScene);

            connectedScenes.RemoveAll(x => x.ActivationState != UISceneActivationState.ForegroundActive);
            uiWindow = (connectedScenes.FirstOrDefault(x => x is UIWindowScene) as UIWindowScene)?.Windows[0];
        } catch (Exception) {
            // Console.WriteLine(e.Message);
        }

        return uiWindow;
    }
#endif
}