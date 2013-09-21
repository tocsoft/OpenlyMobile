using System;
namespace OpenlyLocal.Core.Services
{
    public interface IPostCodeService
    {
        void GetPostcode(string postcode, Action<OpenlyLocal.Core.Models.Postcode> success, Action<Exception> fail);
    }
}
