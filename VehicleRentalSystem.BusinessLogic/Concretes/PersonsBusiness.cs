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
    public class PersonsBusiness : IDisposable
    {
        public PersonsBusiness()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public bool InsertPerson(Persons entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new PersonsRepository())
                {
                    isSuccess = repo.Insert(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:PersonsBusiness::InsertPerson::Error occured.", ex);
            }
        }

        public bool DeletePersonById(int ID)
        {
            try
            {
                bool isSuccess;
                using (var repo = new PersonsRepository())
                {
                    isSuccess = repo.DeletedById(ID);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:PersonsBusiness::DeletePerson::Error occured.", ex);
            }
        }

        public Persons SelectPersonById(int personId)
        {
            try
            {
                Persons responseEntitiy;
                using (var repo = new PersonsRepository())
                {
                    responseEntitiy = repo.SelectedById(personId);
                    if (responseEntitiy == null)
                        throw new NullReferenceException("Person doesnt exists!");
                }
                return responseEntitiy;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:PersonsBusiness::SelectPersonById::Error occured.", ex);
            }
        }

        public List<Persons> SelectAllPersons()
        {
            var responseEntities = new List<Persons>();

            try
            {
                using (var repo = new PersonsRepository())
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
                throw new Exception("BusinessLogic:PersonsBusiness::SelectAllPersons::Error occured.", ex);
            }
        }

        public bool UpdatePerson(Persons entity)
        {
            try
            {
                bool isSuccess;
                using (var repo = new PersonsRepository())
                {
                    isSuccess = repo.Update(entity);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                LogHelper.Log(LogTarget.File, ExceptionHelper.ExceptionToString(ex), true);
                throw new Exception("BusinessLogic:PersonsBusiness::UpdatePerson::Error occured.", ex);
            }
        }
    }
}
