using SGR.DAL.Contracts;
using SGR.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace SGR.DAL.Concrete
{
    public class DapperManager : IDapperManager
    {
        private readonly IConfiguration config;
        private readonly String conString;
        public DapperManager(IConfiguration config)
        {
            this.config = config;
            conString = config.GetConnectionString("ApplicationConnection");
        }

        public DbConnection GetConnection()
        {
            return new SqlConnection(conString);
        }

        public T Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(conString);            
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        public T Get<T, U>(string sp, Func<T, U, T> lambda, string dataJoin, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(conString);

            var query = db.Query<T, U, T>(sp, lambda, parms, commandType: commandType, splitOn: dataJoin).AsQueryable();

            return query.FirstOrDefault();
        }        

        public List<T> GetAll<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(conString);
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public List<T> GetAll<T, U>(string sp, Func<T, U, T> lambda, string dataJoin, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(conString);

            var query = db.Query<T, U, T>(sp, lambda, parms, commandType: commandType, splitOn: dataJoin).AsQueryable();

            return query.ToList();
        }

        public int Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(conString);
            return db.Execute(sp, parms, commandType: commandType);
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(conString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(conString);
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }


        public void Dispose()
        {

        }
    }
}
