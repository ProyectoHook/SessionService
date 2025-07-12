using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Request
{
    public class CreateParticipantRequest
    {        
        public required Guid idUser { get; set; }
        public required Guid idSession { get; set; }

    }
}
