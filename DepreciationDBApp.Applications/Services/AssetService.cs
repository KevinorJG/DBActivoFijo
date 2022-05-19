using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace DepreciationDBApp.Applications.Services
{
    public class AssetService : IAssetService
    {
        private IAssetRepository assetRepository;

        public AssetService(IAssetRepository assetRepository)
        {
            this.assetRepository = assetRepository;
        }
        public void Create(ActivoFijo t)
        {
            if (t == null)
            {
                throw new ArgumentNullException("El Asset no puede ser null.");
            }

            assetRepository.Create(t);
        }

        public bool Delete(ActivoFijo t)
        {
            return assetRepository.Delete(t);
        }

        public ActivoFijo FindByCode(string code)
        {
            return assetRepository.FindByCode(code);
        }

        public ActivoFijo FindById(int id)
        {
            return assetRepository.FindById(id);
        }

        public List<ActivoFijo> FindByName(string name)
        {
            return assetRepository.FindByName(name);
        }

        public List<ActivoFijo> GetAll()
        {
            return assetRepository.GetAll();
        }

        public int Update(ActivoFijo t)
        {
            return assetRepository.Update(t);
        }
    }
}
