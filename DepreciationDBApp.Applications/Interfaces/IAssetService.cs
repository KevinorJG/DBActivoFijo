using DepreciationDBApp.Domain.Entities;
using System.Collections.Generic;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IAssetService : IService<ActivoFijo>
    {
        ActivoFijo FindById(int id);
        ActivoFijo FindByCode(string code);
        List<ActivoFijo> FindByName(string name);
    }
}
