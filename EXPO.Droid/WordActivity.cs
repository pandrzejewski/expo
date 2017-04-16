using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using Android.OS;
using EXPO.Droid.Services;

namespace EXPO.Droid
{
    [Activity]
    public class WordActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.page_word);
        }
    }
}

