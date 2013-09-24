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
        private Dictionary<string, Models.Postcode> _postcodeCache = new Dictionary<string, Models.Postcode>();
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

        public void GetPostcode(string postcode, Action<Models.Postcode> success, Action<Exception> fail)
        {

            //normalize postcode
            postcode = postcode.Replace(" ", "").ToLower();

            var url = "http://openlylocal.com/areas/postcodes/" + Uri.EscapeDataString(postcode) + ".json";

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading {0} as {1}", postcode, url);

            if (_postcodeCache.ContainsKey(postcode))
            {
                success(_postcodeCache[postcode]);
            }
            else
            {
                GetUrl<PostcodeRootObject, Postcode>(url, r => r.postcode, p =>
                {
                    lock (_lockObject)
                    {
                        if (!_postcodeCache.ContainsKey(postcode))
                        {
                            _postcodeCache.Add(postcode, p);
                        }
                    }

                    //re cache otehr content
                    success(_postcodeCache[postcode]);

                }, fail);
            }
        }

        public void GetWard(int wardid, Action<Models.Ward> success, Action<Exception> fail)
        {

            //normalize postcode
            var wardKey = wardid.ToString();
            var url = "http://openlylocal.com/wards/" + wardKey + ".json";

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading ward {0} as {1}", wardKey, url);

            if (_wardCache.ContainsKey(wardKey))
            {
                success(_wardCache[wardKey]);
            }
            else
            {
                GetUrl<WardRootObject, Ward>(url, r => r.ward, w =>
                {

                    lock (_lockObject)
                    {
                        if (!_wardCache.ContainsKey(wardKey))
                        {
                            _wardCache.Add(wardKey, w);
                        }
                        else
                            success(_wardCache[wardKey]);
                    }


                    GetUrl<GeoJson>("http://mapit.mysociety.org/area/" + w.snac_id + ".geojson", g =>
                    {
                        //set the GeoGason for this council
                        w.GeoJson = g;

                        success(_wardCache[wardKey]);
                    }, fail);


                }, fail);
            }
        }

        public void GetCouncil(int councilId, Action<Models.Council> success, Action<Exception> fail)
        {

            //normalize postcode
            var councilKey = councilId.ToString();
            var url = "http://openlylocal.com/councils/" + councilKey + ".json";

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading council {0} as {1}", councilKey, url);

            if (_councilCache.ContainsKey(councilKey))
            {
                success(_councilCache[councilKey]);
            }
            else
            {
                GetUrl<CouncilRootObject, Council>(url, r => r.council, c =>
                {

                    lock (_lockObject)
                    {
                        if (!_councilCache.ContainsKey(councilKey))
                        {
                            _councilCache.Add(councilKey, c);
                        }
                        else
                        {
                            //all downloaded in the past
                            success(_councilCache[councilKey]);
                        }
                    }

                    //TODO download polygon data from http://mapit.mysociety.org/area/{c.snac_id}.html
                    var geourl = "http://mapit.mysociety.org/area/" + c.snac_id + ".geojson";
                    _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading {0}", geourl);
                    GetTextUrl(geourl, geoJson =>
                    {

                        var converter = _client.JsonConverterProvider();
                        GeoJson geo = null;
                        try
                        {
                            geo = converter.DeserializeObject<Polygon>(geoJson);
                        }
                        catch(Exception ex)
                        {

                        }

                        if (geo == null) {
                            try
                            {
                                geo = converter.DeserializeObject<MultiPolygon>(geoJson);
                            }
                            catch (Exception e)
                            {
                                fail(e);
                                return;
                            }
                        }
                        _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loaded geodata from {0}", geourl);
                        //_trace.Trace(MvxTraceLevel.Diagnostic, "data", g);
                        //set the GeoGason for this council
                        c.GeoJson = geo;

                        success(_councilCache[councilKey]);
                    }, e => { 
                        _trace.Trace(MvxTraceLevel.Diagnostic, "data", "failed geodata from {0}", geourl);
                        success(_councilCache[councilKey]);
                    });


                }, fail);
            }
        }

        private IEnumerable<Models.Council> _councilListCache;
        public void GetCouncilList( Action<IEnumerable<Models.Council>> success, Action<Exception> fail)
        {

            //normalize postcode
            
            var url = "http://openlylocal.com/councils.json";

            _trace.Trace(MvxTraceLevel.Diagnostic, "data", "loading list of councils as {0}", url);

            if (_councilListCache != null)
            {
                success(_councilListCache);
            }
            else
            {
                GetUrl<CouncilsRootObject, Council[]>(url, r => r.councils, c =>
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
