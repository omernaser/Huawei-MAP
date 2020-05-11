using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Com.Huawei.Hms.Maps;
using Android;
using Com.Huawei.Hms.Maps.Internal;
using IOnMapReadyCallback = Com.Huawei.Hms.Maps.IOnMapReadyCallback;
using Android.Support.V4.App;
using Android.Content;
using System;
using Android.Content.PM;
using Com.Huawei.Agconnect.Config;
using HmsMapDemo;

namespace HMS_Map_Demo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private static string TAG = "MapViewDemoActivity";
        //Huawei map
        private HuaweiMap hMap;

        private MapView mMapView;

        private static string[] RUNTIME_PERMISSIONS = {
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.Internet
    };

        private static string MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            AGConnectServicesConfig config = AGConnectServicesConfig.FromContext(ApplicationContext);
            config.OverlayWith(new HmsLazyInputStream(this));
            Com.Huawei.Agconnect.AGConnectInstance.Initialize(this);

            if (!hasPermissions(this, RUNTIME_PERMISSIONS))
            {
                ActivityCompat.RequestPermissions(this, RUNTIME_PERMISSIONS, 3);
            }

            //get mapview instance
            mMapView = FindViewById<MapView>(Resource.Id.mapView);
            Bundle mapViewBundle = null;
            if (savedInstanceState != null)
            {
                mapViewBundle = savedInstanceState.GetBundle(MAPVIEW_BUNDLE_KEY);
            }
              mMapView.OnCreate(mapViewBundle);
            //get map instance
            mMapView.GetMapAsync(this);
        }

        private bool hasPermissions(MainActivity context, string[] permissions)
        {
            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.M && permissions != null)
            {
                foreach (var permission in permissions)
                {
                    if (ActivityCompat.CheckSelfPermission(context, permission) != Permission.Granted)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void OnMapReady(HuaweiMap map)
        {
            //get map instance in a callback method
            //Log.d(TAG, "onMapReady: ");
            hMap = map;
        }
        protected override void OnStart()
        {
            base.OnStart();
            mMapView.OnStart();
        }
        protected override void OnStop()
        {
            base.OnStop();
            mMapView.OnStop();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            mMapView.OnDestroy();
        }
        protected override void OnPause()
        {
            mMapView.OnPause();
            base.OnPause();
        }

    }
}