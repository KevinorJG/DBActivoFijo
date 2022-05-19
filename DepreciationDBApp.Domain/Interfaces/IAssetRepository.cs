using DepreciationDBApp.Domain.Entities;
using System.Collections.Generic;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IAssetRepository : IRepository<ActivoFijo>
    {
        ActivoFijo FindById(int id);
        ActivoFijo FindByCode(string code);
        List<ActivoFijo> FindByName(string name);
    }
}
