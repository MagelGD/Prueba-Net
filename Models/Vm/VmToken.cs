using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Vm
{
    public class VmToken
    {
        public string Token { get; set; }
        public DateTime exp { get; set; }
        public int idusuario { get; set; }
    }
}
