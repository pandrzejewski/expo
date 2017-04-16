using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Widget;
using EstimoteSdk;

namespace EXPO.Droid.Services
{
    [Service]
    [IntentFilter(new String[] { "com.eps.expo" })]
    public class BeaconService : Service
    {
        private static BeaconManager _beaconManager;
        private static readonly Region BeaconRegion = new Region("estimote", "B9407F30-F5F8-466E-AFF9-25556B57FE6D");
        private static BeaconService _context;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _context = this;
            _beaconManager = new BeaconManager(ApplicationContext);
            _beaconManager.SetBackgroundScanPeriod(500, 500);
            _beaconManager.Connect(new ServiceReadyCallback());
            var l = new MonitoringListener();
            _beaconManager.SetMonitoringListener(l);
            l.ShowNotification("asd", "asd", "11");
            return StartCommandResult.Sticky;
        }
        private class ServiceReadyCallback : Activity, BeaconManager.IServiceReadyCallback
        {
            public void OnServiceReady()
            {
                _beaconManager.StartMonitoring(BeaconRegion);
            }
        }
        private class MonitoringListener : Activity, BeaconManager.IMonitoringListener
        {

            public MonitoringListener()
            {
            }

            public void OnEnteredRegion(Region region, IList<Beacon> beacons)
            {
               ShowNotification("EXPO 2022", "New word discovered!", region.Identifier);
            }

            public void OnExitedRegion(Region region)
            {
                var handler = new Handler(Looper.MainLooper);
                handler.Post(() =>
                {
                    Toast.MakeText(_context, "EXITED <<<<<<<<<<<<<<<<<<<<<<<<<<<<", ToastLength.Short);
                });
            }

            public void ShowNotification(string title, string message, string id)
            {
                var intent = new Intent(_context, typeof(MainActivity));
                intent.PutExtra("word_id", id);
                intent.AddFlags(ActivityFlags.SingleTop);
                var pendingIntent = PendingIntent.GetActivity(_context, 0, intent, PendingIntentFlags.OneShot);

                var notificationBuilder = new Notification.Builder(_context)
                    .SetSmallIcon(Resource.Drawable.expo_logotype)
                    .SetContentTitle(title)
                    .SetContentText(message)
                    .SetAutoCancel(true)
                    .SetContentIntent(pendingIntent)
                    .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification));

                var notificationManager = (NotificationManager)_context.GetSystemService(Context.NotificationService);
                notificationManager.Notify((int)(DateTime.Now.Ticks & 0xFFFFFFFF), notificationBuilder.Build());
            }
        }
    }
}