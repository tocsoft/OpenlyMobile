using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.Services;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OpenlyLocal.Core.Models;
using System.Threading;
using System;
using OpenlyLocal.Core.Extensions;

namespace OpenlyLocal.Core.ViewModels
{
    public class HomeViewModel
        : BaseViewModel
    {
        static Regex postcodeRegex = new Regex("^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$");
        private IOpenlyLocalService _postcodes;
        
        public HomeViewModel(IOpenlyLocalService postcodes)
        {
            _postcodes = postcodes;
            AllCouncils = new ObservableCollection<Models.CouncilSimple>();
            ReloadCouncils();

            new Timer(s=>{
                


            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }
        public string SearchTerm { get; set; }

    
        public bool IsValidPostcode
        {

            get
            {
                return !(string.IsNullOrEmpty(SearchTerm)) && postcodeRegex.IsMatch(SearchTerm);
            }
        }

        private void ReloadCouncils() {
            IsLoading = true;
            LoadingError = false;
            _postcodes.GetCouncilList(councils =>
            {
                IsLoading = false;
                AllCouncils = councils;
            },
            f =>
            {
                IsLoading = false;
                LoadingError = true;
                throw f;
            });
        }



        public bool IsLoading { get; set; }
        public bool LoadingError { get; set; }

        public IEnumerable<Models.CouncilSimple> AllCouncils { get; set; }
        public IEnumerable<Models.CouncilSimple> Councils
        {
            get
            {
                if (AllCouncils == null)
                    return Enumerable.Empty<Models.CouncilSimple>();
                if (string.IsNullOrEmpty(SearchTerm))
                    return AllCouncils;

                
                return AllCouncils.Where(x => x.Name.ToLower().ContainsAll(SearchTerm.ToLower().Split(' ').ToArray()));
            }
        }

        public ICommand Search
        {
            get
            {
                return new MvxCommand(() =>
                {
                    //if (IsValidPostcode)
                    //{
                    //    ShowViewModel<PostcodeViewModel>(new PostcodeViewModel.PostcodeSearch { Postcode = SearchTerm });
                    //}
                    //else
                    //{
                    //    ShowAlert("Invalid Postcode");
                    //}
                });
            }
        }
        public ICommand ViewCouncil
        {
            get
            {
                return new MvxCommand<CouncilSimple>(c =>
                {
                    if (c != null)
                    {
                        ShowViewModel<CouncilViewModel>(new CouncilViewModel.Search { Id = c.Id });
                    }
                });
            }
        }
        
        public ICommand Browse
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<BrowseViewModel>();
                });
            }
        }

        public ICommand About
        {
            get
            {
                return new MvxCommand(() =>
                {
                    ShowViewModel<AboutViewModel>();
                });
            }
        }



    }
}