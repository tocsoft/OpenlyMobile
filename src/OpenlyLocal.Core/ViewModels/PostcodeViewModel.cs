using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace OpenlyLocal.Core.ViewModels
{
    public class PostcodeViewModel : BaseViewModel
    {
        private IOpenlyLocalService _postcodes;
        public Models.Postcode PostcodeData { get; set; }
        public PostcodeViewModel(IOpenlyLocalService postcodes)
        {
            IsLoading = true;
            _postcodes = postcodes;
        }
        public void Init(PostcodeSearch search)
        {
            //lookup the postcode (if we can)
            _postcodes.GetPostcode(search.Postcode,
                p =>
                {
                    PostcodeData = p;

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

        public string ImageUrl {
            get {
                if (PostcodeData == null)
                    return null;
                return string.Format("http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom=13&size=600x300&sensor=false", PostcodeData.lat, PostcodeData.lng);
            }
        }

        public string WardName
        {
            get {
                if (PostcodeData == null)
                    return null;
                return PostcodeData.ward.Name;
            }
        }

        public string CouncilName
        {
            get
            {
                if (PostcodeData == null)
                    return null;
                return PostcodeData.ward.council.Name;
            }
        }
        
        public string Postcode
        {
            get
            {
                if (PostcodeData == null)
                    return null;
                return PostcodeData.code;
            }
        }

        public bool IsLoading
        {
            get;
            set;
        }


        public class PostcodeSearch
        {
            public string Postcode { get; set; }
        }

        public ICommand ViewWard
        {
            get
            {
                return new MvxCommand(() =>
                {
                    if (PostcodeData != null)
                        ShowViewModel<WardViewModel>(new WardViewModel.WardSearch { Id = PostcodeData.ward_id });
                });
            }
        }
        public ICommand ViewCouncil
        {
            get
            {
                return new MvxCommand(() =>
                {
                    if (PostcodeData != null)
                        ShowViewModel<CouncilViewModel>(new CouncilViewModel.Search { Id = PostcodeData.council_id });
                });
            }
        }
    }
}
