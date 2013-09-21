using Cirrious.CrossCore;
using Cirrious.CrossCore.Core;
using Cirrious.MvvmCross.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Views;
using NUnit.Framework;
using OpenlyLocal.Core.ViewModels;
using OpenlyLocal.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenlyLocal.Tests.ViewModels
{
    public class BaseViewModelTests :
        Cirrious.MvvmCross.Test.Core.MvxIoCSupportingTest
    {
        public Mocks.MockIMvxMessenger messenger;
        public MockDispatcher dispatcher;

        public void Setup()
        {
            base.Setup();

            messenger = new Mocks.MockIMvxMessenger();

            dispatcher = new MockDispatcher();


            Ioc.RegisterSingleton<IMvxStringToTypeParser>(new MvxStringToTypeParser());
            Ioc.RegisterSingleton<IMvxMessenger>(messenger);
            Ioc.RegisterSingleton<IMvxViewDispatcher>(dispatcher);
            Ioc.RegisterSingleton<IMvxMainThreadDispatcher>(dispatcher);

        }

    }
}
