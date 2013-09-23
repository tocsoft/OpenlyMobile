using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Views;
using OpenlyLocal.Core.ViewModels;

namespace OpenlyLocal.Droid.Views
{
    [Activity(Label = "Openly Mobile"
        , Theme = "@style/Theme.OpenlyMobile")]
    public class HomeView : ViewBase<HomeViewModel>
    {
        protected override int ContentView { get { return Resource.Layout.HomeView; } }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var searchBox = this.FindViewById<EditText>(Resource.Id.search);


            searchBox.KeyPress += (object sender, Android.Views.View.KeyEventArgs e) =>
            {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    if (CurrentViewModel.Search.CanExecute(null))
                    {
                        CurrentViewModel.Search.Execute(null);
                    }

                    e.Handled = true;
                }
            };
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