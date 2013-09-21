﻿using Cirrious.CrossCore;
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
    public class LandingViewModelTests : BaseViewModelTests
    {
        private LandingViewModel _view;
        
        [SetUp]
        public void SetupTest() {
            base.Setup();

            _view = new LandingViewModel();
        }



        [Test]
        [TestCase("CW3 9SS", true)]
        [TestCase("SE5 0EG", true)]
        [TestCase("SE50EG", true)]
        [TestCase("se5 0eg", true)]
        [TestCase("WC2H 7LT", true)]
        [TestCase("aWC2H 7LT", false)]
        [TestCase("WC2H 7LTa", false)]
        [TestCase("WC2H", false)]
        public void IsValidPostcodeInFalseWithInvalidePostcode(string postcode, bool isValid)
        {
            _view.Postcode = postcode;
            Assert.AreEqual(isValid, _view.IsValidPostcode);
        }

        [Test]
        public void PreventNavigationWhenSearchingInvalidPostcode()
        {

            _view.Postcode = "WC2H";
            _view.Search.Execute((object)null);

            Assert.AreEqual(0, dispatcher.Requests.Count);
        }

        [Test]
        public void ShowMessageFiredOnInvalidPostcodeSearch()
        {

            _view.Postcode = "WC2H";
            _view.Search.Execute((object)null);

            Assert.AreEqual(1, messenger.Messages.OfType<OpenlyLocal.Core.ViewModels.BaseViewModel.ShowAlertMessage>().Count());
        }

        [Test]
        public void NavigatedToPostcodeViewModelWhenSearchingValidPostcode()
        {

            _view.Postcode = "CH41 4LZ";
            _view.Search.Execute((object)null);

            Assert.AreEqual(1, dispatcher.Requests.Count);
        }
    }
}