using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONTROLATEC.Modelos
{
    [Table("DIARIO_DETALLE")]
    public class DIARIO_DETALLE
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int id_diario { get; set; }
        public int id_concepto_detalle { get; set; }
        public string valor { get; set; }
        public string estatus { get; set; }
    }
}
