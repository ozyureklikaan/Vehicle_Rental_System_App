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
    public class RentalTransactionsBusiness : IDisposable
    {
        private VehiclesBusiness _vehiclebusiness = new VehiclesBusiness();
        private bool _bDisposed;
        private readonly object _lock = new object();
        private RentalTransactions generalRentalTransaction;

        public RentalTransactionsBusiness()
        {
            _vehiclebusiness = new VehiclesBusiness();
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
                    _vehiclebusiness = null;
                }

                _bDisposed = true;
            }
        }

        public bool InsertRentalTransaction(RentalTransactions entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentalTransactionsRepository())
                {
                    isSuccess = repo.Insert(entity);
                    /*using (var vehicleRepo = new VehiclesRepository())
                    {
                        Vehicles v = vehicleRepo.SelectedById(entity.VehicleID);
                        generalRentalTransaction = v.RentalTransaction.ElementAt(v.RentalTransaction.Count - 1);
                    }*/
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentalTransactionsBusiness::InsertRentalTransaction::Error occured.", ex);
            }
        }

        public bool DeleteRentalTransactionById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentalTransactionsRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentalTransactionsBusiness::DeleteRentalTransactionById::Error occured.", ex);
            }
        }

        public RentalTransactions SelectRentalTransactionleById(int rentalTransactionId)
        {
            try
            {
                RentalTransactions responseEntitiy;
                using (var repo = new RentalTransactionsRepository())
                {
                    responseEntitiy = repo.SelectedById(rentalTransactionId);
                    responseEntitiy.Vehicles = new VehiclesRepository().SelectAll().Where(x => x.VehicleID.Equals(responseEntitiy.VehicleID)).ToList();
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Rental Transaction doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:RentalTransactionsBusiness::RentalTransactions::Error occured.", ex);
            }
        }

        public List<RentalTransactions> SelectAllRentalTransaction()
        {
            var responseEntities = new List<RentalTransactions>();

            try
            {
                using (var repo = new RentalTransactionsRepository())
                {
                    foreach (var entity in repo.SelectAll())
                    {
                        responseEntities.Add(entity);
                    }
                }
                return responseEntities;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateRentalTransaction(RentalTransactions rentalTransactions)
        {
            try
            {
                bool isSuccess;
                using (var repo = new RentalTransactionsRepository())
                {
                    isSuccess = repo.Update(rentalTransactions);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw new Exception("BusinessLogic:RentalTransactionsBusiness::UpdateRentalTransaction::Error occured.", ex);
            }
        }
    }
}
