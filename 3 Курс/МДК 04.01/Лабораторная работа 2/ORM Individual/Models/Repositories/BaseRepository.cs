using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.Models.Repositories
{
    public abstract class BaseRepository
    {
        public abstract void Add(object entity);
        public abstract void Update(int index);
        public abstract void Delete(int index);
    }
}
