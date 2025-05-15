using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AccesCode
    {
        public int idCode { get; set; }

        public required string code { get; set; }
        public required bool status { get; set; }

        public Session Session { get; set; }
    }
}
