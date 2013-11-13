using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using OpenlyLocal.Core.ViewModels;

namespace OpenlyLocal.Droid.Views
{
    

    [Activity(Label = "Openly Mobile - Council"
        , Theme = "@style/Theme.OpenlyMobile")]
    public class WardView : MapViewBase<WardViewModel>
    {
        protected override int ContentView { get { return Resource.Layout.Page_WardView; } }
        

        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            CurrentViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "WardName")
                {
                    if (CurrentViewModel.WardName != null)
                        LegacyBar.Title = CurrentViewModel.WardName;
                }else 
                if (e.PropertyName == "Ward")
                {
                    if (CurrentViewModel.Ward != null)
                        Overlay = CurrentViewModel.Ward.BoundaryLine;
                }
            };
            
        }



    }
}