using Foundation;

namespace caculator
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => ExperienceCalculatorr.CreateMauiApp();
    }
}
