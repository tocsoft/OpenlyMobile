using Cirrious.CrossCore;
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

        public BaseViewModel()
        {
            _messenger = Mvx.Resolve<IMvxMessenger>();
            
        }

        protected void ShowAlert(string message) {
            _messenger.Publish(new ShowAlertMessage(this, message));
        }

        public class ShowAlertMessage : MvxMessage {


            public string Message { get; set; }
            public ShowAlertMessage(object sender, string msg) : base(sender) { Message = msg; }
        }
    }
}
