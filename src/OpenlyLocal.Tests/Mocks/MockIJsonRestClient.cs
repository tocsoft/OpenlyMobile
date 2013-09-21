using Cirrious.MvvmCross.Plugins.Network.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenlyLocal.Tests
{
    public class MockIJsonRestClient : Cirrious.MvvmCross.Plugins.Network.Rest.IMvxJsonRestClient
    {

        public MockIJsonRestClient() { }


        public Func<MvxRestRequest, object> RequestIntercepter { get; set; }

        public Func<Cirrious.CrossCore.Platform.IMvxJsonConverter> JsonConverterProvider
        {
            get;
            set;
        }

        public void MakeRequestFor<T>(Cirrious.MvvmCross.Plugins.Network.Rest.MvxRestRequest restRequest,
            Action<Cirrious.MvvmCross.Plugins.Network.Rest.MvxDecodedRestResponse<T>> successAction, 
            Action<Exception> errorAction)
        {

            try
            {
                successAction(new MvxDecodedRestResponse<T>()
                {
                    Result = (T)RequestIntercepter(restRequest)
                });
            }
            catch (Exception e) {
                errorAction(e);
            }
        }
    }
}
