using Android.App;
using Android.Content.PM;
using Android.OS;
using AndroidX.AppCompat.App;
using Microsoft.Maui.ApplicationModel;

namespace Hotel_Maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightYes;
        base.OnCreate(savedInstanceState);
    }
}
