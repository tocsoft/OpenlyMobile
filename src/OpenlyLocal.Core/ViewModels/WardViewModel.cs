using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.ViewModels
{
    public class WardViewModel : BaseViewModel
    {
        private IOpenlyLocalService _postcodes;
        public WardViewModel(IOpenlyLocalService postcodes)
        {
            IsLoading = true;
            _postcodes = postcodes;
        }
        public void Init(WardSearch search)
        {
            //lookup the postcode (if we can)
            _postcodes.GetWard(search.Id,
                p =>
                {
                    Ward = p;
                    WardName = p.Name;
                    IsLoading = false;
                },
                e =>
                {

                    ShowAlert("Sorry but it looks like we can't find any details about that postcode.");
                    //TODO 
                    Close(this);
                }
            );
        }

        public string WardName
        {
            get;
            set;
        }

        public Models.Ward Ward
        {
            get;
            set;
        }

        public bool IsLoading
        {
            get;
            set;
        }


        public class WardSearch
        {
            public int Id { get; set; }
        }
    }
}
