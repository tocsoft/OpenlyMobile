using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.ViewModels
{
    public class PostcodeViewModel : BaseViewModel
    {
        private IPostCodeService _postcodes;
        public PostcodeViewModel(IPostCodeService postcodes)
        {
            Loading = true;
            _postcodes = postcodes;
        }

        public void Init(string postcode){
            //lookup the postcode (if we can)
            _postcodes.GetPostcode(postcode,
                p => {
                    Postcode = p;
                    Loading = false;
                },
                e => {
                    
                    ShowAlert("Sorry but it looks like we can't find any details about that postcode.");
                    //TODO 
                    Close(this); 
                }
            );
        }

        public Models.Postcode Postcode
        {
            get;
            set;
        }
        public bool Loading
        {
            get;
            set;
        }
    }
}
