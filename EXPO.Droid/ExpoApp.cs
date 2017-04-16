using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using EstimoteSdk;
using EXPO.Droid.Services;

namespace EXPO.Droid
{
    [Application]
    public class MyApp : Application
    {
        private BeaconManager _beaconManager;

        public MyApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //_beaconManager = new BeaconManager(ApplicationContext);
            StartService(new Intent(this, typeof(BeaconService)));
        }
    }
}