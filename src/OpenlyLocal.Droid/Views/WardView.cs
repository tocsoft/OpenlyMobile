using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using OpenlyLocal.Core.ViewModels;

namespace OpenlyLocal.Droid.Views
{
    [Activity(Label = "Openly Mobile - Ward"
        , Theme = "@style/Theme.OpenlyMobile")]
    public class WardView : MvxActivity
    {

        protected WardViewModel WardViewModel
        {
            get { return base.ViewModel as WardViewModel; }
        }

        public LegacyBar.Library.Bar.LegacyBar LegacyBar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Page_WardView);

            LegacyBar = this.FindViewById<global::LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

            WardViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "WardName")
                {
                    if (WardViewModel.WardName != null)
                        LegacyBar.Title = WardViewModel.WardName;
                }
            };
            
        }



    }
}