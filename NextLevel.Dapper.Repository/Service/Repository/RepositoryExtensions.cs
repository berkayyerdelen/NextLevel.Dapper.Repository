using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public static class RepositoryExtensions<TEntity>
    {
        public static Dictionary<string,object> GetProperties(TEntity entity)
        {

            IList<PropertyInfo> props = new List<PropertyInfo>(entity.GetType().GetProperties());

            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(entity, null);

            }
           
        }
    }
}