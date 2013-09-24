using Cirrious.MvvmCross.ViewModels;
using OpenlyLocal.Core.Models;
using OpenlyLocal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace OpenlyLocal.Core.ViewModels
{
    public class CouncilViewModel : BaseViewModel
    {
        private IOpenlyLocalService _postcodes;
        public CouncilViewModel(IOpenlyLocalService postcodes)
        {
            IsLoading = true;
            _postcodes = postcodes;
        }
        public void Init(Search search)
        {
            //lookup the postcode (if we can)
            _postcodes.GetCouncil(search.Id,
                p =>
                {
                    Council = p;
                    IsLoading = false;
                },
                e =>
                {

                    ShowAlert("Sorry but it looks like we can't find any details about that council.");
                    //TODO 
                    Close(this);
                }
            );
        }
        public string CouncilName
        {

            get {
                if (Council == null)
                    return null;

                return Council.Name;
            }
        }

        public Models.Council Council
        {
            get;
            set;
        }

        public IEnumerable<Ward> Wards
        {
            get {
                if (Council == null)
                    return Enumerable.Empty<Ward>();

                return Council.wards;
            }
        }

        public bool IsLoading
        {
            get;
            set;
        }

        public ICommand ViewWard
        {
            get
            {
                return new MvxCommand<Ward>(c =>
                {
                    if (c != null)
                    {
                        ShowViewModel<WardViewModel>(new WardViewModel.WardSearch { Id = c.id });
                    }
                });
            }
        }

        public class Search
        {
            public int Id { get; set; }
        }
    }
}
