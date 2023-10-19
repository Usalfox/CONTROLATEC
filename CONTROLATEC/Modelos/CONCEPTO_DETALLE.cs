using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONTROLATEC.Modelos
{
    [Table("CONCEPTO_DETALLE")]
    public class CONCEPTO_DETALLE
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int id_concepto { get; set; }
        public string etiqueta { get; set; }
        public decimal valor { get; set; } 
        public string estatus { get; set; }
    }
}
