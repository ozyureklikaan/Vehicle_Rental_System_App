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
    public class CompaniesBusiness : IDisposable
    {
        public CompaniesBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool InsertCompany(Companies entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompaniesRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CompaniesBusiness::InsertCompany::Error occured.", ex);
            }
        }

        public bool DeleteCompanyById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompaniesRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CompaniesBusiness::DeleteCompanyById::Error occured.", ex);
            }
        }
        
        public Companies SelectCompanyById (int customerId)
        {
            try
            {
                Companies responseEntitiy;
                using (var repo = new CompaniesRepository())
                {
                    responseEntitiy = repo.SelectedById(customerId);
                    responseEntitiy.Vehicles = new VehiclesRepository().SelectAll().Where(x => x.CompanyID.Equals(responseEntitiy.CompanyID)).ToList();
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Customer doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CompaniesBusiness::SelectCompanyById::Error occured.", ex);
            }
        }

        public List<Companies> SelectAllCompanies()
        {
            var responseEntities = new List<Companies>();

            try
            {
                using (var repo = new CompaniesRepository())
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
                throw new Exception("BusinessLogic:CompaniesBusiness::SelectAllCompanies::Error occured.", ex);
            }
        }

        public bool UpdateCompany(Companies entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new CompaniesRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:CompaniesBusiness::UpdateCompany::Error occured.", ex);
            }
        }
    }
}
