using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Huawei.Hms.Maps;
using Com.Huawei.Hms.Maps.Model;

namespace HMS_Map_Demo
{
    class CustomMapInfoWindow : Java.Lang.Object, HuaweiMap.IInfoWindowAdapter
    {
        private Activity m_context;
        private View m_View;
        private Marker m_currentMarker;

        public CustomMapInfoWindow(Activity activity)
        {
            m_context = activity;
            m_View = m_context.LayoutInflater.Inflate(Resource.Layout.custom_infowindow, null);
        }
        public View GetInfoContents(Marker marker)
        {
            return null;
        }

        public View GetInfoWindow(Marker marker)
        {
            if (marker == null)
                return null;

            if (marker.Id != "Marker103")
                return null;

            m_currentMarker = marker;

            ImageView imageview = m_View.FindViewById<ImageView>(Resource.Id.customInfoImage);
            TextView textviewTitle = m_View.FindViewById<TextView>(Resource.Id.customInfoTitle);
            TextView textviewDescription = m_View.FindViewById<TextView>(Resource.Id.customInfoDescription);
            RatingBar ratingBar = m_View.FindViewById<RatingBar>(Resource.Id.customInfoRatingBar);

            if (marker.Title != null)
                imageview.SetImageResource(Resource.Drawable.maplogo);

            textviewTitle.Text = marker.Title;

            textviewDescription.Text = marker.Snippet;

            ratingBar.Rating = 5;

            return m_View;
        }
    }
}