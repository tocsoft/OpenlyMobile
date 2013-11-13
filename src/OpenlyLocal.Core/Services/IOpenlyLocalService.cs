using System;
using System.Collections.Generic;
namespace OpenlyLocal.Core.Services
{
    public interface IOpenlyLocalService
    {
        void GetCouncil(int councilId, Action<OpenlyLocal.Core.Models.Council> success, Action<Exception> fail);
        //void GetPostcode(string postcode, Action<OpenlyLocal.Core.Models.Postcode> success, Action<Exception> fail);
        void GetWard(int wardid, Action<OpenlyLocal.Core.Models.Ward> success, Action<Exception> fail);
        void GetCouncilList(Action<IEnumerable<Models.CouncilSimple>> success, Action<Exception> fail);
    }
}
