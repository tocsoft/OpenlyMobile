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
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Droid.Fragging;
using Android.Gms.Maps;
using Android.Support.V4.App;
using OpenlyLocal.Core.Models;
using Android.Gms.Maps.Model;
using Android.Graphics;

namespace OpenlyLocal.Droid.Views
{
    public abstract class ViewBase<TViewModel> : MvxFragmentActivity
    {
        protected TViewModel CurrentViewModel
        {
            get { return (TViewModel)base.ViewModel; }
        }

        protected abstract int ContentView { get; }

        public LegacyBar.Library.Bar.LegacyBar LegacyBar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(ContentView);

            LegacyBar = Window.FindViewById<LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);
        }

    }

    public abstract class MapViewBase<TViewModel> : ViewBase<TViewModel>
    {
        private SupportMapFragment _mapFragment;
        private GoogleMap _map;

        ILocation _centerLocation;
        public ILocation CenterLocation
        {
            get
            {
                return _centerLocation;
            }
            set
            {
                _centerLocation = value;
                UpdateCenterLocation();
            }
        }

        GeoJson _overlay;
        public GeoJson Overlay
        {
            get
            {
                return _overlay;
            }
            set
            {
                _overlay = value;
                UpdateOverlay();
            }
        }

        IEnumerable<ILocation> _locations;
        public IEnumerable<ILocation> Locations
        {
            get
            {
                return _locations;
            }
            set
            {
                _locations = value;
                UpdateLocationMarkers();
            }
        }
        private void UpdateOverlay()
        {
            if (_map != null)
            {
                _map.Clear();
                if (_overlay != null)
                {
                    var bld = new LatLngBounds.Builder();
                    foreach (var polygon in _overlay.Polygons)
                    {

                        PolygonOptions poly = new PolygonOptions();
                        foreach (var point in polygon)
                        {
                            var lnglat = new LatLng(point.Lat, point.Lng);
                            poly.Add(lnglat);
                            bld.Include(lnglat);
                        }
                        poly.InvokeStrokeWidth(0.1f);
                        poly.InvokeFillColor(new Color(Color.Aquamarine.R, Color.Aquamarine.G, Color.Aquamarine.B, (byte)128));
                        _map.AddPolygon(poly);
                    }

                    if (_centerLocation == null)
                    {
                        try
                        {
                            _map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bld.Build(), 100));
                        }
                        catch
                        {
                            _map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bld.Build(),400, 400, 100));
                        }
                    }
                }
            }
        }

        private void UpdateLocationMarkers()
        {
            if (_map != null)
            {
                _map.Clear();
                if (_locations != null)
                {
                    var bld = new LatLngBounds.Builder();
                    foreach (var loc in _locations)
                    {
                        MarkerOptions marker1 = new MarkerOptions();
                        var latlng = new LatLng(loc.Lat, loc.Lng);
                        bld.Include(latlng);
                        marker1.SetPosition(latlng);

                        if (loc is INamedLocation)
                        {
                            marker1.SetTitle(((INamedLocation)loc).Name);
                        }
                        _map.AddMarker(marker1);

                    }
                    if (_centerLocation == null)
                    {
                        _map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bld.Build(), 10));
                    }
                }

            }

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            InitMapFragment();
            SetupMapIfNeeded(); // It's not gauranteed that the map will be available at this point.

        }

        private void UpdateCenterLocation() {
            if (_map != null)
            {
                if (_centerLocation != null)
                {
                    // We create an instance of CameraUpdate, and move the map to it.
                    CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(new Android.Gms.Maps.Model.LatLng(_centerLocation.Lat, _centerLocation.Lng), 15);
                    _map.MoveCamera(cameraUpdate);
                }
            }
        }
        private void SetupMapIfNeeded()
        {
            if (_map == null)
            {
                _map = _mapFragment.Map;
                UpdateCenterLocation();
                UpdateLocationMarkers();
            }
        }


        private void InitMapFragment()
        {
            _mapFragment = SupportFragmentManager.FindFragmentById(Resource.Id.map) as SupportMapFragment;
            
            //if (_mapFragment == null)
            //{
            //    GoogleMapOptions mapOptions = new GoogleMapOptions()
            //        .InvokeMapType(GoogleMap.MapTypeSatellite)
            //            .InvokeZoomControlsEnabled(false)
            //            .InvokeCompassEnabled(true);

            //    FragmentTransaction fragTx = SupportFragmentManager.BeginTransaction();
            //    _mapFragment = SupportMapFragment.NewInstance(mapOptions);
            //    fragTx.Add(Resource.Id.map, _mapFragment, "map");
            //    fragTx.Commit();
            //}
        }
        protected override void OnResume()
        {
            base.OnResume();
            SetupMapIfNeeded();
        }


    }
}