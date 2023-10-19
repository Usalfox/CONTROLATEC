using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONTROLATEC.Modelos
{
    [Table("CONCEPTO")]
    public class CONCEPTO
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int id_cuenta { get; set; }
        public string tipo { get; set; }
        public string etiqueta { get; set; }
        public string estatus { get; set; }
    }
}
