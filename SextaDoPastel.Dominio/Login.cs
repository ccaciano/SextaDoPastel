using System;
using System.Collections.Generic;
using System.Text;

namespace SextaDoPastel.Dominio
{
    public class Login
    {
        public int ID_LOG { get; set; }
        public string EMAIL { get; set; }
        public string NICK { get; set; }
        public string SENHA { get; set; }
        public int PERFIL { get; set; }
        public int STATUS { get; set; }
    }
}
