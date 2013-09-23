using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using OpenlyLocal.Core.ViewModels;

namespace OpenlyLocal.Droid.Views
{
    [Activity(Label = "Openly Mobile - Postcode"
        , Theme = "@style/Theme.OpenlyMobile")]
    public class PostcodeView : MvxActivity
    {

        protected PostcodeViewModel PostcodeViewModel
        {
            get { return base.ViewModel as PostcodeViewModel; }
        }

        public LegacyBar.Library.Bar.LegacyBar LegacyBar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PostcodeView);
            LegacyBar = this.FindViewById<global::LegacyBar.Library.Bar.LegacyBar>(Resource.Id.actionbar);

            PostcodeViewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Postcode")
                {
                    if (PostcodeViewModel.Postcode != null)
                        LegacyBar.Title = PostcodeViewModel.Postcode;
                }
            };

            
        }



    }
}