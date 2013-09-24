using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private IMvxMessenger _messenger;
        IMvxTrace _trace;
        public BaseViewModel()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
            _trace = Mvx.Resolve<IMvxTrace>();
        }

        protected void ShowAlert(string message) {
            _trace.Trace(MvxTraceLevel.Diagnostic, "ShowAlert", message);
            _messenger.Publish(new ShowAlertMessage(this, message));
        }

        public class ShowAlertMessage : MvxMessage {
            public string Message { get; set; }
            public ShowAlertMessage(object sender, string msg) : base(sender) { Message = msg; }
        }
    }
}
