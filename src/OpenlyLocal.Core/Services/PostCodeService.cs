using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Services
{
    public class PostCodeService : OpenlyLocal.Core.Services.IPostCodeService
    {
        private  Cirrious.MvvmCross.Plugins.Network.Rest.IMvxJsonRestClient _client;
        public PostCodeService(Cirrious.MvvmCross.Plugins.Network.Rest.IMvxJsonRestClient client)
        {
            _client = client;
        }

        Dictionary<string, Models.Postcode> _cache = new Dictionary<string, Models.Postcode>();
        object _lockObject = new object();
        public void GetPostcode(string postcode, Action<Models.Postcode> success, Action<Exception> fail){

            //normalize postcode
            postcode = postcode.Replace(" ", "").ToLower();


            var url = "http://openlylocal.com/areas/postcodes/"+Uri.EscapeDataString(postcode)+".json";


            var containsKey = false; 
            lock (_lockObject)
            {
                containsKey = (_cache.ContainsKey(postcode));
            }

            //return from cache
            if (containsKey)
            {
                lock (_lockObject)
                {
                    success(_cache[postcode]);
                }
            }
            else
            {
                _client.MakeRequestFor<Models.PostcodeRootObject>(
                    new Cirrious.MvvmCross.Plugins.Network.Rest.MvxRestRequest(url),
                    r => {

                        lock (_lockObject)
                        {
                            if (!_cache.ContainsKey(postcode))
                            {
                                _cache.Add(postcode, r.Result.postcode);
                            }

                            success(_cache[postcode]);
                        }
                    },
                    fail);
            }
        }
    }
}
