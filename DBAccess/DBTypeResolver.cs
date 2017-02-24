using System;
using System.Collections.Generic;
using System.Data;

namespace DBAccess
{
    class DBTypeResolver
    {
        public static DbType Resolve(Type givenType)
        {
            var types = new Dictionary<Type, DbType>();
            types.Add(typeof(string), DbType.String);
            types.Add(typeof(int), DbType.Int32);
            types.Add(typeof(double), DbType.Double);
            types.Add(typeof(DateTime), DbType.DateTime);
            types.Add(typeof(bool), DbType.Boolean);
            if (types.ContainsKey(givenType))
            {
                return types[givenType];
            }
            return DbType.String;
        }
    }
}
