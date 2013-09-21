using Cirrious.MvvmCross.ViewModels;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace OpenlyLocal.Core.ViewModels
{
    public class LandingViewModel
        : BaseViewModel
    {
        public string Postcode { get; set; }

        static Regex postcodeRegex = new Regex("^(([gG][iI][rR] {0,}0[aA]{2})|((([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y]?[0-9][0-9]?)|(([a-pr-uwyzA-PR-UWYZ][0-9][a-hjkstuwA-HJKSTUW])|([a-pr-uwyzA-PR-UWYZ][a-hk-yA-HK-Y][0-9][abehmnprv-yABEHMNPRV-Y]))) {0,}[0-9][abd-hjlnp-uw-zABD-HJLNP-UW-Z]{2}))$");
        
        public bool IsValidPostcode {

            get {
                return !(string.IsNullOrWhiteSpace(Postcode)) && postcodeRegex.IsMatch(Postcode);
            }
        }

        public ICommand Search {
            get {
                return new MvxCommand(() => {
                    if (IsValidPostcode)
                    {
                        ShowViewModel<PostcodeViewModel>(new
                        {
                            Postcode = Postcode
                        });
                    }
                    else {
                        ShowAlert("Invalid Postcode");
                    }
                });
            }
        }
    }
}
