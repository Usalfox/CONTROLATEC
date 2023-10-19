using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CONTROLATEC.Modelos
{
    [Table("DIARIO")]
    public class DIARIO
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public int cuenta { get; set; }
        public string fecha { get; set; }
        public string etiqueta { get; set; }
        public string total { get; set; }
        public string status { get; set; }


    }
}
