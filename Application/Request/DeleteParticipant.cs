using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Request
{
    public class DeleteParticipant
    {
        public int idParticipant { get; set; }

        //fk de session
         public int connectionId { get; set; }

    }
}
