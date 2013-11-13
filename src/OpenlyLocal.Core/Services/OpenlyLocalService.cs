using Cirrious.CrossCore.Platform;
using OpenlyLocal.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Services
{
    public class OpenlyLocalService : OpenlyLocal.Core.Services.IOpenlyLocalService
    {
        private Cirrious.MvvmCross.Plugins.Network.Rest.IMvxJsonRestClient _client;
        private Cirrious.MvvmCross.Plugins.Network.Rest.IMvxRestClient _restClient;
       // private Dictionary<string, Models.Postcode> _postcodeCache = new Dictionary<string, Models.Postcode>();
        private Dictionary<string, Models.Ward> _wardCache = new Dictionary<string, Models.Ward>();
        private Dictionary<string, Models.Council> _councilCache = new Dictionary<string, Models.Council>();

        private object _lockObject = new object();
        private IMvxTrace _trace;

        public OpenlyLocalService(Cirrious.MvvmCross.Plugins.Network.Rest.IMvxJsonRestClient client, Cirrious.MvvmCross.Plugins.Network.Rest.IMvxRestClient restclient, IMvxTrace trace)
        {
            _client = client;
            _restClient = restclient;
            _trace = trace;
        }

        private void GetUrl<Trequest, Treturn>(string url, Func<Trequest, Treturn> converter, Action<Treturn> success, Action<Exception> failure)
        {
            _client.MakeRequestFor<Trequest>(
                       new Cirrious.MvvmCross.Plugins.Network.Rest.MvxRestRequest(url),
                       r =>
                       {
                           success(converter(r.Result));
                       },
                      failure);
        }

        private void GetUrl<Trequest>(string url, Action<Trequest> success, Action<Exception> failure)
        {
            GetUrl<Trequest, Trequest>(url, r => r, success, failure);
        }

        private void GetTextUrl(string url, Action<string> success, Action<Exception> failure)
        {
            _restClient.MakeRequest(
                       new Cirrious.MvvmCross.Plugins.Network.Rest.MvxRestRequest(url),
                       r =>
                       {

                           success(new StreamReader(r.Stream).ReadToEnd());
                       },
                      failure);
        }

        //public void GetPostcode(string postcode, Action<Models.Postcode> success, Action<Exception> fail)
        //{

        //    //normalize postcode
        //    postcode = postcode.Replace(" ", "").ToLower();

        //    var url = "http://openlylocal.com/areas/postcodes/" + Uri.EscapeDataString(postcode) + ".json";

        //    _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading {0} as {1}", postcode, url);

        //    if (_postcodeCache.ContainsKey(postcode))
        //    {
        //        success(_postcodeCache[postcode]);
        //    }
        //    else
        //    {
        //        GetUrl<PostcodeRootObject, Postcode>(url, r => r.postcode, p =>
        //        {
        //            lock (_lockObject)
        //            {
        //                if (!_postcodeCache.ContainsKey(postcode))
        //                {
        //                    _postcodeCache.Add(postcode, p);
        //                }
        //            }

        //            //re cache otehr content
        //            success(_postcodeCache[postcode]);

        //        }, fail);
        //    }
        //}

        public void GetWard(int wardid, Action<Models.Ward> success, Action<Exception> fail)
        {

            //normalize postcode
            var wardKey = wardid.ToString();
            var url = "http://scoile.apphb.com/api/tocsoft/OpenlyLocal/Lb4WLZ26oEGXz0oNGgxiXQ?type=ward&id=" + wardKey ;

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading ward {0} as {1}", wardKey, url);

            if (_wardCache.ContainsKey(wardKey))
            {
                success(_wardCache[wardKey]);
            }
            else
            {
                GetUrl<Ward[]>(url, w =>
                {

                    lock (_lockObject)
                    {
                        if (!_wardCache.ContainsKey(wardKey))
                        {
                            _wardCache.Add(wardKey, w.First());
                        }
                    }

                    success(_wardCache[wardKey]);

                }, fail);
            }
        }

        public void GetCouncil(int councilId, Action<Models.Council> success, Action<Exception> fail)
        {

            //normalize postcode
            var councilKey = councilId.ToString();
            var url = "http://scoile.apphb.com/api/tocsoft/OpenlyLocal/Lb4WLZ26oEGXz0oNGgxiXQ?type=council&id=" + councilKey;

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading council {0} as {1}", councilKey, url);

            if (_councilCache.ContainsKey(councilKey))
            {
                success(_councilCache[councilKey]);
            }
            else
            {
                GetUrl<Council[]>(url,  c =>
                {
                    lock (_lockObject)
                    {
                        if (!_councilCache.ContainsKey(councilKey))
                        {
                            _councilCache.Add(councilKey, c.First());
                        }
                    }

                    success(_councilCache[councilKey]);
                  
                }, fail);
            }
        }

        private IEnumerable<Models.CouncilSimple> _councilListCache;
        public void GetCouncilList(Action<IEnumerable<Models.CouncilSimple>> success, Action<Exception> fail)
        {

            //normalize postcode

            var url = "http://scoile.apphb.com/api/tocsoft/OpenlyLocal/Lb4WLZ26oEGXz0oNGgxiXQ";

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading list of councils as {0}", url);

            if (_councilListCache != null)
            {
                success(_councilListCache);
            }
            else
            {
                GetUrl<CouncilSimple[]>(url, c =>
                {

                    lock (_lockObject)
                    {
                        if (_councilListCache == null)
                        {
                            _councilListCache = c;
                        }
                    }

                    
                    success(_councilListCache);

                }, fail);
            }
        }
    }
}
