using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CONTROLATEC.Modelos;
using SQLite;

namespace CONTROLATEC.Data
{
    public class SQLiteHelper
    {
        
        public SQLiteAsyncConnection db { get; set; }

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<CUENTA>().Wait();
            db.CreateTableAsync<CONCEPTO>().Wait();
            db.CreateTableAsync<CONCEPTO_DETALLE>().Wait();
            db.CreateTableAsync<DIARIO>().Wait();
            db.CreateTableAsync<DIARIO_DETALLE>().Wait();
        }

        public Task<int> InserCuenta(CUENTA cuenta)
        {
            if(cuenta.id== 0)
            {
                return db.InsertAllAsync((System.Collections.IEnumerable)cuenta);
            }

            else
            {
                return null;
            }
          
        }

    }


  

}
