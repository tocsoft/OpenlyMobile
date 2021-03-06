using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Cirrious.MvvmCross.Droid.Fragging;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.Binding.ExtensionMethods;
using OpenlyLocal.Core.ViewModels;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace OpenlyLocal.Droid.Views
{
    [Activity(Label = "Openly Mobile - Council"
        , Theme = "@style/Theme.OpenlyMobile")]
    public class CouncilView : MapViewBase<CouncilViewModel>
    {
        protected override int ContentView { get { return Resource.Layout.CouncilView; } }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var set = this.CreateBindingSet<CouncilView, CouncilViewModel>();
            set.Bind(this).For("Location").To(x => x.Council).OneWay();
            set.Bind(this).For("Wards").To(x => x.Wards).OneWay();
                set.Bind(LegacyBar)
                        .For("Title")
                        .To(x => x.CouncilName);
                set.Apply();

                CurrentViewModel.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == "CouncilName")
                    {
                        if (CurrentViewModel.CouncilName != null)
                            LegacyBar.Title = CurrentViewModel.CouncilName;
                    }
                    else if (e.PropertyName == "Council")
                    {
                        Overlay = CurrentViewModel.Council.GeoJson;
                        //CenterLocation = CurrentViewModel.Council;
                    }
                };
        }



    }
}