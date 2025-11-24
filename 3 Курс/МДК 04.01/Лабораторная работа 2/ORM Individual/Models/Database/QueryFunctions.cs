using ORM_Individual.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//- на выборку(2 запроса с различными условиями), 
//- на использование статистических функций(1 запрос),
//- на соединение таблиц. 


namespace ORM_Individual.Models.Database
{
    public static class QueryFunctions
    {
        public static ObservableCollection<IEntity> idQueries(ObservableCollection<IEntity> entities, int IdMoreThan, int IdLessThan)
        {
            return new ObservableCollection<IEntity>(
                from entity in entities
                where entity.Id > IdMoreThan && entity.Id < IdLessThan
                select entity
                );

        }
    }
}
