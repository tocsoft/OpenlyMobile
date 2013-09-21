using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.ViewModels.Home;

namespace OpenlyLocal.Core.ViewModels
{
    public class HomeViewModel
        : BaseViewModel
    {

        public LandingViewModel LandingView = new LandingViewModel();
        public BrowseViewModel BrowseView = new BrowseViewModel();

        public AboutViewModel AboutView = new AboutViewModel();


    }
}