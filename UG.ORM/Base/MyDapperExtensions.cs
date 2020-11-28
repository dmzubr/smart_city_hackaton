using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

using Dapper;

namespace UG.ORM.Base
{
    public static class SqlMapperExtension
    {
        public static async Task<IEnumerable<T>> QueryAndCloseConnAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = default(int?), CommandType? commandType = default(CommandType?))
        {            
            var res = await cnn.QueryAsync<T>(
                sql: sql,
                param: param,
                transaction: transaction,
                commandTimeout: commandTimeout,
                commandType: commandType);

            cnn.Close();
            return res;
        }
    }
}