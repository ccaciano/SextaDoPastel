using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SextaDoPastel.Model
{
    public class LoginInput
    {
        public string EMAIL { get; set; }
        public string NICK { get; set; }
        public string SENHA { get; set; }
        public int PERFIL { get; set; }
        public int STATUS { get; set; }
    }
}
