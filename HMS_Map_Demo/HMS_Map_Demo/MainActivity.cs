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
using Android.Support.V4.Content;
using Com.Huawei.Hms.Maps.Model;
using Android.Graphics;
using Com.Huawei.Hms.Maps.Util;

namespace HMS_Map_Demo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        private static string TAG = "MapViewDemoActivity";
        //Huawei map
        private HuaweiMap hMap;

        private MapFragment mMapView;

        Button btnPolylineDemo, btnPolygonDemo, btnCircleDemo, btnMarkersDemo, btnOverlayDemo, btntype;


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

            checkPermission(new string[] {
                Android.Manifest.Permission.WriteExternalStorage,
                Android.Manifest.Permission.ReadExternalStorage,
                Android.Manifest.Permission.AccessCoarseLocation,
                Android.Manifest.Permission.AccessFineLocation,
                Android.Manifest.Permission.Internet }, 100);

            //get mapview instance
            mMapView = FragmentManager.FindFragmentById<MapFragment>(Resource.Id.mapview);

            btnMarkersDemo = FindViewById<Button>(Resource.Id.btnMarkersDemo);
            btnPolylineDemo = FindViewById<Button>(Resource.Id.btnPolylineDemo);
            btnPolygonDemo = FindViewById<Button>(Resource.Id.btnPolygonDemo);
            btnCircleDemo = FindViewById<Button>(Resource.Id.btnCircleDemo);
            btnOverlayDemo = FindViewById<Button>(Resource.Id.btnOverlayDemo);
            btntype = FindViewById<Button>(Resource.Id.btntype);
            btntype.Click += OnClick;
            btnMarkersDemo.Click += btnMarkersDemo_Click;
            btnPolylineDemo.Click += btnPolylineDemo_Click;
            btnPolygonDemo.Click += btnPolygonDemo_Click;
            btnCircleDemo.Click += btnCircleDemo_Click;
            btnOverlayDemo.Click += btnOverlayDemo_Click;

            Bundle mapViewBundle = null;
            if (savedInstanceState != null)
            {
                mapViewBundle = savedInstanceState.GetBundle(MAPVIEW_BUNDLE_KEY);
            }
            mMapView.OnCreate(mapViewBundle);
            //get map instance
            RunOnUiThread(()=> mMapView.GetMapAsync(this));
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (hMap.MapType == 1 || hMap.MapType == 0)
                hMap.MapType = 2;
            else
                hMap.MapType = 1;
          
            RunOnUiThread(() => mMapView.GetMapAsync(this));
        }

        public void checkPermission(string[] permissions, int requestCode)
        {
            foreach (string permission in permissions)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) == Permission.Denied)
                {
                    ActivityCompat.RequestPermissions(this, permissions, requestCode);
                }
            }

        }
        protected override void OnStart()
        {
            base.OnStart();
            mMapView.OnStart();

        }
        protected override void OnPause()
        {
            base.OnStart();
            mMapView.OnPause();

        }
        protected override void OnResume()
        {
            base.OnStart();
            mMapView.OnResume();

        }
        protected override void OnDestroy()
        {
            base.OnStart();
            mMapView.OnDestroy();

        }
        protected override void OnStop()
        {
            base.OnStart();
            mMapView.OnStop();

        }

        private void SetUpMap()
        {
            MapView mapFragment = FindViewById<MapView>(Resource.Id.mapview);

            mapFragment.GetMapAsync(this);
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

            hMap.UiSettings.ZoomControlsEnabled = true;
            hMap.UiSettings.CompassEnabled = true;
            hMap.UiSettings.MyLocationButtonEnabled = true;

            hMap.MyLocationEnabled = true;
            // hMap.MapType = HuaweiMap.MapTypeNone;

            hMap.MarkerClick += OnMarkerClick;
            hMap.MarkerDragStart += OnMarkerDragStart;
            hMap.MarkerDrag += OnMarkerDrag;
            hMap.MarkerDragEnd += OnMarkerDragEnd;

            hMap.PolylineClick += OnPolylineClick;
            hMap.PolygonClick += OnPolygonClick;
            hMap.CircleClick += OnCircleClick;
            hMap.GroundOverlayClick += OnGroundOverlayClick;

            hMap.SetInfoWindowAdapter(new CustomMapInfoWindow(this));

            Toast.MakeText(this, "OnMapReady done.", ToastLength.Short).Show();

            MarkersDemo(hMap);
        }
        private void btnMarkersDemo_Click(object sender, EventArgs e)
        {
            if (hMap != null)
                MarkersDemo(hMap);
            else
                Toast.MakeText(this, "map not ready", ToastLength.Short).Show();
        }
        private void btnPolylineDemo_Click(object sender, EventArgs e)
        {
            if (hMap != null)
                PolylineDemo(hMap);
            else
                Toast.MakeText(this, "map not ready", ToastLength.Short).Show();
        }
        private void btnPolygonDemo_Click(object sender, EventArgs e)
        {
            if (hMap != null)
                PolygonDemo(hMap);
            else
                Toast.MakeText(this, "map not ready", ToastLength.Short).Show();
        }
        private void btnCircleDemo_Click(object sender, EventArgs e)
        {
            if (hMap != null)
                CircleDemo(hMap);
            else
                Toast.MakeText(this, "map not ready", ToastLength.Short).Show();
        }
        private void btnOverlayDemo_Click(object sender, EventArgs e)
        {
            if (hMap != null)
                OverlayDemo(hMap);
            else
                Toast.MakeText(this, "map not ready", ToastLength.Short).Show();
        }

        private void MarkersDemo(HuaweiMap hMap)
        {
            hMap.Clear();

            Marker marker1;
            MarkerOptions marker1Options = new MarkerOptions()
                .InvokePosition(new LatLng(41.0083, 28.9784))
                .InvokeTitle("Marker Title #1")
                .InvokeSnippet("Marker Desc #1");
            marker1 = hMap.AddMarker(marker1Options);

            Marker marker2;
            MarkerOptions marker2Options = new MarkerOptions()
                .InvokePosition(new LatLng(41.022231, 29.008118))
                .InvokeTitle("Marker Title #2")
                .InvokeSnippet("Marker Desc #2");
            marker2 = hMap.AddMarker(marker2Options);

            Marker marker3;
            MarkerOptions marker3Options = new MarkerOptions()
                .InvokePosition(new LatLng(41.005784, 28.997364))
                .InvokeTitle("Marker Title #3")
                .InvokeSnippet("Marker Desc #3");
            Bitmap bitmap1 = ResourceBitmapDescriptor.DrawableToBitmap(this, ContextCompat.GetDrawable(this, Resource.Drawable.markerblue));
            marker3Options.InvokeIcon(BitmapDescriptorFactory.FromBitmap(bitmap1));
            marker3 = hMap.AddMarker(marker3Options);

            Marker marker4;
            MarkerOptions marker4Options = new MarkerOptions()
                .InvokePosition(new LatLng(41.028435, 28.988186))
                .InvokeTitle("Marker Title #4")
                .InvokeSnippet("Marker Desc #4")
                .Anchor(0.9F, 0.9F)
                .Draggable(true);
            Bitmap bitmap2 = ResourceBitmapDescriptor.DrawableToBitmap(this, ContextCompat.GetDrawable(this, Resource.Drawable.marker));
            marker4Options.InvokeIcon(BitmapDescriptorFactory.FromBitmap(bitmap2));
            marker4 = hMap.AddMarker(marker4Options);

        }
        private void PolylineDemo(HuaweiMap hMap)
        {
            hMap.Clear();

            Polyline polyline1;
            PolylineOptions polyline1Options = new PolylineOptions()
            .Add(new LatLng(41.01929, 28.967267), new LatLng(41.016785, 28.986971), new LatLng(41.001917, 28.978743), new LatLng(41.002298, 28.954132));
            polyline1Options.InvokeColor(Color.Blue);
            polyline1Options.InvokeWidth(20);
            // polyline1Options.InvokeZIndex(2);
            polyline1Options.Visible(false);
            polyline1Options.Clickable(true);
            polyline1 = hMap.AddPolyline(polyline1Options);


            Polyline polyline2;
            PolylineOptions polyline2Options = new PolylineOptions()
            .Add(new LatLng(41.010410, 28.978511), new LatLng(41.035243, 29.026812), new LatLng(41.022122, 29.00653), new LatLng(41.00415, 29.012449), new LatLng(41.001699, 28.978743));
            polyline2Options.InvokeColor(Color.Red);
            polyline1Options.InvokeZIndex(1);
            polyline2Options.Clickable(true);
            polyline2 = hMap.AddPolyline(polyline2Options);
        }
        private void PolygonDemo(HuaweiMap hMap)
        {
            hMap.Clear();

            Polygon polygon1;
            PolygonOptions polygon1Options = new PolygonOptions()
            .Add(new LatLng(41.01929, 28.967267), new LatLng(41.016785, 28.986971), new LatLng(41.014623, 28.999753), new LatLng(41.001917, 28.978743), new LatLng(41.002298, 28.954132));
            polygon1Options.InvokeFillColor(Color.Argb(60, 255, 200, 0));
            polygon1Options.InvokeStrokeColor(Color.Yellow);
            polygon1Options.InvokeStrokeWidth(30);
            polygon1Options.Clickable(true);
            polygon1Options.InvokeZIndex(2);
            polygon1 = hMap.AddPolygon(polygon1Options);

            Polygon polygon2;
            PolygonOptions polygon2Options = new PolygonOptions()
            .Add(new LatLng(41.035243, 29.026812), new LatLng(41.022122, 29.00653), new LatLng(41.00415, 29.012449), new LatLng(41.001699, 28.978743), new LatLng(41.017384, 28.986827), new LatLng(41.037711, 28.996791));
            polygon2Options.InvokeFillColor(Color.Argb(60, 0, 0, 255));
            polygon2Options.InvokeStrokeColor(Color.Blue);
            polygon2Options.Clickable(true);
            polygon2 = hMap.AddPolygon(polygon2Options);

        }
        private void CircleDemo(HuaweiMap hMap)
        {
            hMap.Clear();

            Circle circle1;
            LatLng circle1LatLng = new LatLng(41.01019, 28.974475);
            CircleOptions circle1Options = new CircleOptions();
            circle1Options.InvokeCenter(circle1LatLng);
            circle1Options.InvokeRadius(1600);
            circle1Options.InvokeStrokeWidth(5);
            circle1Options.InvokeStrokeColor(Color.Blue);
            circle1Options.InvokeFillColor(Color.Argb(60, 0, 0, 255));
            circle1Options.Clickable(true);
            circle1Options.InvokeZIndex(2);
            circle1 = hMap.AddCircle(circle1Options);

            Circle circle2;
            LatLng circle2LatLng = new LatLng(41.01563, 29.052667);
            CircleOptions circle2Options = new CircleOptions();
            circle2Options.InvokeCenter(circle2LatLng);
            circle2Options.InvokeRadius(5000);
            circle2Options.InvokeStrokeWidth(10);
            circle2Options.InvokeStrokeColor(Color.OrangeRed);
            circle2Options.InvokeFillColor(Color.Argb(60, 255, 200, 0));
            circle2Options.Clickable(true);
            circle2Options.InvokeZIndex(1);
            circle2 = hMap.AddCircle(circle2Options);

        }
        private void OverlayDemo(HuaweiMap hMap)
        {
            hMap.Clear();

            GroundOverlay groundOverlay1;
            BitmapDescriptor descriptor1 = BitmapDescriptorFactory.FromResource(Resource.Drawable.hmslogo);
            GroundOverlayOptions groundOverlay1Options = new GroundOverlayOptions()
                .Position(new LatLng(41.010037, 28.9819363), 2400, 2400)
                .InvokeImage(descriptor1);
            groundOverlay1Options.Clickable(true);
            groundOverlay1 = hMap.AddGroundOverlay(groundOverlay1Options);

            GroundOverlay groundOverlay2;
            BitmapDescriptor descriptor2 = BitmapDescriptorFactory.FromResource(Resource.Drawable.maplogo);
            GroundOverlayOptions groundOverlay2Options = new GroundOverlayOptions()
                .Position(new LatLng(41.024204, 29.009406), 2400, 2400)
                .InvokeImage(descriptor2);
            groundOverlay2Options.Clickable(true);
            groundOverlay2 = hMap.AddGroundOverlay(groundOverlay2Options);



        }

        public void OnMarkerClick(object sender, HuaweiMap.MarkerClickEventArgs e)
        {
            Toast.MakeText(this, $"Marker Click Marker ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnMarkerDragStart(object sender, HuaweiMap.MarkerDragStartEventArgs e)
        {
            //Toast.MakeText(this, $"Drag Start Marker ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnMarkerDrag(object sender, HuaweiMap.MarkerDragEventArgs e)
        {
            //Toast.MakeText(this, $"Dragging Marker ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnMarkerDragEnd(object sender, HuaweiMap.MarkerDragEndEventArgs e)
        {
            //Toast.MakeText(this, $"Drag End Marker ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnPolylineClick(object sender, HuaweiMap.PolylineClickEventArgs e)
        {
            Toast.MakeText(this, $"Polyline Click Polyline ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnPolygonClick(object sender, HuaweiMap.PolygonClickEventArgs e)
        {
            Toast.MakeText(this, $"Polygon Click Polygon ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnCircleClick(object sender, HuaweiMap.CircleClickEventArgs e)
        {
            Toast.MakeText(this, $"Circle Click Circle ID: {e.P0.Id}", ToastLength.Short).Show();
        }
        public void OnGroundOverlayClick(object sender, HuaweiMap.GroundOverlayClickEventArgs e)
        {
            Toast.MakeText(this, $"Overlay Click Overlay ID: {e.P0.Id}", ToastLength.Short).Show();
        }
    }
}