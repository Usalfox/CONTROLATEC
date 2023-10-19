using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONTROLATEC.Modelos
{
    [Table("CUENTA")]
    public class CUENTA
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string etiqueta { get; set; }

        /* */
        //Cuenta newCuenta = new Cuenta
        //{
        //    etiqueta = "Ingreso",
             
        // };
   
    }

    
}
