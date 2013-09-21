using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using OpenlyLocal.Core.ViewModels;

namespace OpenlyLocal.Droid.Views
{
    [Activity(Label = "OpenlyMobile")]
    public class HomeView : MvxTabActivity
    {

        protected HomeViewModel HomeViewModel
        {
            get { return base.ViewModel as HomeViewModel; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.HomeView);

            var spec = TabHost.NewTabSpec("landing");
            spec.SetIndicator("1");
            spec.SetContent(this.CreateIntentFor(HomeViewModel.LandingView));
            TabHost.AddTab(spec);


            spec = TabHost.NewTabSpec("browse");
            spec.SetIndicator("2");
            spec.SetContent(this.CreateIntentFor(HomeViewModel.BrowseView));
            TabHost.AddTab(spec);

            spec = TabHost.NewTabSpec("about");
            spec.SetIndicator("3");
            spec.SetContent(this.CreateIntentFor(HomeViewModel.AboutView));
            TabHost.AddTab(spec);


        }
    }

    [Activity(Label = "Openly Mobile")]
    public class LandingView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LandingView);
        }
    }

    [Activity(Label = "Browse Councils")]
    public class BrowseView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.BrowseView);
        }
    }

    [Activity(Label = "About Openly Mobile")]
    public class AboutView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AboutView);
        }
    }
}