using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRentalSystem.Commons.Concretes.Helpers;
using VehicleRentalSystem.Commons.Concretes.Logger;
using VehicleRentalSystem.DataAccess.Concretes;
using VehicleRentalSystem.Models.Concretes;

namespace VehicleRentalSystem.BusinessLogic.Concretes
{
    public class VehiclesBusiness : IDisposable
    {
        private CompaniesBusiness _companybusiness = new CompaniesBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();
        private Vehicles generalVehicle;

        public VehiclesBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    _companybusiness = null;
                }

                _bDisposed = true;
            }
        }

        public bool InsertVehicle(Vehicles entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new VehiclesRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:VehiclesBusiness::InsertVehicle::Error occured.", ex);
            }
        }

        public bool DeleteVehicleById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new VehiclesRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:VehiclesBusiness::DeleteVehicleById::Error occured.", ex);
            }
        }

        public Vehicles SelectVehicleById(int vehicleId)
        {
            try
            {
                Vehicles responseEntitiy;
                using (var repo = new VehiclesRepository())
                {
                    responseEntitiy = repo.SelectedById(vehicleId);
                    responseEntitiy.Company = new CompaniesRepository().SelectedById(responseEntitiy.CompanyID);
                    responseEntitiy.RentalTransaction = new RentalTransactionsRepository().SelectAll().Where(x => x.VehicleID.Equals(responseEntitiy.VehicleID)).ToList();
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Vehicle doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:VehiclesBusiness::SelectVehicleById::Error occured.", ex);
            }
        }

        public List<Vehicles> SelectAllVehicles()
        {
            var responseEntities = new List<Vehicles>();

            try
            {
                using (var repo = new VehiclesRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:VehiclesBusiness::SelectAllVehicles::Error occured.", ex);
            }
        }

        public bool UpdateVehicle(Vehicles entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new VehiclesRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:VehiclesBusiness::UpdateVehicle::Error occured.", ex);
            }
        }
    }
}
