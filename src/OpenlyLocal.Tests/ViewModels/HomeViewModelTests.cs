using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using NUnit.Framework;
using OpenlyLocal.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenlyLocal.Tests.ViewModels
{
    [TestFixture]
    public class HomeViewModelTests : BaseViewModelTests
    {
        private HomeViewModel _homeView;
        
        [SetUp]
        public  void SetupTest() {
            base.Setup();

       
            _homeView = new HomeViewModel();
        }

        [Test]
        public void LandingViewIsPopulated()
        {

            Assert.IsNotNull(_homeView.LandingView);
        }

        [Test]
        public void BrowseViewIsPopulated()
        {
            Assert.IsNotNull(_homeView.BrowseView);
        }
    }
}
