using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Response
{
    public class GetParticipantResponse
    {
        public int idParticipant { get; set; }
        public required Guid idUser { get; set; }
        public required DateTime connectionStart { get; set; }
        public required bool activityStatus { get; set; }
        public required int connectionId { get; set; }
        public int  idSession { get; set; }
    }
}
