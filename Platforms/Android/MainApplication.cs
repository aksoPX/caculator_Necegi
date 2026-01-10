using Android.App;
using Android.Runtime;

namespace caculator
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => ExperienceCalculatorr.CreateMauiApp();
    }
}
