﻿using System.Collections.Generic;
using System.Reflection;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public static class RepositoryExtensions<TEntity>
    {
        public static Dictionary<string, string> GetProperties(TEntity entity)
        {
            var listOfProperties = new Dictionary<string, string>();
            IList<PropertyInfo> props = new List<PropertyInfo>(entity.GetType().GetProperties());
            foreach (var prop in props)
            {
                var propValue = prop.GetValue(entity,null)?.ToString();
                listOfProperties.Add(prop.Name,propValue);
            }
            return listOfProperties;
        }
    }
}