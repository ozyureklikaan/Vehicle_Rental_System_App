using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSystem.DataAccess.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool DeletedById(int id);
        TEntity SelectedById(int id);
        IList<TEntity> SelectAll();
    }
}
