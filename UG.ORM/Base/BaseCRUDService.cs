using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Dapper.Contrib.Extensions;

using UG.Configuration;
using UG.ORM.Base;

namespace UG.ORM.Base
{    
    public class BaseCRUDService<T> : ISimpleCRUDService<T> where T: class
    {
        private readonly string _connStr;

        public BaseCRUDService(IOptions<ConnectionStringsConfiguration> optionsAccessor)
        {
            this._connStr = optionsAccessor.Value.DB;            
        }

        public BaseCRUDService(string connStr)
        {
            this._connStr = connStr;
        }

        protected MySqlConnection GetMySqlConnection(bool open = true,
            bool convertZeroDatetime = false, bool allowZeroDatetime = false)
        {
            string cs = this._connStr;
            var csb = new MySqlConnectionStringBuilder(cs);
            csb.AllowZeroDateTime = allowZeroDatetime;
            csb.ConvertZeroDateTime = convertZeroDatetime;
            var conn = new MySqlConnection(csb.ConnectionString);
            if (open)
                conn.Open();
            return conn;
        }

        public async Task<IEnumerable<T>>  GetList()
        {
            var connection = GetMySqlConnection();
            var res = await connection.GetAllAsync<T>();
            connection.Close();
            return res;
        }

        public async Task<T> Get(object id)
        {
            var connection = GetMySqlConnection();
            T res = await connection.GetAsync<T>(id);
            connection.Close();
            return res;
        }

        public async Task Add(T item) 
        {
            var connection = GetMySqlConnection();
            await connection.InsertAsync(item);
            connection.Close();
        }

        public async Task Update(T newItemState)
        {
            var connection = GetMySqlConnection();
            await connection.UpdateAsync(newItemState);
            connection.Close();
        }

        public async Task Delete(object id)
        {
            var connection = GetMySqlConnection();
            var item = await Get(id);
            await connection.DeleteAsync(item);
            connection.Close();
        }

        public async Task<int> Count()
        {
            var connection = GetMySqlConnection();
            var sql = $"SELECT COUNT(*) FROM {typeof(T).Name}";
            var execRes = await connection.QueryAndCloseConnAsync<int>(sql);
            return execRes.FirstOrDefault();
        }
    }
}