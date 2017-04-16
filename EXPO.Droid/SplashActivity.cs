using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.OS;
using EXPO.Droid.Services;

namespace EXPO.Droid
{
    [Activity(MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var id = Intent.Extras?.GetString("word_id");

            if (!string.IsNullOrEmpty(id))
            {
                StartActivity(typeof(WordActivity));
            }
        }
    }
}

