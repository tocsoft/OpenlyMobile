using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;

namespace OpenlyLocal.Core.ViewModels
{
    public class HomeViewModel
        : BaseViewModel
    {
        
        public LandingViewModel LandingView = new LandingViewModel();
        public BrowseViewModel BrowseView = new BrowseViewModel();

        public string AboutText = @"OpenlyMobile is a client that serves up the great information provided at http://openlylocal.com.

OpenlyMobile is in no way affilated with Openlylocal and would like to thank them for providing such a welth of information.";
    }
}
